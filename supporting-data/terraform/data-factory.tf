resource "azurerm_data_factory" "supporting-data" {
  #checkov:skip=CKV_AZURE_103:See ADO backlog AB#207729
  #checkov:skip=CKV_AZURE_104:See ADO backlog AB#207946
  name                = "${var.environment-prefix}-ebis-adf"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  tags                = local.common-tags

  public_network_enabled = true

  identity {
    type = "SystemAssigned"
  }

  # You must log in to the Data Factory management UI to complete the authentication to the GitHub repository.
  github_configuration {
    account_name       = "dfe-digital"
    branch_name        = "adf"
    git_url            = "https://github.com"
    publishing_enabled = true
    repository_name    = "education-benchmarking-and-insights"
    root_folder        = "/supporting-data/adf"
  }
}

resource "azurerm_data_factory_linked_service_key_vault" "supporting-data" {
  name            = "${var.environment-prefix}-ebis-adf-kv"
  data_factory_id = azurerm_data_factory.supporting-data.id
  key_vault_id    = data.azurerm_key_vault.key-vault.id
}

resource "azurerm_data_factory_linked_service_azure_blob_storage" "supporting-data" {
  name            = "${var.environment-prefix}-ebis-adf-storage"
  data_factory_id = azurerm_data_factory.supporting-data.id
  sas_uri         = "${azurerm_storage_account.supporting-data.primary_blob_endpoint}${azurerm_storage_container.raw-data.name}"

  key_vault_sas_token {
    linked_service_name = azurerm_data_factory_linked_service_key_vault.supporting-data.name
    secret_name         = azurerm_key_vault_secret.supporting-data-storage-sas-token.name
  }
}

resource "azurerm_data_factory_linked_service_cosmosdb" "supporting-data" {
  name              = "${var.environment-prefix}-ebis-adf-cosmos"
  data_factory_id   = azurerm_data_factory.supporting-data.id
  connection_string = azurerm_key_vault_secret.supporting-data-cosmos-connection-string.value
}

resource "azurerm_data_factory_linked_service_azure_sql_database" "supporting-data" {
  name            = "${var.environment-prefix}-ebis-adf-sql"
  data_factory_id = azurerm_data_factory.supporting-data.id

  key_vault_connection_string {
    linked_service_name = azurerm_data_factory_linked_service_key_vault.supporting-data.name
    secret_name         = data.azurerm_key_vault_secret.platform-sql-connection-string.name
  }
}