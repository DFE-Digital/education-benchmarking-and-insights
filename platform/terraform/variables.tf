variable "environment" {}
variable "cip-environment" {}
variable "environment-prefix" {}
variable "location" {}

variable "configuration" {
  type = map(object({
    search = object({
      sku = string
    })
  }))
  default = {
    development = {
      search = {
        sku = "basic"
      }
    }
    automated-test = {
      search = {
        sku = "basic"
      }
    }
    test = {
      search = {
        sku = "basic"
      }
    }
    feature = {
      search = {
        sku = "basic"
      }
    }
    pre-production = {
      search = {
        sku = "basic"
      }
    }
    production = {
      search = {
        sku = "basic"
      }
    }
  }
}