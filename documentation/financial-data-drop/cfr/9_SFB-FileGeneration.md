# CFR File Generation

1. Meticulously  edit all five (5) sql scripts copied over from last academic reporting year's folder to the newly created academic reporting year's folder as mentioned under Prerequisite subsection in this documentation [Overview](documentation\financial-data-drop\cfr\1_Overview .md) page. Edits must reflect

    - All table reference must be in accordance to the current reporting academic year, for instance `[CFR25].[dbo].[Context_GIAS_ClosedSchools_220725]` be edited to `[CFR26].[dbo].[Dim_GIAS_ClosedSchools_220726]`
    - All slowly changing dimension tables must be in accordance to the previous reporting academic year, for instance `[dbo].[Context_WorkforceNov2024]` be edited to `[dbo].[Sld_WorkforceNov2025]`

2. Run all five (5) correctly edited SQL scripts to generate SFB files

3. Export all record in `[CFR25].[dbo].[SFB_Maintained_20yy-yy]`, save as `maintained_schools_master_list.csv` in the newly created academic reporting year's folder within Azure blob storage as mentioned under Prerequisite subsection in this documentation [Overview](documentation\financial-data-drop\cfr\1_Overview .md) page.

## SFB_Dim_202y2y Data Type

| Column Name                       | Data type |
|-----------------------------------|-----------|
|LAEstab|float|
|LA|float|
|Estab|float|
|URN|float|
|School_Name|nvarchar|
|Period Covered|int|
|DNS|varchar|
|NewFedFlag|varchar|
|GIAS_Federation|nvarchar|
|GIAS_Federation Name|nvarchar|
|Gender |nvarchar|
|Overall Phase|varchar|
|SFB Phase|nvarchar|
|Type|nvarchar|
|TypeGroup|nvarchar|
|UrbanRural|varchar|
|Region|nvarchar|
|London Borough|varchar|
|London Weighting|varchar|
|Easting|float|
|Northing|float|
|Admissions_Policy|nvarchar|
|Has a 6th Form|nvarchar|
|Lowest age of pupils|float|
|Highest age of pupils|float|
|PFI Funding|varchar|
|Open_Closed|nvarchar|
|OpenDate|datetime|
|CloseDate|datetime|
|Hospital School|varchar|
|Ind. Pupils FTE|float|
|Aggregated Pupils FTE|float|
|PRU pupil nums|float|
|Ind. Pupils Headcount|float|
|% of pupils with EHCP_agg|numeric|
|% of pupils with EHCP_ind|numeric|
|% of pupils with SEN Support_agg|numeric|
|% of pupils with SEN Support_ind|numeric|
|% of pupils eligible for FSM_agg|float|
|% of pupils eligible for FSM_ind|float|
|% of pupils with EAL_agg|float|
|% of pupils with EAL_ind|float|
|% of pupils who are Boarders_agg|numeric|
|% of pupils who are Boarders_ind|numeric|
|No of pupils in 6th form_agg|int|
|No of pupils in 6th form_ind|int|
|Teachers FTE_agg|float|
|Teachers FTE_ind|float|
|% of teachers with QTS|float|
|FTE of Teaching Assistants_agg|float|
|FTE of Teaching Assistants_ind|float|

## SFB_Federations_Aggregated_202y2y Data Type

| Column Name                       | Data type |
|-----------------------------------|-----------|
|LA|float|
|LAEstab|float|
|Headcount|float|
|FTE|float|
|Boarders|float|
|PC_Boarders|numeric|
|NumberFSM|int|
|PC_FSM|numeric|
|Pupils_EAL|int|
|PC_EAL|numeric|
|SEN_pupils|float|
|SEN_support|float|
|EHCP|float|
|PC_SENSupport|numeric|
|PC_EHCP|numeric|
|VIthForm|int|
|TotalWorkforce_HC|float|
|Classroom_HC|float|
|Leadership_HC|float|
|Teachers_HC|float|
|TA_HC|float|
|Support_HC|int|
|Auxillary_HC|float|
|TotalWorkforce_FTE|float|
|Classroom_FTE|float|
|Leadership_FTE|float|
|Teachers_FTE|float|
|TA_FTE|float|
|Support_FTE|int|
|Auxillary_FTE|float|
|FederatedSchool1|nvarchar|
|FederatedSchool2|nvarchar|
|FederatedSchool3|nvarchar|
|FederatedSchool4|nvarchar|
|FederatedSchool5|nvarchar|
|FederatedSchool6|nvarchar|
|FederatedSchool7|nvarchar|
|FederatedSchool8|nvarchar|
|FederatedSchool9|nvarchar|
|FederatedSchool10|nvarchar|

