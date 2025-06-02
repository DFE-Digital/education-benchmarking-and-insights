resource "azurerm_service_plan" "redirect-asp" {
  #checkov:skip=CKV_AZURE_225:Ensure the App Service Plan is zone redundant
  #checkov:skip=CKV_AZURE_212:Ensure App Service has a minimum number of instances for failover
  #checkov:skip=CKV_AZURE_211:Ensure App Service plan suitable for production use
  count               = var.configuration[var.environment].redirect_app_service ? 1 : 0
  name                = "${var.environment-prefix}-redirect-asp"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  os_type             = "Windows"
  sku_name            = "B1"
  tags                = local.common-tags
}

resource "azurerm_windows_web_app" "redirect" {
  #checkov:skip=CKV_AZURE_17:Ensure the web app has 'Client Certificates (Incoming client certificates)' set
  #checkov:skip=CKV_AZURE_213:Ensure that App Service configures health check
  #checkov:skip=CKV_AZURE_13:Ensure App Service Authentication is set on Azure App Service
  #checkov:skip=CKV_AZURE_222:Ensure that Azure Web App public network access is disabled
  #checkov:skip=CKV_AZURE_88:Ensure that app services use Azure Files
  count               = var.configuration[var.environment].redirect_app_service ? 1 : 0
  name                = "${var.environment-prefix}-redirect"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  service_plan_id     = azurerm_service_plan.redirect-asp[0].id
  https_only          = true
  tags                = local.common-tags

  identity {
    type = "SystemAssigned"
  }

  site_config {
    http2_enabled = true
    application_stack {
      current_stack  = "dotnet"
      dotnet_version = "v8.0"
    }
    use_32_bit_worker = false
    ftps_state        = "Disabled"
  }

  logs {
    failed_request_tracing  = true
    detailed_error_messages = true
    http_logs {
      file_system {
        retention_in_days = 0
        retention_in_mb   = 25
      }
    }
  }

  app_settings = {
    "ASPNETCORE_ENVIRONMENT"                = "Production"
    "APPINSIGHTS_INSTRUMENTATIONKEY"        = data.azurerm_application_insights.application-insights.instrumentation_key
    "APPLICATIONINSIGHTS_CONNECTION_STRING" = data.azurerm_application_insights.application-insights.connection_string
  }
}

resource "azurerm_monitor_diagnostic_setting" "redirect-diagnostics" {
  count                      = var.configuration[var.environment].redirect_app_service ? 1 : 0
  name                       = "${var.environment-prefix}-redirect-diagnostics"
  target_resource_id         = azurerm_windows_web_app.redirect[0].id
  log_analytics_workspace_id = data.azurerm_log_analytics_workspace.application-insights-workspace.id

  enabled_log {
    category = "AppServiceHTTPLogs"
  }

  enabled_log {
    category = "AppServiceAppLogs"
  }
}
