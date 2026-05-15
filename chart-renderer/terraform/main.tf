resource "azurerm_resource_group" "chart-renderer" {
  name     = "${var.environment-prefix}-ebis-chart-renderer"
  location = var.location
  tags     = local.common-tags
}

data "azurerm_subnet" "compute" {
  name                 = "${var.environment-prefix}-chart-renderer-compute-subnet"
  virtual_network_name = "${var.environment-prefix}-app-service-network"
  resource_group_name  = "${var.environment-prefix}-ebis-core"
}

data "azurerm_subnet" "inbound" {
  name                 = "${var.environment-prefix}-chart-renderer-inbound-subnet"
  virtual_network_name = "${var.environment-prefix}-app-service-network"
  resource_group_name  = "${var.environment-prefix}-ebis-core"
}

data "azurerm_key_vault" "core" {
  name                = "${var.environment-prefix}-ebis-keyvault"
  resource_group_name = "${var.environment-prefix}-ebis-core"
}

data "azurerm_private_dns_zone" "app-service" {
  name                = "privatelink.azurewebsites.net"
  resource_group_name = "${var.environment-prefix}-ebis-core"
}

data "azurerm_private_dns_zone" "blob" {
  name                = "privatelink.blob.core.windows.net"
  resource_group_name = "${var.environment-prefix}-ebis-core"
}
