Feature: Trust compare your costs

    Scenario: Download total expenditure chart
        Given I am on compare your costs page for trust with company number '10074054'
        When I click on save as image for 'total expenditure'
        Then the 'total expenditure' chart image is downloaded

    Scenario: Copy total expenditure chart
        Given I am on compare your costs page for trust with company number '10074054'
        When I click on copy image for 'total expenditure'
        Then the 'total expenditure' chart image is copied

    Scenario: Change dimension of total expenditure and change view to table
        Given I am on compare your costs page for trust with company number '10074054'
        And the 'total expenditure' dimension is '£ per pupil'
        When I change 'total expenditure' dimension to 'actuals'
        Then the 'total expenditure' dimension is 'actuals'

    Scenario: Change dimension in table view for total expenditure
        Given I am on compare your costs page for trust with company number '10074054'
        And table view is selected
        And the 'total expenditure' dimension is '£ per pupil'
        When I change 'total expenditure' dimension to 'actuals'
        Then the following is shown for 'total expenditure'
          | School name             | Local Authority | School type                 | Number of pupils | Amount     |
          | Test academy school 375 | Reading         | Academy special sponsor led | 232              | £1,580,913 |
          | Test academy school 392 | Trafford        | Academy special converter   | 204              | £1,424,986 |
        But save as image buttons are hidden
        And copy image buttons are hidden

    Scenario: Table view for total expenditure for trust(s) with part-year data
        Given I am on compare your costs page for trust with company number '8104190'
        And table view is selected
        And the 'total expenditure' dimension is '£ per pupil'
        Then the following is shown for 'total expenditure'
          | School name                                                                           | Local Authority        | School type       | Number of pupils | Amount |
          | Test academy school 90\n!\nWarning\nThis school only has 10 months of data available. | Camden                 | Academy converter | 191              | £9,000 |
          | Test academy school 93                                                                | Hammersmith and Fulham | Academy converter | 399              | £8,584 |
          | Test academy school 92\n!\nWarning\nThis school only has 3 months of data available.  | Hackney                | Free school       | 216              | £7,781 |
          | Test academy school 87                                                                | City of London         | Academy converter | 335              | £7,754 |
          | Test academy school 94                                                                | Islington              | Academy converter | 339              | £7,318 |
          | Test academy school 91                                                                | Greenwich              | Academy converter | 230              | £7,281 |

    Scenario: Show all should expand all sections
        Given I am on compare your costs page for trust with company number '10074054'
        When I click on show all sections
        Then all sections on the page are expanded
        And the show all text changes to hide all sections

    Scenario: Change all charts to table view
        Given I am on compare your costs page for trust with company number '10074054'
        And all sections are shown
        When I click on view as table
        Then all sections on the page are expanded
        And are showing table view
        But save as image buttons are hidden
        And copy image buttons are hidden

    Scenario: Hide single section
        Given I am on compare your costs page for trust with company number '10074054'
        And all sections are shown
        When I click section link for 'non educational support staff'
        Then the section 'non educational support staff' is hidden

    Scenario: View additional details upon hover
        Given I am on compare your costs page for trust with company number '10074054'
        When I hover over a chart bar
        Then additional information is displayed
        And additional information contains
          | Item             | Value                     |
          | Local authority  | Trafford                  |
          | School type      | Academy special converter |
          | Number of pupils | 204                       |

    Scenario: View additional details upon hover for part-year trust
        Given I am on compare your costs page for trust with company number '8104190'
        And the 'total expenditure' dimension is '£ per pupil'
        When I hover over the nth chart bar 2
        Then additional information is displayed
        And additional information contains
          | Item             | Value       |
          | Local authority  | Hackney     |
          | School type      | Free school |
          | Number of pupils | 216         |
        And additional information shows part year warning for 3 months

    Scenario: Warning icon displayed in chart for part-year trust
        Given I am on compare your costs page for trust with company number '8104190'
        And the 'total expenditure' dimension is '£ per pupil'
        Then the nth chart bar 2 displays the establishment name 'Test academy school 92'
        And the nth chart bar 2 displays the warning icon

    Scenario: Clicking school name in chart directs to homepage
        Given I am on compare your costs page for trust with company number '10074054'
        When I select the school name on the chart
        Then I am navigated to selected school home page with Trust name 'Brunel Academies Trust'

    Scenario: Tabbing to and selecting school name in chart directs to homepage
        Given I am on compare your costs page for trust with company number '10074054'
        When I tab to the school name on the chart
        And I press the Enter key when focused on the school name
        Then I am navigated to selected school home page with Trust name 'Brunel Academies Trust'

    Scenario: Tabbing to school name in chart displays tooltip
        Given I am on compare your costs page for trust with company number '10074054'
        When I tab to the school name on the chart
        Then I can view the associated tooltip

    Scenario: View Catering staff and services using Gross figures
        Given I am on compare your costs page for trust with company number '10074054'
        And table view is selected
        And Section 'catering staff' is visible
        And the 'catering staff' dimension is 'actuals'
        Then the following is shown for 'catering staff'
          | School name             | Local Authority | School type                 | Number of pupils | Amount  |
          | Test academy school 392 | Trafford        | Academy special converter   | 204              | £44,110 |
          | Test academy school 375 | Reading         | Academy special sponsor led | 232              | £34,924 |

    Scenario: View Catering staff and services using Net figures
        Given I am on compare your costs page for trust with company number '10074054'
        And table view is selected
        And Section 'catering staff' is visible
        And the 'catering staff' dimension is 'actuals'
        When I click on display as Net
        Then the following is shown for 'catering staff'
          | School name             | Local Authority | School type                 | Number of pupils | Amount  |
          | Test academy school 392 | Trafford        | Academy special converter   | 204              | £53,473 |
          | Test academy school 375 | Reading         | Academy special sponsor led | 232              | £42,585 |
          
    Scenario: Charts have correct dimension options
        Given I am on compare your costs page for trust with company number '10074054'
        Then all sections on the page have the correct dimension options