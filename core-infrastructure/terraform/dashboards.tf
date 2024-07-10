resource "random_uuid" "popular-school-requests-chart-id" {}
resource "random_uuid" "popular-trust-requests-chart-id" {}
resource "random_uuid" "popular-local-authority-requests-chart-id" {}

locals {
  popular-school-requests-chart-query          = replace(replace(file("${path.module}/queries/popular-school-requests-chart.kql"), "/[\r\n]+/", "\\n"), "\"", "\\\"")
  popular-trust-requests-chart-query           = replace(replace(file("${path.module}/queries/popular-trust-requests-chart.kql"), "/[\r\n]+/", "\\n"), "\"", "\\\"")
  popular-local-authority-requests-chart-query = replace(replace(file("${path.module}/queries/popular-local-authority-requests-chart.kql"), "/[\r\n]+/", "\\n"), "\"", "\\\"")
}

resource "azurerm_portal_dashboard" "mi-dashboard" {
  name                = "${var.environment-prefix}-ebis-dashboard-mi"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  tags = merge(
    local.common-tags,
    {
      "hidden-title" = "Management Information - ${var.environment-prefix}-ebis"
  })

  dashboard_properties = templatefile("${path.module}/dashboards/mi.tpl",
    {
      workspace_id = azurerm_log_analytics_workspace.application-insights-workspace.id,

      popular_school_requests_chart_id    = random_uuid.popular-school-requests-chart-id.result,
      popular_school_requests_chart_query = local.popular-school-requests-chart-query,
      popular_school_requests_chart_title = azurerm_log_analytics_query_pack_query.popular-school-requests-chart.description

      popular_trust_requests_chart_id    = random_uuid.popular-trust-requests-chart-id.result,
      popular_trust_requests_chart_query = local.popular-trust-requests-chart-query,
      popular_trust_requests_chart_title = azurerm_log_analytics_query_pack_query.popular-trust-requests-chart.description

      popular_local_authority_requests_chart_id    = random_uuid.popular-local-authority-requests-chart-id.result
      popular_local_authority_requests_chart_query = local.popular-local-authority-requests-chart-query,
      popular_local_authority_requests_chart_title = azurerm_log_analytics_query_pack_query.popular-local-authority-requests-chart.description
  })
}

resource "azurerm_portal_dashboard" "oi-dashboard" {
  name                = "${var.environment-prefix}-ebis-dashboard-oi"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  tags = merge(
    local.common-tags,
    {
      "hidden-title" = "Operational Information - ${var.environment-prefix}-ebis"
  })

  dashboard_properties = templatefile("${path.module}/dashboards/oi.tpl",
    {
      subscription_id     = data.azurerm_subscription.current.subscription_id
      resource_group_name = azurerm_resource_group.resource-group.name

      workspace_id      = azurerm_log_analytics_workspace.application-insights-workspace.id,
      app_insights_id   = azurerm_application_insights.application-insights.id,
      app_insights_name = azurerm_application_insights.application-insights.name,
  })
}