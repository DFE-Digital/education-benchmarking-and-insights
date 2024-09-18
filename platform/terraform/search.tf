resource "azurerm_search_service" "search" {
  #checkov:skip=CKV_AZURE_124:See ADO backlog AB#206514
  #checkov:skip=CKV_AZURE_208:See ADO backlog AB#206514
  #checkov:skip=CKV_AZURE_209:See ADO backlog AB#206514
  name                = "${var.environment-prefix}-ebis-search"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  sku                 = var.configuration[var.environment].search_sku
  tags                = local.common-tags
  replica_count       = var.configuration[var.environment].search_replica_count

  identity {
    type = "SystemAssigned"
  }
}

resource "azurerm_key_vault_secret" "platform-search-key" {
  #checkov:skip=CKV_AZURE_41:See ADO backlog AB#206511
  name         = "ebis-search-admin-key"
  value        = azurerm_search_service.search.primary_key
  key_vault_id = data.azurerm_key_vault.key-vault.id
  content_type = "key"
}

# This fails due to current user having insufficient permissions to assign roles to others:
# https://github.com/hashicorp/terraform-provider-azurerm/issues/495#issuecomment-341833320
# but this is a requirement as per the configuration instructions:
# https://learn.microsoft.com/en-us/azure/search/search-howto-managed-identities-sql
resource "azurerm_role_assignment" "search-db-role" {
  scope                = data.azurerm_mssql_server.sql-server.id
  role_definition_name = "Reader"
  principal_id         = azurerm_search_service.search.identity[0].principal_id
  principal_type       = "ServicePrincipal"
}

# ClientId rather than PrincipalId required for managed identity user in SQL database:
# https://github.com/betr-io/terraform-provider-mssql/issues/54#issuecomment-1632638595
data "azapi_resource" "search-service-identity" {
  name                   = "default"
  parent_id              = azurerm_search_service.search.id
  type                   = "Microsoft.ManagedIdentity/identities@2018-11-30"
  response_export_values = ["properties.clientId"]
}

resource "mssql_user" "search-service-user" {
  server {
    host = data.azurerm_mssql_server.sql-server.fully_qualified_domain_name
    login {
      username = data.azurerm_key_vault_secret.sql-user-name.value
      password = data.azurerm_key_vault_secret.sql-password.value
    }
  }

  database  = "data"
  username  = azurerm_search_service.search.name
  object_id = jsondecode(data.azapi_resource.search-service-identity.output).properties.clientId
  roles     = ["db_datareader"]
}
