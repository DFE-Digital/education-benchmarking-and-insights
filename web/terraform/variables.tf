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
      tier = "Basic"
      size = "B1"
    }
  }
}