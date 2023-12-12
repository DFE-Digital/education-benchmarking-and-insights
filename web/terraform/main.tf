locals {
  common-tags = {
    "Environment"      = var.cip-environment
    "Service Offering" = "DfE Financial Benchmarking service"
    "Product"          = "DfE Financial Benchmarking service"
    "Source"           = "terraform"
  }
}

data "azurerm_application_insights" "application-insights" {
  name                = "${var.environment-prefix}-ebis-ai"
  resource_group_name = "${var.environment-prefix}-ebis-core"
}

data "azurerm_key_vault" "key-vault" {
  name                            = "${var.environment-prefix}-ebis-keyvault"
  resource_group_name             = "${var.environment-prefix}-ebis-core"
}

data "azurerm_key_vault_secret" "school-api-key" {
  name         = "${var.environment-prefix}-ebis-school-fa-host-key"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "school-api-host" {
  name         = "${var.environment-prefix}-ebis-school-fa-host-host"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

resource "azurerm_resource_group" "resource-group" {
  name     = "${var.environment-prefix}-ebis-web"
  location = var.location
  tags     = local.common-tags
}

resource "azurerm_app_service_plan" "education-benchmarking-asp" {
  name                = "${var.environment-prefix}-education-benchmarking-asp"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  
  sku {
    tier = var.sizing[var.environment].tier
    size = var.sizing[var.environment].size
  }
  tags                = local.common-tags
}

resource "azurerm_windows_web_app" "education-benchmarking-as" {
  name                    = "${var.environment-prefix}-education-benchmarking"
  location                = azurerm_resource_group.resource-group.location
  resource_group_name     = azurerm_resource_group.resource-group.name
  service_plan_id         = azurerm_app_service_plan.education-benchmarking-asp.id
  client_affinity_enabled = false
  https_only              = true

  site_config {
    http2_enabled = true
  }

  app_settings = {
    "APPINSIGHTS_INSTRUMENTATIONKEY" = data.azurerm_application_insights.application-insights.instrumentation_key
    "Apis:School:Url" = data.azurerm_key_vault_secret.school-api-host.value
    "Apis:School:Key" = data.azurerm_key_vault_secret.school-api-key.value
  }
  tags = local.common-tags
}