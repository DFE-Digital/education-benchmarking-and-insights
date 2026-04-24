output "function_app" {
  description = "Cohesive object containing the deployed Function App's attributes."
  value = {
    id           = local.func_app_id
    name         = local.func_app_name
    principal_id = local.func_app_principal_id
    hostname     = local.func_app_hostname
    uri          = local.host
    master_key   = local.key
  }
}

# Legacy outputs preserved for compatibility
output "function-host-address" {
  value = one(azurerm_key_vault_secret.fa-host[*].value)
}

output "function-host-key" {
  value = one(azurerm_key_vault_secret.fa-key[*].value)
}

output "function-resource-id" {
  value = local.func_app_id
}
