variable "environment" {}
variable "cip-environment" {}
variable "environment-prefix" {}
variable "location" {}
variable "disable-purge-protection" {}


variable "configuration" {
  type = map(object({
    sql_db_sku_name    = string
    sql_db_max_size_gb = number
  }))
  default = {
    development = {
      sql_db_sku_name    = "S0"
      sql_db_max_size_gb = 5
    }
    automated-test = {
      sql_db_sku_name    = "S1"
      sql_db_max_size_gb = 5
    }
    test = {
      sql_db_sku_name    = "S1"
      sql_db_max_size_gb = 5
    }
    feature = {
      sql_db_sku_name    = "S0"
      sql_db_max_size_gb = 5
    }
    pre-production = {
      sql_db_sku_name    = "S1"
      sql_db_max_size_gb = 5
    }
    production = {
      sql_db_sku_name    = "S2"
      sql_db_max_size_gb = 5
    }
  }
}

variable "trackedEvents" {
  type = list(string)
  default = [
    "gias-school-details",
    "commercial-resource",
    "guidance-resource",
    "data-source",
    "organisation",
    "service-banner",
    "change-organisation",
    "survey"
  ]
}