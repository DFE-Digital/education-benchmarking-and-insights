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

    Scenario: Hovering on a bar in a chart shows tooltip
        Given I am on it spend page for school with URN '777042'
        When I hover on a bar for the school with urn '777042' in a chart
        Then the tooltip for 'Test school 102' is correctly displayed

    Scenario: Hovering on a bar in a chart shows tooltip for part year school
        Given I am on it spend page for school with URN '990004'
        When I hover on a bar for the school with urn '777043' in a chart
        Then the tooltip for 'Test Part year school with pupil and builiding comparators' is correctly displayed with part year warning for 10 months
        
    Scenario: Tabbing between school names in chart shows tooltip
        Given I am on it spend page for school with URN '777042'
        And the focused element is the last filter button
        When I press tab to select the school with urn '990218' in a chart
        Then the tooltip for 'Test school 263' is correctly displayed
        When I press tab to select the school with urn '777042' in a chart
        Then the tooltip for 'Test school 102' is correctly displayed
        
    Scenario: Save chart images button opens modal and starts download
        Given I am on it spend page for school with URN '777042'
        Then the save chart images button is visible
        When I click the save chart images button
        Then the save chart images modal is visible
        And the 'benchmark-it-spending-777042.zip' file is downloaded