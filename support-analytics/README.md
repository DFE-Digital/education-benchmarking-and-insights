# Support & Analytics

This is a supplementary project that contains configuration for supporting and monitoring the service.

## Prerequisites

1. Install [Adaptive Card Previewer](https://marketplace.visualstudio.com/items?itemName=TeamsDevApp.vscode-adaptive-cards)
for VS Code

## Log Analytics

### Functions

The functions in `./queries/functions` are published to a query pack within the resource group for use either directly against
Application Insights or Log Analytics Workspace, or via Queries defined below. They are maintained in the repository as `.kql`
files which may be executed in Azure for validation purposes. Some intentionally contain tokenised values that are populated via
the Terraform scripts during a pipeline run. These are denoted by `${TOKEN_NAME}`, where `templatefile()` contains `TOKEN_NAME`
with an appropriate value in the `vars` dynamic block.

### Queries

The queries in `./queries` are published to a query pack within the resource group for use either directly against Application
Insights or Log Analytics Workspace, or via the Dashboards befined below. They may have dependencies on the Functions defined
above.

### Request logs

Requests from the Web App Service log custom properties along with the base
[Request telemetry](https://github.com/DFE-Digital/education-benchmarking-and-insights/tree/main/web/src/Web.App/Attributes/RequestTelemetry).
Some of these are consumed by Functions or Queries defined above, and include:

| Name            | Purpose               | Example       | Enum                                                                                                                                                            |
|-----------------|-----------------------|---------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Code`          | Local Authority code  | `123`         |                                                                                                                                                                 |
| `CompanyNumber` | Trust company number  | `012354678`   |                                                                                                                                                                 |
| `Establishment` | Type of establishment | `school`      | [TrackedRequestEstablishmentType](https://github.com/DFE-Digital/education-benchmarking-and-insights/blob/main/web/src/Web.App/Constants/TrackedRequestType.cs) |
| `Feature`       | Feature name          | `home`        | [TrackedRequestFeature](https://github.com/DFE-Digital/education-benchmarking-and-insights/blob/main/web/src/Web.App/Constants/TrackedRequestType.cs)           |
| `Referrer`      | Referrer name         | `school-home` |                                                                                                                                                                 |
| `Urn`           | School URN            | `123546`      |                                                                                                                                                                 |

## Dashboards

Shared Dashboards in `./dashboards` are maintained as `.tpl` JSON files in the repository. When making changes to the Dashboards
it is far easier to do this live in Azure Portal and then export and merge the source JSON with the appropriate `.tpl` file here.
Hard-coded environmental values will need to be tokenised and populated in the existing `templatefile()` Terraform function.

### Management Information

Usage and user analytics, such as page views, session length and establishment feature breakdowns.

### Operational Information

Platform metrics for consumption by operational support staff, such as availability, performance, failure rates and infrastructure
map.

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

## Alerts

<!-- TODO -->