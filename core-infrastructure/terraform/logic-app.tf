resource "azurerm_logic_app_workflow" "database-backup" {
  name                = "${var.environment-prefix}-ebis-database-backup"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  tags                = local.common-tags

  identity {
    type = "SystemAssigned"
  }

  workflow_parameters = {
    SubscriptionId = jsonencode({
      type         = "String"
      defaultValue = ""
      description  = "The Azure Subscription ID where the databases exists to backup."
    })
    ResourceGroup = jsonencode({
      type         = "String"
      defaultValue = ""
      description  = "The resource group name where the source database exists to backup."
    })
    KeyVaultName = jsonencode({
      type         = "String"
      defaultValue = ""
      description  = "The key vault name where secrets may be resolved from."
    })
    ServerName = jsonencode({
      type         = "String"
      defaultValue = ""
      description  = "The SQL Server that contain the database that you want to backup."
    })
    DatabaseName = jsonencode({
      type         = "String"
      defaultValue = ""
      description  = "Name of the database that should be backed up."
    })
    DatabaseUsernameSecret = jsonencode({
      type         = "String"
      defaultValue = ""
      description  = "Key Vault secret name coresponding to the destination SQL Server administrator username."
    })
    DatabasePasswordSecret = jsonencode({
      type         = "String"
      defaultValue = ""
      description  = "Key Vault secret name coresponding to the destination SQL Server administrator password."
    })
    StorageAccountName = jsonencode({
      type         = "String"
      defaultValue = ""
      description  = "The StorageAccount that should hold the BACPAC file for backup."
    })
    StorageKeySecret = jsonencode({
      type         = "String"
      defaultValue = ""
      description  = "Key Vault secret name coresponding to the storage account access key."
    })
    ContainerName = jsonencode({
      type         = "String"
      defaultValue = ""
      description  = "The StorageAccount container name that should hold the BACPAC file backup."
    })
    ExpirySeconds = jsonencode({
      type         = "Int"
      defaultValue = 0
      description  = "Specifies expiry period in seconds assigned to the BACPAC file stored in blob storage."
    })
    "$connections" = jsonencode({
      type         = "Object"
      defaultValue = {}
    })
  }

  parameters = {
    SubscriptionId         = "${data.azurerm_subscription.current.subscription_id}"
    ResourceGroup          = "${azurerm_resource_group.resource-group.name}"
    KeyVaultName           = "${azurerm_key_vault.key-vault.name}"
    ServerName             = "${azurerm_mssql_server.sql-server.name}"
    DatabaseName           = "${azurerm_mssql_database.sql-db.name}"
    DatabaseUsernameSecret = "${azurerm_key_vault_secret.sql-user-name.name}"
    DatabasePasswordSecret = "${azurerm_key_vault_secret.sql-password.name}"
    StorageAccountName     = "${azurerm_storage_account.backup.name}"
    StorageKeySecret       = "${azurerm_key_vault_secret.backup-storage-key.name}"
    ContainerName          = "${azurerm_storage_container.pipeline-database-backup.name}"
    ExpirySeconds          = 604800

    "$connections" = jsonencode({
      keyvault = {
        connectionId   = "/subscriptions/${data.azurerm_subscription.current.subscription_id}/resourceGroups/${azurerm_resource_group.resource-group.name}/providers/Microsoft.Web/connections/keyvault"
        connectionName = "keyvault"
        connectionProperties = {
          authentication = {
            type = "ManagedServiceIdentity"
          }
        }
        id = "/subscriptions/${data.azurerm_subscription.current.subscription_id}/providers/Microsoft.Web/locations/westeurope/managedApis/keyvault"
      }
    })
  }
}

resource "azurerm_key_vault_access_policy" "logic-app-access-policy" {
  key_vault_id       = azurerm_key_vault.key-vault.id
  tenant_id          = azurerm_logic_app_workflow.database-backup.identity[0].tenant_id
  object_id          = azurerm_logic_app_workflow.database-backup.identity[0].principal_id
  secret_permissions = ["Get"]
}

resource "azurerm_role_definition" "logic-app-database-export-definition" {
  name  = "logic-app-database-export-policy"
  scope = azurerm_mssql_database.sql-db.id

  permissions {
    actions     = ["Microsoft.Sql/servers/databases/export/action"]
    not_actions = []
  }

  assignable_scopes = [
    azurerm_mssql_database.sql-db.id,
  ]
}

# CIP will block this
# resource "azurerm_role_assignment" "logic-app-database-export-assignment" {
#   scope              = azurerm_mssql_database.sql-db.id
#   role_definition_id = azurerm_role_definition.logic-app-database-export-definition.role_definition_resource_id
#   principal_id       = azurerm_logic_app_workflow.database-backup.identity[0].principal_id
# }

