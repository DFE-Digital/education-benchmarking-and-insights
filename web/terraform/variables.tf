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
      CurriculumFinancialPlanning   = bool
      CustomData                    = bool
      Trusts                        = bool
      LocalAuthorities              = bool
      UserDefinedComparators        = bool
      DisableOrganisationClaimCheck = bool
      ForecastRisk                  = bool
      TrustComparison               = bool
    })
  }))
  default = {
    development = {
      sku_name               = "B1"
      zone_balancing_enabled = false
      worker_count           = 1
      front_door_sku_name    = "Standard_AzureFrontDoor"
      features = {
        Trusts                        = true
        CurriculumFinancialPlanning   = true
        CustomData                    = true
        LocalAuthorities              = true
        UserDefinedComparators        = true
        DisableOrganisationClaimCheck = true
        ForecastRisk                  = true
        TrustComparison               = true
      }
    }
    automated-test = {
      sku_name               = "B1"
      zone_balancing_enabled = false
      worker_count           = 1
      front_door_sku_name    = "Standard_AzureFrontDoor"
      features = {
        Trusts                        = true
        CurriculumFinancialPlanning   = true
        CustomData                    = true
        LocalAuthorities              = true
        UserDefinedComparators        = true
        DisableOrganisationClaimCheck = true
        ForecastRisk                  = true
        TrustComparison               = true
      }
    }
    test = {
      sku_name               = "P0v3"
      zone_balancing_enabled = false
      worker_count           = 1
      front_door_sku_name    = "Standard_AzureFrontDoor"
      features = {
        Trusts                        = true
        CurriculumFinancialPlanning   = true
        CustomData                    = true
        LocalAuthorities              = true
        UserDefinedComparators        = true
        DisableOrganisationClaimCheck = true
        ForecastRisk                  = true
        TrustComparison               = true
      }
    }
    feature = {
      sku_name               = "B1"
      zone_balancing_enabled = false
      worker_count           = 1
      front_door_sku_name    = "Standard_AzureFrontDoor"
      features = {
        Trusts                        = true
        CurriculumFinancialPlanning   = true
        CustomData                    = true
        LocalAuthorities              = true
        UserDefinedComparators        = true
        DisableOrganisationClaimCheck = true
        ForecastRisk                  = true
        TrustComparison               = true
      }
    }
    pre-production = {
      sku_name               = "P0v3"
      zone_balancing_enabled = false
      worker_count           = 1
      front_door_sku_name    = "Standard_AzureFrontDoor"
      features = {
        Trusts                        = true
        CurriculumFinancialPlanning   = true
        CustomData                    = true
        LocalAuthorities              = true
        UserDefinedComparators        = true
        DisableOrganisationClaimCheck = false
        ForecastRisk                  = true
        TrustComparison               = true
      }
    }
    production = {
      sku_name               = "P1v3"
      zone_balancing_enabled = false
      worker_count           = 1
      front_door_sku_name    = "Standard_AzureFrontDoor"
      features = {
        Trusts                        = true
        CurriculumFinancialPlanning   = true
        CustomData                    = true
        LocalAuthorities              = true
        UserDefinedComparators        = true
        DisableOrganisationClaimCheck = false
        ForecastRisk                  = false
        TrustComparison               = true
      }
    }
  }
}

variable "dfe-signin" {}