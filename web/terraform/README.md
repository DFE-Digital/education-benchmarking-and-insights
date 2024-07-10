<!-- BEGIN_TF_DOCS -->
## Requirements

| Name | Version |
|------|---------|
| <a name="requirement_terraform"></a> [terraform](#requirement\_terraform) | >= 1.0 |
| <a name="requirement_azurerm"></a> [azurerm](#requirement\_azurerm) | ~> 3.87 |

## Providers

| Name | Version |
|------|---------|
| <a name="provider_azurerm"></a> [azurerm](#provider\_azurerm) | 3.110.0 |
| <a name="provider_random"></a> [random](#provider\_random) | 3.6.2 |

## Modules

No modules.

## Resources

| Name | Type |
|------|------|
| [azurerm_application_insights_web_test.web_app_test](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/application_insights_web_test) | resource |
| [azurerm_cdn_frontdoor_custom_domain.web-app-custom-domain](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/cdn_frontdoor_custom_domain) | resource |
| [azurerm_cdn_frontdoor_custom_domain_association.web-app-custom-domain](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/cdn_frontdoor_custom_domain_association) | resource |
| [azurerm_cdn_frontdoor_endpoint.web-app-front-door-endpoint](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/cdn_frontdoor_endpoint) | resource |
| [azurerm_cdn_frontdoor_firewall_policy.web-app-front-door-waf-policy](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/cdn_frontdoor_firewall_policy) | resource |
| [azurerm_cdn_frontdoor_origin.web-app-front-door-origin-app-service](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/cdn_frontdoor_origin) | resource |
| [azurerm_cdn_frontdoor_origin_group.web-app-front-door-origin-group](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/cdn_frontdoor_origin_group) | resource |
| [azurerm_cdn_frontdoor_profile.web-app-front-door-profile](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/cdn_frontdoor_profile) | resource |
| [azurerm_cdn_frontdoor_route.web-app-front-door-route](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/cdn_frontdoor_route) | resource |
| [azurerm_cdn_frontdoor_security_policy.web-app-front-door-security-policy](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/cdn_frontdoor_security_policy) | resource |
| [azurerm_cosmosdb_account.session-cache-account](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/cosmosdb_account) | resource |
| [azurerm_cosmosdb_sql_container.session-cache-container](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/cosmosdb_sql_container) | resource |
| [azurerm_cosmosdb_sql_database.session-cache-database](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/cosmosdb_sql_database) | resource |
| [azurerm_cosmosdb_sql_role_assignment.app-service-cache](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/cosmosdb_sql_role_assignment) | resource |
| [azurerm_monitor_diagnostic_setting.front-door-analytics](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/monitor_diagnostic_setting) | resource |
| [azurerm_resource_group.resource-group](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/resource_group) | resource |
| [azurerm_service_plan.education-benchmarking-asp](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/service_plan) | resource |
| [azurerm_storage_account.data-source-storage](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/storage_account) | resource |
| [azurerm_storage_container.return-container](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/storage_container) | resource |
| [azurerm_windows_web_app.education-benchmarking-as](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/windows_web_app) | resource |
| [random_uuid.guidgen](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/uuid) | resource |
| [random_uuid.idgen](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/uuid) | resource |
| [azurerm_application_insights.application-insights](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/application_insights) | data source |
| [azurerm_key_vault.key-vault](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/key_vault) | data source |
| [azurerm_key_vault_secret.benchmark-api-host](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/key_vault_secret) | data source |
| [azurerm_key_vault_secret.benchmark-api-key](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/key_vault_secret) | data source |
| [azurerm_key_vault_secret.establishment-api-host](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/key_vault_secret) | data source |
| [azurerm_key_vault_secret.establishment-api-key](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/key_vault_secret) | data source |
| [azurerm_key_vault_secret.insight-api-host](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/key_vault_secret) | data source |
| [azurerm_key_vault_secret.insight-api-key](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/key_vault_secret) | data source |
| [azurerm_log_analytics_workspace.application-insights-workspace](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/log_analytics_workspace) | data source |
| [azurerm_subnet.web-app-subnet](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/subnet) | data source |

## Inputs

| Name | Description | Type | Default | Required |
|------|-------------|------|---------|:--------:|
| <a name="input_cip-environment"></a> [cip-environment](#input\_cip-environment) | n/a | `any` | n/a | yes |
| <a name="input_configuration"></a> [configuration](#input\_configuration) | n/a | <pre>map(object({<br>    sku_name               = string<br>    zone_balancing_enabled = bool<br>    worker_count           = number<br>    front_door_sku_name    = string<br>    features = object({<br>      CurriculumFinancialPlanning   = bool<br>      CustomData                    = bool<br>      Trusts                        = bool<br>      LocalAuthorities              = bool<br>      UserDefinedComparators        = bool<br>      DisableOrganisationClaimCheck = bool<br>      ForecastRisk                  = bool<br>      TrustComparison               = bool<br>    })<br>  }))</pre> | <pre>{<br>  "automated-test": {<br>    "features": {<br>      "CurriculumFinancialPlanning": true,<br>      "CustomData": true,<br>      "DisableOrganisationClaimCheck": true,<br>      "ForecastRisk": true,<br>      "LocalAuthorities": true,<br>      "TrustComparison": true,<br>      "Trusts": true,<br>      "UserDefinedComparators": true<br>    },<br>    "front_door_sku_name": "Standard_AzureFrontDoor",<br>    "sku_name": "B1",<br>    "worker_count": 1,<br>    "zone_balancing_enabled": false<br>  },<br>  "development": {<br>    "features": {<br>      "CurriculumFinancialPlanning": true,<br>      "CustomData": true,<br>      "DisableOrganisationClaimCheck": true,<br>      "ForecastRisk": true,<br>      "LocalAuthorities": true,<br>      "TrustComparison": true,<br>      "Trusts": true,<br>      "UserDefinedComparators": true<br>    },<br>    "front_door_sku_name": "Standard_AzureFrontDoor",<br>    "sku_name": "B1",<br>    "worker_count": 1,<br>    "zone_balancing_enabled": false<br>  },<br>  "feature": {<br>    "features": {<br>      "CurriculumFinancialPlanning": true,<br>      "CustomData": true,<br>      "DisableOrganisationClaimCheck": true,<br>      "ForecastRisk": true,<br>      "LocalAuthorities": true,<br>      "TrustComparison": true,<br>      "Trusts": true,<br>      "UserDefinedComparators": true<br>    },<br>    "front_door_sku_name": "Standard_AzureFrontDoor",<br>    "sku_name": "B1",<br>    "worker_count": 1,<br>    "zone_balancing_enabled": false<br>  },<br>  "pre-production": {<br>    "features": {<br>      "CurriculumFinancialPlanning": false,<br>      "CustomData": false,<br>      "DisableOrganisationClaimCheck": false,<br>      "ForecastRisk": false,<br>      "LocalAuthorities": false,<br>      "TrustComparison": false,<br>      "Trusts": false,<br>      "UserDefinedComparators": false<br>    },<br>    "front_door_sku_name": "Standard_AzureFrontDoor",<br>    "sku_name": "P0v3",<br>    "worker_count": 1,<br>    "zone_balancing_enabled": false<br>  },<br>  "production": {<br>    "features": {<br>      "CurriculumFinancialPlanning": false,<br>      "CustomData": false,<br>      "DisableOrganisationClaimCheck": false,<br>      "ForecastRisk": false,<br>      "LocalAuthorities": false,<br>      "TrustComparison": false,<br>      "Trusts": false,<br>      "UserDefinedComparators": false<br>    },<br>    "front_door_sku_name": "Standard_AzureFrontDoor",<br>    "sku_name": "P1v3",<br>    "worker_count": 1,<br>    "zone_balancing_enabled": false<br>  },<br>  "test": {<br>    "features": {<br>      "CurriculumFinancialPlanning": true,<br>      "CustomData": true,<br>      "DisableOrganisationClaimCheck": true,<br>      "ForecastRisk": true,<br>      "LocalAuthorities": true,<br>      "TrustComparison": true,<br>      "Trusts": true,<br>      "UserDefinedComparators": true<br>    },<br>    "front_door_sku_name": "Standard_AzureFrontDoor",<br>    "sku_name": "P0v3",<br>    "worker_count": 1,<br>    "zone_balancing_enabled": false<br>  }<br>}</pre> | no |
| <a name="input_dfe-signin"></a> [dfe-signin](#input\_dfe-signin) | n/a | `any` | n/a | yes |
| <a name="input_environment"></a> [environment](#input\_environment) | n/a | `any` | n/a | yes |
| <a name="input_environment-prefix"></a> [environment-prefix](#input\_environment-prefix) | n/a | `any` | n/a | yes |
| <a name="input_location"></a> [location](#input\_location) | n/a | `any` | n/a | yes |

## Outputs

No outputs.
<!-- END_TF_DOCS -->