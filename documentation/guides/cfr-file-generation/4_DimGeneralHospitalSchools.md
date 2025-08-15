# General Hospital Schools - Dimension Data

The CFR data is processed along with a few dimension data to create the downstream dataset named `maintained_schools_master_list` and an accompanying transparency file. These dimension data are:

- Pupil Referral Unit (PRU) & Alternative Provision (AP)
- **General Hospital schools**
- Get Information About School (GIAS)
- School Census
- Special Education Needs (SEN)
- School Workforce Census

A list of general hospital schools is required. General hospital schools return data as part of the School Level Annual School Census (SLASC). There are only a few of these, typically attached to major children’s hospitals.  The data uses headcounts for both sole and dual registrations, this is due to funding which is by per place not per pupil.  

## Getting General Hospital Schools Data

### General Hospital Schools Dimension Data

1. Connect to [iStore SQL Server](https://educationgovuk.sharepoint.com/:w:/r/sites/DfEFinancialBenchmarking/_layouts/15/Doc.aspx?sourcedoc=%7BA47507F6-2C23-487A-98EC-0B6C75A7471A%7D&file=CFR%20source%20data%20access%20request.docx&action=default&mobileredirect=true) to run the below query against the database that references the end of the reporting academic year. For instance, when generating 2024-2025 CFR downstream dataset, use [GeneralHospitalSchool2025]

    ```sql
    SELECT a.[LAESTAB]
    ,a.[SRBoyTot]
    ,a.[SRGirlTot]
    ,b.[DRBoyTot]
    ,b.[DRGirlTot]
    ,a.[SRBoyTot] + a.[SRGirlTot] + b.[DRBoyTot] + b.[DRGirlTot] as [TotalHeadcount]
    ,'GHS' as [GHS_indicator] 
    FROM [GeneralHospitalSchool2025].[dbo].[GEN_Approved_SoleRegByAge] as a
    LEFT JOIN [GeneralHospitalSchool2025].[dbo].[GEN_Approved_DualRegByAge] as b
    ON a.[LA] = b.[LA] AND a.[Estab] = b.[Estab]
    ```

2. Either by using a database GUI Tool or CSV export, ingest the result of the query into the created local database. Name the table `Dim_GenHospital_20YY` where 20YY represents the end of the reporting academic year. For instance `Dim_GenHospital_2025` for 2024-2025 academic year.

### General Hospital Schools Slowly Changing Dimension (SCD) Data

1. Run the below query against the database that references the end of the previous academic year (end of reporting academic year minus one), for instance, when generating SCD data to create 2024-2025 CFR downstream dataset, use [GeneralHospitalSchool2024]

    ```sql
    SELECT a.[LAESTAB]
    ,a.[SRBoyTot]
    ,a.[SRGirlTot]
    ,b.[DRBoyTot]
    ,b.[DRGirlTot]
    ,a.[SRBoyTot] + a.[SRGirlTot] + b.[DRBoyTot] + b.[DRGirlTot] as [TotalHeadcount]
    ,'GHS' as [GHS_indicator] 
    FROM [GeneralHospitalSchool2024].[dbo].[GEN_Approved_SoleRegByAge] as a
    LEFT JOIN [GeneralHospitalSchool2024].[dbo].[GEN_Approved_DualRegByAge] as b
    ON a.[LA] = b.[LA] AND a.[Estab] = b.[Estab]
    ```

2. Either by using a database GUI Tool or CSV export, ingest result of the query into the created local database. Name the table `Dim_GenHospital_20YY` where 20YY represents of previous academic year (end of reporting academic year minus one). For instance, when generating General Hospital Schools SCD data to create 2024-2025 CFR downstream dataset, use `Dim_GenHospital_2024`

### General Hospital Schools Fields Data Type

| Column Name                       | Data type |
|-----------------------------------|-----------|
|LAEstab                            |float      |
|SRBoy                              |float      |
|SRGirl                             |float      |
|DRBoy                              |float      |
|DRGirl                             |float      |
|TotalHeadcount                     |float      |
|GHSIndicator                       |varchar    |

<!-- Leave the rest of this page blank -->
\newpage
