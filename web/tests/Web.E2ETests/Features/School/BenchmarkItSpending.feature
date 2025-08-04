Feature: School benchmark IT spending
    
    Scenario: Before interacting with filters all charts should be displayed
        Given I am on it spend page for school with URN '777042'
        Then I should see the following IT spend charts:
          | Chart Title                              |
          | Administration software and systems E20D |
          | Connectivity E20A                        |
          | IT learning resources E20C               |
          | IT support E20G                          |
          | Laptops, desktops and tablets E20E       |
          | Onsite servers E20B                      |
          | Other hardware E20F                      |

    Scenario: Clicking school name in chart directs to homepage
        Given I am on it spend page for school with URN '777042'
        When I click on the school name on the chart
        Then I am navigated to selected school home page
        
    Scenario: Pressing Enter on school name in chart directs to homepage
        Given I am on it spend page for school with URN '777042'
        When I enter on the school name on the chart
        Then I am navigated to selected school home page
