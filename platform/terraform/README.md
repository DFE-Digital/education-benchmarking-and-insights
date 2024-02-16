<!-- BEGIN_TF_DOCS -->
## Requirements

| Name | Version |
|------|---------|
| <a name="requirement_terraform"></a> [terraform](#requirement\_terraform) | >= 1.0 |
| <a name="requirement_azurerm"></a> [azurerm](#requirement\_azurerm) | = 3.87.0 |

## Providers

| Name | Version |
|------|---------|
| <a name="provider_azurerm"></a> [azurerm](#provider\_azurerm) | 3.87.0 |

## Modules

| Name | Source | Version |
|------|--------|---------|
| <a name="module_benchmark-fa"></a> [benchmark-fa](#module\_benchmark-fa) | ./modules/functions | n/a |
| <a name="module_establishment-fa"></a> [establishment-fa](#module\_establishment-fa) | ./modules/functions | n/a |
| <a name="module_insight-fa"></a> [insight-fa](#module\_insight-fa) | ./modules/functions | n/a |

## Resources

| Name | Type |
|------|------|
| [azurerm_cosmosdb_account.cosmosdb-account](https://registry.terraform.io/providers/hashicorp/azurerm/3.87.0/docs/resources/cosmosdb_account) | resource |
| [azurerm_cosmosdb_sql_database.cosmosdb-container](https://registry.terraform.io/providers/hashicorp/azurerm/3.87.0/docs/resources/cosmosdb_sql_database) | resource |
| [azurerm_key_vault_secret.platform-cosmos-connection-string](https://registry.terraform.io/providers/hashicorp/azurerm/3.87.0/docs/resources/key_vault_secret) | resource |
| [azurerm_key_vault_secret.platform-search-key](https://registry.terraform.io/providers/hashicorp/azurerm/3.87.0/docs/resources/key_vault_secret) | resource |
| [azurerm_key_vault_secret.platform-storage-connection-string](https://registry.terraform.io/providers/hashicorp/azurerm/3.87.0/docs/resources/key_vault_secret) | resource |
| [azurerm_resource_group.resource-group](https://registry.terraform.io/providers/hashicorp/azurerm/3.87.0/docs/resources/resource_group) | resource |
| [azurerm_search_service.search](https://registry.terraform.io/providers/hashicorp/azurerm/3.87.0/docs/resources/search_service) | resource |
| [azurerm_storage_account.platform-storage](https://registry.terraform.io/providers/hashicorp/azurerm/3.87.0/docs/resources/storage_account) | resource |
| [azurerm_storage_container.local-authorities-container](https://registry.terraform.io/providers/hashicorp/azurerm/3.87.0/docs/resources/storage_container) | resource |
| [azurerm_application_insights.application-insights](https://registry.terraform.io/providers/hashicorp/azurerm/3.87.0/docs/data-sources/application_insights) | data source |
| [azurerm_key_vault.key-vault](https://registry.terraform.io/providers/hashicorp/azurerm/3.87.0/docs/data-sources/key_vault) | data source |

## Inputs

| Name | Description | Type | Default | Required |
|------|-------------|------|---------|:--------:|
| <a name="input_cip-environment"></a> [cip-environment](#input\_cip-environment) | n/a | `any` | n/a | yes |
| <a name="input_configuration"></a> [configuration](#input\_configuration) | n/a | <pre>map(object({<br>    cosmos = object({<br>      capabilities = list(string)<br>    })<br>    search = object({<br>      sku = string<br>    })<br>  }))</pre> | <pre>{<br>  "automated-test": {<br>    "cosmos": {<br>      "capabilities": [<br>        "EnableServerless"<br>      ]<br>    },<br>    "search": {<br>      "sku": "free"<br>    }<br>  },<br>  "development": {<br>    "cosmos": {<br>      "capabilities": []<br>    },<br>    "search": {<br>      "sku": "free"<br>    }<br>  },<br>  "test": {<br>    "cosmos": {<br>      "capabilities": [<br>        "EnableServerless"<br>      ]<br>    },<br>    "search": {<br>      "sku": "free"<br>    }<br>  }<br>}</pre> | no |
| <a name="input_environment"></a> [environment](#input\_environment) | n/a | `any` | n/a | yes |
| <a name="input_environment-prefix"></a> [environment-prefix](#input\_environment-prefix) | n/a | `any` | n/a | yes |
| <a name="input_location"></a> [location](#input\_location) | n/a | `any` | n/a | yes |

## Outputs

No outputs.
<!-- END_TF_DOCS -->