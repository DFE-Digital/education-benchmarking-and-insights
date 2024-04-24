resource "azurerm_cosmosdb_account" "cosmosdb-account" {
  #checkov:skip=CKV_AZURE_100:To be reviewed
  #checkov:skip=CKV_AZURE_101:To be reviewed
  #checkov:skip=CKV_AZURE_102:To be reviewed
  #checkov:skip=CKV_AZURE_132:To be reviewed
  #checkov:skip=CKV_AZURE_140:To be reviewed
  #checkov:skip=CKV_AZURE_99:To be reviewed
  name                = "${var.environment-prefix}-ebis-cdb"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  offer_type          = "Standard"
  kind                = "GlobalDocumentDB"

  consistency_policy {
    consistency_level = "Session"
  }

  tags = local.common-tags
  geo_location {
    failover_priority = 0
    location          = azurerm_resource_group.resource-group.location
  }

  dynamic "capabilities" {
    for_each = var.configuration[var.environment].cosmos.capabilities
    content {
      name = capabilities.value
    }
  }
}

resource "azurerm_key_vault_secret" "platform-cosmos-read-connection-string" {
  #checkov:skip=CKV_AZURE_41:Secrets expiration to be reviewed
  name         = "ebis-cdb-connection-string-r"
  value        = azurerm_cosmosdb_account.cosmosdb-account.primary_readonly_sql_connection_string
  key_vault_id = data.azurerm_key_vault.key-vault.id
  content_type = "connection-string"
}

resource "azurerm_key_vault_secret" "platform-cosmos-readwrite-connection-string" {
  #checkov:skip=CKV_AZURE_41:Secrets expiration to be reviewed
  name         = "ebis-cdb-connection-string-rw"
  value        = azurerm_cosmosdb_account.cosmosdb-account.primary_sql_connection_string
  key_vault_id = data.azurerm_key_vault.key-vault.id
  content_type = "connection-string"
}

#TODO: rename resource - it's database not container
resource "azurerm_cosmosdb_sql_database" "cosmosdb-container" {
  name                = "ebis-data"
  account_name        = azurerm_cosmosdb_account.cosmosdb-account.name
  resource_group_name = azurerm_resource_group.resource-group.name
}

resource "azurerm_cosmosdb_sql_container" "cosmosdb-fp-container" {
  name                = "financial-plans"
  resource_group_name = azurerm_resource_group.resource-group.name
  account_name        = azurerm_cosmosdb_account.cosmosdb-account.name
  database_name       = azurerm_cosmosdb_sql_database.cosmosdb-container.name
  partition_key_path  = "/partitionKey"

  indexing_policy {
    indexing_mode = "consistent"

    included_path {
      path = "/*"
    }
  }
}

resource "random_password" "sql-admin-password" {
  length           = 16
  special          = true
  override_special = "!@#$*()-_=+"
}

locals {
  sql-admin-login = "ebis-sql-admin"
}

resource "azurerm_key_vault_secret" "platform-sql-connection-string" {
  #checkov:skip=CKV_AZURE_41:Secrets expiration to be reviewed
  name         = "ebis-sql-connection-string"
  value        = "Server=tcp:${azurerm_mssql_server.sql-server.fully_qualified_domain_name},1433;Database=${azurerm_mssql_database.sql-db.name};User ID=${local.sql-admin-login};Password=${random_password.sql-admin-password.result};Trusted_Connection=False;Encrypt=True;"
  key_vault_id = data.azurerm_key_vault.key-vault.id
  content_type = "connection-string"
}

resource "azurerm_mssql_server" "sql-server" {
  #checkov:skip=CKV_AZURE_113:Public access required for Azure Devops build agents
  #checkov:skip=CKV2_AZURE_45:To be reviewed for production
  name                         = "${var.environment-prefix}-ebis-sql"
  version                      = "12.0"
  resource_group_name          = azurerm_resource_group.resource-group.name
  location                     = azurerm_resource_group.resource-group.location
  administrator_login          = local.sql-admin-login
  administrator_login_password = random_password.sql-admin-password.result
  tags                         = local.common-tags
  minimum_tls_version          = "1.2"

  azuread_administrator {
    login_username = "michael.fielding@education.gov.uk"
    object_id      = "42665fd4-ab1f-4192-9033-bfc059c6ea9a"
  }
}

resource "azurerm_mssql_server_extended_auditing_policy" "sql-server-audit-policy" {
  server_id                               = azurerm_mssql_server.sql-server.id
  storage_endpoint                        = azurerm_storage_account.audit-storage.primary_blob_endpoint
  storage_account_access_key              = azurerm_storage_account.audit-storage.primary_access_key
  storage_account_access_key_is_secondary = false
  retention_in_days                       = 120
  log_monitoring_enabled                  = true
}

resource "azurerm_mssql_database" "sql-db" {
  #checkov:skip=CKV_AZURE_224:To be reviewed
  #checkov:skip=CKV_AZURE_229:To be reviewed for production
  name        = "ebis-data"
  server_id   = azurerm_mssql_server.sql-server.id
  tags        = local.common-tags
  sku_name    = "S0"
  max_size_gb = 5

  /*  threat_detection_policy {
    state                      = "Enabled"
    storage_endpoint           = azurerm_storage_account.threat-storage.primary_blob_endpoint
    storage_account_access_key = azurerm_storage_account.threat-storage.primary_access_key
    retention_days             = 120
  }*/
}

/*resource "azurerm_mssql_database_extended_auditing_policy" "db-audit-policy" {
  database_id                             = azurerm_mssql_database.sql-db.id
  storage_endpoint                        = azurerm_storage_account.audit-storage.primary_blob_endpoint
  storage_account_access_key              = azurerm_storage_account.audit-storage.primary_access_key
  storage_account_access_key_is_secondary = false
  retention_in_days                       = 120
  log_monitoring_enabled                  = true
}*/

resource "azurerm_mssql_firewall_rule" "sql-server-fw-dfe" {
  name             = "DFE_VPN"
  server_id        = azurerm_mssql_server.sql-server.id
  start_ip_address = "35.176.86.207"
  end_ip_address   = "35.176.86.207"
}

resource "azurerm_mssql_firewall_rule" "sql-server-fw-azure-services" {
  name             = "allow_azure_services_fw"
  server_id        = azurerm_mssql_server.sql-server.id
  start_ip_address = "0.0.0.0"
  end_ip_address   = "0.0.0.0"
}