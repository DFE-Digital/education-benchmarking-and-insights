locals {
  common-tags = {
    "Environment"               = var.cip-environment
    "Service Offering"          = "DfE Financial Benchmarking service"
    "Product"                   = "DfE Financial Benchmarking service"
    "Source"                    = "terraform"
  }
}

resource "azurerm_resource_group" "resource-group" {
  name = "${var.environment-prefix}-ebis-core"
  location = var.location
  tags = local.common-tags
}

resource "random_integer" "ri" {
  min = 10000
  max = 99999
}

resource "azurerm_resource_group" "resource-group" {
  name = "${var.environment-prefix}-ebis-prototype"
  location = var.location
  tags = local.common-tags
}

resource "azurerm_service_plan" "app-service-plan" {
  name                = "${var.environment-prefix}-ebis-asp-prototype"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  os_type             = "Linux"
  sku_name            = "B1"
}

resource "azurerm_linux_web_app" "web-app" {
  name                  = "${var.environment-prefix}-ebis-prototype"
  location              = azurerm_resource_group.resource-group.location
  resource_group_name   = azurerm_resource_group.resource-group.name
  service_plan_id       = azurerm_service_plan.app-service-plan.id
  https_only            = true
  site_config {
    minimum_tls_version = "1.2"
  }
}