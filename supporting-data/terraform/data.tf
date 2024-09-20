data "azurerm_key_vault" "key-vault" {
  name                = "${var.dev-environment-prefix}-ebis-keyvault"
  resource_group_name = "${var.dev-environment-prefix}-ebis-core"
}

data "azurerm_key_vault_secret" "platform-sql-connection-string" {
  name         = "core-sql-connection-string"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}
