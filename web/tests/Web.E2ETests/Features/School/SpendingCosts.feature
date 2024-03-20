Feature: School spending and costs

    Scenario: View how we choose similar school details
        Given I am on spending and costs page for school with URN '118168'
        When I click on how we choose similar schools
        Then the details section is expanded

    Scenario: View all teaching and teaching supply staff
        Given I am on spending and costs page for school with URN '118168'
        And the following is shown
          | This school spends | Similar schools spend | This school spends |
          | 4,133              | 4,398                 | 265                |
        When I click on 'View all teaching and teaching supply staff costs'
        Then I am directed to compare your costs page
        And the accordion 'teaching and teaching supply staff' is expanded

    Scenario: View all administrative supplies
        Given I am on spending and costs page for school with URN '118168'
        And the following is shown
          | This school spends | Similar schools spend | This school spends |
          | 23                 | 47                    | 24                 |
        When I click on 'View all teaching and teaching supply staff costs'
        Then I am directed to compare your costs page
        And the accordion 'teaching and teaching supply staff' is expanded