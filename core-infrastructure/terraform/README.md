<!-- BEGIN_TF_DOCS -->
## Requirements

| Name | Version |
|------|---------|
| <a name="requirement_terraform"></a> [terraform](#requirement\_terraform) | >= 1.0 |
| <a name="requirement_azapi"></a> [azapi](#requirement\_azapi) | 1.13.1 |
| <a name="requirement_azurerm"></a> [azurerm](#requirement\_azurerm) | ~> 3.87 |

## Providers

| Name | Version |
|------|---------|
| <a name="provider_azapi"></a> [azapi](#provider\_azapi) | 1.13.1 |
| <a name="provider_azurerm"></a> [azurerm](#provider\_azurerm) | 3.98.0 |
| <a name="provider_random"></a> [random](#provider\_random) | 3.6.1 |

## Modules

No modules.

## Resources

| Name | Type |
|------|------|
| [azapi_resource_action.sql-server-auto-tuning](https://registry.terraform.io/providers/azure/azapi/1.13.1/docs/resources/resource_action) | resource |
| [azurerm_application_insights.application-insights](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/application_insights) | resource |
| [azurerm_container_registry.acr](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/container_registry) | resource |
| [azurerm_key_vault.key-vault](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault) | resource |
| [azurerm_key_vault_access_policy.terraform_sp_access](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_access_policy) | resource |
| [azurerm_key_vault_secret.data-storage-connection-string](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_secret) | resource |
| [azurerm_key_vault_secret.sql-connection-string](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_secret) | resource |
| [azurerm_key_vault_secret.sql-db-name](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_secret) | resource |
| [azurerm_key_vault_secret.sql-domain-name](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_secret) | resource |
| [azurerm_key_vault_secret.sql-password](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_secret) | resource |
| [azurerm_key_vault_secret.sql-user-name](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_secret) | resource |
| [azurerm_log_analytics_query_pack.query-pack](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_query_pack) | resource |
| [azurerm_log_analytics_query_pack_query.pipeline-runs](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_query_pack_query) | resource |
| [azurerm_log_analytics_query_pack_query.popular-local-authority-requests-chart](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_query_pack_query) | resource |
| [azurerm_log_analytics_query_pack_query.popular-school-requests-chart](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_query_pack_query) | resource |
| [azurerm_log_analytics_query_pack_query.popular-trust-requests-chart](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_query_pack_query) | resource |
| [azurerm_log_analytics_query_pack_query.tracked-links](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_query_pack_query) | resource |
| [azurerm_log_analytics_saved_search.get-establishment-requests](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_saved_search) | resource |
| [azurerm_log_analytics_saved_search.get-tracked-links](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_saved_search) | resource |
| [azurerm_log_analytics_workspace.application-insights-workspace](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_workspace) | resource |
| [azurerm_monitor_action_group.action-group](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/monitor_action_group) | resource |
| [azurerm_monitor_smart_detector_alert_rule.failure-anomalies-detector](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/monitor_smart_detector_alert_rule) | resource |
| [azurerm_mssql_database.sql-db](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/mssql_database) | resource |
| [azurerm_mssql_database_extended_auditing_policy.db-audit-policy](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/mssql_database_extended_auditing_policy) | resource |
| [azurerm_mssql_firewall_rule.sql-server-fw-azure-services](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/mssql_firewall_rule) | resource |
| [azurerm_mssql_firewall_rule.sql-server-fw-dfe-remote](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/mssql_firewall_rule) | resource |
| [azurerm_mssql_server.sql-server](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/mssql_server) | resource |
| [azurerm_mssql_server_extended_auditing_policy.sql-server-audit-policy](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/mssql_server_extended_auditing_policy) | resource |
| [azurerm_mssql_server_security_alert_policy.sql-security-alert-policy](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/mssql_server_security_alert_policy) | resource |
| [azurerm_mssql_server_vulnerability_assessment.sql-server-vulnerability](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/mssql_server_vulnerability_assessment) | resource |
| [azurerm_network_security_group.network-security-group](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/network_security_group) | resource |
| [azurerm_resource_group.resource-group](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/resource_group) | resource |
| [azurerm_role_assignment.sql-db-admin-log-storage-role-blob](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/role_assignment) | resource |
| [azurerm_role_assignment.sql-log-storage-role-blob](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/role_assignment) | resource |
| [azurerm_storage_account.data](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/storage_account) | resource |
| [azurerm_storage_account.sql-log-storage](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/storage_account) | resource |
| [azurerm_storage_container.pipeline-raw-data](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/storage_container) | resource |
| [azurerm_storage_container.sql-vulnerability-container](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/storage_container) | resource |
| [azurerm_storage_queue.pipeline-message-finished-queue](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/storage_queue) | resource |
| [azurerm_storage_queue.pipeline-message-pending-queue](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/storage_queue) | resource |
| [azurerm_storage_queue.pipeline-message-start-queue](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/storage_queue) | resource |
| [azurerm_subnet.platform-subnet](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/subnet) | resource |
| [azurerm_subnet.web-app-subnet](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/subnet) | resource |
| [azurerm_subnet_network_security_group_association.platform-subnet-nsg-association](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/subnet_network_security_group_association) | resource |
| [azurerm_subnet_network_security_group_association.web-app-subnet-nsg-association](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/subnet_network_security_group_association) | resource |
| [azurerm_user_assigned_identity.sql-db-admin](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/user_assigned_identity) | resource |
| [azurerm_virtual_network.app-service-network](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/virtual_network) | resource |
| [random_password.sql-admin-password](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/password) | resource |
| [random_uuid.tracked-links-id](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/uuid) | resource |
| [azurerm_client_config.client](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/client_config) | data source |

## Inputs

| Name | Description | Type | Default | Required |
|------|-------------|------|---------|:--------:|
| <a name="input_cip-environment"></a> [cip-environment](#input\_cip-environment) | n/a | `any` | n/a | yes |
| <a name="input_configuration"></a> [configuration](#input\_configuration) | n/a | <pre>map(object({<br>    sql_db_sku_name    = string<br>    sql_db_max_size_gb = number<br>  }))</pre> | <pre>{<br>  "automated-test": {<br>    "sql_db_max_size_gb": 5,<br>    "sql_db_sku_name": "S1"<br>  },<br>  "development": {<br>    "sql_db_max_size_gb": 5,<br>    "sql_db_sku_name": "S0"<br>  },<br>  "feature": {<br>    "sql_db_max_size_gb": 5,<br>    "sql_db_sku_name": "S0"<br>  },<br>  "pre-production": {<br>    "sql_db_max_size_gb": 5,<br>    "sql_db_sku_name": "S1"<br>  },<br>  "production": {<br>    "sql_db_max_size_gb": 5,<br>    "sql_db_sku_name": "S2"<br>  },<br>  "test": {<br>    "sql_db_max_size_gb": 5,<br>    "sql_db_sku_name": "S1"<br>  }<br>}</pre> | no |
| <a name="input_disable-purge-protection"></a> [disable-purge-protection](#input\_disable-purge-protection) | n/a | `any` | n/a | yes |
| <a name="input_environment"></a> [environment](#input\_environment) | n/a | `any` | n/a | yes |
| <a name="input_environment-prefix"></a> [environment-prefix](#input\_environment-prefix) | n/a | `any` | n/a | yes |
| <a name="input_location"></a> [location](#input\_location) | n/a | `any` | n/a | yes |
| <a name="input_trackedEvents"></a> [trackedEvents](#input\_trackedEvents) | n/a | `set(string)` | <pre>[<br>  "gias-school-details",<br>  "commercial-resource",<br>  "guidance-resource",<br>  "data-source",<br>  "organisation",<br>  "service-banner",<br>  "change-organisation"<br>]</pre> | no |

## Outputs

No outputs.
<!-- END_TF_DOCS -->