variable "environment" {}
variable "cip-environment" {}
variable "environment-prefix" {}
variable "location" {}
variable "redirect-app-service-provision" {}
variable "shutter-app-service-provision" {}
variable "shutter-app-service-enabled" {}

# noinspection TfIncorrectVariableType
variable "configuration" {
  type = map(object({
    sku_name                       = string
    zone_balancing_enabled         = bool
    worker_count                   = number
    front_door_profile_sku_name    = string
    front_door_waf_policy_sku_name = string
    waf_mode                       = string
    features = object({
      CurriculumFinancialPlanning          = optional(bool, true)
      CustomData                           = optional(bool, true)
      Trusts                               = optional(bool, true)
      LocalAuthorities                     = optional(bool, true)
      UserDefinedComparators               = optional(bool, true)
      ForecastRisk                         = optional(bool, true)
      TrustComparison                      = optional(bool, true)
      FinancialBenchmarkingInsightsSummary = optional(bool, true)
      HistoricalTrends                     = optional(bool, true)
      HighExecutivePay                     = optional(bool, true)
      HighNeeds                            = optional(bool, true)
      SchoolSpendingPrioritiesSsrCharts    = optional(bool, true)
      CfrItSpendBreakdown                  = optional(bool, false)
      News                                 = optional(bool, false)
    })
    CacheOptions = object({
      ReturnYears = object({
        SlidingExpiration  = number
        AbsoluteExpiration = number
      })
      CommercialResources = object({
        SlidingExpiration  = number
        AbsoluteExpiration = number
      })
      Banners = object({
        SlidingExpiration  = number
        AbsoluteExpiration = number
      })
    })
    DISABLE_ORG_CLAIM_CHECK = optional(bool, false)
  }))
  default = {
    development = {
      sku_name                       = "B1"
      zone_balancing_enabled         = false
      worker_count                   = 1
      front_door_profile_sku_name    = "Standard_AzureFrontDoor"
      front_door_waf_policy_sku_name = "Standard_AzureFrontDoor"
      waf_mode                       = "Detection"
      features = {
        CfrItSpendBreakdown = true,
        News                = true
      },
      CacheOptions = {
        ReturnYears = {
          SlidingExpiration  = 10
          AbsoluteExpiration = 60
        },
        CommercialResources = {
          SlidingExpiration  = 10
          AbsoluteExpiration = 60
        },
        Banners = {
          SlidingExpiration  = 10
          AbsoluteExpiration = 60
        }
      },
      DISABLE_ORG_CLAIM_CHECK = true
    }
    automated-test = {
      sku_name                       = "B1"
      zone_balancing_enabled         = false
      worker_count                   = 1
      front_door_profile_sku_name    = "Standard_AzureFrontDoor"
      front_door_waf_policy_sku_name = "Standard_AzureFrontDoor"
      waf_mode                       = "Detection"
      features = {
        CfrItSpendBreakdown = true,
        News                = true
      },
      CacheOptions = {
        ReturnYears = {
          SlidingExpiration  = 10
          AbsoluteExpiration = 60
        },
        CommercialResources = {
          SlidingExpiration  = 10
          AbsoluteExpiration = 60
        },
        Banners = {
          SlidingExpiration  = 10
          AbsoluteExpiration = 60
        }
      },
    }
    test = {
      sku_name                       = "P0v3"
      zone_balancing_enabled         = false
      worker_count                   = 1
      front_door_profile_sku_name    = "Standard_AzureFrontDoor"
      front_door_waf_policy_sku_name = "Standard_AzureFrontDoor"
      waf_mode                       = "Prevention"
      features = {
        CfrItSpendBreakdown = true,
        News                = true
      },
      CacheOptions = {
        ReturnYears = {
          SlidingExpiration  = 10
          AbsoluteExpiration = 60
        },
        CommercialResources = {
          SlidingExpiration  = 10
          AbsoluteExpiration = 60
        },
        Banners = {
          SlidingExpiration  = 10
          AbsoluteExpiration = 60
        }
      },
      DISABLE_ORG_CLAIM_CHECK = true
    }
    feature = {
      sku_name                       = "B1"
      zone_balancing_enabled         = false
      worker_count                   = 1
      front_door_profile_sku_name    = "Standard_AzureFrontDoor"
      front_door_waf_policy_sku_name = "Standard_AzureFrontDoor"
      waf_mode                       = "Detection"
      features = {
        CfrItSpendBreakdown = true,
        News                = true
      },
      CacheOptions = {
        ReturnYears = {
          SlidingExpiration  = 10
          AbsoluteExpiration = 60
        },
        CommercialResources = {
          SlidingExpiration  = 10
          AbsoluteExpiration = 60
        },
        Banners = {
          SlidingExpiration  = 10
          AbsoluteExpiration = 60
        }
      },
      DISABLE_ORG_CLAIM_CHECK = true
    }
    pre-production = {
      sku_name                       = "P0v3"
      zone_balancing_enabled         = false
      worker_count                   = 1
      front_door_profile_sku_name    = "Standard_AzureFrontDoor"
      front_door_waf_policy_sku_name = "Standard_AzureFrontDoor"
      waf_mode                       = "Detection"
      features = {
      }
      CacheOptions = {
        ReturnYears = {
          SlidingExpiration  = 10
          AbsoluteExpiration = 60
        },
        CommercialResources = {
          SlidingExpiration  = 10
          AbsoluteExpiration = 60
        },
        Banners = {
          SlidingExpiration  = 10
          AbsoluteExpiration = 60
        }
      }
    }
    production = {
      sku_name                       = "P1v3"
      zone_balancing_enabled         = false
      worker_count                   = 1
      front_door_profile_sku_name    = "Premium_AzureFrontDoor"
      front_door_waf_policy_sku_name = "Premium_AzureFrontDoor"
      waf_mode                       = "Detection"
      features = {
      },
      CacheOptions = {
        ReturnYears = {
          SlidingExpiration  = 10
          AbsoluteExpiration = 60
        },
        CommercialResources = {
          SlidingExpiration  = 10
          AbsoluteExpiration = 60
        },
        Banners = {
          SlidingExpiration  = 10
          AbsoluteExpiration = 60
        }
      }
    }
  }
}

variable "dfe-signin" {}
