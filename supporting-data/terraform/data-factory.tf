resource "azurerm_data_factory" "supporting-data" {
  #checkov:skip=CKV_AZURE_103:See ADO backlog AB#207729
  #checkov:skip=CKV_AZURE_104:See ADO backlog AB#207946
  name                = "${var.environment-prefix}-ebis-adf"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  tags                = local.common-tags

  public_network_enabled = true

  identity {
    type = "SystemAssigned"
  }
}

resource "azurerm_data_factory_linked_service_key_vault" "supporting-data" {
  name            = "${var.environment-prefix}-ebis-adf-kv"
  data_factory_id = azurerm_data_factory.supporting-data.id
  key_vault_id    = data.azurerm_key_vault.key-vault.id
}

resource "azurerm_key_vault_access_policy" "supporting-data-policy" {
  key_vault_id       = data.azurerm_key_vault.key-vault.id
  tenant_id          = azurerm_data_factory.supporting-data.identity[0].tenant_id
  object_id          = azurerm_data_factory.supporting-data.identity[0].principal_id
  secret_permissions = ["Get"]
}
