# Data Sourcing and Validation for Consistent Financial Reporting (CFR) Data

This procedure details the necessary steps to process the Consistent Financial Reporting (CFR) data extract to produce the definitive `maintained_schools_master_list.csv` dataset. The core of this process involves rigorous data validation and cleansing to ensure the accuracy and integrity of the final output.

## CFR Quality Assurance Check

The main checks on CFR data relate to the fields for federated schools. A federation of schools is a structure where two or more maintained schools join under a single governing body, while retaining their individual identities. Section 24 of the Education Act 2002 allows for a local authority (LA) to allocate a single budget share to a governing body where there are two or more schools federated.

Federated schools receiving a single budget share should report collectively in their CFR return. There should therefore be only one CFR return made for federated schools. The “lead school” in the federation should make the return while including the LA Establishment (`LAEstab`) numbers of other schools within the federation. There should be no individual return from schools within a federation other than the lead school. In practice however, some schools submit returns both through a federation and on their own, resulting in duplicate submissions which need to be resolved as part of preprocessing.

![Federated School Example](../images/federated-school.png)

The image above shows an example of a federated school with the lead school alone actioning the financial return. There is validation in the data collection to identify federation reporting issues but it is possible that some errors remain. The mis-recording of federated schools can cause issues such as duplication and incorrect totals.

### CFR Data Source

