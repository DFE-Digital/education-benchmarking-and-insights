# Databricks connectivity to s198

Databricks and FBIT are hosted in [CIP](https://docs.education.gov.uk/gettingStartedCIP/). To migrate our data preprocessing to Databricks, we needed to establish connectivity between Databricks's subscription and FBIT's in CIP, then use [Databricks Access Connectors](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/databricks_access_connector) to allow a storage container in FBIT to be mounted as a volume to Databricks. This allows Databricks users to write data to an FBIT storage container. This architecture is one of a couple that have been architecturally approved by DfE, the other being using Azure Data Factory. Here are some steps:

* Create a landing zone storage container with HNS enabled as is required for Databricks
* Raise a ticket for ADA To create Databricks access connectors for thise storage containers (in s101)
* Get CIP central services to configure the permissions cross-subscription to allow the connectors to write to the landing zone
* Test the solution

<!-- Leave the rest of this page blank -->
\newpage