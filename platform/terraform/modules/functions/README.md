<!-- BEGIN_TF_DOCS -->
## Requirements

| Name | Version |
|------|---------|
| <a name="requirement_terraform"></a> [terraform](#requirement\_terraform) | >= 1.0 |
| <a name="requirement_azapi"></a> [azapi](#requirement\_azapi) | 1.15.0 |
| <a name="requirement_mssql"></a> [mssql](#requirement\_mssql) | 0.3.1 |

## Providers

| Name | Version |
|------|---------|
| <a name="provider_azapi"></a> [azapi](#provider\_azapi) | 1.13.1 |
| <a name="provider_azurerm"></a> [azurerm](#provider\_azurerm) | 3.112.0 |
| <a name="provider_mssql"></a> [mssql](#provider\_mssql) | 0.3.1 |

## Modules

No modules.

## Resources

| Name | Type |
|------|------|
| [azurerm_key_vault_access_policy.keyvault_policy](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_access_policy) | resource |
| [azurerm_key_vault_secret.fa-host](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_secret) | resource |
| [azurerm_key_vault_secret.fa-key](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_secret) | resource |
| [azurerm_resource_group_template_deployment.function_keys](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/resource_group_template_deployment) | resource |
| [azurerm_service_plan.func-asp](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/service_plan) | resource |
| [azurerm_windows_function_app.func-app](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/windows_function_app) | resource |
| [mssql_user.app-service-user](https://registry.terraform.io/providers/betr-io/mssql/0.3.1/docs/resources/user) | resource |
| [azapi_resource.app-service-identity](https://registry.terraform.io/providers/azure/azapi/1.15.0/docs/data-sources/resource) | data source |

## Inputs

| Name | Description | Type | Default | Required |
|------|-------------|------|---------|:--------:|
| <a name="input_always-on"></a> [always-on](#input\_always-on) | n/a | `bool` | `false` | no |
| <a name="input_app-settings"></a> [app-settings](#input\_app-settings) | n/a | `map(string)` | n/a | yes |
| <a name="input_common-tags"></a> [common-tags](#input\_common-tags) | n/a | `map(string)` | n/a | yes |
| <a name="input_dotnet-version"></a> [dotnet-version](#input\_dotnet-version) | n/a | `string` | `"v8.0"` | no |
| <a name="input_enable-restrictions"></a> [enable-restrictions](#input\_enable-restrictions) | n/a | `bool` | n/a | yes |
| <a name="input_environment-prefix"></a> [environment-prefix](#input\_environment-prefix) | n/a | `string` | n/a | yes |
| <a name="input_function-name"></a> [function-name](#input\_function-name) | n/a | `string` | n/a | yes |
| <a name="input_instrumentation-conn-string"></a> [instrumentation-conn-string](#input\_instrumentation-conn-string) | n/a | `string` | `null` | no |
| <a name="input_instrumentation-key"></a> [instrumentation-key](#input\_instrumentation-key) | n/a | `string` | `null` | no |
| <a name="input_key-vault-id"></a> [key-vault-id](#input\_key-vault-id) | n/a | `string` | n/a | yes |
| <a name="input_location"></a> [location](#input\_location) | n/a | `string` | n/a | yes |
| <a name="input_requires-keys"></a> [requires-keys](#input\_requires-keys) | n/a | `bool` | `true` | no |
| <a name="input_resource-group-name"></a> [resource-group-name](#input\_resource-group-name) | n/a | `string` | n/a | yes |
| <a name="input_sku"></a> [sku](#input\_sku) | n/a | <pre>object({<br>    size = string<br>  })</pre> | <pre>{<br>  "size": "Y1"<br>}</pre> | no |
| <a name="input_sql-server-fqdn"></a> [sql-server-fqdn](#input\_sql-server-fqdn) | n/a | `string` | n/a | yes |
| <a name="input_sql-server-password"></a> [sql-server-password](#input\_sql-server-password) | n/a | `string` | n/a | yes |
| <a name="input_sql-server-username"></a> [sql-server-username](#input\_sql-server-username) | n/a | `string` | n/a | yes |
| <a name="input_storage-account-id"></a> [storage-account-id](#input\_storage-account-id) | n/a | `string` | n/a | yes |
| <a name="input_storage-account-key"></a> [storage-account-key](#input\_storage-account-key) | n/a | `string` | `null` | no |
| <a name="input_storage-account-name"></a> [storage-account-name](#input\_storage-account-name) | n/a | `string` | n/a | yes |
| <a name="input_subnet_ids"></a> [subnet\_ids](#input\_subnet\_ids) | n/a | `list(string)` | n/a | yes |
| <a name="input_use-32-bit-worker"></a> [use-32-bit-worker](#input\_use-32-bit-worker) | n/a | `bool` | `false` | no |
| <a name="input_use-isolated-runtime"></a> [use-isolated-runtime](#input\_use-isolated-runtime) | n/a | `bool` | `true` | no |
| <a name="input_worker-runtime"></a> [worker-runtime](#input\_worker-runtime) | n/a | `string` | `"dotnet-isolated"` | no |

## Outputs

| Name | Description |
|------|-------------|
| <a name="output_function-host-address"></a> [function-host-address](#output\_function-host-address) | n/a |
| <a name="output_function-host-key"></a> [function-host-key](#output\_function-host-key) | n/a |
| <a name="output_function-resource-id"></a> [function-resource-id](#output\_function-resource-id) | n/a |
<!-- END_TF_DOCS -->