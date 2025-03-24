locals {
  function-app-settings = merge(var.app-settings, {
    "FUNCTIONS_WORKER_RUNTIME"    = var.worker-runtime
    "AzureWebJobsDisableHomepage" = true
  })
  function-app-name = "${var.environment-prefix}-ebis-${var.function-name}-fa"
}

resource "azurerm_key_vault_access_policy" "keyvault_policy" {
  key_vault_id = var.key-vault-id
  tenant_id = (var.os-type == "Windows" ? azurerm_windows_function_app.func-app[0].identity[0].tenant_id :
  azurerm_linux_function_app.func-app[0].identity[0].tenant_id)
  object_id = (var.os-type == "Windows" ? azurerm_windows_function_app.func-app[0].identity[0].principal_id :
  azurerm_linux_function_app.func-app[0].identity[0].tenant_id)
  secret_permissions = ["Get"]
}

/*resource "azurerm_role_assignment" "func-storage-role-blob" {
  scope                = var.storage-account-id
  role_definition_name = "Storage Blob Data Contributor"
  principal_id         = azurerm_windows_function_app.func-app.identity[0].principal_id
  principal_type       = "ServicePrincipal"
}

resource "azurerm_role_assignment" "func-storage-role-file" {
  scope                = var.storage-account-id
  role_definition_name = "Storage File Data SMB Share Contributor"
  principal_id         = azurerm_windows_function_app.func-app.identity[0].principal_id
  principal_type       = "ServicePrincipal"
}*/

resource "azurerm_service_plan" "func-asp" {
  #checkov:skip=CKV_AZURE_212:See ADO backlog AB#206517
  #checkov:skip=CKV_AZURE_225:See ADO backlog AB#206517
  name                         = "${var.environment-prefix}-ebis-${var.function-name}-function-asp"
  location                     = var.location
  resource_group_name          = var.resource-group-name
  os_type                      = var.os-type
  sku_name                     = var.sku.size
  tags                         = var.common-tags
  maximum_elastic_worker_count = substr(var.sku.size, 0, 2) == "EP" ? var.maximum-elastic-worker-count : null
}

