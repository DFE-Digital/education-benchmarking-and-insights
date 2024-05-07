resource "azurerm_key_vault_secret" "supporting-data-cosmos-connection-string" {
  #checkov:skip=CKV_AZURE_41:See ADO backlog AB#206511
  name         = "supporting-data-cosmos-connection-string"
  value        = "${data.azurerm_key_vault_secret.platform-cosmos-readwrite-connection-string.value};Database=ebis-data"
  key_vault_id = data.azurerm_key_vault.key-vault.id
  content_type = "connection-string"
}

resource "azurerm_key_vault_access_policy" "supporting-data-policy" {
  key_vault_id       = data.azurerm_key_vault.key-vault.id
  tenant_id          = azurerm_data_factory.supporting-data.identity[0].tenant_id
  object_id          = azurerm_data_factory.supporting-data.identity[0].principal_id
  secret_permissions = ["Get", "List"]
}