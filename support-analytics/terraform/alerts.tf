locals {
  pipeline-messages-finished-query = file("${path.module}/queries/pipeline-messages-finished.kql")
}

resource "azurerm_monitor_metric_alert" "availability-alert" {
  name                = "availability-alert"
  resource_group_name = azurerm_resource_group.resource-group.name
  scopes              = [data.azurerm_application_insights.application-insights.id]
  description         = "Alert if availability is below ${var.configuration[var.environment].thresholds.availability}%"
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
    action_group_id = azurerm_monitor_action_group.service-support-action.id
  }
}

resource "azurerm_monitor_metric_alert" "cpu_alert" {
  name                = "web-app-cpu-alert"
  resource_group_name = azurerm_resource_group.resource-group.name
  scopes              = [data.azurerm_service_plan.web-app-service-plan.id]
  description         = "Alert if CPU utilisation exceeds ${var.configuration[var.environment].thresholds.cpu}% for more than 5 minutes"
  severity            = 2
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
    action_group_id = azurerm_monitor_action_group.service-support-action.id
  }
}

resource "azurerm_monitor_metric_alert" "memory_alert" {
  name                = "web-app-memory-alert"
  resource_group_name = azurerm_resource_group.resource-group.name
  scopes              = [data.azurerm_service_plan.web-app-service-plan.id]
  description         = "Alert if memory utilisation exceeds ${var.configuration[var.environment].thresholds.memory}% for more than 5 minutes"
  severity            = 2
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
    action_group_id = azurerm_monitor_action_group.service-support-action.id
  }
}

resource "azurerm_monitor_metric_alert" "web_app_error_alert" {
  name                = "web-app-error-alert"
  resource_group_name = azurerm_resource_group.resource-group.name
  scopes              = [data.azurerm_windows_web_app.web-app-service.id]
  description         = "Alert if HTTP 5xx error count exceeds ${var.configuration[var.environment].thresholds.error}"
  severity            = 0
  frequency           = "PT1M"
  window_size         = "PT30M"
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
    action_group_id = azurerm_monitor_action_group.service-support-action.id
  }
}

resource "azurerm_monitor_metric_alert" "benchmark_api_error_alert" {
  name                = "benchmark-api-error-alert"
  resource_group_name = azurerm_resource_group.resource-group.name
  scopes              = [data.azurerm_windows_function_app.benchmark-api.id]
  description         = "Alert if HTTP 5xx error count exceeds ${var.configuration[var.environment].thresholds.error}"
  severity            = 0
  frequency           = "PT1M"
  window_size         = "PT30M"
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
    action_group_id = azurerm_monitor_action_group.service-support-action.id
  }
}

resource "azurerm_monitor_metric_alert" "establishment_api_error_alert" {
  name                = "establishment-api-error-alert"
  resource_group_name = azurerm_resource_group.resource-group.name
  scopes              = [data.azurerm_windows_function_app.establishment-api.id]
  description         = "Alert if HTTP 5xx error count exceeds ${var.configuration[var.environment].thresholds.error}"
  severity            = 0
  frequency           = "PT1M"
  window_size         = "PT30M"
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
    action_group_id = azurerm_monitor_action_group.service-support-action.id
  }
}

resource "azurerm_monitor_metric_alert" "insight_api_error_alert" {
  name                = "insight-api-error-alert"
  resource_group_name = azurerm_resource_group.resource-group.name
  scopes              = [data.azurerm_windows_function_app.insight-api.id]
  description         = "Alert if HTTP 5xx error count exceeds ${var.configuration[var.environment].thresholds.error}"
  severity            = 0
  frequency           = "PT1M"
  window_size         = "PT30M"
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
    action_group_id = azurerm_monitor_action_group.service-support-action.id
  }
}

resource "azurerm_monitor_scheduled_query_rules_alert_v2" "failed-finished-pipeline-messages" {
  name                    = "failed-finished-pipeline-messages-alert"
  resource_group_name     = azurerm_resource_group.resource-group.name
  scopes                  = [data.azurerm_application_insights.application-insights.id]
  location                = azurerm_resource_group.resource-group.location
  display_name            = "Failed finished pipeline messages alert"
  description             = "Alert if number of failed finished pipeline messages exceeds ${var.configuration[var.environment].thresholds.error}"
  severity                = 2
  evaluation_frequency    = "PT10M"
  window_duration         = "PT30M"
  auto_mitigation_enabled = true
  enabled                 = var.configuration[var.environment].alerts_enabled
  tags                    = local.common-tags

  criteria {
    query                   = local.pipeline-messages-finished-query
    time_aggregation_method = "Total"
    metric_measure_column   = "Events"
    operator                = "GreaterThan"
    threshold               = var.configuration[var.environment].thresholds.error

    dimension {
      name     = "dimension"
      operator = "Include"
      values   = ["False", "True"]
    }

    failing_periods {
      minimum_failing_periods_to_trigger_alert = 1
      number_of_evaluation_periods             = 1
    }
  }

  action {
    action_groups = [azurerm_monitor_action_group.service-support-action.id]
  }
}
