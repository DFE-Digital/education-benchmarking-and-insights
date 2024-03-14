module "benchmark-fa" {
  source                                 = "./modules/functions"
  function-name                          = "benchmark"
  common-tags                            = local.common-tags
  environment-prefix                     = var.environment-prefix
  resource-group-name                    = azurerm_resource_group.resource-group.name
  storage-account-name                   = azurerm_storage_account.platform-storage.name
  storage-account-key                    = azurerm_storage_account.platform-storage.primary_access_key
  key-vault-id                           = data.azurerm_key_vault.key-vault.id
  location                               = var.location
  application-insights-connection-string = data.azurerm_application_insights.application-insights.connection_string
  app-settings = merge(local.default_app_settings, {
    "Cosmos__ConnectionString"            = azurerm_cosmosdb_account.cosmosdb-account.primary_sql_connection_string
    "Cosmos__DatabaseId"                  = azurerm_cosmosdb_sql_database.cosmosdb-container.name
    "Cosmos__LookupCollectionName"        = "fibre-directory"
    "Cosmos__FinancialPlanCollectionName" = "financial-plans"
  })
}

module "insight-fa" {
  source                                 = "./modules/functions"
  function-name                          = "insight"
  common-tags                            = local.common-tags
  environment-prefix                     = var.environment-prefix
  resource-group-name                    = azurerm_resource_group.resource-group.name
  storage-account-name                   = azurerm_storage_account.platform-storage.name
  storage-account-key                    = azurerm_storage_account.platform-storage.primary_access_key
  key-vault-id                           = data.azurerm_key_vault.key-vault.id
  location                               = var.location
  application-insights-connection-string = data.azurerm_application_insights.application-insights.connection_string
  app-settings = merge(local.default_app_settings, {
    "Cosmos__ConnectionString"     = azurerm_cosmosdb_account.cosmosdb-account.primary_readonly_sql_connection_string
    "Cosmos__DatabaseId"           = azurerm_cosmosdb_sql_database.cosmosdb-container.name
    "Cosmos__LookupCollectionName" = "fibre-directory"
  })
}

module "establishment-fa" {
  source                                 = "./modules/functions"
  function-name                          = "establishment"
  common-tags                            = local.common-tags
  environment-prefix                     = var.environment-prefix
  resource-group-name                    = azurerm_resource_group.resource-group.name
  storage-account-name                   = azurerm_storage_account.platform-storage.name
  storage-account-key                    = azurerm_storage_account.platform-storage.primary_access_key
  key-vault-id                           = data.azurerm_key_vault.key-vault.id
  location                               = var.location
  application-insights-connection-string = data.azurerm_application_insights.application-insights.connection_string
  app-settings = merge(local.default_app_settings, {
    "Cosmos__ConnectionString"     = azurerm_cosmosdb_account.cosmosdb-account.primary_readonly_sql_connection_string
    "Cosmos__DatabaseId"           = azurerm_cosmosdb_sql_database.cosmosdb-container.name
    "Cosmos__LookupCollectionName" = "fibre-directory"
    "Search__Name"                 = azurerm_search_service.search.name
    "Search__Key"                  = azurerm_search_service.search.query_keys[0].key
  })
}