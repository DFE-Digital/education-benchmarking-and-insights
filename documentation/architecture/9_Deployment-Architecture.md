# Deployment Architecture

## Build pipelines

## Release pipelines

### Production release

This documentation does not cover the process of release planning and approval, rather the process to initiate a production-ready release to pre-prod and then approval onto prod after UAT sign-off.

Usually the release is taken off the artifacts produced from the latest merge to `main` for each project in the monorepo. The version numbers associated with each of these may be found in Azure DevOps at the following locations:

| Project                                                                                                                                                                           |
|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Core infrastructure](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_build?definitionId=2862&branchFilter=39627%2C39627%2C39627%2C39627%2C39627%2C39627)         |
| [Data pipeline](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_build?definitionId=2879&branchFilter=39627%2C39627%2C39627)                                       |
| [Platform](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_build?definitionId=2865&branchFilter=39627%2C39627%2C39627%2C39627%2C39627%2C39627)                    |
| [Web](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_build?definitionId=2866&branchFilter=39627%2C39627%2C39627%2C39627%2C39627%2C39627%2C39627%2C39627%2C39627) |

For example, the latest build against `main` that would have gone through QA in `t01` for the Core project in the example below has version `1.2.35`:

![Latest builds on Core on main](./images/latest-core-builds-main.png)

Once all of the latest version numbers have been gathered, a new pipeline run can be initiated from the [Release pipeline page](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_build?definitionId=2878) in DevOps. Leave the source branch as `main` and enter each of the four version numbers gathered previously. Any leading `#` symbols should _not_ be included with the version value provided. Then click `Run`.

![Run release pipeline](./images/run-release-pipeline.png)

The deployment to the pre-production environment will then commence and once complete, will be available for post-release tasks (such as running data pipelines), sanity testing and then onto UAT. The release version number should be communicated with relevant parties in order to cross-reference in documentation.

![Release approval](./images/release-approval.png)

At this stage, if issues are identified during UAT the release may be abandoned, a fix issued and the above process repeated. Alternatively, it may be approved for release to production. Either way, an action from the 'Review' dialog should be taken. If no action is taken after 24 hours then the approval is automatically rejected.

> **NOTE:** Only users in the `Release approvers` for the `production` environment may perform a Review action. There are no such restrictions on the initial deployment to pre-prod.

![Release review](./images/release-review.png)

Once approval has been made the deployment will continue onto production and once complete, will be available for post-release tasks and then smoke testing.

![Complete release pipeline](./images/complete-release-pipeline.png)

### Feature release

To deploy a short-running feature to infrastructure, the [Feature](../../pipelines/feature/release.yaml) release pipeline may be executed.
The intended audience is for features that may contain potentially breaking or in-flight changes that may cause the Development environment to fail should they be merged to `main` prematurely. The feature may be released off a selected source branch and subsequently [destroyed](#destroy-pipeline) once sufficient validations have taken place.

> **NOTE**: Feature deployments should be carefully managed as they will incur additional cost, both in provisioning time as well as financially. This may be achieved by keeping an up-to-date list in, for example, [Slack](https://hippo-digital.slack.com/lists/T12TVEKHQ/F09GXQAJ16F) along with environment ownership.

| Input parameter      | Example       | Purpose                                                                                                                                       |
|----------------------|---------------|-----------------------------------------------------------------------------------------------------------------------------------------------|
| `environment`        | `development` | Environment associated with the feature to deploy.                                                                                            |
| `featureEnvironment` | `13`          | Feature environment to create or update. Used with the `environment` choice, this will provision resources with the prefix such as `s198d13`. |

This pipeline is linked to the `feature` environment.

```mermaid
flowchart TD
    accDescr: Feature pipeline flow
    
    A[Provision Terraform Dependencies] --> B[Build, Provision and Deploy Core Infrastructure]
    B --> C[Build, Provision and Deploy Data Pipeline Infrastructure]
    C --> D[Build, Provision and Deploy Platform Infrastructure]
    D --> E[Build and Publish Front End Components]
    E --> F[Build, Provision and Deploy Web Infrastructure]
```

> **NOTE**: Data seeding is intentionally currently omitted from a feature release to infrastructure. To populate, obtain source files and begin a [data pipeline](../../data-pipeline/README.md) run.

#### Known issues

Deploying to a feature environment that has been used in the past sometimes leads to conflicts on resources that were not properly destroyed previously. This is most commonly seen on Diagnostic Setting and Smart Detector Alert Rule resources. e.g.:

```text
Error: creating/updating Monitor Smart Detector Alert Rule 
(Subscription: "xxx", Resource Group Name: "xxx-ebis-support" Smart Detector Alert Rule Name: "failure-anomalies"): 

unexpected status 409 (409 Conflict) with error: ScopeInUse: A FailureAnomaliesDetector alert rule with id '/subscriptions/xxx/resourcegroups/xxx-ebis-core/providers/microsoft.alertsmanagement/smartdetectoralertrules/failure anomalies - xxx-ebis-ai' is already defined on the resource '/subscriptions/xxx/resourcegroups/xxx-ebis-core/providers/microsoft.insights/components/xxx-ebis-ai'. Only a single FailureAnomaliesDetector alert rule can be define for the same resource.
```

In this case, manually remove the offending resource in Azure Portal and re-run the failed job.

### Destroy pipeline

To destroy a short-running feature in infrastructure, the [Destroy](../../pipelines/destroy/release.yaml) release pipeline may be executed.

| Input parameter      | Example       | Purpose                                                                                                                            |
|----------------------|---------------|------------------------------------------------------------------------------------------------------------------------------------|
| `environment`        | `development` | Environment associated with the feature to destroy.                                                                                |
| `featureEnvironment` | `13`          | Feature environment to destroy. Used with the `environment` choice, this will destroy resources with the prefix such as `s198d13`. |
| `areYouSure`         | `No`          | The first gate on this pipeline. Select `Yes` to confirm infrastructure destruction.                                               |

This pipeline is linked to the `destroy` environment, which itself has an approval gate assigned.

```mermaid
flowchart TD
    accDescr: Destroy pipeline flow
    
    A[Destroy Web Infrastructure] --> B[Destroy Platform Infrastructure]
    B --> C[Destroy Data Pipeline Infrastructure]
    C --> D[Destroy Core Infrastructure]
    D --> E[Destroy Terraform Dependencies]
```

#### Known issues

The destroy pipeline is known to be flaky and may require multiple runs. Some resources containing soft-deleted items (such as Key Vault) may also need to be manually removed upon repeat failures.

<!-- Leave the rest of this page blank -->
\newpage
