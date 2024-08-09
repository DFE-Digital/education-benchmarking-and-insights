# Decision - 0005 - API Performance Testing with Azure Load Tests

Date: 2024-08-09

## Status

Pending

## Context

As per [#219594](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/219594) and its child tasks, a technical solution is required
to be able to initiate performance testing of the Platform APIs. The solution needs to be able to support different load strategies (Average, Load, Soak) and
allow results to be analysed and compared to historical test runs to see if resource manipulation improves performance and/or bottlenecks to be identified.

For the purposes of making this decision, a spike was completed an documented in
[#222305](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/222305) and a demo of the features carried out across the technical
team.

## Decision

[Azure Load Testing](https://learn.microsoft.com/en-gb/azure/load-testing/) is well suited to the requirements that the performance test runs are:

- Simple to maintain
- Maintained and provisioned with existing architecture
- Integrated with other existing metrics to help analyse results
- Executable on-demand or as part of existing pipelines

## Feature Comparison

| Feature       | Azure Load Testing                                                                                                                                           | k6                                                                                                                                                                             |
|---------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Test types    | [Linear, Step, Spike](https://learn.microsoft.com/en-us/azure/load-testing/how-to-high-scale-load?tabs=portal#configure-load-parameters-for-url-based-tests) | [Smoke, Average, Stress, Soak, Spike, Breakpoint](https://grafana.com/docs/k6/latest/testing-guides/test-types/)                                                               |
| Configuration | YAML & JSON / Azure Portal *                                                                                                                                | [JavaScript](https://grafana.com/docs/k6/latest/javascript-api/)                                                                                                               |
| Scenarios | Up to five requests per test run | [JavaScript](https://grafana.com/docs/k6/latest/javascript-api/) |
| External data | Built-in via [`configurationFiles`](https://learn.microsoft.com/en-gb/azure/load-testing/reference-test-config-yaml) | Standard Node.JS support |
| Provisioning  | [Terraform](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/load_test), ADO Task & [Azure CLI](https://learn.microsoft.com/en-us/cli/azure/load/test/app-component?view=azure-cli-latest#az-load-test-app-component-add) (in preview)                                                                                                                              | ADO Task                                                                                                                                                                       |
| ADO Task      | [AzureLoadTest](https://learn.microsoft.com/en-us/azure/devops/pipelines/tasks/reference/azure-load-test-v1?view=azure-pipelines)                            | [k6-load-test](https://marketplace.visualstudio.com/items?itemName=k6.k6-load-test)                                                                                            |
| Hosting       | Azure                                                                                                                                                        | Local or [Grafana Cloud](https://grafana.com/auth/sign-up/create-user?plcmt=sidebar&pg=docs-grafana-cloud-testing-k6-author-run-tokens-and-cli-authentication&redirectPath=k6) |
| Multi region  | Build-in via [`engineInstances`](https://learn.microsoft.com/en-gb/azure/load-testing/how-to-high-scale-load?tabs=pipelines) config                                                                                                                         | [Grafana Cloud](https://grafana.com/auth/sign-up/create-user?plcmt=sidebar&pg=docs-grafana-cloud-testing-k6-author-run-tokens-and-cli-authentication&redirectPath=k6)          |
| Cost          | [$10 per month](https://azure.microsoft.com/en-gb/pricing/details/load-testing/#pricing) for Load Testing Resource<br/>(including 50 Virtual User Hours; each extra hour is $0.15 for first 9,950)                                           | From [$0](https://grafana.com/pricing/?src=k6io)                                                                                                                                                                        |
| Analysis      |     [Azure Portal](https://learn.microsoft.com/en-gb/azure/load-testing/how-to-compare-multiple-test-runs)                                                                                                                                                         | [Dashboard](https://grafana.com/docs/k6/latest/results-output/web-dashboard/)                                                                                                  |

\* This also includes the ability to author the tests in Azure Portal and then download the resources for inclusion in VSC
