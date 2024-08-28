locals {
  common-tags = {
    "Environment"      = var.cip-environment
    "Service Offering" = "DfE Financial Benchmarking service"
    "Product"          = "DfE Financial Benchmarking service"
    "Source"           = "terraform"
  }
}

resource "azurerm_resource_group" "resource-group" {
  name     = "${var.environment-prefix}-ebis-support"
  location = var.location
  tags     = local.common-tags
}

resource "azurerm_monitor_action_group" "critical-alerts-action" {
  name                = "CriticalAlertsAction"
  resource_group_name = azurerm_resource_group.resource-group.name
  short_name          = "p0action"

  email_receiver {
    name                    = "send-to-support"
    email_address           = var.support-alert-email
    use_common_alert_schema = true
  }
}

