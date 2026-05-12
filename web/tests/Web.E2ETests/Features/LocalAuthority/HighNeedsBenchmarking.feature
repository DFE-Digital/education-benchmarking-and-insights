@HighNeedsBenchmarkingFlagEnabled
Feature: Local Authority high needs benchmarking

    Background:
        Given I am on local authority high needs start benchmarking for local authority with code '201'
        And I click the Save and continue button

    Scenario: Can change local authorities to benchmark against
        Given I am on local authority high needs benchmarking for local authority with code '201'
        When I click the Change comparators link
        Then the local authority high needs start benchmarking page is displayed

    Scenario: Can view local authority benchmarking charts
        Given I am on local authority high needs benchmarking for local authority with code '201'
        Then chart view is visible, showing '31' charts
        And the legend is visible on all s251 charts

    Scenario: Can view local authority benchmarking tables
        Given I am on local authority high needs benchmarking for local authority with code '201'
        When I click on view as table
        Then table view is visible, showing '31' tables

    Scenario: Can view local authority benchmarking table data for S251
        Given I am on local authority high needs benchmarking for local authority with code '201'
        When I click on view as table
        Then the following is shown for 'Total place funding for special schools and AP/PRUs'
          | Local Authority        | Number of pupils | Outturn (£ per pupil) |
          | Camden                 | 50,101           | £150                  |
          | Hackney                | 57,444           | £120                  |
          | Greenwich              | 63,850           | £36                   |
          | City of London         | 1,650            | £0                    |
          | Hammersmith and Fulham | 18,500           | £0                    |           

    Scenario: Line codes are displayed
        Given I am on local authority high needs benchmarking for local authority with code '201'
        Then the line codes are present
        
    Scenario: Clicking download button downloads .zip file
        Given I am on local authority high needs benchmarking for local authority with code '201'
        When I click on download data
        Then the file 'benchmark-high-needs-spending-outturn-per-pupil-201.zip' is downloaded
        
    Scenario: Clicking Save chart images downloads .zip file
        Given I am on local authority high needs benchmarking for local authority with code '201'
        When I click on save chart images
        Then the file 'benchmark-high-needs-spending-201.zip' is downloaded
