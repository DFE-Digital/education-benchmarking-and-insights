variable "environment" {}
variable "cip-environment" {}
variable "environment-prefix" {}
variable "location" {}
variable "support-alert-email" {}
variable "configuration" {
  type = map(object({
    alerts_enabled = bool
  }))
  default = {
    development = {
      alerts_enabled = true
    }
    automated-test = {
      alerts_enabled = false
    }
    test = {
      alerts_enabled = false
    }
    feature = {
      alerts_enabled = false
    }
    pre-production = {
      alerts_enabled = false
    }
    production = {
      alerts_enabled = false
    }
  }
}
