Feature: School senior leadership

    @SeniorLeadershipFlagEnabled
    Scenario: Before interacting with form options the chart should be displayed
        Given I am on school senior leadership page for school with URN '990001'
        Then I should see the school senior leadership chart

    @SeniorLeadershipFlagEnabled
    Scenario: Clicking school name in chart directs to homepage
        Given I am on school senior leadership page for school with URN '990001'
        When I click on the school name on the chart
        Then I am navigated to selected school home page

    @SeniorLeadershipFlagEnabled
    Scenario: Pressing Enter on school name in chart directs to homepage
        Given I am on school senior leadership page for school with URN '990001'
        When I enter on the school name on the chart
        Then I am navigated to selected school home page

    @SeniorLeadershipFlagEnabled
    Scenario: Hovering on a bar in a chart shows tooltip
        Given I am on school senior leadership page for school with URN '990002'
        When I hover on a bar for the school with urn '990000' in a chart
        Then the tooltip for 'Test school 176' is correctly displayed

    @SeniorLeadershipFlagEnabled
    Scenario: Tabbing between school names in chart shows tooltip
        Given I am on school senior leadership page for school with URN '990002'
        And the focused element is the save as image control
        When I press tab to select the school with urn '990000' in a chart
        Then the tooltip for 'Test school 176' is correctly displayed
        When I press tab to select the school with urn '990027' in a chart
        Then the tooltip for 'Test school 215' is correctly displayed
    
    @SeniorLeadershipFlagEnabled    
    Scenario: Save as image button starts download
        Given I am on school senior leadership page for school with URN '990002'
        When I click on save as image
        Then the 'senior-leadership-composition-full-time-equivalent' chart image is downloaded