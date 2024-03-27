locals {
  db-connection-string = "Server=tcp:${azurerm_mssql_server.sql-server.fully_qualified_domain_name},1433;Database=${azurerm_mssql_database.sql-db.name};User ID=${azurerm_key_vault_secret.platform-sql-admin-username.value};Password=${azurerm_key_vault_secret.platform-sql-admin-password.value};Trusted_Connection=False;Encrypt=True;"
}

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
    "Cosmos__FinancialPlanCollectionName" = azurerm_cosmosdb_sql_container.cosmosdb-fp-container.name
    "Sql__ConnectionString"               = local.db-connection-string
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
    "Cosmos__ConnectionString"        = azurerm_cosmosdb_account.cosmosdb-account.primary_readonly_sql_connection_string
    "Cosmos__DatabaseId"              = azurerm_cosmosdb_sql_database.cosmosdb-container.name
    "Cosmos__FloorAreaCollectionName" = "Floor-Area-2021-2022"
    "Cosmos__CfrLatestYear"           = 2023,
    "Cosmos__AarLatestYear"           = 2022
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
    "Cosmos__ConnectionString"            = azurerm_cosmosdb_account.cosmosdb-account.primary_readonly_sql_connection_string
    "Cosmos__DatabaseId"                  = azurerm_cosmosdb_sql_database.cosmosdb-container.name
    "Cosmos__EstablishmentCollectionName" = "GIAS"
    "Search__Name"                        = azurerm_search_service.search.name
    "Search__Key"                         = azurerm_search_service.search.query_keys[0].key
  })
}