terraform {
  required_version = ">= 1.0"
  required_providers {
    azapi = {
      source  = "azure/azapi"
      version = "1.15.0"
    }
    mssql = {
      source  = "betr-io/mssql"
      version = "0.3.1"
    }
  }
}
