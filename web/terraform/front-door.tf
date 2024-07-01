resource "azurerm_cdn_frontdoor_profile" "web-app-front-door-profile" {
  name                = "${var.environment-prefix}-education-benchmarking-fd-profile"
  resource_group_name = azurerm_resource_group.resource-group.name
  tags                = local.common-tags

  sku_name = var.configuration[var.environment].front_door_sku_name
}

resource "azurerm_cdn_frontdoor_origin_group" "web-app-front-door-origin-group" {
  name                     = "${var.environment-prefix}-education-benchmarking-fd-origin-group"
  cdn_frontdoor_profile_id = azurerm_cdn_frontdoor_profile.web-app-front-door-profile.id
  session_affinity_enabled = true

  load_balancing {
    additional_latency_in_milliseconds = 0
    sample_size                        = 4
    successful_samples_required        = 3
  }
}

resource "azurerm_cdn_frontdoor_origin" "web-app-front-door-origin-app-service" {
  name                          = "${var.environment-prefix}-education-benchmarking-fd-origin-app-service"
  cdn_frontdoor_origin_group_id = azurerm_cdn_frontdoor_origin_group.web-app-front-door-origin-group.id
  enabled                       = true

  certificate_name_check_enabled = false

  host_name          = "${var.environment-prefix}-education-benchmarking.azurewebsites.net"
  http_port          = 80
  https_port         = 443
  origin_host_header = "${var.environment-prefix}-education-benchmarking.azurewebsites.net"
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

  link_to_default_domain = true
}

resource "azurerm_cdn_frontdoor_firewall_policy" "web-app-front-door-waf-policy" {
  name                = "${var.environment-prefix}wafpolicy"
  resource_group_name = azurerm_resource_group.resource-group.name
  enabled             = true
  tags                = local.common-tags

  sku_name = azurerm_cdn_frontdoor_profile.web-app-front-door-profile.sku_name
  mode     = "Detection"

  dynamic "managed_rule" {
    for_each = azurerm_cdn_frontdoor_profile.web-app-front-door-profile.sku_name == "Premium_AzureFrontDoor" ? [""] : []
    content {
      type    = "DefaultRuleSet"
      version = "1.0"
      action  = "Block"
    }
  }

  dynamic "managed_rule" {
    for_each = azurerm_cdn_frontdoor_profile.web-app-front-door-profile.sku_name == "Premium_AzureFrontDoor" ? [""] : []
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
      cdn_frontdoor_firewall_policy_id = azurerm_cdn_frontdoor_firewall_policy.web-app-front-door-waf-policy.id

      association {
        domain {
          # Custom domain will need to be used for production.
          # In the meantime, the non-prod format will be `[prefix]-education-benchmarking-[random].[instance].azurefd.net`
          cdn_frontdoor_domain_id = azurerm_cdn_frontdoor_endpoint.web-app-front-door-endpoint.id
        }
        patterns_to_match = ["/*"]
      }
    }
  }
}


resource "random_uuid" "idgen" {
}

resource "random_uuid" "guidgen" {
}

resource "azurerm_application_insights_web_test" "web_app_test" {
  name                    = "${var.environment-prefix}-web-app-test"
  resource_group_name     = data.azurerm_application_insights.application-insights.resource_group_name
  location                = data.azurerm_application_insights.application-insights.location
  application_insights_id = data.azurerm_application_insights.application-insights.id
  kind                    = "ping"
  frequency               = 300
  timeout                 = 60
  enabled                 = true
  geo_locations           = ["emea-nl-ams-azr"]

  lifecycle {
    ignore_changes = [tags]
  }

  configuration = <<XML
<WebTest Name="${var.environment-prefix}-web-app-test" Id="${random_uuid.idgen.result}" Enabled="True" CssProjectStructure="" CssIteration="" Timeout="0" WorkItemIds="" xmlns="http://microsoft.com/schemas/VisualStudio/TeamTest/2010" Description="" CredentialUserName="" CredentialPassword="" PreAuthenticate="True" Proxy="default" StopOnError="False" RecordedResultFile="" ResultsLocale="">
  <Items>
    <Request Method="GET" Guid="${random_uuid.guidgen.result}" Version="1.1" Url="https://${azurerm_cdn_frontdoor_endpoint.web-app-front-door-endpoint.host_name}" ThinkTime="0" Timeout="300" ParseDependentRequests="True" FollowRedirects="True" RecordResult="True" Cache="False" ResponseTimeGoal="0" Encoding="utf-8" ExpectedHttpStatusCode="200" ExpectedResponseUrl="" ReportingName="" IgnoreHttpStatusCode="False" />
  </Items>
</WebTest>
XML
}