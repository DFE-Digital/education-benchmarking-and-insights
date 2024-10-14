resource "azurerm_automation_account" "automation" {
  name                = "${var.environment-prefix}-ebis-automation"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  tags                = local.common-tags
  sku_name            = "Basic"

  identity {
    type = "SystemAssigned"
  }

  public_network_access_enabled = var.environment == "development" ? true : false
}

data "local_file" "backup-database-script" {
  filename = "${path.module}/scripts/backup-database.ps1"
}

resource "azurerm_automation_schedule" "daily-schedule" {
  name                    = "${var.environment-prefix}-daily-schedule"
  resource_group_name     = azurerm_resource_group.resource-group.name
  automation_account_name = azurerm_automation_account.automation.name
  frequency               = "Day"
  interval                = 1

  # begin schedule from midnight tomorrow (UTC)
  start_time = formatdate("YYYY-MM-DD'T'00:00:00+00:00", timeadd(timestamp(), "24h"))
}

resource "azurerm_automation_runbook" "backup-database-runbook" {
  name                    = "${var.environment-prefix}-backup-database"
  location                = azurerm_resource_group.resource-group.location
  resource_group_name     = azurerm_resource_group.resource-group.name
  automation_account_name = azurerm_automation_account.automation.name
  log_verbose             = "true"
  log_progress            = "true"
  description             = "Performs a backup of the given database to the specified blob storage container"
  runbook_type            = "PowerShell"
  tags                    = local.common-tags
  content                 = data.local_file.backup-database-script.content
}

resource "azurerm_automation_job_schedule" "backup-database-runbook-daily-schedule" {
  resource_group_name     = azurerm_resource_group.resource-group.name
  automation_account_name = azurerm_automation_account.automation.name
  runbook_name            = azurerm_automation_runbook.backup-database-runbook.name
  schedule_name           = azurerm_automation_schedule.daily-schedule.name

  # Due to a bug in the implementation of Runbooks in Azure, the parameter names need to be specified in lowercase only. 
  # https://github.com/Azure/azure-sdk-for-go/issues/4780
  parameters = {
    subscriptionname       = data.azurerm_subscription.current.display_name
    resourcegroup          = azurerm_resource_group.resource-group.name
    keyvaultname           = azurerm_key_vault.key-vault.name
    servername             = azurerm_mssql_server.sql-server.name
    databasename           = azurerm_mssql_database.sql-db.name
    databaseusernamesecret = azurerm_key_vault_secret.sql-user-name.name
    databasepasswordsecret = azurerm_key_vault_secret.sql-password.name
    storageaccountname     = azurerm_storage_account.backup.name
    storagekeysecret       = azurerm_key_vault_secret.backup-storage-key.name
    containername          = azurerm_storage_container.pipeline-database-backup.name
    expiryseconds          = 604800
  }
}

resource "azurerm_key_vault_access_policy" "automation-access-policy" {
  key_vault_id       = azurerm_key_vault.key-vault.id
  tenant_id          = azurerm_automation_account.automation.identity[0].tenant_id
  object_id          = azurerm_automation_account.automation.identity[0].principal_id
  secret_permissions = ["Get"]
}
