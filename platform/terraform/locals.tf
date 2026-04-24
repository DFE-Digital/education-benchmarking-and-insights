locals {
  common-tags = {
    "Environment"      = var.cip-environment
    "Service Offering" = "DfE Financial Benchmarking service"
    "Product"          = "DfE Financial Benchmarking service"
    "Source"           = "terraform"
    "Context"          = var.environment
  }

  default_app_settings = {
    "WEBSITE_RUN_FROM_PACKAGE" = "0"
    "Sql__TelemetryEnabled"    = module.config.sql.telemetry_enabled
  }

  # Storage account definitions
  storage_accounts = toset([
    "platformstorage",
    "orchestrator"
  ])

  # Shared configuration blocks for Function App modules
  shared_monitoring = {
    log_analytics_id                  = data.azurerm_log_analytics_workspace.application-insights-workspace.id
    instrumentation_connection_string = data.azurerm_application_insights.application-insights.connection_string
  }
  shared_key_vault = {
    id = data.azurerm_key_vault.key-vault.id
  }
  shared_sql_server = {
    fqdn     = data.azurerm_mssql_server.sql-server.fully_qualified_domain_name
    username = data.azurerm_key_vault_secret.sql-user-name.value
    password = data.azurerm_key_vault_secret.sql-password.value
  }
  shared_platform_storage = {
    id   = azurerm_storage_account.storage["platformstorage"].id
    name = azurerm_storage_account.storage["platformstorage"].name
    key  = azurerm_storage_account.storage["platformstorage"].primary_access_key
  }
  shared_networking = {
    enable_restrictions = module.config.enable_ip_restrictions
    subnet_ids = [
      data.azurerm_subnet.web-app-subnet.id,
      data.azurerm_subnet.load-test-subnet.id
    ]
  }

  # Shared Key Vault references for app settings
  shared_app_settings = {
    search_name             = azurerm_search_service.search.name
    search_key              = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.platform-search-key.versionless_id})"
    sql_connection          = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.core-sql-connection-string.versionless_id})"
    cache_host              = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.cache-host-name.versionless_id})"
    cache_port              = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.cache-ssl-port.versionless_id})"
    pipeline_hub_connection = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.pipeline-message-hub-storage-connection-string.versionless_id})"
    pipeline_hub_pending    = "data-pipeline-job-pending"
    pipeline_hub_finished   = "data-pipeline-job-finished"
    pipeline_hub_custom     = "data-pipeline-job-custom-start"
    pipeline_hub_default    = "data-pipeline-job-default-start"
    use_dotnet_isolated     = 1
  }
}
