resource "azurerm_storage_account" "main" {
  #checkov:skip=CKV_AZURE_43:False positive on storage account adhering to the naming rules
  #checkov:skip=CKV_AZURE_33:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_1:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_33:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_40:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_41:See ADO backlog AB#206389
  #checkov:skip=CKV_AZURE_59:See ADO backlog AB#206389
  name                            = "${var.environment-prefix}pipelinestorage"
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
}

resource "azurerm_storage_queue" "worker-queue" {
  name                 = "worker-queue"
  storage_account_name = azurerm_storage_account.main.name
}

resource "azurerm_storage_queue" "work-complete-queue" {
  name                 = "work-complete-queue"
  storage_account_name = azurerm_storage_account.main.name
}