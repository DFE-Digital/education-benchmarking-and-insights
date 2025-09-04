# School Workforce Census - Dimension Data

The CFR data is processed along with a few dimension data to create the downstream dataset named `maintained_schools_master_list` and an accompanying transparency file. These dimension data are:

- Pupil Referral Unit (PRU) & Alternative Provision (AP)
- School Census
- General Hospital schools
- Get Information About School (GIAS)
- Special Education Needs (SEN)
- **School Workforce Census**

School workforce data is taken from the School Census produced by the Department of Education and published in the [School workforce in England - GOV.UK School Education statistic website](https://explore-education-statistics.service.gov.uk/find-statistics/school-workforce-in-england); this data is updated annually by July/August.

## Getting School Workforce Census Data

### School Workforce Census Data**

1. Click on [URL link](https://content.explore-education-statistics.service.gov.uk/api/releases/ba5318f9-2f18-4ef5-8c71-a4db8546758c/files?fromPage=ReleaseUsefulInfo) to download `school-workforce-in-england_YYYY` zipped folder to local folder, where YYYY represents previous calendar year (end of reporting year minus one). For instance, `school-workforce-in-england_2024` would be used to generate 2024-2025 CFR downstream dataset.

2. In the `supporting-files` sub-folder of the `school-workforce-in-england_YYYY` zipped folder, access the
`School_Tables_School_Workforce_Census` file.

3. Delete the first eight (8) rows making the ninth (9th) row the header

4. Clean data by replacing any value returned as `x`,`c` and `z` to `0.00` (zero)

5. Save As file to `Dim_Workforce_20YY.csv` where 20YY is previous calendar year (end of reporting year minus one). For instance, Save As `Dim_Workforce_2024.csv` when generating 2024-2025 CFR downstream dataset.

### School Workforce Census Slowly Changing Dimension Data**

1. In the page view of the [School workforce in England - GOV.UK School Education statistic website](https://explore-education-statistics.service.gov.uk/find-statistics/school-workforce-in-england), navigate to `Releases in this series` to expand on the `View releases` button where last two calendar years (end of reporting year minus two) data can be accessed.

2. For instance, to obtain the School Workforce Census SCD data for generating 2024-2025 CFR downstream dataset, click on the `Reporting year 2023` to download `school-workforce-in-england_2023` zipped folder to local folder.

3. Repeat step 3 and 4 as listed above.

4. Save and rename file to `Dim_Workforce_20YY.csv` where 20YY is the downloaded calendar year, for instance, `Dim_Workforce_2023.csv` generating 2024-2025 CFR downstream dataset.

### Flat File Ingestion into CFRyy Local Database

Using a database GUI Tool, ingest both flat files as a tables into the created local database
    - Confirm that the two destination table names reflect their respective reporting academic years
    - Confirm schema name for both destination tables is `dbo`
    - In the Modify Columns tab, confirm column data type matches with GIAS Fields Data Type as detailed below

### School Workforce Census Data Type

| Column Name                       | Data type |
|-----------------------------------|-----------|
|LA number                          |  float    |
|School code                        |  float    |
|LAEstab                            |  float    |
|LA name                            |  nvarchar |
|LA code                            |  nvarchar |
|URN                                |  float    |
|School Name                        |  nvarchar |
|School Type Description            |  nvarchar |
|School Type                        |  nvarchar |
|School Phase                       |  nvarchar |
|Religious Character                |  nvarchar |
|Government Office Region Name      |  nvarchar |
|Parliamentary Constituency         |  nvarchar |
|LA District                        |  nvarchar |
|Ward                               |  nvarchar |
|Statutory Low Age                  |  float    |
|Statutory High Age                 |  float    |
|Total School Workforce (Headcount) |  float    |
|Total Number of Classroom Teachers (Headcount)|  float    |
|Total Number of Teachers in the Leadership Group (Headcount)|  float    |
|Total Number of Teachers (Headcount)|  float    |
|Full-time Qualified Teachers (Headcount)|  float    |
|Total Number of Teaching Assistants (Headcount)|  float    |
|Total Number of Other School Support Staff (Headcount)|  float    |
|Total Number of Technicians (Headcount)|  float    |
|Total Number of Leadership Non-Teachers (Headcount)|  float    |
|Total Number of School Business Professionals (Headcount)|  float    |
|Total Number of Administrative Staff (Headcount)|  float    |
|Total Number of Auxiliary Staff (Headcount)|  float    |
|Total Number of Third Party Support Staff (Headcount)|  float    |
|Percentage of teachers who work part-time (%)|  float    |
|Total School Workforce (FTE)|  float    |
|Total Number of Classroom Teachers (FTE)|  float    |
|Total Number of Teachers in the Leadership Group (FTE)|  float    |
|Total Number of Teachers (FTE)|  float    |
|Full-time Qualified Teachers (FTE)|  float    |
|Total Number of Teaching Assistants (FTE)|  float    |
|Total Number of Other School Support Staff (FTE)|  float    |
|Total Number of Technicians (FTE)|  float    |
|Total Number of Leadership Non-Teachers (FTE)|  float    |
|Total Number of School Business Professionals (FTE)|  float    |
|Total Number of Administrative Staff (FTE)|  float    |
|Total Number of Auxiliary Staff (FTE)|  float    |
|Ratio of Teaching Assistants to All Teachers (FTE)|  float    |
|Pupil: Teacher Ratio (FTE of qualified and unqualified teachers)|  float    |
|All Teachers who Are Male (%) (Headcount)|  float    |
|All Teachers from Minority Ethnic Groups (ie Non-white British) |  nvarchar |
|Teachers Aged 50 or over (%) (Headcount)|  float    |
|Teachers with Qualified Teacher Status (%) (Headcount)|  float    |
|Unqualified Teachers on a QTS Route as a Proportion of the Total Number of Unqualified Teachers (%) (Headcount) |  nvarchar |
|Teaching Assistants who Are Male (%) (Headcount)|  nvarchar |
|Other Support Staff who Are Male (%) (Headcount)|  nvarchar |
|Technicians who Are Male (%) (Headcount)|  nvarchar |
|Administrative Staff who Are Male (%) (Headcount)|  nvarchar |
|Auxiliary Staff who Are Male (%) (Headcount)|  nvarchar |
|Leadership Non-Teachers who Are Male (%) (Headcount)|  nvarchar |
|School Business Professionals who Are Male (%) (Headcount)|  nvarchar |
|All Teaching Assistants from Minority Ethnic Groups (ie Non-white British) (%) (Headcount) |  nvarchar |
|All Non-classroom Based School Support Staff from Minority Ethnic Groups (ie Non-white British) (%) (Headcount) |  nvarchar |
|Auxiliary Staff from Minority Ethnic Groups (ie Non-white British) (%) (Headcount)|  nvarchar |
|Regional Pay Spine|  nvarchar |
|Mean Gross Salary of All Teachers (£)|  float    |
|All Teachers on the Leadership Pay Range (%)|  float    |
|Teachers with at Least One Period of Sickness Absence in 2023/24|  float    |
|Total Number of Days Lost to Sickness Absence in 2023/24|  float    |
|Average (Mean) Number of Days Lost to Teacher Sickness Absence (only Teachers in School Taking Sickness Absence) in 2023/24 |  float    |
|Average Number of Days Lost to Teacher Sickness Absence (All Teachers in School) in 2023/24 |  float    |
|Number of Vacant Teacher Posts|  float    |
|Rate of Teacher Vacancies - Demoninator is an addition of both teachers and vacancies (%) |  float    |
|Number of Temporarily Filled Teacher Posts|  float    |
|Rate of Temporarily Filled Teacher Posts - Demoninator is an addition of both teachers and vacancies (%) |  float    |

<!-- Leave the rest of this page blank -->
\newpage
