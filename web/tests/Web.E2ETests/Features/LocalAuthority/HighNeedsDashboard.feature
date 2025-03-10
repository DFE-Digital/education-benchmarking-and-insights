Feature: Local Authority high needs dashboard

    @HighNeedsFlagEnabled
    Scenario: Go to start benchmarking page
        Given I am on local authority high needs benchmarking for local authority with code '204'
        When I click on start benchmarking
        Then the start benchmarking page is displayed

    @HighNeedsFlagEnabled
    Scenario: Go to view national rankings
        Given I am on local authority high needs benchmarking for local authority with code '204'
        When I click on view national rankings
        Then the national rankings page is displayed

    @HighNeedsFlagEnabled
    Scenario: Go to view historic data
        Given I am on local authority high needs benchmarking for local authority with code '204'
        When I click on view historic data
        Then the historic data page is displayed