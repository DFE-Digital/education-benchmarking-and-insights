# Platform

**//TODO: Describe platform component**

[![Build Status](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_apis/build/status%2FDevelopment%2FPlatform?branchName=main)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_build/latest?definitionId=2865&branchName=main)

## Prerequisites
1. Install .NET 6 SDK (Platform)
2. Install Visual Studio 2022 Professional (with C# and Azure Workflows) or Rider 2023
3. Clone the project `git clone https://github.com/DFE-Digital/education-benchmarking-and-insights.git`

> Note: Ensure that, if cloning to a DfE user area, the root folder is outside any of the ‘OneDrive’ folders to prevent ‘too long path name’ errors at build time.

## Getting started
> Docker: Compose file contents configuration for running SQL server and Azurite locally

### Running Platform APIs

#### Establishment Function App
Add configuration in `local.settings.json` for `Platform.Api.Establishment`
```
{
  "IsEncrypted": false,
  "Values": {
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "ASPNETCORE_ENVIRONMENT": "Development",
    "Cosmos__ConnectionString" : "[INSERT CONNECTION STRING VALUE]",
    "Cosmos__DatabaseId" : "ebis-data",
    "Cosmos__EstablishmentCollectionName" : "GIAS",
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
```
{
  "IsEncrypted": false,
  "Values": {
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "ASPNETCORE_ENVIRONMENT": "Development",
    "Cosmos__ConnectionString" : "[INSERT CONNECTION STRING VALUE]",
    "Cosmos__DatabaseId" : "ebis-data",
    "Sql__ConnectionString" : "[INSERT CONNECTION STRING VALUE]"
  },
  "Host": {
    "CORS": "*",
    "LocalHttpPort": 7072
  }
}
```

#### Insight Function App
Add configuration in `local.settings.json` for `Platform.Api.Insight`
```
{
  "IsEncrypted": false,
  "Values": {
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "ASPNETCORE_ENVIRONMENT": "Development",
    "Cosmos__ConnectionString" : "[INSERT CONNECTION STRING VALUE]",
    "Cosmos__DatabaseId" : "ebis-data",
    "Cosmos__FloorAreaCollectionName" : "Floor-Area-2021-2022",
    "Cosmos__CfrLatestYear" : "[INSERT LATEST YEAR]",
    "Cosmos__AarLatestYear" : "[INSERT LATEST YEAR]",
    "Cosmos__EstablishmentCollectionName" : "GIAS",
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
```
{
    "IsEncrypted": false,
    "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet",
        "PipelineMessageHub__ConnectionString": "UseDevelopmentStorage=true",
        "PipelineMessageHub__JobFinishedQueue": "data-pipeline-job-finished",
        "PipelineMessageHub__JobStartQueue": "data-pipeline-job-start"
    },
    "Host": {
        "CORS": "*",
        "LocalHttpPort": 7081
    }
}
```

### Running tests
Tests will run when creating new Pull Requests and when code is merged into the main branch.
#### Unit Tests
From the root of the `platform` run
```
dotnet test tests\Platform.Tests
```
#### Functional Tests
Add configuration in `appsetings.local.json` for `Platform.ApiTests`
```
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
```
dotnet test tests\Platform.ApiTests
```