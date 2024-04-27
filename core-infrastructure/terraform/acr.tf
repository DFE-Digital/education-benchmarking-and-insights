resource "azurerm_container_registry" "acr" {
  #checkov:skip=CKV_AZURE_233:See ADO backlog AB#206776
  #checkov:skip=CKV_AZURE_165:See ADO backlog AB#206776
  #checkov:skip=CKV_AZURE_166:See ADO backlog AB#206776
  name                          = "${var.environment-prefix}acr"
  resource_group_name           = azurerm_resource_group.resource-group.name
  location                      = azurerm_resource_group.resource-group.location
  sku                           = "Standard"
  admin_enabled                 = false
  public_network_access_enabled = false
  retention_policy {
    days    = 30
    enabled = true
  }

  identity {
    type = "SystemAssigned"
  }

  trust_policy {
    enabled = true
  }

  tags = local.common-tags
}
