resource "azurerm_search_service" "search" {
  #checkov:skip=CKV_AZURE_124:See ADO backlog AB#206514
  #checkov:skip=CKV_AZURE_207:See ADO backlog AB#206514
  #checkov:skip=CKV_AZURE_208:See ADO backlog AB#206514
  #checkov:skip=CKV_AZURE_209:See ADO backlog AB#206514
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

resource "azurerm_search_shared_private_link_service" "search-sql-link" {
  name               = "search-to-sql-link"
  search_service_id  = azurerm_search_service.search.id
  target_resource_id = data.azurerm_mssql_server.sql-server.id
  subresource_name   = "sqlServer"
  request_message    = "Shared Private Link connection from Search Service to SQL Database"
}
