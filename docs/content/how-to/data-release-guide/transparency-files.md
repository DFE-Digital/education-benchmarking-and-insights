---
title: "How to update transparency files"
layout: sub-navigation
sectionKey: "How-to guides"
includeInBreadcrumbs: true
eleventyNavigation:
  key: "Transparency Files"
  parent: "Data Release Guide"
---

Both Consistent Financial Reporting (CFR) and Academies Accounts Return (AAR) data releases include an accompanying transparency file published for download on the FBIT website.

This guide describes how to upload a new transparency file to blob storage, configure container access, and insert the required record into the database so the file becomes available on the service.

## 1. Upload the file to blob storage

Transparency files are resolved through Azure Front Door and served directly from blob storage rather than from the application service.

* **Target storage account:** `<environment-prefix>webassets` (for example, `s198p01webassets` in production, or `s198t01webassets` in test)
* **Target container:** `files`

### Temporarily enable storage account key access

Azure Front Door authenticates with the storage account using a system-assigned managed identity, meaning storage account key access (Shared Access Signature / SAS keys) is disabled by default for security.

Due to Central Infrastructure Platform (CIP) restrictions on Microsoft Entra access control lists (ACLs), standard production credentials cannot write to the container while key access is disabled. You must temporarily enable storage account key access to upload files:

1. In the Azure Portal, navigate to the relevant storage account (for example, `s198p01webassets`).
2. In the left-hand navigation menu, select **Settings > Configuration**.
3. Set **Allow storage account key access** to **Enabled**.
4. Select **Save** and wait a few minutes for the setting to take effect (you may need to refresh the portal or sign out and sign back in).

For full details on origin authentication and Front Door routing behaviour, see the [Web Assets how-to guide](/how-to/web-assets/#storage-account-authentication).

### Upload the file

1. In the storage account, navigate to **Data storage > Containers** and select the `files` container.
2. Select **Upload** and upload the new transparency file (for example, `CFR_2024-25_Full_Data_Workbook.xlsx` or `AAR_2024-25_download.xlsx`).
3. Ensure the file name matches the naming convention used in previous releases.

### Re-disable storage account key access

Once the upload has completed, return to **Settings > Configuration**, set **Allow storage account key access** to **Disabled**, and select **Save**.

## 2. Register the file in the database

The FBIT Content API and web application query active files from the `[dbo].[VW_ActiveFiles]` view, which reads from the `[dbo].[File]` table in the core database.

### File table schema

The `[dbo].[File]` table uses the following columns (defined in database migration `106-CreateFileTable.sql`):

* `Type`: The file type identifier: `'transparency-cfr'` for maintained schools or `'transparency-aar'` for academies.
* `Label`: The display text shown in the user interface on the Data sources page (for example, `'CFR 2024/25'` or `'AAR 2024/25'`).
* `FileName`: The exact file name uploaded to the `files` container.
* `ValidFrom`: A `datetimeoffset` timestamp specifying when the file becomes active. Defaults to `GETUTCDATE()`.
* `ValidTo`: An optional `datetimeoffset` timestamp specifying when the file expires. Leave as `NULL` for indefinite availability.

### Insert query

Run the following SQL script against the database (for example, via Azure Cloud Shell, Azure Portal Query Editor, or SQL Server Management Studio connected to `s198p01-sql` / `data`):

```sql
-- Insert new CFR transparency file
IF NOT EXISTS (
    SELECT 1
    FROM [dbo].[File]
    WHERE [Type] = 'transparency-cfr' AND [Label] = 'CFR 2024/25'
)
BEGIN
    INSERT INTO [dbo].[File] ([Type], [Label], [FileName], [ValidFrom])
    VALUES (
        'transparency-cfr',
        'CFR 2024/25',
        'CFR_2024-25_Full_Data_Workbook.xlsx',
        GETUTCDATE() -- Use GETUTCDATE() for immediate release, or specify a future release date
    );
END;

-- Or insert new AAR transparency file
IF NOT EXISTS (
    SELECT 1
    FROM [dbo].[File]
    WHERE [Type] = 'transparency-aar' AND [Label] = 'AAR 2024/25'
)
BEGIN
    INSERT INTO [dbo].[File] ([Type], [Label], [FileName], [ValidFrom])
    VALUES (
        'transparency-aar',
        'AAR 2024/25',
        'AAR_2024-25_download.xlsx',
        GETUTCDATE()
    );
END;
```

> 💡 **Staging in advance:** To stage a file ahead of the official go-live date, set `ValidFrom` to a future timestamp (such as `DATEADD(day, 7, GETUTCDATE())` or `'2026-09-01T00:00:00+00:00'`). The file will automatically become visible once that timestamp is reached.

## 3. Verify the update

Verify both the database state and the user-facing web page:

1. **Verify database rows:**

   ```sql
   -- Check that the record is present in the table
   SELECT *
   FROM [dbo].[File]
   WHERE [Type] IN ('transparency-cfr', 'transparency-aar')
   ORDER BY [ValidFrom] DESC;

   -- Check that the file is active in the view
   SELECT *
   FROM [dbo].[VW_ActiveFiles]
   WHERE [Type] IN ('transparency-cfr', 'transparency-aar')
   ORDER BY [Label] DESC;
   ```

2. **Verify in the web application:**
   * Go to the **Data sources** page (`/data-sources`).
   * Maintained school files appear under the **Maintained schools** section, and academy files appear under the **Academies** section.
   * Verify that the new label is displayed and that selecting the link downloads the file from `/files/<filename>` with an HTTP 200 response.
