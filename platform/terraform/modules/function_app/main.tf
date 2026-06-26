
# Create a storage account
resource "azurerm_storage_account" "func_app_sa" {
  name                     = local.storage-account-name
  resource_group_name      = var.core.resource_group_name
  location                 = var.core.location
  account_tier             = local.storage_account_tier
  account_replication_type = local.storage_account_replication_type
}

# Create a storage container
resource "azurerm_storage_container" "func_app_sc" {
  name                  = "func-app"
  storage_account_id    = azurerm_storage_account.func_app_sa.id
  container_access_type = "private"
}

resource "azurerm_role_assignment" "storage-data-owner" {
  scope                = azurerm_storage_account.func_app_sa.id
  role_definition_name = "Storage Blob Data Owner"
  principal_id         = azurerm_user_assigned_identity.func-identity.principal_id
  principal_type       = "ServicePrincipal"
}

# Create a key vault
resource "azurerm_key_vault" "func_app_kv" {
  name                            = local.key-vault-name
  location                        = var.core.location
  resource_group_name             = var.core.resource_group_name
  enabled_for_deployment          = true
  enabled_for_template_deployment = true
  tenant_id                       = var.identity.tenant_id
  sku_name                        = "standard"
  purge_protection_enabled        = true
  soft_delete_retention_days      = 7

  network_acls {
    default_action = "Allow"
    bypass         = "AzureServices"
  }

  tags = var.core.tags
}

# Create a new default function key
resource "time_rotating" "function_key_rotation" {
  rotation_days = 90
}

resource "random_password" "function_key" {
  length           = 32
  special          = true
  override_special = "-_"
  keepers = {
    rotation_id = time_rotating.function_key_rotation.id
  }
}

# Set up key vault access
resource "azurerm_key_vault_access_policy" "terraform-agent-kv-access" {
  key_vault_id = azurerm_key_vault.func_app_kv.id
  tenant_id    = var.identity.tenant_id
  object_id    = var.identity.object_id

  secret_permissions = [
    "Get",
    "List",
    "Set",
    "Delete",
    "Purge",
    "Restore"
  ]
}

resource "azurerm_user_assigned_identity" "func-identity" {
  name                = local.identity_name
  resource_group_name = var.core.resource_group_name
  location            = var.core.location
  tags                = var.core.tags
}

resource "azurerm_key_vault_access_policy" "func-kv-access" {
  key_vault_id = azurerm_key_vault.func_app_kv.id
  tenant_id    = var.identity.tenant_id
  object_id    = azurerm_user_assigned_identity.func-identity.principal_id

  secret_permissions = [
    "Get",
    "List",
    "Set",
    "Delete"
  ]
}

# Add the default function key to the dedicated key vault
resource "azurerm_key_vault_secret" "default-function-key" {
  name         = "host--functionKey--default"
  value        = random_password.function_key.result
  key_vault_id = azurerm_key_vault.func_app_kv.id

  depends_on = [
    azurerm_key_vault_access_policy.terraform-agent-kv-access
  ]
}

# Add the default function key to the shared key vault
resource "azurerm_key_vault_access_policy" "shared_key_vault_policy" {
  key_vault_id       = var.shared_key_vault.id
  tenant_id          = var.identity.tenant_id
  object_id          = azurerm_user_assigned_identity.func-identity.principal_id
  secret_permissions = ["Get", "List", "Set", "Delete"]
}

resource "azurerm_key_vault_secret" "fa-key" {
  #checkov:skip=CKV_AZURE_41:See ADO backlog AB#206511
  name         = "${var.core.name}-host-key"
  value        = random_password.function_key.result
  key_vault_id = var.shared_key_vault.id
  content_type = "key"
}

resource "azurerm_key_vault_secret" "fa-host" {
  #checkov:skip=CKV_AZURE_41:See ADO backlog AB#206511
  name         = "${var.core.name}-host"
  value        = local.host
  key_vault_id = var.shared_key_vault.id
  content_type = "host"
}

