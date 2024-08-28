resource "azurerm_load_test" "platform-load-test" {
  count               = var.environment == "test" ? 0 : 1
  name                = "${var.environment-prefix}-platform-load-test"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  tags                = local.common-tags

  identity {
    type = "SystemAssigned"
  }
}

resource "azurerm_key_vault_access_policy" "keyvault_load_test_policy" {
  count              = var.environment == "test" ? 0 : 1
  key_vault_id       = data.azurerm_key_vault.key-vault.id
  tenant_id          = azurerm_load_test.platform-load-test.identity[0].tenant_id
  object_id          = azurerm_load_test.platform-load-test.identity[0].principal_id
  secret_permissions = ["Get"]
}