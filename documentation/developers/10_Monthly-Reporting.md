# Monthly FBIT Analytics Reporting

Report location:  [DfE Sharepoint](https://educationgovuk.sharepoint.com/:f:/r/sites/DfEFinancialBenchmarking/Shared%20Documents/FBIT%20Product/Analytics)

## School engagement

On `p01` Log Analytics workspace, run the following query to cover whole of the last month. The maximum number of rows being shown need to be updated in the query results.

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

Export the results to a .csv file and append the `Urn` and `ExcelDate` columns into the `SpendingPrioritiesRequestData` table.
Then expand to add the `Sector` and `Month` calculated columns from the existing rows.
If the date is not parsed correctly in the `Month` column then the format in the original query may need to be adjusted until Excel is happy.
Ensure the `PivotTables` sheet has updated and then copy the values into the relevant section in `Summary`.
Also update the totals from the second PivotTable in the same `Summary` section.

> **NOTE:** Although the query above could be modified to capture the whole of the past year, this is not advised due to potentially exceeding the row limit if executing from within Log Analytics in Azure Portal.

### Total Schools

To update the `SchoolData` tab, use the results of the following on `p01` database:

```sql
SELECT
    [URN],
    [OverallPhase],
    [FinanceType]
FROM [dbo].[School]
ORDER BY [URN]
```

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
    Count=count(), DistinctCount=dcount(Identifier)
    by Feature
| sort by 
    Count desc
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
    Count=count(), DistinctCount=dcount(Identifier)
    by Feature
| sort by 
    Count desc
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
    Count=count(), DistinctCount=dcount(Identifier)
    by Feature
| sort by 
    Count desc
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

<!-- Leave the rest of this page blank -->
\newpage
