resource "azurerm_key_vault" "chart-renderer-kv" {
  name                            = "${var.environment-prefix}-ebis-chart-renderer-kv"
  location                        = azurerm_resource_group.chart-renderer.location
  resource_group_name             = azurerm_resource_group.chart-renderer.name
  enabled_for_deployment          = true
  enabled_for_template_deployment = true
  tenant_id                       = data.azurerm_client_config.current.tenant_id
  sku_name                        = "standard"
  purge_protection_enabled        = var.disable-purge-protection == "true" ? false : true
  soft_delete_retention_days      = 7

  network_acls {
    default_action = "Allow"
    bypass         = "AzureServices"
  }

  tags = local.common-tags
}

resource "azurerm_key_vault_access_policy" "terraform-agent-kv-access" {
  key_vault_id = azurerm_key_vault.chart-renderer-kv.id
  tenant_id    = data.azurerm_client_config.current.tenant_id
  object_id    = data.azurerm_client_config.current.object_id

  secret_permissions = [
    "Get",
    "List",
    "Set",
    "Delete",
    "Purge",
    "Restore"
  ]
}

resource "azurerm_key_vault_access_policy" "func-kv-access" {
  key_vault_id = azurerm_key_vault.chart-renderer-kv.id
  tenant_id    = data.azurerm_client_config.current.tenant_id
  object_id    = azurerm_user_assigned_identity.func-identity.principal_id

  secret_permissions = [
    "Get",
    "List",
    "Set",
    "Delete"
  ]
}

resource "time_rotating" "function-key-rotation" {
  rotation_days = 90
}

resource "random_password" "function-key" {
  length           = 32
  special          = true
  override_special = "-_"
  keepers = {
    rotation_id = time_rotating.function-key-rotation.id
  }
}

resource "azurerm_key_vault_secret" "default-function-key" {
  name         = "host--functionKey--default"
  value        = random_password.function-key.result
  key_vault_id = azurerm_key_vault.chart-renderer-kv.id

  depends_on = [
    azurerm_key_vault_access_policy.terraform-agent-kv-access
  ]
}
