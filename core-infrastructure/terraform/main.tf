locals {
  common-tags = {
    "Environment"      = var.cip-environment
    "Service Offering" = "DfE Financial Benchmarking service"
    "Product"          = "DfE Financial Benchmarking service"
    "Source"           = "terraform"
  }
}

data "azurerm_client_config" "client" {}

data "azurerm_subscription" "current" {}

resource "azurerm_resource_group" "resource-group" {
  name     = "${var.environment-prefix}-ebis-core"
  location = var.location
  tags     = local.common-tags
}
