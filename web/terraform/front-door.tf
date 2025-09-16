locals {
  host_name = (lower(var.environment) == "production" ?
    azurerm_cdn_frontdoor_custom_domain.web-app-custom-domain[0].host_name :
  azurerm_cdn_frontdoor_endpoint.web-app-front-door-endpoint.host_name)
  custom-domain-ids = (lower(var.environment) == "production" ? [
    azurerm_cdn_frontdoor_custom_domain.web-app-custom-domain[0].id,
  ] : [])
  front-door-origin-shutter-enabled = var.shutter-app-service-provision == "true" && var.shutter-app-service-enabled == "true"
}

resource "azurerm_cdn_frontdoor_profile" "web-app-front-door-profile" {
  name                = "${var.environment-prefix}-education-benchmarking-fd-profile"
  resource_group_name = azurerm_resource_group.resource-group.name
  tags                = local.common-tags

  sku_name = var.configuration[var.environment].front_door_profile_sku_name

  identity {
    type = "SystemAssigned"
  }
}

resource "azurerm_cdn_frontdoor_origin_group" "web-app-front-door-origin-group" {
  name                     = "${var.environment-prefix}-education-benchmarking-fd-origin-group"
  cdn_frontdoor_profile_id = azurerm_cdn_frontdoor_profile.web-app-front-door-profile.id
  session_affinity_enabled = false

  health_probe {
    interval_in_seconds = 120
    path                = "/health"
    protocol            = "Https"
    request_type        = "GET"
  }

  load_balancing {
    additional_latency_in_milliseconds = 0
    sample_size                        = 4
    successful_samples_required        = 2
  }
}

resource "azurerm_cdn_frontdoor_origin" "web-app-front-door-origin-app-service" {
  name                          = "${var.environment-prefix}-education-benchmarking-fd-origin-app-service"
  cdn_frontdoor_origin_group_id = azurerm_cdn_frontdoor_origin_group.web-app-front-door-origin-group.id
  enabled                       = !local.front-door-origin-shutter-enabled

  certificate_name_check_enabled = false

  host_name          = "${var.environment-prefix}-education-benchmarking.azurewebsites.net"
  http_port          = 80
  https_port         = 443
  origin_host_header = "${var.environment-prefix}-education-benchmarking.azurewebsites.net"
  priority           = 1
  weight             = 1
}

resource "azurerm_cdn_frontdoor_origin" "web-app-front-door-origin-shutter" {
  count                         = var.shutter-app-service-provision == "true" ? 1 : 0
  name                          = "${var.environment-prefix}-education-benchmarking-fd-origin-shutter"
  cdn_frontdoor_origin_group_id = azurerm_cdn_frontdoor_origin_group.web-app-front-door-origin-group.id
  enabled                       = local.front-door-origin-shutter-enabled

  certificate_name_check_enabled = false

  host_name          = azurerm_linux_web_app.shutter[0].default_hostname
  http_port          = 80
  https_port         = 443
  origin_host_header = azurerm_linux_web_app.shutter[0].default_hostname
  priority           = 1
  weight             = 1
}

resource "azurerm_cdn_frontdoor_endpoint" "web-app-front-door-endpoint" {
  name                     = "${var.environment-prefix}-education-benchmarking"
  cdn_frontdoor_profile_id = azurerm_cdn_frontdoor_profile.web-app-front-door-profile.id
  tags                     = local.common-tags
}

resource "azurerm_cdn_frontdoor_route" "web-app-front-door-route" {
  name                          = "${var.environment-prefix}-education-benchmarking-fd-route"
  cdn_frontdoor_endpoint_id     = azurerm_cdn_frontdoor_endpoint.web-app-front-door-endpoint.id
  cdn_frontdoor_origin_group_id = azurerm_cdn_frontdoor_origin_group.web-app-front-door-origin-group.id
  cdn_frontdoor_origin_ids      = [azurerm_cdn_frontdoor_origin.web-app-front-door-origin-app-service.id]
  enabled                       = true

  forwarding_protocol    = "MatchRequest"
  https_redirect_enabled = true
  patterns_to_match      = ["/*"]
  supported_protocols    = ["Http", "Https"]

  cdn_frontdoor_custom_domain_ids = local.custom-domain-ids
  link_to_default_domain          = true
}

