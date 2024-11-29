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

| Name            | Purpose               | Example       | Enum |
|-----------------|-----------------------|---------------|------|
| `Code`          | Local Authority code  | `123`         |      |
| `CompanyNumber` | Trust company number  | `012354678`   |      |
| `Establishment` | Type of establishment | `school`      | [TrackedRequestEstablishmentType](https://github.com/DFE-Digital/education-benchmarking-and-insights/blob/main/web/src/Web.App/Constants/TrackedRequestType.cs) |
| `Feature`       | Feature name          | `home`        | [TrackedRequestFeature](https://github.com/DFE-Digital/education-benchmarking-and-insights/blob/main/web/src/Web.App/Constants/TrackedRequestType.cs)     |
| `Referrer`      | Referrer name         | `school-home` |      |
| `Urn`           | School URN            | `123546`      |      |

## Dashboards

Shared Dashboards in `./dashboards` are maintained as `.tpl` JSON files in the repository. When making changes to the Dashboards
it is far easier to do this live in Azure Portal and then export and merge the source JSON with the appropriate `.tpl` file here.
Hard-coded environmental values will need to be tokenised and populated in the existing `templatefile()` Terraform function.

### Management Information

Usage and user analytics, such as page views, session length and establishment feature breakdowns.

### Operational Information

Platform metrics for consumption by operational support staff, such as availability, performance, failure rates and infrastructure
map.

## Alerts

<!-- TODO -->