## SFB_Federations_Aggregated_202425 Data Type

| Column Name                       | Data type |
|-----------------------------------|-----------|
|LA|float|
|LAEstab|float|
|Headcount|float|
|FTE|float|
|Boarders|float|
|PC_Boarders|numeric|
|NumberFSM|int|
|PC_FSM|numeric|
|Pupils_EAL|int|
|PC_EAL|numeric|
|SEN_pupils|float|
|SEN_support|float|
|EHCP|float|
|PC_SENSupport|numeric|
|PC_EHCP|numeric|
|VIthForm|int|
|TotalWorkforce_HC|float|
|Classroom_HC|float|
|Leadership_HC|float|
|Teachers_HC|float|
|TA_HC|float|
|Support_HC|int|
|Auxillary_HC|float|
|TotalWorkforce_FTE|float|
|Classroom_FTE|float|
|Leadership_FTE|float|
|Teachers_FTE|float|
|TA_FTE|float|
|Support_FTE|int|
|Auxillary_FTE|float|
|FederatedSchool1|nvarchar|
|FederatedSchool2|nvarchar|
|FederatedSchool3|nvarchar|
|FederatedSchool4|nvarchar|
|FederatedSchool5|nvarchar|
|FederatedSchool6|nvarchar|
|FederatedSchool7|nvarchar|
|FederatedSchool8|nvarchar|
|FederatedSchool9|nvarchar|
|FederatedSchool10|nvarchar|

## SFB_Maintained_202y-2y Data Type

