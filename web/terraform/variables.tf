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
    waf_mode               = string
    features = object({
      CurriculumFinancialPlanning   = bool
      CustomData                    = bool
      Trusts                        = bool
      LocalAuthorities              = bool
      UserDefinedComparators        = bool
      DisableOrganisationClaimCheck = bool
      ForecastRisk                  = bool
      TrustComparison               = bool
      BenchmarkingReportCards       = bool
    })
    CacheOptions = object({
      ReturnYears = object({
        SlidingExpiration  = number
        AbsoluteExpiration = number
      })
    })
  }))
  default = {
    development = {
      sku_name               = "B1"
      zone_balancing_enabled = false
      worker_count           = 1
      front_door_sku_name    = "Standard_AzureFrontDoor"
      waf_mode               = "Detection"
      features = {
        Trusts                        = true
        CurriculumFinancialPlanning   = true
        CustomData                    = true
        LocalAuthorities              = true
        UserDefinedComparators        = true
        DisableOrganisationClaimCheck = true
        ForecastRisk                  = true
        TrustComparison               = true
        BenchmarkingReportCards       = true
      },
      CacheOptions = {
        ReturnYears = {
          SlidingExpiration  = 60
          AbsoluteExpiration = 3600
        }
      }
    }
    automated-test = {
      sku_name               = "B1"
      zone_balancing_enabled = false
      worker_count           = 1
      front_door_sku_name    = "Standard_AzureFrontDoor"
      waf_mode               = "Detection"
      features = {
        Trusts                        = true
        CurriculumFinancialPlanning   = true
        CustomData                    = true
        LocalAuthorities              = true
        UserDefinedComparators        = true
        DisableOrganisationClaimCheck = false
        ForecastRisk                  = true
        TrustComparison               = true
        BenchmarkingReportCards       = true
      },
      CacheOptions = {
        ReturnYears = {
          SlidingExpiration  = 60
          AbsoluteExpiration = 3600
        }
      }
    }
    test = {
      sku_name               = "P0v3"
      zone_balancing_enabled = false
      worker_count           = 1
      front_door_sku_name    = "Standard_AzureFrontDoor"
      waf_mode               = "Prevention"
      features = {
        Trusts                        = true
        CurriculumFinancialPlanning   = true
        CustomData                    = true
        LocalAuthorities              = true
        UserDefinedComparators        = true
        DisableOrganisationClaimCheck = true
        ForecastRisk                  = true
        TrustComparison               = true
        BenchmarkingReportCards       = false
      },
      CacheOptions = {
        ReturnYears = {
          SlidingExpiration  = 60
          AbsoluteExpiration = 3600
        }
      }
    }
    feature = {
      sku_name               = "B1"
      zone_balancing_enabled = false
      worker_count           = 1
      front_door_sku_name    = "Standard_AzureFrontDoor"
      waf_mode               = "Detection"
      features = {
        Trusts                        = true
        CurriculumFinancialPlanning   = true
        CustomData                    = true
        LocalAuthorities              = true
        UserDefinedComparators        = true
        DisableOrganisationClaimCheck = true
        ForecastRisk                  = true
        TrustComparison               = true
        BenchmarkingReportCards       = true
      },
      CacheOptions = {
        ReturnYears = {
          SlidingExpiration  = 60
          AbsoluteExpiration = 3600
        }
      }
    }
    pre-production = {
      sku_name               = "P0v3"
      zone_balancing_enabled = false
      worker_count           = 1
      front_door_sku_name    = "Standard_AzureFrontDoor"
      waf_mode               = "Prevention"
      features = {
        Trusts                        = true
        CurriculumFinancialPlanning   = true
        CustomData                    = true
        LocalAuthorities              = true
        UserDefinedComparators        = true
        DisableOrganisationClaimCheck = false
        ForecastRisk                  = true
        TrustComparison               = true
        BenchmarkingReportCards       = false
      }
      CacheOptions = {
        ReturnYears = {
          SlidingExpiration  = 60
          AbsoluteExpiration = 3600
        }
      }
    }
    production = {
      sku_name               = "P1v3"
      zone_balancing_enabled = false
      worker_count           = 1
      front_door_sku_name    = "Standard_AzureFrontDoor"
      waf_mode               = "Prevention"
      features = {
        Trusts                        = true
        CurriculumFinancialPlanning   = true
        CustomData                    = true
        LocalAuthorities              = true
        UserDefinedComparators        = true
        DisableOrganisationClaimCheck = false
        ForecastRisk                  = true
        TrustComparison               = true
        BenchmarkingReportCards       = false
      },
      CacheOptions = {
        ReturnYears = {
          SlidingExpiration  = 60
          AbsoluteExpiration = 3600
        }
      }
    }
  }
}

variable "dfe-signin" {}