resource "azurerm_windows_function_app" "func-app" {
  #checkov:skip=CKV_AZURE_221:See ADO backlog AB#206517
  count                      = var.os-type == "Windows" ? 1 : 0
  name                       = local.function-app-name
  location                   = var.location
  resource_group_name        = var.resource-group-name
  service_plan_id            = azurerm_service_plan.func-asp.id
  storage_account_name       = var.storage-account-name
  storage_account_access_key = var.storage-account-key
  https_only                 = true
  builtin_logging_enabled    = false

  identity {
    type = "SystemAssigned"
  }

  site_config {
    always_on                              = var.always-on
    http2_enabled                          = true
    application_insights_connection_string = var.instrumentation-conn-string
    use_32_bit_worker                      = var.use-32-bit-worker
    elastic_instance_minimum = (substr(var.sku.size, 0, 2) == "EP" ? var.minimum-elastic-instance-count :
    null)

    application_stack {
      dotnet_version              = var.dotnet-version
      use_dotnet_isolated_runtime = var.use-isolated-runtime
    }

    ip_restriction_default_action = var.enable-restrictions ? "Deny" : "Allow"

    #To be removed post ITHC
    ip_restriction {
      ip_address = "51.142.180.30/32"
      name       = "ITHC - 1"
    }

    #To be removed post ITHC
    ip_restriction {
      ip_address = "185.120.72.241/30"
      name       = "ITHC - 2"
    }

    dynamic "ip_restriction" {
      for_each = var.enable-restrictions ? var.subnet_ids : []
      content {
        virtual_network_subnet_id = ip_restriction.value
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

resource "azurerm_linux_function_app" "func-app" {
  #checkov:skip=CKV_AZURE_221:See ADO backlog AB#206517
  count                      = var.os-type == "Windows" ? 0 : 1
  name                       = local.function-app-name
  location                   = var.location
  resource_group_name        = var.resource-group-name
  service_plan_id            = azurerm_service_plan.func-asp.id
  storage_account_name       = var.storage-account-name
  storage_account_access_key = var.storage-account-key
  https_only                 = true
  builtin_logging_enabled    = false

  identity {
    type = "SystemAssigned"
  }

  site_config {
    always_on                              = var.always-on
    http2_enabled                          = true
    application_insights_connection_string = var.instrumentation-conn-string
    use_32_bit_worker                      = var.use-32-bit-worker
    elastic_instance_minimum = (substr(var.sku.size, 0, 2) == "EP" ? var.minimum-elastic-instance-count :
    null)

    application_stack {
      node_version = var.node-version
    }

    ip_restriction_default_action = var.enable-restrictions ? "Deny" : "Allow"

    dynamic "ip_restriction" {
      for_each = var.enable-restrictions ? var.subnet_ids : []
      content {
        virtual_network_subnet_id = ip_restriction.value
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
      value = (var.os-type == "Windows" ? azurerm_windows_function_app.func-app[0].name :
      azurerm_linux_function_app.func-app[0].name)
    }
  })
  resource_group_name = var.resource-group-name
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
  key = (var.requires-keys
    ? jsondecode(azurerm_resource_group_template_deployment.function_keys[0].output_content).functionkey.value
  : null)
  host = "https://${var.os-type == "Windows" ? azurerm_windows_function_app.func-app[0].default_hostname : azurerm_linux_function_app.func-app[0].default_hostname}"
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

# ClientId rather than PrincipalId required for managed identity user in SQL database:
# https://github.com/betr-io/terraform-provider-mssql/issues/54#issuecomment-1632638595
data "azapi_resource" "app-service-identity" {
  name = "default"
  parent_id = (var.os-type == "Windows" ? azurerm_windows_function_app.func-app[0].id :
  azurerm_linux_function_app.func-app[0].id)
  type                   = "Microsoft.ManagedIdentity/identities@2018-11-30"
  response_export_values = ["properties.clientId"]
}

resource "mssql_user" "app-service-user" {
  server {
    host = var.sql-server-fqdn
    login {
      username = var.sql-server-username
      password = var.sql-server-password
    }
  }

  database = "data"
  username = (var.os-type == "Windows" ? azurerm_windows_function_app.func-app[0].name :
  azurerm_linux_function_app.func-app[0].name)
  object_id = data.azapi_resource.app-service-identity.output.properties.clientId
  roles     = ["db_datareader", "db_datawriter"]
}

resource "azurerm_redis_cache_access_policy_assignment" "contributor" {
  count              = var.cache-contributor ? 1 : 0
  name               = "${var.function-name}-contributor"
  redis_cache_id     = var.redis-cache-id
  access_policy_name = "Data Contributor"
  object_id = (var.os-type == "Windows" ? azurerm_windows_function_app.func-app[0].identity[0].principal_id :
  azurerm_linux_function_app.func-app[0].identity[0].principal_id)
  object_id_alias = "${var.function-name}-contributor"
}

resource "azurerm_redis_cache_access_policy_assignment" "owner" {
  count              = var.cache-owner ? 1 : 0
  name               = "${var.function-name}-owner"
  redis_cache_id     = var.redis-cache-id
  access_policy_name = "Data Owner"
  object_id = (var.os-type == "Windows" ? azurerm_windows_function_app.func-app[0].identity[0].principal_id :
  azurerm_linux_function_app.func-app[0].identity[0].principal_id)
  object_id_alias = "${var.function-name}-owner"
}

resource "azurerm_monitor_diagnostic_setting" "func-app" {
  name = "${(var.os-type == "Windows" ? azurerm_windows_function_app.func-app[0].name : azurerm_linux_function_app.func-app[0].name)}-logs"
  target_resource_id = (var.os-type == "Windows" ? azurerm_windows_function_app.func-app[0].id :
  azurerm_linux_function_app.func-app[0].id)
  log_analytics_workspace_id = var.log-analytics-id

  metric {
    category = "AllMetrics"
    enabled  = true

    // The following is not used by Log Analytics backed diagnostics, but Terraform adds it anyway and `ignore_changes` 
    // is not currently supported by block level configuration (https://github.com/hashicorp/terraform/issues/26359). 
    // The 'deprecated' warning here and below may therefore be ignored.
    retention_policy {
      days    = 0
      enabled = false
    }
  }

  enabled_log {
    category = "FunctionAppLogs"
  }
}

resource "azurerm_monitor_diagnostic_setting" "func-app-service" {
  name                       = "${(var.os-type == "Windows" ? azurerm_windows_function_app.func-app[0].name : azurerm_linux_function_app.func-app[0].name)}-asp-logs"
  target_resource_id         = azurerm_service_plan.func-asp.id
  log_analytics_workspace_id = var.log-analytics-id

  metric {
    category = "AllMetrics"
    enabled  = true
  }
}