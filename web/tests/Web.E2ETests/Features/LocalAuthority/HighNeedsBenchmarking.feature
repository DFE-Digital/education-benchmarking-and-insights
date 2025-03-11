Feature: Local Authority high needs benchmarking

    Background:
        Given I am on local authority high needs start benchmarking for local authority with code '201'
        And I have no comparators selected
        And I add the comparator matching the value 'hack'
        And I click the Save and continue button

    @HighNeedsFlagEnabled
    Scenario: Can view local authority benchmarking charts
        Given I am on local authority high needs benchmarking for local authority with code '201'
        When I click on show all sections
        Then all sections on the page are expanded
        And chart view is visible, showing '33' charts

    @HighNeedsFlagEnabled
    Scenario: Can view local authority benchmarking tables
        Given I am on local authority high needs benchmarking for local authority with code '201'
        When I click on show all sections
        And I click on view as table
        Then all sections on the page are expanded
        And table view is visible, showing '33' tables

    @HighNeedsFlagEnabled
    Scenario: Can view local authority benchmarking table data for S251
        Given I am on local authority high needs benchmarking for local authority with code '201'
        When I click on show all sections
        And I click on view as table
        Then the table at index '0' contains the following S251 values:
          | Name                | Actual     | Planned    |
          | Local authority 201 | £1,002,226 | £1,102,226 |
          | Local authority 204 | £1,002,229 | £1,102,229 |

    @HighNeedsFlagEnabled
    Scenario: Can view local authority benchmarking table data for SEND2
        Given I am on local authority high needs benchmarking for local authority with code '201'
        When I click on show all sections
        And I click on view as table
        Then the table at index '32' contains the following SEND2 values:
          | Name                | Amount |
          | Local authority 201 | £1.00  |
          | Local authority 204 | £5.00  |