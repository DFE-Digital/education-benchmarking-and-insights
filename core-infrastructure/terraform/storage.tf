resource "azurerm_storage_account" "pipeline-message-hub-storage" {
  #checkov:skip=CKV_AZURE_43:False positive on storage account adhering to the naming rules
  #checkov:skip=CKV_AZURE_33:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_1:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_33:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_40:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_41:See ADO backlog AB#206389
  #checkov:skip=CKV_AZURE_59:See ADO backlog AB#206389
  name                            = "${var.environment-prefix}messagehubstorage"
  location                        = azurerm_resource_group.resource-group.location
  resource_group_name             = azurerm_resource_group.resource-group.name
  account_tier                    = "Standard"
  account_replication_type        = "GRS"
  allow_nested_items_to_be_public = false
  tags                            = local.common-tags
  min_tls_version                 = "TLS1_2"
  public_network_access_enabled   = true

  blob_properties {
    delete_retention_policy {
      days = 7
    }
  }

  sas_policy {
    expiration_action = "Log"
    expiration_period = "90.00:00:00"
  }
}

resource "azurerm_storage_queue" "pipeline-message-start-queue" {
  name                 = "job-start"
  storage_account_name = azurerm_storage_account.pipeline-message-hub-storage.name
}

resource "azurerm_storage_queue" "pipeline-message-finished-queue" {
  name                 = "job-finished"
  storage_account_name = azurerm_storage_account.pipeline-message-hub-storage.name
}

resource "azurerm_key_vault_secret" "pipeline-message-hub-storage-connection-string" {
  #checkov:skip=CKV_AZURE_41:See ADO backlog AB#206511
  name         = "pipeline-message-hub-storage-connection-string"
  value        = azurerm_storage_account.pipeline-message-hub-storage.primary_connection_string
  key_vault_id = azurerm_key_vault.key-vault.id
  content_type = "connection-string"
}