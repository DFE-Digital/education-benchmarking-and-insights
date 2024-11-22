<!-- BEGIN_TF_DOCS -->
## Requirements

| Name | Version |
|------|---------|
| <a name="requirement_terraform"></a> [terraform](#requirement\_terraform) | >= 1.9.8 |
| <a name="requirement_azurerm"></a> [azurerm](#requirement\_azurerm) | ~> 4.8.0 |

## Providers

| Name | Version |
|------|---------|
| <a name="provider_azurerm"></a> [azurerm](#provider\_azurerm) | 4.8.0 |
| <a name="provider_random"></a> [random](#provider\_random) | 3.6.3 |

## Modules

No modules.

## Resources

| Name | Type |
|------|------|
| [azurerm_api_connection.teams-api-connection](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/api_connection) | resource |
| [azurerm_log_analytics_query_pack.query-pack](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_query_pack) | resource |
| [azurerm_log_analytics_query_pack_query.custom-data-funnel](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_query_pack_query) | resource |
| [azurerm_log_analytics_query_pack_query.custom-pipeline-runs](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_query_pack_query) | resource |
| [azurerm_log_analytics_query_pack_query.default-pipeline-runs](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_query_pack_query) | resource |
| [azurerm_log_analytics_query_pack_query.dlq-new-messages-per-hour](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_query_pack_query) | resource |
| [azurerm_log_analytics_query_pack_query.feature-requests](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_query_pack_query) | resource |
| [azurerm_log_analytics_query_pack_query.feature-requests-by-auth](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_query_pack_query) | resource |
| [azurerm_log_analytics_query_pack_query.function-app-role-instance-count](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_query_pack_query) | resource |
| [azurerm_log_analytics_query_pack_query.popular-commercial-resources](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_query_pack_query) | resource |
| [azurerm_log_analytics_query_pack_query.popular-local-authority-requests](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_query_pack_query) | resource |
| [azurerm_log_analytics_query_pack_query.popular-school-requests](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_query_pack_query) | resource |
| [azurerm_log_analytics_query_pack_query.popular-trust-requests](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_query_pack_query) | resource |
| [azurerm_log_analytics_query_pack_query.session-count-per-day](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_query_pack_query) | resource |
| [azurerm_log_analytics_query_pack_query.session-length-per-day](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_query_pack_query) | resource |
| [azurerm_log_analytics_query_pack_query.tracked-links](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_query_pack_query) | resource |
| [azurerm_log_analytics_query_pack_query.user-auth-count-per-day](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_query_pack_query) | resource |
| [azurerm_log_analytics_query_pack_query.user-count-per-day](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_query_pack_query) | resource |
| [azurerm_log_analytics_query_pack_query.user-counts-table](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_query_pack_query) | resource |
| [azurerm_log_analytics_query_pack_query.user-new-count-per-day](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_query_pack_query) | resource |
| [azurerm_log_analytics_query_pack_query.user-sessions-table](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_query_pack_query) | resource |
| [azurerm_log_analytics_query_pack_query.waf-blocked-requests-per-hour](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_query_pack_query) | resource |
| [azurerm_log_analytics_query_pack_query.waf-requests](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_query_pack_query) | resource |
| [azurerm_log_analytics_query_pack_query.weekly-active-users](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_query_pack_query) | resource |
| [azurerm_log_analytics_saved_search.get-anon-requests](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_saved_search) | resource |
| [azurerm_log_analytics_saved_search.get-auth-users](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_saved_search) | resource |
| [azurerm_log_analytics_saved_search.get-commercial-resources](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_saved_search) | resource |
| [azurerm_log_analytics_saved_search.get-establishment-requests](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_saved_search) | resource |
| [azurerm_log_analytics_saved_search.get-fa-traces](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_saved_search) | resource |
| [azurerm_log_analytics_saved_search.get-feature-requests](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_saved_search) | resource |
| [azurerm_log_analytics_saved_search.get-new-users](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_saved_search) | resource |
| [azurerm_log_analytics_saved_search.get-queue-put-messages](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_saved_search) | resource |
| [azurerm_log_analytics_saved_search.get-school-financial-benchmarking-insights-summary-requests](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_saved_search) | resource |
| [azurerm_log_analytics_saved_search.get-session-length](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_saved_search) | resource |
| [azurerm_log_analytics_saved_search.get-sessions](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_saved_search) | resource |
| [azurerm_log_analytics_saved_search.get-tracked-links](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_saved_search) | resource |
| [azurerm_log_analytics_saved_search.get-tracked-save-charts](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_saved_search) | resource |
| [azurerm_log_analytics_saved_search.get-users](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_saved_search) | resource |
| [azurerm_log_analytics_saved_search.get-waf-blocked-requests](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_saved_search) | resource |
| [azurerm_log_analytics_saved_search.get-waf-logs](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/log_analytics_saved_search) | resource |
| [azurerm_logic_app_action_custom.condition-post-teams](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/logic_app_action_custom) | resource |
| [azurerm_logic_app_action_custom.initialise-affected-resource-ids](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/logic_app_action_custom) | resource |
| [azurerm_logic_app_trigger_http_request.http-trigger](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/logic_app_trigger_http_request) | resource |
| [azurerm_logic_app_workflow.alert-teams](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/logic_app_workflow) | resource |
| [azurerm_monitor_action_group.service-support-action](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/monitor_action_group) | resource |
| [azurerm_monitor_metric_alert.availability-alert](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/monitor_metric_alert) | resource |
| [azurerm_monitor_metric_alert.benchmark_api_error_alert](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/monitor_metric_alert) | resource |
| [azurerm_monitor_metric_alert.cpu_alert](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/monitor_metric_alert) | resource |
| [azurerm_monitor_metric_alert.establishment_api_error_alert](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/monitor_metric_alert) | resource |
| [azurerm_monitor_metric_alert.insight_api_error_alert](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/monitor_metric_alert) | resource |
| [azurerm_monitor_metric_alert.memory_alert](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/monitor_metric_alert) | resource |
| [azurerm_monitor_metric_alert.web_app_error_alert](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/monitor_metric_alert) | resource |
| [azurerm_monitor_smart_detector_alert_rule.dependency-performance-degradation-detector](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/monitor_smart_detector_alert_rule) | resource |
| [azurerm_monitor_smart_detector_alert_rule.exception-volume-changed-detector](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/monitor_smart_detector_alert_rule) | resource |
| [azurerm_monitor_smart_detector_alert_rule.failure-anomalies-detector](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/monitor_smart_detector_alert_rule) | resource |
| [azurerm_monitor_smart_detector_alert_rule.memory-leak-detector](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/monitor_smart_detector_alert_rule) | resource |
| [azurerm_monitor_smart_detector_alert_rule.request-performance-degradation-detector](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/monitor_smart_detector_alert_rule) | resource |
| [azurerm_monitor_smart_detector_alert_rule.trace-severity-detector](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/monitor_smart_detector_alert_rule) | resource |
| [azurerm_portal_dashboard.mi-dashboard](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/portal_dashboard) | resource |
| [azurerm_portal_dashboard.oi-dashboard](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/portal_dashboard) | resource |
| [azurerm_resource_group.resource-group](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/resource_group) | resource |
| [random_uuid.custom-data-funnel-id](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/uuid) | resource |
| [random_uuid.custom-pipeline-runs-id](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/uuid) | resource |
| [random_uuid.default-pipeline-runs-id](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/uuid) | resource |
| [random_uuid.dlq-new-messages-per-hour-id](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/uuid) | resource |
| [random_uuid.feature-requests-by-auth-id](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/uuid) | resource |
| [random_uuid.feature-requests-id](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/uuid) | resource |
| [random_uuid.function-app-role-instance-count-id](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/uuid) | resource |
| [random_uuid.popular-commercial-resources-id](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/uuid) | resource |
| [random_uuid.popular-local-authority-requests-id](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/uuid) | resource |
| [random_uuid.popular-school-requests-id](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/uuid) | resource |
| [random_uuid.popular-trust-requests-id](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/uuid) | resource |
| [random_uuid.session-count-per-day-id](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/uuid) | resource |
| [random_uuid.session-length-per-day-id](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/uuid) | resource |
| [random_uuid.tracked-links-id](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/uuid) | resource |
| [random_uuid.user-auth-count-per-day-id](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/uuid) | resource |
| [random_uuid.user-count-per-day-id](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/uuid) | resource |
| [random_uuid.user-counts-table-id](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/uuid) | resource |
| [random_uuid.user-new-count-per-day-id](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/uuid) | resource |
| [random_uuid.user-sessions-table-id](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/uuid) | resource |
| [random_uuid.waf-blocked-requests-per-hour-id](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/uuid) | resource |
| [random_uuid.waf-requests-id](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/uuid) | resource |
| [random_uuid.weekly-active-users-id](https://registry.terraform.io/providers/hashicorp/random/latest/docs/resources/uuid) | resource |
| [azurerm_application_insights.application-insights](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/application_insights) | data source |
| [azurerm_cdn_frontdoor_custom_domain.web-app-custom-domain](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/cdn_frontdoor_custom_domain) | data source |
| [azurerm_cdn_frontdoor_endpoint.web-app-front-door-endpoint](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/cdn_frontdoor_endpoint) | data source |
| [azurerm_client_config.client](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/client_config) | data source |
| [azurerm_log_analytics_workspace.application-insights-workspace](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/log_analytics_workspace) | data source |
| [azurerm_managed_api.teams-api](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/managed_api) | data source |
| [azurerm_service_plan.web-app-service-plan](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/service_plan) | data source |
| [azurerm_storage_account.data](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/storage_account) | data source |
| [azurerm_subscription.current](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/subscription) | data source |
| [azurerm_windows_function_app.benchmark-api](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/windows_function_app) | data source |
| [azurerm_windows_function_app.establishment-api](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/windows_function_app) | data source |
| [azurerm_windows_function_app.insight-api](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/windows_function_app) | data source |
| [azurerm_windows_web_app.web-app-service](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/windows_web_app) | data source |

## Inputs

| Name | Description | Type | Default | Required |
|------|-------------|------|---------|:--------:|
| <a name="input_cip-environment"></a> [cip-environment](#input\_cip-environment) | n/a | `any` | n/a | yes |
| <a name="input_configuration"></a> [configuration](#input\_configuration) | n/a | <pre>map(object({<br>    alerts_enabled       = bool<br>    smart_alerts_enabled = bool<br>    thresholds = object({<br>      availability = number<br>      cpu          = number<br>      memory       = number<br>      error        = number<br>    })<br>  }))</pre> | <pre>{<br>  "automated-test": {<br>    "alerts_enabled": false,<br>    "smart_alerts_enabled": false,<br>    "thresholds": {<br>      "availability": 90,<br>      "cpu": 95,<br>      "error": 5,<br>      "memory": 95<br>    }<br>  },<br>  "development": {<br>    "alerts_enabled": false,<br>    "smart_alerts_enabled": true,<br>    "thresholds": {<br>      "availability": 90,<br>      "cpu": 95,<br>      "error": 5,<br>      "memory": 95<br>    }<br>  },<br>  "feature": {<br>    "alerts_enabled": false,<br>    "smart_alerts_enabled": false,<br>    "thresholds": {<br>      "availability": 90,<br>      "cpu": 95,<br>      "error": 5,<br>      "memory": 95<br>    }<br>  },<br>  "pre-production": {<br>    "alerts_enabled": true,<br>    "smart_alerts_enabled": true,<br>    "thresholds": {<br>      "availability": 99.9,<br>      "cpu": 85,<br>      "error": 1,<br>      "memory": 85<br>    }<br>  },<br>  "production": {<br>    "alerts_enabled": true,<br>    "smart_alerts_enabled": true,<br>    "thresholds": {<br>      "availability": 99.9,<br>      "cpu": 85,<br>      "error": 1,<br>      "memory": 85<br>    }<br>  },<br>  "test": {<br>    "alerts_enabled": false,<br>    "smart_alerts_enabled": false,<br>    "thresholds": {<br>      "availability": 90,<br>      "cpu": 95,<br>      "error": 5,<br>      "memory": 95<br>    }<br>  }<br>}</pre> | no |
| <a name="input_environment"></a> [environment](#input\_environment) | n/a | `any` | n/a | yes |
| <a name="input_environment-prefix"></a> [environment-prefix](#input\_environment-prefix) | n/a | `any` | n/a | yes |
| <a name="input_location"></a> [location](#input\_location) | n/a | `any` | n/a | yes |
| <a name="input_support-alert-email"></a> [support-alert-email](#input\_support-alert-email) | n/a | `any` | n/a | yes |
| <a name="input_teams-channel-id"></a> [teams-channel-id](#input\_teams-channel-id) | n/a | `any` | n/a | yes |
| <a name="input_teams-team-id"></a> [teams-team-id](#input\_teams-team-id) | n/a | `any` | n/a | yes |
| <a name="input_trackedEvents"></a> [trackedEvents](#input\_trackedEvents) | n/a | `list(string)` | <pre>[<br>  "gias-school-details",<br>  "commercial-resource",<br>  "guidance-resource",<br>  "data-source",<br>  "organisation",<br>  "service-banner",<br>  "change-organisation",<br>  "survey"<br>]</pre> | no |

## Outputs

No outputs.
<!-- END_TF_DOCS -->