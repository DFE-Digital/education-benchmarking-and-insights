data "azurerm_key_vault" "key-vault" {
  name                = "${var.dev-environment-prefix}-ebis-keyvault"
  resource_group_name = "${var.dev-environment-prefix}-ebis-core"
}

data "azurerm_key_vault_secret" "platform-cosmos-readwrite-connection-string" {
  name         = "ebis-cdb-connection-string-rw"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "platform-sql-connection-string" {
  name         = "ebis-sql-connection-string"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_cosmosdb_account" "cosmosdb-account" {
  name                = "${var.dev-environment-prefix}-ebis-cdb"
  resource_group_name = "${var.dev-environment-prefix}-ebis-platform"
}