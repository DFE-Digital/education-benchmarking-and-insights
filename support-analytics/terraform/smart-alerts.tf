locals {
  smart_alert = {
    frequency_minute = "PT1M"
    frequency_daily  = "PT24H"
    severity         = "Sev3"
  }
}

resource "azurerm_monitor_smart_detector_alert_rule" "failure-anomalies-detector" {
  name                = "failure-anomalies"
  description         = "Detects if experiencing an abnormal rise in the rate in failed HTTP requests or dependency calls"
  resource_group_name = azurerm_resource_group.resource-group.name
  severity            = local.smart_alert.severity
  scope_resource_ids  = [data.azurerm_application_insights.application-insights.id]
  frequency           = local.smart_alert.frequency_minute
  detector_type       = "FailureAnomaliesDetector"
  tags                = local.common-tags
  enabled             = var.configuration[var.environment].smart_alerts_enabled

  action_group {
    ids = [azurerm_monitor_action_group.service-support-action.id]
  }
}

resource "azurerm_monitor_smart_detector_alert_rule" "request-performance-degradation-detector" {
  name                = "request-performance-degradation"
  description         = "Alerts if responses to requests appear more slowly than they used to"
  resource_group_name = azurerm_resource_group.resource-group.name
  severity            = local.smart_alert.severity
  scope_resource_ids  = [data.azurerm_application_insights.application-insights.id]
  frequency           = local.smart_alert.frequency_daily
  detector_type       = "RequestPerformanceDegradationDetector"
  tags                = local.common-tags
  enabled             = var.configuration[var.environment].smart_alerts_enabled

  action_group {
    ids = [azurerm_monitor_action_group.service-support-action.id]
  }
}

resource "azurerm_monitor_smart_detector_alert_rule" "dependency-performance-degradation-detector" {
  name                = "dependency-performance-degradation"
  description         = "Alerts if a dependency call has started responding to requests more slowly than it used to"
  resource_group_name = azurerm_resource_group.resource-group.name
  severity            = local.smart_alert.severity
  scope_resource_ids  = [data.azurerm_application_insights.application-insights.id]
  frequency           = local.smart_alert.frequency_daily
  detector_type       = "DependencyPerformanceDegradationDetector"
  tags                = local.common-tags
  enabled             = var.configuration[var.environment].smart_alerts_enabled

  action_group {
    ids = [azurerm_monitor_action_group.service-support-action.id]
  }
}

resource "azurerm_monitor_smart_detector_alert_rule" "exception-volume-changed-detector" {
  name                = "exception-volume-changed"
  description         = "Alerts if exhibiting an abnormal rise in the number of exceptions"
  resource_group_name = azurerm_resource_group.resource-group.name
  severity            = local.smart_alert.severity
  scope_resource_ids  = [data.azurerm_application_insights.application-insights.id]
  frequency           = local.smart_alert.frequency_daily
  detector_type       = "ExceptionVolumeChangedDetector"
  tags                = local.common-tags
  enabled             = var.configuration[var.environment].smart_alerts_enabled

  action_group {
    ids = [azurerm_monitor_action_group.service-support-action.id]
  }
}

resource "azurerm_monitor_smart_detector_alert_rule" "trace-severity-detector" {
  name                = "trace-severity"
  description         = "Alerts if the percentage of “bad” traces (logged with a level of Warning, Error, or Fatal) out of all traces is degrading"
  resource_group_name = azurerm_resource_group.resource-group.name
  severity            = local.smart_alert.severity
  scope_resource_ids  = [data.azurerm_application_insights.application-insights.id]
  frequency           = local.smart_alert.frequency_daily
  detector_type       = "TraceSeverityDetector"
  tags                = local.common-tags
  enabled             = var.configuration[var.environment].smart_alerts_enabled

  action_group {
    ids = [azurerm_monitor_action_group.service-support-action.id]
  }
}

resource "azurerm_monitor_smart_detector_alert_rule" "memory-leak-detector" {
  name                = "memory-leak"
  description         = "Alerts on a consistent increase in memory consumption over a long period of time"
  resource_group_name = azurerm_resource_group.resource-group.name
  severity            = local.smart_alert.severity
  scope_resource_ids  = [data.azurerm_application_insights.application-insights.id]
  frequency           = local.smart_alert.frequency_daily
  detector_type       = "MemoryLeakDetector"
  tags                = local.common-tags
  enabled             = var.configuration[var.environment].smart_alerts_enabled

  action_group {
    ids = [azurerm_monitor_action_group.service-support-action.id]
  }
}