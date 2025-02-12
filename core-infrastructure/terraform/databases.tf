resource "random_password" "sql-admin-password" {
  length           = 16
  special          = true
  override_special = "!@#$*()-_=+"
}

locals {
  sql-admin-login = "sql-admin"
}

resource "azurerm_key_vault_secret" "sql-connection-string" {
  #checkov:skip=CKV_AZURE_41:See ADO backlog AB#206511
  name         = "core-sql-connection-string"
  value        = "Server=tcp:${azurerm_mssql_server.sql-server.fully_qualified_domain_name},1433;Database=${azurerm_mssql_database.sql-db.name};User ID=${local.sql-admin-login};Password=${random_password.sql-admin-password.result};Trusted_Connection=False;Encrypt=True;"
  key_vault_id = azurerm_key_vault.key-vault.id
  content_type = "connection-string"
  depends_on   = [azurerm_key_vault_access_policy.terraform_sp_access]
}

resource "azurerm_key_vault_secret" "sql-connection-string-managed-identity" {
  #checkov:skip=CKV_AZURE_41:See ADO backlog AB#206511
  name         = "core-sql-connection-string-managed-identity"
  value        = "Server=tcp:${azurerm_mssql_server.sql-server.fully_qualified_domain_name},1433;Database=${azurerm_mssql_database.sql-db.name};Authentication=Active Directory Managed Identity;TrustServerCertificate=True;Encrypt=True;"
  key_vault_id = azurerm_key_vault.key-vault.id
  content_type = "connection-string"
  depends_on   = [azurerm_key_vault_access_policy.terraform_sp_access]
}

resource "azurerm_key_vault_secret" "sql-connection-string-default" {
  #checkov:skip=CKV_AZURE_41:See ADO backlog AB#206511
  name         = "core-sql-connection-string-default"
  value        = "Server=tcp:${azurerm_mssql_server.sql-server.fully_qualified_domain_name},1433;Database=${azurerm_mssql_database.sql-db.name};Authentication=Active Directory Default;TrustServerCertificate=True;Encrypt=True;"
  key_vault_id = azurerm_key_vault.key-vault.id
  content_type = "connection-string"
  depends_on   = [azurerm_key_vault_access_policy.terraform_sp_access]
}


resource "azurerm_key_vault_secret" "sql-domain-name" {
  #checkov:skip=CKV_AZURE_41:See ADO backlog AB#206511
  key_vault_id = azurerm_key_vault.key-vault.id
  name         = "core-sql-domain-name"
  value        = azurerm_mssql_server.sql-server.fully_qualified_domain_name
  content_type = "text/plain"
  depends_on   = [azurerm_key_vault_access_policy.terraform_sp_access]
}

resource "azurerm_key_vault_secret" "sql-db-name" {
  #checkov:skip=CKV_AZURE_41:See ADO backlog AB#206511
  key_vault_id = azurerm_key_vault.key-vault.id
  name         = "core-sql-db-name"
  value        = azurerm_mssql_database.sql-db.name
  content_type = "text/plain"
  depends_on   = [azurerm_key_vault_access_policy.terraform_sp_access]
}

resource "azurerm_key_vault_secret" "sql-user-name" {
  #checkov:skip=CKV_AZURE_41:See ADO backlog AB#206511
  key_vault_id = azurerm_key_vault.key-vault.id
  name         = "core-sql-user-name"
  value        = local.sql-admin-login
  content_type = "text/plain"
  depends_on   = [azurerm_key_vault_access_policy.terraform_sp_access]
}

resource "azurerm_key_vault_secret" "sql-password" {
  #checkov:skip=CKV_AZURE_41:See ADO backlog AB#206511
  key_vault_id = azurerm_key_vault.key-vault.id
  name         = "core-sql-password"
  value        = random_password.sql-admin-password.result
  content_type = "password"
  depends_on   = [azurerm_key_vault_access_policy.terraform_sp_access]
}

resource "azurerm_mssql_server" "sql-server" {
  #checkov:skip=CKV_AZURE_113:See ADO backlog AB#206493
  #checkov:skip=CKV2_AZURE_45:See ADO backlog AB#206493
  name                         = "${var.environment-prefix}-sql"
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

  identity {
    type = "SystemAssigned"
  }

  lifecycle {
    prevent_destroy = true
  }
}

resource "azurerm_mssql_server_extended_auditing_policy" "sql-server-audit-policy" {
  server_id              = azurerm_mssql_server.sql-server.id
  storage_endpoint       = azurerm_storage_account.sql-log-storage.primary_blob_endpoint
  retention_in_days      = 120
  log_monitoring_enabled = true
}

resource "azurerm_user_assigned_identity" "sql-db-admin" {
  name                = "sql-db-admin"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
}

resource "azurerm_mssql_database" "sql-db" {
  #checkov:skip=CKV_AZURE_224:See ADO backlog AB#206493
  #checkov:skip=CKV_AZURE_229:See ADO backlog AB#206493
  name        = "data"
  server_id   = azurerm_mssql_server.sql-server.id
  tags        = local.common-tags
  sku_name    = var.configuration[var.environment].sql_db_sku_name
  max_size_gb = var.configuration[var.environment].sql_db_max_size_gb

  threat_detection_policy {
    state                      = "Enabled"
    storage_endpoint           = azurerm_storage_account.sql-log-storage.primary_blob_endpoint
    storage_account_access_key = azurerm_storage_account.sql-log-storage.primary_access_key
    retention_days             = 120
  }

  short_term_retention_policy {
    backup_interval_in_hours = 24
    retention_days           = 7
  }

  long_term_retention_policy {
    weekly_retention = "P52W"
  }

  lifecycle {
    prevent_destroy = true
  }
}

