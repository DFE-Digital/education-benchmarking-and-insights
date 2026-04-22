# Support & Analytics: Agent Mandates

This file defines specialized mandates and procedural constraints for AI agents working within the `support-analytics` module.

## Development Standards

- **KQL Externalization**: KQL must be stored in `.kql` files within `terraform/queries/` or `terraform/queries/functions/`.
- **Documentation Quality**: All Markdown files must adhere to the repository-wide linting standards enforced via pre-commit hooks and CI checks.
- **Function vs. Query**: Use `azurerm_log_analytics_saved_search` for reusable functions and `azurerm_log_analytics_query_pack_query` for standalone dashboard/discovery queries.
- **Tokenization**: Environment-specific values in KQL or JSON must be tokenized as `${TOKEN_NAME}` and injected via Terraform's `templatefile()` function.
- **Dashboard Maintenance**: Modify dashboards interactively in the Azure Portal, then export the JSON to `terraform/dashboards/*.tpl` files and apply tokenization before committing.
- **Logic App Deployment**: The Logic App uses `azurerm_logic_app_action_custom` for fine-grained workflow control; changes should be tested in a development environment first.
- **Teams API Authorization**: The `teams-api-connection` resource creates the API connection but *does not* authorize it. Authorization must be performed manually in the Azure Portal.

## Anti-Patterns

- **Modifying Teams API Display Name**: Do not change the `display_name` of the `teams-api-connection` in Terraform. This forces resource replacement and will silently drop the manual Azure Portal O365 authorization, breaking all alerts.
- **Mixing SQL and KQL**: Do not put T-SQL validation scripts (found in `./queries`) into the `terraform/queries` deployment pipeline, and vice versa.
- **Hardcoding GUIDs**: Never hardcode environment-specific resource IDs or Teams channel IDs; use variables and data sources.
- **Portal-Only Changes**: Avoid creating alerts or dashboards directly in the Portal without backporting them to Terraform.
- **Direct SQL Alerts**: Do not use T-SQL queries against the production database for real-time alerting; use KQL against the logs instead.
- **Schema Versions**: Do not use Adaptive Card schema versions >1.5 as they are not currently supported by Microsoft Teams.
- **Missing Telemetry**: Avoid adding new features to the Web project without ensuring appropriate custom properties are logged for tracking in MI dashboards.
