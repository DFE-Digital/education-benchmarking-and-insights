# Monthly FBIT Analytics Reporting

## Overview
There is capability to report user interactions based on visited school pages within the FBIT service by harnessing transactional data from Azure monitor logs (via a log analytics workspace) and realtional store in Azure SQL databse. An automated ETL process implemented in Excel's Get & Transform (Power Query) facilitates the production of a monthly summary report for intended skeholders.

## Refresh Query
1. Locate the [FBIT Report TEMPLATE](https://educationgovuk.sharepoint.com/:x:/r/sites/DfEFinancialBenchmarking/Shared%20Documents/FBIT%20Product/Analytics/FBIT%20Report%20-%20TEMPLATE.xlsx?d=w52e2c483bb034e69a5cb70a0996f9ac3&csf=1&web=1&e=5A6PSg) file found in this [DfE Sharepoint Location](https://educationgovuk.sharepoint.com/:f:/r/sites/DfEFinancialBenchmarking/Shared%20Documents/FBIT%20Product/Analytics).
2. Open the report with the `Open in app` option and not the browser option.
3. In the Data Ribbon of excel, locate `Get Data` button, click on the drop down to select `Data Source Settings`.
4. As seen below, select edit permission and provide credentials for access to data source. Use organization account for log analytics and database for Azure SQL connection.
![image](https://github.com/user-attachments/assets/01735fd7-35d9-4d0a-ba67-f5744e82c884)
5. Upon successful credential log-in, still on the Data ribbon, locate and select the `Queris & Connections` button.
6. Confirm the seven queries in the Queris & Connection view by the right hand side of excel work book.

   _The image below signifies successful access to the data sources_

   ![image](https://github.com/user-attachments/assets/ae4ac219-0c98-43b4-a6cf-d9063b706677)
7. On the Data ribbon, locate and select the `Refesh All` button to refresh all seven queries.
8. Confirm refresh by hovering on each query to see `Last refreshed` timestamp, ignore `Load staus` and `Data Sources` values as it has no correlation with the data refresh status.

## Prepare Report
#### Total Schools
The workbook's `SchoolData` sheet is updated and loaded (overwrite) after a successful refresh. The `school` table in the `data` SQL database is updated after a new academic/financial year, it is typical to have the same records of school data loaded through out an academic/finacial year.

#### CFP Completion
The workbook's `CfpData` sheet is updated and loaded (overwrite) after a successful refresh.

#### School engagement
9. In the `SpendingPrioritiesRequestData` sheet of the workbook, navigate to the end of the existing `SpendingPrioritiesRequestData` table and select an empty cell preferably in column F.
10. Load the `SchoolEngaement` query to the existing worksheet.
   - _To achieve this step, right click on the `SchoolEngaement` query under the `Queries & Connections` view,_
   - _Select `Load to`,_
   - _In the Import Data pop-up view, select the options as shown in the below image and click on the ```OK``` button to initiate load_
   ![image](https://github.com/user-attachments/assets/04647b06-36be-4f9e-a9db-e8ad5bbfc855)

11. Copy the loaded data exculding headers and paste value at the end of existing table to append data.
12. Delete the loaded data after append.
13. In the `PivotTables` sheet of the workbook, refresh all four pivot tables.

#### Summary
14. 
15. Save the Excel workbook.
16. Make a copy of the saved file and save to the same [DfE Sharepoint Location](https://educationgovuk.sharepoint.com/:f:/r/sites/DfEFinancialBenchmarking/Shared%20Documents/FBIT%20Product/Analytics).
   - _Rename file to match `FBIT Report - yyyy-mm``, for instance, if reporting for the month of June in the year 2025, file name should be `FBIT Report - 2025-05`_
## School engagement

On `p01` Log Analytics workspace, run the following query to cover whole of the last month. The maximum number of rows being shown needs to be updated in the query results in order for them to not be truncated.

```kql
let time_start = startofmonth(datetime(now), -1); 
let time_end = endofmonth(datetime(now), -1);
GetEstablishmentRequests
| where 
    Establishment == "school" 
| where 
    ResultCode == 200
| where 
    Feature == "spending-priorities"
| where
    TimeGenerated between (time_start .. time_end)
| order by 
    TimeGenerated asc
| project 
    Urn = Identifier,
    ExcelDate = format_datetime(TimeGenerated, 'dd/MM/yyyy HH:mm:ss')
```

Export the results to a .csv file and append the `Urn` and `ExcelDate` rows into the `SpendingPrioritiesRequestData` table.
Then expand to add the `Sector` and `Month` calculated columns from the existing rows.
If the date is not parsed correctly in the `Month` column then the format in the original query may need to be adjusted until Excel is happy.
Ensure the `PivotTables` sheet has updated and then copy the values into the relevant section in `Summary`.
Also update the totals from the second PivotTable in the same `Summary` section.

> **NOTE:** Although the query above could be modified to capture the whole of the past year, this is not advised due to potentially exceeding the row limit if executing from within Log Analytics in Azure Portal.

### Total Schools (optional)

To update the `SchoolData` tab, use the results of the following on `p01` database:

```sql
SELECT
    [URN],
    [OverallPhase],
    [FinanceType]
FROM [dbo].[School]
ORDER BY [URN]
```

This only needs to be done after a new academic/financial year data import. Ensure column types are properly set in the `SchoolData` table to resolve `VLOOKUP`s in the spreadsheet in the `SpendingPrioritiesRequestData` table.

## School features

On `p01` Log Analytics workspace, run the following query to cover whole of the year up to the end of the last month:

```kql
let time_start = startofmonth(datetime(now), -12); 
let time_end = endofmonth(datetime(now), -1);
GetEstablishmentRequests
| where 
    Establishment == "school" 
| where 
    ResultCode == 200
| where
    TimeGenerated between (time_start .. time_end)
| project 
    TimeGenerated, 
    Feature, 
    Identifier
| summarize 
    Visits=count(), UniqueVisits=count_distinct(Identifier)
    by Feature
| sort by 
    Visits desc
```

## Trust features

On `p01` Log Analytics workspace, run the following query to cover whole of the year up to the end of the last month:

```kql
let time_start = startofmonth(datetime(now), -12); 
let time_end = endofmonth(datetime(now), -1);
GetEstablishmentRequests
| where 
    Establishment == "trust" 
| where 
    ResultCode == 200
| where
    TimeGenerated between (time_start .. time_end)
| project 
    TimeGenerated, 
    Feature, 
    Identifier
| summarize 
    Visits=count(), UniqueVisits=count_distinct(Identifier)
    by Feature
| sort by 
    Visits desc
```

## Local Authority features

On `p01` Log Analytics workspace, run the following query to cover whole of the year up to the end of the last month:

```kql
let time_start = startofmonth(datetime(now), -12); 
let time_end = endofmonth(datetime(now), -1);
GetEstablishmentRequests
| where 
    Establishment == "local-authority" 
| where 
    ResultCode == 200
| where
    TimeGenerated between (time_start .. time_end)
| project 
    TimeGenerated, 
    Feature, 
    Identifier
| summarize 
    Visits=count(), UniqueVisits=count_distinct(Identifier)
    by Feature
| sort by 
    Visits desc
```

## CFP Completion

On `p01` database, obtain the CFP records for the whole year up to the end of the last month:

```sql
SELECT
    [URN],
    [Year],
    [IsComplete]
FROM [dbo].[FinancialPlan]
WHERE   [UpdatedAt] > CAST(DATEADD(MONTH, -12, DATEADD(day, 1 - DAY(GETDATE()), GETDATE())) AS DATE)
    AND [UpdatedAt] < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0)
ORDER BY [URN], [Year]
```

Ensure the `PivotTables` sheet has updated and then copy the values into the relevant section in `Summary`.
Also update the totals from the fourth PivotTable in the same `Summary` section.

## SFB Decommissioning

On `p01` Log Analytics workspace, run the following query to cover whole of the year up to the end of the last month:

```kql
let time_start = startofmonth(datetime(now), -12); 
let time_end = endofmonth(datetime(now), -1);
GetSfbReferrerRequests
| where
    TimeGenerated between (time_start .. time_end)
| project 
    TimeGenerated, 
    IsDeepLink
| summarize 
    Visits=count()
    by IsDeepLink
```

<!-- Leave the rest of this page blank -->
\newpage
