# Prerequisite

1. SQL Server Access
2. Local Database
3. CFR Data Source Access

Create a copy of file with remove unwanted fields. The table below shows wanted columns

| Column Name                | Data type |
|----------------------------|-----------|
| URN                        | Int64     |
| Gender (name)              | string    |
| LA (code)                  | Int64     |
| LA (name)                  | string    |
| EstablishmentNumber        | Int64     |
| EstablishmentName          | string    |
| EstablishmentStatus (name) | string    |
| TypeOfEstablishment (code) | Int64     |
| TypeOfEstablishment (name) | string    |
| OpenDate                   | string    |
| CloseDate                  | string    |
| PhaseOfEducation (code)    | Int64     |
| PhaseOfEducation (name)    | string    |
| Boarders (code)            | Int64     |
| Boarders (name)            | string    |
| UrbanRural (code)          | Int64     |
| UrbanRural (name)          | string    |
| AdmissionsPolicy (code)    | Int64     |
| AdmissionsPolicy (name)    | string    |
| OfficialSixthForm (code)   | Int64     |
| StatutoryLowAge            | Int64     |
| StatutoryHighAge           | Int64     |

PS: GIAS cols are
Region
London Borough
