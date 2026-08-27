# Dedicated Storage
resource "azurerm_storage_account" "analytics_storage" {
  name                     = "${replace(var.environment-prefix, "-", "")}ebisanalytics"
  resource_group_name      = azurerm_resource_group.resource-group.name
  location                 = var.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
  min_tls_version          = "TLS1_2"
  tags                     = local.common-tags
}

# Container
resource "azurerm_storage_container" "log_outputs" {
  name                  = "transformed-log-data"
  storage_account_name  = azurerm_storage_account.analytics_storage.name
  container_access_type = "private"
}

module "log-processor-fc-fa" {
  source = "./modules/fc_function_app"

  app-settings = {
    "LOG_WORKSPACE_ID"          = data.azurerm_log_analytics_workspace.application-insights-workspace.workspace_id
    "BLOB_CONTAINER_NAME"       = azurerm_storage_container.log_outputs.name
    "STORAGE_CONNECTION_STRING" = azurerm_storage_account.analytics_storage.primary_connection_string
    "Sql__ConnectionString"     = local.shared_app_settings.sql_connection #need to address this
  }

  core = {
    name                = "log-processor-fc"
    short_name          = "log"
    environment_prefix  = var.environment-prefix
    resource_group_name = azurerm_resource_group.resource-group.name
    location            = var.location
    tags                = local.common-tags
  }

  monitoring       = local.shared_monitoring
  shared_key_vault = local.shared_key_vault
  sql_server       = local.shared_sql_server

  networking = {
    enable_restrictions = false
    join_subnet_id      = data.azurerm_subnet.platform-subnet.id
    allow_subnet_ids    = [data.azurerm_subnet.web-app-subnet.id]
  }

  application_stack = {
    worker_runtime  = "python"
    runtime_version = "3.12"
  }

  identity = {
    tenant_id = data.azurerm_client_config.client.tenant_id
    object_id = data.azurerm_client_config.client.object_id
  }
}