| Column Name                       | Data type |
|-----------------------------------|-----------|
|LAEstab|float|
|LA|float|
|LA Name|nvarchar|
|Estab|float|
|URN|float|
|School Name|nvarchar|
|Period covered by return|int|
|Did Not Supply flag|varchar|
|FederatedSubmission|varchar|
|Lead school in federation|float|
|General Hospital School Indicator|varchar|
|IndividualPupilsFTE|float|
|AggregatedPupilsFTE|float|
|IndTeachers_FTE|float|
|AggregatedTeachersFTE|float|
|Gender|nvarchar|
|Overall Phase|varchar|
|Phase|nvarchar|
|Type|nvarchar|
|UrbanRural|varchar|
|Region|nvarchar|
|London Borough|varchar|
|London Weighting|varchar|
|Ind_PC_FSM|float|
|Aggregated_PC_FSM|float|
|Ind_PC_EHCP|numeric|
|Aggregated_PC_EHCP|numeric|
|Ind_PC_SEN_Support|numeric|
|Aggregated_PC_SEN_Support|numeric|
|Ind_PC_EAL|float|
|Aggregated_PC_EAL|float|
|Ind_PC_Boarders|numeric|
|Aggregated_PC_Boarders|numeric|
|Admissions_Policy|nvarchar|
|PFI|varchar|
|Has a 6th form|nvarchar|
|Ind_VIthForm|int|
|Aggregated_VIthForm|int|
|Lowest age of pupils|float|
|Highest age of pupils|float|
|Ind_TA_FTE|float|
|Aggregated_TA_FTE|float|
|Teachers_PC_QTS|float|
|I01 Pre-16 Funding|float|
|I02 Post-16 Funding|float|
|I01/2 Pre and Post-16 Funding|float|
|I03 SEN|float|
|I04 Funding for minority ethnic pupils|float|
|I05 Pupil Premium|float|
|I06 Other DfE/EFA Revenue Grants|float|
|I07 Other income (LA & other Government grants)|float|
|I08a Income from lettings|float|
|I08b Income from facilities and services|float|
|I08a/b Income from facilities and services|float|
|I09 Income from catering|float|
|I10 Receipts from supply teacher insurance claims|float|
|I11 Receipts from other insurance claims|float|
|I12 Income from contributions to visits etc|float|
|I13 Donations and/or voluntary funds|float|
|I15 Pupil focussed extended school funding and/or grants|float|
|I16 Community focussed school funding and/or grants|float|
|I17 Community focused school facilities income|float|
|I18c Income from the �1bn COVID-19 catch-up package announced on 20 July 2020|float|
|I18d Income from other additional grants|float|
|I18 Total additional grant for schools|float|
|Total Income: I01 to I08, I11 to I15, I18 minus E30|float|
|Total Income: I01 to I18 minus E30|float|
|E01 Teaching staff|float|
|E02 Supply teaching staff|float|
|E03 Education support staff|float|
|E04 Premises staff|float|
|E05 Administrative and clerical staff|float|
|E06 Catering staff|float|
|E07 Other staff|float|
|E08 Indirect employee expenses|float|
|E09 Staff development and training|float|
|E10 Supply teacher insurance|float|
|E11 Staff-related insurance|float|
|E12 Building maintenance and improvement|float|
|E13 Grounds maintenance and improvement|float|
|E14 Cleaning and caretaking|float|
|E15 Water and sewerage|float|
|E16 Energy|float|
|E17 Rates|float|
|E18 Other occupation costs|float|
|E19 Learning resources (not ICT equipment)|float|
|E20A Connectivity|float|
|E20B Onsite servers|float|
|E20C IT learning resources|float|
|E20D Administration software and systems|float|
|E20E Laptops, desktops and tablets|float|
|E20F Other hardware|float|
|E20G IT support|float|
|E21 Examination fees|float|
|E22 Administrative supplies - non educational|float|
|E23 Other insurance premiums|float|
|E24 Special facilities|float|
|E25 Catering supplies|float|
|E26 Agency supply teaching staff|float|
|E27 Educational Consultancy|float|
|E28a Bought in professional services, other, not PFI|float|
|E28b Bought in professional services, other, PFI|float|
|E28a/b Bought in professional services|float|
|E29 Interest charges for Loan and Bank|float|
|E30 Direct revenue financing (Revenue contributions to capital)|float|
|E31 Community focused school staff|float|
|E32 Community focused school costs|float|
|Total Expenditure:E01 to E29 and E31 to E32 minus I9, I10, I16 and I17|float|
|Total Expenditure:E01 to E29 and E31 to E32|float|
|OB01 Opening pupil-focused revenue balance|float|
|OB02 Opening community-focused revenue balance|float|
|OB03 Opening capital balance|float|
|CI01 Capital income|float|
|CI03 Voluntary or private income|float|
|CI04 Direct revenue financing|float|
|CE01 Acquisition of land and existing buildings|float|
|CE02 New construction, conversion and renovation|float|
|CE03 Vehicles, plant, equipment and machinery|float|
|CE04 Information and communication technology|float|
|B01 Committed revenue balances|float|
|B02 Uncommitted revenue balances|float|
|B03 Devolved formula capital balance|float|
|B05 Other capital balances|float|
|B06 Community-focused school revenue balances|float|
|B07 Outstanding balance on capital loans to school|float|
|Revenue Reserve: B01 + B02 + B06|float|
|In-year Balance|float|
|Grant Funding: (I01:I07) + I15 + I16 + I18a/b/c/d|float|
|Direct Grants: I01:I02 + I06:I07|float|
|Community Grants: I16+I18|float|
|Targetted Grants: I03:I05 + I15|float|
|Self Generated Funding: (I08a/b:I13) + I17|float|
|Total income|float|
|Teaching Staff: E01|float|
|Supply Staff: E02 + E10 + E26|float|
|Education support staff: E03|float|
|Other Staff Costs: (E07:E9) + E11|float|
|Staff Total: (E01:E03) + E05 + (E07: E11) + E26|float|
|Maintenance & Improvement: E12 + E13|float|
|Premises: (E12:E14) + E04 + E28b|float|
|Catering Expenses: E06 + E25|float|
|Occupation: E06 + (E15:E18) + E23 + E25|float|
|Supplies and Services: (E19:E22) + (E27:E28b)|float|
|Educational Supplies: (E19:E21)|float|
|Brought in Professional Sevices: (E27 + E28a)|float|
|Community Exp: E31 + E32|float|
|Total Expenditure excluding E30|float|
