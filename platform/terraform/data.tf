data "azurerm_key_vault" "key-vault" {
  name                = "${var.environment-prefix}-ebis-keyvault"
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