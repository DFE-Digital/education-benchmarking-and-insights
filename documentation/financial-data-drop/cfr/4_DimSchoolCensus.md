# School Census - Dimension Data

The CFR data is processed along with a few dimension data to create the downstream dataset named `maintained_schools_master_list`. These dimension data are:

- Get Information About School (GIAS)
- **School Census**
- Pupil Referral Unit (PRU) & Alternative Provision (AP)
- Special Education Needs (SEN)
- General Hospital schools
- School Workforce Census

Pupil data from the school census is produced by the Department of Education and published in the [Pupils and their Characteristics - GOV.UK School Education statistic website](https://explore-education-statistics.service.gov.uk/find-statistics/school-pupils-and-their-characteristics.), this data is updated annually by July.

## Getting School Census Data

- School Census Dimension Data

1. Click on [URL link](https://content.explore-education-statistics.service.gov.uk/api/releases/e16d24b0-e813-4510-aa21-a6200dffaa97/files?fromPage=ReleaseUsefulInfo) to download `school-pupils-and-their-characteristics_YYYY-YY` zipped folder to local folder, where YYYY-YY represents the reporting academic year `school-pupils-and-their-characteristics_2024-25` would be used to generate 2024-2025 academic year financial return
2. In the `supporting-files` sub-folder of the `school-pupils-and-their-characteristics_YYYY-YY` zipped folder, access the
`spc_school_level_underlying_data_20yy.csv` file.
3. Clean data by replacing any value returned as `z` to `0` (zero)
4. Save and rename file to `Dim_SchoolLevelPupilCensus.csv`

- School Census Slowly Changing Dimension

1. tbc

### School Census Fields Data Type

| Column Name                       | Data type |
|-----------------------------------|-----------|
|time_period                        |float      |
|time_identifier                    |nvarchar   |
|geographic_level                   |nvarchar   |
|country_code                       |nvarchar   |
|country_name                       |nvarchar   |
|region_name                        |nvarchar   |
|region_code                        |nvarchar   |
|urn                                |float      |
|old_la_code                        |float      |
|la_name                            |nvarchar   |
|new_la_code                        |nvarchar   |
|estab                              |float      |
|school_name                        |nvarchar   |
|laestab                            |float      |
|phase_type_grouping                |nvarchar   |
|trust                              |nvarchar   |
|opendate                           |datetime   |
|sex_of_school_description          |nvarchar   |
|typeofestablishment_name           |nvarchar   |
|middle_school                      |nvarchar   |
|all_through_school                 |nvarchar   |
|hospital_school                    |nvarchar   |
|denomination                       |nvarchar   |
|admissions_policy                  |nvarchar   |
|district_administrative_code       |nvarchar   |
|district_administrative_name       |nvarchar   |
|parl_con_code                      |nvarchar   |
|parl_con_name                      |nvarchar   |
|ward_code                          |nvarchar   |
|ward_name                          |nvarchar   |
|urban_rural                        |nvarchar   |
|school_postcode                    |nvarchar   |
|academy_flag                       |nvarchar   |
|headcount total female             |float      |
|headcount total male               |float      |
|full-time pupils                   |float      |
|part-time pupils                   |float      |
|headcount of pupils                |float      |
|fte pupils                         |float      |
|male boarders                      |float      |
|female boarders                    |float      |
|total boarders                     |float      |
|number of FSM eligible pupils taking a free school meal on census day   |nvarchar        |
|% of FSM eligible pupils taking free school meals         |nvarchar        |
|number of pupils known to be eligible for free school meals         |nvarchar        |
|% of pupils known to be eligible for free school meals         |nvarchar        |
|Number of pupils (used for FSM calculation in Performance Tables)         |nvarchar        |
|number of pupils known to be eligible for free school meals (Performance Tables)         |nvarchar        |
|% of pupils known to be eligible for free school meals (Performance Tables)         |nvarchar        |
|Infants taken a free school meal on census day         |nvarchar        |
|number of pupils classified as white British ethnic origin         |nvarchar        |
|% of pupils classified as white British ethnic origin         |nvarchar        |
|number of pupils classified as Irish ethnic origin         |nvarchar        |
|% of pupils classified as Irish ethnic origin         |nvarchar        |
|number of pupils classified as traveller of Irish heritage ethnic origin         |nvarchar        |
|% of pupils classified as traveller of Irish heritage ethnic origin         |nvarchar        |
|number of pupils classified as any other white background ethnic         |nvarchar        |
|% of pupils classified as any other white background ethnic origin         |nvarchar        |
|number of pupils classified as Gypsy/Roma ethnic origin         |nvarchar        |
|% of pupils classified as Gypsy/Roma ethnic origin         |nvarchar        |
|number of pupils classified as white and black Caribbean ethnic          |nvarchar        |
|% of pupils classified as white and black Caribbean ethnic origin         |nvarchar        |
|number of pupils classified as white and black African ethnic origin         |nvarchar        |
|% of pupils classified as white and black African ethnic origin         |nvarchar        |
|number of pupils classified as white and Asian ethnic origin         |nvarchar        |
|% of pupils classified as white and Asian ethnic origin         |nvarchar        |
|number of pupils classified as any other mixed background ethnic         |nvarchar        |
|% of pupils classified as any other mixed background ethnic origin         |nvarchar        |
|number of pupils classified as Indian ethnic origin         |nvarchar        |
|% of pupils classified as Indian ethnic origin         |nvarchar        |
|number of pupils classified as Pakistani ethnic origin         |nvarchar        |
|% of pupils classified as Pakistani ethnic origin         |nvarchar        |
|number of pupils classified as Bangladeshi ethnic origin         |nvarchar        |
|% of pupils classified as Bangladeshi ethnic origin         |nvarchar        |
|number of pupils classified as any other Asian background ethnic         |nvarchar        |
|% of pupils classified as any other Asian background ethnic origin         |nvarchar        |
|number of pupils classified as Caribbean ethnic origin         |nvarchar        |
|% of pupils classified as Caribbean ethnic origin         |nvarchar        |
|number of pupils classified as African ethnic origin         |nvarchar        |
|% of pupils classified as African ethnic origin         |nvarchar        |
|number of pupils classified as any other black background ethnic         |nvarchar        |
|% of pupils classified as any other black background ethnic orig         |nvarchar        |
|number of pupils classified as Chinese ethnic origin         |nvarchar        |
|% of pupils classified as Chinese ethnic origin         |nvarchar        |
|number of pupils classified as any other ethnic group ethnic origin         |nvarchar        |
|% of pupils classified as any other ethnic group ethnic origin         |nvarchar        |
|number of pupils unclassified         |nvarchar        |
|% of pupils unclassified         |nvarchar        |
|number of pupils of compulsory school age and above (rounded)         |nvarchar        |
|number of pupils whose first language is known or believed to be English        |nvarchar        |
|% of pupils whose first language is known or believed to be English         |nvarchar        |
|number of pupils whose first language is known or believed to be other than English        |nvarchar        |
|% of pupils whose first language is known or believed to be other than English         |nvarchar        |
|number of pupils whose first language is unclassified         |nvarchar        |
|% of pupils whose first language is unclassified         |nvarchar        |
|number of pupils who are a young carer         |nvarchar        |
|number_of_dual_subsidiary_registrations         |nvarchar        |
|school_time         |nvarchar        |
