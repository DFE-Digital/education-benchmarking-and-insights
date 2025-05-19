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
    redirect_app_service   = bool
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
      HighNeeds                            = bool
      FilteredSearch                       = bool
      SchoolSpendingPrioritiesSsrCharts    = bool
    })
    CacheOptions = object({
      ReturnYears = object({
        SlidingExpiration  = number
        AbsoluteExpiration = number
      }),
      CommercialResources = object({
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
      redirect_app_service   = false
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
        HighNeeds                            = true
        FilteredSearch                       = false
        SchoolSpendingPrioritiesSsrCharts    = false
      },
      CacheOptions = {
        ReturnYears = {
          SlidingExpiration  = 10
          AbsoluteExpiration = 60
        },
        CommercialResources = {
          SlidingExpiration  = 10
          AbsoluteExpiration = 60
        }
      }
    }
    automated-test = {
      sku_name               = "B1"
      zone_balancing_enabled = false
      worker_count           = 1
      front_door_sku_name    = "Standard_AzureFrontDoor"
      waf_mode               = "Detection"
      redirect_app_service   = false
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
        HighNeeds                            = true
        FilteredSearch                       = false
        SchoolSpendingPrioritiesSsrCharts    = false
      },
      CacheOptions = {
        ReturnYears = {
          SlidingExpiration  = 10
          AbsoluteExpiration = 60
        },
        CommercialResources = {
          SlidingExpiration  = 10
          AbsoluteExpiration = 60
        }
      }
    }
    test = {
      sku_name               = "P0v3"
      zone_balancing_enabled = false
      worker_count           = 1
      front_door_sku_name    = "Standard_AzureFrontDoor"
      waf_mode               = "Prevention"
      redirect_app_service   = false
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
        HighNeeds                            = true
        FilteredSearch                       = false
        SchoolSpendingPrioritiesSsrCharts    = false
      },
      CacheOptions = {
        ReturnYears = {
          SlidingExpiration  = 10
          AbsoluteExpiration = 60
        },
        CommercialResources = {
          SlidingExpiration  = 10
          AbsoluteExpiration = 60
        }
      }
    }
    feature = {
      sku_name               = "B1"
      zone_balancing_enabled = false
      worker_count           = 1
      front_door_sku_name    = "Standard_AzureFrontDoor"
      waf_mode               = "Detection"
      redirect_app_service   = true
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
        }
      }
    }
    pre-production = {
      sku_name               = "P0v3"
      zone_balancing_enabled = false
      worker_count           = 1
      front_door_sku_name    = "Standard_AzureFrontDoor"
      waf_mode               = "Prevention"
      redirect_app_service   = false
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
        HighNeeds                            = true
        FilteredSearch                       = false
        SchoolSpendingPrioritiesSsrCharts    = false
      }
      CacheOptions = {
        ReturnYears = {
          SlidingExpiration  = 10
          AbsoluteExpiration = 60
        },
        CommercialResources = {
          SlidingExpiration  = 10
          AbsoluteExpiration = 60
        }
      }
    }
    production = {
      sku_name               = "P1v3"
      zone_balancing_enabled = false
      worker_count           = 1
      front_door_sku_name    = "Standard_AzureFrontDoor"
      waf_mode               = "Prevention"
      redirect_app_service   = true
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
        HighNeeds                            = true
        FilteredSearch                       = false
        SchoolSpendingPrioritiesSsrCharts    = false
      },
      CacheOptions = {
        ReturnYears = {
          SlidingExpiration  = 10
          AbsoluteExpiration = 60
        },
        CommercialResources = {
          SlidingExpiration  = 10
          AbsoluteExpiration = 60
        }
      }
    }
  }
}

variable "dfe-signin" {}
