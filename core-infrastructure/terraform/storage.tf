resource "azurerm_storage_account" "data" {
  #checkov:skip=CKV_AZURE_43:False positive on storage account adhering to the naming rules
  #checkov:skip=CKV2_AZURE_33:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_1:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_40:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_41:See ADO backlog AB#206389
  #checkov:skip=CKV_AZURE_59:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_50:potential false positive https://github.com/bridgecrewio/checkov/issues/6388
  name                            = "${var.environment-prefix}data"
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

resource "azurerm_storage_queue" "pipeline-message-pending-queue" {
  name                 = "data-pipeline-job-pending"
  storage_account_name = azurerm_storage_account.data.name
}

resource "azurerm_storage_queue" "pipeline-message-start-queue" {
  name                 = "data-pipeline-job-start"
  storage_account_name = azurerm_storage_account.data.name
}

resource "azurerm_storage_queue" "pipeline-message-finished-queue" {
  name                 = "data-pipeline-job-finished"
  storage_account_name = azurerm_storage_account.data.name
}

resource "azurerm_storage_queue" "pipeline-message-dead-letter-queue" {
  name                 = "data-pipeline-job-dlq"
  storage_account_name = azurerm_storage_account.data.name
}

resource "azurerm_storage_container" "pipeline-raw-data" {
  #checkov:skip=CKV2_AZURE_21:See ADO backlog AB#206507
  name                 = "raw"
  storage_account_name = azurerm_storage_account.data.name
}

resource "azurerm_key_vault_secret" "data-storage-connection-string" {
  #checkov:skip=CKV_AZURE_41:See ADO backlog AB#206511
  name         = "data-storage-connection-string"
  value        = azurerm_storage_account.data.primary_connection_string
  key_vault_id = azurerm_key_vault.key-vault.id
  content_type = "connection-string"
  depends_on   = [azurerm_key_vault_access_policy.terraform_sp_access]
}