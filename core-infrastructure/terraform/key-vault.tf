resource "azurerm_key_vault" "key-vault" {
  #checkov:skip=CKV_AZURE_109:See ADO backlog AB#206344
  #checkov:skip=CKV_AZURE_189:See ADO backlog AB#206344
  #checkov:skip=CKV2_AZURE_32:See ADO backlog AB#206344
  name                            = "${var.environment-prefix}-ebis-keyvault"
  location                        = azurerm_resource_group.resource-group.location
  resource_group_name             = azurerm_resource_group.resource-group.name
  enabled_for_deployment          = true
  enabled_for_template_deployment = true
  tenant_id                       = data.azurerm_client_config.client.tenant_id
  sku_name                        = "standard"
  purge_protection_enabled        = var.disable-purge-protection == "true" ? false : true
  soft_delete_retention_days      = 7

  network_acls {
    default_action = "Allow"
    bypass         = "AzureServices"
  }

  tags = local.common-tags

  lifecycle {
    prevent_destroy = true
  }
}

resource "azurerm_key_vault_access_policy" "terraform_sp_access" {
  key_vault_id       = azurerm_key_vault.key-vault.id
  tenant_id          = data.azurerm_client_config.client.tenant_id
  object_id          = data.azurerm_client_config.client.object_id
  secret_permissions = ["Get", "List", "Set", "Delete", "Purge", "Restore"]
  key_permissions    = ["Get", "List", "Create", "Purge", "Delete"]
}
