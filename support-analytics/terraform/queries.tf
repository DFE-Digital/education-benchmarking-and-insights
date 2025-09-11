locals {
  query-tags = {
    "Type"   = "MI"
    "Source" = "terraform"
  }
  host_name = lower(var.environment) == "production" ? data.azurerm_cdn_frontdoor_custom_domain.web-app-custom-domain[0].host_name : data.azurerm_cdn_frontdoor_endpoint.web-app-front-door-endpoint.host_name
}

resource "random_uuid" "popular-school-requests-id" {}

resource "azurerm_log_analytics_saved_search" "get-establishment-requests" {
  name                       = "GetEstablishmentRequests"
  log_analytics_workspace_id = data.azurerm_log_analytics_workspace.application-insights-workspace.id
  category                   = "Function"
  display_name               = "GetEstablishmentRequests"
  function_alias             = "GetEstablishmentRequests"
  tags                       = local.query-tags

  query = file("${path.module}/queries/functions/get-establishment-requests.kql")
}

resource "random_uuid" "popular-trust-requests-id" {}

resource "azurerm_log_analytics_query_pack_query" "popular-school-requests" {
  name          = random_uuid.popular-school-requests-id.result
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "Popular Requests – School"
  description   = "The most popular School requests, split by feature"
  categories    = ["applications"]
  tags          = local.query-tags

  body = file("${path.module}/queries/popular-school-requests.kql")
}

resource "random_uuid" "popular-local-authority-requests-id" {}

resource "azurerm_log_analytics_query_pack_query" "popular-trust-requests" {
  name          = random_uuid.popular-trust-requests-id.result
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "Popular Requests – Trust"
  description   = "The most popular Trust requests, split by feature"
  categories    = ["applications"]
  tags          = local.query-tags

  body = file("${path.module}/queries/popular-trust-requests.kql")
}

resource "azurerm_log_analytics_query_pack_query" "popular-local-authority-requests" {
  name          = random_uuid.popular-local-authority-requests-id.result
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  tags          = local.query-tags
  display_name  = "Popular Requests – Local Authority"
  description   = "The most popular Local Authority requests, split by feature"
  categories    = ["applications"]

  body = file("${path.module}/queries/popular-local-authority-requests.kql")
}

resource "azurerm_log_analytics_saved_search" "get-tracked-links" {
  name                       = "GetTrackedLinks"
  log_analytics_workspace_id = data.azurerm_log_analytics_workspace.application-insights-workspace.id
  category                   = "Function"
  display_name               = "GetTrackedLinks"
  function_alias             = "GetTrackedLinks"
  tags                       = local.query-tags

  query = templatefile("${path.module}/queries/functions/get-tracked-links.kql", {
    trackedEvents = <<EOT
      %{for index, trackedEvent in var.trackedEvents~}
      "${trackedEvent}"
      %{if index < length(var.trackedEvents) - 1}
      ,
      %{endif}
      %{endfor~}
    EOT
  })
}

resource "random_uuid" "tracked-links-id" {
  for_each = toset(var.trackedEvents)
}

locals {
  trackedEventUuids = tomap({
    for trackedEventName, uuid in random_uuid.tracked-links-id : trackedEventName => uuid.result
  })
}

resource "azurerm_log_analytics_query_pack_query" "tracked-links" {
  for_each      = local.trackedEventUuids
  name          = each.value
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "Tracked Links – ${each.key}"
  description   = "Table of ${each.key} clicks"
  categories    = ["applications"]
  tags          = local.query-tags

  body = templatefile("${path.module}/queries/tracked-links.kql", {
    name = each.key
  })
}

resource "random_uuid" "default-pipeline-runs-id" {}

resource "azurerm_log_analytics_query_pack_query" "default-pipeline-runs" {
  name          = random_uuid.default-pipeline-runs-id.result
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "Recent default pipeline runs"
  description   = "Logs from the most recent default pipeline runs"
  categories    = ["applications"]
  tags          = local.query-tags

  body = file("${path.module}/queries/pipeline-runs-default.kql")
}

resource "random_uuid" "custom-pipeline-runs-id" {}

resource "azurerm_log_analytics_query_pack_query" "custom-pipeline-runs" {
  name          = random_uuid.custom-pipeline-runs-id.result
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "Recent custom pipeline runs"
  description   = "Logs from the most recent custom pipeline runs"
  categories    = ["applications"]
  tags          = local.query-tags

  body = file("${path.module}/queries/pipeline-runs-custom.kql")
}

