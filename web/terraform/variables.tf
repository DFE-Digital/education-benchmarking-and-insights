variable "environment" {}
variable "cip-environment" {}
variable "environment-prefix" {}
variable "location" {}
variable "redirect-app-service-provision" {}
variable "shutter-app-service-provision" {}
variable "shutter-app-service-enabled" {}

variable "configuration" {
  type = map(object({
    sku_name                       = string
    zone_balancing_enabled         = bool
    worker_count                   = number
    front_door_profile_sku_name    = string
    front_door_waf_policy_sku_name = string
    waf_mode                       = string
    features = object({
      CurriculumFinancialPlanning          = bool
      CustomData                           = bool
      Trusts                               = bool
      LocalAuthorities                     = bool
      UserDefinedComparators               = bool
      DisableOrganisationClaimCheck        = bool
      ForecastRisk                         = bool
      TrustComparison                      = bool
      FinancialBenchmarkingInsightsSummary = bool
      HistoricalTrends                     = bool
      HighExecutivePay                     = bool
      HighNeeds                            = bool
      FilteredSearch                       = bool
      SchoolSpendingPrioritiesSsrCharts    = bool
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
        Trusts                               = true
        CurriculumFinancialPlanning          = true
        CustomData                           = true
        LocalAuthorities                     = true
        UserDefinedComparators               = true
        DisableOrganisationClaimCheck        = true
        ForecastRisk                         = true
        TrustComparison                      = true
        FinancialBenchmarkingInsightsSummary = true
        HistoricalTrends                     = true
        HighExecutivePay                     = true
        HighNeeds                            = true
        FilteredSearch                       = true
        SchoolSpendingPrioritiesSsrCharts    = true
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
    automated-test = {
      sku_name                       = "B1"
      zone_balancing_enabled         = false
      worker_count                   = 1
      front_door_profile_sku_name    = "Standard_AzureFrontDoor"
      front_door_waf_policy_sku_name = "Standard_AzureFrontDoor"
      waf_mode                       = "Detection"
      features = {
        Trusts                               = true
        CurriculumFinancialPlanning          = true
        CustomData                           = true
        LocalAuthorities                     = true
        UserDefinedComparators               = true
        DisableOrganisationClaimCheck        = false
        ForecastRisk                         = true
        TrustComparison                      = true
        FinancialBenchmarkingInsightsSummary = true
        HistoricalTrends                     = true
        HighExecutivePay                     = true
        HighNeeds                            = true
        FilteredSearch                       = true
        SchoolSpendingPrioritiesSsrCharts    = true
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
    test = {
      sku_name                       = "P0v3"
      zone_balancing_enabled         = false
      worker_count                   = 1
      front_door_profile_sku_name    = "Standard_AzureFrontDoor"
      front_door_waf_policy_sku_name = "Standard_AzureFrontDoor"
      waf_mode                       = "Prevention"
      features = {
        Trusts                               = true
        CurriculumFinancialPlanning          = true
        CustomData                           = true
        LocalAuthorities                     = true
        UserDefinedComparators               = true
        DisableOrganisationClaimCheck        = true
        ForecastRisk                         = true
        TrustComparison                      = true
        FinancialBenchmarkingInsightsSummary = true
        HistoricalTrends                     = true
        HighExecutivePay                     = true
        HighNeeds                            = true
        FilteredSearch                       = true
        SchoolSpendingPrioritiesSsrCharts    = true
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
    feature = {
      sku_name                       = "B1"
      zone_balancing_enabled         = false
      worker_count                   = 1
      front_door_profile_sku_name    = "Standard_AzureFrontDoor"
      front_door_waf_policy_sku_name = "Standard_AzureFrontDoor"
      waf_mode                       = "Detection"
      features = {
        Trusts                               = true
        CurriculumFinancialPlanning          = true
        CustomData                           = true
        LocalAuthorities                     = true
        UserDefinedComparators               = true
        DisableOrganisationClaimCheck        = true
        ForecastRisk                         = true
        TrustComparison                      = true
        FinancialBenchmarkingInsightsSummary = true
        HistoricalTrends                     = true
        HighExecutivePay                     = true
        HighNeeds                            = true
        FilteredSearch                       = true
        SchoolSpendingPrioritiesSsrCharts    = true
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
    pre-production = {
      sku_name                       = "P0v3"
      zone_balancing_enabled         = false
      worker_count                   = 1
      front_door_profile_sku_name    = "Standard_AzureFrontDoor"
      front_door_waf_policy_sku_name = "Standard_AzureFrontDoor"
      waf_mode                       = "Prevention"
      features = {
        Trusts                               = true
        CurriculumFinancialPlanning          = true
        CustomData                           = true
        LocalAuthorities                     = true
        UserDefinedComparators               = true
        DisableOrganisationClaimCheck        = false
        ForecastRisk                         = true
        TrustComparison                      = true
        FinancialBenchmarkingInsightsSummary = true
        HistoricalTrends                     = true
        HighExecutivePay                     = false
        HighNeeds                            = true
        FilteredSearch                       = true
        SchoolSpendingPrioritiesSsrCharts    = true
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
      front_door_profile_sku_name    = "Standard_AzureFrontDoor"
      front_door_waf_policy_sku_name = "Premium_AzureFrontDoor"
      waf_mode                       = "Prevention"
      features = {
        Trusts                               = true
        CurriculumFinancialPlanning          = true
        CustomData                           = true
        LocalAuthorities                     = true
        UserDefinedComparators               = true
        DisableOrganisationClaimCheck        = false
        ForecastRisk                         = true
        TrustComparison                      = true
        FinancialBenchmarkingInsightsSummary = true
        HistoricalTrends                     = true
        HighExecutivePay                     = false
        HighNeeds                            = true
        FilteredSearch                       = true
        SchoolSpendingPrioritiesSsrCharts    = true
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
