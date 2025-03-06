<!-- BEGIN_TF_DOCS -->
## Requirements

| Name | Version |
|------|---------|
| <a name="requirement_terraform"></a> [terraform](#requirement\_terraform) | >= 1.9.8 |
| <a name="requirement_azurerm"></a> [azurerm](#requirement\_azurerm) | ~> 4.21.1 |

## Providers

| Name | Version |
|------|---------|
| <a name="provider_azurerm"></a> [azurerm](#provider\_azurerm) | 4.21.1 |
| <a name="provider_external"></a> [external](#provider\_external) | 2.3.4 |

## Modules

| Name | Source | Version |
|------|--------|---------|
| <a name="module_benchmark-fa"></a> [benchmark-fa](#module\_benchmark-fa) | ./modules/functions | n/a |
| <a name="module_data-clean-up-fa"></a> [data-clean-up-fa](#module\_data-clean-up-fa) | ./modules/functions | n/a |
| <a name="module_establishment-fa"></a> [establishment-fa](#module\_establishment-fa) | ./modules/functions | n/a |
| <a name="module_insight-fa"></a> [insight-fa](#module\_insight-fa) | ./modules/functions | n/a |
| <a name="module_local-authority-finances-fa"></a> [local-authority-finances-fa](#module\_local-authority-finances-fa) | ./modules/functions | n/a |
| <a name="module_non-financial-fa"></a> [non-financial-fa](#module\_non-financial-fa) | ./modules/functions | n/a |
| <a name="module_orchestrator-fa"></a> [orchestrator-fa](#module\_orchestrator-fa) | ./modules/functions | n/a |

## Resources

| Name | Type |
|------|------|
| [azurerm_key_vault_secret.cache-host-name](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_secret) | resource |
| [azurerm_key_vault_secret.cache-ssl-port](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_secret) | resource |
| [azurerm_key_vault_secret.platform-search-key](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_secret) | resource |
| [azurerm_monitor_diagnostic_setting.platform-storage-blob](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/monitor_diagnostic_setting) | resource |
| [azurerm_redis_cache.cache](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/redis_cache) | resource |
| [azurerm_redis_cache_access_policy_assignment.owner](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/redis_cache_access_policy_assignment) | resource |
| [azurerm_resource_group.resource-group](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/resource_group) | resource |
| [azurerm_search_service.search](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/search_service) | resource |
| [azurerm_storage_account.orchestrator-storage](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/storage_account) | resource |
| [azurerm_storage_account.platform-storage](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/storage_account) | resource |
| [azurerm_application_insights.application-insights](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/application_insights) | data source |
| [azurerm_client_config.client](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/client_config) | data source |
| [azurerm_key_vault.key-vault](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/key_vault) | data source |
| [azurerm_key_vault_secret.core-sql-connection-string](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/key_vault_secret) | data source |
| [azurerm_key_vault_secret.pipeline-message-hub-storage-connection-string](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/key_vault_secret) | data source |
| [azurerm_key_vault_secret.sql-password](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/key_vault_secret) | data source |
| [azurerm_key_vault_secret.sql-user-name](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/key_vault_secret) | data source |
| [azurerm_log_analytics_workspace.application-insights-workspace](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/log_analytics_workspace) | data source |
| [azurerm_mssql_server.sql-server](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/mssql_server) | data source |
| [azurerm_subnet.load-test-subnet](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/subnet) | data source |
| [azurerm_subnet.platform-subnet](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/subnet) | data source |
| [azurerm_subnet.web-app-subnet](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/subnet) | data source |
| [external_external.agent_ip_address](https://registry.terraform.io/providers/hashicorp/external/latest/docs/data-sources/external) | data source |

## Inputs

| Name | Description | Type | Default | Required |
|------|-------------|------|---------|:--------:|
| <a name="input_cip-environment"></a> [cip-environment](#input\_cip-environment) | n/a | `any` | n/a | yes |
| <a name="input_configuration"></a> [configuration](#input\_configuration) | n/a | <pre>map(object({<br>    search_sku            = string<br>    search_replica_count  = number<br>    sql_telemetry_enabled = bool<br>    cache_sku             = string<br>    cache_capacity        = number<br>  }))</pre> | <pre>{<br>  "automated-test": {<br>    "cache_capacity": 1,<br>    "cache_sku": "Basic",<br>    "search_replica_count": 1,<br>    "search_sku": "basic",<br>    "sql_telemetry_enabled": false<br>  },<br>  "development": {<br>    "cache_capacity": 1,<br>    "cache_sku": "Basic",<br>    "search_replica_count": 1,<br>    "search_sku": "basic",<br>    "sql_telemetry_enabled": true<br>  },<br>  "feature": {<br>    "cache_capacity": 1,<br>    "cache_sku": "Basic",<br>    "search_replica_count": 1,<br>    "search_sku": "basic",<br>    "sql_telemetry_enabled": true<br>  },<br>  "pre-production": {<br>    "cache_capacity": 1,<br>    "cache_sku": "Standard",<br>    "search_replica_count": 1,<br>    "search_sku": "basic",<br>    "sql_telemetry_enabled": false<br>  },<br>  "production": {<br>    "cache_capacity": 1,<br>    "cache_sku": "Standard",<br>    "search_replica_count": 3,<br>    "search_sku": "basic",<br>    "sql_telemetry_enabled": false<br>  },<br>  "test": {<br>    "cache_capacity": 1,<br>    "cache_sku": "Basic",<br>    "search_replica_count": 1,<br>    "search_sku": "basic",<br>    "sql_telemetry_enabled": false<br>  }<br>}</pre> | no |
| <a name="input_environment"></a> [environment](#input\_environment) | n/a | `any` | n/a | yes |
| <a name="input_environment-prefix"></a> [environment-prefix](#input\_environment-prefix) | n/a | `any` | n/a | yes |
| <a name="input_location"></a> [location](#input\_location) | n/a | `any` | n/a | yes |

## Outputs

No outputs.
<!-- END_TF_DOCS -->