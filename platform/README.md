# Platform

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
    "Cosmos__SizingCollectionName" : "SizelookupTest",
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
    "Cosmos__LookupCollectionName" : "fibre-directory",
    "Cosmos__RatingCollectionName" : "SADBandingTest"
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
  "Apis": {
    "Insight": {
      "Host": "http://localhost:7071/",
      "Key": "x"
    },
    "Benchmark": {
      "Host": "http://localhost:7072/",
      "Key": "x"
    },
    "Establishment": {
      "Host": "http://localhost:7073/",
      "Key": "x"
    }
  }
}
```
Run:
```
dotnet test tests\EducationBenchmarking.Platform.ApiTests
```