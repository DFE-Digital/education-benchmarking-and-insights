# name can only consist of lowercase letters and numbers, and must be between 3 and 24 characters long
locals {
  platform-storage-name = "${lower(substr(replace(var.environment-prefix, "/[^\\w]/", ""), 0, 24 - length("platformstorage")))}platformstorage"
  audit-storage-name    = "${lower(substr(replace(var.environment-prefix, "/[^\\w]/", ""), 0, 24 - length("audit")))}audit"
  threat-storage-name   = "${lower(substr(replace(var.environment-prefix, "/[^\\w]/", ""), 0, 24 - length("threat")))}threat"
}

resource "azurerm_storage_account" "platform-storage" {
  #checkov:skip=CKV_AZURE_206:Only LRS required
  #checkov:skip=CKV_AZURE_43:Name needs to include prefix
  #checkov:skip=CKV_AZURE_33:Storage queues not used
  #checkov:skip=CKV2_AZURE_1:To be reviewed
  #checkov:skip=CKV2_AZURE_33:To be reviewed
  #checkov:skip=CKV2_AZURE_40:To be reviewed
  #checkov:skip=CKV2_AZURE_41:To be reviewed
  name                            = local.platform-storage-name
  location                        = azurerm_resource_group.resource-group.location
  resource_group_name             = azurerm_resource_group.resource-group.name
  account_tier                    = "Standard"
  account_replication_type        = "LRS"
  allow_nested_items_to_be_public = false
  tags                            = local.common-tags
  min_tls_version                 = "TLS1_2"
  public_network_access_enabled   = false

  blob_properties {
    delete_retention_policy {
      days = 7
    }
  }

  sas_policy {
    expiration_action = "Log"
    expiration_period = "90.00:00:00"
  }
}

resource "azurerm_storage_account_network_rules" "platform-network-rules" {
  storage_account_id = azurerm_storage_account.platform-storage.id

  default_action = "Deny"

  private_link_access {
    endpoint_resource_id = module.establishment-fa.function-resource-id
  }

  private_link_access {
    endpoint_resource_id = module.insight-fa.function-resource-id
  }

  private_link_access {
    endpoint_resource_id = module.benchmark-fa.function-resource-id
  }
}

resource "azurerm_storage_container" "local-authorities-container" {
  #checkov:skip=CKV2_AZURE_21:To be reviewed
  name                 = "local-authorities"
  storage_account_name = azurerm_storage_account.platform-storage.name
}

resource "azurerm_key_vault_secret" "platform-storage-connection-string" {
  #checkov:skip=CKV_AZURE_41:Secrets expiration to be reviewed
  name         = "platform-storage-connection-string"
  value        = azurerm_storage_account.platform-storage.primary_connection_string
  key_vault_id = data.azurerm_key_vault.key-vault.id
  content_type = "connection-string"
}

resource "azurerm_storage_account" "audit-storage" {
  #checkov:skip=CKV_AZURE_206:Only LRS required
  #checkov:skip=CKV_AZURE_43:Name needs to include prefix
  #checkov:skip=CKV_AZURE_33:Storage queues not used
  #checkov:skip=CKV2_AZURE_1:To be reviewed
  #checkov:skip=CKV2_AZURE_40:To be reviewed
  #checkov:skip=CKV2_AZURE_41:To be reviewed
  #checkov:skip=CKV2_AZURE_33:To be reviewed
  name                            = local.audit-storage-name
  location                        = azurerm_resource_group.resource-group.location
  resource_group_name             = azurerm_resource_group.resource-group.name
  account_tier                    = "Standard"
  account_replication_type        = "LRS"
  allow_nested_items_to_be_public = false
  tags                            = local.common-tags
  min_tls_version                 = "TLS1_2"
  public_network_access_enabled   = false

  blob_properties {
    delete_retention_policy {
      days = 7
    }
  }
  sas_policy {
    expiration_action = "Log"
    expiration_period = "90.00:00:00"
  }
}

resource "azurerm_storage_account_network_rules" "audit-network-rules" {
  storage_account_id = azurerm_storage_account.audit-storage.id

  default_action = "Deny"

  private_link_access {
    endpoint_resource_id = azurerm_mssql_database.sql-db.id
  }

  private_link_access {
    endpoint_resource_id = azurerm_mssql_server.sql-server.id
  }
}

resource "azurerm_storage_account" "threat-storage" {
  #checkov:skip=CKV_AZURE_206:Only LRS required
  #checkov:skip=CKV_AZURE_43:Name needs to include prefix
  #checkov:skip=CKV_AZURE_33:Storage queues not used
  #checkov:skip=CKV2_AZURE_1:To be reviewed
  #checkov:skip=CKV2_AZURE_40:Terraform uses Shared Key Authorisation
  #checkov:skip=CKV2_AZURE_41:To be reviewed
  #checkov:skip=CKV2_AZURE_33:To be reviewed
  name                            = local.threat-storage-name
  location                        = azurerm_resource_group.resource-group.location
  resource_group_name             = azurerm_resource_group.resource-group.name
  account_tier                    = "Standard"
  account_replication_type        = "LRS"
  allow_nested_items_to_be_public = false
  tags                            = local.common-tags
  min_tls_version                 = "TLS1_2"
  public_network_access_enabled   = false

  blob_properties {
    delete_retention_policy {
      days = 7
    }
  }

  sas_policy {
    expiration_action = "Log"
    expiration_period = "90.00:00:00"
  }
}

resource "azurerm_storage_account_network_rules" "threat-network-rules" {
  storage_account_id = azurerm_storage_account.threat-storage.id

  default_action = "Deny"

  private_link_access {
    endpoint_resource_id = azurerm_mssql_database.sql-db.id
  }

  private_link_access {
    endpoint_resource_id = azurerm_mssql_server.sql-server.id
  }
}