resource "azurerm_role_assignment" "logic-app-role-blob" {
  scope                = azurerm_storage_account.backup.id
  role_definition_name = "Storage Blob Data Contributor"
  principal_id         = azurerm_logic_app_workflow.database-backup.identity[0].principal_id
  principal_type       = "ServicePrincipal"
}

resource "azurerm_logic_app_trigger_recurrence" "start" {
  name         = "daily-schedule"
  logic_app_id = azurerm_logic_app_workflow.database-backup.id
  frequency    = "Day"
  interval     = 1

  schedule {
    at_these_hours   = [0]
    at_these_minutes = [0]
  }
}

resource "azurerm_logic_app_action_custom" "get-database-username" {
  name         = "Get_database_username"
  logic_app_id = azurerm_logic_app_workflow.database-backup.id

  body = <<BODY
{
    "description": "Gets the database username from Key Vault",
    "inputs": {
        "host": {
            "connection": {
                "name": "@parameters('$connections')['keyvault']['connectionId']"
            }
        },
        "method": "get",
        "path": "/secrets/@{encodeURIComponent(parameters('DatabaseUsernameSecret'))}/value"
    },
    "runAfter": {},
    "type": "ApiConnection"
}
BODY

}

resource "azurerm_logic_app_action_custom" "get-database-password" {
  name         = "Get_database_password"
  logic_app_id = azurerm_logic_app_workflow.database-backup.id

  body = <<BODY
{
    "description": "Gets the database password from Key Vault",
    "inputs": {
        "host": {
            "connection": {
                "name": "@parameters('$connections')['keyvault']['connectionId']"
            }
        },
        "method": "get",
        "path": "/secrets/@{encodeURIComponent(parameters('DatabasePasswordSecret'))}/value"
    },
    "runAfter": {},
    "runtimeConfiguration": {
        "secureData": {
            "properties": [
                "inputs",
                "outputs"
            ]
        }
    },
    "type": "ApiConnection"
}
BODY

}

resource "azurerm_logic_app_action_custom" "get-storage-key-secret" {
  name         = "Get_storage_key_secret"
  logic_app_id = azurerm_logic_app_workflow.database-backup.id

  body = <<BODY
{
    "description": "Gets the storage key secret from Key Vault",
    "inputs": {
        "host": {
            "connection": {
                "name": "@parameters('$connections')['keyvault']['connectionId']"
            }
        },
        "method": "get",
        "path": "/secrets/@{encodeURIComponent(parameters('StorageKeySecret'))}/value"
    },
    "runAfter": {},
    "runtimeConfiguration": {
        "secureData": {
            "properties": [
                "inputs",
                "outputs"
            ]
        }
    },
    "type": "ApiConnection"
}
BODY

}

resource "azurerm_logic_app_action_custom" "post-azure-api" {
  name         = "POST_to_Azure_API"
  logic_app_id = azurerm_logic_app_workflow.database-backup.id

  body = <<BODY
{
    "description": "Initiates SQL Server database export to a blob storage container via API call",
    "inputs": {
      "uri": "https://management.azure.com/subscriptions/@{parameters('SubscriptionId')}/resourceGroups/@{parameters('ResourceGroup')}/providers/Microsoft.Sql/servers/@{parameters('ServerName')}/databases/@{parameters('DatabaseName')}/export?api-version=2021-11-01",
      "method": "POST",
      "headers": {
        "Content-Type": "application/json"
      },
      "body": {
        "administratorLogin": "@{body('Get_database_username')?['value']}",
        "administratorLoginPassword": "@{body('Get_database_password')?['value']}",
        "authenticationType": "Sql",
        "storageKey": "@{body('Get_storage_key_secret')?['value']}",
        "storageKeyType": "StorageAccessKey",
        "storageUri": "https://@{parameters('StorageAccountName')}.blob.core.windows.net/@{parameters('ContainerName')}/@{parameters('DatabaseName')}_@{utcNow()}.bacpac"
      },
      "authentication": {
        "type": "ManagedServiceIdentity"
      }
    },
    "runAfter": {
      "Get_database_username": [
        "Succeeded"
      ],
      "Get_database_password": [
        "Succeeded"
      ],
      "Get_storage_key_secret": [
        "Succeeded"
      ]
    },
    "runtimeConfiguration": {
      "contentTransfer": {
        "transferMode": "Chunked"
      },
      "secureData": {
        "properties": [
          "inputs"
        ]
      }
    },
    "type": "http"
}
BODY

}
