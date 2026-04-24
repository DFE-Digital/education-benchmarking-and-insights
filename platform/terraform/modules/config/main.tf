variable "env_configs" {
  type = map(object({
    search = object({
      sku           = string
      replica_count = number
    })
    sql = object({
      telemetry_enabled = bool
    })
    cache = object({
      sku      = string
      capacity = number
      family   = string
    })
    app_service_plan = object({
      sku                   = string
      worker_process_count  = number
      elastic_max_workers   = number
      elastic_min_instances = number
    })
    enable_ip_restrictions = bool
  }))
  description = "Internal configuration map. Defined as a variable to provide a strict type schema for IDEs."
  default = {
    development = {
      search                 = { sku = "basic", replica_count = 1 }
      sql                    = { telemetry_enabled = true }
      cache                  = { sku = "Basic", capacity = 1, family = "C" }
      app_service_plan       = { sku = "Y1", worker_process_count = 1, elastic_max_workers = 1, elastic_min_instances = 0 }
      enable_ip_restrictions = false
    }
    automated-test = {
      search                 = { sku = "basic", replica_count = 1 }
      sql                    = { telemetry_enabled = false }
      cache                  = { sku = "Basic", capacity = 1, family = "C" }
      app_service_plan       = { sku = "Y1", worker_process_count = 1, elastic_max_workers = 1, elastic_min_instances = 0 }
      enable_ip_restrictions = true
    }
    test = {
      search                 = { sku = "basic", replica_count = 1 }
      sql                    = { telemetry_enabled = false }
      cache                  = { sku = "Standard", capacity = 1, family = "C" }
      app_service_plan       = { sku = "Y1", worker_process_count = 1, elastic_max_workers = 1, elastic_min_instances = 0 }
      enable_ip_restrictions = true
    }
    feature = {
      search                 = { sku = "basic", replica_count = 1 }
      sql                    = { telemetry_enabled = true }
      cache                  = { sku = "Basic", capacity = 1, family = "C" }
      app_service_plan       = { sku = "Y1", worker_process_count = 1, elastic_max_workers = 1, elastic_min_instances = 0 }
      enable_ip_restrictions = true
    }
    pre-production = {
      search                 = { sku = "basic", replica_count = 1 }
      sql                    = { telemetry_enabled = false }
      cache                  = { sku = "Standard", capacity = 1, family = "C" }
      app_service_plan       = { sku = "Y1", worker_process_count = 1, elastic_max_workers = 1, elastic_min_instances = 0 }
      enable_ip_restrictions = true
    }
    production = {
      search                 = { sku = "basic", replica_count = 3 }
      sql                    = { telemetry_enabled = false }
      cache                  = { sku = "Standard", capacity = 1, family = "C" }
      app_service_plan       = { sku = "Y1", worker_process_count = 1, elastic_max_workers = 1, elastic_min_instances = 0 }
      enable_ip_restrictions = true
    }
  }
}
