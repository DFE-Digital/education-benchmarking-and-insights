locals {
  dashboard-suffix = var.environment == "development" || var.environment == "feature" ? var.environment-prefix : var.environment

  popular-school-requests-query          = replace(replace(file("${path.module}/queries/popular-school-requests.kql"), "/[\r\n]+/", "\\n"), "\"", "\\\"")
  popular-trust-requests-query           = replace(replace(file("${path.module}/queries/popular-trust-requests.kql"), "/[\r\n]+/", "\\n"), "\"", "\\\"")
  popular-local-authority-requests-query = replace(replace(file("${path.module}/queries/popular-local-authority-requests.kql"), "/[\r\n]+/", "\\n"), "\"", "\\\"")
  user-counts-table-query                = replace(replace(file("${path.module}/queries/user-counts.kql"), "/[\r\n]+/", "\\n"), "\"", "\\\"")
  user-sessions-table-query              = replace(replace(file("${path.module}/queries/user-sessions.kql"), "/[\r\n]+/", "\\n"), "\"", "\\\"")
  custom-data-funnel-query               = replace(replace(file("${path.module}/queries/custom-data-funnel.kql"), "/[\r\n]+/", "\\n"), "\"", "\\\"")
  popular-commercial-resources-query     = replace(replace(file("${path.module}/queries/popular-commercial-resources.kql"), "/[\r\n]+/", "\\n"), "\"", "\\\"")
  feature-requests-query                 = replace(replace(file("${path.module}/queries/feature-requests.kql"), "/[\r\n]+/", "\\n"), "\"", "\\\"")
  weekly-active-users-query              = replace(replace(file("${path.module}/queries/weekly-active-users.kql"), "/[\r\n]+/", "\\n"), "\"", "\\\"")
}

resource "azurerm_portal_dashboard" "mi-dashboard" {
  name                = "${var.environment-prefix}-mi-dashboard"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  tags = merge(
    local.common-tags,
    {
      "hidden-title" = "Management Information - ${local.dashboard-suffix}"
  })

  dashboard_properties = templatefile("${path.module}/dashboards/mi.tpl",
    {
      workspace_id    = azurerm_log_analytics_workspace.application-insights-workspace.id
      app_insights_id = azurerm_application_insights.application-insights.id
      environment     = local.dashboard-suffix

      popular_school_requests_id    = azurerm_log_analytics_query_pack_query.popular-school-requests.name
      popular_school_requests_query = local.popular-school-requests-query,
      popular_school_requests_title = azurerm_log_analytics_query_pack_query.popular-school-requests.description

      popular_trust_requests_id    = azurerm_log_analytics_query_pack_query.popular-trust-requests.name
      popular_trust_requests_query = local.popular-trust-requests-query,
      popular_trust_requests_title = azurerm_log_analytics_query_pack_query.popular-trust-requests.description

      popular_local_authority_requests_id    = azurerm_log_analytics_query_pack_query.popular-local-authority-requests.name
      popular_local_authority_requests_query = local.popular-local-authority-requests-query,
      popular_local_authority_requests_title = azurerm_log_analytics_query_pack_query.popular-local-authority-requests.description

      user_counts_table_id    = azurerm_log_analytics_query_pack_query.user-counts-table.name
      user_counts_table_query = local.user-counts-table-query,
      user_counts_table_title = azurerm_log_analytics_query_pack_query.user-counts-table.description

      user_sessions_table_id    = azurerm_log_analytics_query_pack_query.user-sessions-table.name
      user_sessions_table_query = local.user-sessions-table-query,
      user_sessions_table_title = azurerm_log_analytics_query_pack_query.user-sessions-table.description

      custom_data_funnel_id    = azurerm_log_analytics_query_pack_query.custom-data-funnel.name
      custom_data_funnel_query = local.custom-data-funnel-query,
      custom_data_funnel_title = azurerm_log_analytics_query_pack_query.custom-data-funnel.description

      popular_commercial_resources_id    = azurerm_log_analytics_query_pack_query.popular-commercial-resources.name,
      popular_commercial_resources_query = local.popular-commercial-resources-query,
      popular_commercial_resources_title = azurerm_log_analytics_query_pack_query.popular-commercial-resources.description

      feature_requests_id    = azurerm_log_analytics_query_pack_query.feature-requests.name,
      feature_requests_query = local.feature-requests-query,
      feature_requests_title = azurerm_log_analytics_query_pack_query.feature-requests.description

      weekly_active_users_id    = azurerm_log_analytics_query_pack_query.weekly-active-users.name,
      weekly_active_users_query = local.weekly-active-users-query,
      weekly_active_users_title = azurerm_log_analytics_query_pack_query.weekly-active-users.description
  })
}

locals {
  failures-query-prefix               = replace(replace(file("${path.module}/queries/failures-query-prefix.kql"), "/[\r\n]+/", "\\n"), "\"", "\\\"")
  waf-requests-query                  = replace(replace(file("${path.module}/queries/waf-requests.kql"), "/[\r\n]+/", "\\n"), "\"", "\\\"")
  waf-blocked-requests-per-hour-query = replace(replace(file("${path.module}/queries/waf-blocked-requests-per-hour.kql"), "/[\r\n]+/", "\\n"), "\"", "\\\"")
}

resource "azurerm_portal_dashboard" "oi-dashboard" {
  name                = "${var.environment-prefix}-oi-dashboard"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  tags = merge(
    local.common-tags,
    {
      "hidden-title" = "Operational Information - ${local.dashboard-suffix}"
  })

  dashboard_properties = templatefile("${path.module}/dashboards/oi.tpl",
    {
      subscription_id     = data.azurerm_subscription.current.subscription_id
      resource_group_name = azurerm_resource_group.resource-group.name

      workspace_id      = azurerm_log_analytics_workspace.application-insights-workspace.id,
      app_insights_id   = azurerm_application_insights.application-insights.id,
      app_insights_name = azurerm_application_insights.application-insights.name,

      failures_query_prefix = local.failures-query-prefix,

      waf_requests_id    = azurerm_log_analytics_query_pack_query.waf-requests.name,
      waf_requests_query = local.waf-requests-query,
      waf_requests_title = azurerm_log_analytics_query_pack_query.waf-requests.description

      waf_blocked_requests_per_hour_id    = azurerm_log_analytics_query_pack_query.waf-blocked-requests-per-hour.name,
      waf_blocked_requests_per_hour_query = local.waf-blocked-requests-per-hour-query,
      waf_blocked_requests_per_hour_title = azurerm_log_analytics_query_pack_query.waf-blocked-requests-per-hour.description
  })
}