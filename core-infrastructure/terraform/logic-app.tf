resource "azurerm_logic_app_workflow" "database-backup" {
  name                = "${var.environment-prefix}-ebis-database-backup"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  tags                = local.common-tags

  identity {
    type = "SystemAssigned"
  }

  workflow_parameters = {
    SubscriptionName = jsonencode({
      type         = "string"
      defaultValue = "${data.azurerm_subscription.current.display_name}"
      description  = "The Azure Subscription name where the databases exists to backup."
    })
    ResourceGroup = jsonencode({
      type         = "string"
      defaultValue = "${azurerm_resource_group.resource-group.name}"
      description  = "The resource group name where the source database exists to backup."
    })
    KeyVaultName = jsonencode({
      type         = "string"
      defaultValue = "${azurerm_key_vault.key-vault.name}"
      description  = "The key vault name where secrets may be resolved from."
    })
    ServerName = jsonencode({
      type         = "string"
      defaultValue = "${azurerm_mssql_server.sql-server.name}"
      description  = "The SQL Server that contain the database that you want to backup."
    })
    DatabaseName = jsonencode({
      type         = "string"
      defaultValue = "${azurerm_mssql_database.sql-db.name}"
      description  = "Name of the database that should be backed up."
    })
    DatabaseUsernameSecret = jsonencode({
      type         = "string"
      defaultValue = "${azurerm_key_vault_secret.sql-user-name.name}"
      description  = "Key Vault secret name coresponding to the destination SQL Server administrator username."
    })
    DatabasePasswordSecret = jsonencode({
      type         = "string"
      defaultValue = "${azurerm_key_vault_secret.sql-password.name}"
      description  = "Key Vault secret name coresponding to the destination SQL Server administrator password."
    })
    StorageAccountName = jsonencode({
      type         = "string"
      defaultValue = "${azurerm_storage_account.backup.name}"
      description  = "The StorageAccount that should hold the BACPAC file for backup."
    })
    StorageKeySecret = jsonencode({
      type         = "string"
      defaultValue = "${azurerm_key_vault_secret.backup-storage-key.name}"
      description  = "Key Vault secret name coresponding to the storage account access key."
    })
    ContainerName = jsonencode({
      type         = "string"
      defaultValue = "${azurerm_storage_container.pipeline-database-backup.name}"
      description  = "The StorageAccount container name that should hold the BACPAC file backup."
    })
    ExpirySeconds = jsonencode({
      type         = "int"
      defaultValue = 604800
      description  = "Specifies expiry period in seconds assigned to the BACPAC file stored in blob storage."
    })
    "$connections" = jsonencode({
      type         = "Object"
      defaultValue = {}
    })
  }
}

resource "azurerm_key_vault_access_policy" "logic-app-access-policy" {
  key_vault_id       = azurerm_key_vault.key-vault.id
  tenant_id          = azurerm_logic_app_workflow.database-backup.identity[0].tenant_id
  object_id          = azurerm_logic_app_workflow.database-backup.identity[0].principal_id
  secret_permissions = ["Get"]
}

resource "azurerm_role_assignment" "logic-app-role-blob" {
  scope                = azurerm_storage_account.backup.id
  role_definition_name = "Storage Blob Data Contributor"
  principal_id         = azurerm_logic_app_workflow.database-backup.identity[0].principal_id
  principal_type       = "ServicePrincipal"
}
