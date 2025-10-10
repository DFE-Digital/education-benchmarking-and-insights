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
    Scenario: Schools accordion is displayed when feature is enabled
        Given I am on local authority homepage for local authority with code '204'
        Then the schools accordion should be displayed
        
    @LocalAuthorityHomepageV2FlagEnabled
    Scenario: Schools accordion is not displayed when feature is enabled
        Given I am on local authority homepage for local authority with code '204'
        Then the schools accordion should not be displayed
