# Data Storage

## Introduction

Perhaps the most important decision of any service is how the data will be stored. The factors to be considered include:

* Storage costs
* Bandwidth costs
* Suitability to the service usage
* Ease of data ingestion 

### Option 1
Azure Cosmos DB, a fully-managed and distributed NoSQL database

### Option 2
Azure Managed SQL, a fully-managed and serverless distributed SQL database

### Option 3
Azure Blob Storage, an object storage solution

### Option 4
Azure Synapse, a Data Warehousing solution

## Evaluation

| Criteria | Comment | Cosmos DB | SQL | Blob Storage | Synapse | 
|:--------:|:--------|:---------------:|:-----------:|:-----------:|
| Suitability | The end data is very static. Once the benchmark and default comparator sets have been created, they do not change. The data is mainly non-relational | 4 | 3 | 4 | 3 |
| Ease of Data Ingestion | As we will be creating the ETL pipeline as well, this criteria is less critical | 3 | 3 | 3 | 3 |
| Storage Cost | We need to store 5 years' worth of data. Overall storage will be approximately 15MB for a parque  | 4 | 5 |
| Traffic Costs | Some rationale on why the score | 4 | 5 |
|Total||4|5|

## Conclusion

//TODO: Add a conclusion, summarising which technology will be selected and why

## Considerations on selected technology 

//TODO: Add considerations specific to the domain and the selected technology. This might include things like how it will scale or be secured in the environment specfic to this project.


Cosmos £0.007 per 100 RU per hour - £0.206 per gb per month - 2 free copies per month (periodic backup) - free caching
SQL 
Synapse £18.939 per TB per month, £4.118 per TB processed
Blob/  