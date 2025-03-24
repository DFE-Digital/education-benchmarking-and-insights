output "function-host-address" {
  value = var.requires-keys ? local.host : null
}

output "function-host-key" {
  value = var.requires-keys ? local.key : null
}

output "function-resource-id" {
  value = (var.os-type == "Windows" ? azurerm_windows_function_app.func-app[0].id :
  azurerm_linux_function_app.func-app[0].id)
}