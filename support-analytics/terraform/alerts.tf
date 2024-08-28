resource "azurerm_monitor_metric_alert" "availability-alert" {
  name                = "availability-alert"
  resource_group_name = azurerm_resource_group.resource-group.name
  scopes              = [data.azurerm_application_insights.application-insights.id]
  description         = "Alert for availability test failure"
  severity            = 0
  frequency           = "PT1M"
  window_size         = "PT1H"
  enabled             = var.configuration[var.environment].alerts_enabled

  criteria {
    metric_namespace = "microsoft.insights/components"
    metric_name      = "availabilityResults/availabilityPercentage"
    aggregation      = "Average"
    operator         = "LessThan"
    threshold        = 99.9
  }

  action {
    action_group_id = azurerm_monitor_action_group.critical-alerts-action.id
  }
}