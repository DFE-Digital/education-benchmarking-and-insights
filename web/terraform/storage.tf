resource "azurerm_storage_account" "data-source-storage" {
  #checkov:skip=CKV_AZURE_43:False positive on storage account adhering to the naming rules
  #checkov:skip=CKV2_AZURE_33:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_1:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_40:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_41:See ADO backlog AB#206389
  #checkov:skip=CKV_AZURE_59:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_50:potential false positive https://github.com/bridgecrewio/checkov/issues/6388
  #checkov:skip=CKV_AZURE_33:False positive on queue logging due to new azurerm_storage_account_queue_properties resource (https://github.com/bridgecrewio/checkov/issues/7174)
  name                            = "${var.environment-prefix}datasource"
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
    cors_rule {
      allowed_headers    = ["*"]
      allowed_methods    = ["GET"]
      allowed_origins    = ["*"]
      exposed_headers    = ["*"]
      max_age_in_seconds = 300
    }
  }

  sas_policy {
    expiration_action = "Log"
    expiration_period = "90.00:00:00"
  }
}

resource "azurerm_storage_account_queue_properties" "data-source-storage-queue-properties" {
  storage_account_id = azurerm_storage_account.data-source-storage.id

  logging {
    delete                = true
    read                  = true
    write                 = true
    version               = "1.0"
    retention_policy_days = 10
  }
}

resource "azurerm_storage_container" "return-container" {
  #checkov:skip=CKV2_AZURE_21:See ADO backlog AB#206507
  name               = "returns"
  storage_account_id = azurerm_storage_account.data-source-storage.id
}

resource "azurerm_key_vault_secret" "data-web-storage-connection-string" {
  #checkov:skip=CKV_AZURE_41:See ADO backlog AB#232052
  name         = "data-web-storage-connection-string"
  value        = azurerm_storage_account.data-source-storage.primary_connection_string
  key_vault_id = data.azurerm_key_vault.key-vault.id
  content_type = "connection-string"
}

resource "azurerm_storage_account" "web-assets-storage" {
  #checkov:skip=CKV_AZURE_33: No queues used in this storage account
  #checkov:skip=CKV_AZURE_59: Public access is required for CDN to access the storage account (for Premium AFD, IP whitelisting is applied)
  #checkov:skip=CKV2_AZURE_1: See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_33: See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_40: See ADO backlog AB#206389
  name                            = "${var.environment-prefix}webassets"
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
    cors_rule {
      allowed_headers    = ["*"]
      allowed_methods    = ["GET"]
      allowed_origins    = ["*"]
      exposed_headers    = ["*"]
      max_age_in_seconds = 300
    }
  }

  sas_policy {
    expiration_action = "Log"
    expiration_period = "90.00:00:00"
  }
}

resource "azurerm_storage_account_network_rules" "web-assets-storage-network-rules" {
  count              = (azurerm_cdn_frontdoor_profile.web-app-front-door-profile.sku_name == "Premium_AzureFrontDoor" ? 1 : 0)
  storage_account_id = azurerm_storage_account.web-assets-storage.id
  default_action     = "Deny"
  ip_rules           = var.web-assets-config.ip_whitelist
}

resource "azurerm_monitor_diagnostic_setting" "web-assets-storage-blob" {
  name                       = "${azurerm_storage_account.web-assets-storage.name}-blob-logs"
  target_resource_id         = "${azurerm_storage_account.web-assets-storage.id}/blobServices/default/"
  log_analytics_workspace_id = data.azurerm_log_analytics_workspace.application-insights-workspace.id

  enabled_metric {
    category = "Transaction"
  }

  enabled_log {
    category = "StorageRead"
  }
}

resource "azurerm_storage_container" "web-asset-container" {
  #checkov:skip=CKV2_AZURE_21: False positive (storage account logging defined above)
  for_each           = var.web-assets-config.containers
  name               = each.key
  storage_account_id = azurerm_storage_account.web-assets-storage.id
}

resource "azurerm_key_vault_secret" "web-assets-storage-connection-string" {
  #checkov:skip=CKV_AZURE_41: See ADO backlog AB#232052
  name         = "web-assets-storage-connection-string"
  value        = azurerm_storage_account.web-assets-storage.primary_connection_string
  key_vault_id = data.azurerm_key_vault.key-vault.id
  content_type = "connection-string"
}

# Create an SAS token for auth in a storage account
data "azurerm_storage_account_sas" "web-assets-storage-sas" {
  connection_string = azurerm_storage_account.web-assets-storage.primary_connection_string
  https_only        = true
  signed_version    = "2024-11-04"

  resource_types {
    service   = false
    container = false
    object    = true
  }

  services {
    blob  = true
    queue = false
    table = false
    file  = false
  }

  start  = "2025-01-01T00:00:00Z"
  expiry = "2035-01-01T00:00:00Z"

  permissions {
    read    = true
    write   = false
    delete  = false
    list    = false
    add     = false
    create  = false
    update  = false
    process = false
    tag     = false
    filter  = false
  }
}

# Get the private endpoint connections from the target resource if using Premium AFD
data "azapi_resource" "web-assets-private-endpoint-connections" {
  count                  = azurerm_cdn_frontdoor_profile.web-app-front-door-profile.sku_name == "Premium_AzureFrontDoor" ? 1 : 0
  type                   = "Microsoft.Storage/storageAccounts@2022-09-01"
  resource_id            = azurerm_storage_account.web-assets-storage.id
  depends_on             = [azurerm_cdn_frontdoor_origin.web-app-front-door-origin-web-assets]
  response_export_values = ["properties.privateEndpointConnections"]
}

# Extract the connection name for the one created by Front Door, if any available.
# Use `try` to avoid `data.azapi_resource.web-assets-private-endpoint-connections is empty tuple` error
locals {
  private_endpoint_connection_name = (azurerm_cdn_frontdoor_profile.web-app-front-door-profile.sku_name == "Premium_AzureFrontDoor"
    ? try(
      length(data.azapi_resource.web-assets-private-endpoint-connections[0].output.properties.privateEndpointConnections) > 0
      ? element([
        for connection in data.azapi_resource.web-assets-private-endpoint-connections[0].output.properties.privateEndpointConnections : connection.name
        if startswith(connection.name, "${var.environment-prefix}webassets.")
      ], 0)
      : null,
    null)
  : null)
}

# Approve the private endpoint connection if using Premium AFD
resource "azapi_update_resource" "approve-front-door-web-assets-connection" {
  count      = azurerm_cdn_frontdoor_profile.web-app-front-door-profile.sku_name == "Premium_AzureFrontDoor" ? 1 : 0
  type       = "Microsoft.Storage/storageAccounts/privateEndpointConnections@2022-09-01"
  name       = local.private_endpoint_connection_name
  parent_id  = azurerm_storage_account.web-assets-storage.id
  depends_on = [azurerm_cdn_frontdoor_origin.web-app-front-door-origin-web-assets]

  body = {
    properties = {
      privateLinkServiceConnectionState = {
        status      = "Approved"
        description = "Approved via Terraform"
      }
    }
  }
}
