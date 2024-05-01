# Test Data Storage and Retrieval Decision Document

## Problem Statement
We've modified existing data from d01 to update our test environment data. Currently, this data resides in both Cosmos and SQL databases. However, if the test environment is destroyed, it would pose a significant obstacle for us to regenerate the test data.

## Proposed Solution
To mitigate the risk of losing test data and streamline its retrieval process, we propose the following solution:
- **Blob Storage**:
    - Store the test data in blob storage.
    - Blob storage offers durability and scalability, ensuring data availability even if the test environment is disrupted.

### Cosmos Containers:
#### Prerequisites:
- **Azure Data Factory**:
    - Utilise Azure Data Factory to convert JSON files to Excel format and store them in blob storage.
    - Excel format provides easy visualisation and transformation options if required.

#### Data Loading in Environment:
- Use Azure Data Factory to read data from blob storage and load it into the testing environment.

### SQL Database:
#### Prerequisites:
- **Transformation to Excel**:
    - Transform SQL tables into Excel format and store them in blob storage.
    - This ensures that SQL data can be easily accessed and utilised alongside other data formats.

#### Data Loading in Environment:
- Use Azure Data Factory to read data from blob storage and load it into the testing environment.

## Future Scope:
- **Automation Pipeline**:
    - Develop a pipeline that automates the entire process triggered by events or schedules.
    - This pipeline would replace manual tasks currently performed by Azure Data Factory, enhancing efficiency and reliability.

By implementing this solution, we ensure that our test data remains accessible and resilient, reducing dependencies and mitigating risks associated with potential environment disruptions.



