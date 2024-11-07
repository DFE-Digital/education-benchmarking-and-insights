<!-- BEGIN_TF_DOCS -->
## Requirements

No requirements.

## Providers

| Name | Version |
|------|---------|
| <a name="provider_azurerm"></a> [azurerm](#provider\_azurerm) | 4.7.0 |

## Modules

No modules.

## Resources

| Name | Type |
|------|------|
| [azurerm_container_app.data-pipeline](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/container_app) | resource |
| [azurerm_container_registry.acr](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/container_registry) | data source |
| [azurerm_key_vault.key-vault](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/key_vault) | data source |
| [azurerm_key_vault_secret.core-db-domain-name](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/key_vault_secret) | data source |
| [azurerm_key_vault_secret.core-db-name](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/key_vault_secret) | data source |
| [azurerm_key_vault_secret.core-db-password](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/key_vault_secret) | data source |
| [azurerm_key_vault_secret.core-db-user-name](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/key_vault_secret) | data source |
| [azurerm_storage_account.main](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/storage_account) | data source |

## Inputs

| Name | Description | Type | Default | Required |
|------|-------------|------|---------|:--------:|
| <a name="input_common-tags"></a> [common-tags](#input\_common-tags) | Tags to be applied to all resources. | `map(string)` | n/a | yes |
| <a name="input_container-app-environment-id"></a> [container-app-environment-id](#input\_container-app-environment-id) | Container App Environment ID | `string` | n/a | yes |
| <a name="input_container-app-name-suffix"></a> [container-app-name-suffix](#input\_container-app-name-suffix) | Unique suffix for the Container App name. | `string` | n/a | yes |
| <a name="input_container-app-resource-group-name"></a> [container-app-resource-group-name](#input\_container-app-resource-group-name) | Name of the Azure Resource group in which Container App resources are to be created. | `string` | n/a | yes |
| <a name="input_core-db-domain-name-secret-name"></a> [core-db-domain-name-secret-name](#input\_core-db-domain-name-secret-name) | Name of the Azure Key Vault Secret for the DB host. | `string` | `"core-sql-domain-name"` | no |
| <a name="input_core-db-name-secret-name"></a> [core-db-name-secret-name](#input\_core-db-name-secret-name) | Name of the Azure Key Vault Secret for the DB name. | `string` | `"core-sql-db-name"` | no |
| <a name="input_core-db-password-secret-name"></a> [core-db-password-secret-name](#input\_core-db-password-secret-name) | Name of the Azure Key Vault Secret for the DB password. | `string` | `"core-sql-password"` | no |
| <a name="input_core-db-user-name-secret-name"></a> [core-db-user-name-secret-name](#input\_core-db-user-name-secret-name) | Name of the Azure Key Vault Secret for the DB username. | `string` | `"core-sql-user-name"` | no |
| <a name="input_core-resource-group-name"></a> [core-resource-group-name](#input\_core-resource-group-name) | Name of the core Azure Resource group from which various data sources are referenced. | `string` | n/a | yes |
| <a name="input_environment-prefix"></a> [environment-prefix](#input\_environment-prefix) | Prefix to be used for resources in the current environment. | `string` | n/a | yes |
| <a name="input_image-name"></a> [image-name](#input\_image-name) | Container image to be used for each worker. | `string` | n/a | yes |
| <a name="input_key-vault-name"></a> [key-vault-name](#input\_key-vault-name) | Azure Key Vault name. | `string` | n/a | yes |
| <a name="input_max-replicas"></a> [max-replicas](#input\_max-replicas) | The max. number of Container App replicas to launch. | `number` | n/a | yes |
| <a name="input_registry-name"></a> [registry-name](#input\_registry-name) | Azure Container Registry name. | `string` | n/a | yes |
| <a name="input_storage-account-name"></a> [storage-account-name](#input\_storage-account-name) | Azure Storage Account name. | `string` | n/a | yes |
| <a name="input_worker-queue-name"></a> [worker-queue-name](#input\_worker-queue-name) | Name of the queue which will trigger the worker. | `string` | n/a | yes |

## Outputs

No outputs.
<!-- END_TF_DOCS -->