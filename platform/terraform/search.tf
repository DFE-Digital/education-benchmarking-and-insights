resource "azurerm_search_service" "search" {
  #checkov:skip=CKV_AZURE_124:To be reviewed
  #checkov:skip=CKV_AZURE_207:To be reviewed
  #checkov:skip=CKV_AZURE_208:To be reviewed
  #checkov:skip=CKV_AZURE_209:To be reviewed
  #checkov:skip=CKV_AZURE_225:To be reviewed
  name                = "${var.environment-prefix}-ebis-search"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  sku                 = var.configuration[var.environment].search.sku
  tags                = local.common-tags
}

resource "azurerm_key_vault_secret" "platform-search-key" {
  #checkov:skip=CKV_AZURE_41:Secrets expiration to be reviewed
  name         = "ebis-search-admin-key"
  value        = azurerm_search_service.search.primary_key
  key_vault_id = data.azurerm_key_vault.key-vault.id
  content_type = "key"
}