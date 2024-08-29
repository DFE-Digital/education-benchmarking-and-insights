resource "azurerm_monitor_metric_alert" "availability-alert" {
  name                = "availability-alert"
  resource_group_name = azurerm_resource_group.resource-group.name
  scopes              = [data.azurerm_application_insights.application-insights.id]
  description         = "Alert for availability test failure"
  severity            = 0
  frequency           = "PT1M"
  window_size         = "PT1H"
  enabled             = var.configuration[var.environment].alerts_enabled
  tags                = local.common-tags

  criteria {
    metric_namespace = "microsoft.insights/components"
    metric_name      = "availabilityResults/availabilityPercentage"
    aggregation      = "Average"
    operator         = "LessThan"
    threshold        = var.configuration[var.environment].thresholds.availability
  }

  action {
    action_group_id = azurerm_monitor_action_group.critical-alerts-action.id
  }
}

resource "azurerm_monitor_metric_alert" "cpu_alert" {
  name                = "web-app-cpu-alert"
  resource_group_name = azurerm_resource_group.resource-group.name
  scopes              = [data.azurerm_service_plan.web-app-service-plan.id]
  description         = "Alert if CPU utilisation exceeds 90% for more than 5 minutes"
  severity            = 0
  frequency           = "PT1M"
  window_size         = "PT5M"
  enabled             = var.configuration[var.environment].alerts_enabled
  tags                = local.common-tags

  criteria {
    metric_namespace = "Microsoft.Web/serverfarms"
    metric_name      = "CpuPercentage"
    aggregation      = "Average"
    operator         = "GreaterThan"
    threshold        = var.configuration[var.environment].thresholds.cpu
  }

  action {
    action_group_id = azurerm_monitor_action_group.critical-alerts-action.id
  }
}

resource "azurerm_monitor_metric_alert" "memory_alert" {
  name                = "web-app-memory-alert"
  resource_group_name = azurerm_resource_group.resource-group.name
  scopes              = [data.azurerm_service_plan.web-app-service-plan.id]
  description         = "Alert if memory utilisation exceeds 90% for more than 5 minutes"
  severity            = 0
  frequency           = "PT1M"
  window_size         = "PT5M"
  enabled             = var.configuration[var.environment].alerts_enabled
  tags                = local.common-tags

  criteria {
    metric_namespace = "Microsoft.Web/serverfarms"
    metric_name      = "MemoryPercentage"
    aggregation      = "Average"
    operator         = "GreaterThan"
    threshold        = var.configuration[var.environment].thresholds.memory
  }

  action {
    action_group_id = azurerm_monitor_action_group.critical-alerts-action.id
  }
}

resource "azurerm_monitor_metric_alert" "web_app_error_alert" {
  name                = "web-app-error-alert"
  resource_group_name = azurerm_resource_group.resource-group.name
  scopes              = [data.azurerm_windows_web_app.web-app-service.id]
  description         = "Alert if HTTP 5xx error count exceeds 1"
  severity            = 0
  frequency           = "PT1M"
  window_size         = "PT5M"
  enabled             = var.configuration[var.environment].alerts_enabled
  tags                = local.common-tags

  criteria {
    metric_namespace = "Microsoft.Web/sites"
    metric_name      = "Http5xx"
    aggregation      = "Total"
    operator         = "GreaterThan"
    threshold        = var.configuration[var.environment].thresholds.error
  }

  action {
    action_group_id = azurerm_monitor_action_group.critical-alerts-action.id
  }
}

resource "azurerm_monitor_metric_alert" "benchmark_api_error_alert" {
  name                = "benchmark-api-error-alert"
  resource_group_name = azurerm_resource_group.resource-group.name
  scopes              = [data.azurerm_windows_function_app.benchmark-api.id]
  description         = "Alert if HTTP 5xx error count exceeds 1"
  severity            = 0
  frequency           = "PT1M"
  window_size         = "PT5M"
  enabled             = var.configuration[var.environment].alerts_enabled
  tags                = local.common-tags

  criteria {
    metric_namespace = "Microsoft.Web/sites"
    metric_name      = "Http5xx"
    aggregation      = "Total"
    operator         = "GreaterThan"
    threshold        = var.configuration[var.environment].thresholds.error
  }

  action {
    action_group_id = azurerm_monitor_action_group.critical-alerts-action.id
  }
}

resource "azurerm_monitor_metric_alert" "establishment_api_error_alert" {
  name                = "establishment-api-error-alert"
  resource_group_name = azurerm_resource_group.resource-group.name
  scopes              = [data.azurerm_windows_function_app.establishment-api.id]
  description         = "Alert if HTTP 5xx error count exceeds 1"
  severity            = 0
  frequency           = "PT1M"
  window_size         = "PT5M"
  enabled             = var.configuration[var.environment].alerts_enabled
  tags                = local.common-tags

  criteria {
    metric_namespace = "Microsoft.Web/sites"
    metric_name      = "Http5xx"
    aggregation      = "Total"
    operator         = "GreaterThan"
    threshold        = var.configuration[var.environment].thresholds.error
  }

  action {
    action_group_id = azurerm_monitor_action_group.critical-alerts-action.id
  }
}

resource "azurerm_monitor_metric_alert" "insight_api_error_alert" {
  name                = "insight-api-error-alert"
  resource_group_name = azurerm_resource_group.resource-group.name
  scopes              = [data.azurerm_windows_function_app.insight-api.id]
  description         = "Alert if HTTP 5xx error count exceeds 1"
  severity            = 0
  frequency           = "PT1M"
  window_size         = "PT5M"
  enabled             = var.configuration[var.environment].alerts_enabled
  tags                = local.common-tags

  criteria {
    metric_namespace = "Microsoft.Web/sites"
    metric_name      = "Http5xx"
    aggregation      = "Total"
    operator         = "GreaterThan"
    threshold        = var.configuration[var.environment].thresholds.error
  }

  action {
    action_group_id = azurerm_monitor_action_group.critical-alerts-action.id
  }
}