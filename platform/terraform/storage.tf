resource "azurerm_storage_account" "storage" {
  #checkov:skip=CKV_AZURE_43:False positive on storage account adhering to the naming rules
  #checkov:skip=CKV2_AZURE_33:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_1:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_40:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_41:See ADO backlog AB#206389
  #checkov:skip=CKV_AZURE_59:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_50:potential false positive https://github.com/bridgecrewio/checkov/issues/6388
  #checkov:skip=CKV_AZURE_33:False positive on queue logging due to new azurerm_storage_account_queue_properties resource (https://github.com/bridgecrewio/checkov/issues/7174)
  for_each                        = local.storage_accounts
  name                            = "${var.environment-prefix}${each.key}"
  location                        = azurerm_resource_group.resource-group.location
  resource_group_name             = azurerm_resource_group.resource-group.name
  account_tier                    = "Standard"
  account_replication_type        = "GRS"
  allow_nested_items_to_be_public = false
  tags                            = local.common-tags
  min_tls_version                 = "TLS1_2"
  public_network_access_enabled   = true
  shared_access_key_enabled       = true
  local_user_enabled              = false

  blob_properties {
    delete_retention_policy {
      days = 7
    }
    container_delete_retention_policy {
      days = 7
    }
    versioning_enabled = true
  }

  sas_policy {
    expiration_action = "Log"
    expiration_period = "90.00:00:00"
  }
}

resource "azurerm_storage_account_queue_properties" "storage-queue-properties" {
  for_each           = local.storage_accounts
  storage_account_id = azurerm_storage_account.storage[each.key].id

  logging {
    delete                = true
    read                  = true
    write                 = true
    version               = "1.0"
    retention_policy_days = 10
  }
}

resource "azurerm_monitor_diagnostic_setting" "storage-blob" {
  for_each                   = local.storage_accounts
  name                       = "${azurerm_storage_account.storage[each.key].name}-blob-logs"
  target_resource_id         = "${azurerm_storage_account.storage[each.key].id}/blobServices/default/"
  log_analytics_workspace_id = data.azurerm_log_analytics_workspace.application-insights-workspace.id

  enabled_metric {
    category = "Transaction"
  }

  enabled_log {
    category = "StorageRead"
  }
}

moved {
  from = azurerm_storage_account.platform-storage
  to   = azurerm_storage_account.storage["platformstorage"]
}

moved {
  from = azurerm_storage_account.orchestrator-storage
  to   = azurerm_storage_account.storage["orchestrator"]
}

moved {
  from = azurerm_storage_account_queue_properties.platform-storage-queue-properties
  to   = azurerm_storage_account_queue_properties.storage-queue-properties["platformstorage"]
}

moved {
  from = azurerm_storage_account_queue_properties.orchestrator-storage-properties
  to   = azurerm_storage_account_queue_properties.storage-queue-properties["orchestrator"]
}

moved {
  from = azurerm_monitor_diagnostic_setting.platform-storage-blob
  to   = azurerm_monitor_diagnostic_setting.storage-blob["platformstorage"]
}
