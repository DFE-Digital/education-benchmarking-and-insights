resource "azurerm_network_security_group" "network-security-group" {
  name                = "${var.environment-prefix}-nsg"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  tags                = local.common-tags
}

resource "azurerm_subnet_network_security_group_association" "web-app-subnet-nsg-association" {
  subnet_id                 = azurerm_subnet.web-app-subnet.id
  network_security_group_id = azurerm_network_security_group.network-security-group.id
}

resource "azurerm_virtual_network" "app-service-network" {
  name                = "${var.environment-prefix}-app-service-network"
  address_space       = ["10.0.0.0/16"]
  tags                = local.common-tags
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
}

resource "azurerm_subnet" "web-app-subnet" {
  name                 = "${var.environment-prefix}-web-app-subnet"
  resource_group_name  = azurerm_resource_group.resource-group.name
  virtual_network_name = azurerm_virtual_network.app-service-network.name
  address_prefixes     = ["10.0.1.0/24"]

  delegation {
    name = "delegation"

    service_delegation {
      name    = "Microsoft.Web/serverFarms"
      actions = ["Microsoft.Network/virtualNetworks/subnets/action"]
    }
  }

  service_endpoints = [
    "Microsoft.Web",
    "Microsoft.AzureCosmosDB"
  ]
}