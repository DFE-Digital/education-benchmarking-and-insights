data "azurerm_container_registry" "acr" {
  name                = var.registry-name
  resource_group_name = var.core-resource-group-name
}

data "azurerm_storage_account" "main" {
  name                = var.storage-account-name
  resource_group_name = var.core-resource-group-name
}

data "azurerm_key_vault" "key-vault" {
  name                = var.key-vault-name
  resource_group_name = var.core-resource-group-name
}

data "azurerm_key_vault_secret" "core-db-domain-name" {
  name         = var.core-db-domain-name-secret-name
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "core-db-name" {
  name         = var.core-db-name-secret-name
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "core-db-user-name" {
  name         = var.core-db-user-name-secret-name
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "core-db-password" {
  name         = var.core-db-password-secret-name
  key_vault_id = data.azurerm_key_vault.key-vault.id
}
