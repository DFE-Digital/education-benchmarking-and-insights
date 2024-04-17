@ignore
Feature: InsightMaintainedSchools
Insights maintained schools endpoints

    Scenario: Sending a valid maintained school request
        Given a valid maintained school request with urn '125491'
        When I submit the maintained school request
        Then the maintained school result should be ok

    Scenario: Sending an invalid maintained school should return not found
        Given a invalid maintained school request
        When I submit the maintained school request
        Then the maintained school result should be not found