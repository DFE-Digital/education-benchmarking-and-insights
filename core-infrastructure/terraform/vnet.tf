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

resource "azurerm_subnet" "chart-renderer-subnet" {
  name                 = "${var.environment-prefix}-chart-renderer-subnet"
  resource_group_name  = azurerm_resource_group.resource-group.name
  virtual_network_name = azurerm_virtual_network.app-service-network.name
  address_prefixes     = ["10.0.4.0/24"]

  delegation {
    name = "delegation"

    service_delegation {
      name    = "Microsoft.App/environments"
      actions = ["Microsoft.Network/virtualNetworks/subnets/action"]
    }
  }

  service_endpoints = [
    "Microsoft.Storage",
    "Microsoft.KeyVault"
  ]
}

resource "azurerm_subnet" "chart-renderer-endpoints-subnet" {
  name                 = "${var.environment-prefix}-chart-renderer-endpoints-subnet"
  resource_group_name  = azurerm_resource_group.resource-group.name
  virtual_network_name = azurerm_virtual_network.app-service-network.name
  address_prefixes     = ["10.0.5.0/24"]

  service_endpoints = [
    "Microsoft.Storage",
    "Microsoft.KeyVault"
  ]
}

resource "azurerm_subnet_network_security_group_association" "chart-renderer-subnet-nsg-association" {
  subnet_id                 = azurerm_subnet.chart-renderer-subnet.id
  network_security_group_id = azurerm_network_security_group.network-security-group.id
}

resource "azurerm_subnet_network_security_group_association" "chart-renderer-endpoints-subnet-nsg-association" {
  subnet_id                 = azurerm_subnet.chart-renderer-endpoints-subnet.id
  network_security_group_id = azurerm_network_security_group.network-security-group.id
}

resource "azurerm_private_dns_zone" "app-service-dns-zone" {
  name                = "privatelink.azurewebsites.net"
  resource_group_name = azurerm_resource_group.resource-group.name
  tags                = local.common-tags
}

resource "azurerm_private_dns_zone_virtual_network_link" "app-service-dns-zone-vnet-link" {
  name                  = "${var.environment-prefix}-app-service-dns-zone-vnet-link"
  resource_group_name   = azurerm_resource_group.resource-group.name
  private_dns_zone_name = azurerm_private_dns_zone.app-service-dns-zone.name
  virtual_network_id    = azurerm_virtual_network.app-service-network.id
}

resource "azurerm_private_dns_zone" "blob-storage-dns-zone" {
  name                = "privatelink.blob.core.windows.net"
  resource_group_name = azurerm_resource_group.resource-group.name
  tags                = local.common-tags
}

resource "azurerm_private_dns_zone_virtual_network_link" "blob-storage-dns-zone-vnet-link" {
  name                  = "${var.environment-prefix}-blob-storage-dns-zone-vnet-link"
  resource_group_name   = azurerm_resource_group.resource-group.name
  private_dns_zone_name = azurerm_private_dns_zone.blob-storage-dns-zone.name
  virtual_network_id    = azurerm_virtual_network.app-service-network.id
}

resource "azurerm_private_dns_zone" "queue-storage-dns-zone" {
  name                = "privatelink.queue.core.windows.net"
  resource_group_name = azurerm_resource_group.resource-group.name
  tags                = local.common-tags
}

resource "azurerm_private_dns_zone_virtual_network_link" "queue-storage-dns-zone-vnet-link" {
  name                  = "${var.environment-prefix}-queue-storage-dns-zone-vnet-link"
  resource_group_name   = azurerm_resource_group.resource-group.name
  private_dns_zone_name = azurerm_private_dns_zone.queue-storage-dns-zone.name
  virtual_network_id    = azurerm_virtual_network.app-service-network.id
}

resource "azurerm_private_dns_zone" "table-storage-dns-zone" {
  name                = "privatelink.table.core.windows.net"
  resource_group_name = azurerm_resource_group.resource-group.name
  tags                = local.common-tags
}

resource "azurerm_private_dns_zone_virtual_network_link" "table-storage-dns-zone-vnet-link" {
  name                  = "${var.environment-prefix}-table-storage-dns-zone-vnet-link"
  resource_group_name   = azurerm_resource_group.resource-group.name
  private_dns_zone_name = azurerm_private_dns_zone.table-storage-dns-zone.name
  virtual_network_id    = azurerm_virtual_network.app-service-network.id
}