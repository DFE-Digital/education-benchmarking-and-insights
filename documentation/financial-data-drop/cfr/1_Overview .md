# Overview

Consistent Financial Reporting (CFR) is one of the four financial data used in the Financial Benchmarking and Insights Tool (FBIT) service. The CFR data is typically received yearly by mid/late August when Local Authorities require maintained schools to complete their financial returns. This financial data is processed along with a few dimensional data (non-financial data) to generate the School Financial Benchmark (SFB) dataset named `maintained_schools_master_list`. _See [FBIT Domain](/documentation/data/1_Domain.md) for more information on FBIT Financial data._

This documentation outlines the input data, quality assurance, data sourcing, cleansing and transformation procedures required to generate `maintained_schools_master_list.csv` dataset for deployment in the FBIT data pipelines.

> **Note**
>
> At the point of this documentation, the data source for CFR is SQL server.
> Data cleansing would be executed after data is ingested from the SQL server into a working folder. This means that the raw data is retained in SQL server.

## Prerequisite

1. Read and write access to Azure blob storage - [s198d01data](https://s198d01data.blob.core.windows.net/raw/default/) within DfE Platform Identity directory.
2. Create a folder with the naming convention 202y in [s198d01data](https://s198d01data.blob.core.windows.net/raw/default/) that represent the current reporting academic year. For instance, folder would be named `2025` for `2024-2025` reporting academic year.
3. Access to [DfE iStore, the server where all COLLECT data is stored)](https://educationgovuk.sharepoint.com/:w:/r/sites/DfEFinancialBenchmarking/_layouts/15/Doc.aspx?sourcedoc=%7BA47507F6-2C23-487A-98EC-0B6C75A7471A%7D&file=CFR%20source%20data%20access%20request.docx&action=default&mobileredirect=true)
4. Read and write access to [DfE Financial Benchmark Sharepoint](https://educationgovuk.sharepoint.com/sites/DfEFinancialBenchmarking/Shared%20Documents/Forms/AllItems.aspx?viewid=7afed90f%2D9f2f%2D431a%2D93ce%2D48075c0e93d8&csf=1&web=1&e=boXhxD&CID=0fb7a62d%2De68f%2D4f86%2Dac15%2D27e9c4f7b4a6&FolderCTID=0x012000B007B75DE8F91C4B82D20FE8B354FCBD)
5. Create a folder with the naming convention 2Y-2Y in [CFR Data Procurement Process Sharepoint location](https://educationgovuk.sharepoint.com/sites/DfEFinancialBenchmarking/Shared%20Documents/Forms/AllItems.aspx?id=%2Fsites%2FDfEFinancialBenchmarking%2FShared%20Documents%2FCFR%20Data%20Procurement%20Process&viewid=7afed90f%2D9f2f%2D431a%2D93ce%2D48075c0e93d8&csf=1&web=1&e=boXhxD&CID=0fb7a62d%2De68f%2D4f86%2Dac15%2D27e9c4f7b4a6&FolderCTID=0x012000B007B75DE8F91C4B82D20FE8B354FCBD) that represent the reporting academic year. For instance, folder would be named `24-25` for `2024-2025` reporting academic year.
6. Copy the five (5) `.sql` files over from last academic reporting year's folder to newly created academic reporting year's folder. The file are;

    - My_Step1.sql
    - My_Step2.sql
    - My_Step3.sql
    - My_Step4.sql
    - My_Step5.sql

7. Create a local Database to be named `CFRYY` where YY represents the reporting academic year end, for instance `CFR25` for data related to the financial year end 2025.
