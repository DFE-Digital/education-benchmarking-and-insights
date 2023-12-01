# Information Architecture

## Logical Data Model

![Logical Data Model](images/Logical-Data-Model.png)

### Entities

| Entity               | Scope          | Repository  | Description                                                                                      |
|:---------------------|:---------------|:------------|:-------------------------------------------------------------------------------------------------|
| School Details       | Transactional  | Platform DB | Holds general information specific to a school, academy or trust                                 |
| Federation           | Transactional  | Platform DB |                                                                                                  |
| Multi Academy Trusts | Transactional  | Platform DB |                                                                                                  |
| Benchmarking Group   | Transactional  | Platform DB | Holds the set of similar establishments that a school, academy or trust will be compared against |
| Workforce            | Transactional  | Platform DB | Holds information specific to the staff at a school, academy or trust                            |
| Pupils               | Transactional  | Platform DB | Holds information specific to the pupils at a school, academy or trust                           |
| SEN                  | Transactional  | Platform DB | Holds information specific to Special Educational Needs for a school, academy or trust           |
| Academic Performance | Transactional  | Platform DB | Holds information specific to the academic performance of a school, academy or trust             |
| Submission Details   | Transactional  | Platform DB | Holds information specific to the particular data submission for a school, academy or trust      |
| Income | Transactional  | Platform DB | Holds information specific to income for a school, academy or trust                              |
| Expenditure | Transactional  | Platform DB | Holds information specific to expenditure for a school, academy or trust                         |
| Forecasting | Transactional | Platform DB | Holds information specific to future budgets for a school, academy or trust                      |
| Event | Transactional | Platform DB | An event raised from a user or system interaction                                                |
| Audit Header | Reporting | Operational DB | An audit header record                                                                           |
| Audit Detail | Reporting | Operational DB | An audit detail record |

#### Schemas
TBD

### Events
TBD

## Data Repositories

| Name | Technology             |
|:-----|:-----------------------|
| Platform DB | Azure CosmosDB |
| Operational DB | Azure SQL Server       |
| Platform Storage | Azure Blob storage |
| Operational Monitoring | Azure App Insights |

