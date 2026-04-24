output "search" {
  value = var.env_configs[var.environment].search
}

output "sql" {
  value = var.env_configs[var.environment].sql
}

output "cache" {
  value = var.env_configs[var.environment].cache
}

output "enable_ip_restrictions" {
  value = var.env_configs[var.environment].enable_ip_restrictions
}
