variable "environment" {}
variable "cip-environment" {}
variable "environment-prefix" {}
variable "location" {}

variable "configuration" {
  type = map(object({
    sku_name               = string
    zone_balancing_enabled = bool
    worker_count           = number
    features = object({
      CurriculumFinancialPlanning = bool
      Trusts                      = bool
    })
  }))
  default = {
    development = {
      sku_name               = "B1"
      zone_balancing_enabled = false
      worker_count           = 1
      features = {
        Trusts                      = true
        CurriculumFinancialPlanning = true
      }
    }
    automated-test = {
      sku_name               = "B1"
      zone_balancing_enabled = false
      worker_count           = 1
      features = {
        Trusts                      = false
        CurriculumFinancialPlanning = true
      }
    }
    test = {
      sku_name               = "B1"
      zone_balancing_enabled = false
      worker_count           = 1
      features = {
        Trusts                      = false
        CurriculumFinancialPlanning = false
      }
    }
  }
}

variable "dfe-signin" {}