resource "azurerm_key_vault_access_policy" "supporting-data-policy" {
  key_vault_id       = data.azurerm_key_vault.key-vault.id
  tenant_id          = azurerm_data_factory.supporting-data.identity[0].tenant_id
  object_id          = azurerm_data_factory.supporting-data.identity[0].principal_id
  secret_permissions = ["Get", "List"]
}
