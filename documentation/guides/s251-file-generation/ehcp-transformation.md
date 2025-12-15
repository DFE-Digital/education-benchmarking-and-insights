# EEducation, health and care plans (EHCP) file transformation

## Overview

The `sen2_estab_caseload.csv` file is an ancillary non-financial dataset used to enrich the Section 251 (S251) financial data within the Financial Benchmarking and Insights Tool (FBIT). _See [FBIT Domain](/documentation/data/1_Domain.md) for more information on FBIT Financial and non financial data._

This document outlines the transformation process used to align the current EHCP file to the structure of the previous year's dataset. _See [FBIT Sources](/documentation/data/2_Sources.md) for information non financial data, description and sources._

### Get started

1. Access the [Sen2-caseload transformation.xlsx](https://educationgovuk.sharepoint.com/:x:/r/sites/DfEFinancialBenchmarking/Shared%20Documents/General/Analytics%20Discovery/s251/Sen2-caseload%20transformation.xlsx?d=wdccd3fb3704944f3a46bf4cc37b5615f&csf=1&web=1&e=13uUfn) file in [DfE Financial Benchmark s251 Sharepoint location](https://educationgovuk.sharepoint.com/:f:/r/sites/DfEFinancialBenchmarking/Shared%20Documents/General/Analytics%20Discovery/s251?csf=1&web=1&e=4M38O3). Open the report with the `Open in app` option and not the browser option.
2. Access the current EHCP file which is named `caseload.csv` from [Explore Education Statistics - Education, health and care plans](https://explore-education-statistics.service.gov.uk/find-statistics/education-health-and-care-plans) as described in [FBIT Sources](/documentation/data/2_Sources.md).
3. In the `caseload.csv` file, filter file on `time_period` field (Col A) to the reporting academic year.
4. Copy all relevant data excluding headers and paste to the `Raw EHCP` sheet in the `Sen2-caseload transformation.xlsx` file. Make sure to overwrite and not append data.

### Transform file

1. In the `Data` Ribbon of excel, locate and click on `Queries & Connections` button.
2. Right click on the **EHCP transformation** query in the `Queries & Connections` view by the right hand side of excel work book.
3. Select `Refresh` to refresh the query.
4. Write the data in the `EHCP transformation` sheet including headers to `sen2_estab_caseload.csv`
5. Share data with data engineer.

### Power Query transformation

The [power query transformation steps](https://educationgovuk.sharepoint.com/:t:/r/sites/DfEFinancialBenchmarking/Shared%20Documents/General/Analytics%20Discovery/s251/power%20query.txt?csf=1&web=1&e=5uIcYF) edited in Power query M language from the Advanced Editor view or through the user interface from the applied list view.

1. In the `Data` Ribbon of excel, locate and click on `Queries & Connections` button.
2. Right click on the **EHCP transformation** query in the `Queries & Connections` view by the right hand side of excel work book.
3. Select `Edit` to view transformation steps or/and to edit query.

_**TIPS!!**_

_To integrate, update or change query in Excel see [External Reference](https://learn.microsoft.com/en-us/power-query/power-query-ui)_

_To know more on Power Query see [External Reference](https://learn.microsoft.com/en-us/power-query/power-query-what-is-power-query)_
