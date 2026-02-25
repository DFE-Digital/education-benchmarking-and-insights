# Adding Data to Databricks

(As of Feb 2026)

[DfE's Databricks instance](https://analytical-data-access.education.gov.uk/) is an analytical data store — it doesn't sit behind any operational service. Instead, data from operational DfE systems (such as [COLLECT](https://www.gov.uk/government/publications/collect-guides-for-schools-and-local-authorities)) and external sources is replicated into Databricks via configured mirrors. The built-in data catalogue shows what's currently available.

## Requesting a New Dataset

If the data you need isn't in the catalogue but exists in a DfE database, contact the **Analytical Data Access (ADA)** team — they manage the Databricks service and configure new data mirrors.

Email `ADA.SUPPORT@education.gov.uk` with the following:

* **Database, schema, and table identifiers** for the data you want added
* A completed **Data Supply Agreement (DSA)**, submitted via the [DSA portal](https://apps.powerapps.com/play/e/default-fad277c9-c60a-4da1-b5f3-b3b8b34a82f9/a/b73d96d1-da0b-4045-87ad-21c0e2b01ff8?tenantId=fad277c9-c60a-4da1-b5f3-b3b8b34a82f9&source=portal&screenColor=rgba%280%2C%20176%2C%20240%2C%201%29&skipAppMetadata=true)

To complete the DSA, the dataset must be registered on the [Information Asset Register](https://educationgovuk.sharepoint.com/sites/iac/SitePages/Information-Asset-Register.aspx), and you'll need approval from the relevant **Information Asset Officer (IAO)**. ADA facilitate access but are not the data owners.

Once approved, ADA will set up an ADF pipeline that refreshes the data approximately daily.

## Manual Uploads

Smaller or static datasets don't need a formal mirror. These can be uploaded directly to Databricks without involving ADA. Bear in mind that manually uploaded datasets have no automated refresh — whoever uploads them is responsible for keeping them up to date.

<!-- Leave the rest of this page blank -->
\newpage
