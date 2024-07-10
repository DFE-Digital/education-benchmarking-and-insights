locals {
  query-tags = {
    "Type"   = "MI"
    "Source" = "terraform"
  }
}

resource "random_uuid" "popular-school-requests-chart-id" {}

resource "azurerm_log_analytics_saved_search" "get-establishment-requests" {
  name                       = "GetEstablishmentRequests"
  log_analytics_workspace_id = azurerm_log_analytics_workspace.application-insights-workspace.id
  category                   = "Function"
  display_name               = "GetEstablishmentRequests"
  function_alias             = "GetEstablishmentRequests"
  tags                       = local.query-tags

  query = file("${path.module}/queries/functions/get-establishment-requests.kql")
}

resource "random_uuid" "popular-trust-requests-chart-id" {}

resource "azurerm_log_analytics_query_pack_query" "popular-school-requests-chart" {
  name          = random_uuid.popular-school-requests-chart-id.result
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "Popular Requests – School"
  description   = "Chart of the most popular School requests, split by feature"
  categories    = ["applications"]
  tags          = local.query-tags

  body = file("${path.module}/queries/popular-school-requests-chart.kql")
}

resource "random_uuid" "popular-local-authority-requests-chart-id" {}

resource "azurerm_log_analytics_query_pack_query" "popular-trust-requests-chart" {
  name          = random_uuid.popular-trust-requests-chart-id.result
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "Popular Requests – Trust"
  description   = "Chart of the most popular Trust requests, split by feature"
  categories    = ["applications"]
  tags          = local.query-tags

  body = file("${path.module}/queries/popular-trust-requests-chart.kql")
}

resource "azurerm_log_analytics_query_pack_query" "popular-local-authority-requests-chart" {
  name          = random_uuid.popular-local-authority-requests-chart-id.result
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  tags          = local.query-tags
  display_name  = "Popular Requests – Local Authority"
  description   = "Chart of the most popular Local Authority requests, split by feature"
  categories    = ["applications"]

  body = file("${path.module}/queries/popular-local-authority-requests-chart.kql")
}

resource "azurerm_log_analytics_saved_search" "get-tracked-links" {
  name                       = "GetTrackedLinks"
  log_analytics_workspace_id = azurerm_log_analytics_workspace.application-insights-workspace.id
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

resource "random_uuid" "pipeline-runs-id" {}

resource "azurerm_log_analytics_query_pack_query" "pipeline-runs" {
  name          = random_uuid.pipeline-runs-id.result
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "Recent pipeline runs"
  description   = "Logs from the most recent data pipeline runs"
  categories    = ["applications"]
  tags          = local.query-tags

  body = templatefile("${path.module}/queries/pipeline-runs.kql", {
    name = "${var.environment-prefix}-ebis-data-pipeline"
  })
}

resource "azurerm_log_analytics_saved_search" "get-feature-requests" {
  name                       = "GetFeatureRequests"
  log_analytics_workspace_id = azurerm_log_analytics_workspace.application-insights-workspace.id
  category                   = "Function"
  display_name               = "GetFeatureRequests"
  function_alias             = "GetFeatureRequests"
  tags                       = local.query-tags

  query = file("${path.module}/queries/functions/get-feature-requests.kql")
}

resource "random_uuid" "feature-requests-id" {}

resource "azurerm_log_analytics_query_pack_query" "feature-requests" {
  name          = random_uuid.feature-requests-id.result
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "Feature Requests"
  description   = "Table of the most popular Feature requests, split by authenticated state"
  categories    = ["applications"]
  tags          = local.query-tags

  body = file("${path.module}/queries/feature-requests.kql")
}

resource "azurerm_log_analytics_saved_search" "get-sessions" {
  name                       = "GetSessions"
  log_analytics_workspace_id = azurerm_log_analytics_workspace.application-insights-workspace.id
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
  log_analytics_workspace_id = azurerm_log_analytics_workspace.application-insights-workspace.id
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
  log_analytics_workspace_id = azurerm_log_analytics_workspace.application-insights-workspace.id
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
  log_analytics_workspace_id = azurerm_log_analytics_workspace.application-insights-workspace.id
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
  log_analytics_workspace_id = azurerm_log_analytics_workspace.application-insights-workspace.id
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
  log_analytics_workspace_id = azurerm_log_analytics_workspace.application-insights-workspace.id
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

resource "random_uuid" "feature-requests-chart-id" {}

resource "azurerm_log_analytics_query_pack_query" "feature-requests-chart" {
  name          = random_uuid.feature-requests-chart-id.result
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  display_name  = "Application features"
  description   = "Chart of application features by establishment type"
  categories    = ["applications"]
  tags          = local.query-tags

  body = file("${path.module}/queries/feature-requests-chart.kql")
}
