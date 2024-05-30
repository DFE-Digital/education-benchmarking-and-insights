data "azurerm_key_vault" "key-vault" {
  name                = "${var.environment-prefix}-ebis-keyvault"
  resource_group_name = "${var.environment-prefix}-ebis-core"
}

data "azurerm_application_insights" "application-insights" {
  name                = "${var.environment-prefix}-ebis-ai"
  resource_group_name = "${var.environment-prefix}-ebis-core"
}

data "azurerm_log_analytics_workspace" "application-insights-workspace" {
  name                = "${var.environment-prefix}-ebis-aiw"
  resource_group_name = "${var.environment-prefix}-ebis-core"
}

data "azurerm_container_registry" "acr" {
  name                = "${var.environment-prefix}acr"
  resource_group_name = "${var.environment-prefix}-ebis-core"
}

data "azurerm_storage_account" "main" {
  name                = "${var.environment-prefix}data"
  resource_group_name = "${var.environment-prefix}-ebis-core"
}

data "azurerm_key_vault_secret" "core-db-domain-name" {
  name         = "core-sql-domain-name"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "core-db-name" {
  name         = "core-sql-db-name"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "core-db-user-name" {
  name         = "core-sql-user-name"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "core-db-password" {
  name         = "core-sql-password"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}