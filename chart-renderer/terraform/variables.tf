variable "environment" {}
variable "cip-environment" {}
variable "environment-prefix" {}
variable "location" {}
variable "disable-purge-protection" {
  type    = string
  default = "false"
}

locals {
  common-tags = {
    "Environment"      = var.cip-environment
    "Service Offering" = "DfE Financial Benchmarking service"
    "Product"          = "DfE Financial Benchmarking service"
    "Source"           = "terraform"
    "Component"        = "Chart Renderer"
    "Context"          = var.environment
  }
}