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

resource "azurerm_automation_schedule" "daily-schedule" {
  name                    = "${var.environment-prefix}-daily-schedule"
  resource_group_name     = azurerm_resource_group.resource-group.name
  automation_account_name = azurerm_automation_account.automation.name
  frequency               = "Day"
  interval                = 1

  # begin schedule from midnight tomorrow (UTC)
  start_time = formatdate("YYYY-MM-DD'T'00:00:00+00:00", timeadd(timestamp(), "24h"))
}

data "local_file" "backup-database-script" {
  filename = "${path.module}/scripts/backup-database.ps1"
}

resource "azurerm_automation_runbook" "backup-database-runbook" {
  name                    = "${var.environment-prefix}-backup-database"
  location                = azurerm_resource_group.resource-group.location
  resource_group_name     = azurerm_resource_group.resource-group.name
  automation_account_name = azurerm_automation_account.automation.name
  log_verbose             = "true"
  log_progress            = "true"
  description             = "Performs a backup of the given database to the specified blob storage container"
  runbook_type            = "PowerShell72"
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

data "local_file" "backup-container-script" {
  filename = "${path.module}/scripts/backup-container.ps1"
}

resource "azurerm_automation_runbook" "backup-raw-data-runbook" {
  name                    = "${var.environment-prefix}-backup-raw-data"
  location                = azurerm_resource_group.resource-group.location
  resource_group_name     = azurerm_resource_group.resource-group.name
  automation_account_name = azurerm_automation_account.automation.name
  log_verbose             = "true"
  log_progress            = "true"
  description             = "Performs a backup of the given container to the specified blob storage container"
  runbook_type            = "PowerShell72"
  tags                    = local.common-tags
  content                 = data.local_file.backup-container-script.content
}

resource "azurerm_automation_job_schedule" "backup-raw-data-runbook-daily-schedule" {
  resource_group_name     = azurerm_resource_group.resource-group.name
  automation_account_name = azurerm_automation_account.automation.name
  runbook_name            = azurerm_automation_runbook.backup-raw-data-runbook.name
  schedule_name           = azurerm_automation_schedule.daily-schedule.name

  # Due to a bug in the implementation of Runbooks in Azure, the parameter names need to be specified in lowercase only. 
  # https://github.com/Azure/azure-sdk-for-go/issues/4780
  parameters = {
    subscriptionname         = data.azurerm_subscription.current.display_name
    resourcegroup            = azurerm_resource_group.resource-group.name
    keyvaultname             = azurerm_key_vault.key-vault.name
    sourcestorageaccountname = azurerm_storage_account.data.name
    sourcestoragekeysecret   = azurerm_key_vault_secret.data-storage-key.name
    sourcecontainername      = azurerm_storage_container.pipeline-raw-data.name
    targetstorageaccountname = azurerm_storage_account.backup.name
    targetstoragekeysecret   = azurerm_key_vault_secret.backup-storage-key.name
    targetcontainername      = azurerm_storage_container.pipeline-raw-data-backup.name
    targetfoldertimestamp    = true
    expiryseconds            = 604800
  }
}

resource "azurerm_key_vault_access_policy" "automation-access-policy" {
  key_vault_id       = azurerm_key_vault.key-vault.id
  tenant_id          = azurerm_automation_account.automation.identity[0].tenant_id
  object_id          = azurerm_automation_account.automation.identity[0].principal_id
  secret_permissions = ["Get"]
}
