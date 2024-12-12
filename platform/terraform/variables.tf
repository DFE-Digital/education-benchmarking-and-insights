variable "environment" {}
variable "cip-environment" {}
variable "environment-prefix" {}
variable "location" {}

variable "configuration" {
  type = map(object({
    search_sku            = string
    search_replica_count  = number
    sql_telemetry_enabled = bool
  }))
  default = {
    development = {
      search_sku            = "basic"
      search_replica_count  = 1
      sql_telemetry_enabled = true
    }
    automated-test = {
      search_sku            = "basic"
      search_replica_count  = 1
      sql_telemetry_enabled = false
    }
    test = {
      search_sku            = "basic"
      search_replica_count  = 1
      sql_telemetry_enabled = false
    }
    feature = {
      search_sku            = "basic"
      search_replica_count  = 1
      sql_telemetry_enabled = true
    }
    pre-production = {
      search_sku            = "basic"
      search_replica_count  = 1
      sql_telemetry_enabled = false
    }
    production = {
      search_sku            = "basic"
      search_replica_count  = 3
      sql_telemetry_enabled = false
    }
  }
}