resource "azurerm_log_analytics_saved_search" "get-feature-requests" {
  name                       = "GetFeatureRequests"
  log_analytics_workspace_id = data.azurerm_log_analytics_workspace.application-insights-workspace.id
  category                   = "Function"
  display_name               = "GetFeatureRequests"
  function_alias             = "GetFeatureRequests"
  tags                       = local.query-tags

  query = file("${path.module}/queries/functions/get-feature-requests.kql")
}

resource "random_uuid" "feature-requests-by-auth-id" {}

resource "azurerm_log_analytics_query_pack_query" "feature-requests-by-auth" {
  name          = random_uuid.feature-requests-by-auth-id.result
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "Feature Requests"
  description   = "Table of the most popular Feature requests, split by authenticated state"
  categories    = ["applications"]
  tags          = local.query-tags

  body = file("${path.module}/queries/feature-requests.kql")
}

resource "azurerm_log_analytics_saved_search" "get-sessions" {
  name                       = "GetSessions"
  log_analytics_workspace_id = data.azurerm_log_analytics_workspace.application-insights-workspace.id
  category                   = "Function"
  display_name               = "GetSessions"
  function_alias             = "GetSessions"
  tags                       = local.query-tags

  query = file("${path.module}/queries/functions/get-sessions.kql")
}

resource "random_uuid" "session-count-per-day-id" {}

resource "azurerm_log_analytics_query_pack_query" "session-count-per-day" {
  name          = random_uuid.session-count-per-day-id.result
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "User sessions per day"
  description   = "Table of the number of user sessions per day"
  categories    = ["applications"]
  tags          = local.query-tags

  body = file("${path.module}/queries/session-count-per-day.kql")
}

resource "azurerm_log_analytics_saved_search" "get-session-length" {
  name                       = "GetSessionLength"
  log_analytics_workspace_id = data.azurerm_log_analytics_workspace.application-insights-workspace.id
  category                   = "Function"
  display_name               = "GetSessionLength"
  function_alias             = "GetSessionLength"
  tags                       = local.query-tags

  query = file("${path.module}/queries/functions/get-session-length.kql")
}

resource "random_uuid" "session-length-per-day-id" {}

resource "azurerm_log_analytics_query_pack_query" "session-length-per-day" {
  name          = random_uuid.session-length-per-day-id.result
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "Session duration per day"
  description   = "Table of the duration of user sessions per day"
  categories    = ["applications"]
  tags          = local.query-tags

  body = file("${path.module}/queries/session-length-per-day.kql")
}

resource "azurerm_log_analytics_saved_search" "get-users" {
  name                       = "GetUsers"
  log_analytics_workspace_id = data.azurerm_log_analytics_workspace.application-insights-workspace.id
  category                   = "Function"
  display_name               = "GetUsers"
  function_alias             = "GetUsers"
  tags                       = local.query-tags

  query = file("${path.module}/queries/functions/get-users.kql")
}

resource "random_uuid" "user-count-per-day-id" {}

resource "azurerm_log_analytics_query_pack_query" "user-count-per-day" {
  name          = random_uuid.user-count-per-day-id.result
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "Users"
  description   = "Table of the number of users per day"
  categories    = ["applications"]
  tags          = local.query-tags

  body = file("${path.module}/queries/user-count-per-day.kql")
}

resource "azurerm_log_analytics_saved_search" "get-auth-users" {
  name                       = "GetAuthUsers"
  log_analytics_workspace_id = data.azurerm_log_analytics_workspace.application-insights-workspace.id
  category                   = "Function"
  display_name               = "GetAuthUsers"
  function_alias             = "GetAuthUsers"
  tags                       = local.query-tags

  query = file("${path.module}/queries/functions/get-auth-users.kql")
}

resource "random_uuid" "user-auth-count-per-day-id" {}

resource "azurerm_log_analytics_query_pack_query" "user-auth-count-per-day" {
  name          = random_uuid.user-auth-count-per-day-id.result
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "Authenticated users"
  description   = "Table of the number of authenticated users per day"
  categories    = ["applications"]
  tags          = local.query-tags

  body = file("${path.module}/queries/user-auth-count-per-day.kql")
}

resource "azurerm_log_analytics_saved_search" "get-new-users" {
  name                       = "GetNewUsers"
  log_analytics_workspace_id = data.azurerm_log_analytics_workspace.application-insights-workspace.id
  category                   = "Function"
  display_name               = "GetNewUsers"
  function_alias             = "GetNewUsers"
  tags                       = local.query-tags

  query = file("${path.module}/queries/functions/get-new-users.kql")
}

resource "random_uuid" "user-new-count-per-day-id" {}

