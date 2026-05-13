resource "azurerm_resource_group" "chart-renderer" {
  name     = "${var.environment-prefix}-ebis-chart-renderer"
  location = var.location
  tags     = local.common-tags
}

data "azurerm_resource_group" "core" {
  name = "${var.environment-prefix}-ebis-core"
}

data "azurerm_subnet" "outbound" {
  name                 = "${var.environment-prefix}-chart-renderer-subnet"
  virtual_network_name = "${var.environment-prefix}-app-service-network"
  resource_group_name  = data.azurerm_resource_group.core.name
}

data "azurerm_subnet" "endpoints" {
  name                 = "${var.environment-prefix}-chart-renderer-endpoints-subnet"
  virtual_network_name = "${var.environment-prefix}-app-service-network"
  resource_group_name  = data.azurerm_resource_group.core.name
}

data "azurerm_key_vault" "core" {
  name                = "${var.environment-prefix}-ebis-keyvault"
  resource_group_name = data.azurerm_resource_group.core.name
}

data "azurerm_private_dns_zone" "app-service" {
  name                = "privatelink.azurewebsites.net"
  resource_group_name = data.azurerm_resource_group.core.name
}

data "azurerm_private_dns_zone" "blob" {
  name                = "privatelink.blob.core.windows.net"
  resource_group_name = data.azurerm_resource_group.core.name
}

data "azurerm_private_dns_zone" "queue" {
  name                = "privatelink.queue.core.windows.net"
  resource_group_name = data.azurerm_resource_group.core.name
}

data "azurerm_private_dns_zone" "table" {
  name                = "privatelink.table.core.windows.net"
  resource_group_name = data.azurerm_resource_group.core.name
}
