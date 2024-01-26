variable "environment" {}
variable "cip-environment" {}
variable "environment-prefix" {}
variable "location" {}

variable "sizing" {
  type = map(object({
    sku = string
  }))
  default = {
    development = {
      sku = "B1"
    }
  }
}

variable "dfe-signin" {}