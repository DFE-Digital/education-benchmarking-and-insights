variable "environment" {}
variable "cip-environment" {}
variable "environment-prefix" {}
variable "location" {}

variable "sizing" {
  type = map(object({
    sku                    = string
    zone_balancing_enabled = bool
    worker_count           = number
  }))
  default = {
    development = {
      sku_name               = "B1"
      zone_balancing_enabled = false
      worker_count           = 1
    }
  }
}

variable "dfe-signin" {}