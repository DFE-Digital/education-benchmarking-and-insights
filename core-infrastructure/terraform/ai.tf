resource "azurerm_log_analytics_workspace" "application-insights-workspace" {
  name                = "${var.environment-prefix}-ebis-aiw"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  retention_in_days   = 180
  tags                = local.common-tags
}

resource "azurerm_application_insights" "application-insights" {
  name                = "${var.environment-prefix}-ebis-ai"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  application_type    = "web"
  workspace_id        = azurerm_log_analytics_workspace.application-insights-workspace.id
  tags                = local.common-tags
}

###Added due to bug in TF/Azure whereby resources are auto created but destroy not cascaded
resource "azurerm_monitor_action_group" "action-group" {
  name                = "Application Insights Smart Detection"
  resource_group_name = azurerm_resource_group.resource-group.name
  short_name          = "SmartDetect"
  tags                = local.common-tags
}

resource "azurerm_monitor_smart_detector_alert_rule" "failure-anomalies-detector" {
  name                = "${var.environment-prefix}-failure-anomalies-smart-detector-alert-rule"
  resource_group_name = azurerm_resource_group.resource-group.name
  severity            = "Sev2"
  scope_resource_ids  = [azurerm_application_insights.application-insights.id]
  frequency           = "PT1M"
  detector_type       = "FailureAnomaliesDetector"
  tags                = local.common-tags

  action_group {
    ids = [azurerm_monitor_action_group.action-group.id]
  }
}

resource "azurerm_log_analytics_query_pack" "query-pack" {
  name                = "${var.environment-prefix}-ebis-query-pack"
  resource_group_name = azurerm_resource_group.resource-group.name
  location            = azurerm_resource_group.resource-group.location
  tags                = local.common-tags
}

resource "azurerm_log_analytics_query_pack_query" "establishment-requests" {
  name          = "5fd0997f-94e2-481f-a390-3ebedf324ca1"
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  body          = "AppRequests\n| extend Urn = tostring(Properties[\"Urn\"]), CompanyNumber = tostring(Properties[\"CompanyNumber\"]), Code = tostring(Properties[\"Code\"]), Establishment = tostring(Properties[\"Establishment\"]), Feature = tostring(Properties[\"Feature\"])\n| where isnotempty(Establishment)\n| project TimeGenerated, Name, ResultCode, OperationId, UserId, Establishment, Feature, Identifier = iff(Establishment == \"school\", Urn, iff(Establishment == \"trust\", CompanyNumber, iff(Establishment == \"local-authority\", Code, \"\")))"
  display_name  = "Requests - Establishments"
  categories    = ["applications"]
  tags          = local.common-tags
}