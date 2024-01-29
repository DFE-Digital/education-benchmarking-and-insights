<!-- BEGIN_TF_DOCS -->
## Requirements

No requirements.

## Providers

| Name | Version |
|------|---------|
| <a name="provider_azurerm"></a> [azurerm](#provider\_azurerm) | n/a |

## Modules

No modules.

## Resources

| Name | Type |
|------|------|
| [azurerm_key_vault_secret.fa-host](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_secret) | resource |
| [azurerm_key_vault_secret.fa-key](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_secret) | resource |
| [azurerm_service_plan.func-asp](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/service_plan) | resource |
| [azurerm_template_deployment.function_keys](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/template_deployment) | resource |
| [azurerm_windows_function_app.func-app](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/windows_function_app) | resource |

## Inputs

| Name | Description | Type | Default | Required |
|------|-------------|------|---------|:--------:|
| <a name="input_always-on"></a> [always-on](#input\_always-on) | n/a | `bool` | `false` | no |
| <a name="input_app-settings"></a> [app-settings](#input\_app-settings) | n/a | `map(string)` | n/a | yes |
| <a name="input_application-insights-connection-string"></a> [application-insights-connection-string](#input\_application-insights-connection-string) | n/a | `string` | n/a | yes |
| <a name="input_common-tags"></a> [common-tags](#input\_common-tags) | n/a | `map(string)` | n/a | yes |
| <a name="input_cors"></a> [cors](#input\_cors) | n/a | `list(string)` | <pre>[<br>  "*"<br>]</pre> | no |
| <a name="input_environment-prefix"></a> [environment-prefix](#input\_environment-prefix) | n/a | `string` | n/a | yes |
| <a name="input_function-name"></a> [function-name](#input\_function-name) | n/a | `string` | n/a | yes |
| <a name="input_key-vault-id"></a> [key-vault-id](#input\_key-vault-id) | n/a | `string` | n/a | yes |
| <a name="input_location"></a> [location](#input\_location) | n/a | `string` | n/a | yes |
| <a name="input_requires-keys"></a> [requires-keys](#input\_requires-keys) | n/a | `bool` | `true` | no |
| <a name="input_resource-group-name"></a> [resource-group-name](#input\_resource-group-name) | n/a | `string` | n/a | yes |
| <a name="input_sku"></a> [sku](#input\_sku) | n/a | <pre>object({<br>    tier = string<br>    size = string<br>  })</pre> | <pre>{<br>  "size": "Y1",<br>  "tier": "Dynamic"<br>}</pre> | no |
| <a name="input_storage-account-key"></a> [storage-account-key](#input\_storage-account-key) | n/a | `string` | n/a | yes |
| <a name="input_storage-account-name"></a> [storage-account-name](#input\_storage-account-name) | n/a | `string` | n/a | yes |

## Outputs

| Name | Description |
|------|-------------|
| <a name="output_function-host-address"></a> [function-host-address](#output\_function-host-address) | n/a |
| <a name="output_function-host-key"></a> [function-host-key](#output\_function-host-key) | n/a |
<!-- END_TF_DOCS -->