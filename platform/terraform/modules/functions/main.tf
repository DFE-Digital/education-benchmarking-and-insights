locals {
  function-app-settings = merge(var.app-settings, {
    "FUNCTIONS_WORKER_RUNTIME"    = "dotnet"
    "AzureWebJobsDisableHomepage" = true
  })
  function-app-name = "${var.environment-prefix}-ebis-${var.function-name}-fa"
}

resource "azurerm_key_vault_access_policy" "keyvault_policy" {
  key_vault_id       = var.key-vault-id
  tenant_id          = azurerm_windows_function_app.func-app.identity[0].tenant_id
  object_id          = azurerm_windows_function_app.func-app.identity[0].principal_id
  secret_permissions = ["Get"]
}

resource "azurerm_role_assignment" "func-storage-role" {
  scope                = var.storage-account-id
  role_definition_name = "Storage Blob Data Contributor"
  principal_id         = azurerm_windows_function_app.func-app.identity[0].principal_id
}

resource "azurerm_service_plan" "func-asp" {
  #checkov:skip=CKV_AZURE_212:See ADO backlog AB#206517
  #checkov:skip=CKV_AZURE_225:See ADO backlog AB#206517
  name                = "${var.environment-prefix}-ebis-${var.function-name}-function-asp"
  location            = var.location
  resource_group_name = var.resource-group-name
  os_type             = "Windows"
  sku_name            = var.sku.size
  tags                = var.common-tags
}

resource "azurerm_windows_function_app" "func-app" {
  #checkov:skip=CKV_AZURE_221:See ADO backlog AB#206517
  name                       = local.function-app-name
  location                   = var.location
  resource_group_name        = var.resource-group-name
  service_plan_id            = azurerm_service_plan.func-asp.id
  storage_account_name       = var.storage-account-name
  storage_account_access_key = var.storage-account-key
  https_only                 = true

  identity {
    type = "SystemAssigned"
  }

  site_config {
    always_on     = var.always-on
    http2_enabled = true

    application_insights_connection_string = var.application-insights-connection-string
    application_stack {
      dotnet_version              = "v6.0"
      use_dotnet_isolated_runtime = false
    }

    dynamic "ip_restriction" {
      for_each = var.enable-restrictions ? ["apply"] : []
      content {
        virtual_network_subnet_id = var.subnet_id
      }
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
  #checkov:skip=CKV_AZURE_41:See ADO backlog AB#206511
  count        = var.requires-keys ? 1 : 0
  name         = "${var.function-name}-host-key"
  value        = local.key
  key_vault_id = var.key-vault-id
  content_type = "key"
}

resource "azurerm_key_vault_secret" "fa-host" {
  #checkov:skip=CKV_AZURE_41:See ADO backlog AB#206511
  count        = var.requires-keys ? 1 : 0
  name         = "${var.function-name}-host"
  value        = local.host
  key_vault_id = var.key-vault-id
  content_type = "host"
}
