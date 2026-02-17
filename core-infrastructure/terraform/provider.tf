terraform {
  required_version = ">= 1.9.8"
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 4.58.0"
    }
    azapi = {
      source  = "azure/azapi"
      version = "~> 2.8.0"
    }
    mssql = {
      source  = "betr-io/mssql"
      version = "0.3.1"
    }
  }
  backend "azurerm" {}
}

provider "azurerm" {
  storage_use_azuread = true
  features {
    key_vault {
      purge_soft_delete_on_destroy    = true
      recover_soft_deleted_key_vaults = true
    }
  }
}

provider "azurerm" {
  alias = "databricks_sub"
  subscription_id = var.configuration[var.environment].databricks_subscription_id
}

provider "azapi" {
}
