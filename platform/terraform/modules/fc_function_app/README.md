# Flex Consumption Function App Module

Terraform module to deploy Azure Function Apps on a Flex Consumption app service Plan for the Financial Benchmarking and Insights platform.

## Key Features
- Groups variables into logical objects (`core`, `monitoring`, `key_vault`, `service_plan`, `storage_account`, `sql_server`, etc.).
- Automatically configures Key Vault access policies and SQL database users.
- Optionally supports joining the function app onto a vnet
- Function app deployed with dedicated storge account to prevent latency issues with shared storage accounts
- Function app deployed with dedicated key vault, however keys also stored in shared key vault for developer convenience

## Usage

```terraform
module "my-function" {
  source       = "./modules/fc_function_app"
  app-settings = {
    "MySetting" = "Value"
  }

  core = {
    environment_prefix  = "s198d01"
    name                = "my-api"
    short_name          = "ma"
    resource_group_name = "my-rg"
    location            = "UK South"
    tags                = { "Environment" = "dev" }
  }

  identity = {
    tenant_id = "my_azure_tenant_id"
    object_id = "my_terraform_user_object_id"
  }

  monitoring = {
    log_analytics_id                  = "/subscriptions/.../workspaces/my-law"
    instrumentation_connection_string = "..."
  }

  shared_key_vault = {
    id            = "/subscriptions/.../keyVaults/my-kv"
    requires_keys = true
  }

  networking = {
    enable_restrictions = true
    subnet_ids          = ["/subscriptions/.../subnets/my-subnet"]
    join_subnet_id      = "/subscriptions/.../subnets/another-subnet"
  }

  sql_server = {
    fqdn     = "my-sql.database.windows.net"
    username = "admin"
    password = "password"
  }

  application_stack = {
    worker_runtime = "dotnet-isolated"
    dotnet_version = "v8.0"
  }
}
```
<!-- BEGIN_TF_DOCS -->
## Inputs

| Name                | Description                                      | Type | Default | Required |
|---------------------|--------------------------------------------------|------|---------|:--------:|
| `core`              | Object defining core naming, location, and tags. | `object` | n/a |   yes    |
| `identity`          | Object defining Azure identity details           | `object` | n/a |   yes    |
| `monitoring`        | Object defining Log Analytics and App Insights.  | `object` | n/a |   yes    |
| `shared_key_vault`  | Object defining Key Vault ID and key generation. | `object` | n/a |   yes    |
| `networking`        | Object defining IP restrictions and subnets.     | `object` | n/a |   yes    |
| `sql_server`        | Object defining SQL server connection details.   | `object` | n/a |   yes    |
| `application_stack` | Object defining runtime, version, and always-on. | `object` | `{}` |    no    |
| `redis_cache`       | Object defining Redis cache integration.         | `object` | `{}` |    no    |
| `app-settings`      | Map of application settings.                     | `map(string)` | n/a |   yes    |

## Outputs

| Name | Description |
|------|-------------|
| `function_app` | Cohesive object containing `id`, `name`, `principal_id`, `hostname`, `uri`, and `master_key`. |
| `function-resource-id` | Legacy output for the Function App ID. |
| `function-host-address` | Legacy output for the Function App URI. |
| `function-host-key` | Legacy output for the Function App Master Key. |

<!-- BEGIN_TF_DOCS -->
## Requirements

