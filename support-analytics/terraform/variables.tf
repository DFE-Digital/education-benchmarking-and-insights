variable "environment" {}
variable "cip-environment" {}
variable "environment-prefix" {}
variable "location" {}
variable "support-alert-email" {}
variable "teams-team-id" {}
variable "teams-channel-id" {}

variable "configuration" {
  type = map(object({
    alerts_enabled       = bool
    smart_alerts_enabled = bool
    thresholds = object({
      availability = number
      cpu          = number
      memory       = number
      error        = number
    })
  }))
  default = {
    development = {
      alerts_enabled       = false
      smart_alerts_enabled = true
      thresholds = {
        availability = 90
        cpu          = 95
        memory       = 95
        error        = 5
      }
    }
    automated-test = {
      alerts_enabled       = false
      smart_alerts_enabled = false
      thresholds = {
        availability = 90
        cpu          = 95
        memory       = 95
        error        = 5
      }
    }
    test = {
      alerts_enabled       = false
      smart_alerts_enabled = false
      thresholds = {
        availability = 90
        cpu          = 95
        memory       = 95
        error        = 5
      }
    }
    feature = {
      alerts_enabled       = false
      smart_alerts_enabled = false
      thresholds = {
        availability = 90
        cpu          = 95
        memory       = 95
        error        = 5
      }
    }
    pre-production = {
      alerts_enabled       = true
      smart_alerts_enabled = true
      thresholds = {
        availability = 99.9
        cpu          = 85
        memory       = 85
        error        = 1
      }
    }
    production = {
      alerts_enabled       = true
      smart_alerts_enabled = true
      thresholds = {
        availability = 99.9
        cpu          = 85
        memory       = 85
        error        = 1
      }
    }
  }
}

variable "trackedEvents" {
  type = list(string)
  default = [
    "gias-school-details",
    "commercial-resource",
    "guidance-resource",
    "data-source",
    "organisation",
    "service-banner",
    "change-organisation",
    "survey"
  ]
}
