variable "environment" {}
variable "cip-environment" {}
variable "environment-prefix" {}
variable "location" {}

variable "configuration" {
  type = map(object({
    search_sku            = string
    search_replica_count  = number
    sql_telemetry_enabled = bool
    cache_sku             = string
    cache_capacity        = number
  }))
  default = {
    development = {
      search_sku            = "basic"
      search_replica_count  = 1
      sql_telemetry_enabled = true
      cache_sku             = "Basic"
      cache_capacity        = 1
    }
    automated-test = {
      search_sku            = "basic"
      search_replica_count  = 1
      sql_telemetry_enabled = false
      cache_sku             = "Basic"
      cache_capacity        = 1
    }
    test = {
      search_sku            = "basic"
      search_replica_count  = 1
      sql_telemetry_enabled = false
      cache_sku             = "Basic"
      cache_capacity        = 1
    }
    feature = {
      search_sku            = "basic"
      search_replica_count  = 1
      sql_telemetry_enabled = true
      cache_sku             = "Basic"
      cache_capacity        = 1
    }
    pre-production = {
      search_sku            = "basic"
      search_replica_count  = 1
      sql_telemetry_enabled = false
      cache_sku             = "Basic"
      cache_capacity        = 1
    }
    production = {
      search_sku            = "basic"
      search_replica_count  = 3
      sql_telemetry_enabled = false
      cache_sku             = "Basic"
      cache_capacity        = 1
    }
  }
}
