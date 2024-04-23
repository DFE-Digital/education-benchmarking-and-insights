resource "azurerm_cosmosdb_account" "session-cache-account" {
  #checkov:skip=CKV_AZURE_100:To be reviewed for production - not set for local dev
  #checkov:skip=CKV_AZURE_101:To be reviewed for production - not set for local dev
  #checkov:skip=CKV_AZURE_132:To be reviewed for production - not set for local dev
  #checkov:skip=CKV_AZURE_140:To be reviewed for production - not set for local dev
  #checkov:skip=CKV_AZURE_99:To be reviewed for production - not set for local dev
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

  virtual_network_rule {
    id = data.azurerm_subnet.web-app-subnet.id
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