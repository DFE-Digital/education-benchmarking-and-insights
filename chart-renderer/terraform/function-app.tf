resource "azurerm_service_plan" "flex" {
  name                = "${var.environment-prefix}-ebis-chart-renderer-sp"
  resource_group_name = azurerm_resource_group.chart-renderer.name
  location            = azurerm_resource_group.chart-renderer.location
  os_type             = "Linux"
  sku_name            = "FC1"
  tags                = local.common-tags
}

resource "azurerm_user_assigned_identity" "func-identity" {
  name                = "${var.environment-prefix}-ebis-chart-renderer-identity"
  resource_group_name = azurerm_resource_group.chart-renderer.name
  location            = azurerm_resource_group.chart-renderer.location
  tags                = local.common-tags
}

resource "azurerm_role_assignment" "storage-data-owner" {
  scope                = azurerm_storage_account.storage.id
  role_definition_name = "Storage Blob Data Owner"
  principal_id         = azurerm_user_assigned_identity.func-identity.principal_id
  principal_type       = "ServicePrincipal"
}

resource "azurerm_role_assignment" "terraform-storage-data-owner" {
  scope                = azurerm_storage_account.storage.id
  role_definition_name = "Storage Blob Data Owner"
  principal_id         = data.azurerm_client_config.current.object_id
  principal_type       = "ServicePrincipal"
}

resource "azurerm_role_assignment" "storage-queue-contributor" {
  scope                = azurerm_storage_account.storage.id
  role_definition_name = "Storage Queue Data Contributor"
  principal_id         = azurerm_user_assigned_identity.func-identity.principal_id
  principal_type       = "ServicePrincipal"
}

resource "azurerm_role_assignment" "storage-table-contributor" {
  scope                = azurerm_storage_account.storage.id
  role_definition_name = "Storage Table Data Contributor"
  principal_id         = azurerm_user_assigned_identity.func-identity.principal_id
  principal_type       = "ServicePrincipal"
}

resource "azurerm_role_assignment" "storage-account-contributor" {
  scope                = azurerm_storage_account.storage.id
  role_definition_name = "Storage Account Contributor"
  principal_id         = azurerm_user_assigned_identity.func-identity.principal_id
  principal_type       = "ServicePrincipal"
}

# Upload the Function App package to the deployment container
resource "azurerm_storage_blob" "package" {
  name                   = "chart-renderer-${filemd5(var.package_path)}.zip"
  storage_account_name   = azurerm_storage_account.storage.name
  storage_container_name = azurerm_storage_container.deployment.name
  type                   = "Block"
  source                 = var.package_path

  depends_on = [
    azurerm_role_assignment.terraform-storage-data-owner
  ]
}

resource "azurerm_function_app_flex_consumption" "function-app" {
  name                = "${var.environment-prefix}-ebis-chart-renderer-fa"
  resource_group_name = azurerm_resource_group.chart-renderer.name
  location            = azurerm_resource_group.chart-renderer.location
  service_plan_id     = azurerm_service_plan.flex.id

  # Storage configuration
  storage_container_type      = "blobContainer"
  storage_container_endpoint  = "${azurerm_storage_account.storage.primary_blob_endpoint}${azurerm_storage_container.deployment.name}"
  storage_authentication_type = "UserAssignedIdentity"
  storage_user_assigned_identity_id = azurerm_user_assigned_identity.func-identity.id

  # Runtime configuration
  runtime_name    = "node"
  runtime_version = "20"

  # Networking
  public_network_access_enabled = true # Allowed for temporary pipeline whitelisting
  virtual_network_subnet_id     = data.azurerm_subnet.outbound.id

  # Security
  https_only = true

  identity {
    type         = "UserAssigned"
    identity_ids = [azurerm_user_assigned_identity.func-identity.id]
  }

  app_settings = {
    "AzureWebJobsStorage"                = ""
    "AzureWebJobsStorage__accountName"   = azurerm_storage_account.storage.name
    "AzureWebJobsStorage__credential"    = "managedidentity"
    "AzureWebJobsStorage__clientId"      = azurerm_user_assigned_identity.func-identity.client_id
    "AzureWebJobsStorage__blobServiceUri"  = azurerm_storage_account.storage.primary_blob_endpoint
    "AzureWebJobsStorage__queueServiceUri" = azurerm_storage_account.storage.primary_queue_endpoint
    "AzureWebJobsStorage__tableServiceUri" = azurerm_storage_account.storage.primary_table_endpoint
    "AzureWebJobsSecretStorageType"      = "blob"
  }

  site_config {
    # Deny all by default (unmatched rules)
    ip_restriction_default_action     = "Deny"
    scm_ip_restriction_default_action = "Deny"
  }

  tags = local.common-tags

  lifecycle {
    ignore_changes = [
      app_settings["FUNCTIONS_EXTENSION_VERSION"],
      app_settings["WEBSITE_ENABLE_SYNC_UPDATE_SITE"],
    ]
  }

  depends_on = [
    azurerm_role_assignment.storage-data-owner,
    azurerm_role_assignment.storage-queue-contributor,
    azurerm_role_assignment.storage-table-contributor,
    azurerm_role_assignment.storage-account-contributor,
    azurerm_private_endpoint.storage-blob-endpoint,
    azurerm_storage_blob.package
  ]
}

