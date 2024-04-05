data "azurerm_subnet" "web-app-subnet" {
  name                 = "${var.environment-prefix}-web-app-subnet"
  virtual_network_name = "${var.environment-prefix}-app-service-network"
  resource_group_name  = "${var.environment-prefix}-ebis-core"
}