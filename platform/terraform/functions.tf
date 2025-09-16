module "benchmark-fa" {
  source                      = "./modules/functions"
  function-name               = "benchmark"
  common-tags                 = local.common-tags
  environment-prefix          = var.environment-prefix
  resource-group-name         = azurerm_resource_group.resource-group.name
  storage-account-name        = azurerm_storage_account.platform-storage.name
  storage-account-id          = azurerm_storage_account.platform-storage.id
  storage-account-key         = azurerm_storage_account.platform-storage.primary_access_key
  key-vault-id                = data.azurerm_key_vault.key-vault.id
  location                    = var.location
  enable-restrictions         = lower(var.cip-environment) != "dev"
  instrumentation-conn-string = data.azurerm_application_insights.application-insights.connection_string
  log-analytics-id            = data.azurerm_log_analytics_workspace.application-insights-workspace.id
  app-settings = merge(local.default_app_settings, {
    "Search__Name"                           = azurerm_search_service.search.name
    "Search__Key"                            = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.platform-search-key.versionless_id})"
    "Sql__ConnectionString"                  = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.core-sql-connection-string.versionless_id})"
    "PipelineMessageHub__ConnectionString"   = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.pipeline-message-hub-storage-connection-string.versionless_id})"
    "PipelineMessageHub__JobPendingQueue"    = "data-pipeline-job-pending"
    "WEBSITE_USE_PLACEHOLDER_DOTNETISOLATED" = 1
  })
  subnet_ids = [
    data.azurerm_subnet.web-app-subnet.id,
    data.azurerm_subnet.load-test-subnet.id
  ]
  sql-server-fqdn     = data.azurerm_mssql_server.sql-server.fully_qualified_domain_name
  sql-server-username = data.azurerm_key_vault_secret.sql-user-name.value
  sql-server-password = data.azurerm_key_vault_secret.sql-password.value
}

module "insight-fa" {
  source                      = "./modules/functions"
  function-name               = "insight"
  common-tags                 = local.common-tags
  environment-prefix          = var.environment-prefix
  resource-group-name         = azurerm_resource_group.resource-group.name
  storage-account-name        = azurerm_storage_account.platform-storage.name
  storage-account-id          = azurerm_storage_account.platform-storage.id
  storage-account-key         = azurerm_storage_account.platform-storage.primary_access_key
  key-vault-id                = data.azurerm_key_vault.key-vault.id
  location                    = var.location
  enable-restrictions         = lower(var.cip-environment) != "dev"
  instrumentation-conn-string = data.azurerm_application_insights.application-insights.connection_string
  log-analytics-id            = data.azurerm_log_analytics_workspace.application-insights-workspace.id
  app-settings = merge(local.default_app_settings, {
    "Sql__ConnectionString"                  = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.core-sql-connection-string.versionless_id})"
    "WEBSITE_USE_PLACEHOLDER_DOTNETISOLATED" = 1
    "Cache__Host"                            = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.cache-host-name.versionless_id})"
    "Cache__Port"                            = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.cache-ssl-port.versionless_id})"
  })
  subnet_ids = [
    data.azurerm_subnet.web-app-subnet.id,
    data.azurerm_subnet.load-test-subnet.id
  ]
  sql-server-fqdn     = data.azurerm_mssql_server.sql-server.fully_qualified_domain_name
  sql-server-username = data.azurerm_key_vault_secret.sql-user-name.value
  sql-server-password = data.azurerm_key_vault_secret.sql-password.value
  redis-cache-id      = azurerm_redis_cache.cache.id
  cache-contributor   = true
}

module "establishment-fa" {
  source                      = "./modules/functions"
  function-name               = "establishment"
  common-tags                 = local.common-tags
  environment-prefix          = var.environment-prefix
  resource-group-name         = azurerm_resource_group.resource-group.name
  storage-account-name        = azurerm_storage_account.platform-storage.name
  storage-account-id          = azurerm_storage_account.platform-storage.id
  storage-account-key         = azurerm_storage_account.platform-storage.primary_access_key
  key-vault-id                = data.azurerm_key_vault.key-vault.id
  location                    = var.location
  enable-restrictions         = lower(var.cip-environment) != "dev"
  instrumentation-conn-string = data.azurerm_application_insights.application-insights.connection_string
  log-analytics-id            = data.azurerm_log_analytics_workspace.application-insights-workspace.id
  app-settings = merge(local.default_app_settings, {
    "Search__Name"                           = azurerm_search_service.search.name
    "Search__Key"                            = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.platform-search-key.versionless_id})"
    "Sql__ConnectionString"                  = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.core-sql-connection-string.versionless_id})"
    "WEBSITE_USE_PLACEHOLDER_DOTNETISOLATED" = 1
  })
  subnet_ids = [
    data.azurerm_subnet.web-app-subnet.id,
    data.azurerm_subnet.load-test-subnet.id
  ]
  sql-server-fqdn     = data.azurerm_mssql_server.sql-server.fully_qualified_domain_name
  sql-server-username = data.azurerm_key_vault_secret.sql-user-name.value
  sql-server-password = data.azurerm_key_vault_secret.sql-password.value
}

