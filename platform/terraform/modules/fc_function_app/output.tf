# Legacy outputs preserved for compatibility
output "function-host-address" {
  value = one(azurerm_key_vault_secret.fa-host[*].value)
}

output "function-host-key" {
  value = one(azurerm_key_vault_secret.fa-key[*].value)
}

output "function-resource-id" {
  value = azurerm_function_app_flex_consumption.func-app.id
}
