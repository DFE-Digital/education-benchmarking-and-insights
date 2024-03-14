locals {
  function-app-settings = merge(var.app-settings, {
    "FUNCTIONS_WORKER_RUNTIME" = "dotnet"
  })
  cors              = var.cors
  function-app-name = "${var.environment-prefix}-ebis-${var.function-name}-fa"
}

resource "azurerm_service_plan" "func-asp" {
  name                = "${var.environment-prefix}-ebis-${var.function-name}-function-asp"
  location            = var.location
  resource_group_name = var.resource-group-name
  os_type             = "Windows"
  sku_name            = var.sku.size
  tags                = var.common-tags
}

resource "azurerm_windows_function_app" "func-app" {
  name                       = local.function-app-name
  location                   = var.location
  resource_group_name        = var.resource-group-name
  service_plan_id            = azurerm_service_plan.func-asp.id
  storage_account_name       = var.storage-account-name
  storage_account_access_key = var.storage-account-key
  https_only                 = true

  site_config {
    always_on     = var.always-on
    http2_enabled = true
    cors {
      allowed_origins = local.cors
    }
    application_insights_connection_string = var.application-insights-connection-string
    application_stack {
      dotnet_version              = "v6.0"
      use_dotnet_isolated_runtime = false
    }
  }

  app_settings = local.function-app-settings
  tags         = var.common-tags

  lifecycle {
    ignore_changes = [
      app_settings["FUNCTIONS_EXTENSION_VERSION"],
      app_settings["WEBSITE_ENABLE_SYNC_UPDATE_SITE"],
      app_settings["WEBSITE_RUN_FROM_PACKAGE"],
    ]
  }
}

resource "azurerm_resource_group_template_deployment" "function_keys" {
  count = var.requires-keys ? 1 : 0
  name  = "${var.function-name}-key-deployment"
  parameters_content = jsonencode({
    "functionApp" = {
      value = azurerm_windows_function_app.func-app.name
    }
  })
  resource_group_name = azurerm_windows_function_app.func-app.resource_group_name
  deployment_mode     = "Incremental"

  template_content = <<BODY
  {
      "$schema": "https://schema.management.azure.com/schemas/2015-01-01/deploymentTemplate.json#",
      "contentVersion": "1.0.0.0",
      "parameters": {
          "functionApp": {
            "type": "String",
            "defaultValue": ""
          }
      },
      "variables": {
          "functionAppId": "[resourceId('Microsoft.Web/sites', parameters('functionApp'))]"
      },
      "resources": [
      ],
      "outputs": {
          "functionkey": {
              "type": "String",
              "value": "[listkeys(concat(variables('functionAppId'), '/host/default'), '2018-11-01').masterKey]"                                                                                
            }
       }
  }
  BODY
}


locals {
  key  = var.requires-keys ? jsondecode(azurerm_resource_group_template_deployment.function_keys[0].output_content).functionkey.value : null
  host = "https://${azurerm_windows_function_app.func-app.default_hostname}"
}

resource "azurerm_key_vault_secret" "fa-key" {
  #checkov:skip=CKV_AZURE_41:Secrets expiration to be reviewed
  count        = var.requires-keys ? 1 : 0
  name         = "${var.function-name}-host-key"
  value        = local.key
  key_vault_id = var.key-vault-id
  content_type = "key"
}

resource "azurerm_key_vault_secret" "fa-host" {
  #checkov:skip=CKV_AZURE_41:Secrets expiration to be reviewed
  count        = var.requires-keys ? 1 : 0
  name         = "${var.function-name}-host"
  value        = local.host
  key_vault_id = var.key-vault-id
  content_type = "host"
}
