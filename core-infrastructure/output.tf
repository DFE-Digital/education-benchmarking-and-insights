output "resource-group" {
  value = {
    name = azurerm_resource_group.resource-group.name
    location = azurerm_resource_group.resource-group.location
  }
}

output "application-insights-key" {
  value = azurerm_application_insights.application-insights.instrumentation_key
}

output "application-insights-location" {
  value = azurerm_application_insights.application-insights.location
}

output "application-insights-id" {
  value = azurerm_application_insights.application-insights.id
}

output "key-vault-id" {
  value = azurerm_key_vault.key-vault.id
}

output "common-tags" {
  value = local.common-tags
}