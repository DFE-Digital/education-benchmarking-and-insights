# CFR File Generation

1. Meticulously  edit all five (5) sql scripts copied over from last academic reporting year's folder to newly created academic reporting year's folder as mentioned under Prerequisite subsection in documentation [Overview](documentation\financial-data-drop\cfr\1_Overview .md) page. Edits must reflect

    - All table reference must be in accordance to the current reporting academic year, for instance `[CFR25].[dbo].[Context_GIAS_ClosedSchools_220725]` be edited to `[CFR26].[dbo].[Dim_GIAS_ClosedSchools_220726]`
    - All slowly changing dimension tables must be in accordance to the previous reporting academic year, for instance `[dbo].[Context_WorkforceNov2024]` be edited to `[dbo].[Dim_WorkforceNov2025]`

2. Run all five (5) correctly edited SQL scripts to generate SFB files
3. Export all record in `[CFR25].[dbo].[SFB_Maintained_2024-25]`, save as `maintained_schools_master_list.csv` in newly created academic reporting year's folder within Azure blob storage as mentioned under Prerequisite subsection in documentation [Overview](documentation\financial-data-drop\cfr\1_Overview .md) page.