# Create an app service plan
resource "azurerm_service_plan" "func-asp" {
  #checkov:skip=CKV_AZURE_212:See ADO backlog AB#206517
  #checkov:skip=CKV_AZURE_225:See ADO backlog AB#206517
  name                         = "${var.core.environment_prefix}-ebis-${var.core.name}-function-asp"
  location                     = var.core.location
  resource_group_name          = var.core.resource_group_name
  os_type                      = "Linux"
  sku_name                     = var.service_plan.size
  tags                         = var.core.tags
  maximum_elastic_worker_count = var.service_plan.maximum_elastic_worker_count
}

# Create the function app
resource "azurerm_function_app_flex_consumption" "func-app" {
  #checkov:skip=CKV_AZURE_221:See ADO backlog AB#206517
  name                              = local.function-app-name
  location                          = var.core.location
  resource_group_name               = var.core.resource_group_name
  service_plan_id                   = azurerm_service_plan.func-asp.id
  storage_container_type            = "blobContainer"
  storage_container_endpoint        = "${azurerm_storage_account.func_app_sa.primary_blob_endpoint}${azurerm_storage_container.func_app_sc.name}"
  storage_authentication_type       = "UserAssignedIdentity"
  storage_user_assigned_identity_id = azurerm_user_assigned_identity.func-identity.id
  public_network_access_enabled     = true # Allowed for temporary pipeline whitelisting
  runtime_name                      = var.application_stack.worker_runtime
  runtime_version                   = var.application_stack.runtime_version
  https_only                        = true

  identity {
    type         = "UserAssigned"
    identity_ids = [azurerm_user_assigned_identity.func-identity.id]
  }

  site_config {

    application_insights_connection_string = var.monitoring.instrumentation_connection_string
    ip_restriction_default_action = local.ip_default_action

    dynamic "ip_restriction" {
      for_each = local.ip_restrictions
      content {
        virtual_network_subnet_id = ip_restriction.value
      }
    }
  }

  app_settings = local.function-app-settings
  tags         = var.core.tags

  depends_on = [
    azurerm_role_assignment.storage-data-owner,
    azurerm_key_vault_access_policy.func-kv-access,
  ]
}

resource "mssql_user" "app-service-user" {
  server {
    host = var.sql_server.fqdn
    login {
      username = var.sql_server.username
      password = var.sql_server.password
    }
  }

  database  = "data"
  username  = azurerm_function_app_flex_consumption.func-app.name
  object_id = azurerm_user_assigned_identity.func-identity.client_id
  roles     = ["db_datareader", "db_datawriter"]
}

resource "azurerm_redis_cache_access_policy_assignment" "contributor" {
  count              = local.redis_contributor_count
  name               = "${var.core.name}-contributor"
  redis_cache_id     = var.redis_cache.id
  access_policy_name = "Data Contributor"
  object_id          = azurerm_function_app_flex_consumption.func-app.identity[0].principal_id
  object_id_alias    = "${var.core.name}-contributor"
}

resource "azurerm_redis_cache_access_policy_assignment" "owner" {
  count              = local.redis_owner_count
  name               = "${var.core.name}-owner"
  redis_cache_id     = var.redis_cache.id
  access_policy_name = "Data Owner"
  object_id          = azurerm_function_app_flex_consumption.func-app.identity[0].principal_id
  object_id_alias    = "${var.core.name}-owner"
}

resource "azurerm_monitor_diagnostic_setting" "func-app" {
  name                       = "${azurerm_function_app_flex_consumption.func-app.name}-logs"
  target_resource_id         = azurerm_function_app_flex_consumption.func-app.id
  log_analytics_workspace_id = var.monitoring.log_analytics_id

  enabled_metric {
    category = "AllMetrics"
  }

  enabled_log {
    category = "FunctionAppLogs"
  }
}

resource "azurerm_monitor_diagnostic_setting" "func-app-service" {
  name                       = "${azurerm_function_app_flex_consumption.func-app.name}-asp-logs"
  target_resource_id         = azurerm_service_plan.func-asp.id
  log_analytics_workspace_id = var.monitoring.log_analytics_id

  enabled_metric {
    category = "AllMetrics"
  }
}
