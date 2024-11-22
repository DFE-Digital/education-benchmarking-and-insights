data "azurerm_client_config" "client" {}

data "azurerm_application_insights" "application-insights" {
  name                = "${var.environment-prefix}-ebis-ai"
  resource_group_name = "${var.environment-prefix}-ebis-core"
}

data "azurerm_service_plan" "web-app-service-plan" {
  name                = "${var.environment-prefix}-education-benchmarking-asp"
  resource_group_name = "${var.environment-prefix}-ebis-web"
}

data "azurerm_windows_web_app" "web-app-service" {
  name                = "${var.environment-prefix}-education-benchmarking"
  resource_group_name = "${var.environment-prefix}-ebis-web"
}

data "azurerm_windows_function_app" "benchmark-api" {
  name                = "${var.environment-prefix}-ebis-benchmark-fa"
  resource_group_name = "${var.environment-prefix}-ebis-platform"
}

data "azurerm_windows_function_app" "establishment-api" {
  name                = "${var.environment-prefix}-ebis-establishment-fa"
  resource_group_name = "${var.environment-prefix}-ebis-platform"
}

data "azurerm_windows_function_app" "insight-api" {
  name                = "${var.environment-prefix}-ebis-insight-fa"
  resource_group_name = "${var.environment-prefix}-ebis-platform"
}

data "azurerm_log_analytics_workspace" "application-insights-workspace" {
  name                = "${var.environment-prefix}-ebis-aiw"
  resource_group_name = "${var.environment-prefix}-ebis-core"
}

data "azurerm_storage_account" "data" {
  name                = "${var.environment-prefix}data"
  resource_group_name = "${var.environment-prefix}-ebis-core"
}

data "azurerm_cdn_frontdoor_endpoint" "web-app-front-door-endpoint" {
  name                = "${var.environment-prefix}-education-benchmarking"
  profile_name        = "${var.environment-prefix}-education-benchmarking-fd-profile"
  resource_group_name = "${var.environment-prefix}-ebis-web"
}

data "azurerm_cdn_frontdoor_custom_domain" "web-app-custom-domain" {
  count               = lower(var.environment) == "production" ? 1 : 0
  name                = "${var.environment-prefix}-custom-domain"
  profile_name        = "${var.environment-prefix}-education-benchmarking-fd-profile"
  resource_group_name = "${var.environment-prefix}-ebis-web"
}
