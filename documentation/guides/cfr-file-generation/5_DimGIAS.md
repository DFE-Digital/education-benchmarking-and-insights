# GIAS - Dimension Data

The CFR data is processed along with a few dimension data to create the downstream dataset named `maintained_schools_master_list` and an accompanying transparency file. These dimension data are:

- Pupil Referral Unit (PRU) & Alternative Provision (AP)
- General Hospital schools
- **Get Information About School (GIAS)**
- School Census
- Special Education Needs (SEN)
- School Workforce Census

Get Information about Schools (GIAS) is the Department for Education’s register for several school establishment types and where information on other school organisations is recorded and maintained. This data is published in the [GOV.UK Get Information about Schools website](https://www.get-information-schools.service.gov.uk/).

## Getting GIAS Data

1. Go to GIAS [download page](https://www.get-information-schools.service.gov.uk/Downloads).

2. Have `Latest` selected as option for download.

3. Check `Establishment fields CSV, xx.xxMB` where xx.xx represent size of file for instance `Establishment fields CSV, 60.84 MB`.

4. Scroll down to the bottom of page to select `Download selected file`.

5. Wait for the download generation to be completed, afterwards click on `Results.zip` to action download.

6. Navigate to `extract` folder within local downloaded folder to view the `edubasealldataYYYMMDD.csv` file.

7. Open the file and proceed to remove all but the referenced columns/fields in the **GIAS Fields Data Type** subsection of this documentation, failure to do so will lead to data ingestion errors.

8. At the end of sheet (column BT), create an additional field called `LAEstab` which is a concatenate of `LA (code)` and `EstablishmentNumber` values.

### Flat File Ingestion into CFRyy Local Database

1. Using a database GUI Tool, ingest flat file as a table into the newly created local database.
    - Confirm destination table name is `GIASYYYYMMDD` where YYYYMMDD is same the year, month and date of `edubasealldataYYYMMDD.csv` file
    - Confirm schema name for both destination tables is `dbo`
    - In the Modify Columns tab, select `Allow Nulls` option to accept null values for all the fields
    - In the Modify Columns tab, confirm column data type matches with GIAS Fields Data Type as detailed below

2. Using SQL statement option within the database GUI Tool, create two additional tables based on `EstablishmentStatus (name)` field. Edit both local database and table name to reflect current reporting year.

     ```sql
     --Save table name as Dim_GIAS_OpenSchools_20YY where 20YY rep end of reporting academic year. For instance, Dim_GIAS_OpenSchools_2025 for 2024-2024 academic year
     SELECT * 
     FROM [CFR25].[dbo].[GIAS20250722]
     WHERE [EstablishmentStatus (name)] IN ('Open', 'Open, but proposed to close', 'Proposed to open')
      ```

     ```sql
     --Save table name as Dim_GIAS_ClosedSchools_20YY where 20YY rep end of reporting academic year. For instance, Dim_GIAS_ClosedSchools_2025 for 2024-2024 academic year
     SELECT * 
     FROM [CFR25].[dbo].[GIAS20250722]
     WHERE [EstablishmentStatus (name)] = 'Closed'
      ```

### GIAS Fields Data Type

| Column Name                       | Data type |
|-----------------------------------|-----------|
|URN                                | float     |
|LA (code)                          | float     |
|LA (name)                          | nvarchar  |
|EstablishmentNumber                | float     |
|EstablishmentName                  | nvarchar  |
|TypeOfEstablishment (code)         | float     |
|TypeOfEstablishment (name)         | nvarchar  |
|EstablishmentTypeGroup (code)      | float     |
|EstablishmentTypeGroup (name)      | nvarchar  |
|EstablishmentStatus (code)         | float     |
|EstablishmentStatus (name)         | nvarchar  |
|OpenDate                           | datetime  |
|CloseDate                          | datetime  |
|PhaseOfEducation (code)            | float     |
|PhaseOfEducation (name)            | nvarchar  |
|StatutoryLowAge                    | float     |
|StatutoryHighAge                   | float     |
|Boarders (code)                    | float     |
|Boarders (name)                    | nvarchar  |
|NurseryProvision (name)            | nvarchar  |
|OfficialSixthForm (code)           | float     |
|OfficialSixthForm (name)           | nvarchar  |
|Gender (code)                      | float     |
|Gender (name)                      | nvarchar  |
|ReligiousCharacter (code)          | float     |
|AdmissionsPolicy (code)            | float     |
|AdmissionsPolicy (name)            | nvarchar  |
|SchoolCapacity                     | float     |
|CensusDate                         | datetime  |
|NumberOfPupils                     | float     |
|PercentageFSM                      | float     |
|FederationFlag (name)              | nvarchar  |
|Federations (code)                 | nvarchar  |
|Federations (name)                 | nvarchar  |
|UKPRN                              | float     |
|LastChangedDate                    | datetime  |
|Street                             | nvarchar  |
|Locality                           | nvarchar  |
|Address3                           | nvarchar  |
|Town                               | nvarchar  |
|County (name)                      | nvarchar  |
|Postcode                           | nvarchar  |
|SENPRU (name)                      | nvarchar  |
|TypeOfResourcedProvision (name)    | nvarchar  |
|ResourcedProvisionOnRoll           | nvarchar  |
|ResourcedProvisionCapacity         | nvarchar  |
|SenUnitOnRoll                      | float     |
|SenUnitCapacity                    | float     |
|GOR (code)                         | nvarchar  |
|GOR (name)                         | nvarchar  |
|DistrictAdministrative (code)      | nvarchar  |
|DistrictAdministrative (name)      | nvarchar  |
|AdministrativeWard (code)          | nvarchar  |
|AdministrativeWard (name)          | nvarchar  |
|ParliamentaryConstituency (code)   | nvarchar  |
|ParliamentaryConstituency (name)   | nvarchar  |
|UrbanRural (code)                  | nvarchar  |
|UrbanRural (name)                  | nvarchar  |
|Easting                            | float     |
|Northing                           | float     |
|MSOA (name)                        | nvarchar  |
|LSOA (name)                        | nvarchar  |
|SENStat                            | float     |
|SENNoStat                          | float     |
|BoardingEstablishment (name)       | nvarchar  |
|PropsName                          | nvarchar  |
|UPRN                               | float     |
|SiteName                           | nvarchar  |
|MSOA (code)                        | nvarchar  |
|LSOA (code)                        | nvarchar  |
|FSM                                | float     |
|**LAEstab**                        | float     |

<!-- Leave the rest of this page blank -->
\newpage
