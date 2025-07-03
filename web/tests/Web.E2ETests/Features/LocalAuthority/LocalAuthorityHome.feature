Feature: Local Authority homepage

    Scenario: Go to compare your costs page
        Given I am on local authority homepage for local authority with code '204'
        When I click on compare your costs
        Then the compare your costs page is displayed

    Scenario: Go to benchmark census data page
        Given I am on local authority homepage for local authority with code '204'
        When I click on benchmark census data
        Then the benchmark census page is displayed

    @HighNeedsFlagEnabled
    Scenario: Go to high needs benchmarking page
        Given I am on local authority homepage for local authority with code '204'
        When I click on high needs benchmarking
        Then the high needs benchmarking dashboard page is displayed

    Scenario: Service banner is displayed
        Given I am on local authority homepage for local authority with code '204'
        Then the service banner displays the title 'Local authority home page', heading 'Banner on local authority home page' and body 'This banner has been configured on the automated test environment for the local authority home page only'