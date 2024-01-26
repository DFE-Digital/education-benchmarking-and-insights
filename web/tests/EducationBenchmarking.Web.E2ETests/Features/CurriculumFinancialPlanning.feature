Feature: CurriculumFinancialPlanning
Integrated Curriculum and financial planning feature

    Scenario: see help page of curriculum and financial planning
        Given I am on page 1 of the curriculum and financial planning journey for school with URN '139696'
        When I click the can be found here in the content of page one
        Then I am on the help page

    Scenario: Go back from help page
        Given I am on the help page for school with URN '139696'
        When I click back link on help page
        Then I am on page 1 of the curriculum and financial planning journey