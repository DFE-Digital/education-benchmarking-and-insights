# Test Data Preparation and Transformation Process

## Prerequisite
Download existing Cosmos using Azure DMT. [Azure DMT Guide](https://learn.microsoft.com/en-us/azure/cosmos-db/how-to-migrate-desktop-tool?tabs=azure-cli)

An example of migration settings is as below:
```` json
{
	"Source": "cosmos-nosql",
	"SourceSettings": {
		"ConnectionString": "{connection string here}",
		"Database": "{}",
		"Container": "{}",
		"PartitionKeyPath": "{refer to the container}"
	},
	"Sink": "json",
	"SinkSettings": {
		"FilePath": "{path followed by file name}"
	}
} 
````
## Filtering of data
Now that you have downloaded the db locally, follow the below steps to filter out data for test data.

### Step 1
From maintained file, filter out 50 of each having the overall phase as below and save them in a new file. 
- Primary
- Secondary
- Pupil referral unit
- Special
- All-through
- Nursery

### Step 2
From Academies file, filter out 50 records each which has below overall phase and  "MAT SAT or Central Services": "MAT"
- Primary
- Secondary
- All-through
- Special
- Pupil referral unit
- 16 plus

From Academies file, filter out 50 records each which has below overall phase and "MAT SAT or Central Services": "SAT"
- Primary
- Secondary
- All-through
- Special
- Pupil referral unit
- 16 plus

### Step 3
Now that we have extracted the data, it's time to keep only the filtered records in GIAS files. Match the URN of extracted files (academies and schools) with the URN in the GIAS file and only keep those records.

### Step 4
Filter out MAT allocations data and only keep the records that match with the URN of step 2 data.

### Step 5
Filter and copy TrustHistoryxxx.json and only keep the records of the trust which we have filtered in step 2. Match the URNs from TrustHistory with the academies file and keep the matching records.

### Step 6
Filter out Floor Area with matching URN from step 1 and step 2.

### Step 7
Match the UID from trustHistory.json to the UID in MAT Central, Overview, and MAT total and keep only records that match.

## Changing filtered data to dummy data
### Step 8
Update Maintained school names to test school 1,2,3.

### Step 9
Update Academy names to Test Academy school 1,2,3.

### Step 10
Match maintained school URNs with GIAS files and replace the EstablishmentName of the matched ones with updated School name from the maintained school file (step 8).

### Step 11
Match Academies school URNs with GIAS files and replace the EstablishmentName of the matched ones with updated School name from the academies file (step 9).

### Step 12
Match MAT Allocs with academies URN and if matches update the MAT file school names with updated Academy school name (step 9).

### Step 13
Match the floor area URNs with maintained schools and replace the school name with the updated school name from the maintained school file.

### Step 14
Match floor area URNs with academies schools and replace the school name in floor area with the updated academy school name.

### Step 15
Match the TrustHistory URNs with the academies schools and replace the EstablishmentName with the academy name.

### Step 16
Update GroupName in filtered TrustHistory to dummy names.

### Step 17
Update the "TrustOrCompanyName" from MAT files to "GroupName" from TrustHistory whose UID matches with TrustHistory file.

### Step 18
Update the TrustOrCompanyName of academies file to GroupName of trusthistory file whose UID matches.

### Finally
once the data is filtered and udpated it can be updated into the testing environment using DMT.

### Adding Comparator set data
Comparator set data from development environment is copied to automated testing environment. The SQL database is then edited to update with the URNs that we have filtered in the above steps. This can be edited as of need base to add/update comparator sets for a particular school. 
