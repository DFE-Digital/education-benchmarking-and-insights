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

resource "azurerm_function_app_flex_consumption" "function-app" {
  name                = "${var.environment-prefix}-ebis-chart-renderer-fa"
  resource_group_name = azurerm_resource_group.chart-renderer.name
  location            = azurerm_resource_group.chart-renderer.location
  service_plan_id     = azurerm_service_plan.flex.id

  # Storage configuration
  storage_container_type            = "blobContainer"
  storage_container_endpoint        = "${azurerm_storage_account.storage.primary_blob_endpoint}${azurerm_storage_container.deployment.name}"
  storage_authentication_type       = "UserAssignedIdentity"
  storage_user_assigned_identity_id = azurerm_user_assigned_identity.func-identity.id

  # Runtime configuration
  runtime_name    = "node"
  runtime_version = "20"

  # Networking
  public_network_access_enabled = true # Allowed for temporary pipeline whitelisting
  virtual_network_subnet_id     = data.azurerm_subnet.compute.id

  # Security
  https_only = true

  identity {
    type         = "UserAssigned"
    identity_ids = [azurerm_user_assigned_identity.func-identity.id]
  }

  app_settings = {
    "AzureWebJobsSecretStorageType"             = "keyvault"
    "AzureWebJobsSecretStorageKeyVaultUri"      = azurerm_key_vault.chart-renderer-kv.vault_uri
    "AzureWebJobsSecretStorageKeyVaultClientId" = azurerm_user_assigned_identity.func-identity.client_id
  }

  site_config {
    # Deny all by default (unmatched rules)
    ip_restriction_default_action     = "Deny"
    scm_ip_restriction_default_action = "Deny"
  }

  tags = local.common-tags

  depends_on = [
    azurerm_role_assignment.storage-data-owner,
    azurerm_key_vault_access_policy.func-kv-access,
    azurerm_private_endpoint.storage-blob-endpoint
  ]
}

resource "azurerm_private_endpoint" "function-app-endpoint" {
  name                = "${azurerm_function_app_flex_consumption.function-app.name}-endpoint"
  location            = azurerm_resource_group.chart-renderer.location
  resource_group_name = azurerm_resource_group.chart-renderer.name
  subnet_id           = data.azurerm_subnet.inbound.id
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
