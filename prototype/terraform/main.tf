locals {
  common-tags = {
    "Environment"      = var.cip-environment
    "Service Offering" = "DfE Financial Benchmarking service"
    "Product"          = "DfE Financial Benchmarking service"
    "Source"           = "terraform"
  }
}

resource "azurerm_resource_group" "resource-group" {
  name     = "${var.environment-prefix}-ebis-prototype"
  location = var.location
  tags     = local.common-tags
}

resource "azurerm_service_plan" "app-service-plan" {
  #checkov:skip=CKV_AZURE_211:Non production - prototype
  #checkov:skip=CKV_AZURE_212:Non production - prototype
  #checkov:skip=CKV_AZURE_225:Non production - prototype
  name                = "${var.environment-prefix}-ebis-asp-prototype"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  os_type             = "Linux"
  sku_name            = "F1"
  tags                = local.common-tags
}

resource "azurerm_linux_web_app" "web-app" {
  #checkov:skip=CKV_AZURE_13:Non production - prototype
  #checkov:skip=CKV_AZURE_88:Non production - prototype
  #checkov:skip=CKV_AZURE_222:Non production - prototype
  #checkov:skip=CKV_AZURE_17:Non production - prototype
  #checkov:skip=CKV_AZURE_213:Non production - prototype
  #checkov:skip=CKV_AZURE_214:Non production - prototype
  name                = "${var.environment-prefix}-ebis-prototype"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  service_plan_id     = azurerm_service_plan.app-service-plan.id
  tags                = local.common-tags
  https_only          = true

  identity {
    type = "SystemAssigned"
  }

  site_config {
    always_on           = false
    http2_enabled       = true
    minimum_tls_version = "1.2"
    app_command_line    = "npm run start"
    ftps_state          = "Disabled"
    application_stack {
      node_version = "20-lts"
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
    "NODE_ENV"                               = "production"
    "PASSWORD"                               = var.prototype-password
    "BUILD_FLAGS"                            = "Off"
    "SCM_DO_BUILD_DURING_DEPLOYMENT"         = true
    "WEBSITE_RESTORE_RAW_REQUEST_PATH"       = "1"
    "AZURE_CONTAINER_APP_INGRESS_DECODE_URL" = false
  }
}

resource "azurerm_service_plan" "app-service-plan-exp" {
  name                = "${var.environment-prefix}-ebis-asp-prototype-exp"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  os_type             = "Linux"
  sku_name            = "B1"
  tags                = local.common-tags
}

resource "azurerm_linux_web_app" "web-app-exp" {
  name                = "${var.environment-prefix}-ebis-prototype-exp"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  service_plan_id     = azurerm_service_plan.app-service-plan-exp.id
  tags                = local.common-tags
  https_only          = true

  site_config {
    always_on           = false
    http2_enabled       = true
    minimum_tls_version = "1.2"
    app_command_line    = "npm run start:custom"
    application_stack {
      node_version = "20-lts"
    }
  }

  app_settings = {
    "NODE_ENV"                       = "production"
    "PASSWORD"                       = var.prototype-password
    "BUILD_FLAGS"                    = "Off"
    "SCM_DO_BUILD_DURING_DEPLOYMENT" = false
  }
}