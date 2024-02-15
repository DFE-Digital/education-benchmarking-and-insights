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
    automated-test = {
      cosmos = {
        capabilities = [
          "EnableServerless"
        ]
      }
    }
  }
}