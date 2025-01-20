Feature: School create custom data

    Background:
        Given I am on create custom data page for school with URN '990234'
        And I have signed in with organisation '010: FBIT TEST - Multi-Academy Trust (Open)'

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
          | School workforce (full time equivalent) | 20    |
        And I save the custom data
        Then the submitted page is displayed