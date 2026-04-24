resource "azurerm_resource_group" "resource-group" {
  name     = "${var.environment-prefix}-ebis-platform"
  location = var.location
  tags     = local.common-tags
}
