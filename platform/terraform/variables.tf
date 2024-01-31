variable "environment" {}
variable "cip-environment" {}
variable "environment-prefix" {}
variable "location" {}

variable "configuration" {
  type = map(object({
    cosmos = object({
      capabilities = list(string)
    })
  }))
  default = {
    development = {
      cosmos = {
        capabilities = []
      }
    }
    functional-test = {
      cosmos = {
        capabilities = [
          "EnableServerless"
        ]
      }
    }
    accessibility-test = {
      cosmos = {
        capabilities = [
          "EnableServerless"
        ]
      }
    }
  }
}