locals {
  common-tags = {
    "Environment"      = var.cip-environment
    "Service Offering" = "DfE Financial Benchmarking service"
    "Product"          = "DfE Financial Benchmarking service"
    "Source"           = "terraform"
  }
}

resource "azurerm_resource_group" "resource-group" {
  name     = "${var.environment-prefix}-ebis-perf-tests"
  location = var.location
  tags     = local.common-tags
}

resource "azurerm_load_test" "load-test" {
  name                = "${var.environment-prefix}-load-tests"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  tags                = local.common-tags

  identity {
    type = "SystemAssigned"
  }
}

resource "azurerm_key_vault_access_policy" "keyvault_load_test_policy" {
  key_vault_id       = data.azurerm_key_vault.key-vault.id
  tenant_id          = azurerm_load_test.load-test.identity[0].tenant_id
  object_id          = azurerm_load_test.load-test.identity[0].principal_id
  secret_permissions = ["Get"]
}