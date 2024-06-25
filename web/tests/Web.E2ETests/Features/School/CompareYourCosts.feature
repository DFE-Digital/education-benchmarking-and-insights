Feature: School compare your costs

    Scenario: Download total expenditure chart
        Given I am on compare your costs page for school with URN '777042'
        When I click on save as image for 'total expenditure'
        Then the 'total expenditure' chart image is downloaded

    Scenario: Change dimension of total expenditure and change view to table
        Given I am on compare your costs page for school with URN '777042'
        And the 'total expenditure' dimension is '£ per pupil'
        When I change 'total expenditure' dimension to 'actuals'
        Then the 'total expenditure' dimension is 'actuals'

    Scenario: Change dimension in table view for total expenditure
        Given I am on compare your costs page for school with URN '777042'
        And table view is selected
        And the 'total expenditure' dimension is '£ per pupil'
        When I change 'total expenditure' dimension to 'actuals'
        Then the following is shown for 'total expenditure'
          | School name             | Local Authority         | School type                        | Number of pupils | Amount      |
          | Test academy school 353 | Bolton                  | Academy special converter          | 1326             | £10,478,892 |
          | Test school 48          | South Tyneside          | Community school                   | 883              | £8,937,065  |
          | Test academy school 444 | Lincolnshire            | Academy sponsor led                | 991              | £8,015,605  |
          | Test school 26          | Hounslow                | Community school                   | 769              | £6,220,620  |
          | Test academy school 411 | Telford and Wrekin      | Academy sponsor led                | 399              | £3,424,906  |
          | Test school 7           | Dorset                  | Local authority nursery school     | 317              | £3,076,398  |
          | Test academy school 111 | Reading                 | Free schools alternative provision | 317              | £3,076,398  |
          | Test academy school 414 | Cornwall                | Academy sponsor led                | 317              | £3,076,398  |
          | Test academy school 407 | Blackpool               | Academy converter                  | 367              | £2,826,867  |
          | Test academy school 191 | Bexley                  | Free school 16 to 19               | 367              | £2,826,867  |
          | Test academy school 147 | Cheshire West & Chester | Academy special converter          | 418              | £2,790,504  |
          | Test academy school 365 | North Lincolnshire      | Academy special sponsor led        | 407              | £2,740,736  |
          | Test academy school 456 | Westmorland and Furness | Academy 16-19 converter            | 339              | £2,480,660  |
          | Test academy school 133 | Shropshire              | Academy special converter          | 260              | £2,357,695  |
          | Test academy school 469 | Hillingdon              | Free school 16 to 19               | 235              | £2,110,442  |
          | Test academy school 246 | Sunderland              | Academy converter                  | 236              | £1,762,937  |
          | Test academy school 496 | Hackney                 | Academy 16 to 19 sponsor led       | 191              | £1,719,060  |
          | Test school 158         | Newcastle upon Tyne     | Community school                   | 206              | £1,694,231  |
          | Test academy school 498 | Islington               | Academy 16-19 converter            | 216              | £1,680,594  |
          | Test academy school 162 | West Northamptonshire   | Academy special converter          | 231              | £1,597,953  |
          | Test academy school 465 | Enfield                 | Academy 16-19 converter            | 231              | £1,597,953  |
          | Test school 102         | Hammersmith and Fulham  | Community school                   | 212              | £1,587,223  |
          | Test school 154         | Kirklees                | Community school                   | 174              | £1,545,387  |
          | Test school 243         | Lambeth                 | Voluntary aided school             | 174              | £1,545,387  |
          | Test school 149         | Barnsley                | Pupil referral unit                | 232              | £1,540,100  |
          | Test academy school 12  | Hounslow                | Academy sponsor led                | 232              | £1,540,100  |
          | Test academy school 466 | Haringey                | Academy 16-19 converter            | 232              | £1,540,100  |
          | Test school 263         | Redbridge               | Community school                   | 114              | £1,487,825  |
          | Test academy school 435 | Bradford                | Academy sponsor led                | 204              | £1,424,986  |
          | Test academy school 333 | Hampshire               | Free schools alternative provision | 190              | £1,179,475  |




        But save as image buttons are hidden

    Scenario: Show all should expand all sections
        Given I am on compare your costs page for school with URN '777042'
        When I click on show all sections
        Then all sections on the page are expanded
        And the show all text changes to hide all sections

    Scenario: Change all charts to table view
        Given I am on compare your costs page for school with URN '777042'
        And all sections are shown
        When I click on view as table
        Then all sections on the page are expanded
        And are showing table view
        But save as image buttons are hidden

    Scenario: Hide single section
        Given I am on compare your costs page for school with URN '777042'
        And all sections are shown
        When I click section link for 'non educational support staff'
        Then the section 'non educational support staff' is hidden

    Scenario: View additional details upon hover
        Given I am on compare your costs page for school with URN '777042'
        When I hover over a chart bar
        Then additional information is displayed

    Scenario: Clicking school name in chart directs to homepage
        Given I am on compare your costs page for school with URN '777042'
        When I select the school name on the chart
        Then I am navigated to selected school home page