locals {
  smart_alert = {
    frequency = "PT24H"
    severity  = "Sev3"
  }
}

resource "azurerm_monitor_smart_detector_alert_rule" "failure-anomalies-detector" {
  name                = "failure-anomalies"
  description         = "Detects if experiencing an abnormal rise in the rate in failed HTTP requests or dependency calls"
  resource_group_name = azurerm_resource_group.resource-group.name
  severity            = local.smart_alert.severity
  scope_resource_ids  = [data.azurerm_application_insights.application-insights.id]
  frequency           = local.smart_alert.frequency
  detector_type       = "FailureAnomaliesDetector"
  tags                = local.common-tags
  enabled             = var.configuration[var.environment].smart_alerts_enabled

  action_group {
    ids = [azurerm_monitor_action_group.service-support-action.id]
  }
}

resource "azurerm_monitor_smart_detector_alert_rule" "request-performance-degradation-detector" {
  name                = "request-performance-degradation"
  resource_group_name = azurerm_resource_group.resource-group.name
  severity            = local.smart_alert.severity
  scope_resource_ids  = [data.azurerm_application_insights.application-insights.id]
  frequency           = local.smart_alert.frequency
  detector_type       = "RequestPerformanceDegradationDetector"
  tags                = local.common-tags
  enabled             = var.configuration[var.environment].smart_alerts_enabled

  action_group {
    ids = [azurerm_monitor_action_group.service-support-action.id]
  }
}

resource "azurerm_monitor_smart_detector_alert_rule" "dependency-performance-degradation-detector" {
  name                = "dependency-performance-degradation"
  resource_group_name = azurerm_resource_group.resource-group.name
  severity            = local.smart_alert.severity
  scope_resource_ids  = [data.azurerm_application_insights.application-insights.id]
  frequency           = local.smart_alert.frequency
  detector_type       = "DependencyPerformanceDegradationDetector"
  tags                = local.common-tags
  enabled             = var.configuration[var.environment].smart_alerts_enabled

  action_group {
    ids = [azurerm_monitor_action_group.service-support-action.id]
  }
}

resource "azurerm_monitor_smart_detector_alert_rule" "exception-volume-changed-detector" {
  name                = "exception-volume-changed"
  resource_group_name = azurerm_resource_group.resource-group.name
  severity            = local.smart_alert.severity
  scope_resource_ids  = [data.azurerm_application_insights.application-insights.id]
  frequency           = local.smart_alert.frequency
  detector_type       = "ExceptionVolumeChangedDetector"
  tags                = local.common-tags
  enabled             = var.configuration[var.environment].smart_alerts_enabled

  action_group {
    ids = [azurerm_monitor_action_group.service-support-action.id]
  }
}

resource "azurerm_monitor_smart_detector_alert_rule" "trace-severity-detector" {
  name                = "trace-severity"
  resource_group_name = azurerm_resource_group.resource-group.name
  severity            = local.smart_alert.severity
  scope_resource_ids  = [data.azurerm_application_insights.application-insights.id]
  frequency           = local.smart_alert.frequency
  detector_type       = "TraceSeverityDetector"
  tags                = local.common-tags
  enabled             = var.configuration[var.environment].smart_alerts_enabled

  action_group {
    ids = [azurerm_monitor_action_group.service-support-action.id]
  }
}

resource "azurerm_monitor_smart_detector_alert_rule" "memory-leak-detector" {
  name                = "memory-leak"
  resource_group_name = azurerm_resource_group.resource-group.name
  severity            = local.smart_alert.severity
  scope_resource_ids  = [data.azurerm_application_insights.application-insights.id]
  frequency           = local.smart_alert.frequency
  detector_type       = "MemoryLeakDetector"
  tags                = local.common-tags
  enabled             = var.configuration[var.environment].smart_alerts_enabled

  action_group {
    ids = [azurerm_monitor_action_group.service-support-action.id]
  }
}