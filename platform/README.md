# Platform

[![Build Status](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_apis/build/status%2FPlatform%20CICD?branchName=refs%2Fpull%2F267%2Fmerge)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_build/latest?definitionId=2595&branchName=refs%2Fpull%2F267%2Fmerge)

## Getting started

### Running Platform APIs

#### Establishment Function App
Add configuration in `local.settings.json`
```
{
  "IsEncrypted": false,
  "Values": {
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "ASPNETCORE_ENVIRONMENT": "Development",
    "Cosmos__ConnectionString" : "[INSERT CONNECTION STRING VALUE]",
    "Cosmos__DatabaseId" : "ebis-data",
    "Cosmos__LookupCollectionName" : "fibre-directory",
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
Add configuration in `local.settings.json`
```
{
  "IsEncrypted": false,
  "Values": {
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "ASPNETCORE_ENVIRONMENT": "Development",
    "Cosmos__ConnectionString" : "[INSERT CONNECTION STRING VALUE]",
    "Cosmos__DatabaseId" : "ebis-data",
    "Cosmos__FinancialPlanCollectionName" : "financial-plans"
  },
  "Host": {
    "CORS": "*",
    "LocalHttpPort": 7072
  }
}
```

#### Insight Function App
Add configuration in `local.settings.json`
```
{
  "IsEncrypted": false,
  "Values": {
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "ASPNETCORE_ENVIRONMENT": "Development",
    "Cosmos__ConnectionString" : "[INSERT CONNECTION STRING VALUE]",
    "Cosmos__DatabaseId" : "ebis-data",
    "Cosmos__LookupCollectionName" : "fibre-directory"
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
Run:
```
dotnet test tests\EducationBenchmarking.Platform.Tests
```
#### Functional Tests
Add configuration in `appsetings.local.json`
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
Run:
```
dotnet test tests\EducationBenchmarking.Platform.ApiTests
```