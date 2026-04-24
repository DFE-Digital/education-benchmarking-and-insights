<!-- BEGIN_TF_DOCS -->
## Requirements

| Name | Version |
|------|---------|
| <a name="requirement_terraform"></a> [terraform](#requirement\_terraform) | >= 1.9.8 |
| <a name="requirement_azurerm"></a> [azurerm](#requirement\_azurerm) | ~> 4.63.0 |

## Providers

| Name | Version |
|------|---------|
| <a name="provider_azurerm"></a> [azurerm](#provider\_azurerm) | 4.63.0 |
| <a name="provider_external"></a> [external](#provider\_external) | 2.3.5 |

## Modules

| Name | Source | Version |
|------|--------|---------|
| <a name="module_benchmark-fa"></a> [benchmark-fa](#module\_benchmark-fa) | ./modules/functions | n/a |
| <a name="module_chart-rendering-fa"></a> [chart-rendering-fa](#module\_chart-rendering-fa) | ./modules/functions | n/a |
| <a name="module_config"></a> [config](#module\_config) | ./modules/config | n/a |
| <a name="module_content-fa"></a> [content-fa](#module\_content-fa) | ./modules/functions | n/a |
| <a name="module_insight-fa"></a> [insight-fa](#module\_insight-fa) | ./modules/functions | n/a |
| <a name="module_local-authority-fa"></a> [local-authority-fa](#module\_local-authority-fa) | ./modules/functions | n/a |
| <a name="module_maintenance-tasks-fa"></a> [maintenance-tasks-fa](#module\_maintenance-tasks-fa) | ./modules/functions | n/a |
| <a name="module_orchestrator-fa"></a> [orchestrator-fa](#module\_orchestrator-fa) | ./modules/functions | n/a |
| <a name="module_school-fa"></a> [school-fa](#module\_school-fa) | ./modules/functions | n/a |
| <a name="module_trust-fa"></a> [trust-fa](#module\_trust-fa) | ./modules/functions | n/a |

## Resources

| Name | Type |
|------|------|
| [azurerm_key_vault_secret.cache-host-name](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_secret) | resource |
| [azurerm_key_vault_secret.cache-ssl-port](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_secret) | resource |
| [azurerm_key_vault_secret.platform-search-key](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_secret) | resource |
| [azurerm_monitor_diagnostic_setting.storage-blob](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/monitor_diagnostic_setting) | resource |
| [azurerm_redis_cache.cache](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/redis_cache) | resource |
| [azurerm_redis_cache_access_policy_assignment.owner](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/redis_cache_access_policy_assignment) | resource |
| [azurerm_resource_group.resource-group](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/resource_group) | resource |
| [azurerm_search_service.search](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/search_service) | resource |
| [azurerm_storage_account.storage](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/storage_account) | resource |
| [azurerm_storage_account_queue_properties.storage-queue-properties](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/storage_account_queue_properties) | resource |
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
| <a name="input_environment"></a> [environment](#input\_environment) | n/a | `any` | n/a | yes |
| <a name="input_environment-prefix"></a> [environment-prefix](#input\_environment-prefix) | n/a | `any` | n/a | yes |
| <a name="input_location"></a> [location](#input\_location) | n/a | `any` | n/a | yes |

## Outputs

No outputs.
<!-- END_TF_DOCS -->