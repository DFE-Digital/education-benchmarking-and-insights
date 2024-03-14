resource "azurerm_storage_account" "platform-storage" {
  #checkov:skip=CKV_AZURE_206:Only LRS required
  #checkov:skip=CKV_AZURE_43:Name needs to include prefix
  #checkov:skip=CKV_AZURE_33:Storage queues not used
  #checkov:skip=CKV2_AZURE_1:To be reviewed
  name                            = "${var.environment-prefix}platformstorage"
  location                        = azurerm_resource_group.resource-group.location
  resource_group_name             = azurerm_resource_group.resource-group.name
  account_tier                    = "Standard"
  account_replication_type        = "LRS"
  allow_nested_items_to_be_public = false
  tags                            = local.common-tags
  min_tls_version                 = "TLS1_2"

  blob_properties {
    delete_retention_policy {
      days = 7
    }
  }
}

resource "azurerm_storage_container" "local-authorities-container" {
  name                 = "local-authorities"
  storage_account_name = azurerm_storage_account.platform-storage.name
}

resource "azurerm_key_vault_secret" "platform-storage-connection-string" {
  #checkov:skip=CKV_AZURE_41:Secrets expiration to be reviewed
  name         = "platform-storage-connection-string"
  value        = azurerm_storage_account.platform-storage.primary_connection_string
  key_vault_id = data.azurerm_key_vault.key-vault.id
  content_type = "connection-string"
}

resource "azurerm_storage_account" "audit-storage" {
  #checkov:skip=CKV_AZURE_206:Only LRS required
  #checkov:skip=CKV_AZURE_43:Name needs to include prefix
  #checkov:skip=CKV_AZURE_33:Storage queues not used
  #checkov:skip=CKV2_AZURE_1:To be reviewed
  name                            = "${var.environment-prefix}audit"
  location                        = azurerm_resource_group.resource-group.location
  resource_group_name             = azurerm_resource_group.resource-group.name
  account_tier                    = "Standard"
  account_replication_type        = "LRS"
  allow_nested_items_to_be_public = false
  tags                            = local.common-tags
  min_tls_version                 = "TLS1_2"

  blob_properties {
    delete_retention_policy {
      days = 7
    }
  }
}

resource "azurerm_storage_account" "threat-storage" {
  #checkov:skip=CKV_AZURE_206:Only LRS required
  #checkov:skip=CKV_AZURE_43:Name needs to include prefix
  #checkov:skip=CKV_AZURE_33:Storage queues not used
  #checkov:skip=CKV2_AZURE_1:To be reviewed
  name                            = "${var.environment-prefix}threat"
  location                        = azurerm_resource_group.resource-group.location
  resource_group_name             = azurerm_resource_group.resource-group.name
  account_tier                    = "Standard"
  account_replication_type        = "LRS"
  allow_nested_items_to_be_public = false
  tags                            = local.common-tags
  min_tls_version                 = "TLS1_2"

  blob_properties {
    delete_retention_policy {
      days = 7
    }
  }
}
