locals {
  common-tags = {
    "Environment"      = var.cip-environment
    "Service Offering" = "DfE Financial Benchmarking service"
    "Product"          = "DfE Financial Benchmarking service"
    "Source"           = "terraform"
  }
}

data "azurerm_application_insights" "application-insights" {
  name                = "${var.environment-prefix}-ebis-ai"
  resource_group_name = "${var.environment-prefix}-ebis-core"
}

data "azurerm_key_vault" "key-vault" {
  name                = "${var.environment-prefix}-ebis-keyvault"
  resource_group_name = "${var.environment-prefix}-ebis-core"
}

data "azurerm_key_vault_secret" "insight-api-key" {
  name         = "insight-host-key"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "insight-api-host" {
  name         = "insight-host"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "benchmark-api-key" {
  name         = "benchmark-host-key"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "benchmark-api-host" {
  name         = "benchmark-host"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "establishment-api-host" {
  name         = "establishment-host"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

data "azurerm_key_vault_secret" "establishment-api-key" {
  name         = "establishment-host-key"
  key_vault_id = data.azurerm_key_vault.key-vault.id
}

resource "azurerm_resource_group" "resource-group" {
  name     = "${var.environment-prefix}-ebis-web"
  location = var.location
  tags     = local.common-tags
}

resource "azurerm_service_plan" "education-benchmarking-asp" {
  name                = "${var.environment-prefix}-education-benchmarking-asp"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  os_type             = "Windows"
  sku_name            = var.sizing[var.environment].sku
  tags                = local.common-tags
}

resource "azurerm_windows_web_app" "education-benchmarking-as" {
  name                    = "${var.environment-prefix}-education-benchmarking"
  location                = azurerm_resource_group.resource-group.location
  resource_group_name     = azurerm_resource_group.resource-group.name
  service_plan_id         = azurerm_service_plan.education-benchmarking-asp.id
  client_affinity_enabled = false
  https_only              = true

  site_config {
    http2_enabled = true
    application_stack {
      dotnet_version = "v8.0"
    }
    use_32_bit_worker_process = false
  }

  app_settings = {
    "APPINSIGHTS_INSTRUMENTATIONKEY"          = data.azurerm_application_insights.application-insights.instrumentation_key
    "APPINSIGHTS_CONNECTIONSTRING"            = data.azurerm_application_insights.application-insights.connection_string
    "Apis:Insight:Url"                        = data.azurerm_key_vault_secret.insight-api-host.value
    "Apis:Insight:Key"                        = data.azurerm_key_vault_secret.insight-api-key.value
    "Apis:Establishment:Url"                  = data.azurerm_key_vault_secret.establishment-api-host.value
    "Apis:Establishment:Key"                  = data.azurerm_key_vault_secret.establishment-api-key.value
    "Apis:Benchmark:Url"                      = data.azurerm_key_vault_secret.benchmark-api-host.value
    "Apis:Benchmark:Key"                      = data.azurerm_key_vault_secret.benchmark-api-key.value
    "DFESignInSettings:APISecret"             = var.dfe-signin.api-secret
    "DFESignInSettings:APIUri"                = var.dfe-signin.api-uri
    "DFESignInSettings:Audience"              = var.dfe-signin.audience
    "DFESignInSettings:CallbackPath"          = var.dfe-signin.callback-path
    "DFESignInSettings:ClientID"              = var.dfe-signin.client-id
    "DFESignInSettings:ClientSecret"          = var.dfe-signin.client-secret
    "DFESignInSettings:Issuer"                = var.dfe-signin.issuer
    "DFESignInSettings:MetadataAddress"       = var.dfe-signin.metadata-address
    "DFESignInSettings:SignedOutCallbackPath" = var.dfe-signin.signed-out-callback-path
    "DFESignInSettings:SignOutUri"            = var.dfe-signin.sign-out-uri
  }
  tags = local.common-tags
}