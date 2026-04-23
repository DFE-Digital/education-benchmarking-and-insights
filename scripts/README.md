# Development Scripts

This directory contains PowerShell scripts to assist with local development and environment management.

## Environment Switching

These scripts allow you to quickly switch the configuration of the Platform APIs and the API Tests between local development and test environment dependencies.

### Platform API Environment Switcher

`switch-api-env.ps1` updates the `local.settings.json` files for all Platform API projects in `platform/src/apis`.

**Usage:**

```powershell
.\switch-api-env.ps1 -Environment test
```

**Configuration:**
Create a file named `api-env.json` in this directory. This file is ignored by Git to prevent secrets from leaking.

**Example `api-env.json`:**

```json
{
  "ApiTargets": [
    "Platform.Api.Benchmark",
    "Platform.Api.ChartRendering",
    "Platform.Api.Content",
    "Platform.Api.Insight",
    "Platform.Api.LocalAuthority",
    "Platform.Api.NonFinancial",
    "Platform.Api.School",
    "Platform.Api.Trust",
    "Platform.MaintenanceTasks",
    "Platform.Orchestrator"
  ],
  "Environments": {
    "dev": {
      "Sql__ConnectionString": "Server=localhost,1433;Database=data;User Id=SA;Password=mystrong!Pa55word;Encrypt=False;",
      "Search__Name": "[INSERT_NAME]",
      "Search__Key": "[INSERT_KEY]",
      "Cache__Host": "localhost"
    },
    "test": {
      "Sql__ConnectionString": "[INSERT_CONNECTION_STRING]",
      "Search__Name": "[INSERT_NAME]",
      "Search__Key": "[INSERT_KEY]",
      "Cache__Host": "[INSERT_HOST]"
    }
  }
}
```

### API Tests Environment Switcher

`switch-api-tests-env.ps1` updates the `appsettings.local.json` file for the `Platform.ApiTests` project.

**Usage:**

```powershell
.\switch-api-tests-env.ps1 -Environment local
```

**Configuration:**
Create a file named `api-tests-env.json` in this directory. This file is ignored by Git.

**Example `api-tests-env.json`:**

```json
{
  "Environments": {
    "local": {
      "School:Host": "http://localhost:7302",
      "School:Key": "[INSERT_KEY]",
      "Trust:Host": "http://localhost:7303",
      "Trust:Key": "[INSERT_KEY]",
      "LocalAuthority:Host": "http://localhost:7301",
      "LocalAuthority:Key": "[INSERT_KEY]",
      "Insight:Host": "http://localhost:7071",
      "Insight:Key": "[INSERT_KEY]",
      "Benchmark:Host": "http://localhost:7072",
      "Benchmark:Key": "[INSERT_KEY]",
      "NonFinancial:Host": "http://localhost:7075",
      "NonFinancial:Key": "[INSERT_KEY]",
      "ChartRendering:Host": "http://localhost:7076",
      "ChartRendering:Key": "[INSERT_KEY]",
      "Content:Host": "http://localhost:7077",
      "Content:Key": "[INSERT_KEY]"
    },
    "test": {
      "School:Host": "[INSERT_URL]",
      "School:Key": "[INSERT_KEY]",
      "Trust:Host": "[INSERT_URL]",
      "Trust:Key": "[INSERT_KEY]",
      "LocalAuthority:Host": "[INSERT_URL]",
      "LocalAuthority:Key": "[INSERT_KEY]",
      "Insight:Host": "[INSERT_URL]",
      "Insight:Key": "[INSERT_KEY]",
      "Benchmark:Host": "[INSERT_URL]",
      "Benchmark:Key": "[INSERT_KEY]",
      "NonFinancial:Host": "[INSERT_URL]",
      "NonFinancial:Key": "[INSERT_KEY]",
      "ChartRendering:Host": "[INSERT_URL]",
      "ChartRendering:Key": "[INSERT_KEY]",
      "Content:Host": "[INSERT_URL]",
      "Content:Key": "[INSERT_KEY]"
    }
  }
}
```
