variable "environment" {}
variable "cip-environment" {}
variable "environment-prefix" {}
variable "location" {}

locals {
  common-tags = {
    Environment    = var.environment
    Service        = "Education Benchmarking and Insights"
    Component      = "Chart Renderer"
    CIP-Environment = var.cip-environment
  }
}
