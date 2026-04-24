output "function_apps" {
  description = "Detailed attributes for all deployed platform Function Apps."
  value = {
    local_authority = module.local-authority-fa.function_app
    school          = module.school-fa.function_app
    trust           = module.trust-fa.function_app
    benchmark       = module.benchmark-fa.function_app
    insight         = module.insight-fa.function_app
    chart_rendering = module.chart-rendering-fa.function_app
    content         = module.content-fa.function_app
    maintenance     = module.maintenance-tasks-fa.function_app
    orchestrator    = module.orchestrator-fa.function_app
  }
}

output "storage_accounts" {
  description = "Attributes for the platform storage accounts."
  value = {
    for k, v in azurerm_storage_account.storage : k => {
      id   = v.id
      name = v.name
    }
  }
}

output "redis_cache" {
  description = "Attributes for the platform Redis cache."
  value = {
    id       = azurerm_redis_cache.cache.id
    name     = azurerm_redis_cache.cache.name
    hostname = azurerm_redis_cache.cache.hostname
  }
}
