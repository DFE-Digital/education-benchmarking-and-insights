resource "azurerm_service_plan" "redirect-asp" {
  count               = var.configuration[var.environment].redirect_app_service ? 1 : 0
  name                = "${var.environment-prefix}-redirect-asp"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  os_type             = "Linux"
  sku_name            = "B1"
  tags                = local.common-tags
}

resource "azurerm_linux_web_app" "redirect" {
  count               = var.configuration[var.environment].redirect_app_service ? 1 : 0
  name                = "${var.environment-prefix}-redirect"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  service_plan_id     = azurerm_service_plan.redirect-asp[0].id
  tags                = local.common-tags

  site_config {}

  app_settings = {
    "ASPNETCORE_ENVIRONMENT"                = "Production"
    "APPLICATIONINSIGHTS_CONNECTION_STRING" = data.azurerm_application_insights.application-insights.connection_string
  }
}

# As soon as Terraform attempts to provision custom binding it will likely fail on validation of A and TXT DNS records,
# e.g. "A TXT record pointing from asuid.schools-financial-benchmarking.service.gov.uk to XXX was not found."
# Either the custom domain binding is manually added later, or support is on-hand to make the DNS changes immediately,
# to prevent the pipeline from failing before the related app service code has even been deployed. According to the docs
# the TXT record alone may be added at this stage to pass validation. The A record change may be made at a later point.
resource "azurerm_app_service_custom_hostname_binding" "redirect" {
  count = (var.configuration[var.environment].redirect_app_service && var.environment == "production" ? 1
  : 0)
  hostname            = "schools-financial-benchmarking.service.gov.uk"
  app_service_name    = azurerm_linux_web_app.redirect[0].name
  resource_group_name = azurerm_resource_group.resource-group.name

  lifecycle {
    ignore_changes = [ssl_state, thumbprint]
  }
}