resource "azurerm_log_analytics_query_pack_query" "user-new-count-per-day" {
  name          = random_uuid.user-new-count-per-day-id.result
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "New users"
  description   = "Table of the number of new users per day"
  categories    = ["applications"]
  tags          = local.query-tags

  body = file("${path.module}/queries/user-new-count-per-day.kql")
}

resource "random_uuid" "user-counts-table-id" {}

resource "azurerm_log_analytics_query_pack_query" "user-counts-table" {
  name          = random_uuid.user-counts-table-id.result
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "User counts"
  description   = "Table of the number of users"
  categories    = ["applications"]
  tags          = local.query-tags

  body = file("${path.module}/queries/user-counts.kql")
}

resource "random_uuid" "request-counts-table-id" {}

resource "azurerm_log_analytics_query_pack_query" "request-counts-table" {
  name          = random_uuid.request-counts-table-id.result
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "Request counts"
  description   = "Table of the number of requests"
  categories    = ["applications"]
  tags          = local.query-tags

  body = file("${path.module}/queries/request-counts.kql")
}

resource "random_uuid" "user-sessions-table-id" {}

resource "azurerm_log_analytics_query_pack_query" "user-sessions-table" {
  name          = random_uuid.user-sessions-table-id.result
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "User sessions"
  description   = "Table of the number of user sessions"
  categories    = ["applications"]
  tags          = local.query-tags

  body = file("${path.module}/queries/user-sessions.kql")
}

resource "random_uuid" "custom-data-funnel-id" {}

resource "azurerm_log_analytics_query_pack_query" "custom-data-funnel" {
  name          = random_uuid.custom-data-funnel-id.result
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "Custom Data funnel"
  description   = "Funnel of users through Custom Data journey"
  categories    = ["applications"]
  tags          = local.query-tags

  body = file("${path.module}/queries/custom-data-funnel.kql")
}

resource "azurerm_log_analytics_saved_search" "get-commercial-resources" {
  name                       = "GetCommercialResources"
  log_analytics_workspace_id = data.azurerm_log_analytics_workspace.application-insights-workspace.id
  category                   = "Function"
  display_name               = "GetCommercialResources"
  function_alias             = "GetCommercialResources"
  tags                       = local.query-tags

  query = file("${path.module}/queries/functions/get-commercial-resources.kql")
}

resource "random_uuid" "popular-commercial-resources-id" {}

resource "azurerm_log_analytics_query_pack_query" "popular-commercial-resources" {
  name          = random_uuid.popular-commercial-resources-id.result
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "Commercial resources"
  description   = "Table of 10 most popular commercial resources"
  categories    = ["applications"]
  tags          = local.query-tags

  body = file("${path.module}/queries/popular-commercial-resources.kql")
}

resource "random_uuid" "feature-requests-id" {}

resource "azurerm_log_analytics_query_pack_query" "feature-requests" {
  name          = random_uuid.feature-requests-id.result
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "Application features"
  description   = "Application features by establishment type"
  categories    = ["applications"]
  tags          = local.query-tags

  body = file("${path.module}/queries/feature-requests.kql")
}

resource "random_uuid" "weekly-active-users-id" {}

resource "azurerm_log_analytics_query_pack_query" "weekly-active-users" {
  name          = random_uuid.weekly-active-users-id.result
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "Weekly active users"
  description   = "Weekly active users over past 30 days"
  categories    = ["applications"]
  tags          = local.query-tags

  body = file("${path.module}/queries/weekly-active-users.kql")
}

resource "azurerm_log_analytics_saved_search" "get-requests" {
  name                       = "GetRequests"
  log_analytics_workspace_id = data.azurerm_log_analytics_workspace.application-insights-workspace.id
  category                   = "Function"
  display_name               = "GetRequests"
  function_alias             = "GetRequests"
  tags                       = local.query-tags

  query = templatefile("${path.module}/queries/functions/get-requests.kql", {
    hostName          = local.host_name,
    environmentPrefix = var.environment-prefix
  })
}

resource "azurerm_log_analytics_saved_search" "get-waf-logs" {
  name                       = "GetWafLogs"
  log_analytics_workspace_id = data.azurerm_log_analytics_workspace.application-insights-workspace.id
  category                   = "Function"
  display_name               = "GetWafLogs"
  function_alias             = "GetWafLogs"
  tags                       = local.query-tags

  query = file("${path.module}/queries/functions/get-waf-logs.kql")
}

resource "random_uuid" "waf-requests-id" {}

resource "azurerm_log_analytics_query_pack_query" "waf-requests" {
  name          = random_uuid.waf-requests-id.result
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "Firewall requests"
  description   = "Firewall request count by host, path, rule, and action"
  categories    = ["audit"]
  tags          = local.query-tags

  body = file("${path.module}/queries/waf-requests.kql")
}

