Feature: Local Authority homepage

    Scenario: Go to compare your costs page
        Given I am on local authority homepage for local authority with code '204'
        When I click on compare your costs
        Then the compare your costs page is displayed

    Scenario: Go to benchmark census data page
        Given I am on local authority homepage for local authority with code '204'
        When I click on benchmark census data
        Then the benchmark census page is displayed

    Scenario: Service banner is displayed
        Given I am on local authority homepage for local authority with code '204'
        Then the service banner displays the title 'Local authority home page', heading 'Banner on local authority home page' and body 'This banner has been configured on the automated test environment for the local authority home page only'

    Scenario: Go to High needs benchmarking data page
        Given I am on local authority homepage for local authority with code '204'
        When I click on benchmark high needs
        Then the High needs benchmarking page is displayed

    Scenario: Go to High needs history page
        Given I am on local authority homepage for local authority with code '204'
        When I click on high needs historic data
        Then the High needs historic data page is displayed

    @LocalAuthorityHomepageV2FlagDisabled
    Scenario: Schools accordion is displayed when feature is disabled
        Given I am on local authority homepage for local authority with code '204'
        Then the schools accordion should be displayed

    @LocalAuthorityHomepageV2FlagEnabled
    Scenario: Schools accordion is not displayed when feature is enabled
        Given I am on local authority homepage for local authority with code '204'
        Then the schools accordion should not be displayed

    @LocalAuthorityHomepageV2FlagEnabled
    Scenario: Priority school RAGs is displayed when feature is enabled
        Given I am on local authority homepage for local authority with code '205'
        Then the priority school RAGs section should be displayed for 'Primary schools' containing the following rows:
          | School          | Status                                                    |
          | Test school 240 | 1 high, 0 medium and 0 low priorities for Test school 240 |
          | Test school 102 | 6 high, 2 medium and 0 low priorities for Test school 102 |

    @LocalAuthorityHomepageV2FlagEnabled
    Scenario: Can apply filters on the financal tab
        Given I am on local authority homepage for local authority with code '205'
        And I should see the following table data in financial tab
          | School name     | Pupils | Expenditure (%) | Staffing spend (%) | Revenue reserves (%) |
          | Test school 102 | 212    | 98.3%           | 71.4%              | 10.1%                |
          | Test school 240 | 235    | 98.1%           | 69.4%              | 5.8%                 |
        When I click on show filters on the financial tab
        And I apply has nursery classes filter on the financial tab
        And I click Apply filters on the financial tab
        Then I should see the following table data in financial tab
          | School name     | Pupils | Expenditure (%) | Staffing spend (%) | Revenue reserves (%) |
          | Test school 102 | 212    | 98.3%           | 71.4%              | 10.1%                |
          
    @LocalAuthorityHomepageV2FlagEnabled
    Scenario: Can apply filters on the workforce tab
        Given I am on local authority homepage for local authority with code '201'
        And I click on the workforce tab
        And I should see the following table data in workforce tab
          | School name     | Pupils | Pupil:teacher ratio | EHC plan (%) | SEN support (%) |
          | Test school 237 | 225    | 1.89                | 1.8%         | 5.3%            |
          | Test school 1   | 271    | 1.82                | 5.2%         | 11.8%           |
        When I click on show filters on the workforce tab
        And I apply has nursery classes filter on the workforce tab
        And I click Apply filters on the workforce tab
        Then I should see the following table data in workforce tab
          | School name     | Pupils | Pupil:teacher ratio | EHC plan (%) | SEN support (%) |
          | Test school 237 | 225    | 1.89                | 1.8%         | 5.3%            |