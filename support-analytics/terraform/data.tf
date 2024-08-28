data "azurerm_client_config" "client" {}

data "azurerm_application_insights" "application-insights" {
  name                = "${var.environment-prefix}-ebis-ai"
  resource_group_name = "${var.environment-prefix}-ebis-core"
}

data "azurerm_service_plan" "web-app-service-plan" {
  name                = "${var.environment-prefix}-education-benchmarking-asp"
  resource_group_name = "${var.environment-prefix}-ebis-web"
}