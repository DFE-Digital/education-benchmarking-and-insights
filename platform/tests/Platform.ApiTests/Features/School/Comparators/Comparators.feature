Feature: School Comparators

  Scenario: Post comparators with fully populated characteristics body
    Given a valid comparators request with URN '777042' and a fully populated characteristics body
    When I submit the comparators request
    Then the comparators result should be 'ok' and match the expected output in 'comparators-fully-populated.json'

  Scenario: Post comparators with an empty characteristics body
    Given a valid comparators request with URN '777042' and an empty characteristics body
    When I submit the comparators request
    Then the comparators result should be 'ok' and match the expected output in 'comparators-empty.json'

  Scenario: Post comparators with a partially populated characteristics body
    Given a valid comparators request with URN '777042' and a partially populated characteristics body
    When I submit the comparators request
    Then the comparators result should be 'ok' and match the expected output in 'comparators-partially-populated.json'

  Scenario Outline: Post comparators returns bad request for invalid parameters
    Given an invalid comparators request with <parameter>
    When I submit the comparators request
    Then the comparators result should be 'bad request' and match the expected output in '<expected_file>'

    Examples:
      | parameter           | expected_file                        |
      | invalid api version | bad-request-invalid-version.json     |
