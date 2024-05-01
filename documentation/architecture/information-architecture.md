# Information Architecture

## Logical Data Model

![Logical Data Model](../images/Logical-Data-Model.png)

### Entities

| Entity             | Scope         | Repository  | Description                                                                                                 |
|:-------------------|:--------------|:------------|:------------------------------------------------------------------------------------------------------------|
| **Academies**          | Transactional | Platform DB | Holds information specific to an academies, including staff, pupil and financial                            |
| **Comparator Sets**    | Transactional | Platform DB | Holds the sets of similar schools/academies that each school is compared to                                 |
| **Fibre Directory**    | Transactional | Platform DB | Mapping directory                                                                                           |
| **Financial Plans**    | Transactional | Platform DB | Holds financial plans for a school or academy designed using the Curriculum & Financial Planning (CFP) tool |
| **Floor Area**         | Transactional | Platform DB | Holds building information specific to a school or academy                                                  |
| **GIAS**               | Transactional | Platform DB | Holds information for a school, academy or trust taken from the Get Information About Schools service       |
| **Maintained Schools** | Transactional | Platform DB | Holds information specific to an LEA maintained school, including staff, pupils and finances                |
| **MAT Allocs**         | Transactional | Platform DB | Holds information about multi-academy trust allocations                                                     |
| **MAT Central**        | Transactional | Platform DB | Holds information about centrally assigned multi-academy trust financials                                   |
| **MAT Overview**       | Transactional | Platform DB | Holds general information regarding multi-academy trusts                                                    |
| **MAT Totals**         | Transactional | Platform DB | Holds aggregate information specific to multi-academy trusts                                                |

#### Schemas
Comparator Sets - Comparator sets are currently taken from the VMFI database, and transformed to a more usable format for the Non-SQL CosmosDB. Each school has 4 distinct comparator sets built, based upon a peer group and a cost group as described below, and this type is used as the partition key in the Comparator Sets.

| Peer Group | Cost Group | Description |
|:-----------|:-----------|:------------|
| Default | Pupil | Comparator schools are of the same type (maintained or academy) as the base school, and are determined by pupil charateristics |
| Default | Area | Comparator schools are of the same type (maintained or academy) as the base school, and are determined by building charateristics |
| Mixed | Pupil | Comparator schools are a mix of maintained and academy schools, and are determined by pupil charateristics |
| Mixed | Area | Comparator schools are a mix of maintained and academy schools, and are determined by building charateristics |

 

### Events
TBD

## Data Repositories

| Name | Technology             |
|:-----|:-----------------------|
| Platform DB | Azure CosmosDB |
| Operational DB | Azure SQL Server       |
| Platform Storage | Azure Blob storage |
| Operational Monitoring | Azure App Insights |

