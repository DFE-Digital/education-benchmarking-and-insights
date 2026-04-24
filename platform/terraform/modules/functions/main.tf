resource "azurerm_key_vault_access_policy" "keyvault_policy" {
  key_vault_id       = var.key_vault.id
  tenant_id          = local.func_app_tenant_id
  object_id          = local.kv_policy_object_id
  secret_permissions = ["Get"]
}

resource "azurerm_service_plan" "func-asp" {
  #checkov:skip=CKV_AZURE_212:See ADO backlog AB#206517
  #checkov:skip=CKV_AZURE_225:See ADO backlog AB#206517
  name                         = "${var.core.environment_prefix}-ebis-${var.core.name}-function-asp"
  location                     = var.core.location
  resource_group_name          = var.core.resource_group_name
  os_type                      = var.service_plan.os_type
  sku_name                     = var.service_plan.size
  tags                         = var.core.tags
  maximum_elastic_worker_count = var.service_plan.maximum_elastic_worker_count
}

resource "azurerm_windows_function_app" "func-app" {
  #checkov:skip=CKV_AZURE_221:See ADO backlog AB#206517
  count                      = local.windows_count
  name                       = local.function-app-name
  location                   = var.core.location
  resource_group_name        = var.core.resource_group_name
  service_plan_id            = azurerm_service_plan.func-asp.id
  storage_account_name       = var.storage_account.name
  storage_account_access_key = var.storage_account.key
  https_only                 = true
  builtin_logging_enabled    = false

  identity {
    type = "SystemAssigned"
  }

  site_config {
    always_on                              = var.application_stack.always_on
    http2_enabled                          = true
    application_insights_connection_string = var.monitoring.instrumentation_connection_string
    use_32_bit_worker                      = var.application_stack.use_32_bit_worker
    elastic_instance_minimum               = var.service_plan.minimum_elastic_instance_count

    application_stack {
      dotnet_version              = var.application_stack.dotnet_version
      use_dotnet_isolated_runtime = var.application_stack.use_isolated_runtime
    }

    ip_restriction_default_action = local.ip_default_action

    dynamic "ip_restriction" {
      for_each = local.ip_restrictions
      content {
        virtual_network_subnet_id = ip_restriction.value
      }
    }
  }

  app_settings = local.function-app-settings
  tags         = var.core.tags

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
  count                      = local.linux_count
  name                       = local.function-app-name
  location                   = var.core.location
  resource_group_name        = var.core.resource_group_name
  service_plan_id            = azurerm_service_plan.func-asp.id
  storage_account_name       = var.storage_account.name
  storage_account_access_key = var.storage_account.key
  https_only                 = true
  builtin_logging_enabled    = false

  identity {
    type = "SystemAssigned"
  }

  site_config {
    always_on                              = var.application_stack.always_on
    http2_enabled                          = true
    application_insights_connection_string = var.monitoring.instrumentation_connection_string
    use_32_bit_worker                      = var.application_stack.use_32_bit_worker
    elastic_instance_minimum               = var.service_plan.minimum_elastic_instance_count

    application_stack {
      node_version = var.application_stack.node_version
    }

    ip_restriction_default_action = local.ip_default_action

    dynamic "ip_restriction" {
      for_each = local.ip_restrictions
      content {
        virtual_network_subnet_id = ip_restriction.value
      }
    }
  }

  app_settings = local.function-app-settings
  tags         = var.core.tags

  lifecycle {
    ignore_changes = [
      app_settings["FUNCTIONS_EXTENSION_VERSION"],
      app_settings["WEBSITE_ENABLE_SYNC_UPDATE_SITE"],
      app_settings["WEBSITE_RUN_FROM_PACKAGE"],
    ]
  }
}

resource "azurerm_resource_group_template_deployment" "function_keys" {
  count = local.requires_keys_count
  name  = "${var.core.name}-key-deployment"
  parameters_content = jsonencode({
    "functionApp" = {
      value = local.func_app_name
    }
  })
  resource_group_name = var.core.resource_group_name
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

resource "azurerm_key_vault_secret" "fa-key" {
  #checkov:skip=CKV_AZURE_41:See ADO backlog AB#206511
  count        = local.requires_keys_count
  name         = "${var.core.name}-host-key"
  value        = local.key
  key_vault_id = var.key_vault.id
  content_type = "key"
}

resource "azurerm_key_vault_secret" "fa-host" {
  #checkov:skip=CKV_AZURE_41:See ADO backlog AB#206511
  count        = local.requires_keys_count
  name         = "${var.core.name}-host"
  value        = local.host
  key_vault_id = var.key_vault.id
  content_type = "host"
}

# ClientId rather than PrincipalId required for managed identity user in SQL database:
# https://github.com/betr-io/terraform-provider-mssql/issues/54#issuecomment-1632638595
data "azapi_resource" "app-service-identity" {
  name                   = "default"
  parent_id              = local.func_app_id
  type                   = "Microsoft.ManagedIdentity/identities@2018-11-30"
  response_export_values = ["properties.clientId"]
}

resource "mssql_user" "app-service-user" {
  server {
    host = var.sql_server.fqdn
    login {
      username = var.sql_server.username
      password = var.sql_server.password
    }
  }

  database  = "data"
  username  = local.func_app_name
  object_id = data.azapi_resource.app-service-identity.output.properties.clientId
  roles     = ["db_datareader", "db_datawriter"]
}

resource "azurerm_redis_cache_access_policy_assignment" "contributor" {
  count              = local.redis_contributor_count
  name               = "${var.core.name}-contributor"
  redis_cache_id     = var.redis_cache.id
  access_policy_name = "Data Contributor"
  object_id          = local.func_app_principal_id
  object_id_alias    = "${var.core.name}-contributor"
}

resource "azurerm_redis_cache_access_policy_assignment" "owner" {
  count              = local.redis_owner_count
  name               = "${var.core.name}-owner"
  redis_cache_id     = var.redis_cache.id
  access_policy_name = "Data Owner"
  object_id          = local.func_app_principal_id
  object_id_alias    = "${var.core.name}-owner"
}

resource "azurerm_monitor_diagnostic_setting" "func-app" {
  name                       = "${local.func_app_name}-logs"
  target_resource_id         = local.func_app_id
  log_analytics_workspace_id = var.monitoring.log_analytics_id

  enabled_metric {
    category = "AllMetrics"
  }

  enabled_log {
    category = "FunctionAppLogs"
  }
}

resource "azurerm_monitor_diagnostic_setting" "func-app-service" {
  name                       = "${local.func_app_name}-asp-logs"
  target_resource_id         = azurerm_service_plan.func-asp.id
  log_analytics_workspace_id = var.monitoring.log_analytics_id

  enabled_metric {
    category = "AllMetrics"
  }
}
