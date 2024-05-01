output "function-host-address" {
  value = var.requires-keys ? local.host : null
}

output "function-host-key" {
  value = var.requires-keys ? local.key : null
}

output "function-resource-id" {
  value = azurerm_windows_function_app.func-app.id
}