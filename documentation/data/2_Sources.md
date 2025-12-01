# Sources

There are two main data types used in the Financial Benchmarking and Insights Tool (FBIT) service: Financial and Non-Financial data. Both are obtained from a range of sources within the Department for Education. This document outlines the list of raw data files used within the service, their update frequency, and information on how to retrieve them.

**Financial Data:**

FBIT performs four data releases each submission year. These are the Consistent Financial Reporting (CFR) for Local Authority Maintained Schools, Section 251 (S251) for Local Authorities, Budget Forecast Returns (BFR)S for Academy Trusts, and Academy Account Returns (AAR) also for Academy Trusts.

CFR data is typically received between August and September when Local Authorities require schools to complete their financial returns. These returns cover the period of April to March of the respective submission year.

Section 251 data comprises of two parts; budget statements (planned spending) which is published in September and outturn statements (actual spending) which is published in December of the following year. This data details Local Authority funding and expenditure for education and children's social care services.

BFR data submission occurs between August and September, this data provides a forecasted return for academies for the following 3 years.

AAR data is submitted in January when Academies complete their financial returns. This return covers a period of September to August of the respective submission year.

The table below provides an overview of the 7 financial data files used within the FBIT service.

| raw file name | raw file description | source | update frequency | access instructions |
|---------------|----------------------|--------|------------------|---------------------|
| aar.csv       | The academies' financial returns | Sourced from the AnM database under the RA_Dataset | Yearly (January as part of the AAR release) | Access to the AnM database is required to extract this file. To gain the necessary permissions, submit an [Analytical Data Access Request](https://dfe.service-now.com/serviceportal?id=sc_cat_item&sys_id=74bc3be81b212d504f999978b04bcb0b) via the ServiceNow portal. The target view for extraction is `RA_Datasets.AccountsReturn.vw_AR<Version>_BenchmarkReport_<Year>`. For reference, an example of a resulting file name might be `vw_AR8_BenchmarkReport_2023`. |

\* This data is restricted to users and requires a login to access.

<!-- Leave the rest of this page blank -->
\newpage
