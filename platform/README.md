# Platform

[![Build Status](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_apis/build/status%2FPlatform%20CICD?branchName=main)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_build/latest?definitionId=2595&branchName=main)

## Getting started
[comment]: <> (I think we need a more high level overview of what is in this directory and where to find it.)
[comment]: <> (With respect to the connection string values, can we add in where to find these / who to contact to get them. A big issue in DfE is not knowing who has access to what, and where to get certain access critical information. Adding something in here could help aleviate that in our project.)

### Running Platform APIs
[comment]: <> (Can we explain what the purpose of each of these APIs is at a high level?)
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
Add configuration in `local.settings.json`
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
Add configuration in `local.settings.json`
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
[comment]: <> (Are there any other tests for the APIs? Should we document at a high level what the tests are doing?)
Tests will run when creating new Pull Requests and when code is merged into the main branch.
#### Unit Tests
[comment]: <> (Do the run requirements change if your using WSL vs cmd etc? I'd assume not, but just checking.)
[comment]: <> (Can you add which directory you need to be in to run these please?)
Run:
```
dotnet test tests\Platform.Tests
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
dotnet test tests\Platform.ApiTests
```