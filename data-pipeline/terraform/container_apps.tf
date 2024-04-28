resource "azurerm_container_app_environment" "main" {
  name                       = "${var.environment-prefix}-ebis-cae"
  location                   = azurerm_resource_group.resource-group.location
  resource_group_name        = azurerm_resource_group.resource-group.name
  log_analytics_workspace_id = data.azurerm_log_analytics_workspace.application-insights-workspace.id
}

resource "azurerm_user_assigned_identity" "container-app" {
  location            = azurerm_resource_group.resource-group.location
  name                = "${var.environment-prefix}containerappmi"
  resource_group_name = azurerm_resource_group.resource-group.name
}

resource "azurerm_role_assignment" "container-app" {
  scope                = azurerm_resource_group.resource-group.id
  role_definition_name = "acrpull"
  principal_id         = azurerm_user_assigned_identity.container-app.principal_id
  depends_on = [
    azurerm_user_assigned_identity.container-app
  ]
}

resource "azurerm_container_app" "data-pipeline" {
  name                         = "${var.environment-prefix}-ebis-data-pipeline"
  container_app_environment_id = azurerm_container_app_environment.main.id
  resource_group_name          = azurerm_resource_group.resource-group.name
  revision_mode                = "Single"

  identity {
    type         = "SystemAssigned, UserAssigned"
    identity_ids = [azurerm_user_assigned_identity.container-app.id]
  }

  secret {
    name  = "queue-connection-string"
    value = azurerm_storage_account.main.primary_connection_string
  }

  registry {
    server   = data.azurerm_container_registry.acr.login_server
    identity = azurerm_user_assigned_identity.container-app.id
  }

  template {
    min_replicas = 0
    max_replicas = 100

    container {
      name   = "edis-data-pipeline"
      image  = var.image-name
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
      authentication {
        secret_name       = "queue-connection-string"
        trigger_parameter = "connection"
      }
    }
  }

  depends_on = [
    azurerm_user_assigned_identity.container-app
  ]
}