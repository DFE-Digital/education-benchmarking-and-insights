data "azurerm_application_insights" "application-insights" {
  name                = "${var.environment-prefix}-ebis-ai"
  resource_group_name = "${var.environment-prefix}-ebis-core"
}

data "azurerm_log_analytics_workspace" "application-insights-workspace" {
  name                = "${var.environment-prefix}-ebis-aiw"
  resource_group_name = "${var.environment-prefix}-ebis-core"
}

data "azurerm_mssql_server" "sql-server" {
  name                = "${var.environment-prefix}-sql"
  resource_group_name = "${var.environment-prefix}-ebis-core"
}
