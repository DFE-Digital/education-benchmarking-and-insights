resource "azurerm_container_app_environment" "main" {
  name                       = "${var.environment-prefix}-ebis-cae"
  location                   = azurerm_resource_group.resource-group.location
  resource_group_name        = azurerm_resource_group.resource-group.name
  log_analytics_workspace_id = data.azurerm_log_analytics_workspace.application-insights-workspace.id

  workload_profile {
    name                  = "Pipeline"
    workload_profile_type = "D4"
    minimum_count         = 0
    maximum_count         = 10
  }

  tags = local.common-tags
}

module "container_app_default" {
  source = "./container_app"

  container-app-name-suffix         = "default"
  container-app-environment-id      = azurerm_container_app_environment.main.id
  container-app-resource-group-name = azurerm_resource_group.resource-group.name

  core-resource-group-name = "${var.environment-prefix}-ebis-core"

  registry-name = "${var.environment-prefix}acr"
  image-name    = var.image-name

  storage-account-name = "${var.environment-prefix}data"

  environment-prefix = var.environment-prefix

  key-vault-name = "${var.environment-prefix}-ebis-keyvault"

  worker-queue-name = "data-pipeline-job-default-start"
  max-replicas      = 1

  common-tags = local.common-tags
}

module "container_app_custom" {
  source = "./container_app"

  container-app-name-suffix         = "custom"
  container-app-environment-id      = azurerm_container_app_environment.main.id
  container-app-resource-group-name = azurerm_resource_group.resource-group.name

  core-resource-group-name = "${var.environment-prefix}-ebis-core"

  registry-name = "${var.environment-prefix}acr"
  image-name    = var.image-name

  storage-account-name = "${var.environment-prefix}data"

  environment-prefix = var.environment-prefix

  key-vault-name = "${var.environment-prefix}-ebis-keyvault"

  worker-queue-name = "data-pipeline-job-custom-start"
  max-replicas      = var.environment == "development" ? 1 : 10

  common-tags = local.common-tags
}

resource "azurerm_mssql_firewall_rule" "cae-fw-rule" {
  name             = "${var.environment-prefix}-ebis-cae-fw"
  server_id        = data.azurerm_mssql_server.sql-server.id
  start_ip_address = azurerm_container_app_environment.main.static_ip_address
  end_ip_address   = azurerm_container_app_environment.main.static_ip_address
}

locals {
  container_app_outbound_ips = toset(concat(
    module.container_app_default.outbound_ip_addresses,
    module.container_app_custom.outbound_ip_addresses
  ))
}

resource "azurerm_mssql_firewall_rule" "ca-fw-rule" {
  for_each         = local.container_app_outbound_ips
  name             = "${var.environment-prefix}-ebis-ca-fw-${replace(each.value, ".", "-")}"
  server_id        = data.azurerm_mssql_server.sql-server.id
  start_ip_address = each.value
  end_ip_address   = each.value
}
