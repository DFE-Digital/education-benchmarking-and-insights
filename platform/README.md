# Platform

**//TODO: Describe platform component**

[![Build Status](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_apis/build/status%2FPlatform%20CICD?branchName=main)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_build/latest?definitionId=2595&branchName=main)

## Getting started

### Running Platform APIs

#### Establishment Function App
Add configuration in `local.settings.json` for `Platform.Establishment.Api`
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
    "Search__Key" : "[INSERT KEY VALUE]"
  },
  "Host": {
    "CORS": "*",
    "LocalHttpPort": 7073
  }
}
```

#### Benchmark Function App
Add configuration in `local.settings.json` for `Platform.Benchmark.Api`
```
{
  "IsEncrypted": false,
  "Values": {
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "ASPNETCORE_ENVIRONMENT": "Development",
    "Cosmos__ConnectionString" : "[INSERT CONNECTION STRING VALUE]",
    "Cosmos__DatabaseId" : "ebis-data",
    "Cosmos__FinancialPlanCollectionName" : "financial-plans",
    "Sql__ConnectionString" : "[INSERT CONNECTION STRING VALUE]"
  },
  "Host": {
    "CORS": "*",
    "LocalHttpPort": 7072
  }
}
```

#### Insight Function App
Add configuration in `local.settings.json` for `Platform.Insight.Api`
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