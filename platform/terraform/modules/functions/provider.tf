terraform {
  required_version = ">= 1.0"
  required_providers {
    azapi = {
      source  = "azure/azapi"
      version = "1.13.1"
    }
    mssql = {
      source  = "betr-io/mssql"
      version = "0.3.1"
    }
  }
}
