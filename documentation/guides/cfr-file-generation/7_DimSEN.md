# Special Educational Needs - Dimension Data

The CFR data is processed along with a few dimension data to create the downstream dataset named `maintained_schools_master_list` and an accompanying transparency file. These dimension data are:

- Pupil Referral Unit (PRU) & Alternative Provision (AP)
- School Census
- General Hospital schools
- Get Information About School (GIAS)
- **Special Education Needs (SEN)**
- School Workforce Census

SEN data is taken from the School Census produced by the Department of Education and published in the [Special educational needs in England - GOV.UK School Education statistic website](https://explore-education-statistics.service.gov.uk/find-statistics/special-educational-needs-in-england), this data is updated annually by June.

## Getting SEN Data

### Special Educational Needs Dimension Data

1. Click on [URL link](https://content.explore-education-statistics.service.gov.uk/api/releases/1818d5d3-56fb-47fb-b057-08dd86900487/files?fromPage=ReleaseUsefulInfo) to download `special-educational-needs-in-england_20YY-yy` zipped folder to local folder, where 20YY-yy represents the reporting academic year. For instance,`special-educational-needs-in-england_2024-25` when generating 2024-2025 CFR downstream dataset.

2. In the `supporting-files` sub-folder of the `special-educational-needs-in-england_20YY-YY` zipped folder, access the
`sen_school_level_underlying_data.csv` file.

3. Clean data by replacing any value returned as `z` to `0.00` (zero)

4. Save and rename file to `Dim_SENSchoolLevel_20YY.csv` where 20YY is the end of current reporting academic year. For instance, Save As `Dim_SchoolLevelPupilCensus_2025.csv` for reporting academic year 2024-2025.

### Special Educational Needs Slowly Changing Dimension Data

1. In the page view of the [Special educational needs in England - GOV.UK School Education statistic website](https://explore-education-statistics.service.gov.uk/find-statistics/special-educational-needs-in-england), navigate to `Releases in this series` to expand on the `View releases` button where last reporting academic year (current academic year minus one) data can be accessed.

2. For SEN SCD data, access the previous data by clicking on `Academic year 20yy/yy` link where 20yy/yy represents the previous reporting academic year. For instance, `Academic year 2023/24` would be accessed to download `special-educational-needs-in-england_2023-24` zipped folder when generating 2024-2025 CFR downstream dataset.

3. Repeat step 2 and 3 as listed above.

4. Save and rename file to `Dim_SENSchoolLevel_20YY.csv` where 20YY is the downloaded academic year. For instance, Save As `Dim_SENSchoolLevel_2024.csv` when generating 2024-2025 CFR downstream dataset.

### Flat File Ingestion into CFRyy Local Database

Using a database GUI Tool, ingest both flat files as a tables into the created local database
    - Confirm that the two destination table names reflect their respective reporting academic years
    - Confirm schema name for both destination tables is `dbo`
    - In the Modify Columns tab, confirm column data type matches with GIAS Fields Data Type as detailed below

### Special Educational Needs Fields Data Type

| Column Name                       | Data type |
|-----------------------------------|-----------|
|time_period                        |  float    |
|time_identifier                    |  nvarchar |
|geographic_level                   |  nvarchar |
|country_code                       |  nvarchar |
|country_name                       |  nvarchar |
|region_name                        |  nvarchar |
|region_code                        |  nvarchar |
|old_la_code                        |  float    |
|new_la_code                        |  nvarchar |
|la_name                            |  nvarchar |
|URN                                |  float    |
|estab                              |  float    |
|school_name                        |  nvarchar |
|laestab                            |  float    |
|open_date                          |  datetime |
|sex_of_school_description          |  nvarchar |
|phase_type_grouping                |  nvarchar |
|type_of_establishment              |  nvarchar |
|middle_school                      |  nvarchar |
|all_through                        |  nvarchar |
|hospital_school                    |  nvarchar |
|denomination                       |  nvarchar |
|admissions_policy                  |  nvarchar |
|district_administrative_code       |  nvarchar |
|district_administrative_name       |  nvarchar |
|parlc_code                         |  nvarchar |
|parlc_name                         |  nvarchar |
|ward_code                          |  nvarchar |
|ward_name                          |  nvarchar |
|urban_rural                        |  nvarchar |
|school_postcode                    |  nvarchar |
|LSOA11                             |  nvarchar |
|Total pupils                       |  float    |
|SEN support                        |  float    |
|EHC plan                           |  float    |
|EHC_Primary_need_spld              |  float    |
|EHC_Primary_need_mld               |  float    |
|EHC_Primary_need_sld               |  float    |
|EHC_Primary_need_pmld              |  float    |
|EHC_Primary_need_semh              |  float    |
|EHC_Primary_need_slcn              |  float    |
|EHC_Primary_need_hi                |  float    |
|EHC_Primary_need_vi                |  float    |
|EHC_Primary_need_msi               |  float    |
|EHC_Primary_need_pd                |  float    |
|EHC_Primary_need_asd               |  float    |
|EHC_Primary_need_oth               |  float    |
|EHC_Primary_need_nsa               |  float    |
|SUP_Primary_need_spld              |  float    |
|SUP_Primary_need_mld               |  float    |
|SUP_Primary_need_sld               |  float    |
|SUP_Primary_need_pmld              |  float    |
|SUP_Primary_need_semh              |  float    |
|SUP_Primary_need_slcn              |  float    |
|SUP_Primary_need_hi                |  float    |
|SUP_Primary_need_vi                |  float    |
|SUP_Primary_need_msi               |  float    |
|SUP_Primary_need_pd                |  float    |
|SUP_Primary_need_asd               |  float    |
|SUP_Primary_need_oth               |  float    |
|SUP_Primary_need_nsa               |  float    |
|Prov_SPLD                          |  float    |
|Prov_MLD                           |  float    |
|Prov_SLD                           |  float    |
|Prov_PMLD                          |  float    |
|Prov_SEMH                          |  float    |
|prov_slcn                          |  float    |
|prov_hi                            |  float    |
|prov_vi                            |  float    |
|prov_msi                           |  float    |
|prov_pd                            |  float    |
|prov_asd                           |  float    |
|prov_oth                           |  float    |
|SEN_Unit                           |  float    |
|RP_Unit                            |  float    |

<!-- Leave the rest of this page blank -->
\newpage
