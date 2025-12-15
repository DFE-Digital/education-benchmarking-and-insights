# EEducation, health and care plans (EHCP) file transformation

## Overview

The `sen2_estab_caseload.csv` file is an ancillary non-financial dataset used to enrich the Section 251 (S251) financial data within the Financial Benchmarking and Insights Tool (FBIT).
>_See [FBIT Domain](/documentation/data/1_Domain.md) for more information on FBIT Financial and non financial data._

This document outlines the transformation process used to align the 2025 EHCP file to the structure of the previous year's dataset.
>_See [FBIT Sources](/documentation/data/2_Sources.md) for description and sources of financial and non financial data._

### Get started

1. Access the [Sen2-caseload transformation.xlsx](https://educationgovuk.sharepoint.com/:x:/r/sites/DfEFinancialBenchmarking/Shared%20Documents/General/Analytics%20Discovery/s251/Sen2-caseload%20transformation.xlsx?d=wdccd3fb3704944f3a46bf4cc37b5615f&csf=1&web=1&e=13uUfn) file in [DfE Financial Benchmark s251 Sharepoint location](https://educationgovuk.sharepoint.com/:f:/r/sites/DfEFinancialBenchmarking/Shared%20Documents/General/Analytics%20Discovery/s251?csf=1&web=1&e=4M38O3), open the report with desktop / app option and not browser.

2. Access the current EHCP file which is named `caseload.csv` from [Explore Education Statistics - Education, health and care plans](https://explore-education-statistics.service.gov.uk/find-statistics/education-health-and-care-plans) as described in [FBIT Sources](/documentation/data/2_Sources.md).

3. In the `caseload.csv` file, filter file on `time_period` field (Col A) to the reporting academic year, for instance, filter on '202425' when reporting for 2024-2025 academic year.

4. Copy all relevant data excluding headers and overwrite the `Raw EHCP` sheet in the `Sen2-caseload transformation.xlsx` file. Make sure to overwrite and not append data.

### Transform file

1. In the `Data` Ribbon of excel, locate and click on `Queries & Connections` button.

2. Right click on the **EHCP transformation** query in the `Queries & Connections` view by the right hand side of excel work book.

3. Select `Refresh` to refresh the query.

4. Upon successful refresh, write the data in the `EHCP transformation` sheet, including the headers, to `sen2_estab_caseload.csv`

5. Save and share data with data engineer.

### Power Query transformation

The [power query transformation steps](https://educationgovuk.sharepoint.com/:t:/r/sites/DfEFinancialBenchmarking/Shared%20Documents/General/Analytics%20Discovery/s251/power%20query.txt?csf=1&web=1&e=5uIcYF) can be modified either by using the Applied Steps pane in the user interface or by editing the M code directly in the Advanced Editor.

1. In the `Data` Ribbon of excel, locate and click on `Queries & Connections` button.
2. Right click on the **EHCP transformation** query in the `Queries & Connections` view by the right hand side of excel work book.
3. Select `Edit` to view transformation steps or/and to edit query.

```pq
let
    Source = Excel.CurrentWorkbook(){[Name="Table1"]}[Content],
    #"Changed Type" = Table.TransformColumnTypes(Source,{{"time_period", Int64.Type}, {"time_identifier", type text}, {"geographic_level", type text}, {"country_code", type text}, {"country_name", type text}, {"region_code", type text}, {"region_name", type text}, {"new_la_code", type text}, {"old_la_code", Int64.Type}, {"la_name", type text}, {"breakdown_topic", type text}, {"breakdown", type any}, {"ehcplans", Int64.Type}, {"mainstream_la_maintained", Int64.Type}, {"mainstream_la_maintained_resourced_provision", Int64.Type}, {"mainstream_la_maintained_senunit", Int64.Type}, {"mainstream_academy", Int64.Type}, {"mainstream_academy_resourced_provision", Int64.Type}, {"mainstream_academy_senunit", Int64.Type}, {"mainstream_free_school", Int64.Type}, {"mainstream_free_school_resourced_provision", Int64.Type}, {"mainstream_free_school_senunit", Int64.Type}, {"mainstream_independent", Int64.Type}, {"mainstream_total", Int64.Type}, {"mainstream_total_pc", type number}, {"special_la_maintained", Int64.Type}, {"special_academy_free", Int64.Type}, {"special_independent", Int64.Type}, {"special_non_maintained", Int64.Type}, {"special_total", Int64.Type}, {"special_total_pc", type number}, {"ap_pru_academy", Int64.Type}, {"ap_pru_free_school", Int64.Type}, {"ap_pru_la_maintained", Int64.Type}, {"ap_pru_total", Int64.Type}, {"AP_PRU_total_pc", type number}, {"general_fe_tertiary_colleges", Int64.Type}, {"specialist_post_16_institutions", Int64.Type}, {"ukrlp_provider", Int64.Type}, {"fe_total", Int64.Type}, {"fe_total_pc", type number}, {"elective_home_education", Int64.Type}, {"other_arrangements_la", Int64.Type}, {"other_arrangements_parents", Int64.Type}, {"online_provider", type text}, {"w_settings", type text}, {"other_schools", type text}, {"other_placement_settings", Int64.Type}, {"neet", Int64.Type}, {"neet_ntci", Int64.Type}, {"neet_other", Int64.Type}, {"neet_other_csa", Int64.Type}, {"ed_elsewhere", Int64.Type}, {"ed_elsewhere_pc", type number}, {"nm_early_years", Int64.Type}, {"nm_early_years_pc", type number}, {"placement_unknown", Int64.Type}, {"placement_unknown_pc", type number}, {"await_prov_2022", type text}, {"perm_ex_2022", type text}}),
    #"Removed Columns" = Table.RemoveColumns(#"Changed Type",{"breakdown_topic", "mainstream_total_pc", "special_total_pc", "fe_total_pc", "ed_elsewhere_pc", "nm_early_years_pc", "placement_unknown_pc", "AP_PRU_total_pc"}),
    #"Filtered Rows" = Table.SelectRows(#"Removed Columns", each ([breakdown] = "All EHC plans")),
    #"Added Custom" = Table.AddColumn(#"Filtered Rows", "Sixth form college", each "x"),
    #"Added Custom1" = Table.AddColumn(#"Added Custom", "Other FE", each "x"),
    #"Added Custom2" = Table.AddColumn(#"Added Custom1", "Hospital School", each "x"),
    #"Unpivoted Other Columns" = Table.UnpivotOtherColumns(#"Added Custom2", {"time_period", "time_identifier", "geographic_level", "country_code", "country_name", "region_code", "region_name", "new_la_code", "old_la_code", "la_name", "breakdown"}, "Attribute", "Value"),
    #"Replaced Errors" = Table.ReplaceErrorValues(#"Unpivoted Other Columns", {{"Value", "x"}}),
    #"Replaced Value" = Table.ReplaceValue(#"Replaced Errors","ehcplans","Total",Replacer.ReplaceValue,{"Attribute"}),
    #"Replaced Value1" = Table.ReplaceValue(#"Replaced Value","mainstream_la_maintained","Mainstream - LA maintained",Replacer.ReplaceValue,{"Attribute"}),
    #"Replaced Value2" = Table.ReplaceValue(#"Replaced Value1","mainstream_academy","Mainstream - Academy",Replacer.ReplaceValue,{"Attribute"}),
    #"Replaced Value3" = Table.ReplaceValue(#"Replaced Value2","mainstream_la_maintained_resourced_provision","Mainstream - LA maintained - Resourced provision",Replacer.ReplaceText,{"Attribute"}),
    #"Replaced Value4" = Table.ReplaceValue(#"Replaced Value3","ap_pru_academy","AP/PRU - Academy",Replacer.ReplaceText,{"Attribute"}),
    #"Replaced Value15" = Table.ReplaceValue(#"Replaced Value4","ap_pru_free_school","AP/PRU - Free school",Replacer.ReplaceText,{"Attribute"}),
    #"Replaced Value16" = Table.ReplaceValue(#"Replaced Value15","ap_pru_la_maintained","AP/PRU - LA maintained",Replacer.ReplaceText,{"Attribute"}),
    #"Replaced Value5" = Table.ReplaceValue(#"Replaced Value16","general_fe_tertiary_colleges","General FE and tertiary colleges/HE",Replacer.ReplaceText,{"Attribute"}),
    #"Replaced Value6" = Table.ReplaceValue(#"Replaced Value5","mainstream_la_maintained_senunit","Mainstream - LA maintained - SEN unit",Replacer.ReplaceText,{"Attribute"}),
    #"Replaced Value7" = Table.ReplaceValue(#"Replaced Value6","mainstream_academy_senunit","Mainstream - Academy - SEN unit",Replacer.ReplaceText,{"Attribute"}),
    #"Replaced Value8" = Table.ReplaceValue(#"Replaced Value7","mainstream_academy_resourced_provision","Mainstream - Academy - Resourced Provision",Replacer.ReplaceText,{"Attribute"}),
    #"Replaced Value9" = Table.ReplaceValue(#"Replaced Value8","mainstream_free_school_senunit","Mainstream - Free school - SEN unit",Replacer.ReplaceText,{"Attribute"}),
    #"Replaced Value10" = Table.ReplaceValue(#"Replaced Value9","mainstream_free_school_resourced_provision","Mainstream - Free school - Resourced provision",Replacer.ReplaceText,{"Attribute"}),
    #"Replaced Value11" = Table.ReplaceValue(#"Replaced Value10","mainstream_free_school","Mainstream - Free school",Replacer.ReplaceText,{"Attribute"}),
    #"Replaced Value12" = Table.ReplaceValue(#"Replaced Value11","mainstream_independent","Mainstream - Independent",Replacer.ReplaceText,{"Attribute"}),
    #"Replaced Value13" = Table.ReplaceValue(#"Replaced Value12","special_independent","Special - Independent",Replacer.ReplaceText,{"Attribute"}),
    #"Replaced Value14" = Table.ReplaceValue(#"Replaced Value13","special_non_maintained","Special - Non-maintained",Replacer.ReplaceText,{"Attribute"}),
    #"Replaced Value20" = Table.ReplaceValue(#"Replaced Value14","special_la_maintained","Special - LA maintained",Replacer.ReplaceText,{"Attribute"}),
    #"Replaced Value21" = Table.ReplaceValue(#"Replaced Value20","special_academy_free","Special - Academy/free",Replacer.ReplaceText,{"Attribute"}),
    #"Replaced Value17" = Table.ReplaceValue(#"Replaced Value21","specialist_post_16_institutions","Specialist post-16 institutions",Replacer.ReplaceText,{"Attribute"}),
    #"Replaced Value18" = Table.ReplaceValue(#"Replaced Value17",202425,2025,Replacer.ReplaceValue,{"time_period"}),
    #"Replaced Value19" = Table.ReplaceValue(#"Replaced Value18","All EHC plans","Total",Replacer.ReplaceText,{"breakdown"}),
    #"Added Conditional Column" = Table.AddColumn(#"Replaced Value19", "establishment_group", each if [Attribute] = "Specialist post-16 institutions" then "Further education" else if Text.Contains([Attribute], "FE") then "Further education" else if Text.Contains([Attribute], "Mainstream") then "Mainstream school" else if Text.Contains([Attribute], "Special") then "Special school" else if Text.Contains([Attribute], "AP/PRU") then "Alternative provision/Pupil referral unit" else if [Attribute] = "Total" then "Total" else "z"),
    #"Reordered Columns" = Table.ReorderColumns(#"Added Conditional Column",{"time_period", "time_identifier", "geographic_level", "country_code", "country_name", "region_code", "region_name", "new_la_code", "old_la_code", "la_name", "establishment_group", "Attribute", "breakdown", "Value"}),
    #"Renamed Columns" = Table.RenameColumns(#"Reordered Columns",{{"Attribute", "establishment_type"}, {"Value", "num_caseload"}, {"breakdown", "ehcp_or_statement"}})
in
    #"Renamed Columns"
```

_**TIPS!!**_

_To integrate, update or change query in Excel see [External Reference](https://learn.microsoft.com/en-us/power-query/power-query-ui)_

_To know more on Power Query see [External Reference](https://learn.microsoft.com/en-us/power-query/power-query-what-is-power-query)_
