variable "environment" {}
variable "cip-environment" {}
variable "environment-prefix" {}
variable "location" {}

variable "configuration" {
  type = map(object({
    sku_name               = string
    zone_balancing_enabled = bool
    worker_count           = number
  }))
  default = {
    development = {
      sku_name               = "B1"
      zone_balancing_enabled = false
      worker_count           = 1
    }
    automated-test = {
      sku_name               = "B1"
      zone_balancing_enabled = false
      worker_count           = 1
    }
  }
}

variable "dfe-signin" {}