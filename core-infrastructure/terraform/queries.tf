resource "azurerm_log_analytics_saved_search" "get-establishment-requests" {
  name                       = "GetEstablishmentRequests"
  log_analytics_workspace_id = azurerm_log_analytics_workspace.application-insights-workspace.id

  category       = "Function"
  display_name   = "GetEstablishmentRequests"
  function_alias = "GetEstablishmentRequests"
  query          = <<-EOT
    AppRequests
    | extend
        Urn = tostring(Properties["Urn"]),
        CompanyNumber = tostring(Properties["CompanyNumber"]),
        Code = tostring(Properties["Code"]),
        Establishment = tostring(Properties["Establishment"]),
        Feature = tostring(Properties["Feature"])
    | where isnotempty(Establishment)
    | project
        TimeGenerated,
        Name,
        ResultCode,
        OperationId,
        UserId,
        Establishment,
        Feature,
        Identifier = iff(Establishment == "school", Urn, iff(Establishment == "trust", CompanyNumber, iff(Establishment == "local-authority", Code, "")))
  EOT
  tags           = local.common-tags
}

resource "azurerm_log_analytics_query_pack_query" "popular-school-requests-chart" {
  name          = "5fd0997f-94e2-481f-a390-3ebedf324ca2"
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  body          = <<-EOT
    GetEstablishmentRequests
    | where 
        Establishment == "school" and 
        ResultCode == 200
    | project 
        TimeGenerated, 
        Feature, 
        Urn = Identifier
    | summarize 
        Count=count() by Urn, 
        Feature
    | sort by 
        Count desc
    | take(50)
    | render 
        columnchart with(title="50 most popular school requests")
  EOT
  display_name  = "Popular Requests - School"
  description   = "Chart of the most popular School requests, split by feature"
  categories    = ["applications"]
  tags          = local.common-tags
}

resource "azurerm_log_analytics_query_pack_query" "popular-trust-requests-chart" {
  name          = "5fd0997f-94e2-481f-a390-3ebedf324ca3"
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  body          = <<-EOT
    GetEstablishmentRequests
    | where 
        Establishment == "trust" and 
        ResultCode == 200
    | project 
        TimeGenerated, 
        Feature, 
        CompanyNumber = Identifier
    | summarize 
        Count=count() by CompanyNumber, 
        Feature
    | sort by 
        Count desc
    | take(50)
    | render 
        columnchart with(title="50 most popular trust requests")
  EOT
  display_name  = "Popular Requests - Trust"
  description   = "Chart of the most popular Trust requests, split by feature"
  categories    = ["applications"]
  tags          = local.common-tags
}

resource "azurerm_log_analytics_query_pack_query" "popular-local-authority-requests-chart" {
  name          = "5fd0997f-94e2-481f-a390-3ebedf324ca4"
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  body          = <<-EOT
    GetEstablishmentRequests
    | where 
        Establishment == "local-authority" and 
        ResultCode == 200
    | project 
        TimeGenerated, 
        Feature, 
        Code = Identifier
    | summarize 
        Count=count() by Code, 
        Feature
    | sort by 
        Count desc
    | take(50)
    | render 
        columnchart with(title="50 most popular local authority requests")
  EOT
  display_name  = "Popular Requests - Local Authority"
  description   = "Chart of the most popular Local Authority requests, split by feature"
  categories    = ["applications"]
  tags          = local.common-tags
}

resource "azurerm_log_analytics_saved_search" "get-tracked-links" {
  name                       = "GetTrackedLinks"
  log_analytics_workspace_id = azurerm_log_analytics_workspace.application-insights-workspace.id

  category       = "Function"
  display_name   = "GetTrackedLinks"
  function_alias = "GetTrackedLinks"
  query          = <<-EOT
    AppEvents
    | where 
        Properties["baseTypeSource"] == "ClickEvent" and 
        Name in (
            "gias-school-details", 
            "commercial-resource", 
            "guidance-resource",
            "data-source", 
            "organisation",
            "service-banner", 
            "change-organisation"
        )
    | extend
        Source = tostring(Properties["refUri"]),
        Target = tostring(Properties["targetUri"])
    | join kind=leftouter
        (
        AppRequests
        | extend
            Urn = tostring(Properties["Urn"]),
            CompanyNumber = tostring(Properties["CompanyNumber"]),
            Code = tostring(Properties["Code"]),
            Establishment = tostring(Properties["Establishment"]),
            Feature = tostring(Properties["Feature"])
        | where isnotempty(Establishment))
        on $left.ParentId == $right.OperationId
    | project
        TimeGenerated,
        Name,
        Source,
        Target,
        OperationId,
        UserId,
        Establishment,
        Feature,
        Identifier = iff(Establishment == "school", Urn, iff(Establishment == "trust", CompanyNumber, iff(Establishment == "local-authority", Code, "")))
  EOT
  tags           = local.common-tags
}

resource "azurerm_log_analytics_query_pack_query" "tracked-links-gias-school-details" {
  name          = "5fd0997f-94e2-481f-a390-3ebedf324ca5"
  query_pack_id = azurerm_log_analytics_query_pack.query-pack.id
  body          = <<-EOT
    GetTrackedLinks
    | where 
        Name == "gias-school-details"
    | project 
        TimeGenerated, 
        Source,
        Target,
        Establishment,
        Feature, 
        Identifier
  EOT
  display_name  = "Tracked Links - GIAS school details"
  description   = "Table of GIAS school details clicks"
  categories    = ["applications"]
  tags          = local.common-tags
}