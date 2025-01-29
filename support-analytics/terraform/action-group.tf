resource "azurerm_monitor_action_group" "service-support-action" {
  name                = "Service support"
  resource_group_name = azurerm_resource_group.resource-group.name
  short_name          = "Support"
  tags                = local.common-tags

  dynamic "email_receiver" {
    for_each = var.configuration[var.environment].email_alerts_enabled ? ["true"] : []
    content {
      name                    = "send-to-support"
      email_address           = var.support-alert-email
      use_common_alert_schema = true
    }
  }

  webhook_receiver {
    name                    = "send-to-teams"
    service_uri             = azurerm_logic_app_trigger_http_request.http-trigger.callback_url
    use_common_alert_schema = true
  }
}
