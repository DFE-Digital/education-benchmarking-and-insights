Feature: School compare your costs

    Scenario: Download total expenditure chart
        Given I am on compare your costs page for school with URN '101241'
        When I click on save as image for 'total expenditure'
        Then the 'total expenditure' chart image is downloaded       

    Scenario: Change dimension of total expenditure and change view to table
        Given I am on compare your costs page for school with URN '101241'
        And the 'total expenditure' dimension is '£ per pupil'
        When I change 'total expenditure' dimension to 'actuals'
        Then the 'total expenditure' dimension is 'actuals'

    Scenario: Change dimension in table view for total expenditure
        Given I am on compare your costs page for school with URN '101241'
        And table view is selected
        And the 'total expenditure' dimension is '£ per pupil'
        When I change 'total expenditure' dimension to 'actuals'
        Then the following is shown for 'total expenditure'
          | School name     | Local Authority | School type            | Number of pupils | Amount      |
          | Test school 227 | 301             | Community school       | 2657             | 21041452.77 |
          | Test school 83  | 301             | Community school       | 2325             | 18254060.76 |
          | Test school 207 | 301             | Community school       | 2143             | 16899892.72 |
          | Test school 225 | 383             | Community school       | 2522             | 16310325.63 |
          | Test school 210 | 317             | Community school       | 2227             | 14156455.30 |
          | Test school 75  | 308             | Community school       | 1677             | 11808070.45 |
          | Test school 223 | 302             | Voluntary aided school | 1647             | 10853782.49 |
          | Test school 220 | 825             | Voluntary aided school | 2096.5           | 10438274.94 |
          | Test school 9   | 307             | Community school       | 378.5            | 3282931.02  |
          | Test school 5   | 891             | Community school       | 424.5            | 2714677.23  |
          | Test school 7   | 888             | Community school       | 435              | 2697218.49  |
          | Test school 1   | 921             | Community school       | 409              | 2233310.74  |
          | Test school 8   | 886             | Community school       | 392              | 2078214.12  |
          | Test school 3   | 925             | Community school       | 205              | 1384594.94  |
          | Test school 6   | 893             | Foundation school      | 205              | 1326720.90  |
          | Test school 10  | 921             | Community school       | 204              | 1039250.48  |
          | Test school 4   | 885             | Community school       | 83               | 668841.00   |


        But save as image buttons are hidden

    Scenario: Show all should expand all sections
        Given I am on compare your costs page for school with URN '101241'
        When I click on show all sections
        Then all sections on the page are expanded
        And the show all text changes to hide all sections
        
    Scenario: Change all charts to table view
        Given I am on compare your costs page for school with URN '101241'
        And all sections are shown
        When I click on view as table
        Then all sections on the page are expanded
        And are showing table view
        But save as image buttons are hidden

    Scenario: Hide single section
        Given I am on compare your costs page for school with URN '101241'
        And all sections are shown
        When I click section link for 'non educational support staff'
        Then the section 'non educational support staff' is hidden
        
    Scenario: View how we choose similar school details
        Given I am on compare your costs page for school with URN '101241'
        When I click on how we choose similar schools
        Then the details section is expanded
