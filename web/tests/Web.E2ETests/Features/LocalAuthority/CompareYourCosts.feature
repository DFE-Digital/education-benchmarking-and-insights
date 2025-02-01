Feature: Local Authority compare your costs

    Scenario: Download total expenditure chart
        Given I am on compare your costs page for local authority with code '201'
        When I click on save as image for 'total expenditure'
        Then the 'total expenditure' chart image is downloaded

    Scenario: Copy total expenditure chart
        Given I am on compare your costs page for local authority with code '201'
        When I click on copy image for 'total expenditure'
        Then the 'total expenditure' chart image is copied

    Scenario: Change dimension of total expenditure and change view to table
        Given I am on compare your costs page for local authority with code '201'
        And the 'total expenditure' dimension is '£ per pupil'
        When I change 'total expenditure' dimension to 'actuals'
        Then the 'total expenditure' dimension is 'actuals'

    Scenario: Change dimension in table view for total expenditure
        Given I am on compare your costs page for local authority with code '201'
        And table view is selected
        And the 'total expenditure' dimension is '£ per pupil'
        When I change 'total expenditure' dimension to 'actuals'
        Then the following is shown for 'total expenditure'
          | School name             | Local Authority | School type            | Number of pupils | Amount     |
          | Test academy school 239 | City of London  | Academy converter      | 399              | £3,424,906 |
          | Test school 237         | City of London  | Voluntary aided school | 231              | £1,597,953 |
          | Test school 1           | City of London  | Voluntary aided school | 232              | £1,580,913 |
        But save as image buttons are hidden
        And copy image buttons are hidden

    Scenario: Show all should expand all sections
        Given I am on compare your costs page for local authority with code '201'
        When I click on show all sections
        Then all sections on the page are expanded
        And the show all text changes to hide all sections

    Scenario: Change all charts to table view
        Given I am on compare your costs page for local authority with code '201'
        And all sections are shown
        When I click on view as table
        Then all sections on the page are expanded
        And are showing table view
        But save as image buttons are hidden
        And copy image buttons are hidden

    Scenario: Hide single section
        Given I am on compare your costs page for local authority with code '201'
        And all sections are shown
        When I click section link for 'non educational support staff'
        Then the section 'non educational support staff' is hidden

    Scenario: View additional details upon hover
        Given I am on compare your costs page for local authority with code '201'
        When I hover over a chart bar
        Then additional information is displayed
        And additional information contains
          | Item             | Value             |
          | Local authority  | City of London    |
          | School type      | Academy converter |
          | Number of pupils | 399               |

    Scenario: Clicking school name in chart directs to homepage
        Given I am on compare your costs page for local authority with code '201'
        When I select the school name on the chart
        Then I am navigated to selected school home page with Trust name 'Test Company/Trust 31'

    Scenario: Tabbing to and selecting school name in chart directs to homepage
        Given I am on compare your costs page for local authority with code '201'
        When I tab to the school name on the chart
        And I press the Enter key when focused on the school name
        Then I am navigated to selected school home page with Trust name 'Test Company/Trust 31'

    Scenario: Tabbing to school name in chart displays tooltip
        Given I am on compare your costs page for local authority with code '201'
        When I tab to the school name on the chart
        Then I can view the associated tooltip

    Scenario: View Catering staff and services using Gross figures
        Given I am on compare your costs page for local authority with code '201'
        And table view is selected
        And Section 'catering staff' is visible
        And the 'catering staff' dimension is 'actuals'
        Then the following is shown for 'catering staff'
          | School name             | Local Authority | School type            | Number of pupils | Amount  |
          | Test academy school 239 | City of London  | Academy converter      | 399              | £95,978 |
          | Test school 237         | City of London  | Voluntary aided school | 231              | £50,014 |
          | Test school 1           | City of London  | Voluntary aided school | 232              | £34,924 |

    Scenario: View Catering staff and services using Net figures
        Given I am on compare your costs page for local authority with code '201'
        And table view is selected
        And Section 'catering staff' is visible
        And the 'catering staff' dimension is 'actuals'
        When I click on display as Net
        Then the following is shown for 'catering staff'
          | School name             | Local Authority | School type            | Number of pupils | Amount   |
          | Test academy school 239 | City of London  | Academy converter      | 399              | £109,578 |
          | Test school 237         | City of London  | Voluntary aided school | 231              | £99,307  |
          | Test school 1           | City of London  | Voluntary aided school | 232              | £42,585  |
          
    Scenario: Charts have correct dimension options
        Given I am on compare your costs page for local authority with code '201'
        Then all sections on the page have the correct dimension options