resource "azurerm_cosmosdb_account" "session-cache-account" {
  #checkov:skip=CKV_AZURE_100:See ADO backlog AB#206519
  #checkov:skip=CKV_AZURE_101:See ADO backlog AB#206519
  #checkov:skip=CKV_AZURE_132:See ADO backlog AB#206519
  #checkov:skip=CKV_AZURE_140:See ADO backlog AB#206519
  #checkov:skip=CKV_AZURE_99:See ADO backlog AB#206519
  name                              = "${var.environment-prefix}-ebis-session"
  location                          = azurerm_resource_group.resource-group.location
  resource_group_name               = azurerm_resource_group.resource-group.name
  offer_type                        = "Standard"
  kind                              = "GlobalDocumentDB"
  is_virtual_network_filter_enabled = true

  consistency_policy {
    consistency_level = "Strong"
  }

  tags = local.common-tags
  geo_location {
    failover_priority = 0
    location          = azurerm_resource_group.resource-group.location
  }

  virtual_network_rule {
    id = data.azurerm_subnet.web-app-subnet.id
  }

  capabilities {
    name = "EnableServerless"
  }
}

resource "azurerm_cosmosdb_sql_database" "session-cache-database" {
  name                = "session-data"
  account_name        = azurerm_cosmosdb_account.session-cache-account.name
  resource_group_name = azurerm_resource_group.resource-group.name
}

resource "azurerm_cosmosdb_sql_container" "session-cache-container" {
  name                  = "sessions"
  resource_group_name   = azurerm_resource_group.resource-group.name
  account_name          = azurerm_cosmosdb_account.session-cache-account.name
  database_name         = azurerm_cosmosdb_sql_database.session-cache-database.name
  partition_key_paths   = ["/id"]
  partition_key_version = 1
}

resource "azurerm_cosmosdb_sql_role_assignment" "app-service-cache" {
  resource_group_name = azurerm_resource_group.resource-group.name
  account_name        = azurerm_cosmosdb_account.session-cache-account.name
  scope               = azurerm_cosmosdb_account.session-cache-account.id
  principal_id        = azurerm_windows_web_app.education-benchmarking-as.identity[0].principal_id

  # see https://learn.microsoft.com/en-us/azure/cosmos-db/how-to-setup-rbac#built-in-role-definitions
  role_definition_id = "${azurerm_cosmosdb_account.session-cache-account.id}/sqlRoleDefinitions/00000000-0000-0000-0000-000000000002"
}
