# Platform

**//TODO: Describe platform component**

## Prerequisites

1. Install .NET 6 SDK (Platform)
2. Install Visual Studio 2022 Professional (with C# and Azure Workflows) or Rider 2024
3. Clone the project `git clone https://github.com/DFE-Digital/education-benchmarking-and-insights.git`

> **Note:** Ensure that, if cloning to a DfE user area, the root folder is outside any of the 'OneDrive' folders to prevent 'too long path name' errors at build time.

## Getting started

> Docker: Compose file contents configuration for running SQL server and Azurite locally

### Running Platform APIs

#### Establishment Function App

Add configuration in `local.settings.json` for `Platform.Api.Establishment`

```json
{
  "IsEncrypted": false,
  "Values": {
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "ASPNETCORE_ENVIRONMENT": "Development",
    "Search__Name" : "s198d01-ebis-search",
    "Search__Key" : "[INSERT KEY VALUE]",
    "Sql__ConnectionString" : "[INSERT CONNECTION STRING VALUE]"
  },
  "Host": {
    "CORS": "*",
    "LocalHttpPort": 7073
  }
}
```

#### Benchmark Function App

Add configuration in `local.settings.json` for `Platform.Api.Benchmark`

```json
{
  "IsEncrypted": false,
  "Values": {
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "ASPNETCORE_ENVIRONMENT": "Development",
    "Search__Name" : "s198d01-ebis-search",
    "Search__Key" : "[INSERT KEY VALUE]",
    "Sql__ConnectionString" : "[INSERT CONNECTION STRING VALUE]",
    "PipelineMessageHub__ConnectionString": "UseDevelopmentStorage=true",
    "PipelineMessageHub__JobPendingQueue": "data-pipeline-job-pending"
  },
  "Host": {
    "CORS": "*",
    "LocalHttpPort": 7072
  }
}
```

#### Insight Function App

Add configuration in `local.settings.json` for `Platform.Api.Insight`

```json
{
  "IsEncrypted": false,
  "Values": {
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "ASPNETCORE_ENVIRONMENT": "Development",
    "Sql__ConnectionString" : "[INSERT CONNECTION STRING VALUE]"
  },
  "Host": {
    "CORS": "*",
    "LocalHttpPort": 7071
  }
}
```

#### Orchestrator Function App

For local development it's assumed Azurite will be used. More information can be found [here](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite?tabs=visual-studio%2Cblob-storage).

Add configuration in `local.settings.json` for `Platform.Orchestrator`

```json
{
    "IsEncrypted": false,
    "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet",
        "ASPNETCORE_ENVIRONMENT": "Development",
        "PipelineMessageHub__ConnectionString": "UseDevelopmentStorage=true",
        "PipelineMessageHub__JobFinishedQueue": "data-pipeline-job-finished",
        "PipelineMessageHub__JobStartQueue": "data-pipeline-job-start",
        "PipelineMessageHub__JobPendingQueue": "data-pipeline-job-pending",
        "Sql__ConnectionString" : "[INSERT CONNECTION STRING VALUE]"
    },
    "Host": {
        "CORS": "*",
        "LocalHttpPort": 7081
    }
}
```

#### User Data Cleanup Function App

For local development it's assumed Azurite will be used. More information can be found [here](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite?tabs=visual-studio%2Cblob-storage).

Add configuration in `local.settings.json` for `Platform.UserDataCleanUp`

```json
{
    "IsEncrypted": false,
    "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet",
        "ASPNETCORE_ENVIRONMENT": "Development",
        "Sql__ConnectionString" : "[INSERT CONNECTION STRING VALUE]"
    },
    "Host": {
        "CORS": "*",
        "LocalHttpPort": 7082
    }
}
```

#### Search Index App

For local development it's assumed deployed instance of Azure Search will be used.

The following program arguments are required to run the search index sync app

```bat
-s 's198d01-ebis-search' -k '[INSERT SEARCH KEY]' -c '[INSERT CONNECTION STRING VALUE]'
```

##### Azurite dependencies

Dependencies when `UseDevelopmentStorage=true` is configured may be managed by connecting directly to Azurite with a tool such as [Azure Storage Explorer](https://azure.microsoft.com/en-us/products/storage/storage-explorer) using the [well-known connection strings](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite?tabs=visual-studio%2Cblob-storage#connection-strings) or by following [these instructions](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite?tabs=visual-studio%2Cblob-storage#microsoft-azure-storage-explorer). If nothing seems to be available locally on ports `10000` to `10002` then ensure Docker is running.

The following items should be created:

| Type  | Name                         | Config |
|-------|------------------------------|--------|
| Queue | `data-pipeline-job-finished` |        |
| Queue | `data-pipeline-job-start`    |        |
| Queue | `data-pipeline-job-pending`  |        |

When running the `Orchestrator` API, errors such as:

```text
Request [0213b688-3f54-4ff5-9b54-4ff3988c672d] GET http://127.0.0.1:10001/devstoreaccount1/data-pipeline-job-finished?comp=metadata
Error response [0213b688-3f54-4ff5-9b54-4ff3988c672d] 404 The specified queue does not exist. (00.0s)
Server:Azurite-Queue/3.29.0
x-ms-error-code:QueueNotFound
x-ms-request-id:b18ea35b-56f4-4e2d-91ea-a4ae82ba59d3
x-ms-version:2024-02-04
```

will be resolved with responses such as:

```text
Request [d33a363d-1dc1-42b3-a847-cfb7f10e8116] GET http://127.0.0.1:10001/devstoreaccount1/data-pipeline-job-finished/messages?numofmessages=16&visibilitytimeout=600
Response [d33a363d-1dc1-42b3-a847-cfb7f10e8116] 200 OK (00.0s)
Server:Azurite-Queue/3.29.0
x-ms-client-request-id:d33a363d-1dc1-42b3-a847-cfb7f10e8116
x-ms-request-id:8a505a9f-64bf-4491-aafc-a6cb1af997e7
x-ms-version:2024-02-04
```

if everything is running as expected.

### Running tests

Tests will run when creating new Pull Requests and when code is merged into the main branch.

#### Unit Tests

From the root of the `platform` run

```bat
dotnet test tests\Platform.Tests
```

#### Functional Tests

Add configuration in `appsetings.local.json` for `Platform.ApiTests`

```json
{
    "Insight": {
      "Host": "[INSERT URL OF INSIGHT API UNDER TEST]",
      "Key": "[INSERT INSIGHT API KEY]"
    },
    "Benchmark": {
      "Host": "[INSERT URL OF BENCHMARK API UNDER TEST]",
      "Key": "[INSERT BENCHMARK API KEY]"
    },
    "Establishment": {
      "Host": "[INSERT URL OF ESTABLISHMENT API UNDER TEST]",
      "Key": "[INSERT ESTABLISHMENT API KEY]"
    }
}
```

From the root of the `platform` run

```bat
dotnet test tests\Platform.ApiTests
```
