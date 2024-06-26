module "benchmark-fa" {
  source               = "./modules/functions"
  function-name        = "benchmark"
  common-tags          = local.common-tags
  environment-prefix   = var.environment-prefix
  resource-group-name  = azurerm_resource_group.resource-group.name
  storage-account-name = azurerm_storage_account.platform-storage.name
  storage-account-id   = azurerm_storage_account.platform-storage.id
  storage-account-key  = azurerm_storage_account.platform-storage.primary_access_key
  key-vault-id         = data.azurerm_key_vault.key-vault.id
  location             = var.location
  enable-restrictions  = lower(var.cip-environment) != "dev"
  instrumentation-key  = data.azurerm_application_insights.application-insights.instrumentation_key
  app-settings = merge(local.default_app_settings, {
    "Search__Name"                         = azurerm_search_service.search.name
    "Search__Key"                          = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.platform-search-key.versionless_id})"
    "Sql__ConnectionString"                = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.core-sql-connection-string.versionless_id})"
    "PipelineMessageHub__ConnectionString" = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.pipeline-message-hub-storage-connection-string.versionless_id})"
    "PipelineMessageHub__JobPendingQueue"  = "data-pipeline-job-pending"
  })
  subnet_id = data.azurerm_subnet.web-app-subnet.id
}

module "insight-fa" {
  source               = "./modules/functions"
  function-name        = "insight"
  common-tags          = local.common-tags
  environment-prefix   = var.environment-prefix
  resource-group-name  = azurerm_resource_group.resource-group.name
  storage-account-name = azurerm_storage_account.platform-storage.name
  storage-account-id   = azurerm_storage_account.platform-storage.id
  storage-account-key  = azurerm_storage_account.platform-storage.primary_access_key
  key-vault-id         = data.azurerm_key_vault.key-vault.id
  location             = var.location
  enable-restrictions  = lower(var.cip-environment) != "dev"
  instrumentation-key  = data.azurerm_application_insights.application-insights.instrumentation_key
  app-settings = merge(local.default_app_settings, {
    "Sql__ConnectionString" = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.core-sql-connection-string.versionless_id})"
  })
  subnet_id = data.azurerm_subnet.web-app-subnet.id
}

module "establishment-fa" {
  source               = "./modules/functions"
  function-name        = "establishment"
  common-tags          = local.common-tags
  environment-prefix   = var.environment-prefix
  resource-group-name  = azurerm_resource_group.resource-group.name
  storage-account-name = azurerm_storage_account.platform-storage.name
  storage-account-id   = azurerm_storage_account.platform-storage.id
  storage-account-key  = azurerm_storage_account.platform-storage.primary_access_key
  key-vault-id         = data.azurerm_key_vault.key-vault.id
  location             = var.location
  enable-restrictions  = lower(var.cip-environment) != "dev"
  instrumentation-key  = data.azurerm_application_insights.application-insights.instrumentation_key
  app-settings = merge(local.default_app_settings, {
    "Search__Name"          = azurerm_search_service.search.name
    "Search__Key"           = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.platform-search-key.versionless_id})"
    "Sql__ConnectionString" = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.core-sql-connection-string.versionless_id})"
  })
  subnet_id = data.azurerm_subnet.web-app-subnet.id
}

module "data-clean-up-fa" {
  source               = "./modules/functions"
  function-name        = "clean-up"
  common-tags          = local.common-tags
  environment-prefix   = var.environment-prefix
  resource-group-name  = azurerm_resource_group.resource-group.name
  storage-account-name = azurerm_storage_account.platform-storage.name
  storage-account-id   = azurerm_storage_account.platform-storage.id
  storage-account-key  = azurerm_storage_account.platform-storage.primary_access_key
  key-vault-id         = data.azurerm_key_vault.key-vault.id
  location             = var.location
  enable-restrictions  = false
  instrumentation-key  = data.azurerm_application_insights.application-insights.instrumentation_key
  app-settings = merge(local.default_app_settings, {
    "Sql__ConnectionString" = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.core-sql-connection-string.versionless_id})"
  })
  subnet_id = data.azurerm_subnet.web-app-subnet.id
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
  instrumentation-key = data.azurerm_application_insights.application-insights.instrumentation_key
  app-settings = merge(local.default_app_settings, {
    "PipelineMessageHub__ConnectionString" = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.pipeline-message-hub-storage-connection-string.versionless_id})"
    "PipelineMessageHub__JobFinishedQueue" = "data-pipeline-job-finished"
    "PipelineMessageHub__JobStartQueue"    = "data-pipeline-job-start"
    "PipelineMessageHub__JobPendingQueue"  = "data-pipeline-job-pending"
    "Sql__ConnectionString"                = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.core-sql-connection-string.versionless_id})"
  })
  subnet_id = data.azurerm_subnet.web-app-subnet.id
}
