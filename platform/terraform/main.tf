locals {
  common-tags = {
    "Environment"      = var.cip-environment
    "Service Offering" = "DfE Financial Benchmarking service"
    "Product"          = "DfE Financial Benchmarking service"
    "Source"           = "terraform"
    "Context"          = var.environment
  }
  default_app_settings = {
    "WEBSITE_RUN_FROM_PACKAGE" = "0"
    "Sql__TelemetryEnabled"    = var.configuration[var.environment].sql_telemetry_enabled
  }
}

resource "azurerm_resource_group" "resource-group" {
  name     = "${var.environment-prefix}-ebis-platform"
  location = var.location
  tags     = local.common-tags
}