resource "azurerm_cdn_frontdoor_firewall_policy" "web-app-front-door-waf" {
  name                = "${var.environment-prefix}waf"
  resource_group_name = azurerm_resource_group.resource-group.name
  enabled             = true
  tags                = local.common-tags

  sku_name = var.configuration[var.environment].front_door_waf_policy_sku_name
  mode     = var.configuration[var.environment].waf_mode

  js_challenge_cookie_expiration_in_minutes = (azurerm_cdn_frontdoor_profile.web-app-front-door-profile.sku_name == "Premium_AzureFrontDoor" ? 30 : null)

  custom_rule {
    name     = "blockrequestmethod"
    action   = "Block"
    priority = 100
    type     = "MatchRule"

    match_condition {
      match_variable     = "RequestMethod"
      operator           = "Equal"
      negation_condition = true
      match_values       = ["GET", "POST"]
    }
  }

  dynamic "custom_rule" {
    for_each = var.environment == "test" ? ["apply"] : []

    content {
      name     = "allowsynthetictraffic"
      action   = "Allow"
      priority = 50
      type     = "MatchRule"

      match_condition {
        match_variable     = "RequestHeader"
        selector           = "x-synthetic-source"
        operator           = "Equal"
        negation_condition = false
        match_values       = ["load-tests"]
      }
    }
  }

  dynamic "managed_rule" {
    for_each = (azurerm_cdn_frontdoor_profile.web-app-front-door-profile.sku_name == "Premium_AzureFrontDoor" ?
    ["apply"] : [])
    content {
      type    = "DefaultRuleSet"
      version = "1.0"
      action  = "Block"
    }
  }

  dynamic "managed_rule" {
    for_each = (azurerm_cdn_frontdoor_profile.web-app-front-door-profile.sku_name == "Premium_AzureFrontDoor" ?
    ["apply"] : [])
    content {
      type    = "Microsoft_BotManagerRuleSet"
      version = "1.0"
      action  = "Log"
    }
  }
}

resource "azurerm_cdn_frontdoor_security_policy" "web-app-front-door-security-policy" {
  name                     = "${var.environment-prefix}-education-benchmarking-fd-security-policy"
  cdn_frontdoor_profile_id = azurerm_cdn_frontdoor_profile.web-app-front-door-profile.id

  security_policies {
    firewall {
      cdn_frontdoor_firewall_policy_id = azurerm_cdn_frontdoor_firewall_policy.web-app-front-door-waf.id

      association {
        domain {
          cdn_frontdoor_domain_id = azurerm_cdn_frontdoor_endpoint.web-app-front-door-endpoint.id

        }

        dynamic "domain" {
          for_each = toset(local.custom-domain-ids)
          content {
            cdn_frontdoor_domain_id = domain.value
          }
        }

        patterns_to_match = ["/*"]
      }
    }
  }
}

resource "azurerm_cdn_frontdoor_custom_domain" "web-app-custom-domain" {
  count                    = lower(var.environment) == "production" ? 1 : 0
  name                     = "${var.environment-prefix}-custom-domain"
  cdn_frontdoor_profile_id = azurerm_cdn_frontdoor_profile.web-app-front-door-profile.id
  host_name                = "financial-benchmarking-and-insights-tool.education.gov.uk"

  tls {
    certificate_type = "ManagedCertificate"
  }
}

resource "azurerm_cdn_frontdoor_custom_domain_association" "web-app-custom-domain" {
  count                          = lower(var.environment) == "production" ? 1 : 0
  cdn_frontdoor_custom_domain_id = azurerm_cdn_frontdoor_custom_domain.web-app-custom-domain[0].id
  cdn_frontdoor_route_ids        = [azurerm_cdn_frontdoor_route.web-app-front-door-route.id]
}

resource "random_uuid" "idgen" {
}

resource "random_uuid" "guidgen" {
}

resource "azurerm_application_insights_web_test" "web_app_test" {
  name                    = "${var.environment-prefix}-web-app-test"
  description             = "Web application availability test"
  resource_group_name     = data.azurerm_application_insights.application-insights.resource_group_name
  location                = data.azurerm_application_insights.application-insights.location
  application_insights_id = data.azurerm_application_insights.application-insights.id
  kind                    = "ping"
  frequency               = 600
  timeout                 = 60
  enabled                 = true
  retry_enabled           = true
  geo_locations           = ["emea-se-sto-edge", "emea-ru-msa-edge"]

  lifecycle {
    ignore_changes = [tags]
  }

  configuration = <<XML
<WebTest Name="${var.environment-prefix}-web-app-test" Id="${random_uuid.idgen.result}" Enabled="True" CssProjectStructure="" CssIteration="" Timeout="60" WorkItemIds="" xmlns="http://microsoft.com/schemas/VisualStudio/TeamTest/2010" Description="" CredentialUserName="" CredentialPassword="" PreAuthenticate="True" Proxy="default" StopOnError="False" RecordedResultFile="" ResultsLocale="">
  <Items>
    <Request Method="GET" Guid="${random_uuid.guidgen.result}" Version="1.1" Url="https://${local.host_name}" ThinkTime="0" Timeout="60" ParseDependentRequests="True" FollowRedirects="True" RecordResult="True" Cache="False" ResponseTimeGoal="60" Encoding="utf-8" ExpectedHttpStatusCode="200" ExpectedResponseUrl="" ReportingName="" IgnoreHttpStatusCode="False" />
  </Items>
</WebTest>
XML
}

resource "azurerm_monitor_diagnostic_setting" "front-door-analytics" {
  name                       = "${var.environment-prefix}-front-door-diagnostic-setting"
  target_resource_id         = azurerm_cdn_frontdoor_profile.web-app-front-door-profile.id
  log_analytics_workspace_id = data.azurerm_log_analytics_workspace.application-insights-workspace.id

  enabled_log {
    category = "FrontdoorAccessLog"
  }

  enabled_log {
    category = "FrontdoorWebApplicationFirewallLog"
  }

  enabled_log {
    category = "FrontdoorHealthProbeLog"
  }

  enabled_metric {
    category = "AllMetrics"
  }
}