resource "azurerm_private_endpoint" "function-app-endpoint" {
  name                = "${azurerm_function_app_flex_consumption.function-app.name}-endpoint"
  location            = azurerm_resource_group.chart-renderer.location
  resource_group_name = azurerm_resource_group.chart-renderer.name
  subnet_id           = data.azurerm_subnet.endpoints.id
  tags                = local.common-tags

  private_service_connection {
    name                           = "${azurerm_function_app_flex_consumption.function-app.name}-connection"
    private_connection_resource_id = azurerm_function_app_flex_consumption.function-app.id
    is_manual_connection           = false
    subresource_names              = ["sites"]
  }

  private_dns_zone_group {
    name                 = "function-app-dns-zone-group"
    private_dns_zone_ids = [data.azurerm_private_dns_zone.app-service.id]
  }
}

# Automated rotation trigger (every 90 days)
resource "time_rotating" "host-key-rotation" {
  rotation_days = 90
}

# Generate a secure host key locally
resource "random_password" "host-key" {
  length  = 64
  special = false

  keepers = {
    # Trigger rotation when the time_rotating resource updates
    rotation_id = time_rotating.host-key-rotation.id
    # Allow for manual rotation by changing this value in variables.tf
    manual_trigger = local.host_key_manual_rotation_trigger
  }
}

# Notify the Function App to mount the new package via OneDeploy
resource "azapi_resource_action" "onedeploy" {
  type        = "Microsoft.Web/sites@2023-12-01"
  resource_id = azurerm_function_app_flex_consumption.function-app.id
  method      = "POST"
  action      = "extensions/onedeploy"

  body = {
    properties = {
      type       = "zip"
      packageUri = azurerm_storage_blob.package.url
    }
  }
}

# Wait for the Function App runtime to initialize and pick up identity roles
resource "time_sleep" "wait-for-func" {
  depends_on = [
    azurerm_function_app_flex_consumption.function-app,
    azapi_resource_action.onedeploy
  ]

  create_duration = "120s"
}

# Explicitly push the host key to the Function App
# This is more robust than fetching it via a data source as it avoids dependencies on the host runtime's availability
resource "azapi_resource_action" "host-key" {
  type        = "Microsoft.Web/sites/host/functionKeys@2023-12-01"
  resource_id = "${azurerm_function_app_flex_consumption.function-app.id}/host/default/functionKeys/default"
  method      = "PUT"

  body = {
    properties = {
      name  = "default"
      value = random_password.host-key.result
    }
  }

  depends_on = [time_sleep.wait-for-func]
}

# Publish the keys and URI to the shared Key Vault
resource "azurerm_key_vault_secret" "chart-renderer-host-key" {
  name         = "chart-renderer-host-key"
  value        = random_password.host-key.result
  key_vault_id = data.azurerm_key_vault.core.id
}

resource "azurerm_key_vault_secret" "chart-renderer-host" {
  name         = "chart-renderer-host"
  value        = "https://${azurerm_function_app_flex_consumption.function-app.default_hostname}"
  key_vault_id = data.azurerm_key_vault.core.id
}
