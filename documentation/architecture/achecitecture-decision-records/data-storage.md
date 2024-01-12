# Data Storage

## Context and Problem Statement

Perhaps the most important decision of any service is how the data will be stored. The factors to be considered include:

* Storage costs
* Bandwidth costs
* Suitability to the service usage
* Ease of data ingestion 

## Considered Options
- Azure Cosmos DB, a fully-managed and distributed NoSQL database
- Azure Managed SQL, a fully-managed and serverless distributed SQL database
- Azure Blob Storage, an object storage solution
- Azure Synapse, a Data Warehousing solution

### Evaluation

| Criteria | Comment | Cosmos DB | SQL | Blob Storage | Synapse | 
|:--------:|:--------|:---------------:|:-----------:|:-----------:|:-----------:|
| Suitability | The end data is very static. Once the benchmark and default comparator sets have been created, they do not change. The data is mainly non-relational | 4 | 3 | 3 | 3 |
| Ease of Data Ingestion | As we will be creating the ETL pipeline as well, this criteria is less critical, although this include understanding of the current piupelines as well | 4 | 3 | 3 | 3 |
| Storage Cost | We need to store 5 years' worth of data. Overall storage will be approximately 15MB for a parque  | 4 | 3 | 4 | 2 |
| **Total** || **12** | **9** | **10** | **8** |

## Decision Outcome

Based on the analysis above, we have chosen CosmosDB as the data storage medium for the project. An additional factor in the decision was that, as we were more confident in the current data structure and ingestion pipeline of the incumbent SFB solution, we can use the current SFB CosmosDB documents as the data source whilst we work on the Beta service.

### Considerations on selected technology 

During development, the CosmosDB will be provisioned using Provisioned Throughput (Autoscale) capacity mode. As the system evolves, this will be re-evaluated to determine if Serverless will provide a better combination of performance and cost-effectiveness.
