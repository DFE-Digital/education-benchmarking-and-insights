terraform {
  required_version = ">= 1.0"
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.32"
    }
  }
  backend "azurerm" {}
}

provider "azurerm" {
  features {}
}

