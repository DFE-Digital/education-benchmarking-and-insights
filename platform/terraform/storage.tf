resource "azurerm_storage_account" "platform-storage" {
  name                            = "${var.environment-prefix}platformstorage"
  location                        = azurerm_resource_group.resource-group.location
  resource_group_name             = azurerm_resource_group.resource-group.name
  account_tier                    = "Standard"
  account_replication_type        = "LRS"
  allow_nested_items_to_be_public = false
  tags                            = local.common-tags
}

resource "azurerm_storage_container" "local-authorities-container" {
  name                 = "local-authorities"
  storage_account_name = azurerm_storage_account.platform-storage.name
}

resource "azurerm_key_vault_secret" "platform-storage-connection-string" {
  #checkov:skip=CKV_AZURE_41:Secrets expiration dto be reviewed
  name         = "platform-storage-connection-string"
  value        = azurerm_storage_account.platform-storage.primary_connection_string
  key_vault_id = data.azurerm_key_vault.key-vault.id
  content_type = "connection-string"
}