<!-- BEGIN_TF_DOCS -->
## Requirements

| Name | Version |
|------|---------|
| <a name="requirement_terraform"></a> [terraform](#requirement\_terraform) | >= 1.0 |
| <a name="requirement_azurerm"></a> [azurerm](#requirement\_azurerm) | ~> 3.32 |

## Providers

| Name | Version |
|------|---------|
| <a name="provider_azurerm"></a> [azurerm](#provider\_azurerm) | ~> 3.32 |

## Modules

No modules.

## Resources

| Name | Type |
|------|------|
| [azurerm_application_insights.application-insights](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/application_insights) | resource |
| [azurerm_key_vault.key-vault](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault) | resource |
| [azurerm_key_vault_access_policy.terraform_sp_access](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/key_vault_access_policy) | resource |
| [azurerm_monitor_action_group.action-group](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/monitor_action_group) | resource |
| [azurerm_monitor_smart_detector_alert_rule.failure-anomalies-detector](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/monitor_smart_detector_alert_rule) | resource |
| [azurerm_resource_group.resource-group](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/resource_group) | resource |
| [azurerm_client_config.client](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/data-sources/client_config) | data source |

## Inputs

| Name | Description | Type | Default | Required |
|------|-------------|------|---------|:--------:|
| <a name="input_cip-environment"></a> [cip-environment](#input\_cip-environment) | n/a | `any` | n/a | yes |
| <a name="input_environment"></a> [environment](#input\_environment) | n/a | `any` | n/a | yes |
| <a name="input_environment-prefix"></a> [environment-prefix](#input\_environment-prefix) | n/a | `any` | n/a | yes |
| <a name="input_location"></a> [location](#input\_location) | n/a | `any` | n/a | yes |

## Outputs

No outputs.
<!-- END_TF_DOCS -->