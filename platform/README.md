# Platform

This project contains the platform APIs as consumed by other components within the monorepo.
Each .NET project is deployed as an independent Azure Function App.

## Prerequisites

1. Install [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
2. Install [Node 22](https://nodejs.org/en/download) and/or switch to that version
   using [nvm](https://github.com/nvm-sh/nvm)
3. Install Visual Studio 2022 Professional (with C# and Azure Workflows) or Rider 2025
4. Clone the project `git clone https://github.com/DFE-Digital/education-benchmarking-and-insights.git`

> **Note:** Ensure that, if cloning to a DfE user area, the root folder is outside any of the 'OneDrive'
> folders to prevent 'too long path name' errors at build time.

## Getting started

> Docker: Compose file contents configuration for running SQL Server, Azurite and Redis locally:

```sh
cd docker
echo "REDIS_PASSWORD=a_password_of_your_choice" >| redis.env
docker-compose up
```

### Running Platform APIs

#### Establishment Function App

Add configuration in `local.settings.json` for `Platform.Api.Establishment`

```json
{
  "IsEncrypted": false,
  "Values": {
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "ASPNETCORE_ENVIRONMENT": "Development",
    "Search__Name": "s198d01-ebis-search",
    "Search__Key": "[INSERT KEY VALUE]",
    "Sql__ConnectionString": "[INSERT CONNECTION STRING VALUE]",
    "Sql__TelemetryEnabled": true,
    "AzureFunctionsJobHost__logging__logLevel__default": "Information",
    "AzureFunctionsJobHost__logging__logLevel__Function": "Information"
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
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "ASPNETCORE_ENVIRONMENT": "Development",
    "Sql__ConnectionString": "[INSERT CONNECTION STRING VALUE]",
    "Sql__TelemetryEnabled": true,
    "PipelineMessageHub__ConnectionString": "UseDevelopmentStorage=true",
    "PipelineMessageHub__JobPendingQueue": "data-pipeline-job-pending",
    "AzureFunctionsJobHost__logging__logLevel__default": "Information",
    "AzureFunctionsJobHost__logging__logLevel__Function": "Information"
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
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "ASPNETCORE_ENVIRONMENT": "Development",
    "Sql__ConnectionString": "[INSERT CONNECTION STRING VALUE]",
    "Sql__TelemetryEnabled": true,
    "AzureFunctionsJobHost__logging__logLevel__default": "Information",
    "AzureFunctionsJobHost__logging__logLevel__Function": "Information",
    "Cache__Host": "localhost",
    "Cache__Port": "6379",
    "Cache__Password": "[PASSWORD DEFINED ABOVE IN redis.env]"
  },
  "Host": {
    "CORS": "*",
    "LocalHttpPort": 7071
  }
}
```

#### LocalAuthorityFinances Function App

Add configuration in `local.settings.json` for `Platform.Api.LocalAuthorityFinances`

```json
{
  "IsEncrypted": false,
  "Values": {
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "ASPNETCORE_ENVIRONMENT": "Development",
    "Sql__ConnectionString": "[INSERT CONNECTION STRING VALUE]",
    "Sql__TelemetryEnabled": true,
    "AzureFunctionsJobHost__logging__logLevel__default": "Information",
    "AzureFunctionsJobHost__logging__logLevel__Function": "Information"
  },
  "Host": {
    "CORS": "*",
    "LocalHttpPort": 7074
  }
}
```

#### NonFinancial Function App

Add configuration in `local.settings.json` for `Platform.Api.NonFinancial`

```json
{
  "IsEncrypted": false,
  "Values": {
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "ASPNETCORE_ENVIRONMENT": "Development",
    "Sql__ConnectionString": "[INSERT CONNECTION STRING VALUE]",
    "Sql__TelemetryEnabled": true,
    "AzureFunctionsJobHost__logging__logLevel__default": "Information",
    "AzureFunctionsJobHost__logging__logLevel__Function": "Information"
  },
  "Host": {
    "CORS": "*",
    "LocalHttpPort": 7075
  }
}
```

#### ChartRendering Function App

Add configuration in `local.settings.json` for `Platform.Api.ChartRendering`

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "node",
    "APPLICATIONINSIGHTS_CONNECTION_STRING": ""
  }
}
```

#### Content Function App

Add configuration in `local.settings.json` for `Platform.Api.Content`

```json
{
  "IsEncrypted": false,
  "Values": {
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "ASPNETCORE_ENVIRONMENT": "Development",
    "Sql__ConnectionString": "[INSERT CONNECTION STRING VALUE]",
    "Sql__TelemetryEnabled": true,
    "AzureFunctionsJobHost__logging__logLevel__default": "Information",
    "AzureFunctionsJobHost__logging__logLevel__Function": "Information"
  },
  "Host": {
    "CORS": "*",
    "LocalHttpPort": 7077
  }
}
```

#### Orchestrator Function App

For local development it's assumed Azurite will be used. More information can be
found [in Microsoft docs](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite?tabs=visual-studio%2Cblob-storage).

Add configuration in `local.settings.json` for `Platform.Orchestrator`

```json
{
    "IsEncrypted": false,
    "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
        "ASPNETCORE_ENVIRONMENT": "Development",
        "PipelineMessageHub__ConnectionString": "UseDevelopmentStorage=true",
        "PipelineMessageHub__JobFinishedQueue": "data-pipeline-job-finished",
        "PipelineMessageHub__JobCustomStartQueue": "data-pipeline-job-custom-start",
        "PipelineMessageHub__JobDefaultStartQueue": "data-pipeline-job-default-start",
        "PipelineMessageHub__JobPendingQueue": "data-pipeline-job-pending",
        "Sql__ConnectionString": "[INSERT CONNECTION STRING VALUE]",
        "Sql__TelemetryEnabled": true,
        "AzureFunctionsJobHost__logging__logLevel__default": "Information",
        "AzureFunctionsJobHost__logging__logLevel__Function": "Information",
        "Search__Name": "s198d01-ebis-search",
        "Search__Key": "[INSERT KEY VALUE]",
        "Cache__Host": "localhost",
        "Cache__Port": "6379",
        "Cache__Password": "[PASSWORD DEFINED ABOVE IN redis.env]"
    },
    "Host": {
        "CORS": "*",
        "LocalHttpPort": 7081
    }
}
```

#### User Data Cleanup Function App

For local development it's assumed Azurite will be used. More information can be
found [in Microsoft docs](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite?tabs=visual-studio%2Cblob-storage).

Add configuration in `local.settings.json` for `Platform.UserDataCleanUp`

```json
{
    "IsEncrypted": false,
    "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
        "ASPNETCORE_ENVIRONMENT": "Development",
        "Sql__ConnectionString": "[INSERT CONNECTION STRING VALUE]",
        "Sql__TelemetryEnabled": true,
        "AzureFunctionsJobHost__logging__logLevel__default": "Information",
        "AzureFunctionsJobHost__logging__logLevel__Function": "Information"
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

Dependencies when `UseDevelopmentStorage=true` is configured may be managed by connecting directly to Azurite
with a tool such as [Azure Storage Explorer](https://azure.microsoft.com/en-us/products/storage/storage-explorer)
using
the [well-known connection strings](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite?tabs=visual-studio%2Cblob-storage#connection-strings)
or by
following [these instructions](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite?tabs=visual-studio%2Cblob-storage#microsoft-azure-storage-explorer).
If nothing seems to be available locally on ports `10000` to `10002` then ensure Docker is running.

The following items should be created:

| Type  | Name                              | Config |
|-------|-----------------------------------|--------|
| Queue | `data-pipeline-job-finished`      |        |
| Queue | `data-pipeline-job-custom-start`  |        |
| Queue | `data-pipeline-job-default-start` |        |
| Queue | `data-pipeline-job-pending`       |        |

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

#### Debugging Redis locally

To debug Redis running in a Docker container, connect interactively and enter:

```sh
redis-cli -a [PASSWORD DEFINED ABOVE IN redis.env]
```

Redis commands may then be executed, e.g.:

```redis
SET test:key Hello
GET test:key
DEL test:key
```

Incoming requests may also be tracked with the command:

```redis
MONITOR
```

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
   "OutputPageResponse": true,
   "Headless": false,
   "Insight": {
      "Host": "http://localhost:7071",
      "Key": "xxx"
   },
   "Benchmark": {
      "Host": "http://localhost:7072",
      "Key": "xxx"
   },
   "Establishment": {
      "Host": "http://localhost:7073",
      "Key": "xxx"
   },
   "LocalAuthorityFinances": {
      "Host": "http://localhost:7074",
      "Key": "xxx"
   },
   "NonFinancial": {
      "Host": "http://localhost:7075",
      "Key": "xxx"
   },
   "ChartRendering": {
      "Host": "http://localhost:7076",
      "Key": "xxx"
   },
   "Content": {
      "Host": "http://localhost:7077",
      "Key": "xxx"
   }
}
```

From the root of the `platform` run

```bat
dotnet test tests\Platform.ApiTests
```

### Deploying Platform APIs

As per the other projects in this monorepo, the Platform APIs are deployed using Terraform via use of the `functions`
module under `./terraform/modules/functions`. The root project at `./terraform` contains the Function App configuration
at each environment level, as well as other supporting resources such as Azure Search and Blob Storage.

#### Environment-specific configuration

| Variable                       | Type   | Description                                                                                                                                                                            |
|--------------------------------|--------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `search_sku` `                 | string |                                                                                                                                                                                        |
| `search_replica_count`         | number |                                                                                                                                                                                        |
| `sql_telemetry_enabled`        | bool   |                                                                                                                                                                                        |
| `cache_sku`                    | string |                                                                                                                                                                                        |
| `cache_capacity`               | number |                                                                                                                                                                                        |
| `ssr_fa_worker_process_count`  | number | [FUNCTIONS_WORKER_PROCESS_COUNT](https://learn.microsoft.com/en-us/azure/azure-functions/functions-app-settings#functions_worker_process_count) for Chart Rendering (SSR) function app |
| `ssr_fa_sku`                   | string | SKU for the Chart Rendering (SSR) function app                                                                                                                                         |
| `ssr_fa_elastic_max_workers`   | number | Maximum number of total workers allowed for the app service plan (if an elastic plan)                                                                                                  |
| `ssr_fa_elastic_min_instances` | number | Minimum number of instances for the app service (if an elastic plan). May be set to `0` to scale down to zero if no load is present.                                                   |

## 🧹 Managing code formatting

The solution uses [EditorConfig](https://editorconfig.org/) to manage code formatting using a set of rules agreed
by the development team. ReSharper/Rider first applies its
[DotSettings](https://www.jetbrains.com/help/resharper/Sharing_Configuration_Options.html) config, then the EditorConfig
settings, plus any local (uncommitted) user-defined settings. To prevent duplication of settings files in the repo
only use `DotSettings` for custom dictionary entries, or those rules that should take priority and instead use
`.editorconfig` file for the formatting settings. When editing settings in Rider the option to merge into
`.editorconfig` is under `Save ▽` > `.editorconfig`.

The `dotnet format` command can be used to apply the settings to the code base using the `.editorconfig` file.
This is also performed automatically by the CI/CD pipeline. In ReSharper/Rider, the solution context menu item
`Reformat and Cleanup...` may be used to apply the settings using the layering order above. This may also be achieved
in the IDE at a project or file level or via the keyboard shortcut `Ctrl+E, C`.
