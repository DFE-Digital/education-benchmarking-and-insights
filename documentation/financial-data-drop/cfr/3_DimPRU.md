# Pupil Referral Unit (Alternative Provision) - Dimension Data

The CFR data is processed along with a few dimension data to create the downstream dataset named `maintained_schools_master_list` and an accompanying transparency file. These dimension data are:

- **Pupil Referral Unit (PRU)**
- General Hospital schools
- Get Information About School (GIAS)
- School Census
- Special Education Needs (SEN)
- School Workforce Census

Pupil Referral Units (PRUs) are a type of Alternative Provision (AP) used to educate students who cannot attend mainstream school. They cater to students facing various challenges, including illness, exclusion, or those awaiting a mainstream school place. PRU details is collected during school census which is managed by the Department of Education.

## Getting Pupil Referral Unit Data

### Pupil Referral Unit Dimension Data

1. Connect to [iStore SQL Server](https://educationgovuk.sharepoint.com/:w:/r/sites/DfEFinancialBenchmarking/_layouts/15/Doc.aspx?sourcedoc=%7BA47507F6-2C23-487A-98EC-0B6C75A7471A%7D&file=CFR%20source%20data%20access%20request.docx&action=default&mobileredirect=true) to run the below query against the database that references the end of the reporting academic year. For instance, when generating 2024-2025 CFR downstream dataset, use [SchoolCensus2025_Spring_SPSSViews]

    ```sql
    SELECT a.[LAEstab]  
        ,a.[Phase]
        ,COUNT (distinct b.[UPN]) as 'Headcount'
        ,SUM (b.[HNentitlement]) as 'FTE' 
    FROM [SchoolCensus2025_Spring_SPSSViews].[dbo].[Approved_School] as a  
    LEFT JOIN [SchoolCensus2025_Spring_SPSSViews].[dbo].[Approved_PupilOnRoll] as b
    ON a.[LAEstab] = b.[LAEstab] 
    WHERE a.[Phase] = 'PR' 
    GROUP BY a.[LAEstab], a.[Phase] 
    ```

2. Either by using a database GUI Tool or CSV export, ingest result of the query into the created local database. Name the table `Dim_PRU_Pupils_SchoolLevel_20YY` where 20YY represents the end of the reporting academic year. For instance `Dim_PRU_Pupils_SchoolLevel_2025` for 2024-2025 academic year.

### Pupil Referral Unit - Slowly Changing Dimension (SCD) Data

1. Run the below query against the database that references the end of the previous academic year (end of reporting academic year minus one), for instance, when generating PRU SCD data to create 2024-2025 CFR downstream dataset, use [SchoolCensus2024_Spring_SPSSViews]

    ```sql
    SELECT a.[LAEstab]  
        ,a.[Phase]
        ,COUNT (distinct b.[UPN]) as 'Headcount'
        ,SUM (b.[HNentitlement]) as 'FTE' 
    FROM [SchoolCensus2024_Spring_SPSSViews].[dbo].[Approved_School] as a  
    LEFT JOIN [SchoolCensus2024_Spring_SPSSViews].[dbo].[Approved_PupilOnRoll] as b
    ON a.[LAEstab] = b.[LAEstab] 
    WHERE a.[Phase] = 'PR' 
    GROUP BY a.[LAEstab], a.[Phase] 
    ```

2. Either by using a database GUI Tool or CSV export, ingest result of the query into the created local database. Name the table `Dim_PRU_Pupils_SchoolLevel_20YY` where 20YY represents end of previous academic year (end of reporting academic year minus one). For instance, when generating PRU SCD data to create 2024-2025 CFR downstream dataset, use `Dim_PRU_Pupils_SchoolLevel_2024`

### PRU Fields Data Type

| Column Name                       | Data type |
|-----------------------------------|-----------|
|LAEstab                            | float     |
|Phase                              | nvarchar  |
|Headcount                          | float     |
|FTE                                | float     |

<!-- Leave the rest of this page blank -->
\newpage
