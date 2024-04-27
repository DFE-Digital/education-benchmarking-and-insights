data "azurerm_key_vault" "key-vault" {
  name                = "${var.environment-prefix}-ebis-keyvault"
  resource_group_name = "${var.environment-prefix}-ebis-core"
}

data "azurerm_application_insights" "application-insights" {
  name                = "${var.environment-prefix}-ebis-ai"
  resource_group_name = "${var.environment-prefix}-ebis-core"
}

data "azurerm_log_analytics_workspace" "application-insights-workspace" {
  name                = "${var.environment-prefix}-ebis-aiw"
  resource_group_name = "${var.environment-prefix}-ebis-core"
}

resource "azurerm_container_registry" "acr" {
  name                = "${var.environment-prefix}-ebis-aiw"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  sku                 = "Basic"
   #TODO: probably need to review managed identities so we can turn off admin, but at the min this is needed for container app's AFAIK
  admin_enabled       = true
  public_network_access_enabled = false

  #TODO: do we need geo-replications

  tags                = local.common-tags
}