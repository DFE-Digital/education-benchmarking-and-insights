variable "environment" {}
variable "cip-environment" {}
variable "environment-prefix" {}
variable "location" {}

variable "configuration" {
  type = map(object({
    cosmos = object({
      capabilities = list(string)
    })
    search = object({
      sku = string
    })
  }))
  default = {
    development = {
      cosmos = {
        capabilities = [
          "EnableServerless"
        ]
      }
      search = {
        sku = "basic"
      }
    }
    automated-test = {
      cosmos = {
        capabilities = [
          "EnableServerless"
        ]
      }
      search = {
        sku = "basic"
      }
    }
    test = {
      cosmos = {
        capabilities = [
          "EnableServerless"
        ]
      }
      search = {
        sku = "basic"
      }
    }
    pre-production = {
      cosmos = {
        capabilities = [
          "EnableServerless"
        ]
      }
      search = {
        sku = "basic"
      }
    }
    production = {
      cosmos = {
        capabilities = [
          "EnableServerless"
        ]
      }
      search = {
        sku = "basic"
      }
    }
  }
}