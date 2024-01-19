terraform {
  required_version = ">= 1.0"
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "= 3.87.0"
    }
  }
  backend "azurerm" {}
}

provider "azurerm" {
  features {}
}

