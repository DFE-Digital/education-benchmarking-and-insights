Feature: School Risks

    # --- Risks History ---
    Scenario: Get school risks history with a valid URN
        Given a school risks history request with URN '990116'
        When I submit the request
        Then the 'history' result status should be 'ok'
        And the 'history' result should match the expected output in 'history.json'

    Scenario Outline: Get school risks history with a not found URN
        Given a school risks history request with URN 'NotFound'
        When I submit the request
        Then the 'history' result status should be 'not found'

    # --- Risks ---
    Scenario: Get school risks with a valid URN
        Given a school risks request with URN '990116'
        When I submit the request
        Then the 'risks' result status should be 'ok'
        And the 'risks' result should match the expected output in 'risks.json'

    Scenario Outline: Get school risks with a not found URN
        Given a school risks request with URN 'NotFound'
        When I submit the request
        Then the 'risks' result status should be 'not found'

    # --- Risks Metrics ---
    Scenario: Get school risks metrics with a valid URN
        Given a school risks metrics request with URN '990116'
        When I submit the request
        Then the 'metrics' result status should be 'ok'
        And the 'metrics' result should match the expected output in 'metrics.json'

    Scenario Outline: Get school risks metrics with a not found URN
        Given a school risks metrics request with URN 'NotFound'
        When I submit the request
        Then the 'metrics' result status should be 'not found'