module "local-authority-finances-fa" {
  source                      = "./modules/functions"
  function-name               = "local-authority-finances"
  common-tags                 = local.common-tags
  environment-prefix          = var.environment-prefix
  resource-group-name         = azurerm_resource_group.resource-group.name
  storage-account-name        = azurerm_storage_account.platform-storage.name
  storage-account-id          = azurerm_storage_account.platform-storage.id
  storage-account-key         = azurerm_storage_account.platform-storage.primary_access_key
  key-vault-id                = data.azurerm_key_vault.key-vault.id
  location                    = var.location
  enable-restrictions         = lower(var.cip-environment) != "dev"
  instrumentation-conn-string = data.azurerm_application_insights.application-insights.connection_string
  log-analytics-id            = data.azurerm_log_analytics_workspace.application-insights-workspace.id
  app-settings = merge(local.default_app_settings, {
    "Sql__ConnectionString"                  = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.core-sql-connection-string.versionless_id})"
    "WEBSITE_USE_PLACEHOLDER_DOTNETISOLATED" = 1
  })
  subnet_ids = [
    data.azurerm_subnet.web-app-subnet.id,
    data.azurerm_subnet.load-test-subnet.id
  ]
  sql-server-fqdn     = data.azurerm_mssql_server.sql-server.fully_qualified_domain_name
  sql-server-username = data.azurerm_key_vault_secret.sql-user-name.value
  sql-server-password = data.azurerm_key_vault_secret.sql-password.value
}

module "non-financial-fa" {
  source                      = "./modules/functions"
  function-name               = "non-financial"
  common-tags                 = local.common-tags
  environment-prefix          = var.environment-prefix
  resource-group-name         = azurerm_resource_group.resource-group.name
  storage-account-name        = azurerm_storage_account.platform-storage.name
  storage-account-id          = azurerm_storage_account.platform-storage.id
  storage-account-key         = azurerm_storage_account.platform-storage.primary_access_key
  key-vault-id                = data.azurerm_key_vault.key-vault.id
  location                    = var.location
  enable-restrictions         = lower(var.cip-environment) != "dev"
  instrumentation-conn-string = data.azurerm_application_insights.application-insights.connection_string
  log-analytics-id            = data.azurerm_log_analytics_workspace.application-insights-workspace.id
  app-settings = merge(local.default_app_settings, {
    "Sql__ConnectionString"                  = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.core-sql-connection-string.versionless_id})"
    "WEBSITE_USE_PLACEHOLDER_DOTNETISOLATED" = 1
  })
  subnet_ids = [
    data.azurerm_subnet.web-app-subnet.id,
    data.azurerm_subnet.load-test-subnet.id
  ]
  sql-server-fqdn     = data.azurerm_mssql_server.sql-server.fully_qualified_domain_name
  sql-server-username = data.azurerm_key_vault_secret.sql-user-name.value
  sql-server-password = data.azurerm_key_vault_secret.sql-password.value
}

module "chart-rendering-fa" {
  source                      = "./modules/functions"
  function-name               = "chart-rendering"
  common-tags                 = local.common-tags
  environment-prefix          = var.environment-prefix
  resource-group-name         = azurerm_resource_group.resource-group.name
  storage-account-name        = azurerm_storage_account.platform-storage.name
  storage-account-id          = azurerm_storage_account.platform-storage.id
  storage-account-key         = azurerm_storage_account.platform-storage.primary_access_key
  key-vault-id                = data.azurerm_key_vault.key-vault.id
  location                    = var.location
  enable-restrictions         = lower(var.cip-environment) != "dev"
  instrumentation-conn-string = data.azurerm_application_insights.application-insights.connection_string
  log-analytics-id            = data.azurerm_log_analytics_workspace.application-insights-workspace.id
  app-settings = merge(local.default_app_settings, {
    "FUNCTIONS_WORKER_PROCESS_COUNT"      = var.configuration[var.environment].ssr_fa_worker_process_count
    "WEBSITES_ENABLE_APP_SERVICE_STORAGE" = true
  })
  subnet_ids = [
    data.azurerm_subnet.web-app-subnet.id,
    data.azurerm_subnet.load-test-subnet.id
  ]
  sql-server-fqdn     = data.azurerm_mssql_server.sql-server.fully_qualified_domain_name
  sql-server-username = data.azurerm_key_vault_secret.sql-user-name.value
  sql-server-password = data.azurerm_key_vault_secret.sql-password.value
  os-type             = "Linux"
  worker-runtime      = "node"
  sku = {
    size = var.configuration[var.environment].ssr_fa_sku
  }
  maximum-elastic-worker-count   = var.configuration[var.environment].ssr_fa_elastic_max_workers
  minimum-elastic-instance-count = var.configuration[var.environment].ssr_fa_elastic_min_instances
}

