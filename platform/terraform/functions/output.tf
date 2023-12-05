output "function-host-address" {
  value = var.requires-keys ? local.host : null
}

output "function-host-key" {
  value = var.requires-keys ? local.key : null
}