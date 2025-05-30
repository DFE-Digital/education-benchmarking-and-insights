locals {
  common-tags = {
    "Environment"      = var.cip-environment
    "Service Offering" = "DfE Financial Benchmarking service"
    "Product"          = "DfE Financial Benchmarking service"
    "Source"           = "terraform"
    "Context"          = var.environment
  }
}

resource "azurerm_resource_group" "resource-group" {
  name     = "${var.environment-prefix}-ebis-prototype"
  location = var.location
  tags     = local.common-tags
}

resource "azurerm_service_plan" "app-service-plan" {
  #checkov:skip=CKV_AZURE_211:Non production service - GOV.UK Prototype Kit
  #checkov:skip=CKV_AZURE_212:Non production service - GOV.UK Prototype Kit
  #checkov:skip=CKV_AZURE_225:Non production service - GOV.UK Prototype Kit
  name                = "${var.environment-prefix}-ebis-asp-prototype"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  os_type             = "Linux"
  sku_name            = "B1"
  tags                = local.common-tags
}

resource "azurerm_linux_web_app" "web-app" {
  #checkov:skip=CKV_AZURE_13:Non production service - GOV.UK Prototype Kit
  #checkov:skip=CKV_AZURE_88:Non production service - GOV.UK Prototype Kit
  #checkov:skip=CKV_AZURE_222:Non production service - GOV.UK Prototype Kit
  #checkov:skip=CKV_AZURE_17:Non production service - GOV.UK Prototype Kit
  #checkov:skip=CKV_AZURE_213:Non production service - GOV.UK Prototype Kit
  #checkov:skip=CKV_AZURE_214:Non production service - GOV.UK Prototype Kit
  name                    = "${var.environment-prefix}-ebis-prototype"
  location                = azurerm_resource_group.resource-group.location
  resource_group_name     = azurerm_resource_group.resource-group.name
  service_plan_id         = azurerm_service_plan.app-service-plan.id
  tags                    = local.common-tags
  https_only              = true
  client_affinity_enabled = true
  identity {
    type = "SystemAssigned"
  }

  site_config {
    always_on           = true
    http2_enabled       = true
    minimum_tls_version = "1.2"
    app_command_line    = "npm run start:custom"
    ftps_state          = "Disabled"
    application_stack {
      node_version = "22-lts"
    }
  }

  logs {
    failed_request_tracing  = true
    detailed_error_messages = true
    http_logs {
      file_system {
        retention_in_days = 4
        retention_in_mb   = 25
      }
    }
  }

  app_settings = {
    "NODE_ENV"                       = "development"
    "PASSWORD"                       = var.prototype-password
    "BUILD_FLAGS"                    = "Off"
    "SCM_DO_BUILD_DURING_DEPLOYMENT" = false
  }
}
