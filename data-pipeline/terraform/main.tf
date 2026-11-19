locals {
  common-tags = {
    "Environment"      = var.cip-environment
    "Service Offering" = "FBIT - Financial Benchmarking & Insights Tool"
    "Product"          = "FBIT - Financial Benchmarking & Insights Tool"
    "Source"           = "terraform"
    "Context"          = var.environment
  }
}

resource "azurerm_resource_group" "resource-group" {
  name     = "${var.environment-prefix}-ebis-pipelines"
  location = var.use-secondary-location == "true" ? var.secondary-location : var.location
  tags     = local.common-tags
}
