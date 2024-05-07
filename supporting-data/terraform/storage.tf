resource "azurerm_storage_account" "supporting-data" {
  #checkov:skip=CKV_AZURE_43:False positive on storage account adhering to the naming rules
  #checkov:skip=CKV_AZURE_33:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_1:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_33:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_40:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_41:See ADO backlog AB#206389
  #checkov:skip=CKV_AZURE_59:See ADO backlog AB#206389
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

  sas_policy {
    expiration_action = "Log"
    expiration_period = "90.00:00:00"
  }
}

resource "azurerm_storage_container" "raw-data" {
  #checkov:skip=CKV2_AZURE_21:See ADO backlog AB#206507
  name                  = "raw"
  storage_account_name  = azurerm_storage_account.supporting-data.name
  container_access_type = "private"
}

resource "azurerm_key_vault_secret" "supporting-data-storage-connection-string" {
  #checkov:skip=CKV_AZURE_41:See ADO backlog AB#206511
  name         = "supporting-data-storage-connection-string"
  value        = azurerm_storage_account.supporting-data.primary_connection_string
  key_vault_id = data.azurerm_key_vault.key-vault.id
  content_type = "connection-string"
}

data "azurerm_storage_account_blob_container_sas" "supporting-data" {
  connection_string = azurerm_storage_account.supporting-data.primary_connection_string
  container_name    = azurerm_storage_container.raw-data.name
  https_only        = true

  start  = "2024-01-01"
  expiry = "2030-12-31"

  permissions {
    read   = true
    add    = false
    create = false
    write  = false
    delete = false
    list   = true
  }
}

resource "azurerm_key_vault_secret" "supporting-data-storage-sas-token" {
  #checkov:skip=CKV_AZURE_41:See ADO backlog AB#206511
  name         = "supporting-data-storage-sas-token"
  value        = data.azurerm_storage_account_blob_container_sas.supporting-data.sas
  key_vault_id = data.azurerm_key_vault.key-vault.id
  content_type = "token"
}