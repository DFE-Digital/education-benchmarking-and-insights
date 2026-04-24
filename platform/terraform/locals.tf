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
    "Sql__TelemetryEnabled"    = var.configuration[var.environment].sql_telemetry_enabled
  }

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
    id   = azurerm_storage_account.platform-storage.id
    name = azurerm_storage_account.platform-storage.name
    key  = azurerm_storage_account.platform-storage.primary_access_key
  }
  shared_networking = {
    enable_restrictions = lower(var.cip-environment) != "dev"
    subnet_ids = [
      data.azurerm_subnet.web-app-subnet.id,
      data.azurerm_subnet.load-test-subnet.id
    ]
  }

  # Shared Key Vault references for app settings
  shared_app_settings = {
    search_name         = azurerm_search_service.search.name
    search_key          = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.platform-search-key.versionless_id})"
    sql_connection      = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.core-sql-connection-string.versionless_id})"
    cache_host          = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.cache-host-name.versionless_id})"
    cache_port          = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.cache-ssl-port.versionless_id})"
    use_dotnet_isolated = 1
  }
}
