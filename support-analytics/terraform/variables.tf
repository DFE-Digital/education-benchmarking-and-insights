variable "environment" {}
variable "cip-environment" {}
variable "environment-prefix" {}
variable "location" {}
variable "support-alert-email" {}
variable "teams-team-id" {}
variable "teams-channel-id" {}
variable "teams-channel-id-dev" {}
variable "teams-channel-id-preprod" {}

variable "configuration" {
  type = map(object({
    alerts_enabled       = bool
    email_alerts_enabled = bool
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
      email_alerts_enabled = false
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
      email_alerts_enabled = false
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
      email_alerts_enabled = false
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
      email_alerts_enabled = false
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
      email_alerts_enabled = false
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
      email_alerts_enabled = true
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
    "change-organisation",
    "commercial-resource",
    "copy-chart-as-image",
    "data-source",
    "download-page-data",
    "gias-school-details",
    "guidance-resource",
    "organisation",
    "save-chart-as-image",
    "service-banner",
    "survey"
  ]
}

variable "establishmentTypes" {
  type = list(string)
  default = [
    "school",
    "trust",
    "local-authority"
  ]
}

variable "trackedAuthEvents" {
  type = list(string)
  default = [
    "user-sign-in-initiated",
    "user-sign-in-success"
  ]
}
