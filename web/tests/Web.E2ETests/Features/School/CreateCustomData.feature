Feature: School create custom data

    Background:
        Given I am on the service home
        And I have signed in with organisation '010: FBIT TEST - Multi-Academy Trust (Open)'
        And I have removed any existing custom data for school with URN '990238' in trust 'FBIT Multi Academy Trust'
        And I am on create custom data page for school with URN '990238'

    Scenario: Can view create custom data page
        When I click start now
        Then the change financial data page is displayed

    Scenario: Can submit custom data
        When I click start now
        And I supply the following financial data:
          | Cost                                      | Value |
          | Administrative supplies (non-educational) | 5000  |
        And I click continue
        And I supply the following non-financial data:
          | Item                                    | Value |
          | Number of pupils (full time equivalent) | 100   |
        And I click continue
        And I supply the following workforce data:
          | Item                                    | Value |
          | School workforce (full time equivalent) | 90    |
        And I save the custom data
        Then the submitted page is displayed

    Scenario: Cannot submit custom data with validation error on user-entered workforce FTE data
        When I click start now
        And I click continue
        And I click continue
        And I supply the following workforce data:
          | Item                                      | Value |
          | School workforce (full time equivalent)   | 90    |
          | Number of teachers (full time equivalent) | 100   |
        And I save the custom data
        Then the validation errors are displayed:
          | Error                                                                                                                |
          | Number of teachers (full time equivalent) cannot be greater than or equal to school workforce (full time equivalent) |

    Scenario: Cannot submit custom data with validation error on backfilled original workforce FTE data
        When I click start now
        And I click continue
        And I click continue
        And I supply the following workforce data:
          | Item                                      | Value |
          | Number of teachers (full time equivalent) | 100   |
        And I save the custom data
        Then the validation errors are displayed:
          | Error                                                                                                                |
          | Number of teachers (full time equivalent) cannot be greater than or equal to school workforce (full time equivalent) |

    Scenario: Cannot submit custom data with validation error on backfilled original teachers FTE data
        When I click start now
        And I click continue
        And I click continue
        And I supply the following workforce data:
          | Item                                     | Value |
          | Senior leadership (full time equivalent) | 100   |
        And I save the custom data
        Then the validation errors are displayed:
          | Error                                                                                                                 |
          | Senior leadership (full time equivalent) cannot be greater than or equal to number of teachers (full time equivalent) |