resource "azurerm_mssql_database_extended_auditing_policy" "db-audit-policy" {
  database_id                             = azurerm_mssql_database.sql-db.id
  storage_endpoint                        = azurerm_storage_account.sql-log-storage.primary_blob_endpoint
  storage_account_access_key              = azurerm_storage_account.sql-log-storage.primary_access_key
  storage_account_access_key_is_secondary = false
  retention_in_days                       = 120
  log_monitoring_enabled                  = true
}

resource "azurerm_mssql_firewall_rule" "sql-server-fw-azure-services" {
  #checkov:skip=CKV2_AZURE_34:See ADO backlog AB#206493
  name             = "allow_azure_services_fw"
  server_id        = azurerm_mssql_server.sql-server.id
  start_ip_address = "0.0.0.0"
  end_ip_address   = "0.0.0.0"
}

resource "azurerm_mssql_firewall_rule" "sql-server-fw-dfe-remote" {
  count            = var.environment == "production" ? 0 : 1
  name             = "DFE_VPN_Remote"
  server_id        = azurerm_mssql_server.sql-server.id
  start_ip_address = "208.127.46.236"
  end_ip_address   = "208.127.46.255"
}

resource "azurerm_mssql_server_security_alert_policy" "sql-security-alert-policy" {
  resource_group_name  = azurerm_resource_group.resource-group.name
  server_name          = azurerm_mssql_server.sql-server.name
  state                = "Enabled"
  email_addresses      = [var.support-alert-email]
  email_account_admins = true
}

resource "azurerm_mssql_server_vulnerability_assessment" "sql-server-vulnerability" {
  server_security_alert_policy_id = azurerm_mssql_server_security_alert_policy.sql-security-alert-policy.id
  storage_container_path          = "${azurerm_storage_account.sql-log-storage.primary_blob_endpoint}${azurerm_storage_container.sql-vulnerability-container.name}/"
  storage_account_access_key      = azurerm_storage_account.sql-log-storage.primary_access_key

  recurring_scans {
    enabled                   = true
    emails                    = [var.support-alert-email]
    email_subscription_admins = true
  }
}

resource "azurerm_role_assignment" "sql-log-storage-role-blob" {
  scope                = azurerm_storage_account.sql-log-storage.id
  role_definition_name = "Storage Blob Data Contributor"
  principal_id         = azurerm_mssql_server.sql-server.identity[0].principal_id
  principal_type       = "ServicePrincipal"
}

resource "azurerm_role_assignment" "sql-db-admin-log-storage-role-blob" {
  scope                = azurerm_storage_account.sql-log-storage.id
  role_definition_name = "Storage Blob Data Contributor"
  principal_id         = azurerm_user_assigned_identity.sql-db-admin.principal_id
  principal_type       = "ServicePrincipal"
}

resource "azurerm_storage_account" "sql-log-storage" {
  #checkov:skip=CKV_AZURE_43:False positive on storage account adhering to the naming rules
  #checkov:skip=CKV2_AZURE_33:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_1:See ADO backlog AB#206389
  #checkov:skip=CKV_AZURE_59:See ADO backlog AB#206389
  #checkov:skip=CKV2_AZURE_50:potential false positive https://github.com/bridgecrewio/checkov/issues/6388
  #checkov:skip=CKV2_AZURE_40:See ADO backlog AB#222562:Managed identity is not supported by `threat_detection_policy`, so SAS must be used instead
  name                            = "${var.environment-prefix}sqllog"
  location                        = azurerm_resource_group.resource-group.location
  resource_group_name             = azurerm_resource_group.resource-group.name
  account_tier                    = "Standard"
  account_replication_type        = "GRS"
  allow_nested_items_to_be_public = false
  tags                            = local.common-tags
  min_tls_version                 = "TLS1_2"
  shared_access_key_enabled       = true
  local_user_enabled              = false

  blob_properties {
    delete_retention_policy {
      days = 7
    }
    container_delete_retention_policy {
      days = 7
    }
    versioning_enabled = true
  }

  queue_properties {
    logging {
      delete                = true
      read                  = true
      write                 = true
      version               = "1.0"
      retention_policy_days = 10
    }
  }

  sas_policy {
    expiration_action = "Log"
    expiration_period = "30.00:00:00"
  }
}

resource "azurerm_storage_container" "sql-vulnerability-container" {
  #checkov:skip=CKV2_AZURE_21:See ADO backlog AB#206507
  name                  = "vulnerability-assessment"
  storage_account_name  = azurerm_storage_account.sql-log-storage.name
  container_access_type = "private"
}

# https://learn.microsoft.com/en-us/rest/api/sql/server-automatic-tuning/update?view=rest-sql-2021-11-01
resource "azapi_resource_action" "sql-server-auto-tuning" {
  count       = lower(var.cip-environment) == "dev" ? 1 : 0
  resource_id = "${azurerm_mssql_server.sql-server.id}/automaticTuning/current"
  type        = "Microsoft.Sql/servers/automaticTuning@2021-11-01"
  method      = "PATCH"
  body = {
    properties = {
      desiredState = "Auto"
      options = {
        # Valid desiredState options = "Default","On","Off"
        forceLastGoodPlan = { desiredState = "Default" }
        createIndex       = { desiredState = "On" }
        dropIndex         = { desiredState = "On" }
      }
    }
  }
  depends_on = [azurerm_mssql_database.sql-db]
}

resource "mssql_user" "sp-user" {
  server {
    host = azurerm_mssql_server.sql-server.fully_qualified_domain_name
    login {
      username = local.sql-admin-login
      password = random_password.sql-admin-password.result
    }
  }

  database  = azurerm_mssql_database.sql-db.name
  username  = data.azuread_service_principal.sp.display_name
  object_id = data.azurerm_client_config.client.client_id
  roles     = ["db_owner"]
}
