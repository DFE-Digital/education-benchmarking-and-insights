# Outline

This directory contains python scripts created to substitute part of the MetricRAG_Calculation pipeline used in the VMFI data drop process. 

# Code structure

The `main.py` file is used in running the scripts. Small tasks, such as those associated with the stored procedures and the benchmarking calculations, are defined in the helper directory as helper scripts. The controller directory contains scripts which execute the helper scripts. The naming convention of the helpers associated with the 6 stored procedures being rewritten have names matching the stored procedures in the VMFI pipeline.

# Running the scripts
1. Pull the code locally
2. in the bau folder, create a new folder called `data`. Upload the following data tables from the VMFI database in csv format:
    - [processing].[academies_data]
    - [processing].[academy_groups_mapping]
    - [processing].[edubase]
    - [processing].[finance] 
    - [processing].[master_list]
    - [processing].[maintained_schools_list]
    - [processing].[SEN]

for [processing].[finance] you will only want to use items with a DataReleaseId matching your desired data release, as the file will be too large to download as a single csv otherwise. Be sure to double check the names of the files against the names of the accepted files in the `data_load_controller.py` file. Note: [processing].[finance] was saved with the name "finance_data_release_id_10".

3. In each of the 6 `sp_my...` helper functions, change the table output destination to your desired directory.

4. In a terminal, navigate to `education-benchmarking-and-insights/bau/src` and enter the command `python main.py`. You may need to update dependencies to get this to run.




    