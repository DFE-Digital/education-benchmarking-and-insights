# Data Seeding in Testing Environment (d02)

This guide provides steps for updating and seeding data in the **d02 testing environment** using the database copy available on SharePoint.

## Database Copy Location

The database copy is stored in SharePoint and can be accessed through the following link:  
[Download d02 Database (d02sqldb.xls)](https://educationgovuk.sharepoint.com/:x:/r/sites/DfEFinancialBenchmarking/Shared%20Documents/General/Technical%20Team/QA%20Testing/Automated%20Testing%20data%20d02/d02sqldb.xls?d=wc7741d0709834e29b342c1b7aa96f47a&csf=1&web=1&e=UCi1mh).

## Steps to Update Data for Tests

1. **Access and Download the File**  
   Download the file `d02sqldb.xls` from the above link. This file contains the data tables used in the d02 testing environment.

2. **Modify the Data**  
   Open the file and make the required changes to the data tables for your test scenarios.

3. **Prepare for Data Upload**
    - Clear the relevant database table you want to update in SQL Server before uploading the new data.
    - Use your preferred method to upload or upsert the data into the database. The recommended approach is using Microsoft SQL Server Management Studio (SSMS).

## Uploading Data Using SSMS Import Wizard

### Step-by-Step Guide:

1. **Open Microsoft SQL Server Management Studio**  
   Connect to the `d02` SQL database using the connection details from the Azure `d02` resource (get the server name, username, and password from the resource settings).

2. **Clear the Target Table**  
   Before importing the new data, ensure that you clear the table you are updating.

3. **Launch the Import Wizard**
    - Right-click on the target database in SSMS.
    - Navigate to `Tasks` -> `Import Data...`.
    - This will open the SQL Server Import and Export Wizard.

4. **Select the Data Source**
    - In the wizard, select `Microsoft Excel` as the data source.
    - Provide the path to the downloaded `d02sqldb.xls` file.

5. **Configure the Destination**
    - In the destination section, select `Microsoft OLE DB Provider for SQL Server`.
    - Enter the server details, including server name, username, and password (retrieved from the Azure d02 resource).

6. **Follow the Wizard**
    - Click `Next` and follow the wizard steps to map the columns from the Excel sheet to the SQL table.
    - Review the mappings and finish the import process.

7. **Verify Data**  
   After the data is imported, verify that the changes are reflected in the database by querying the table.

8. **Replace the Updated Excel File in SharePoint**  
   Once the data changes are complete, **upload the modified `d02sqldb.xls` file back to SharePoint** to ensure that the latest version is available for future use. This way, the next person will have the most up-to-date copy.

    - Go to the SharePoint folder where the file is stored.
    - Replace the existing `d02sqldb.xls` file with the updated version by dragging and dropping the file or using the upload feature.
    - Ensure the file is replaced successfully and is accessible via the same link.

---

## Additional Information

- You can use any preferred method to upload or upsert data into the database (e.g., scripts, custom tools), but this guide recommends the use of the **SSMS Import Wizard** for ease of use.
- Ensure you maintain the integrity of the data format when modifying the Excel file to avoid import issues.

---
