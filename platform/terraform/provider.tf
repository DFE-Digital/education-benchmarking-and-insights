terraform {
  required_version = ">= 1.9.8"
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 4.28.0"
    }
  }
  backend "azurerm" {}
}

provider "azurerm" {
  storage_use_azuread = true
  features {}
}
