# Data Sources

There are two main data types used in the Financial Benchmarking and Insights Tool (FBIT) service: Financial and Non-Financial data. This data is obtained from a range of sources within the Department for Education. This document outlines the list of raw data files used within the service, their update frequency, and information on how to retrieve them. 

## Financial Data

FBIT performs three data releases each submission year. These are the Consistent Finanical Reporting (CFR) for Local Authority Maintained Schools, Budget Forecast Returns (BFR)S for Academy Trusts, and Academy Account Returns (AAR) also for Academy Trusts. 

CFR data is typically received in September when Local Authorities require schools to complete their financial returns. These returns cover the period of April to March of the respective submission year.

BFR data is submitted around August, and provides a forecasted return for academies for the following 3 years.

AAR data is submitted in January when Academies complete their finanical returns. This return covers a period of September to August of the respective submssion year.

The table below provides an overview of the 5 financial data files used within the FBIT service.

| raw file name | raw file description | source    | update frequency| acccess instructions|
|---------------|----------------------|-----------|-----------------|---------------------|
aar.csv| The academies' financial returns  | Sourced from the AnM database under the RA_Dataset    | Yearly (January as part of the AAR release)| You will require access to the AnM database in order to extract this file. To get access to the database, please raise an [Analytical Data Access Request](https://dfe.service-now.com/serviceportal?id=sc_cat_item&sys_id=74bc3be81b212d504f999978b04bcb0b) via a service now ticket. The file you will need to extract the view "RA_Datasets.AccountsReturn.vw_AR<Version>_BenchmarkReport_<Year>". An example of such a file name might be AR8_BenchmarkReport_2023 |
| aar_cs.csv | The central service financial returns  | Sourced from the AnM database under the RA_Dataset  | Yearly (January as part of the AAR release)  | You will require access to the AnM database in order to extract this file. To get access to the database, please raise an [Analytical Data Access Request](https://dfe.service-now.com/serviceportal?id=sc_cat_item&sys_id=74bc3be81b212d504f999978b04bcb0b) via a service now ticket. The file you will need to extract the view "RA_Datasets.AccountsReturn.vw_AR<Version>_CS_BenchmarkReport_<Year>". An example of such a file name might be AR8_CS_BenchmarkReport_2023 
| BFR_3Y_raw.csv| 3 year financial forecast for trusts spanning the 2nd, 3rd and 4th financial year from the current.  | Sourced from the AnM database under the RA_Dataset   |  Yearly (August as part of the BFR release) frequency| You will require access to the AnM database in order to extract this file. To get access to the database, please raise an [Analytical Data Access Request](https://dfe.service-now.com/serviceportal?id=sc_cat_item&sys_id=74bc3be81b212d504f999978b04bcb0b) via a service now ticket. The file you will need to extract the view "RA_Datasets.BFR.vw_Three_Year_Forecast_<Year>". An example of such a file name might be vw_Three_Year_Forecast_2023 
| BFR_SOFA_raw.csv    | Statement of Finanical Accounts table containing the forecasted financial position of trusts for the current and next year (divided into 2 per year)  | Sourced from the AnM database under the RA_Dataset    | Yearly (August as part of the BFR release) | You will require access to the AnM database in order to extract this file. To get access to the database, please raise an [Analytical Data Access Request](https://dfe.service-now.com/serviceportal?id=sc_cat_item&sys_id=74bc3be81b212d504f999978b04bcb0b) via a service now ticket. The file you will need to extract the view "RA_Datasets.BFR.vw_Sofa_<Year>". An example of such a file name might be vw_Sofa_2023 | For AnM support contact: Andrew.HAWKETT@education.gov.uk, Timothy.Vinay@education.gov.uk, Elizabeth.Greenwood@education.gov.uk 
| maintained_schools_master_list.csv| Contains financial returns of maintained schools    | Sourced from downstream in the DfE (Provided by Julie Glenndenning) | Yearly (September as part of the CFR release)    | File is received from Julie Glenndenning via email at the time of the LA maintained schools' financial returns | 





## Non-Financial Data

Non-financial data contains school metadata, encompasing items such as pupil and workforce census, and school building characteristics.

The majority of this data is extracted from GIAS and EES.

The table below provides an overview of the various non-financial data files used within the FBIT service.

| raw file name | raw file description |source | update frequency | acccess instructions |
|-------------- |----------------------|-------|------------------|----------------------|
| academies_master_list.csv | Contains academies metadata | Sourced from the AnM database under the RA_Dataset | Yearly (January as part of the AAR release) | You will require access to the AnM database in order to extract this file. To get access to the database, please raise an [Analytical Data Access Request](https://dfe.service-now.com/serviceportal?id=sc_cat_item&sys_id=74bc3be81b212d504f999978b04bcb0b) via a service now ticket. The file you will need to extract the view "RA_Datasets.AccountsReturn.vw_AR<Version>_BenchmarkReport_<Year>". An example of such a file name might be AR8_BenchmarkReport_2023
| cdc.csv  | Condition Data Collection containing building age and square meterage | Provided by the PO | Update frequency occurs intermittently when a new survey is released | The file was provided by the PO for the previous data release|
| census_pupils.csv | Census data for school pupils | [Explore Education Statistics](https://explore-education-statistics.service.gov.uk/find-statistics/school-pupils-and-their-characteristics)  | Updated twice yearly as part of the latest AAR and CFR data drop  | Open access from website. File is located under "Additional supporting files" > "School level underlying data <Year>" where <Year> could be "2023/2024" for example. 
| census_workforce.xlsx | Census data for school workforce, note this lags a year behind the current release year. i.e for the 2024 data release, the 2023 census data is the latest available report | [Explore Education Statistics](https://explore-education-statistics.service.gov.uk/find-statistics/school-workforce-in-england) | Updated as part of the latest AAR or CFR data drop | Open access from website. File is located under "Additional supporting files" > "School level summary file" 
| cfo.xlsx| Chief Financial Officer contact information| Provided by the PO    | Updated intermittently at the request of the PO | The file was provided by the PO for the previous data release 
| gias.csv| Contains school metadata| [GIAS](https://get-information-schools.service.gov.uk/Downloads)   | Updated once per year as part of the CFR drop, taking the most recent September release | Open access from website. File is located under "All Establishment Data" > "Establishment fields CSV" 
| gias_all_links.csv| Contains group links data used to determine federation membership | [GIAS](https://get-information-schools.service.gov.uk/Downloads)   | Updated once per year as part of the CFR drop, taking the most recent September release | Open access from website. File is located under "All Group Data" > "All group records" 
| gias_links.csv    | Containing data to link establishments | [GIAS](https://get-information-schools.service.gov.uk/Downloads)   | Updated once per year as part of the CFR drop, taking the most recent September release | Open access from website. File is located under "All Establishment Data" > "Establishment links CSV" 
| ks2.xlsx| KS2 progress metrics    | [Compare School Performance](https://www.compare-school-performance.service.gov.uk/download-data)  | Updated twice yearly as part of the latest AAR and CFR data drop    | Select the submission year required, click continue, select all of england, click continue, select Key stage 2 results (final), and key stage 4 results (final), click continue. Choose "Data in XLS format" 
| ks4.xlsx | KS4 progress metrics   | [Compare School Performance](https://www.compare-school-performance.service.gov.uk/download-data) | Updated twice yearly as part of the latest AAR and CFR data drop | Select the submission year required, click continue, select all of england, click continue, select Key stage 2 results (final), and key stage 4 results (final), click continue. Choose "Data in XLS format"
| sen.csv | Special educational needs dataset containing information about the number of pupils requiring various SEN provisions  | [Explore Education Statistics](https://explore-education-statistics.service.gov.uk/find-statistics/special-educational-needs-in-england) | Updated twice yearly as part of the latest AAR and CFR data drop    | Open access from website. File is located under "Additional supporting files" > "School level underlying data <Year>" where <Year> could be "2024" for example.  


## Data Source - Contact Information

For help with any of the data sources used in this service, please reach out to the respective contact point, who can be found in the following [sharepoint file](https://educationgovuk.sharepoint.com/:x:/r/sites/DfEFinancialBenchmarking/Shared%20Documents/General/Technical%20Team/Data%20Pipelines/FBIT%20data%20source%20contacts.xlsx?d=w01b573949f8a487dba9cf8e450b8cbf5&csf=1&web=1&e=kt1SMJ).
