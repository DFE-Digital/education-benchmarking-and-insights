resource "azurerm_container_app_environment" "main" {
  name                       = "${var.environment-prefix}-ebis-keyvault"
  location                   = azurerm_resource_group.resource-group.location
  resource_group_name        = azurerm_resource_group.resource-group.name
  log_analytics_workspace_id = data.azurerm_log_analytics_workspace.application-insights-workspace.id
}

resource "azurerm_container_app" "data-pipeline" {
  name                         = "${var.environment-prefix}-ebis-data-pipeline"
  container_app_environment_id = azurerm_container_app_environment.main.id
  resource_group_name          = azurerm_resource_group.main.name
  revision_mode                = "Single"

  identity {
    type = "SystemAssigned"
  }

  secret {
    name  = "queue-connection-string"
    value = azurerm_storage_account.main.primary_connection_string
  }

  template {
    min_replicas = 0
    max_replicas = 100

    container {
      name   = "edis-data-pipeline"
      image  = "${data.azurerm_container_registry.acr.name}.azurecr.io/${var.image-name}"
      cpu    = 2
      memory = "4Gi"

      ##TODO: Review if this is the best way to build this env
      env {
        name  = "WORKER_QUEUE_NAME"
        value = azurerm_storage_queue.worker-queue.name
      }

      env {
        name  = "COMPLETE_QUEUE_NAME"
        value = azurerm_storage_queue.work-complete-queue.name
      }

      env {
        name        = "QUEUE_CONNECTION_STRING"
        secret_name = "queue-connection-string"
      }
    }

    azure_queue_scale_rule {
      name         = "${var.environment-prefix}-data-pipeline-scaler"
      queue_name   = azurerm_storage_queue.worker-queue.name
      queue_length = 1
    }
  }
}