resource "azurerm_container_registry" "acr" {
  name                = "${var.environment-prefix}acr"
  resource_group_name = azurerm_resource_group.resource-group.name
  location            = azurerm_resource_group.resource-group.location
  sku                 = "Basic"
  # TODO: Need to review as needed for container apps I believe, unless I can allow the SystemAssignedIdentity
  admin_enabled                 = true
  public_network_access_enabled = false

  retention_policy {
    days    = 30
    enabled = true
  }

  identity {
    type = "SystemAssigned"
  }

  tags = local.common-tags
}
