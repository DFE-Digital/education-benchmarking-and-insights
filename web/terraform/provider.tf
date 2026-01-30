terraform {
  required_version = ">= 1.9.8"
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 4.57.0"
    }
    azapi = {
      source  = "azure/azapi"
      version = "~> 2.8.0"
    }
  }
  backend "azurerm" {}
}

provider "random" {}

provider "azurerm" {
  storage_use_azuread = true
  features {}
}

provider "azapi" {
}
