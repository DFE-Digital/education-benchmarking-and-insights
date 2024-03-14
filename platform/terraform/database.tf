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

  dynamic "capabilities" {
    for_each = var.configuration[var.environment].cosmos.capabilities
    content {
      name = capabilities.value
    }
  }
}

resource "azurerm_key_vault_secret" "platform-cosmos-connection-string" {
  #checkov:skip=CKV_AZURE_41:Secrets expiration dto be reviewed
  name         = "ebis-cdb-connection-string"
  value        = azurerm_cosmosdb_account.cosmosdb-account.primary_readonly_sql_connection_string
  key_vault_id = data.azurerm_key_vault.key-vault.id
  content_type = "connection-string"
}

resource "azurerm_cosmosdb_sql_database" "cosmosdb-container" {
  name                = "ebis-data"
  account_name        = azurerm_cosmosdb_account.cosmosdb-account.name
  resource_group_name = azurerm_resource_group.resource-group.name
}

resource "random_password" "sql_admin_password" {
  length           = 16
  special          = true
  override_special = "!@#$*()-_=+"
}

resource "azurerm_key_vault_secret" "platform-sql-admin-username" {
  #checkov:skip=CKV_AZURE_41:Secrets expiration dto be reviewed
  name         = "ebis-sql-admin-username"
  value        = "ebis-sql-admin"
  key_vault_id = data.azurerm_key_vault.key-vault.id
  content_type = "username"
}

resource "azurerm_key_vault_secret" "platform-sql-admin-password" {
  #checkov:skip=CKV_AZURE_41:Secrets expiration dto be reviewed
  name         = "ebis-sql-admin-password"
  value        = random_password.sql_admin_password.result
  key_vault_id = data.azurerm_key_vault.key-vault.id
  content_type = "password"
}

resource "azurerm_mssql_server" "sql_server" {
  name                         = "${var.environment-prefix}-ebis-sql"
  version                      = "12.0"
  resource_group_name          = azurerm_resource_group.resource-group.name
  location                     = azurerm_resource_group.resource-group.location
  administrator_login          = azurerm_key_vault_secret.platform-sql-admin-username.value
  administrator_login_password = random_password.sql_admin_password.result
  tags                         = local.common-tags
}

resource "azurerm_mssql_database" "sql_db" {
  name        = "ebis-data"
  server_id   = azurerm_mssql_server.sql_server.id
  tags        = local.common-tags
  sku_name    = "S0"
  max_size_gb = 5
}