resource "azurerm_search_service" "search" {
  #checkov:skip=CKV_AZURE_124:See ADO backlog AB#206514
  #checkov:skip=CKV_AZURE_207:See ADO backlog AB#206514
  #checkov:skip=CKV_AZURE_208:See ADO backlog AB#206514
  #checkov:skip=CKV_AZURE_209:See ADO backlog AB#206514
  name                = "${var.environment-prefix}-ebis-search"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  sku                 = var.configuration[var.environment].search_sku
  tags                = local.common-tags
  replica_count       = var.configuration[var.environment].search_replica_count
}

resource "azurerm_key_vault_secret" "platform-search-key" {
  #checkov:skip=CKV_AZURE_41:See ADO backlog AB#206511
  name         = "ebis-search-admin-key"
  value        = azurerm_search_service.search.primary_key
  key_vault_id = data.azurerm_key_vault.key-vault.id
  content_type = "key"
}