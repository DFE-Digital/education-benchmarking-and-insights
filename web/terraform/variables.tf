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
      CustomData                  = bool
      Trusts                      = bool
      LocalAuthorities            = bool
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
        CustomData                  = true
        LocalAuthorities            = true
      }
    }
    automated-test = {
      sku_name               = "B1"
      zone_balancing_enabled = false
      worker_count           = 1
      front_door_sku_name    = "Standard_AzureFrontDoor"
      features = {
        Trusts                      = true
        CurriculumFinancialPlanning = true
        CustomData                  = true
        LocalAuthorities            = true
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
        CustomData                  = false
        LocalAuthorities            = false
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
        CustomData                  = true
        LocalAuthorities            = true
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
        CustomData                  = false
        LocalAuthorities            = false
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
        CustomData                  = false
        LocalAuthorities            = false
      }
    }
  }
}

variable "dfe-signin" {}