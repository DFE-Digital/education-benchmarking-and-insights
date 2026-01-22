Feature: School senior leadership

    Scenario: Before interacting with form options the chart should be displayed
        Given I am on school senior leadership page for school with URN '990001'
        Then I should see the school senior leadership chart

    Scenario: Clicking school name in chart directs to homepage
        Given I am on school senior leadership page for school with URN '990001'
        When I click on the school name on the chart
        Then I am navigated to selected school home page

    Scenario: Pressing Enter on school name in chart directs to homepage
        Given I am on school senior leadership page for school with URN '990001'
        When I enter on the school name on the chart
        Then I am navigated to selected school home page

    Scenario: Hovering on a bar in a chart shows tooltip
        Given I am on school senior leadership page for school with URN '990002'
        When I hover on a bar for the school with urn '990000' in a chart
        Then the tooltip for 'Test school 176' is correctly displayed

    Scenario: Tabbing between school names in chart shows tooltip
        Given I am on school senior leadership page for school with URN '990002'
        And the focused element is the save as image control
        When I press tab to select the school with urn '990000' in a chart
        Then the tooltip for 'Test school 176' is correctly displayed
        When I press tab to select the school with urn '990027' in a chart
        Then the tooltip for 'Test school 215' is correctly displayed