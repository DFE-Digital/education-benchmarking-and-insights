# Overview

Consistent Financial Reporting (CFR) is one of the four financial data used in the Financial Benchmarking and Insights Tool (FBIT) service. The CFR data is typically received yearly by mid/late August when Local Authorities require maintained schools to complete their financial returns. This financial data is processed along with a few dimensional data (non-financial data) to create an upstream dataset named `maintained_schools_master_list`.

> See [FBIT Domain](/documentation/data/1_Domain.md) and [FBIT Sources](/documentation/data/2_Sources.md) for more information on FBIT Financial and ancillary data.

This documentation outlines the input data, quality assurance, data sourcing, cleansing and transformation procedures required to generate the `maintained_schools_master_list.csv` dataset for deployment in the FBIT data pipelines.

> **Note**
> At the point of this documentation, the data source for CFR is SQL server.
> Data cleansing would be executed after data is ingested from the SQL server into a working folder. This means that the raw data is retained in SQL server.

## Prerequisite

1. Access to `T1PRISTOAOL01` (DfE iStore, the server where all COLLECT data is stored)
2. Create a local Database to be named `CFRYY` where YY represents the financial year end, for instance `CFR25` for financial year ending 2025.

`CFRYY` Local database should include all tables as shown image below
