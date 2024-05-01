# Development Data
## Context and Problem Statement

The current systems that the new service is to replace store their data is very different ways. SFB stores data in CosmosDb collections, whereas VMFI is backed by a SQL database. For development purposes, a "lift and shift" of the current production data was required. The main considerations for the data during development are:

* Confidence in the accuracy of the data
* Coverage of the data
* Suitability of the storage medium to the data
* Ease of transfer

## Considered Options

The options considered were:

* SFB - the data held in SFB 
* SQL Server - a relational database

| Criteria | Comment | CosmosDB | SQL Server |
|:--------:|:--------|:---------------:|:-----------:|
| Confidence | The current databases are fed by a complex ETL pipeline. Currently, the SFB pipeline has been run succesfully, whereas the VMFI pipeline has been more troublesome | 4 | 2 |
| Coverage | VMFI held all the data required, whereas SFB did not hold building data | 3 | 4|
| Suitability | The school data is diverse, but there is also trusts, academies, comparator sets, etc. that need to be considered. The data is less structired and each entity has slightly different data. | 4 | 2|
| Ease of transfer | Each type is simple to transfer data to the same infrastructure type | 3 | 3 |
|Total||14|11|

## Decision Outcome

The major difference between using the SFB & VMFI data was the way comparator sets were calculated (VMFI method is preferred), and that VMFI held building data that SFB didn't. However, the other factors, especially confidence in the accuracy of the data and its structure, meant that SFB data held in CosmosDB is to be used as that data repository for the new service. Additional data from VMFI will be imported as needed.
