locals {
  function-app-settings = merge(var.app-settings, {
    "FUNCTIONS_WORKER_RUNTIME" = "dotnet",
    "WEBSITE_NODE_DEFAULT_VERSION" = "~10",
    "APPINSIGHTS_INSTRUMENTATIONKEY" = var.application-insights-key
  })
  cors = var.cors
  function-app-name = "${var.environment-prefix}-${var.function-name}-fa"
}

resource "azurerm_service_plan" "func-asp" {
  name = "${var.environment-prefix}-${var.function-name}-function-asp"
  location = var.location
  resource_group_name = var.resource-group-name

  os_type = "Linux"
  sku_name = var.sku.size

  tags = var.common-tags
}

resource "azurerm_linux_function_app" "func-app" {
  name = local.function-app-name
  version = "~4"
  location = var.location
  resource_group_name = var.resource-group-name
  service_plan_id = azurerm_service_plan.func-asp.id
  storage_account_name = var.storage-account-name
  storage_account_access_key = var.storage-account-key
  https_only = true
  
  site_config {
    always_on = var.always-on
    http2_enabled = true
    cors {
      allowed_origins = local.cors
    }
  }
  
  app_settings = local.function-app-settings
  tags = var.common-tags
  
  lifecycle {
    ignore_changes = [
      app_settings["FUNCTIONS_EXTENSION_VERSION"],
      app_settings["WEBSITE_ENABLE_SYNC_UPDATE_SITE"],
      app_settings["WEBSITE_RUN_FROM_PACKAGE"]
    ]
  }
}

resource "azurerm_template_deployment" "function_keys" {
  count = var.requires-keys ? 1 : 0
  name = "${var.function-name}-host-key"
  parameters = {
    "functionApp" = azurerm_linux_function_app.func-app.name
  }
  resource_group_name = azurerm_linux_function_app.func-app.resource_group_name
  deployment_mode     = "Incremental"
  
  template_body = <<BODY
  {
      "$schema": "https://schema.management.azure.com/schemas/2015-01-01/deploymentTemplate.json#",
      "contentVersion": "1.0.0.0",
      "parameters": {
          "functionApp": {"type": "string", "defaultValue": ""}
      },
      "variables": {
          "functionAppId": "[resourceId('Microsoft.Web/sites', parameters('functionApp'))]"
      },
      "resources": [
      ],
      "outputs": {
          "functionkey": {
              "type": "string",
              "value": "[listkeys(concat(variables('functionAppId'), '/host/default'), '2018-11-01').masterKey]"                                                                                
            }
       }
  }
  BODY
}

locals {
  key = var.requires-keys ? lookup(azurerm_template_deployment.function_keys[0].outputs, "functionkey", "") : null
  host = "https://${azurerm_linux_function_app.func-app.default_hostname}" 
}

resource "azurerm_key_vault_secret" "fa-key" {
  count = var.requires-keys ? 1 : 0
  name         = "${var.function-name}-host-key"
  value        =  local.key
  key_vault_id = var.key-vault-id
}

resource "azurerm_key_vault_secret" "fa-host" {
  count = var.requires-keys ? 1 : 0
  name         = "${var.function-name}-host"
  value        = local.host 
  key_vault_id = var.key-vault-id
}


