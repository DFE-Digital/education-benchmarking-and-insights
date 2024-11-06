resource "azurerm_container_app" "data-pipeline" {
  name                         = "${var.environment-prefix}-ebis-data-pipeline"
  container_app_environment_id = var.container-app-environment-id
  resource_group_name          = var.resource-group-name
  revision_mode                = "Single"
  workload_profile_name        = "Pipeline"

  identity {
    type = "SystemAssigned"
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
  }

  template {
    min_replicas    = 0
    max_replicas    = var.max-replicas
    revision_suffix = replace(split(":", var.image-name)[1], ".", "-")
    container {
      name   = "edis-data-pipeline"
      image  = var.image-name
      cpu    = 4
      memory = "16Gi"

      env {
        name  = "WORKER_QUEUE_NAME"
        value = var.worker-queue-name
      }

      env {
        name  = "COMPLETE_QUEUE_NAME"
        value = "data-pipeline-job-finished"
      }

      env {
        name  = "DEAD_LETTER_QUEUE_NAME"
        value = "data-pipeline-job-dlq"
      }

      env {
        name  = "DEAD_LETTER_QUEUE_DEQUEUE_MAX"
        value = "5"
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
      queue_name   = var.worker-queue-name
      queue_length = 1
      authentication {
        secret_name       = "queue-connection-string"
        trigger_parameter = "connection"
      }
    }
  }

  tags = var.common-tags
}
