<!-- BEGIN_TF_DOCS -->
## Requirements

| Name | Version |
|------|---------|
| <a name="requirement_terraform"></a> [terraform](#requirement\_terraform) | >= 1.9.8 |
| <a name="requirement_azapi"></a> [azapi](#requirement\_azapi) | ~> 2.12.0 |
| <a name="requirement_mssql"></a> [mssql](#requirement\_mssql) | 0.3.1 |

## Providers

| Name | Version |
|------|---------|
| <a name="provider_azapi"></a> [azapi](#provider\_azapi) | 2.12.0 |
| <a name="provider_azurerm"></a> [azurerm](#provider\_azurerm) | 5.2.0 |
| <a name="provider_mssql"></a> [mssql](#provider\_mssql) | 0.3.1 |
| <a name="provider_random"></a> [random](#provider\_random) | 3.9.0 |
| <a name="provider_time"></a> [time](#provider\_time) | 0.14.1 |

## Modules

No modules.

## Resources

| Name | Type |
|------|------|
| [azurerm_function_app_flex_consumption.func-app](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/function_app_flex_consumption) | resource |
| [azurerm_key_vault.func_app_kv](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault) | resource |
| [azurerm_key_vault_access_policy.func-kv-access](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_access_policy) | resource |
| [azurerm_key_vault_access_policy.shared_key_vault_policy](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_access_policy) | resource |
| [azurerm_key_vault_access_policy.terraform-agent-kv-access](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_access_policy) | resource |
| [azurerm_key_vault_secret.default-function-key](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_secret) | resource |
| [azurerm_key_vault_secret.fa-host](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_secret) | resource |
| [azurerm_key_vault_secret.fa-key](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_secret) | resource |
| [azurerm_key_vault_secret.master-function-key](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_secret) | resource |
| [azurerm_monitor_diagnostic_setting.func-app](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/monitor_diagnostic_setting) | resource |
| [azurerm_monitor_diagnostic_setting.func-app-service](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/monitor_diagnostic_setting) | resource |
| [azurerm_redis_cache_access_policy_assignment.contributor](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/redis_cache_access_policy_assignment) | resource |
| [azurerm_redis_cache_access_policy_assignment.owner](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/redis_cache_access_policy_assignment) | resource |
| [azurerm_role_assignment.storage-data-owner](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/role_assignment) | resource |
| [azurerm_service_plan.func-asp](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/service_plan) | resource |
| [azurerm_storage_account.func_app_sa](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/storage_account) | resource |
| [azurerm_storage_container.func_app_sc](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/storage_container) | resource |
| [azurerm_user_assigned_identity.func-identity](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/user_assigned_identity) | resource |
| [mssql_user.app-service-user](https://registry.terraform.io/providers/betr-io/mssql/0.3.1/docs/resources/user) | resource |
| [random_password.default_function_key](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/password) | resource |
| [random_password.master_function_key](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/password) | resource |
| [time_rotating.function_key_rotation](https://registry.terraform.io/providers/hashicorp/time/latest/docs/resources/rotating) | resource |
| [azapi_resource.app-service-identity](https://registry.terraform.io/providers/azure/azapi/latest/docs/data-sources/resource) | data source |

## Inputs

| Name | Description | Type | Default | Required |
|------|-------------|------|---------|:--------:|
| <a name="input_app-settings"></a> [app-settings](#input\_app-settings) | n/a | `map(string)` | n/a | yes |
| <a name="input_application_stack"></a> [application\_stack](#input\_application\_stack) | n/a | <pre>object({<br/>    worker_runtime  = optional(string, "dotnet-isolated")<br/>    runtime_version = optional(string, "8.0")<br/>  })</pre> | `{}` | no |
| <a name="input_core"></a> [core](#input\_core) | n/a | <pre>object({<br/>    environment_prefix  = string<br/>    name                = string<br/>    short_name          = string<br/>    location            = string<br/>    resource_group_name = string<br/>    tags                = map(string)<br/>  })</pre> | n/a | yes |
| <a name="input_identity"></a> [identity](#input\_identity) | n/a | <pre>object({<br/>    tenant_id = string<br/>    object_id = string<br/>  })</pre> | n/a | yes |
| <a name="input_monitoring"></a> [monitoring](#input\_monitoring) | n/a | <pre>object({<br/>    log_analytics_id                  = string<br/>    instrumentation_connection_string = optional(string)<br/>  })</pre> | n/a | yes |
| <a name="input_networking"></a> [networking](#input\_networking) | n/a | <pre>object({<br/>    enable_restrictions = bool<br/>    allow_subnet_ids    = list(string)<br/>    join_subnet_id      = string<br/>  })</pre> | n/a | yes |
| <a name="input_redis_cache"></a> [redis\_cache](#input\_redis\_cache) | n/a | <pre>object({<br/>    id          = optional(string)<br/>    contributor = optional(bool, false)<br/>    owner       = optional(bool, false)<br/>  })</pre> | `{}` | no |
| <a name="input_shared_key_vault"></a> [shared\_key\_vault](#input\_shared\_key\_vault) | n/a | <pre>object({<br/>    id            = string<br/>    requires_keys = optional(bool, true)<br/>  })</pre> | n/a | yes |
| <a name="input_sql_server"></a> [sql\_server](#input\_sql\_server) | n/a | <pre>object({<br/>    fqdn     = string<br/>    username = string<br/>    password = string<br/>  })</pre> | n/a | yes |

## Outputs

| Name | Description |
|------|-------------|
| <a name="output_function-host-address"></a> [function-host-address](#output\_function-host-address) | Legacy outputs preserved for compatibility |
| <a name="output_function-host-key"></a> [function-host-key](#output\_function-host-key) | n/a |
| <a name="output_function-resource-id"></a> [function-resource-id](#output\_function-resource-id) | n/a |
<!-- END_TF_DOCS -->
