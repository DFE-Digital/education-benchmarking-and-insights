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

  resource-group-name = "${var.environment-prefix}-ebis-core"

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

  resource-group-name = "${var.environment-prefix}-ebis-core"

  registry-name = "${var.environment-prefix}acr"
  image-name    = var.image-name

  storage-account-name = "${var.environment-prefix}data"

  environment-prefix = var.environment-prefix

  key-vault-name = "${var.environment-prefix}-ebis-keyvault"

  worker-queue-name = "data-pipeline-job-custom-start"
  max-replicas      = var.environment == "development" ? 1 : 10

  common-tags = local.common-tags
}
