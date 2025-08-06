# Special Educational Needs - Dimension Data

The CFR data is processed along with dimension data to create Financial Benchmarking and Insights Tool (FBIT) upstream dataset named `maintained_schools_master_list`. These dimension data are:

- Get Information About School (GIAS)
- School Census
- Pupil Referral Unit (PRU) & Alternative Provision (AP)
- **Special Education Needs (SEN)**
- General Hospital schools
- School Workforce Census

SEN data is taken from the School Census produced by the Department of Education and published in the [GOV.UK School, Special educational needs in England](https://explore-education-statistics.service.gov.uk/find-statistics/special-educational-needs-in-england) education statistic website; this data is updated annually by June.

## Getting SEN Data

1. Click on [URL link](https://content.explore-education-statistics.service.gov.uk/api/releases/1818d5d3-56fb-47fb-b057-08dd86900487/files?fromPage=ReleaseUsefulInfo) to download `special-educational-needs-in-england_YYYY-YY` zipped folder to local folder, where YYYY-YY represents the financial year for instance, data for 2024_2025 financial year would be special-educational-needs-in-england_2024-25
2. In the `supporting-files` sub-folder of the `special-educational-needs-in-england_YYYY-YY` zipped folder, access the
`sen_school_level_underlying_data.csv` file.
3. Clean data by replacing any value returned as `z` to `0` (zero)
4. Save and rename file to `Dim_SENSchoolLevel2025.csv`
