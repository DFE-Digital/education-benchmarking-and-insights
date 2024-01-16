variable "environment" {}
variable "cip-environment" {}
variable "environment-prefix" {}
variable "location" {}

variable "sizing" {
  type = map(object({
    tier = string
    size = string
  }))
  default = {
    development = {
      tier = "Basic"
      size = "B1"
    }
  }
}

variable "dfe-signin" {}