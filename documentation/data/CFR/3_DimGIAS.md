# GIAS - Dimension Data

The CFR data is processed along with dimension data to create upstream dataset named `maintained_schools_master_list`. These dimension data are:

- **Get Information About School (GIAS)**
- School Census
- Pupil Referral Unit (PRU) & Alternative Provision (AP)
- Special Education Needs (SEN)
- General Hospital schools
- School Workforce Census

Get Information about Schools (GIAS) is the Department for Education’s register for several school establishment types and where information on other school organisations is recorded and maintained. This data is published in the [GOV.UK Get Information about Schools website](https://www.get-information-schools.service.gov.uk/).

## Getting GIAS Data

1. Go to GIAS [download page](https://www.get-information-schools.service.gov.uk/Downloads)
2. Have `Latest` selected as option
3. Check `Establishment fields CSV, xx.xxMB` where xx.xx represent size of file for instance `Establishment fields CSV, 60.84 MB`
4. Scroll down to the bottom of page to select `Download selected file`
5. Wait for the download generation to be completed, afterwards click on `Results.zip` to action download
6. Navigate to `extract` folder within local downloaded folder to view the `edubasealldataYYYMMDD.csv` file and rename to `GIAS_YYYYMMDD` naming convention.
7. Open the now `GIAS_YYYYMMDD.csv` file two create two additional copies based on `EstablishmentStatus (name)` field.

    - The file WHERE `EstablishmentStatus (name)` = `Open`, `Open, but proposed to close` and `Proposed to open` should be saved as **Dim_GIAS_OpenSchools.csv**
    - The file WHERE `EstablishmentStatus (name)` = `Closed` should be saved as **Dim_GIAS_ClosedSchools.csv**

8. At the end of both `Dim_GIAS_OpenSchools` and `Dim_GIAS_ClosedSchools`, create an additional field called `LAEstab` which is a concatenate of `LA (code)` and `EstablishmentNumber`
9. In total, we should have the three files below
    - `GIAS_YYYYMMDD.csv`
    - `Dim_GIAS_OpenSchools.csv`
    - `Dim_GIAS_ClosedSchools.csv`
10. Import all three flat files as respective tables into `CFRYY` database keeping the naming conversion.

### GIAS Fields and Data Type

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