module "content-fa" {
  source                      = "./modules/functions"
  function-name               = "content"
  common-tags                 = local.common-tags
  environment-prefix          = var.environment-prefix
  resource-group-name         = azurerm_resource_group.resource-group.name
  storage-account-name        = azurerm_storage_account.platform-storage.name
  storage-account-id          = azurerm_storage_account.platform-storage.id
  storage-account-key         = azurerm_storage_account.platform-storage.primary_access_key
  key-vault-id                = data.azurerm_key_vault.key-vault.id
  location                    = var.location
  enable-restrictions         = lower(var.cip-environment) != "dev"
  instrumentation-conn-string = data.azurerm_application_insights.application-insights.connection_string
  log-analytics-id            = data.azurerm_log_analytics_workspace.application-insights-workspace.id
  app-settings = merge(local.default_app_settings, {
    "Sql__ConnectionString"                  = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.core-sql-connection-string.versionless_id})"
    "WEBSITE_USE_PLACEHOLDER_DOTNETISOLATED" = 1
  })
  subnet_ids = [
    data.azurerm_subnet.web-app-subnet.id,
    data.azurerm_subnet.load-test-subnet.id
  ]
  sql-server-fqdn     = data.azurerm_mssql_server.sql-server.fully_qualified_domain_name
  sql-server-username = data.azurerm_key_vault_secret.sql-user-name.value
  sql-server-password = data.azurerm_key_vault_secret.sql-password.value
}

module "maintenance-tasks-fa" {
  source                      = "./modules/functions"
  function-name               = "maintenance-tasks"
  common-tags                 = local.common-tags
  environment-prefix          = var.environment-prefix
  resource-group-name         = azurerm_resource_group.resource-group.name
  storage-account-name        = azurerm_storage_account.platform-storage.name
  storage-account-id          = azurerm_storage_account.platform-storage.id
  storage-account-key         = azurerm_storage_account.platform-storage.primary_access_key
  key-vault-id                = data.azurerm_key_vault.key-vault.id
  location                    = var.location
  enable-restrictions         = false
  instrumentation-conn-string = data.azurerm_application_insights.application-insights.connection_string
  log-analytics-id            = data.azurerm_log_analytics_workspace.application-insights-workspace.id
  app-settings = merge(local.default_app_settings, {
    "Sql__ConnectionString" = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.core-sql-connection-string.versionless_id})"
  })
  subnet_ids          = [data.azurerm_subnet.web-app-subnet.id]
  sql-server-fqdn     = data.azurerm_mssql_server.sql-server.fully_qualified_domain_name
  sql-server-username = data.azurerm_key_vault_secret.sql-user-name.value
  sql-server-password = data.azurerm_key_vault_secret.sql-password.value
}

module "orchestrator-fa" {
  source               = "./modules/functions"
  function-name        = "orchestrator"
  common-tags          = local.common-tags
  environment-prefix   = var.environment-prefix
  resource-group-name  = azurerm_resource_group.resource-group.name
  storage-account-name = azurerm_storage_account.orchestrator-storage.name
  storage-account-id   = azurerm_storage_account.orchestrator-storage.id
  storage-account-key  = azurerm_storage_account.orchestrator-storage.primary_access_key
  key-vault-id         = data.azurerm_key_vault.key-vault.id
  location             = var.location
  enable-restrictions  = lower(var.cip-environment) != "dev"
  always-on            = true
  sku = {
    size = "P0v3"
  }
  instrumentation-conn-string = data.azurerm_application_insights.application-insights.connection_string
  log-analytics-id            = data.azurerm_log_analytics_workspace.application-insights-workspace.id
  app-settings = merge(local.default_app_settings, {
    "PipelineMessageHub__ConnectionString"     = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.pipeline-message-hub-storage-connection-string.versionless_id})"
    "PipelineMessageHub__JobFinishedQueue"     = "data-pipeline-job-finished"
    "PipelineMessageHub__JobCustomStartQueue"  = "data-pipeline-job-custom-start"
    "PipelineMessageHub__JobDefaultStartQueue" = "data-pipeline-job-default-start"
    "PipelineMessageHub__JobPendingQueue"      = "data-pipeline-job-pending"
    "Sql__ConnectionString"                    = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.core-sql-connection-string.versionless_id})"
    "Search__Name"                             = azurerm_search_service.search.name
    "Search__Key"                              = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.platform-search-key.versionless_id})"
    "Cache__Host"                              = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.cache-host-name.versionless_id})"
    "Cache__Port"                              = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.cache-ssl-port.versionless_id})"
  })
  subnet_ids          = [data.azurerm_subnet.web-app-subnet.id]
  sql-server-fqdn     = data.azurerm_mssql_server.sql-server.fully_qualified_domain_name
  sql-server-username = data.azurerm_key_vault_secret.sql-user-name.value
  sql-server-password = data.azurerm_key_vault_secret.sql-password.value
  redis-cache-id      = azurerm_redis_cache.cache.id
  cache-contributor   = true
}
