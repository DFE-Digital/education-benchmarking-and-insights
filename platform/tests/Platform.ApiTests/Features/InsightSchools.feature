Feature: Insights schools endpoints

    Scenario: Sending a valid school characteristics request
        Given a valid school characteristics request with urn '990000'
        When I submit the insight schools request
        Then the school characteristics result should be ok and match the expected output of 'SchoolCharacteristics.json'

    Scenario: Sending an invalid school characteristics request
        Given an invalid school characteristics request with urn '1000000'
        When I submit the insight schools request
        Then the school characteristics result should be not found

    Scenario: Sending valid school characteristics requests
        Given a valid school characteristics request with urns:
          | Urn    |
          | 990000 |
          | 990001 |
          | 990002 |
        When I submit the insight schools request
        Then the schools characteristics result should be ok and match the expected output of 'SchoolsCharacteristics.json'