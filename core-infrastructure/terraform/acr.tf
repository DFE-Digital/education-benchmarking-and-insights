resource "azurerm_container_registry" "acr" {
  #checkov:skip=CKV_AZURE_233:See ADO backlog AB#206776
  #checkov:skip=CKV_AZURE_165:See ADO backlog AB#206776
  #checkov:skip=CKV_AZURE_166:See ADO backlog AB#206776
  #checkov:skip=CKV_AZURE_167:See ADO backlog AB#206776
  #checkov:skip=CKV_AZURE_164:See ADO backlog AB#206776
  #checkov:skip=CKV_AZURE_139:See ADO backlog AB#206776
  #checkov:skip=CKV_AZURE_137:See ADO backlog AB#206776
  #checkov:skip=CKV_AZURE_237:See ADO backlog AB#206776
  name                = "${var.environment-prefix}acr"
  resource_group_name = azurerm_resource_group.resource-group.name
  location            = azurerm_resource_group.resource-group.location
  sku                 = "Standard"
  #TODO: Review but is required for the moment because can't create managed identities to auth with from the container app
  admin_enabled = true

  #TODO: Review as premium is required to limit publis access
  public_network_access_enabled = true

  #TODO: Review as premium is required for reention policy
  retention_policy {}

  identity {
    type = "SystemAssigned"
  }

  #TODO: Review as premium is required for trust policy  
  trust_policy {}

  tags = local.common-tags
}
