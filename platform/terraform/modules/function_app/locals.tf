locals {
  function-app-settings = merge(var.app-settings, {
    "FUNCTIONS_WORKER_RUNTIME"    = var.application_stack.worker_runtime
    "AzureWebJobsDisableHomepage" = true
  })
  function-app-name = "${var.core.environment_prefix}-ebis-${var.core.name}-fa"

  func_app_id           = one(concat(azurerm_windows_function_app.func-app[*].id, azurerm_linux_function_app.func-app[*].id))
  func_app_name         = one(concat(azurerm_windows_function_app.func-app[*].name, azurerm_linux_function_app.func-app[*].name))
  func_app_principal_id = one(concat(azurerm_windows_function_app.func-app[*].identity[0].principal_id, azurerm_linux_function_app.func-app[*].identity[0].principal_id))
  func_app_tenant_id    = one(concat(azurerm_windows_function_app.func-app[*].identity[0].tenant_id, azurerm_linux_function_app.func-app[*].identity[0].tenant_id))
  func_app_hostname     = one(concat(azurerm_windows_function_app.func-app[*].default_hostname, azurerm_linux_function_app.func-app[*].default_hostname))

  key_json = one(azurerm_resource_group_template_deployment.function_keys[*].output_content)
  key      = try(jsondecode(local.key_json).functionkey.value, null)
  host     = "https://${local.func_app_hostname}"

  # Preserved intentional logic: Linux uses tenant_id for the KV object_id
  kv_policy_object_id = var.service_plan.os_type == "Windows" ? local.func_app_principal_id : local.func_app_tenant_id

  # Simplify IP restrictions
  ip_default_action = var.networking.enable_restrictions ? "Deny" : "Allow"
  ip_restrictions   = var.networking.enable_restrictions ? var.networking.subnet_ids : []

  # Counts for conditional resources
  windows_count           = var.service_plan.os_type == "Windows" ? 1 : 0
  linux_count             = var.service_plan.os_type == "Windows" ? 0 : 1
  requires_keys_count     = var.key_vault.requires_keys ? 1 : 0
  redis_contributor_count = var.redis_cache.contributor ? 1 : 0
  redis_owner_count       = var.redis_cache.owner ? 1 : 0
}