Server Name = [iSore SQL Server](https://educationgovuk.sharepoint.com/:w:/r/sites/DfEFinancialBenchmarking/_layouts/15/Doc.aspx?sourcedoc=%7BA47507F6-2C23-487A-98EC-0B6C75A7471A%7D&file=CFR%20source%20data%20access%20request.docx&action=default&mobileredirect=true)

Database = `ConsistentFinancialReporting_20yy20yySPSSViews` where 20yy20yy represents the reporting financial year, for instance for 2024_2025 financial year, use `ConsistentFinancialReporting_20242025SPSSViews`

View = `CFR_yy-yy_Data` where yy-yy represent financial year, for instance `CFR_24-25_Data`

### The following instructions describe CFR QA checks

1. Connect to the CFR data source as listed above
2. Run the below SQL query to validate non-federated schools financial return. The absence of record / no result suggest that there is no incorrect non-federated school entry in CFR data collection. Edit database and view name to reflect current reporting year

    ```sql
    SELECT *
    FROM  [ConsistentFinancialReporting_20242025SPSSViews].[dbo].[CFR_24-25_Data]
    WHERE ([Federated Flag] IS NULL OR [Federated Flag] = 'No')
    AND   ([LAEstab of School in Federation 1] IS NOT NULL
        OR [LAEstab of School in Federation 2] IS NOT NULL
        OR [LAEstab of School in Federation 3] IS NOT NULL
        OR [LAEstab of School in Federation 4] IS NOT NULL
        OR [LAEstab of School in Federation 5] IS NOT NULL
        OR [LAEstab of School in Federation 6] IS NOT NULL
        OR [LAEstab of School in Federation 7] IS NOT NULL
        OR [LAEstab of School in Federation 8] IS NOT NULL
        OR [LAEstab of School in Federation 9] IS NOT NULL
        OR [LAEstab of School in Federation 10] IS NOT NULL)
    ```

3. Run the below SQL query to validate that only the “lead school” within a federation have CFR return. The absence of record / no result suggest that there is no incorrect federated school entry in CFR data collection. Edit database and view  name to reflect current reporting year

    ```sql
    SELECT b.*
    FROM (
        SELECT [LAEstab of School in Federation 1] AS LAEstab1_to_10 FROM [ConsistentFinancialReporting_20242025SPSSViews].[dbo].[CFR_24-25_Data]
        UNION
        SELECT [LAEstab of School in Federation 2] FROM [ConsistentFinancialReporting_20242025SPSSViews].[dbo].[CFR_24-25_Data]
        UNION
        SELECT [LAEstab of School in Federation 3] FROM [ConsistentFinancialReporting_20242025SPSSViews].[dbo].[CFR_24-25_Data]
        UNION
        SELECT [LAEstab of School in Federation 4] FROM [ConsistentFinancialReporting_20242025SPSSViews].[dbo].[CFR_24-25_Data]
        UNION
        SELECT [LAEstab of School in Federation 5] FROM [ConsistentFinancialReporting_20242025SPSSViews].[dbo].[CFR_24-25_Data]
        UNION
        SELECT [LAEstab of School in Federation 6] FROM [ConsistentFinancialReporting_20242025SPSSViews].[dbo].[CFR_24-25_Data]
        UNION
        SELECT [LAEstab of School in Federation 7] FROM [ConsistentFinancialReporting_20242025SPSSViews].[dbo].[CFR_24-25_Data]
        UNION
        SELECT [LAEstab of School in Federation 8] FROM [ConsistentFinancialReporting_20242025SPSSViews].[dbo].[CFR_24-25_Data]
        UNION
        SELECT [LAEstab of School in Federation 9] FROM [ConsistentFinancialReporting_20242025SPSSViews].[dbo].[CFR_24-25_Data]
        UNION
        SELECT [LAEstab of School in Federation 10] FROM [ConsistentFinancialReporting_20242025SPSSViews].[dbo].[CFR_24-25_Data]) a
    LEFT JOIN (
        SELECT * FROM [ConsistentFinancialReporting_20242025SPSSViews].[dbo].[CFR_24-25_Data])b
    ON a.LAEstab1_to_10 = b.LEAEstab
    WHERE b.LEAEstab IS NOT NULL
    ```

4. Run the below SQL query to validate that all schools where the Federated Flag is “yes” (lead school) have at least one school listed in “LAEstab of School in Federation 1” to 10 (non-lead schools). The absence of record / no result suggest that there is no incorrect federated school entry in CFR data collection. Edit database and view  name to reflect current reporting year

    ```sql
    SELECT TOP 10 *
    FROM  [ConsistentFinancialReporting_20242025SPSSViews].[dbo].[CFR_24-25_Data]
    WHERE [Federated Flag] = 'Yes'
    AND   [LAEstab of School in Federation 1] IS NULL
    AND   [LAEstab of School in Federation 2] IS NULL
    AND   [LAEstab of School in Federation 3] IS NULL
    AND   [LAEstab of School in Federation 4] IS NULL
    AND   [LAEstab of School in Federation 5] IS NULL
    AND   [LAEstab of School in Federation 6] IS NULL
    AND   [LAEstab of School in Federation 7] IS NULL
    AND   [LAEstab of School in Federation 8] IS NULL
    AND   [LAEstab of School in Federation 9] IS NULL
    AND   [LAEstab of School in Federation 10] IS NULL
    ```

5. Run the below SQL query to confirm NULLs in the data. `NULL` values can throw out the total calculations, all NULL return should be replaced with zero in the creation of the final dataset. Edit database and view  name to reflect current reporting year

    ```sql
    SELECT * FROM [ConsistentFinancialReporting_20242025SPSSViews].[dbo].[CFR_24-25_Data]
    WHERE [OB01] IS NULL OR [OB02] IS NULL OR [OB03] IS NULL OR [I01] IS NULL OR [I02] IS NULL
    OR [I03] IS NULL OR [I04] IS NULL OR [I05] IS NULL OR [I06] IS NULL OR [I07] IS NULL
    OR [I08a] IS NULL OR [I08b] IS NULL OR [I09] IS NULL OR [I10] IS NULL OR [I11] IS NULL
    OR [I12] IS NULL OR [I13] IS NULL OR [I15] IS NULL OR [I16] IS NULL OR [I17] IS NULL
    OR [I18c] IS NULL OR [I18d] IS NULL OR [E01] IS NULL OR [E02] IS NULL OR [E03] IS NULL
    OR [E04] IS NULL OR [E05] IS NULL OR [E06] IS NULL OR [E07] IS NULL OR [E08] IS NULL
    OR [E09] IS NULL OR [E10] IS NULL OR [E11] IS NULL OR [E12] IS NULL OR [E13] IS NULL
    OR [E14] IS NULL OR [E15] IS NULL OR [E16] IS NULL OR [E17] IS NULL OR [E18] IS NULL
    OR [E19] IS NULL OR [E20A] IS NULL OR [E20B] IS NULL OR [E20C] IS NULL OR [E20D] IS NULL
    OR [E20E] IS NULL OR [E20F] IS NULL OR [E20G] IS NULL OR [E21] IS NULL OR [E22] IS NULL
    OR [E23] IS NULL OR [E24] IS NULL OR [E25] IS NULL OR [E26] IS NULL OR [E27] IS NULL
    OR [E28a] IS NULL OR [E28b] IS NULL OR [E29] IS NULL OR [E30] IS NULL OR [E31] IS NULL
    OR [E32] IS NULL OR [CI01] IS NULL OR [CI03] IS NULL OR [CI04] IS NULL OR [DeMinimis] IS NULL
    OR [CE01] IS NULL OR [CE02] IS NULL OR [CE03] IS NULL OR [CE04A] IS NULL OR [CE04B] IS NULL
    OR [CE04C]IS NULL OR [CE04D] IS NULL OR [CE04E] IS NULL OR [B01] IS NULL OR [B02] IS NULL
    OR [B03] IS NULL OR [B05] IS NULL OR [B06] IS NULL OR [B07] IS NULL
    ```

#### Handling CFR data errors

Once any issue(s) has been identified from QA checks, an Excel report of the affected schools (`LEAEstab`, `Federated Flag` and `LAEstab of School in Federation 1 to 10` fields and respective values) should be documented and communicated / shared with related stakeholder (product owners).

Report be named `Federation Funnies` and uploaded to the newly created `yy-yy` folder within [CFR Data Procurement Process Sharepoint location](https://educationgovuk.sharepoint.com/sites/DfEFinancialBenchmarking/Shared%20Documents/Forms/AllItems.aspx?id=%2Fsites%2FDfEFinancialBenchmarking%2FShared%20Documents%2FCFR%20Data%20Procurement%20Process&viewid=7afed90f%2D9f2f%2D431a%2D93ce%2D48075c0e93d8&csf=1&web=1&e=boXhxD&CID=0fb7a62d%2De68f%2D4f86%2Dac15%2D27e9c4f7b4a6&FolderCTID=0x012000B007B75DE8F91C4B82D20FE8B354FCBD). See sample for 2024-2025 academic year [here](https://educationgovuk.sharepoint.com/:x:/r/sites/DfEFinancialBenchmarking/Shared%20Documents/CFR%20Data%20Procurement%20Process/24-25/Federation%20Funnies%20-%20250730.xlsx?d=w77a953618eef4ce5a6c6cdc37e6fc14e&csf=1&web=1&e=cXUQtM)

> **Note**
> Our established process has been to manually correct school data submission errors, as this is more efficient than reopening the portal for resubmission. Following these corrections, product owner would make arrangements to notify the affected schools' Local Authorities with instructions on how to report their financial records correctly in the future.

#### Export CFR Data from CFR Source Data into `CFRyy` Local Database

1. Connect to the CFR data source as listed above
2. Either by using a database GUI Tool or CSV export, ingest all `CFR_yy-yy_Data` records from CFR data source within iStore SQL Server into a table to be named `CFR_yyyy` within the newly created local database as mentioned under Prerequisite subsection in this documentation [Overview](documentation\financial-data-drop\cfr\1_Overview .md) page. Where yyyy represents the current reporting academic year, for instance `CFR_2425` for 2024-2025 reporting academic year.
3. Manually correct school data submission errors by making any amendments to the CFR data having consulted with product owner and sought further information from the schools and LAs. This step may not be necessary - it depends on your preliminary CFR data checks. Manual correction may include updating `CFR_yyyy` table within local database based on specific conditions, for instance,

    ```sql
    UPDATE [CFR2y].[dbo].[CFR_2y2y]
    SET [Federated Flag] = 'No' 
    WHERE [LEAEstab] IN (8761005, 8761007);
    ```

4. As per step 5 of the above CFR QA checks, if NULLs values is confirmed, run the below SQL query to replace all `NULL` values with zero (0.00). Edit database and table name to reflect current reporting year

    ```sql
    UPDATE [CFR25].[dbo].[CFR_2425]
    SET    [OB01] = 0.00, [OB02] = 0.00, [OB03] = 0.00, [I01] = 0.00, [I02] = 0.00, [I03] = 0.00, [I04] = 0.00,
        [I05] = 0.0,   [I06] = 0.00, [I07] = 0.00, [I08a] = 0.00, [I08b] = 0.00, [I09] = 0.00, [I10] = 0.00,
        [I11] = 0.00, [I12] = 0.00, [I13] = 0.00, [I15] = 0.00,[I16] = 0.00, [I17] = 0.00, [I18c] = 0.00,
        [I18d] = 0.00, [E01] = 0.00, [E02] = 0.00, [E03] = 0.00, [E04] = 0.00, [E05] = 0.00, [E06] = 0.00,
        [E07] = 0.00, [E08] = 0.00, [E09] = 0.00, [E10] = 0.00, [E11] = 0.00, [E12] = 0.00, [E13] = 0.00,
        [E14] = 0.00, [E15] = 0.00, [E16] = 0.00, [E17] = 0.00, [E18] = 0.00, [E19] = 0.00, [E20A] = 0.00,
        [E20B] = 0.00, [E20C] = 0.00, [E20D] = 0.00, [E20E] = 0.00, [E20F] = 0.00, [E20G] = 0.00, [E21] = 0.00,
        [E22] = 0.00, [E23] = 0.00, [E24] = 0.00, [E25] = 0.00, [E26] = 0.00, [E27] = 0.00, [E28a] = 0.00,
        [E28b] = 0.00, [E29] = 0.00, [E30] = 0.00, [E31] = 0.00, [E32] = 0.00, [CI01] = 0.00, [CI03] = 0.00, [CI04] = 0.00, [DeMinimis] = 0.00, [CE01] = 0.00, [CE02] = 0.00, [CE03] = 0.00, [CE04A] = 0.00, [CE04B] = 0.00,
        [CE04C]= 0.00, [CE04D] = 0.00, [CE04E] = 0.00, [B01] = 0.00, [B02] = 0.00, [B03] = 0.00,
        [B05] = 0.00, [B06] = 0.00, [B07] = 0.00

    WHERE  [OB01] IS NULL OR [OB02] IS NULL OR [OB03] IS NULL OR [I01] IS NULL OR [I02] IS NULL
        OR [I03] IS NULL OR [I04] IS NULL OR [I05] IS NULL OR [I06] IS NULL OR [I07] IS NULL
        OR [I08a] IS NULL OR [I08b] IS NULL OR [I09] IS NULL OR [I10] IS NULL OR [I11] IS NULL
        OR [I12] IS NULL OR [I13] IS NULL OR [I15] IS NULL OR [I16] IS NULL OR [I17] IS NULL
        OR [I18c] IS NULL OR [I18d] IS NULL OR [E01] IS NULL OR [E02] IS NULL OR [E03] IS NULL
        OR [E04] IS NULL OR [E05] IS NULL OR [E06] IS NULL OR [E07] IS NULL OR [E08] IS NULL
        OR [E09] IS NULL OR [E10] IS NULL OR [E11] IS NULL OR [E12] IS NULL OR [E13] IS NULL
        OR [E14] IS NULL OR [E15] IS NULL OR [E16] IS NULL OR [E17] IS NULL OR [E18] IS NULL
        OR [E19] IS NULL OR [E20A] IS NULL OR [E20B] IS NULL OR [E20C] IS NULL OR [E20D] IS NULL
        OR [E20E] IS NULL OR [E20F] IS NULL OR [E20G] IS NULL OR [E21] IS NULL OR [E22] IS NULL
        OR [E23] IS NULL OR [E24] IS NULL OR [E25] IS NULL OR [E26] IS NULL OR [E27] IS NULL
        OR [E28a] IS NULL OR [E28b] IS NULL OR [E29] IS NULL OR [E30] IS NULL OR [E31] IS NULL
        OR [E32] IS NULL OR [CI01] IS NULL OR [CI03] IS NULL OR [CI04] IS NULL OR [DeMinimis] IS NULL
        OR [CE01] IS NULL OR [CE02] IS NULL OR [CE03] IS NULL OR [CE04A] IS NULL OR [CE04B] IS NULL
        OR [CE04C]IS NULL OR [CE04D] IS NULL OR [CE04E] IS NULL OR [B01] IS NULL OR [B02] IS NULL
        OR [B03] IS NULL OR [B05] IS NULL OR [B06] IS NULL OR [B07] IS NULL
    ```

### CFR Fields Data Type

| Column Name                       | Data type |
|-----------------------------------|-----------|
|ReturnStatus                       |nvarchar   |
|HighErrors                         |float      |
|LowErrors                          |float      |
|OKErrors                           |float      |
|DataVersion                        |nvarchar   |
|LA                                 |nvarchar   |
|LANumber                           |float      |
|Estab                              |float      |
|LEAEstab                           |float      |
|COLLECTSchoolName                  |nvarchar   |
|XMLSchoolName                      |nvarchar   |
|FinYear                            |float      |
|OB01                               |float      |
|OB02                               |float      |
|OB03                               |float      |
|I01                                |float      |
|I02                                |float      |
|I03                                |float      |
|I04                                |float      |
|I05                                |float      |
|I06                                |float      |
|I07                                |float      |
|I08a                               |float      |
|I08b                               |float      |
|I09                                |float      |
|I10                                |float      |
|I11                                |float      |
|I12                                |float      |
|I13                                |float      |
|I15                                |float      |
|I16                                |float      |
|I17                                |float      |
|I18c                               |float      |
|I18d                               |float      |
|E01                                |float      |
|E02                                |float      |
|E03                                |float      |
|E04                                |float      |
|E05                                |float      |
|E06                                |float      |
|E07                                |float      |
|E08                                |float      |
|E09                                |float      |
|E10                                |float      |
|E11                                |float      |
|E12                                |float      |
|E13                                |float      |
|E14                                |float      |
|E15                                |float      |
|E16                                |float      |
|E17                                |float      |
|E18                                |float      |
|E19                                |float      |
|E20A                               |float      |
|E20B                               |float      |
|E20C                               |float      |
|E20D                               |float      |
|E20E                               |float      |
|E20F                               |float      |
|E20G                               |float      |
|E21                                |float      |
|E22                                |float      |
|E23                                |float      |
|E24                                |float      |
|E25                                |float      |
|E26                                |float      |
|E27                                |float      |
|E28a                               |float      |
|E28b                               |float      |
|E29                                |float      |
|E30                                |float      |
|E31                                |float      |
|E32                                |float      |
|CI01                               |float      |
|CI03                               |float      |
|CI04                               |float      |
|DeMinimis                          |float      |
|CE01                               |float      |
|CE02                               |float      |
|CE03                               |float      |
|CE04A                              |float      |
|CE04B                              |float      |
|CE04C                              |float      |
|CE04D                              |float      |
|CE04E                              |float      |
|B01                                |float      |
|B02                                |float      |
|B03                                |float      |
|B05                                |float      |
|B06                                |float      |
|B07                                |float      |
|Federated Flag                     |nvarchar   |
|LAEstab of School in Federation 1  |nvarchar   |
|LAEstab of School in Federation 2  |nvarchar   |
|LAEstab of School in Federation 3  |nvarchar   |
|LAEstab of School in Federation 4  |nvarchar   |
|LAEstab of School in Federation 5  |nvarchar   |
|LAEstab of School in Federation 6  |nvarchar   |
|LAEstab of School in Federation 7  |nvarchar   |
|LAEstab of School in Federation 8  |nvarchar   |
|LAEstab of School in Federation 9  |nvarchar   |
|LAEstab of School in Federation 10 |nvarchar   |
|Phase                              |nvarchar   |
|FinancialYear                      |float      |
|InputSystem                        |nvarchar   |
|DataPreparation                    |nvarchar   |
|FullYear                           |nvarchar   |
|CashOrAccruals                     |nvarchar   |
|F109                               |nvarchar   |
|F110                               |nvarchar   |

<!-- Leave the rest of this page blank -->
\newpage
