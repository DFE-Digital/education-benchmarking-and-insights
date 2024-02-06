Feature: School Curriculum and Financial Planning

    Scenario: Go to help page from start page
        Given I am on start page for school with URN '139696'
        When I click the can be found here in the content of page one
        Then I am on the help page

    Scenario: Go back from help page
        Given I am on the help page for school with URN '139696'
        When I click back link on help page
        Then I am on start page
        
    Scenario: Continue to year selection from start page
        Given I am on start page for school with URN '139696'
        When I click continue on start page
        Then I am on the year selection page        
        
    Scenario: Select a year and continue
        Given I am on select a year page for school with URN '139696'
        And I have select year '<Year>'
        When I click continue on select a year page
        Then I am on the pre-populated data page
        
    Examples:
      | Year  |
      | now   |
      | next  |
      | two   |
      | three |        

    Scenario: Go back from year selection
        Given I am on select a year page for school with URN '139696'         
        When I click back link on select a year page
        Then I am on start page