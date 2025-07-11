resource "azurerm_service_plan" "shutter-asp" {
  #checkov:skip=CKV_AZURE_225:Ensure the App Service Plan is zone redundant
  #checkov:skip=CKV_AZURE_212:Ensure App Service has a minimum number of instances for failover
  #checkov:skip=CKV_AZURE_211:Ensure App Service plan suitable for production use
  count               = var.configuration[var.environment].shutter_app_service ? 1 : 0
  name                = "${var.environment-prefix}-shutter-asp"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  os_type             = "Linux"
  sku_name            = "B1"
  tags                = local.common-tags
}

resource "azurerm_linux_web_app" "shutter" {
  #checkov:skip=CKV_AZURE_17:Ensure the web app has 'Client Certificates (Incoming client certificates)' set
  #checkov:skip=CKV_AZURE_213:Ensure that App Service configures health check
  #checkov:skip=CKV_AZURE_13:Ensure App Service Authentication is set on Azure App Service
  #checkov:skip=CKV_AZURE_222:Ensure that Azure Web App public network access is disabled
  #checkov:skip=CKV_AZURE_88:Ensure that app services use Azure Files
  count               = var.configuration[var.environment].shutter_app_service ? 1 : 0
  name                = "${var.environment-prefix}-shutter"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  service_plan_id     = azurerm_service_plan.shutter-asp[0].id
  https_only          = true
  tags                = local.common-tags

  identity {
    type = "SystemAssigned"
  }

  site_config {
    application_stack {
      node_version = "22-lts"
    }
    always_on                         = false
    app_command_line                  = "npm run start"
    ftps_state                        = "Disabled"
    health_check_path                 = "/health"
    health_check_eviction_time_in_min = 2
    http2_enabled                     = true
    use_32_bit_worker                 = false
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
    "AZURE_LOG_LEVEL"                       = "info"
    "APPINSIGHTS_INSTRUMENTATIONKEY"        = data.azurerm_application_insights.application-insights.instrumentation_key
    "APPLICATIONINSIGHTS_CONNECTION_STRING" = data.azurerm_application_insights.application-insights.connection_string
    "ENABLE_ORYX_BUILD"                     = 0
    "LOG_LEVEL"                             = "info"
    "MARKDOWN_CONTENT"                      = "Try again later"
    "NODE_ENV"                              = "production"
    "PORT"                                  = 8080
    "ROLE_NAME"                             = "${var.environment-prefix}-shutter"
    "SCM_DO_BUILD_DURING_DEPLOYMENT"        = 0
    "WEBSITE_ENABLE_SYNC_UPDATE_SITE"       = 0
    "WEBSITE_RUN_FROM_PACKAGE"              = 1
  }
}

resource "azurerm_monitor_diagnostic_setting" "shutter-diagnostics" {
  count                      = var.configuration[var.environment].shutter_app_service ? 1 : 0
  name                       = "${var.environment-prefix}-shutter-diagnostics"
  target_resource_id         = azurerm_linux_web_app.shutter[0].id
  log_analytics_workspace_id = data.azurerm_log_analytics_workspace.application-insights-workspace.id

  enabled_log {
    category = "AppServiceHTTPLogs"
  }

  enabled_log {
    category = "AppServiceAppLogs"
  }
}
