variable "environment" {}
variable "cip-environment" {}
variable "environment-prefix" {}
variable "location" {}
variable "disable-purge-protection" {}
variable "support-alert-email" {}

variable "configuration" {
  type = map(object({
    sql_db_sku_name                  = string
    sql_db_max_size_gb               = number
    databricks_access_connector_name = string
    databricks_access_connector_name = string
  }))
  default = {
    development = {
      sql_db_sku_name                  = "S0"
      sql_db_max_size_gb               = 5
      databricks_access_connector_name = "s101d01edapDatabricksConnector"
      databricks_access_connector_rg   = "s101d01-rg-edap"
    }
    automated-test = {
      sql_db_sku_name                  = "S1"
      sql_db_max_size_gb               = 5
      databricks_access_connector_name = ""
      databricks_access_connector_rg   = ""
    }
    test = {
      sql_db_sku_name                  = "S1"
      sql_db_max_size_gb               = 5
      databricks_access_connector_name = "s101t01adaptDatabricksConnector"
      databricks_access_connector_rg   = "s101t01-rg-adapt"
    }
    feature = {
      sql_db_sku_name                  = "S0"
      sql_db_max_size_gb               = 5
      databricks_access_connector_name = ""
      databricks_access_connector_rg   = ""
    }
    pre-production = {
      sql_db_sku_name                  = "S1"
      sql_db_max_size_gb               = 5
      databricks_access_connector_name = "s101p02adaptDatabricksConnector"
      databricks_access_connector_rg   = "s101p02-rg-edap"
    }
    production = {
      sql_db_sku_name                  = "S2"
      sql_db_max_size_gb               = 5
      databricks_access_connector_name = "s101p01adaptDatabricksConnector"
      databricks_access_connector_rg   = "s101p01-rg-edap"
    }
  }
}
