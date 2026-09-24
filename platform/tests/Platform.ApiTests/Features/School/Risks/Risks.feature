Feature: School Risks

  Scenario: Get school risks history with a valid URN
    Given a school risks history request with URN '990116'
      When I submit the request
    Then the 'history' result should be 'ok' and match the expected output in 'history.json'

  Scenario Outline: Get school risks history with a not found URN
    Given a school risks history request with URN 'NotFound'
    When I submit the request
    Then the 'history' result should be 'not found' and match the expected output in ''
