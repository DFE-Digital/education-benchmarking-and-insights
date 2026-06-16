output "outbound_ip_addresses" {
  value       = azurerm_container_app.data-pipeline.outbound_ip_addresses
  description = "The outbound IP addresses of the container app."
}
