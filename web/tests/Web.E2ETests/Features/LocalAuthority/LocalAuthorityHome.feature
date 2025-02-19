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
        Then the high needs benchmarking page is displayed