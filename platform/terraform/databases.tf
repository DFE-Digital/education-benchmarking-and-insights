resource "azurerm_cosmosdb_account" "cosmosdb-account" {
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

resource "azurerm_key_vault_secret" "platform-cosmos-connection-string" {
  #checkov:skip=CKV_AZURE_41:Secrets expiration to be reviewed
  name         = "ebis-cdb-connection-string"
  value        = azurerm_cosmosdb_account.cosmosdb-account.primary_readonly_sql_connection_string
  key_vault_id = data.azurerm_key_vault.key-vault.id
  content_type = "connection-string"
}

resource "azurerm_cosmosdb_sql_database" "cosmosdb-container" {
  name                = "ebis-data"
  account_name        = azurerm_cosmosdb_account.cosmosdb-account.name
  resource_group_name = azurerm_resource_group.resource-group.name
}

resource "random_password" "sql-admin-password" {
  length           = 16
  special          = true
  override_special = "!@#$*()-_=+"
}

resource "azurerm_key_vault_secret" "platform-sql-admin-username" {
  #checkov:skip=CKV_AZURE_41:Secrets expiration to be reviewed
  name         = "ebis-sql-admin-username"
  value        = "ebis-sql-admin"
  key_vault_id = data.azurerm_key_vault.key-vault.id
  content_type = "username"
}

resource "azurerm_key_vault_secret" "platform-sql-admin-password" {
  #checkov:skip=CKV_AZURE_41:Secrets expiration to be reviewed
  name         = "ebis-sql-admin-password"
  value        = random_password.sql-admin-password.result
  key_vault_id = data.azurerm_key_vault.key-vault.id
  content_type = "password"
}

resource "azurerm_key_vault_secret" "platform-sql-server" {
  #checkov:skip=CKV_AZURE_41:Secrets expiration to be reviewed
  name         = "ebis-sql-server"
  value        = azurerm_mssql_server.sql-server.fully_qualified_domain_name
  key_vault_id = data.azurerm_key_vault.key-vault.id
  content_type = "server"
}

resource "azurerm_key_vault_secret" "platform-sql-db-name" {
  #checkov:skip=CKV_AZURE_41:Secrets expiration to be reviewed
  name         = "ebis-sql-db-data"
  value        = azurerm_mssql_database.sql-db.name
  key_vault_id = data.azurerm_key_vault.key-vault.id
  content_type = "db-name"
}

resource "azurerm_mssql_server" "sql-server" {
  #checkov:skip=CKV_AZURE_113:Public access required for Azure Devops build agents
  name                         = "${var.environment-prefix}-ebis-sql"
  version                      = "12.0"
  resource_group_name          = azurerm_resource_group.resource-group.name
  location                     = azurerm_resource_group.resource-group.location
  administrator_login          = azurerm_key_vault_secret.platform-sql-admin-username.value
  administrator_login_password = azurerm_key_vault_secret.platform-sql-admin-password.value
  tags                         = local.common-tags

  /*
  Needs to be set
  azuread_administrator {
    login_username = "AzureAD Admin"
    object_id      = "00000000-0000-0000-0000-000000000000"
  }*/
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
  name        = "ebis-data"
  server_id   = azurerm_mssql_server.sql-server.id
  tags        = local.common-tags
  sku_name    = "S0"
  max_size_gb = 5

  threat_detection_policy {
    state                      = "Enabled"
    storage_endpoint           = azurerm_storage_account.threat-storage.primary_blob_endpoint
    storage_account_access_key = azurerm_storage_account.threat-storage.primary_access_key
    retention_days             = 120
  }
}

resource "azurerm_mssql_database_extended_auditing_policy" "db-audit-policy" {
  database_id                             = azurerm_mssql_database.sql-db.id
  storage_endpoint                        = azurerm_storage_account.audit-storage.primary_blob_endpoint
  storage_account_access_key              = azurerm_storage_account.audit-storage.primary_access_key
  storage_account_access_key_is_secondary = false
  retention_in_days                       = 120
  log_monitoring_enabled                  = true
}

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