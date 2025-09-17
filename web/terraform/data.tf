data "azurerm_subnet" "web-app-subnet" {
  name                 = "${var.environment-prefix}-web-app-subnet"
  virtual_network_name = "${var.environment-prefix}-app-service-network"
  resource_group_name  = "${var.environment-prefix}-ebis-core"
}

data "azurerm_application_insights" "application-insights" {
  name                = "${var.environment-prefix}-ebis-ai"
  resource_group_name = "${var.environment-prefix}-ebis-core"
}

data "azurerm_log_analytics_workspace" "application-insights-workspace" {
  name                = "${var.environment-prefix}-ebis-aiw"
  resource_group_name = "${var.environment-prefix}-ebis-core"
}

data "azurerm_key_vault" "key-vault" {
  name                = "${var.environment-prefix}-ebis-keyvault"
  resource_group_name = "${var.environment-prefix}-ebis-core"
}

data "azurerm_key_vault_secret" "insight-api-key" {
  name         = "insight-host-key"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "insight-api-host" {
  name         = "insight-host"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "benchmark-api-key" {
  name         = "benchmark-host-key"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "benchmark-api-host" {
  name         = "benchmark-host"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "establishment-api-host" {
  name         = "establishment-host"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "establishment-api-key" {
  name         = "establishment-host-key"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "local-authority-finances-api-key" {
  name         = "local-authority-finances-host-key"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "local-authority-finances-api-host" {
  name         = "local-authority-finances-host"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "non-financial-api-key" {
  name         = "non-financial-host-key"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "non-financial-api-host" {
  name         = "non-financial-host"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "chart-rendering-api-key" {
  name         = "chart-rendering-host-key"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "chart-rendering-api-host" {
  name         = "chart-rendering-host"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "content-api-key" {
  name         = "content-host-key"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "content-api-host" {
  name         = "content-host"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}