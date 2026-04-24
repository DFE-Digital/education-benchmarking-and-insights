# Functions Module

Terraform module to deploy Azure Function Apps (Windows or Linux) for the Financial Benchmarking and Insights platform.

## Key Features
- Supports both **Windows** and **Linux** function apps via a single module.
- Groups variables into logical objects (`core`, `monitoring`, `key_vault`, `service_plan`, `storage_account`, `sql_server`, etc.).
- Automatically configures Key Vault access policies and SQL database users.
- Simplified resource referencing using the `one()` idiom.

## Usage

```terraform
module "my-function" {
  source       = "./modules/functions"
  app-settings = {
    "MySetting" = "Value"
  }

  core = {
    environment_prefix  = "s198d01"
    name                = "my-api"
    resource_group_name = "my-rg"
    location            = "UK South"
    tags                = { "Environment" = "dev" }
  }

  monitoring = {
    log_analytics_id                  = "/subscriptions/.../workspaces/my-law"
    instrumentation_connection_string = "..."
  }

  key_vault = {
    id            = "/subscriptions/.../keyVaults/my-kv"
    requires_keys = true
  }

  service_plan = {
    os_type = "Windows"
    size    = "Y1"
  }

  storage_account = {
    id   = "/subscriptions/.../storageAccounts/my-sa"
    name = "mysa"
    key  = "..." # Optional
  }

  networking = {
    enable_restrictions = true
    subnet_ids          = ["/subscriptions/.../subnets/my-subnet"]
  }

  sql_server = {
    fqdn     = "my-sql.database.windows.net"
    username = "admin"
    password = "password"
  }

  application_stack = {
    worker_runtime = "dotnet-isolated"
    dotnet_version = "v8.0"
  }
}
```

## Inputs

| Name | Description | Type | Default | Required |
|------|-------------|------|---------|:--------:|
| `core` | Object defining core naming, location, and tags. | `object` | n/a | yes |
| `monitoring` | Object defining Log Analytics and App Insights. | `object` | n/a | yes |
| `key_vault` | Object defining Key Vault ID and key generation. | `object` | n/a | yes |
| `service_plan` | Object defining OS, SKU, and elastic settings. | `object` | `{}` | no |
| `storage_account` | Object defining storage account attributes. | `object` | n/a | yes |
| `networking` | Object defining IP restrictions and subnets. | `object` | n/a | yes |
| `sql_server` | Object defining SQL server connection details. | `object` | n/a | yes |
| `application_stack` | Object defining runtime, version, and always-on. | `object` | `{}` | no |
| `redis_cache` | Object defining Redis cache integration. | `object` | `{}` | no |
| `app-settings` | Map of application settings. | `map(string)` | n/a | yes |

## Outputs

| Name | Description |
|------|-------------|
| `function_app` | Cohesive object containing `id`, `name`, `principal_id`, `hostname`, `uri`, and `master_key`. |
| `function-resource-id` | Legacy output for the Function App ID. |
| `function-host-address` | Legacy output for the Function App URI. |
| `function-host-key` | Legacy output for the Function App Master Key. |
