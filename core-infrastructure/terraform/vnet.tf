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

resource "azurerm_subnet_network_security_group_association" "platform-subnet-nsg-association" {
  subnet_id                 = azurerm_subnet.platform-subnet.id
  network_security_group_id = azurerm_network_security_group.network-security-group.id
}

resource "azurerm_virtual_network" "app-service-network" {
  name                = "${var.environment-prefix}-app-service-network"
  address_space       = ["10.0.0.0/16"]
  tags                = local.common-tags
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
}

resource "azurerm_subnet" "platform-subnet" {
  name                 = "${var.environment-prefix}-platform-subnet"
  resource_group_name  = azurerm_resource_group.resource-group.name
  virtual_network_name = azurerm_virtual_network.app-service-network.name
  address_prefixes     = ["10.0.2.0/24"]
  service_endpoints    = ["Microsoft.Sql", "Microsoft.Storage"]
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

resource "azurerm_subnet" "load-test-subnet" {
  name                 = "${var.environment-prefix}-load-test-subnet"
  resource_group_name  = azurerm_resource_group.resource-group.name
  virtual_network_name = azurerm_virtual_network.app-service-network.name
  address_prefixes     = ["10.0.3.0/24"]

  service_endpoints = [
    "Microsoft.Web"
  ]
}

resource "azurerm_subnet_network_security_group_association" "load-test-subnet-nsg-association" {
  subnet_id                 = azurerm_subnet.load-test-subnet.id
  network_security_group_id = azurerm_network_security_group.network-security-group.id
}