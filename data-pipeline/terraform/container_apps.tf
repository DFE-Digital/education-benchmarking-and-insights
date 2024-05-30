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

# resource "azurerm_user_assigned_identity" "container-app" {
#   location            = azurerm_resource_group.resource-group.location
#   name                = "${var.environment-prefix}containerappmi"
#   resource_group_name = azurerm_resource_group.resource-group.name
# }
#
# resource "azurerm_role_assignment" "container-app" {
#   scope                = data.azurerm_container_registry.acr.id
#   role_definition_name = "acrpull"
#   principal_id         = azurerm_user_assigned_identity.container-app.principal_id
#   depends_on = [
#     azurerm_user_assigned_identity.container-app
#   ]
# }

resource "azurerm_container_app" "data-pipeline" {
  name                         = "${var.environment-prefix}-ebis-data-pipeline"
  container_app_environment_id = azurerm_container_app_environment.main.id
  resource_group_name          = azurerm_resource_group.resource-group.name
  revision_mode                = "Single"
  workload_profile_name        = "Pipeline"

  identity {
    type = "SystemAssigned"
    #     identity_ids = [azurerm_user_assigned_identity.container-app.id]
  }

  secret {
    name  = "queue-connection-string"
    value = data.azurerm_storage_account.main.primary_connection_string
  }

  secret {
    name  = "db-password"
    value = data.azurerm_key_vault_secret.core-db-password.value
  }

  secret {
    name  = "registry-password"
    value = data.azurerm_container_registry.acr.admin_password
  }

  registry {
    server               = data.azurerm_container_registry.acr.login_server
    username             = data.azurerm_container_registry.acr.admin_username
    password_secret_name = "registry-password"
    #     identity = azurerm_user_assigned_identity.container-app.id
  }

  template {
    min_replicas    = 0
    max_replicas    = 5
    revision_suffix = replace(split(":", var.image-name)[1], ".", "-")
    container {
      name   = "edis-data-pipeline"
      image  = "${data.azurerm_container_registry.acr.login_server}/${var.image-name}"
      cpu    = 4
      memory = "16Gi"

      env {
        name  = "WORKER_QUEUE_NAME"
        value = "data-pipeline-job-start"
      }

      env {
        name  = "COMPLETE_QUEUE_NAME"
        value = "data-pipeline-job-finished"
      }

      env {
        name  = "RAW_DATA_CONTAINER"
        value = "raw"
      }

      env {
        name        = "STORAGE_CONNECTION_STRING"
        secret_name = "queue-connection-string"
      }

      env {
        name        = "DB_PWD"
        secret_name = "db-password"
      }

      env {
        name  = "DB_HOST"
        value = data.azurerm_key_vault_secret.core-db-domain-name.value
      }

      env {
        name  = "DB_NAME"
        value = data.azurerm_key_vault_secret.core-db-name.value
      }

      env {
        name  = "DB_PORT"
        value = "1433"
      }

      env {
        name  = "DB_USER"
        value = data.azurerm_key_vault_secret.core-db-user-name.value
      }

      env {
        name  = "DB_ARGS"
        value = "Encrypt=no;TrustServerCertificate=no;Connection Timeout=30"
      }
    }

    azure_queue_scale_rule {
      name         = "${var.environment-prefix}-data-pipeline-scaler"
      queue_name   = "data-pipeline-job-start"
      queue_length = 1
      authentication {
        secret_name       = "queue-connection-string"
        trigger_parameter = "connection"
      }
    }
  }

  tags = local.common-tags
}