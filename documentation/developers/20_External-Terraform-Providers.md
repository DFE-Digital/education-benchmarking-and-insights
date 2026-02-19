# External Terraform Providers

When configuring terraform to connect to resources in other subscriptions, this counts as another provider. For example, the subscriptions which host databricks are registered as another azure sub and aliased "databricks_sub".

```terraform
provider "azurerm" {
  alias           = "databricks_sub"
  subscription_id = var.configuration[var.environment].databricks_subscription_id
  features {}
}
```

## Databricks providers

Databricks is in another azure subscription to FBIT. Multiple providers are referenced in terraform using subscription IDs. For FBIT, this is used to define IAM permissions for the databricks connector resources in s101 which facilitate data out from Databricks.

<!-- Leave the rest of this page blank -->
\newpage
