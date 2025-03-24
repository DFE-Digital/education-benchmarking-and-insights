variable "environment" {}
variable "cip-environment" {}
variable "environment-prefix" {}
variable "location" {}

variable "configuration" {
  type = map(object({
    search_sku                   = string
    search_replica_count         = number
    sql_telemetry_enabled        = bool
    cache_sku                    = string
    cache_capacity               = number
    ssr_fa_worker_process_count  = number
    ssr_fa_sku                   = string
    ssr_fa_elastic_max_workers = number # only relevant if ssr_fa_sku
    ssr_fa_elastic_min_instances = number #  is the elastic plan (EPx)
  }))
  default = {
    development = {
      search_sku                   = "basic"
      search_replica_count         = 1
      sql_telemetry_enabled        = true
      cache_sku                    = "Basic"
      cache_capacity               = 1
      ssr_fa_worker_process_count  = 1
      ssr_fa_sku                   = "Y1"
      ssr_fa_elastic_max_workers   = 1
      ssr_fa_elastic_min_instances = 0
    }
    automated-test = {
      search_sku                   = "basic"
      search_replica_count         = 1
      sql_telemetry_enabled        = false
      cache_sku                    = "Basic"
      cache_capacity               = 1
      ssr_fa_worker_process_count  = 1
      ssr_fa_sku                   = "Y1"
      ssr_fa_elastic_max_workers   = 1
      ssr_fa_elastic_min_instances = 0
    }
    test = {
      search_sku                   = "basic"
      search_replica_count         = 1
      sql_telemetry_enabled        = false
      cache_sku                    = "Basic"
      cache_capacity               = 1
      ssr_fa_worker_process_count  = 1
      ssr_fa_sku                   = "Y1"
      ssr_fa_elastic_max_workers   = 1
      ssr_fa_elastic_min_instances = 0
    }
    feature = {
      search_sku                   = "basic"
      search_replica_count         = 1
      sql_telemetry_enabled        = true
      cache_sku                    = "Basic"
      cache_capacity               = 1
      ssr_fa_worker_process_count  = 1
      ssr_fa_sku                   = "Y1"
      ssr_fa_elastic_max_workers   = 1
      ssr_fa_elastic_min_instances = 0
    }
    pre-production = {
      search_sku                   = "basic"
      search_replica_count         = 1
      sql_telemetry_enabled        = false
      cache_sku                    = "Standard"
      cache_capacity               = 1
      ssr_fa_worker_process_count  = 1
      ssr_fa_sku                   = "Y1"
      ssr_fa_elastic_max_workers   = 1
      ssr_fa_elastic_min_instances = 0
    }
    production = {
      search_sku                   = "basic"
      search_replica_count         = 3
      sql_telemetry_enabled        = false
      cache_sku                    = "Standard"
      cache_capacity               = 1
      ssr_fa_worker_process_count  = 10
      ssr_fa_sku                   = "EP1"
      ssr_fa_elastic_max_workers   = 10
      ssr_fa_elastic_min_instances = 1
    }
  }
}
