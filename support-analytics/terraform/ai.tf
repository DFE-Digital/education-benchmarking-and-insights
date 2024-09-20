resource "azurerm_log_analytics_query_pack" "query-pack" {
  name                = "${var.environment-prefix}-ebis-query-pack"
  resource_group_name = azurerm_resource_group.resource-group.name
  location            = azurerm_resource_group.resource-group.location
  tags                = local.common-tags
}