resource "azurerm_cdn_frontdoor_origin_group" "web-assets-front-door-origin-group" {
  name                     = "${var.environment-prefix}-ebis-fd-origin-group-web-assets"
  cdn_frontdoor_profile_id = azurerm_cdn_frontdoor_profile.web-app-front-door-profile.id
  session_affinity_enabled = false

  load_balancing {
    sample_size                 = 4
    successful_samples_required = 3
  }

  health_probe {
    path                = "/"
    request_type        = "HEAD"
    protocol            = "Https"
    interval_in_seconds = 100
  }
}

resource "azapi_update_resource" "data-front-door-origin-group-authentication" {
  type        = "Microsoft.Cdn/profiles/origingroups@2025-06-01"
  resource_id = azurerm_cdn_frontdoor_origin_group.web-assets-front-door-origin-group.id

  body = {
    properties = {
      authentication = {
        scope                = "https://storage.azure.com/.default",
        type                 = "SystemAssignedIdentity",
        userAssignedIdentity = null
      }
    }
  }
}

# The below may raise the error `argument "principal_id" is required, but no definition was found` even
# when used with `skip_service_principal_aad_check = true`. Manually assigning the system assigned 
# managed identity and re-running should resolve, but this is not an ideal manual intervention.
resource "azurerm_role_assignment" "front-door-profile-web-assets-storage-reader" {
  scope                            = azurerm_storage_account.web-assets-storage.id
  role_definition_name             = "Storage Blob Data Reader"
  principal_id                     = azurerm_cdn_frontdoor_profile.web-app-front-door-profile.identity[0].principal_id
  principal_type                   = "ServicePrincipal"
  skip_service_principal_aad_check = true

  depends_on = [
    azurerm_cdn_frontdoor_profile.web-app-front-door-profile,
    azapi_update_resource.data-front-door-origin-group-authentication
  ]
}

resource "azurerm_cdn_frontdoor_origin" "web-app-front-door-origin-web-assets" {
  name                          = "${var.environment-prefix}-education-benchmarking-fd-origin-web-assets"
  cdn_frontdoor_origin_group_id = azurerm_cdn_frontdoor_origin_group.web-assets-front-door-origin-group.id
  enabled                       = !local.front-door-origin-shutter-enabled

  certificate_name_check_enabled = false

  host_name          = "${azurerm_storage_account.web-assets-storage.name}.blob.core.windows.net"
  http_port          = 80
  https_port         = 443
  origin_host_header = "${azurerm_storage_account.web-assets-storage.name}.blob.core.windows.net"
  priority           = 1
  weight             = 1
}

resource "azurerm_cdn_frontdoor_route" "web-assets-front-door-route" {
  name                          = "${var.environment-prefix}-education-benchmarking-fd-route-web-assets"
  cdn_frontdoor_endpoint_id     = azurerm_cdn_frontdoor_endpoint.web-app-front-door-endpoint.id
  cdn_frontdoor_origin_group_id = azurerm_cdn_frontdoor_origin_group.web-assets-front-door-origin-group.id
  cdn_frontdoor_origin_ids      = [azurerm_cdn_frontdoor_origin.web-app-front-door-origin-web-assets.id]
  enabled                       = true

  forwarding_protocol    = "HttpsOnly"
  https_redirect_enabled = true
  patterns_to_match      = [for key in var.web-asset-containers : "/${key}/*"]
  supported_protocols    = ["Http", "Https"]
  link_to_default_domain = true

  cdn_frontdoor_origin_path       = "/"
  cdn_frontdoor_custom_domain_ids = local.custom-domain-ids
  cdn_frontdoor_rule_set_ids      = [azurerm_cdn_frontdoor_rule_set.web-assets-rules.id]

  cache {
    query_string_caching_behavior = "IgnoreQueryString"
    compression_enabled           = false
  }
}

resource "azurerm_cdn_frontdoor_rule_set" "web-assets-rules" {
  name                     = "${var.environment-prefix}webassetsruleset"
  cdn_frontdoor_profile_id = azurerm_cdn_frontdoor_profile.web-app-front-door-profile.id
}

resource "azurerm_cdn_frontdoor_rule" "web-assets-rule" {
  for_each                  = toset(var.web-asset-containers)
  name                      = "${var.environment-prefix}webassets${each.value}"
  cdn_frontdoor_rule_set_id = azurerm_cdn_frontdoor_rule_set.web-assets-rules.id
  order                     = index(var.web-asset-containers, each.value) + 1
  behavior_on_match         = "Stop"

  depends_on = [
    azurerm_cdn_frontdoor_origin.web-app-front-door-origin-web-assets,
    azurerm_cdn_frontdoor_origin_group.web-assets-front-door-origin-group
  ]

  actions {
    url_rewrite_action {
      source_pattern = "/"
      destination    = "/{url_path}"
    }
  }

  conditions {
    url_path_condition {
      match_values = ["/${each.value}/"]
      operator     = "BeginsWith"
      transforms   = ["Lowercase"]
    }
  }
}
