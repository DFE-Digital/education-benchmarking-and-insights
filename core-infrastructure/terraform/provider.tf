terraform {
  required_version = ">= 1.0"
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "3.92.0"
    }
  }
  backend "azurerm" {}
}

provider "azurerm" {
  features {}
}
