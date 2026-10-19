Feature: Local Authority Risk Score File

Scenario: Get local authority risk score file with a valid year
    Given a local authority risk score file request with year '2022'
    When I submit the request
    Then the 'risk score file' result status should be 'ok'
    And the 'risk score file' result should match the expected output in 'risk-score-file.json'

Scenario Outline: Get local authority risk score file with a not found year
    Given a local authority risk score file request with year 'NotFound'
    When I submit the request
    Then the 'risk score file' result status should be 'not found'
