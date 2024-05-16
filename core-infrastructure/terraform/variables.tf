variable "environment" {}
variable "cip-environment" {}
variable "environment-prefix" {}
variable "location" {}
variable "disable-purge-protection" {}


variable "configuration" {
  type = map(object({
    sql-db = object({
      sku_name = string
      max_size_gb = number
    })
  }))
  default = {
    development = {
      sql-db = {
        sku_name = "S0"
        max_size_gb = 5
      }
    }
    automated-test = {
      sql-db = {
        sku_name = "S0"
        max_size_gb = 5
      }
    }
    test = {
      sql-db = {
        sku_name = "S0"
        max_size_gb = 5
      }
    }
    feature = {
      sql-db = {
        sku_name = "S0"
        max_size_gb = 5
      }
    }
    pre-production = {
      sql-db = {
        sku_name = "S0"
        max_size_gb = 5
      }
    }
    production = {
      sql-db = {
        sku_name = "S0"
        max_size_gb = 5
      }
    }
  }
}