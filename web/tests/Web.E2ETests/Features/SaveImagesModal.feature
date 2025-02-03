Feature: Save chart images modal

    Scenario: Save chart images button opens modal
        Given I am on spending and costs page for school with URN '777042'
        Then the save chart images button is visible
        When I click the save chart images button
        Then the save chart images modal is visible
        And the start button is enabled
        And the cancel button is visible
        And the close button is visible

    Scenario: Cancel closes modal
        Given I am on spending and costs page for school with URN '777042'
        When I click the save chart images button
        And I click the cancel button
        Then the save chart images modal is not visible

    Scenario: Close closes modal
        Given I am on spending and costs page for school with URN '777042'
        When I click the save chart images button
        And I click the close button
        Then the save chart images modal is not visible

    Scenario: Esc key closes modal
        Given I am on spending and costs page for school with URN '777042'
        When I click the save chart images button
        And I press the Escape key
        Then the save chart images modal is not visible

    Scenario: Start begins download
        Given I am on spending and costs page for school with URN '777042'
        When I click the save chart images button
        And I click the start button
        Then the start button is disabled
        And the 'spending-priorities-777042.zip' file is downloaded

    Scenario: Validation error if none selected
        Given I am on spending and costs page for school with URN '777042'
        When I click the save chart images button
        And I uncheck the following items:
          | Title                               |
          | Teaching and Teaching support staff |
          | Non-educational support staff       |
          | Administrative supplies             |
          | Educational supplies                |
          | Catering staff and supplies         |
          | Premises staff and services         |
          | Utilities                           |
          | Educational ICT                     |
        And I click the start button without any items selected
        Then the validation error is displayed