resource "azurerm_log_analytics_saved_search" "get-waf-blocked-requests" {
  name                       = "GetWafBlockedRequests"
  log_analytics_workspace_id = data.azurerm_log_analytics_workspace.application-insights-workspace.id
  category                   = "Function"
  display_name               = "GetWafBlockedRequests"
  function_alias             = "GetWafBlockedRequests"
  tags                       = local.query-tags

  query = file("${path.module}/queries/functions/get-waf-blocked-requests.kql")
}

resource "random_uuid" "waf-blocked-requests-per-hour-id" {}

resource "azurerm_log_analytics_query_pack_query" "waf-blocked-requests-per-hour" {
  name          = random_uuid.waf-blocked-requests-per-hour-id.result
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "Firewall blocked requests"
  description   = "Firewall blocked request count per hour"
  categories    = ["audit"]
  tags          = local.query-tags

  body = file("${path.module}/queries/waf-blocked-requests-per-hour.kql")
}

resource "azurerm_log_analytics_saved_search" "get-tracked-save-charts" {
  name                       = "GetTrackedSaveCharts"
  log_analytics_workspace_id = data.azurerm_log_analytics_workspace.application-insights-workspace.id
  category                   = "Function"
  display_name               = "GetTrackedSaveCharts"
  function_alias             = "GetTrackedSaveCharts"
  tags                       = local.query-tags

  query = file("${path.module}/queries/functions/get-tracked-save-charts.kql")
}

resource "azurerm_log_analytics_saved_search" "get-queue-put-messages" {
  name                       = "GetQueuePutMessages"
  log_analytics_workspace_id = data.azurerm_log_analytics_workspace.application-insights-workspace.id
  category                   = "Function"
  display_name               = "GetQueuePutMessages"
  function_alias             = "GetQueuePutMessages"
  tags                       = local.query-tags

  query = templatefile("${path.module}/queries/functions/get-queue-put-messages.kql", {
    storageAccountName = data.azurerm_storage_account.data.name
  })
}

resource "random_uuid" "dlq-new-messages-per-hour-id" {}

resource "azurerm_log_analytics_query_pack_query" "dlq-new-messages-per-hour" {
  name          = random_uuid.dlq-new-messages-per-hour-id.result
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "DLQ new messages"
  description   = "New messages added to the dead-letter queue"
  categories    = ["applications"]
  tags          = local.query-tags

  body = file("${path.module}/queries/dlq-new-messages-per-hour.kql")
}

resource "azurerm_log_analytics_saved_search" "get-fa-traces" {
  name                       = "GetFunctionAppTraces"
  log_analytics_workspace_id = data.azurerm_log_analytics_workspace.application-insights-workspace.id
  category                   = "Function"
  display_name               = "GetFunctionAppTraces"
  function_alias             = "GetFunctionAppTraces"
  tags                       = local.query-tags

  query = templatefile("${path.module}/queries/functions/get-fa-traces.kql", {
    environmentPrefix = var.environment-prefix
  })
}

resource "random_uuid" "function-app-role-instance-count-id" {}

// this query works from the dashboard but rendering the chart breaks, possibly due to large amount of data
resource "azurerm_log_analytics_query_pack_query" "function-app-role-instance-count" {
  name          = random_uuid.function-app-role-instance-count-id.result
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "Role instance count"
  description   = "Function app role instance count"
  categories    = ["applications"]
  tags          = local.query-tags

  body = file("${path.module}/queries/function-app-role-instance-count.kql")
}

resource "azurerm_log_analytics_saved_search" "get-school-financial-benchmarking-insights-summary-requests" {
  name                       = "GetSchoolFinancialBenchmarkingInsightsSummaryRequests"
  log_analytics_workspace_id = data.azurerm_log_analytics_workspace.application-insights-workspace.id
  category                   = "Function"
  display_name               = "GetSchoolFinancialBenchmarkingInsightsSummaryRequests"
  function_alias             = "GetSchoolFinancialBenchmarkingInsightsSummaryRequests"
  tags                       = local.query-tags

  query = file("${path.module}/queries/functions/get-school-financial-benchmarking-insights-summary-requests.kql")
}

resource "random_uuid" "most-popular-recent-schools-id" {}

resource "azurerm_log_analytics_query_pack_query" "most-popular-recent-schools" {
  name          = random_uuid.most-popular-recent-schools-id.result
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "Most popular schools in past 90 days"
  description   = "Top 50 most popular schools (based on priority spending requests)"
  categories    = ["applications"]
  tags          = local.query-tags

  body = file("${path.module}/queries/most-popular-recent-schools.kql")
}

