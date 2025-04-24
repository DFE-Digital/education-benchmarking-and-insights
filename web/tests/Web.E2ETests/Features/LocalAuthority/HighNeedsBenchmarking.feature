Feature: Local Authority high needs benchmarking

    Background:
        Given I am on local authority high needs start benchmarking for local authority with code '201'
        And I have no comparators selected
        And I add the comparator matching the value 'hack'
        And I click the Save and continue button

    @HighNeedsFlagEnabled
    Scenario: Can view local authority benchmarking commentary
        Given I am on local authority high needs benchmarking for local authority with code '201'
        Then comparator commentary label is visible, showing local authority count of '1'

    @HighNeedsFlagEnabled
    Scenario: Can click local authority benchmarking CTA
        Given I am on local authority high needs benchmarking for local authority with code '201'
        When I click the Change comparators button
        Then the local authority high needs start benchmarking page is displayed

    @HighNeedsFlagEnabled
    Scenario: Can view local authority benchmarking charts
        Given I am on local authority high needs benchmarking for local authority with code '201'
        Then chart view is visible, showing '33' charts

    @HighNeedsFlagEnabled
    Scenario: Can view local authority benchmarking tables
        Given I am on local authority high needs benchmarking for local authority with code '201'
        When I click on view as table
        Then table view is visible, showing '33' tables

    @HighNeedsFlagEnabled
    Scenario: Can view local authority benchmarking table data for S251
        Given I am on local authority high needs benchmarking for local authority with code '201'
        When I click on view as table
        Then the table for 'High needs amount per head 2-18 population' contains the following S251 values:
          | Name           | Actual  | Planned | Population |
          | City of London | £0.00   | £0.00   | 1,756      |
          | Hackney        | £115.22 | £113.85 | 59,677     |

    @HighNeedsFlagEnabled
    Scenario: Can view local authority benchmarking table data for SEND2
        Given I am on local authority high needs benchmarking for local authority with code '201'
        When I click on view as table
        Then the table for 'Number aged up to 25 with SEN statement or EHC plan' contains the following SEND2 values:
          | Name           | Amount | Population |
          | City of London | 10.82  | 1,756      |
          | Hackney        | 51.31  | 59,677     |
    
    @HighNeedsFlagEnabled      
    Scenario: Line codes are displayed
        Given I am on local authority high needs benchmarking for local authority with code '201'
        Then the line codes are present 