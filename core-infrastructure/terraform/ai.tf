resource "azurerm_log_analytics_workspace" "application-insights-workspace" {
  name                = "${var.environment-prefix}-ebis-aiw"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  retention_in_days   = 180
  tags                = local.common-tags
}

resource "azurerm_application_insights" "application-insights" {
  name                = "${var.environment-prefix}-ebis-ai"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  application_type    = "web"
  workspace_id        = azurerm_log_analytics_workspace.application-insights-workspace.id
  tags                = local.common-tags
}