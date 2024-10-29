# Data Seeding in Testing Environment (d02)

This guide provides steps for updating and seeding data in the **d02 testing environment** using the files located in the repository's `test-data` folder.

## Data Location

All relevant data files are now stored in the repository under the `test-data` folder. If any file needs to be updated, make the necessary changes to the file in the repository and subsequently update the database as described below.

## Steps to Update Data for Tests

1. **Access the File in the Repository**  
   Navigate to the `test-data` folder in the repository to locate the file you need updating.

2. **Modify the Data**  
   Open the file and make the required changes to the data for your test scenarios.

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
   - In the wizard, select `Flat file` as the data source.
   - Provide the path to the file from the `test-data` folder in your local environment.

5. **Configure the Destination**
   - In the destination section, select `Microsoft OLE DB Provider for SQL Server`.
   - Enter the server details, including server name, username, and password (retrieved from the Azure d02 resource).

6. **Follow the Wizard**
   - Click `Next` and follow the wizard steps to map the columns from your data file to the SQL table.
   - Review the mappings and finish the import process.

7. **Verify Data**  
   After the data is imported, verify that the changes are reflected in the database by querying the table.

8. **Commit and Push Changes to Repository**  
   Once the data changes are complete, **commit the updated file back to the repository** in the `test-data` folder. This ensures that the latest version is available for future use.

---

## Additional Information

- You can use any preferred method to upload or upsert data into the database (e.g., scripts, custom tools), but this guide recommends the use of the **SSMS Import Wizard** for ease of use.
- Ensure you maintain the integrity of the data format when modifying files to avoid import issues.

---
