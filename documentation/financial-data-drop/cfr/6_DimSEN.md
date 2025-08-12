# Special Educational Needs - Dimension Data

The CFR data is processed along with a few dimension data to create the downstream dataset named `maintained_schools_master_list`. These dimension data are:

- Get Information About School (GIAS)
- School Census
- Pupil Referral Unit (PRU) & Alternative Provision (AP)
- **Special Education Needs (SEN)**
- General Hospital schools
- School Workforce Census

SEN data is taken from the School Census produced by the Department of Education and published in the [Special educational needs in England - GOV.UK School Education statistic website](https://explore-education-statistics.service.gov.uk/find-statistics/special-educational-needs-in-england), this data is updated annually by June.

## Getting SEN Data

1. Click on [URL link](https://content.explore-education-statistics.service.gov.uk/api/releases/1818d5d3-56fb-47fb-b057-08dd86900487/files?fromPage=ReleaseUsefulInfo) to download `special-educational-needs-in-england_YYYY-YY` zipped folder to local folder, where YYYY-YY represents the reporting academic year, for instance,`special-educational-needs-in-england_2024-25` would be used to generate 2024-2025 academic year financial return
2. In the `supporting-files` sub-folder of the `special-educational-needs-in-england_YYYY-YY` zipped folder, access the
`sen_school_level_underlying_data.csv` file.
3. Clean data by replacing any value returned as `z` to `0` (zero)
4. Save and rename file to `Dim_SENSchoolLevel20yy.csv`

- Special Educational Needs Slowly Changing Dimension

1. tbc

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
