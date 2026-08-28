# Dedicated Storage
resource "azurerm_storage_account" "analytics_storage" {
  #checkov:skip=CKV_AZURE_43:False positive on storage account adhering to the naming rules
  #checkov:skip=CKV2_AZURE_33:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_1:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_40:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_41:See ADO backlog AB#206389
  #checkov:skip=CKV_AZURE_59:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_50:potential false positive https://github.com/bridgecrewio/checkov/issues/6388
  #checkov:skip=CKV_AZURE_33:False positive on queue logging due to new azurerm_storage_account_queue_properties resource (https://github.com/bridgecrewio/checkov/issues/7174)
  name                            = "${replace(var.environment-prefix, "-", "")}ebisanalytics"
  resource_group_name             = azurerm_resource_group.resource-group.name
  location                        = var.location
  account_tier                    = "Standard"
  account_replication_type        = "LRS"
  allow_nested_items_to_be_public = false
  public_network_access_enabled   = true
  local_user_enabled              = false
  min_tls_version                 = "TLS1_2"
  tags                            = local.common-tags

  blob_properties {
    delete_retention_policy {
      days = 7
    }
    container_delete_retention_policy {
      days = 7
    }
    versioning_enabled = true
  }
}

# Container
resource "azurerm_storage_container" "log_outputs" {
  #checkov:skip=CKV2_AZURE_21:See ADO backlog AB#206507
  name                  = "transformed-log-data"
  storage_account_name  = azurerm_storage_account.analytics_storage.name
  container_access_type = "private"
}
