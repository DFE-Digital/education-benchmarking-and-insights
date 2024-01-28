<!-- BEGIN_TF_DOCS -->
## Requirements

| Name | Version |
|------|---------|
| <a name="requirement_terraform"></a> [terraform](#requirement\_terraform) | >= 1.0 |
| <a name="requirement_azurerm"></a> [azurerm](#requirement\_azurerm) | = 3.87.0 |

## Providers

| Name | Version |
|------|---------|
| <a name="provider_azurerm"></a> [azurerm](#provider\_azurerm) | 3.87.0 |

## Modules

No modules.

## Resources

| Name | Type |
|------|------|
| [azurerm_linux_web_app.education-benchmarking-as](https://registry.terraform.io/providers/hashicorp/azurerm/3.87.0/docs/resources/linux_web_app) | resource |
| [azurerm_resource_group.resource-group](https://registry.terraform.io/providers/hashicorp/azurerm/3.87.0/docs/resources/resource_group) | resource |
| [azurerm_service_plan.education-benchmarking-asp](https://registry.terraform.io/providers/hashicorp/azurerm/3.87.0/docs/resources/service_plan) | resource |
| [azurerm_application_insights.application-insights](https://registry.terraform.io/providers/hashicorp/azurerm/3.87.0/docs/data-sources/application_insights) | data source |
| [azurerm_key_vault.key-vault](https://registry.terraform.io/providers/hashicorp/azurerm/3.87.0/docs/data-sources/key_vault) | data source |
| [azurerm_key_vault_secret.benchmark-api-host](https://registry.terraform.io/providers/hashicorp/azurerm/3.87.0/docs/data-sources/key_vault_secret) | data source |
| [azurerm_key_vault_secret.benchmark-api-key](https://registry.terraform.io/providers/hashicorp/azurerm/3.87.0/docs/data-sources/key_vault_secret) | data source |
| [azurerm_key_vault_secret.establishment-api-host](https://registry.terraform.io/providers/hashicorp/azurerm/3.87.0/docs/data-sources/key_vault_secret) | data source |
| [azurerm_key_vault_secret.establishment-api-key](https://registry.terraform.io/providers/hashicorp/azurerm/3.87.0/docs/data-sources/key_vault_secret) | data source |
| [azurerm_key_vault_secret.insight-api-host](https://registry.terraform.io/providers/hashicorp/azurerm/3.87.0/docs/data-sources/key_vault_secret) | data source |
| [azurerm_key_vault_secret.insight-api-key](https://registry.terraform.io/providers/hashicorp/azurerm/3.87.0/docs/data-sources/key_vault_secret) | data source |

## Inputs

| Name | Description | Type | Default | Required |
|------|-------------|------|---------|:--------:|
| <a name="input_cip-environment"></a> [cip-environment](#input\_cip-environment) | n/a | `any` | n/a | yes |
| <a name="input_configuration"></a> [configuration](#input\_configuration) | n/a | <pre>map(object({<br>    sku_name               = string<br>    zone_balancing_enabled = bool<br>    worker_count           = number<br>  }))</pre> | <pre>{<br>  "development": {<br>    "sku_name": "B1",<br>    "worker_count": 1,<br>    "zone_balancing_enabled": false<br>  }<br>}</pre> | no |
| <a name="input_dfe-signin"></a> [dfe-signin](#input\_dfe-signin) | n/a | `any` | n/a | yes |
| <a name="input_environment"></a> [environment](#input\_environment) | n/a | `any` | n/a | yes |
| <a name="input_environment-prefix"></a> [environment-prefix](#input\_environment-prefix) | n/a | `any` | n/a | yes |
| <a name="input_location"></a> [location](#input\_location) | n/a | `any` | n/a | yes |

## Outputs

No outputs.
<!-- END_TF_DOCS -->