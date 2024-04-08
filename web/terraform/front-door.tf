resource "azurerm_frontdoor" "web-app-frontdoor" {
  name                = "${var.environment-prefix}-education-benchmarking-fd"
  resource_group_name = azurerm_resource_group.resource-group.name
  tags                = local.common-tags

  routing_rule {
    name               = "web-app"
    accepted_protocols = ["Http", "Https"]
    patterns_to_match  = ["/*"]
    frontend_endpoints = ["${var.environment-prefix}-education-benchmarking-fd"]
    forwarding_configuration {
      forwarding_protocol = "MatchRequest"
      backend_pool_name   = "${var.environment-prefix}-web-app-backend"
    }
  }

  backend_pool_load_balancing {
    name = "${var.environment-prefix}-load-balancing-settings"
  }

  backend_pool_health_probe {
    name    = "${var.environment-prefix}-health-brobe-setting"
    enabled = false
  }

  backend_pool {
    name = "${var.environment-prefix}-web-app-backend"
    backend {
      host_header = "${var.environment-prefix}-education-benchmarking.azurewebsites.net"
      address     = "${var.environment-prefix}-education-benchmarking.azurewebsites.net"
      http_port   = 80
      https_port  = 443
    }

    load_balancing_name = "${var.environment-prefix}-load-balancing-settings"
    health_probe_name   = "${var.environment-prefix}-health-brobe-setting"
  }

  backend_pool_settings {
    enforce_backend_pools_certificate_name_check = true
  }

  frontend_endpoint {
    name                                    = "${var.environment-prefix}-education-benchmarking-fd"
    host_name                               = "${var.environment-prefix}-education-benchmarking-fd.azurefd.net"
    web_application_firewall_policy_link_id = azurerm_frontdoor_firewall_policy.firewall-policy.id
  }
}

resource "azurerm_frontdoor_firewall_policy" "firewall-policy" {
  name                = "WebAppFirewallPolicy"
  resource_group_name = azurerm_resource_group.resource-group.name
  tags                = local.common-tags
  mode                = "Detection"

  managed_rule {
    type    = "DefaultRuleSet"
    version = "1.0"
  }

  managed_rule {
    type    = "Microsoft_BotManagerRuleSet"
    version = "1.0"
  }
}