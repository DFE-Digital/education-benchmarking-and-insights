variable "environment" {}
variable "cip-environment" {}
variable "environment-prefix" {}
variable "location" {}
variable "package_path" {}

locals {
  host_key_manual_rotation_trigger = "2"
  common-tags = {
    "Environment"      = var.cip-environment
    "Service Offering" = "DfE Financial Benchmarking service"
    "Product"          = "DfE Financial Benchmarking service"
    "Source"           = "terraform"
    "Component"        = "Chart Renderer"
    "Context"          = var.environment
  }
}