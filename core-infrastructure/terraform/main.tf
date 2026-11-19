locals {
  common-tags = {
    "Environment"      = var.cip-environment
    "Service Offering" = "FBIT - Financial Benchmarking & Insights Tool"
    "Product"          = "FBIT - Financial Benchmarking & Insights Tool"
    "Source"           = "terraform"
    "Context"          = var.environment
  }
}

data "azurerm_client_config" "client" {}

data "azurerm_subscription" "current" {}

data "azuread_service_principal" "sp" {
  client_id = data.azurerm_client_config.client.client_id
}

resource "azurerm_resource_group" "resource-group" {
  name     = "${var.environment-prefix}-ebis-core"
  location = var.location
  tags     = local.common-tags
}