resource "azurerm_log_analytics_saved_search" "get-web-warnings" {
  name                       = "GetWebWarnings"
  log_analytics_workspace_id = data.azurerm_log_analytics_workspace.application-insights-workspace.id
  category                   = "Function"
  display_name               = "GetWebWarnings"
  function_alias             = "GetWebWarnings"
  tags                       = local.query-tags

  query = file("${path.module}/queries/functions/get-web-warnings.kql")
}

resource "random_uuid" "polly-warnings-id" {}

resource "azurerm_log_analytics_query_pack_query" "polly-warnings" {
  name          = random_uuid.polly-warnings-id.result
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "Polly warning messages"
  description   = "Warning messages from the Polly policy handler in the Web project"
  categories    = ["applications"]
  tags          = local.query-tags

  body = file("${path.module}/queries/polly-warnings.kql")
}

resource "azurerm_log_analytics_saved_search" "get-search-results" {
  name                       = "GetSearchResults"
  log_analytics_workspace_id = data.azurerm_log_analytics_workspace.application-insights-workspace.id
  category                   = "Function"
  display_name               = "GetSearchResults"
  function_alias             = "GetSearchResults"
  tags                       = local.query-tags

  query = file("${path.module}/queries/functions/get-search-results.kql")
}

resource "random_uuid" "suggest-results-id" {
  for_each = toset(var.establishmentTypes)
}

locals {
  establishmentTypeSuggestResultUuids = tomap({
    for establishmentType, uuid in random_uuid.suggest-results-id : establishmentType => uuid.result
  })
}

resource "azurerm_log_analytics_query_pack_query" "suggest-results" {
  for_each      = local.establishmentTypeSuggestResultUuids
  name          = each.value
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "Suggest results - ${each.key}"
  description   = "Suggest terms and number of matching results for ${each.key}"
  categories    = ["applications"]
  tags          = local.query-tags

  body = templatefile("${path.module}/queries/suggest-results.kql", {
    establishmentType = each.key
  })
}

resource "random_uuid" "search-results-id" {
  for_each = toset(var.establishmentTypes)
}

locals {
  establishmentTypeSearchResultUuids = tomap({
    for establishmentType, uuid in random_uuid.search-results-id : establishmentType => uuid.result
  })
}

resource "azurerm_log_analytics_query_pack_query" "search-results" {
  for_each      = local.establishmentTypeSearchResultUuids
  name          = each.value
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "Search results - ${each.key}"
  description   = "Search terms, filters and number of matching results for ${each.key}"
  categories    = ["applications"]
  tags          = local.query-tags

  body = templatefile("${path.module}/queries/search-results.kql", {
    establishmentType = each.key
  })
}

resource "azurerm_log_analytics_saved_search" "get-sfb-referrer-requests" {
  name                       = "GetSfbReferrerRequests"
  log_analytics_workspace_id = data.azurerm_log_analytics_workspace.application-insights-workspace.id
  category                   = "Function"
  display_name               = "GetSfbReferrerRequests"
  function_alias             = "GetSfbReferrerRequests"
  tags                       = local.query-tags

  query = file("${path.module}/queries/functions/get-sfb-referrer-requests.kql")
}

resource "azurerm_log_analytics_saved_search" "get-tracked-auth" {
  name                       = "GetTrackedAuth"
  log_analytics_workspace_id = data.azurerm_log_analytics_workspace.application-insights-workspace.id
  category                   = "Function"
  display_name               = "GetTrackedAuth"
  function_alias             = "GetTrackedAuth"
  tags                       = local.query-tags

  query = templatefile("${path.module}/queries/functions/get-tracked-auth.kql", {
    trackedAuthEvents = <<EOT
      %{for index, trackedEvent in var.trackedAuthEvents~}
      "${trackedEvent}"
      %{if index < length(var.trackedAuthEvents) - 1}
      ,
      %{endif}
      %{endfor~}
    EOT
  })
}

resource "random_uuid" "tracked-auth-id" {
  for_each = toset(var.trackedAuthEvents)
}

locals {
  trackedAuthEventUuids = tomap({
    for trackedEventName, uuid in random_uuid.tracked-auth-id : trackedEventName => uuid.result
  })
}

resource "azurerm_log_analytics_query_pack_query" "tracked-auth" {
  for_each      = local.trackedAuthEventUuids
  name          = each.value
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "Tracked Auth – ${each.key}"
  description   = "Table of ${each.key} events"
  categories    = ["applications"]
  tags          = local.query-tags

  body = templatefile("${path.module}/queries/tracked-auth.kql", {
    name = each.key
  })
}
