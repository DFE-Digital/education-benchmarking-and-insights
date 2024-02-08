resource "azurerm_cosmosdb_account" "session-cache-account" {
  name                = "${var.environment-prefix}-ebis-session"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  offer_type          = "Standard"
  kind                = "GlobalDocumentDB"

  consistency_policy {
    consistency_level = "Strong"
  }

  tags = local.common-tags
  geo_location {
    failover_priority = 0
    location          = azurerm_resource_group.resource-group.location
  }

  capabilities {
    name = "EnableServerless"
  }
}

resource "azurerm_cosmosdb_sql_database" "session-cache-database" {
  name                = "session-data"
  account_name        = azurerm_cosmosdb_account.session-cache-account.name
  resource_group_name = azurerm_resource_group.resource-group.name
}

resource "azurerm_cosmosdb_sql_container" "session-cache-container" {
  name                  = "sessions"
  resource_group_name   = azurerm_resource_group.resource-group.name
  account_name          = azurerm_cosmosdb_account.session-cache-account.name
  database_name         = azurerm_cosmosdb_sql_database.session-cache-database.name
  partition_key_path    = "/id"
  partition_key_version = 1
}