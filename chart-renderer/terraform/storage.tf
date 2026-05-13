resource "azurerm_storage_account" "storage" {
  name                            = "${replace(var.environment-prefix, "-", "")}chrtrendst"
  resource_group_name             = azurerm_resource_group.chart-renderer.name
  location                        = azurerm_resource_group.chart-renderer.location
  account_tier                    = "Standard"
  account_replication_type        = "LRS"
  public_network_access_enabled   = true # Changed to true to allow temporary whitelisting from pipeline
  shared_access_key_enabled       = false
  local_user_enabled              = false
  tags                            = local.common-tags

  network_rules {
    default_action             = "Deny"
    virtual_network_subnet_ids = [data.azurerm_subnet.outbound.id, data.azurerm_subnet.endpoints.id]
    bypass                     = ["AzureServices"]
  }
}

resource "azurerm_storage_container" "deployment" {
  name                  = "deployment"
  storage_account_id    = azurerm_storage_account.storage.id
  container_access_type = "private"
}

resource "azurerm_private_endpoint" "storage-blob-endpoint" {
  name                = "${azurerm_storage_account.storage.name}-blob-endpoint"
  location            = azurerm_resource_group.chart-renderer.location
  resource_group_name = azurerm_resource_group.chart-renderer.name
  subnet_id           = data.azurerm_subnet.endpoints.id
  tags                = local.common-tags

  private_service_connection {
    name                           = "${azurerm_storage_account.storage.name}-blob-connection"
    private_connection_resource_id = azurerm_storage_account.storage.id
    is_manual_connection           = false
    subresource_names              = ["blob"]
  }

  private_dns_zone_group {
    name                 = "storage-blob-dns-zone-group"
    private_dns_zone_ids = [data.azurerm_private_dns_zone.blob.id]
  }
}

resource "azurerm_private_endpoint" "storage-queue-endpoint" {
  name                = "${azurerm_storage_account.storage.name}-queue-endpoint"
  location            = azurerm_resource_group.chart-renderer.location
  resource_group_name = azurerm_resource_group.chart-renderer.name
  subnet_id           = data.azurerm_subnet.endpoints.id
  tags                = local.common-tags

  private_service_connection {
    name                           = "${azurerm_storage_account.storage.name}-queue-connection"
    private_connection_resource_id = azurerm_storage_account.storage.id
    is_manual_connection           = false
    subresource_names              = ["queue"]
  }

  private_dns_zone_group {
    name                 = "storage-queue-dns-zone-group"
    private_dns_zone_ids = [data.azurerm_private_dns_zone.queue.id]
  }
}

resource "azurerm_private_endpoint" "storage-table-endpoint" {
  name                = "${azurerm_storage_account.storage.name}-table-endpoint"
  location            = azurerm_resource_group.chart-renderer.location
  resource_group_name = azurerm_resource_group.chart-renderer.name
  subnet_id           = data.azurerm_subnet.endpoints.id
  tags                = local.common-tags

  private_service_connection {
    name                           = "${azurerm_storage_account.storage.name}-table-connection"
    private_connection_resource_id = azurerm_storage_account.storage.id
    is_manual_connection           = false
    subresource_names              = ["table"]
  }

  private_dns_zone_group {
    name                 = "storage-table-dns-zone-group"
    private_dns_zone_ids = [data.azurerm_private_dns_zone.table.id]
  }
}
