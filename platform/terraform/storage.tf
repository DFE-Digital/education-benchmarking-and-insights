resource "azurerm_storage_account" "platform-storage" {
  #checkov:skip=CKV_AZURE_43:False positive on storage account adhering to the naming rules
  #checkov:skip=CKV2_AZURE_33:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_1:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_40:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_41:See ADO backlog AB#206389
  #checkov:skip=CKV_AZURE_59:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_50:potential false positive https://github.com/bridgecrewio/checkov/issues/6388
  name                            = "${var.environment-prefix}platformstorage"
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

  queue_properties {
    logging {
      delete                = true
      read                  = true
      write                 = true
      version               = "1.0"
      retention_policy_days = 10
    }
  }

  sas_policy {
    expiration_action = "Log"
    expiration_period = "90.00:00:00"
  }
}

resource "azurerm_monitor_diagnostic_setting" "platform-storage-blob" {
  name                       = "${azurerm_storage_account.platform-storage.name}-blob-logs"
  target_resource_id         = "${azurerm_storage_account.platform-storage.id}/blobServices/default/"
  log_analytics_workspace_id = data.azurerm_log_analytics_workspace.application-insights-workspace.id

  metric {
    category = "Transaction"
    enabled  = true

    // The following is not used by Log Analytics backed diagnostics, but Terraform adds it anyway and `ignore_changes` 
    // is not currently supported by block level configuration (https://github.com/hashicorp/terraform/issues/26359). 
    // The 'deprecated' warning here and below may therefore be ignored.
    retention_policy {
      days    = 0
      enabled = false
    }
  }

  enabled_log {
    category = "StorageRead"
  }
}

resource "azurerm_storage_account" "orchestrator-storage" {
  #checkov:skip=CKV_AZURE_43:False positive on storage account adhering to the naming rules
  #checkov:skip=CKV2_AZURE_33:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_1:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_40:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_41:See ADO backlog AB#206389
  #checkov:skip=CKV_AZURE_59:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_50:potential false positive https://github.com/bridgecrewio/checkov/issues/6388
  name                            = "${var.environment-prefix}orchestrator"
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

  queue_properties {
    logging {
      delete                = true
      read                  = true
      write                 = true
      version               = "1.0"
      retention_policy_days = 10
    }
  }

  sas_policy {
    expiration_action = "Log"
    expiration_period = "90.00:00:00"
  }
}
