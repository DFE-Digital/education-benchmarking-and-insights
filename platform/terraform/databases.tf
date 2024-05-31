resource "azurerm_cosmosdb_account" "cosmosdb-account" {
  #checkov:skip=CKV_AZURE_100:See ADO backlog AB#206519
  #checkov:skip=CKV_AZURE_101:See ADO backlog AB#206519
  #checkov:skip=CKV_AZURE_132:See ADO backlog AB#206519
  #checkov:skip=CKV_AZURE_140:See ADO backlog AB#206519
  #checkov:skip=CKV_AZURE_99:See ADO backlog AB#206519
  name                = "${var.environment-prefix}-ebis-cdb"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  offer_type          = "Standard"
  kind                = "GlobalDocumentDB"
  tags                = local.common-tags

  # Azure services, plus DFE VPN remote range if non-production
  ip_range_filter = var.environment == "production" ? "0.0.0.0" : "0.0.0.0,208.127.46.236/30,208.127.46.240/28"

  consistency_policy {
    consistency_level = "Session"
  }

  geo_location {
    failover_priority = 0
    location          = azurerm_resource_group.resource-group.location
  }

  dynamic "capabilities" {
    for_each = var.configuration[var.environment].cosmos.capabilities
    content {
      name = capabilities.value
    }
  }

  lifecycle {
    ignore_changes = [
      ip_range_filter
    ]
  }
}

#TODO: rename resource - it's database not container
resource "azurerm_cosmosdb_sql_database" "cosmosdb-container" {
  name                = "ebis-data"
  account_name        = azurerm_cosmosdb_account.cosmosdb-account.name
  resource_group_name = azurerm_resource_group.resource-group.name
}

resource "azurerm_key_vault_secret" "platform-cosmos-read-connection-string" {
  #checkov:skip=CKV_AZURE_41:See ADO backlog AB#206511
  name         = "ebis-cdb-connection-string-r"
  value        = azurerm_cosmosdb_account.cosmosdb-account.primary_readonly_sql_connection_string
  key_vault_id = data.azurerm_key_vault.key-vault.id
  content_type = "connection-string"
}

resource "azurerm_key_vault_secret" "platform-cosmos-readwrite-connection-string" {
  #checkov:skip=CKV_AZURE_41:See ADO backlog AB#206511
  name         = "ebis-cdb-connection-string-rw"
  value        = azurerm_cosmosdb_account.cosmosdb-account.primary_sql_connection_string
  key_vault_id = data.azurerm_key_vault.key-vault.id
  content_type = "connection-string"
}