| Name | Version |
|------|---------|
| <a name="requirement_terraform"></a> [terraform](#requirement\_terraform) | >= 1.9.8 |
| <a name="requirement_azapi"></a> [azapi](#requirement\_azapi) | ~> 2.10.0 |
| <a name="requirement_mssql"></a> [mssql](#requirement\_mssql) | 0.3.1 |

## Providers

| Name | Version |
|------|---------|
| <a name="provider_azapi"></a> [azapi](#provider\_azapi) | 2.10.0 |
| <a name="provider_azurerm"></a> [azurerm](#provider\_azurerm) | 4.80.0 |
| <a name="provider_mssql"></a> [mssql](#provider\_mssql) | 0.3.1 |
| <a name="provider_random"></a> [random](#provider\_random) | 3.9.0 |
| <a name="provider_time"></a> [time](#provider\_time) | 0.14.1 |

## Modules

No modules.

## Resources

| Name                                                                                                                                                                                                 | Type        |
|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-------------|
| [azurerm_storage_account.func_app_sa](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/storage_account)                                                               | resource    |
| [azurerm_storage_container.func_app_sc](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/storage_container)                                                           | resource    |
| [azurerm_role_assignment.storage-data-owner](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/role_assignment)                                                        | resource    |
| [azurerm_key_vault_access_policy.terraform-agent-kv-access](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_access_policy)                                 | resource    |
| [azurerm_key_vault_access_policy.shared_key_vault_policy](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_access_policy)                                   | resource    |
| [azurerm_key_vault_access_policy.func-kv-access](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_access_policy)                                            | resource    |
| [azurerm_key_vault.func_app_kv](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault)                                                                           | resource    |
| [time_rotating.function_key_rotation](https://registry.terraform.io/providers/hashicorp/time/latest/docs/resources/rotating)                                                                         | resource    |
| [random_password.default_function_key](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/password)                                                                      | resource    |
| [random_password.master_function_key](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/password)                                                                       | resource    |
| [azurerm_user_assigned_identity.func-identity](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/user_assigned_identity)                                               | resource    |
| [azurerm_key_vault_secret.fa-host](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_secret)                                                                 | resource    |
| [azurerm_key_vault_secret.fa-key](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_secret)                                                                  | resource    |
| [azurerm_key_vault_secret.default-function-key](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_secret)                                                    | resource    |
| [azurerm_key_vault_secret.master-function-key](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_secret)                                                     | resource    |
| [azurerm_function_app_flex_consumption.func-app](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/function_app_flex_consumption)                                      | resource    |
| [azurerm_monitor_diagnostic_setting.func-app](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/monitor_diagnostic_setting)                                            | resource    |
| [azurerm_monitor_diagnostic_setting.func-app-service](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/monitor_diagnostic_setting)                                    | resource    |
| [azurerm_redis_cache_access_policy_assignment.contributor](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/redis_cache_access_policy_assignment)                     | resource    |
| [azurerm_redis_cache_access_policy_assignment.owner](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/redis_cache_access_policy_assignment)                           | resource    |
| [azurerm_resource_group_template_deployment.function_keys](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/resource_group_template_deployment)                       | resource    |
| [azurerm_service_plan.func-asp](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/service_plan)                                                                        | resource    |
| [azurerm_windows_function_app.func-app](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/windows_function_app)                                                        | resource    |
| [mssql_user.app-service-user](https://registry.terraform.io/providers/betr-io/mssql/0.3.1/docs/resources/user)                                                                                       | resource    |
| [azurerm_app_service_virtual_network_swift_connection.func-app-subnet](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/app_service_virtual_network_swift_connection) | resource    |
| [azapi_resource.app-service-identity](https://registry.terraform.io/providers/azure/azapi/latest/docs/data-sources/resource)                                                                         | data source |

## Inputs

| Name | Description | Type                                                                                                                                                                                                                                                                                                                                                                        | Default | Required |
|------|-------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|---------|:--------:|
| <a name="input_app-settings"></a> [app-settings](#input\_app-settings) | n/a | `map(string)`                                                                                                                                                                                                                                                                                                                                                               | n/a | yes |
| <a name="input_application_stack"></a> [application\_stack](#input\_application\_stack) | n/a | <pre>object({<br/>    worker_runtime       = optional(string, "dotnet-isolated")<br/>    use_32_bit_worker    = optional(bool, false)<br/>    dotnet_version       = optional(string, "v8.0")<br/>    use_isolated_runtime = optional(bool, true)<br/>    node_version         = optional(string, "22")<br/>    always_on            = optional(bool, false)<br/>  })</pre> | `{}` | no |
| <a name="input_core"></a> [core](#input\_core) | n/a | <pre>object({<br/>    environment_prefix  = string<br/>    name                = string<br/>    location            = string<br/>    resource_group_name = string<br/>    tags                = map(string)<br/>  })</pre>                                                                                                                                                  | n/a | yes |
| <a name="input_key_vault"></a> [key\_vault](#input\_key\_vault) | n/a | <pre>object({<br/>    id            = string<br/>    requires_keys = optional(bool, true)<br/>  })</pre>                                                                                                                                                                                                                                                                    | n/a | yes |
| <a name="input_monitoring"></a> [monitoring](#input\_monitoring) | n/a | <pre>object({<br/>    log_analytics_id                  = string<br/>    instrumentation_connection_string = optional(string)<br/>  })</pre>                                                                                                                                                                                                                                | n/a | yes |
| <a name="input_networking"></a> [networking](#input\_networking) | n/a | <pre>object({<br/>    enable_restrictions = bool<br/>    subnet_ids          = list(string)<br/>    join_subnet_id      = string<br/>  })</pre>                                                                                                                                                                                                                             | n/a | yes |
| <a name="input_redis_cache"></a> [redis\_cache](#input\_redis\_cache) | n/a | <pre>object({<br/>    id          = optional(string)<br/>    contributor = optional(bool, false)<br/>    owner       = optional(bool, false)<br/>  })</pre>                                                                                                                                                                                                                 | `{}` | no |
| <a name="input_service_plan"></a> [service\_plan](#input\_service\_plan) | n/a | <pre>object({<br/>    os_type                        = optional(string, "Windows")<br/>    size                           = optional(string, "Y1")<br/>    maximum_elastic_worker_count   = optional(number)<br/>    minimum_elastic_instance_count = optional(number)<br/>  })</pre>                                                                                       | `{}` | no |
| <a name="input_sql_server"></a> [sql\_server](#input\_sql\_server) | n/a | <pre>object({<br/>    fqdn     = string<br/>    username = string<br/>    password = string<br/>  })</pre>                                                                                                                                                                                                                                                                  | n/a | yes |
| <a name="input_storage_account"></a> [storage\_account](#input\_storage\_account) | n/a | <pre>object({<br/>    id   = string<br/>    name = string<br/>    key  = optional(string)<br/>  })</pre>                                                                                                                                                                                                                                                                    | n/a | yes |

## Outputs

| Name | Description |
|------|-------------|
| <a name="output_function-host-address"></a> [function-host-address](#output\_function-host-address) | Legacy outputs preserved for compatibility |
| <a name="output_function-host-key"></a> [function-host-key](#output\_function-host-key) | n/a |
| <a name="output_function-resource-id"></a> [function-resource-id](#output\_function-resource-id) | n/a |
<!-- END_TF_DOCS -->
