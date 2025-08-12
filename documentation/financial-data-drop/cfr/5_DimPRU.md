# Pupil Referral Unit (Alternative Provision) - Dimension Data

The CFR data is processed along with a few dimension data to create the downstream dataset named `maintained_schools_master_list`. These dimension data are:

- Get Information About School (GIAS)
- School Census
- **Pupil Referral Unit (PRU) & Alternative Provision (AP)**
- Special Education Needs (SEN)
- General Hospital schools
- School Workforce Census

Pupil Referral Units (PRUs) are a type of Alternative Provision (AP) used to educate students who cannot attend mainstream school. They cater to students facing various challenges, including illness, exclusion, or those awaiting a mainstream school place. PRU details is collected during school census which is managed by the Department of Education.

## Getting PRU Data

- **Pupil Referral Dimension Data**

1. Connect to [iStore SQL Server](https://educationgovuk.sharepoint.com/:w:/r/sites/DfEFinancialBenchmarking/_layouts/15/Doc.aspx?sourcedoc=%7BA47507F6-2C23-487A-98EC-0B6C75A7471A%7D&file=CFR%20source%20data%20access%20request.docx&action=default&mobileredirect=true) and run below query using the reporting financial year database, for instance, for financial year 2024_2025, use [SchoolCensus2025_Spring_SPSSViews]

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

2. Import the result of the query into the local database, name the table `Dim_PRU_Pupils_SchoolLevel`

- **Pupil Referral Unit Slowly Changing Dimension Data**

tbc

### PRU Fields Data Type

| Column Name                       | Data type |
|-----------------------------------|-----------|
|LAEstab                            | float     |
|Phase                              | nvarchar  |
|Headcount                          | float     |
|FTE                                | float     |

<!-- Leave the rest of this page blank -->
\newpage
