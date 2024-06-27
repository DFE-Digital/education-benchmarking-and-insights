variable "environment" {}
variable "cip-environment" {}
variable "environment-prefix" {}
variable "location" {}

variable "configuration" {
  type = map(object({
    search_sku           = string
    search_replica_count = number
  }))
  default = {
    development = {
      search_sku           = "basic"
      search_replica_count = 1
    }
    automated-test = {
      search_sku           = "basic"
      search_replica_count = 1
    }
    test = {
      search_sku           = "basic"
      search_replica_count = 1
    }
    feature = {
      search_sku           = "basic"
      search_replica_count = 1
    }
    pre-production = {
      search_sku           = "basic"
      search_replica_count = 1
    }
    production = {
      search_sku           = "standard"
      search_replica_count = 2
    }
  }
}