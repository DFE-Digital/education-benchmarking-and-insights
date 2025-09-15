variable "environment" {}
variable "cip-environment" {}
variable "environment-prefix" {}
variable "location" {}
variable "redirect-app-service-provision" {}
variable "shutter-app-service-provision" {}
variable "shutter-app-service-enabled" {}

variable "web-assets-config" {
  type = object({
    ip_whitelist = list(string)
    containers = map(object({
      extensions = list(string)
    }))
  })
  default = {
    // allow DFE VPN ranges container access with Premium_AzureFrontDoor
    ip_whitelist = ["208.127.46.236/30", "208.127.46.240/28"]
    containers = {
      files = {
        extensions = ["xls", "xlsx"]
      }
      images = {
        extensions = ["jpg", "jpeg", "png", "gif", "svg", "webp"]
      }
    }
  }
}

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
      HighExecutivePay                  = optional(bool, true)
      HighNeeds                         = optional(bool, true)
      SchoolSpendingPrioritiesSsrCharts = optional(bool, true)
      CfrItSpendBreakdown               = optional(bool, true)
      News                              = optional(bool, false)
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
        News = true
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
        News = true
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
        News = true
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
