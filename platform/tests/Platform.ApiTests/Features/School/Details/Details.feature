Feature: School Details

    Scenario: Valid single school request
        Given I have a valid single school request
        When I submit the single school request
        Then the single school result should be 'ok' and match the expected output in 'SingleSchool.json'

    Scenario: Single school request with invalid API version
        Given I have a valid single school request
        And the request has an invalid API version
        When I submit the single school request
        Then the single school result should be 'bad request'

    Scenario: Single school request for non-existent school
        Given I have a non-existent single school request
        When I submit the single school request
        Then the single school result should be 'not found'

    Scenario: Valid collection of schools request
        Given I have a valid collection of schools request
        When I submit the collection of schools request
        Then the collection of schools result should be 'ok' and match the expected output in 'SchoolsCollection.json'

    Scenario: Collection of schools request with invalid API version
        Given I have a valid collection of schools request
        And the request has an invalid API version
        When I submit the collection of schools request
        Then the collection of schools result should be 'bad request'

    Scenario: Valid single school characteristics request
        Given I have a valid single school characteristics request
        When I submit the single school characteristics request
        Then the single school characteristics result should be 'ok' and match the expected output in 'SingleSchoolCharacteristics.json'

    Scenario: Single school characteristics request with invalid API version
        Given I have a valid single school characteristics request
        And the request has an invalid API version
        When I submit the single school characteristics request
        Then the single school characteristics result should be 'bad request'

    Scenario: Single school characteristics request for non-existent school
        Given I have a non-existent single school characteristics request
        When I submit the single school characteristics request
        Then the single school characteristics result should be 'not found'
