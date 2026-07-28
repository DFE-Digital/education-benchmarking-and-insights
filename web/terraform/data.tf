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

data "azurerm_key_vault_secret" "school-api-key" {
  name         = "school-fc-host-key"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "school-api-host" {
  name         = "school-fc-host"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "trust-api-key" {
  name         = "trust-fc-host-key"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "trust-api-host" {
  name         = "trust-fc-host"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "local-authority-api-key" {
  name         = "local-authority-fc-host-key"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "local-authority-api-host" {
  name         = "local-authority-fc-host"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "insight-api-key" {
  name         = "insight-fc-host-key"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "insight-api-host" {
  name         = "insight-fc-host"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "benchmark-api-key" {
  name         = "benchmark-fc-host-key"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "benchmark-api-host" {
  name         = "benchmark-fc-host"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "chart-rendering-api-key" {
  name         = "chart-rendering-fc-host-key"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "chart-rendering-api-host" {
  name         = "chart-rendering-fc-host"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "content-api-key" {
  name         = "content-fc-host-key"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "content-api-host" {
  name         = "content-fc-host"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}
