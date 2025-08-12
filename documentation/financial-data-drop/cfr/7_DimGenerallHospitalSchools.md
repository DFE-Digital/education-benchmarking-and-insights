# General Hospital Schools - Dimension Data

The CFR data is processed along with a few dimension data to create the downstream dataset named `maintained_schools_master_list`. These dimension data are:

- Get Information About School (GIAS)
- School Census
- Pupil Referral Unit (PRU) & Alternative Provision (AP)
- Special Education Needs (SEN)
- **General Hospital schools**
- School Workforce Census

A list of general hospital schools is required. General hospital schools return data as part of the School Level Annual School Census (SLASC). There are only a few of these, typically attached to major children’s hospitals.  The data uses headcounts for both sole and dual registrations, this is due to funding which is by per place not per pupil.  

## Getting PRU Data

- **General Hospital Schools Dimension Data**

1. Connect to [SQL Server](https://educationgovuk.sharepoint.com/:w:/r/sites/DfEFinancialBenchmarking/_layouts/15/Doc.aspx?sourcedoc=%7BA47507F6-2C23-487A-98EC-0B6C75A7471A%7D&file=CFR%20source%20data%20access%20request.docx&action=default&mobileredirect=true) and run below query using the reporting financial year database, for instance, for 2024_2025 reporting academic year, use [GeneralHospitalSchool2025]

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

2. Import the result of the query into the local database, name the table `Dim_GenHospital`

- **General Hospital Schools Slowly Changing Dimension Data**

tbc

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
