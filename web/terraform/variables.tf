variable "environment" {}
variable "cip-environment" {}
variable "environment-prefix" {}
variable "location" {}

variable "configuration" {
  type = map(object({
    sku_name               = string
    zone_balancing_enabled = bool
    worker_count           = number
    front_door_sku_name    = string
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
      front_door_sku_name    = "Standard_AzureFrontDoor"
      features = {
        Trusts                      = true
        CurriculumFinancialPlanning = true
      }
    }
    automated-test = {
      sku_name               = "B1"
      zone_balancing_enabled = false
      worker_count           = 1
      front_door_sku_name    = "Standard_AzureFrontDoor"
      features = {
        Trusts                      = false
        CurriculumFinancialPlanning = true
      }
    }
    test = {
      sku_name               = "B1"
      zone_balancing_enabled = false
      worker_count           = 1
      front_door_sku_name    = "Standard_AzureFrontDoor"
      features = {
        Trusts                      = false
        CurriculumFinancialPlanning = false
      }
    }
    feature = {
      sku_name               = "B1"
      zone_balancing_enabled = false
      worker_count           = 1
      front_door_sku_name    = "Standard_AzureFrontDoor"
      features = {
        Trusts                      = true
        CurriculumFinancialPlanning = true
      }
    }
    pre-production = {
      sku_name               = "B1"
      zone_balancing_enabled = false
      worker_count           = 1
      front_door_sku_name    = "Standard_AzureFrontDoor"
      features = {
        Trusts                      = false
        CurriculumFinancialPlanning = false
      }
    }
    production = {
      sku_name               = "B1"
      zone_balancing_enabled = false
      worker_count           = 1
      front_door_sku_name    = "Standard_AzureFrontDoor"
      features = {
        Trusts                      = false
        CurriculumFinancialPlanning = false
      }
    }
  }
}

variable "dfe-signin" {}