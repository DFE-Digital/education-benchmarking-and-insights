# Databricks Connectivity to s198

Databricks and FBIT are both hosted in [CIP](https://docs.education.gov.uk/gettingStartedCIP/). To move our data preprocessing into Databricks, we needed two things: network connectivity between the Databricks and FBIT subscriptions in CIP, and a way for Databricks to write to FBIT storage. The latter is achieved using [Databricks Access Connectors](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs/resources/databricks_access_connector), which allow an FBIT storage container to be mounted as a Databricks volume.

This is one of two architecturally approved patterns I know of for this use case in DfE — the other being Azure Data Factory.

## Setup Steps

1. **Create a landing zone storage container** in FBIT with HNS enabled (required for Databricks volume mounting)
2. **Raise a ticket with ADA** to create Databricks Access Connectors for the storage containers (done from the s101 subscription)
3. **Request cross-subscription permissions** from CIP central services to allow the connectors to write to the landing zone
4. **Test the solution**

<!-- Leave the rest of this page blank -->
\newpage
