resource "azurerm_service_plan" "education-benchmarking-asp" {
  name                   = "${var.environment-prefix}-education-benchmarking-asp"
  location               = azurerm_resource_group.resource-group.location
  resource_group_name    = azurerm_resource_group.resource-group.name
  os_type                = "Linux"
  sku_name               = var.configuration[var.environment].sku_name
  zone_balancing_enabled = var.configuration[var.environment].zone_balancing_enabled
  worker_count           = var.configuration[var.environment].worker_count
  tags                   = local.common-tags
}

resource "azurerm_linux_web_app" "education-benchmarking-as" {
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
      dotnet_version = "8.0"
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
    "ASPNETCORE_ENVIRONMENT"                           = "Production"
    "APPLICATIONINSIGHTS_CONNECTION_STRING"            = data.azurerm_application_insights.application-insights.connection_string
    "FeatureManagement__CurriculumFinancialPlanning"   = var.configuration[var.environment].features.CurriculumFinancialPlanning
    "FeatureManagement__CustomData"                    = var.configuration[var.environment].features.CustomData
    "FeatureManagement__Trusts"                        = var.configuration[var.environment].features.Trusts
    "FeatureManagement__LocalAuthorities"              = var.configuration[var.environment].features.LocalAuthorities
    "FeatureManagement__UserDefinedComparators"        = var.configuration[var.environment].features.UserDefinedComparators
    "FeatureManagement__DisableOrganisationClaimCheck" = var.configuration[var.environment].features.DisableOrganisationClaimCheck
    "Apis__Insight__Url"                               = data.azurerm_key_vault_secret.insight-api-host.value
    "Apis__Insight__Key"                               = data.azurerm_key_vault_secret.insight-api-key.value
    "Apis__Establishment__Url"                         = data.azurerm_key_vault_secret.establishment-api-host.value
    "Apis__Establishment__Key"                         = data.azurerm_key_vault_secret.establishment-api-key.value
    "Apis__Benchmark__Url"                             = data.azurerm_key_vault_secret.benchmark-api-host.value
    "Apis__Benchmark__Key"                             = data.azurerm_key_vault_secret.benchmark-api-key.value
    "DFESignInSettings__APISecret"                     = var.dfe-signin.api-secret
    "DFESignInSettings__APIUri"                        = var.dfe-signin.api-uri
    "DFESignInSettings__Audience"                      = var.dfe-signin.audience
    "DFESignInSettings__CallbackPath"                  = var.dfe-signin.callback-path
    "DFESignInSettings__ClientID"                      = var.dfe-signin.client-id
    "DFESignInSettings__ClientSecret"                  = var.dfe-signin.client-secret
    "DFESignInSettings__Issuer"                        = var.dfe-signin.issuer
    "DFESignInSettings__MetadataAddress"               = var.dfe-signin.metadata-address
    "DFESignInSettings__SignedOutCallbackPath"         = var.dfe-signin.signed-out-callback-path
    "DFESignInSettings__SignOutUri"                    = var.dfe-signin.sign-out-uri
    "SessionData__Using"                               = "Cosmos"
    "SessionData__Settings__ConnectionString"          = azurerm_cosmosdb_account.session-cache-account.primary_sql_connection_string
    "SessionData__Settings__ContainerName"             = azurerm_cosmosdb_sql_container.session-cache-container.name
    "SessionData__Settings__DatabaseName"              = azurerm_cosmosdb_sql_database.session-cache-database.name
  }
  tags = local.common-tags
}