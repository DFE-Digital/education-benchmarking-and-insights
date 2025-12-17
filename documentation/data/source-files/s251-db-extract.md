# Source Files: s251_alleducation_la_regional_national.csv

Source: Collect Server

Period: Academic year

Format: SQL Server database table

**Publication timetable:**

| Month    | Data published |
|----------|----------------|
| December | Settled        |  

For example; for financial year ending Mar 2024, database data is closed off December 2024.

**Obtaining data:**

1. Access Collect Server
2. Edit SQL below to run against view for academic year in question.
3. Save results to csv file (utf-8) called s251_alleducation_la_regional_national.csv
4. The new_la_code is not available in the db view but is required in the file, to add this field the LA_Lookup sheet from the statistical neighbours publication was used.
5. Add a sheet to the s251_alleducation_la_regional_national.csv, called LALookup.  Copy the 'old la code' and 'new la code' lists form the statistical neighbours publication into the new sheet.
6. Add a column that will replace the 'new_la_code' in the s251_alleducation_la_regional_national.csv data.  
7. Run the following lookup in the new column
    - =VLOOKUP(*cellref*,LALookup!A1:B153,2,FALSE).  *where the old_la_code is in the second column of LALookup*
8. Paste the values into the 'new_la_code' column.
9. Delete the 'LAlookup' sheet on the data sheet.
10. Save the file.
11. Transfer the file to the input location for the dev environment.

**SQL:**
*Valid for 2025 but should be checked for consistency in subsequent periods*

```python3
SELECT '202425' as 'time_period'
      ,'Financial year' as 'time_identifier'
      ,'Local authority' as 'geographic_level'
      ,'England' as 'country'
      ,'E92000001' as 'country_code'
      ,'Dont have' as 'region_name'
      ,'Dont have' as 'region_code'
      ,[LAName] as 'la_name'
      ,[lea] as 'old_la_code'
      ,'Dont have' as 'new_la_code'
      ,'Section A: Schools' as 'main_category'
      ,[Description] as 'category_of_expenditure'
      ,round([EarlyYears],0) as 'early_years_establishments'
      ,round([Primary],0) as 'primary_schools'
      ,round([Secondary],0) as 'secondary_schools'
      ,round([SenSpecial],0) as 'sen_and_special_schools'
      ,round([APPru],0) as 'pupil_referral_units_and_alt_provision'
      ,round([PostSchool],0) as 'post_16'
      ,round([Gross],0) as 'gross_expenditure'
      ,round([Income],0) as 'income'
      ,round([Net],0) as 'net_expenditure'
  FROM [S251xxxxx_xxxxxxx].[dbo].[Views_TableA_AllData]
  where TableName = 'SchoolsExpenditure'
```

**File schema:**

| Column   |  Type  | Key |
|----------|:------:|:---:|
|time_period| int | N |
|time_identifier| nvarchar | N |
|geographic_level| nvarchar | N |
|country| nvarchar | N |
|country_code| nvarchar | N |
|region_name| nvarchar | N |
|region_code| nvarchar | N |
|la_name| nvarchar | N |
|old_la_code| smallint | N |
|new_la_code| nvarchar | N |
|main_category| nvarchar | N |
|category_of_expenditure| nvarchar | N |
|early_years_establishments| decimal | N |
|primary_schools| decimal | N |
|secondary_schools| decimal | N |
|sen_and_special_schools| decimal | N |
|pupil_referral_units_and_alt_provision| decimal | N |
|post_16| decimal | N |
|gross_expenditure| decimal | N |
|income| decimal | N |
|net_expenditure| decimal | N |

**Validation:**

//TODO

<!-- Leave the rest of this page blank -->
\newpage
