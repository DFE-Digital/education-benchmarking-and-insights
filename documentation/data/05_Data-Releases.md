# Data Releases


## Sourcing the data

When is it released?
WHo tells you when it is released?

## Cleaning the Data

Things to look out for:
    - dtypes not interpreted by the pipeline. e.g. expecting an int but getting a string
    - changes to the column names
    - removal of columns previously used in the service, e.g. KS2 and 4 between 2020 - 2022

## Loading the data for processing

Load to storage container - how and where?


## Running the pipelines

Open Azure storage container
Run Docker
Post message in ...