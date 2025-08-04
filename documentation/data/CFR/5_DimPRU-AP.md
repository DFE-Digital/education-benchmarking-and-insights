# Pupil Referral Unit & Alternative Provision - Dimension Data

The CFR data is processed along with dimension data to create Financial Benchmarking and Insights Tool (FBIT) upstream dataset named `maintained_schools_master_list`. These dimension data are:

- Get Information About School (GIAS)
- School Census
- **Pupil Referral Unit (PRU) & Alternative Provision (AP)**
- Special Education Needs (SEN)
- General Hospital schools
- School Workforce Census

## Getting PRU Data

1. Connect to `T1PRISTOAOL01` SQL Server and run below query

    ```sql
    SELECT a.[LAEstab]  
        ,a.[Phase]
        ,COUNT (distinct b.[UPN]) as 'Headcount'
        ,SUM (b.[HNentitlement]) as 'FTE' 
    FROM [SchoolCensus2025_Spring_SPSSViews].[dbo].[Approved_School] as a  
    LEFT JOIN [SchoolCensus2025_Spring_SPSSViews].[dbo].[Approved_PupilOnRoll] as b  ON a.[LAEstab] = b.[LAEstab] 
    WHERE a.[Phase] = 'PR' 
    GROUP BY a.[LAEstab], a.[Phase] 
    ```

2. Import the result of the query into the local database, name the table `Context_PRU_Pupils_SchoolLevel_20YY` where YY represents end of financial year, for instance `Context_PRU_Pupils_SchoolLevel_2025`

### PRU Fields and Data Type

| Column Name                       | Data type |
|-----------------------------------|-----------|
|LAEstab                            | float     |
|Phase                              | nvarchar  |
|Headcount                          | float     |
|FTE                                | float     |
