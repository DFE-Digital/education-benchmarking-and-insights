resource "azurerm_cosmosdb_account" "cosmosdb-account" {
  #checkov:skip=CKV_AZURE_100:See ADO backlog AB#206519
  #checkov:skip=CKV_AZURE_101:See ADO backlog AB#206519
  #checkov:skip=CKV_AZURE_132:See ADO backlog AB#206519
  #checkov:skip=CKV_AZURE_140:See ADO backlog AB#206519
  #checkov:skip=CKV_AZURE_99:See ADO backlog AB#206519
  name                = "${var.environment-prefix}-ebis-cdb"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  offer_type          = "Standard"
  kind                = "GlobalDocumentDB"
  tags                = local.common-tags

  # Azure services, plus DFE VPN remote range if non-production
  ip_range_filter = var.environment == "production" ? "0.0.0.0" : "0.0.0.0,208.127.46.236/30,208.127.46.240/29,208.127.46.248/31,208.127.46.250/32"

  consistency_policy {
    consistency_level = "Session"
  }

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

  lifecycle {
    ignore_changes = [
      ip_range_filter
    ]
  }
}

resource "azurerm_key_vault_secret" "platform-cosmos-read-connection-string" {
  #checkov:skip=CKV_AZURE_41:See ADO backlog AB#206511
  name         = "ebis-cdb-connection-string-r"
  value        = azurerm_cosmosdb_account.cosmosdb-account.primary_readonly_sql_connection_string
  key_vault_id = data.azurerm_key_vault.key-vault.id
  content_type = "connection-string"
}

resource "azurerm_key_vault_secret" "platform-cosmos-readwrite-connection-string" {
  #checkov:skip=CKV_AZURE_41:See ADO backlog AB#206511
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
  #checkov:skip=CKV_AZURE_41:See ADO backlog AB#206511
  name         = "ebis-sql-connection-string"
  value        = "Server=tcp:${azurerm_mssql_server.sql-server.fully_qualified_domain_name},1433;Database=${azurerm_mssql_database.sql-db.name};User ID=${local.sql-admin-login};Password=${random_password.sql-admin-password.result};Trusted_Connection=False;Encrypt=True;"
  key_vault_id = data.azurerm_key_vault.key-vault.id
  content_type = "connection-string"
}

resource "azurerm_mssql_server" "sql-server" {
  #checkov:skip=CKV_AZURE_113:See ADO backlog AB#206493
  #checkov:skip=CKV2_AZURE_45:See ADO backlog AB#206493
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
  #checkov:skip=CKV_AZURE_224:See ADO backlog AB#206493
  #checkov:skip=CKV_AZURE_229:See ADO backlog AB#206493
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

resource "azurerm_mssql_firewall_rule" "sql-server-fw-dfe-remote" {
  count            = var.environment == "production" ? 0 : 1
  name             = "DFE_VPN_Remote"
  server_id        = azurerm_mssql_server.sql-server.id
  start_ip_address = "208.127.46.236"
  end_ip_address   = "208.127.46.250"
}

resource "azurerm_mssql_firewall_rule" "sql-server-fw-azure-services" {
  name             = "allow_azure_services_fw"
  server_id        = azurerm_mssql_server.sql-server.id
  start_ip_address = "0.0.0.0"
  end_ip_address   = "0.0.0.0"
}

resource "azurerm_mssql_virtual_network_rule" "sql-server-vnet-rule" {
  name      = "sql-vnet-rule"
  server_id = azurerm_mssql_server.sql-server.id
  subnet_id = data.azurerm_subnet.platform-subnet.id
}

resource "azurerm_mssql_server_security_alert_policy" "sql-security-alert-policy" {
  #checkov:skip=CKV_AZURE_26:See ADO backlog AB#206493
  #checkov:skip=CKV_AZURE_27:See ADO backlog AB#206493
  resource_group_name = azurerm_resource_group.resource-group.name
  server_name         = azurerm_mssql_server.sql-server.name
  state               = "Enabled"
}

resource "azurerm_mssql_server_vulnerability_assessment" "sql-server-vulnerability" {
  #checkov:skip=CKV2_AZURE_4:See ADO backlog AB#206493
  #checkov:skip=CKV2_AZURE_5:See ADO backlog AB#206493
  server_security_alert_policy_id = azurerm_mssql_server_security_alert_policy.sql-security-alert-policy.id
  storage_container_path          = "${azurerm_storage_account.vulnerability-storage.primary_blob_endpoint}${azurerm_storage_container.vulnerability-container.name}/"
  storage_account_access_key      = azurerm_storage_account.vulnerability-storage.primary_access_key

  recurring_scans {
    enabled = true
  }
}