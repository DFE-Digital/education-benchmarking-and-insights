resource "azurerm_service_plan" "education-benchmarking-asp" {
  name                   = "${var.environment-prefix}-education-benchmarking-asp"
  location               = azurerm_resource_group.resource-group.location
  resource_group_name    = azurerm_resource_group.resource-group.name
  os_type                = "Windows"
  sku_name               = var.configuration[var.environment].sku_name
  zone_balancing_enabled = var.configuration[var.environment].zone_balancing_enabled
  worker_count           = var.configuration[var.environment].worker_count
  tags                   = local.common-tags
}

resource "azurerm_windows_web_app" "education-benchmarking-as" {
  #checkov:skip=CKV_AZURE_13:Authentication is handled via DSI
  #checkov:skip=CKV_AZURE_88:Persistent storage not required
  #checkov:skip=CKV_AZURE_17:Client cert no used
  #checkov:skip=CKV_AZURE_222:Web app is public site
  name                    = "${var.environment-prefix}-education-benchmarking"
  location                = azurerm_resource_group.resource-group.location
  resource_group_name     = azurerm_resource_group.resource-group.name
  service_plan_id         = azurerm_service_plan.education-benchmarking-asp.id
  client_affinity_enabled = false
  https_only              = true

  identity {
    type = "SystemAssigned"
  }

  site_config {
    ip_restriction_default_action = "Deny"
    http2_enabled                 = true
    vnet_route_all_enabled        = true
    application_stack {
      current_stack  = "dotnet"
      dotnet_version = "v8.0"
    }
    use_32_bit_worker                 = false
    ftps_state                        = "Disabled"
    health_check_path                 = "/health"
    health_check_eviction_time_in_min = 10

    ip_restriction {
      name        = "Access from Front Door"
      service_tag = "AzureFrontDoor.Backend"
      action      = "Allow"
      priority    = 100
      headers {
        x_azure_fdid = [azurerm_cdn_frontdoor_profile.web-app-front-door-profile.resource_guid]
      }
    }
  }

  virtual_network_subnet_id = data.azurerm_subnet.web-app-subnet.id

  logs {
    failed_request_tracing  = true
    detailed_error_messages = true
    http_logs {
      file_system {
        retention_in_days = 0
        retention_in_mb   = 25
      }
    }
  }

  app_settings = {
    "ASPNETCORE_ENVIRONMENT"                                  = "Production"
    "APPINSIGHTS_INSTRUMENTATIONKEY"                          = data.azurerm_application_insights.application-insights.instrumentation_key
    "APPLICATIONINSIGHTS_CONNECTION_STRING"                   = data.azurerm_application_insights.application-insights.connection_string
    "FeatureManagement__CurriculumFinancialPlanning"          = var.configuration[var.environment].features.CurriculumFinancialPlanning
    "FeatureManagement__CustomData"                           = var.configuration[var.environment].features.CustomData
    "FeatureManagement__Trusts"                               = var.configuration[var.environment].features.Trusts
    "FeatureManagement__LocalAuthorities"                     = var.configuration[var.environment].features.LocalAuthorities
    "FeatureManagement__UserDefinedComparators"               = var.configuration[var.environment].features.UserDefinedComparators
    "FeatureManagement__DisableOrganisationClaimCheck"        = var.configuration[var.environment].features.DisableOrganisationClaimCheck
    "FeatureManagement__ForecastRisk"                         = var.configuration[var.environment].features.ForecastRisk
    "FeatureManagement__TrustComparison"                      = var.configuration[var.environment].features.TrustComparison
    "FeatureManagement__FinancialBenchmarkingInsightsSummary" = var.configuration[var.environment].features.FinancialBenchmarkingInsightsSummary
    "FeatureManagement__HistoricalTrends"                     = var.configuration[var.environment].features.HistoricalTrends
    "FeatureManagement__HighExecutivePay"                     = var.configuration[var.environment].features.HighExecutivePay
    "FeatureManagement__HighNeeds"                            = var.configuration[var.environment].features.HighNeeds
    "FeatureManagement__FilteredSearch"                       = var.configuration[var.environment].features.FilteredSearch
    "FeatureManagement__SchoolSpendingPrioritiesSsrCharts"    = var.configuration[var.environment].features.SchoolSpendingPrioritiesSsrCharts
    "Apis__Insight__Url"                                      = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.insight-api-host.versionless_id})"
    "Apis__Insight__Key"                                      = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.insight-api-key.versionless_id})"
    "Apis__Establishment__Url"                                = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.establishment-api-host.versionless_id})"
    "Apis__Establishment__Key"                                = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.establishment-api-key.versionless_id})"
    "Apis__Benchmark__Url"                                    = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.benchmark-api-host.versionless_id})"
    "Apis__Benchmark__Key"                                    = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.benchmark-api-key.versionless_id})"
    "Apis__LocalAuthorityFinances__Url"                       = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.local-authority-finances-api-host.versionless_id})"
    "Apis__LocalAuthorityFinances__Key"                       = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.local-authority-finances-api-key.versionless_id})"
    "Apis__NonFinancial__Url"                                 = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.non-financial-api-host.versionless_id})"
    "Apis__NonFinancial__Key"                                 = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.non-financial-api-key.versionless_id})"
    "Apis__ChartRendering__Url"                               = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.chart-rendering-api-host.versionless_id})"
    "Apis__ChartRendering__Key"                               = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.chart-rendering-api-key.versionless_id})"
    "Apis__Content__Url"                                      = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.content-api-host.versionless_id})"
    "Apis__Content__Key"                                      = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.content-api-key.versionless_id})"
    "DFESignInSettings__APISecret"                            = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.dfe-signin-api-secret.versionless_id})"
    "DFESignInSettings__APIUri"                               = var.dfe-signin.api-uri
    "DFESignInSettings__Audience"                             = var.dfe-signin.audience
    "DFESignInSettings__CallbackPath"                         = var.dfe-signin.callback-path
    "DFESignInSettings__ClientID"                             = var.dfe-signin.client-id
    "DFESignInSettings__ClientSecret"                         = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.dfe-signin-client-secret.versionless_id})"
    "DFESignInSettings__Issuer"                               = var.dfe-signin.issuer
    "DFESignInSettings__MetadataAddress"                      = var.dfe-signin.metadata-address
    "DFESignInSettings__SignedOutCallbackPath"                = var.dfe-signin.signed-out-callback-path
    "DFESignInSettings__SignOutUri"                           = var.dfe-signin.sign-out-uri
    "DFESignInSettings__SignInUri"                            = var.dfe-signin.sign-in-uri
    "SessionData__Using"                                      = "Cosmos"
    "SessionData__Settings__ConnectionString"                 = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.session-cache-account-connection-string.versionless_id})"
    "SessionData__Settings__ContainerName"                    = azurerm_cosmosdb_sql_container.session-cache-container.name
    "SessionData__Settings__DatabaseName"                     = azurerm_cosmosdb_sql_database.session-cache-database.name
    "Storage__ConnectionString"                               = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.data-web-storage-connection-string.versionless_id})"
    "Storage__ReturnsContainer"                               = azurerm_storage_container.return-container.name
    "CacheOptions__ReturnYears__SlidingExpiration"            = var.configuration[var.environment].CacheOptions.ReturnYears.SlidingExpiration
    "CacheOptions__ReturnYears__AbsoluteExpiration"           = var.configuration[var.environment].CacheOptions.ReturnYears.AbsoluteExpiration
    "CacheOptions__CommercialResources__SlidingExpiration"    = var.configuration[var.environment].CacheOptions.CommercialResources.SlidingExpiration
    "CacheOptions__CommercialResources__AbsoluteExpiration"   = var.configuration[var.environment].CacheOptions.CommercialResources.AbsoluteExpiration
  }
  tags = local.common-tags
}

resource "azurerm_key_vault_access_policy" "keyvault_policy" {
  key_vault_id       = data.azurerm_key_vault.key-vault.id
  tenant_id          = azurerm_windows_web_app.education-benchmarking-as.identity[0].tenant_id
  object_id          = azurerm_windows_web_app.education-benchmarking-as.identity[0].principal_id
  secret_permissions = ["Get"]
}

resource "azurerm_key_vault_secret" "dfe-signin-api-secret" {
  #checkov:skip=CKV_AZURE_41:See ADO backlog AB#232052
  name         = "dfe-signin-api-secret"
  value        = var.dfe-signin.api-secret
  key_vault_id = data.azurerm_key_vault.key-vault.id
  content_type = "key"
}

resource "azurerm_key_vault_secret" "dfe-signin-client-secret" {
  #checkov:skip=CKV_AZURE_41:See ADO backlog AB#232052
  name         = "dfe-signin-client-secret"
  value        = var.dfe-signin.client-secret
  key_vault_id = data.azurerm_key_vault.key-vault.id
  content_type = "key"
}
