data "azurerm_key_vault" "key-vault" {
  name                = "${var.environment-prefix}-ebis-keyvault"
  resource_group_name = "${var.environment-prefix}-ebis-core"
}

data "azurerm_application_insights" "application-insights" {
  name                = "${var.environment-prefix}-ebis-ai"
  resource_group_name = "${var.environment-prefix}-ebis-core"
}

data "azurerm_subnet" "web-app-subnet" {
  name                 = "${var.environment-prefix}-web-app-subnet"
  virtual_network_name = "${var.environment-prefix}-app-service-network"
  resource_group_name  = "${var.environment-prefix}-ebis-core"
}

data "azurerm_subnet" "platform-subnet" {
  name                 = "${var.environment-prefix}-platform-subnet"
  virtual_network_name = "${var.environment-prefix}-app-service-network"
  resource_group_name  = "${var.environment-prefix}-ebis-core"
}

data "azurerm_client_config" "client" {}

data "external" "agent_ip_address" {
  program = ["bash", "${path.module}/scripts/ip-address.sh"]
}

data "azurerm_key_vault_secret" "pipeline-message-hub-storage-connection-string" {
  name         = "data-storage-connection-string"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "core-sql-connection-string" {
  name         = "core-sql-connection-string"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_mssql_server" "sql-server" {
  name                = "${var.environment-prefix}-sql"
  resource_group_name = "${var.environment-prefix}-ebis-core"
}

data "azurerm_key_vault_secret" "sql-user-name" {
  name         = "core-sql-user-name"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "sql-password" {
  name         = "core-sql-password"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}