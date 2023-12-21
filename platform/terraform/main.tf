locals {
  common-tags = {
    "Environment"      = var.cip-environment
    "Service Offering" = "DfE Financial Benchmarking service"
    "Product"          = "DfE Financial Benchmarking service"
    "Source"           = "terraform"
  }
  default_app_settings = {
    "WEBSITE_RUN_FROM_PACKAGE" = "0"
  }
}

data "azurerm_key_vault" "key-vault" {
  name                = "${var.environment-prefix}-ebis-keyvault"
  resource_group_name = "${var.environment-prefix}-ebis-core"
}

data "azurerm_application_insights" "application-insights" {
  name                = "${var.environment-prefix}-ebis-ai"
  resource_group_name = "${var.environment-prefix}-ebis-core"
}

resource "azurerm_resource_group" "resource-group" {
  name     = "${var.environment-prefix}-ebis-platform"
  location = var.location
  tags     = local.common-tags
}

resource "azurerm_storage_account" "platform-storage" {
  name                            = "${var.environment-prefix}platformstorage"
  location                        = azurerm_resource_group.resource-group.location
  resource_group_name             = azurerm_resource_group.resource-group.name
  account_tier                    = "Standard"
  account_replication_type        = "LRS"
  allow_nested_items_to_be_public = false
  tags                            = local.common-tags
}

resource "azurerm_key_vault_secret" "platform-storage-connection-string" {
  name         = "platform-storage-connection-string"
  value        = azurerm_storage_account.platform-storage.primary_connection_string
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

resource "azurerm_cosmosdb_account" "cosmosdb-account" {
  name                = "${var.environment-prefix}-ebis-cdb"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  offer_type          = "Standard"
  kind                = "GlobalDocumentDB"
  consistency_policy {
    consistency_level = "Session"
  }
  tags = local.common-tags
  geo_location {
    failover_priority = 0
    location          = azurerm_resource_group.resource-group.location
  }
}

resource "azurerm_key_vault_secret" "platform-cosmos-connection-string" {
  name         = "ebis-cdb-connection-string"
  value        = azurerm_cosmosdb_account.cosmosdb-account.primary_readonly_sql_connection_string
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

resource "azurerm_cosmosdb_sql_database" "cosmosdb-container" {
  name                = "ebis-data"
  account_name        = azurerm_cosmosdb_account.cosmosdb-account.name
  resource_group_name = azurerm_resource_group.resource-group.name
}

resource "azurerm_search_service" "search" {
  name                = "${var.environment-prefix}-ebis-search"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  sku                 = "standard"
  tags = local.common-tags
}

resource "azurerm_key_vault_secret" "platform-search-key" {
  name         = "ebis-search-admin-key"
  value        = azurerm_search_service.search.primary_key
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

module "benchmark-fa" {
  source                   = "./modules/functions"
  function-name            = "benchmark"
  common-tags              = local.common-tags
  environment-prefix       = var.environment-prefix
  resource-group-name      = azurerm_resource_group.resource-group.name
  storage-account-name     = azurerm_storage_account.platform-storage.name
  storage-account-key      = azurerm_storage_account.platform-storage.primary_access_key
  key-vault-id             = data.azurerm_key_vault.key-vault.id
  location                 = var.location
  application-insights-key = data.azurerm_application_insights.application-insights.instrumentation_key
  app-settings             = local.default_app_settings
}

module "insight-fa" {
  source                   = "./modules/functions"
  function-name            = "insight"
  common-tags              = local.common-tags
  environment-prefix       = var.environment-prefix
  resource-group-name      = azurerm_resource_group.resource-group.name
  storage-account-name     = azurerm_storage_account.platform-storage.name
  storage-account-key      = azurerm_storage_account.platform-storage.primary_access_key
  key-vault-id             = data.azurerm_key_vault.key-vault.id
  location                 = var.location
  application-insights-key = data.azurerm_application_insights.application-insights.instrumentation_key
  app-settings             = merge(local.default_app_settings, {
    "Cosmos__ConnectionString"     = azurerm_cosmosdb_account.cosmosdb-account.primary_readonly_sql_connection_string
    "Cosmos__DatabaseId"           = azurerm_cosmosdb_sql_database.cosmosdb-container.name
    "Cosmos__LookupCollectionName" = "fibre-directory"
  })
}

module "establishment-fa" {
  source                   = "./modules/functions"
  function-name            = "establishment"
  common-tags              = local.common-tags
  environment-prefix       = var.environment-prefix
  resource-group-name      = azurerm_resource_group.resource-group.name
  storage-account-name     = azurerm_storage_account.platform-storage.name
  storage-account-key      = azurerm_storage_account.platform-storage.primary_access_key
  key-vault-id             = data.azurerm_key_vault.key-vault.id
  location                 = var.location
  application-insights-key = data.azurerm_application_insights.application-insights.instrumentation_key
  app-settings             = merge(local.default_app_settings, {
    "Cosmos__ConnectionString"     = azurerm_cosmosdb_account.cosmosdb-account.primary_readonly_sql_connection_string
    "Cosmos__DatabaseId"           = azurerm_cosmosdb_sql_database.cosmosdb-container.name
    "Cosmos__LookupCollectionName" = "fibre-directory"
    "Search__Name"                 = azurerm_search_service.search.name
    "Search__Key"                  = azurerm_search_service.search.query_keys[0].key
  })
}