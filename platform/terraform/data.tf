# "name" may only contain alphanumeric characters and dashes and must be between 3-24 chars
locals {
  key-vault-name = "${substr(replace(var.environment-prefix, "/[^\\w]-/", ""), 0, 24 - length("-ebis-keyvault"))}-ebis-keyvault"
}

data "azurerm_key_vault" "key-vault" {
  name                = local.key-vault-name
  resource_group_name = "${var.environment-prefix}-ebis-core"
}

data "azurerm_application_insights" "application-insights" {
  name                = "${var.environment-prefix}-ebis-ai"
  resource_group_name = "${var.environment-prefix}-ebis-core"
}

data "azurerm_subnet" "web-app-subnet" {
  name                 = "${var.environment-prefix}-web-app-subnet"
  virtual_network_name = "${var.environment-prefix}-app-service-network"
  resource_group_name  = "${var.environment-prefix}-ebis-core"
}

data "azurerm_subnet" "platform-subnet" {
  name                 = "${var.environment-prefix}-platform-subnet"
  virtual_network_name = "${var.environment-prefix}-app-service-network"
  resource_group_name  = "${var.environment-prefix}-ebis-core"
}

data "azurerm_client_config" "client" {}