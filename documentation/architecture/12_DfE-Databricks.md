# Decision - 0011 - Data pipeline Databricks conversion

## Context and Problem Statement

DfE's SQL server databases are being turned off and migrated to Databricks by the end of 2026. Our data pipelines consume some of this data, so we will have to adapt our pipelines to consume from Databricks. The presence of databricks allows an opportunity for updating of our pipelines - we could offload compute into databricks as an example of a good databricks pipeline for DfE.

## Decision Drivers

* The SQL server databases are being turned off and migrated to Databricks by the end of 2026.
* A lot of our source data including ancillary data is on Databricks already. More is scheduled to be added and we can request missing data.
* The current SQL to extract data will need to be rewritten to t-SQL as a Databricks job.
* We will have access to Databricks compute; although there are pre-allocated tokens allocated for funding, there are open questions around how this will be managed in the longer term.
* The FBIT service can work from a csv pushed by a job from Databricks, or the Databricks API allows the FBIT application to trigger an export job.
* Data export is currently tracked by Databricks unity catalogue, and is unrestricted.
* Our data size is not large, the pipeline is not run often, and it doesn't take a lot of time to run. So the performance and scalability of the transformations in Databricks, while an improvement, isn't very relevant.

## Considered Options

A basic structure of the data pipeline illustrates the degrees of databricks involvement:
Database data / Static data -> filtered, etc. -> merged/processed -> comparator-sets -> RAG
Bronze                         silver            gold                data-mart

### **Option 1: Change database sources to Databricks**

Change the existing database data sources to Databricks with no changes to the rest of the pipeline. This will need to happen at the very least, as the old databases are getting switched off and migrated to Databricks (as far as we know, this is just AnM). The SQL to extract pipeline data for us will need to be rewritten to t-SQL (this might not be a large task).

#### Pros

* **Simple & Fast to Implement:** This requires minimal code changes.
* **Lowest Risk:** The core processing logic remains untouched.
* **Directly Addresses the Core Problem:** It solves the immediate issue of the SQL Server decommissioning with the least possible effort.

#### Cons

* **Missed Opportunity:** Fails to take advantage of Databricks' processing power and features, ignoring the opportunity for architectural improvement.
* **Data out costs:** Pulling large volumes of raw (Bronze) data out of Databricks  is an anti-pattern. This can lead to high data egress costs and performance bottlenecks.
* **Misaligned with Strategy:** Does not align with the wider organisational strategy of using Databricks as a primary platform for data pipelines. This could lead to the pipeline being considered "legacy" in the future.
* **Scalability Bottleneck:** The existing FBIT service remains the bottleneck for performance and scalability, which Databricks is designed to solve.

### **Option 2: Change all data sources to Databricks**

Change all data sources to Databricks (static files/ancillary data included) with no changes to the rest of the pipeline. We've seen a list of the data sources to be uploaded to databricks and the large majority of our ancillary data will be available. There might still be a few sources which are uploaded through blob storage, or re need to request to be made available in databricks.

#### Pros

* **Consolidates All Data Sources:** Creates a single source of truth for all pipeline data (both database and static files). This simplifies data management, governance, and discovery.
* **Simplified Ingestion Logic:** The FBIT pipeline only needs to know how to connect to one type of source system (Databricks), simplifying its ingestion code.

#### Cons

* **Delta lake datasets updates:** Who updates the ancillary data from gov.uk sources when it gets updated? Do we prompt the ADA team to make the new release available?
* **Still Inefficient:** Suffers from the same major drawback as Option 1: all data is pulled out of Databricks for external processing, which is inefficient and costly.
* **Marginal Improvement:** Offers only a small architectural improvement over Option 1 for a non-trivial amount of work.

### **Option 3: Preprocess data in Databricks**

Source data from and perform up until the gold layer of compute in Databricks. Calculate comparator sets in FBIT.

#### Pros

* **Leverages Databricks' Strengths:** Uses Databricks for what it excels at: large-scale, distributed data transformation (ETL/ELT). This makes the most performance-intensive part of the pipeline highly scalable. We'd also be more trackable via the unity catalogue.
* **Fewer expensive data transfers:** The data pulled into FBIT is smaller and pre-aggregated (gold layer).
* **Phased Modernisation:** Represents a good "best of both worlds" compromise. It modernises the core data processing while retaining specialised or complex logic ("comparator sets") in the existing, stable FBIT service.
* **Aligns with Medallion Architecture:** This approach is a textbook implementation of the Bronze-Silver-Gold medallion architecture within a lakehouse platform which can be an example in the rest of DfE

#### Cons

* **Stray files not in databricks** We might need to still drag and drop new releases into databricks to make it all work, like we do with the current pipeline.
* **Architectural Complexity:** The pipeline logic is now split across two systems (Databricks and FBIT). This can make orchestration, end-to-end monitoring, and debugging more complex.
**Recommendation**
Option 2: Change all data sources we can to Databricks -lots of our current ancillary data is on there already.
Low risk and easy. Not to rule out option 3 - When the databricks instance is used more and DfE have a vision for it to start handling transformation workloads we can start to think about migrating data pipelines on to it.
For our multiple data sources we can migrate them at different times based on if they are in databricks:

* Anything that is already in databricks we will look to clone extract logic asap - as new data drops come in through the year we can then validate that the migrated pipeline produces the same extract as the old pipeline and change it over.
* Anything not already in databricks will of course have to wait for this same process. If data sources we use are not in the ADA migration roadmap, we'll have to try to get them to add it to their migration roadmap.
