Feature: School curriculum and financial planning

    Scenario: Go to help page from start page
        Given I am on start page for school with URN '139696'
        When I click can be found here on start page
        Then the help page is displayed

    Scenario: Go back from help page
        Given I am on the help page for school with URN '139696'
        When I click back link on help page
        Then the start page is displayed
        
    Scenario: Continue to year selection from start page
        Given I am on start page for school with URN '139696'
        When I click continue on start page
        Then the year selection page is displayed         
        
    Scenario Outline: Select a year and continue
        Given I am on year selection page for school with URN '139696'
        And '<Year>' is selected 
        When I click continue on year selection page
        Then the pre-populated data page is displayed
        
    Examples:
      | Year  |
      | now   |
      | next  |
      | two   |
      | three |        

    Scenario: Go back from year selection
        Given I am on year selection page for school with URN '139696'         
        When I click back link on year selection page
        Then the start page is displayed