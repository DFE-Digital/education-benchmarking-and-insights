output "search" {
  value = var.env_configs[var.environment].search
}

output "sql" {
  value = var.env_configs[var.environment].sql
}

output "cache" {
  value = var.env_configs[var.environment].cache
}

output "app_service_plan" {
  value = var.env_configs[var.environment].app_service_plan
}

output "enable_ip_restrictions" {
  value = var.env_configs[var.environment].enable_ip_restrictions
}
