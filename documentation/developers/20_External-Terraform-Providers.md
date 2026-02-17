# External Terraform Providers

When configuring terraform for core-resources to connect to other subscriptions, other providers e.g. "databricks_sub" can be defined in code.

```terraform
provider "azurerm" {
  alias           = "databricks_sub"
  subscription_id = var.configuration[var.environment].databricks_subscription_id
  features {}
}
```

## Databricks providers

Databricks is in another azure subscription to FBIT, so external providers are defined in code using subscription IDs. This allows our terraform to reference resources in another subscription. For FBIT, this is used to define IAM permissions for the databricks connector resources in s101 which facilitate data out from Databricks.

<!-- Leave the rest of this page blank -->
\newpage
