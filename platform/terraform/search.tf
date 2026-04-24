resource "azurerm_search_service" "search" {
  #checkov:skip=CKV_AZURE_124:Search is currently public facing
  name                          = "${var.environment-prefix}-ebis-search"
  resource_group_name           = azurerm_resource_group.resource-group.name
  location                      = azurerm_resource_group.resource-group.location
  sku                           = module.config.search.sku
  replica_count                 = module.config.search.replica_count
  partition_count               = 1
  public_network_access_enabled = true
  local_authentication_enabled  = true
  tags                          = local.common-tags
}

resource "azurerm_key_vault_secret" "platform-search-key" {
  #checkov:skip=CKV_AZURE_41:See ADO backlog AB#206511
  name         = "ebis-search-admin-key"
  value        = azurerm_search_service.search.primary_key
  key_vault_id = data.azurerm_key_vault.key-vault.id
  content_type = "key"
}
