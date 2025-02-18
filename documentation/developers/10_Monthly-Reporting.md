# Monthly FBIT Analytics Reporting

Report location:  [DfE Sharepoint](https://educationgovuk.sharepoint.com/:f:/r/sites/DfEFinancialBenchmarking/Shared%20Documents/FBIT%20Product/Analytics)

## School engagement

On `p01` Log Analytics workspace, run the following query after setting the time period to the whole of the last month and setting the maximum number of rows:

```kql
GetEstablishmentRequests
| where 
    Establishment == "school" 
| where 
    ResultCode == 200
| where 
    Feature == "spending-priorities"
| project 
    Urn = Identifier,
    ExcelDate = format_datetime(TimeGenerated, 'dd/MM/yyyy HH:mm:ss'),
    TimeGenerated
| order by 
    TimeGenerated
```

Export the results to a .csv file and append the `Urn` and `ExcelDate` columns into the `SpendingPrioritiesRequestData` table.
Then expand to add the `Sector` and `Month` calculated columns from the existing rows.
If the date is not parsed correctly in the `Month` column then the format in the original query may need to be adjusted until Excel is happy.
Ensure the `PivotTables` sheet has updated and then copy the values into the relevant section in `Summary`.
Also update the totals from the second PivotTable in the same `Summary` section.

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

To update the `School features` table, use the results of the following on `p01` Log Analytics after setting the time period to last 12 months:

```kql
GetEstablishmentRequests
| where 
    Establishment == "school" 
| where 
    ResultCode == 200
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

To update the `Trust features` table, use the results of the following on `p01` Log Analytics after setting the time period to last 12 months:

```kql
GetEstablishmentRequests
| where 
    Establishment == "trust" 
| where 
    ResultCode == 200
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

To update the `Local Authority features` table, use the results of the following on `p01` Log Analytics after setting the time period to last 12 months:

```kql
GetEstablishmentRequests
| where 
    Establishment == "local-authority" 
| where 
    ResultCode == 200
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

On `p01` database, obtain the CFP records up to the end of the last month:

```sql
SELECT
    [URN],
    [Year],
    [IsComplete]
FROM [dbo].[FinancialPlan]
WHERE [Created] < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0)
ORDER BY [URN], [Year]
```

Ensure the `PivotTables` sheet has updated and then copy the values into the relevant section in `Summary`.
Also update the totals from the fourth PivotTable in the same `Summary` section.

<!-- Leave the rest of this page blank -->
\newpage
