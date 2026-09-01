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
  account_replication_type        = "GRS"
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
  storage_account_id    = azurerm_storage_account.analytics_storage.id
  container_access_type = "private"
}

# Identity
resource "azurerm_user_assigned_identity" "func_identity" {
  name                = "${var.environment-prefix}-ebis-analytics-function-identity"
  resource_group_name = azurerm_resource_group.resource-group.name
  location            = var.location
  tags                = local.common-tags
}

# Backing Storage Account for Function App State
resource "azurerm_storage_account" "func_app_sa" {
  #checkov:skip=CKV_AZURE_43:False positive on storage account adhering to the naming rules
  #checkov:skip=CKV2_AZURE_33:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_1:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_40:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_41:See ADO backlog AB#206389
  #checkov:skip=CKV_AZURE_59:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_50:potential false positive https://github.com/bridgecrewio/checkov/issues/6388
  #checkov:skip=CKV_AZURE_33:False positive on queue logging due to new azurerm_storage_account_queue_properties resource (https://github.com/bridgecrewio/checkov/issues/7174)
  name                            = "${replace(var.environment-prefix, "-", "")}ebisanalyticsfunc"
  resource_group_name             = azurerm_resource_group.resource-group.name
  location                        = var.location
  account_tier                    = "Standard"
  account_replication_type        = "GRS"
  allow_nested_items_to_be_public = false
  public_network_access_enabled   = true
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

# Storage Container for Function App
resource "azurerm_storage_container" "func_app_sc" {
  #checkov:skip=CKV2_AZURE_21:See ADO backlog AB#206507
  name                  = "func-app"
  storage_account_id    = azurerm_storage_account.func_app_sa.id
  container_access_type = "private"
}

# Storage Role Assignments for Managed Identity
resource "azurerm_role_assignment" "storage_data_owner_func" {
  scope                = azurerm_storage_account.func_app_sa.id
  role_definition_name = "Storage Blob Data Owner"
  principal_id         = azurerm_user_assigned_identity.func_identity.principal_id
  principal_type       = "ServicePrincipal"
}

# Permissions
resource "azurerm_role_assignment" "storage_data_owner_analytics" {
  scope                = azurerm_storage_account.analytics_storage.id
  role_definition_name = "Storage Blob Data Owner"
  principal_id         = azurerm_user_assigned_identity.func_identity.principal_id
  principal_type       = "ServicePrincipal"
}

# Flex App Service Plan
resource "azurerm_service_plan" "func_asp" {
  #checkov:skip=CKV_AZURE_212:See ADO backlog AB#206517
  #checkov:skip=CKV_AZURE_225:See ADO backlog AB#206517
  name                = "${var.environment-prefix}-ebis-analytics-function-asp"
  location            = var.location
  resource_group_name = azurerm_resource_group.resource-group.name
  os_type             = "Linux"
  sku_name            = "FC1"
  tags                = local.common-tags
}

resource "azurerm_function_app_flex_consumption" "func_app" {
  name                              = "${var.environment-prefix}-ebis-analytics-function-app"
  location                          = var.location
  resource_group_name               = azurerm_resource_group.resource-group.name
  service_plan_id                   = azurerm_service_plan.func_asp.id
  storage_container_type            = "blobContainer"
  storage_container_endpoint        = "${azurerm_storage_account.func_app_sa.primary_blob_endpoint}${azurerm_storage_container.func_app_sc.name}"
  storage_authentication_type       = "UserAssignedIdentity"
  storage_user_assigned_identity_id = azurerm_user_assigned_identity.func_identity.id
  storage_access_key                = azurerm_storage_account.func_app_sa.primary_access_key
  public_network_access_enabled     = true
  runtime_name                      = "python"
  runtime_version                   = "3.11"
  https_only                        = true

  identity {
    type         = "SystemAssigned, UserAssigned"
    identity_ids = [azurerm_user_assigned_identity.func_identity.id]
  }

  app_settings = {
    "FUNCTIONS_WORKER_RUNTIME"   = "python"
    "BLOB_CONTAINER_NAME"        = azurerm_storage_container.log_outputs.name
    "STORAGE_CONNECTION_STRING"  = azurerm_storage_account.analytics_storage.primary_connection_string
  }

  tags = local.common-tags

  depends_on = [
    azurerm_role_assignment.storage_data_owner_func,
    azurerm_role_assignment.storage_data_owner_analytics
  ]
}
