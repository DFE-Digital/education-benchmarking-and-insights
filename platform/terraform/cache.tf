resource "azurerm_redis_cache" "cache" {
  #checkov:skip=CKV_AZURE_89:See ADO backlog AB#243011
  name                               = "${var.environment-prefix}-ebis-cache"
  location                           = azurerm_resource_group.resource-group.location
  resource_group_name                = azurerm_resource_group.resource-group.name
  capacity                           = var.configuration[var.environment].cache_capacity
  family                             = var.configuration[var.environment].cache_sku == "Premium" ? "P" : "C"
  sku_name                           = var.configuration[var.environment].cache_sku
  non_ssl_port_enabled               = false
  minimum_tls_version                = "1.2"
  public_network_access_enabled      = true
  access_keys_authentication_enabled = false
  tags                               = local.common-tags

  redis_configuration {
    authentication_enabled                  = true
    active_directory_authentication_enabled = true
  }
}

resource "azurerm_redis_cache_access_policy_assignment" "owner" {
  name               = "owner"
  redis_cache_id     = azurerm_redis_cache.cache.id
  access_policy_name = "Data Owner"
  object_id          = data.azurerm_client_config.client.object_id
  object_id_alias    = "Owner"
}

resource "azurerm_key_vault_secret" "cache-host-name" {
  #checkov:skip=CKV_AZURE_41:See ADO backlog AB#206511
  name         = "ebis-cache-host-name"
  value        = azurerm_redis_cache.cache.hostname
  key_vault_id = data.azurerm_key_vault.key-vault.id
  content_type = "host"
}

resource "azurerm_key_vault_secret" "cache-ssl-port" {
  #checkov:skip=CKV_AZURE_41:See ADO backlog AB#206511
  name         = "ebis-cache-ssl-port"
  value        = azurerm_redis_cache.cache.ssl_port
  key_vault_id = data.azurerm_key_vault.key-vault.id
  content_type = "port"
}
