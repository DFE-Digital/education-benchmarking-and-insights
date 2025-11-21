Feature: School benchmark pupil and workforce data

    Background:
        Given I am on the service home
        And I am not logged in

    Scenario: Download school workforce chart
        Given I am on census page for school with URN '777042'
        When I click on save as image for 'school workforce'
        Then the 'school workforce' chart image is downloaded

    Scenario: Change dimension of school workforce
        Given I am on census page for school with URN '777042'
        When I change 'school workforce' dimension to 'pupils per staff role'
        Then the 'school workforce' dimension is 'pupils per staff role'

    Scenario Outline: Change dimension in table view for total number of teachers
        Given I am on census page for school with URN '777042'
        And table view is selected
        When I change 'total number of teachers' dimension to '<Dimension>'
        Then the following headers are displayed for 'total number of teachers'
          | School name | Local Authority | School type | Number of pupils | <Headers> |

        Examples:
          | Dimension               | Headers               |
          | total                   | Count                 |
          | headcount per FTE       | Ratio                 |
          | percentage of workforce | Percentage            |
          | pupils per staff role   | Pupils per staff role |

    Scenario: Change chart view to table view
        Given I am on census page for school with URN '777042'
        When I click on view as table
        Then the table view is showing
        But save as image buttons are hidden
        And copy image buttons are hidden

    Scenario: Change table view to chart view
        Given I am on census page for school with URN '777042'
        And table view is selected
        When I click on view as chart
        Then chart view is showing
        And save as image buttons are displayed

    Scenario Outline: Checking the charts dimension dropdown items
        Given I am on census page for school with URN '777042'
        When I click the dimension for '<Chart>'
        Then the '<Chart>' dimension is 'pupils per staff role'
        And the dimension has '<Options>' for '<Chart>'

        Examples:
          | Chart                        | Options                                                                  |
          | school workforce             | total, headcount per FTE, pupils per staff role                          |
          | total number of teachers     | total, headcount per FTE, percentage of workforce, pupils per staff role |
          | senior leadership            | total, headcount per FTE, percentage of workforce, pupils per staff role |
          | teaching assistant           | total, headcount per FTE, percentage of workforce, pupils per staff role |
          | non class room support staff | total, headcount per FTE, percentage of workforce, pupils per staff role |
          | auxiliary staff              | total, headcount per FTE, percentage of workforce, pupils per staff role |
          | school workforce headcount   | total, pupils per staff role                                             |

    Scenario: View additional details upon hover
        Given I am on census page for school with URN '777042'
        When I hover over a chart bar
        Then additional information is displayed

    Scenario: Clicking school name in chart directs to homepage
        Given I am on census page for school with URN '777042'
        When I select the school name on the chart
        Then I am navigated to selected school home page

    Scenario: Benchmarking for school(s) with missing census data does not display comparators
        Given I am on census page for part year school with URN '990754'
        Then the benchmarking charts are not displayed

    Scenario: Clicking download button downloads .zip file
        Given I am on census page for school with URN '777042'
        When I click on download data
        Then the file 'census-777042.zip' is downloaded
        
        Scenario: Table view should not display progress bandings when checkbox not selected
        Given I am on census page for school with URN '990002'
        And table view is selected
        Then the following is shown for 'School workforce (Full Time Equivalent)'
          | School name             | Local Authority                  | School type                    | Number of pupils | Pupils per staff role | 
          | Test school 176         | Test Local Authority             | Voluntary aided school         | 337              | 11.62                 | 
          | Test academy school 144 | Dudley                           | Free school special            | 403              | 11.19                 |                    
          | Test academy school 421 | Southwark                        | Free school                    | 221              | 11.05                 |                    
          | Test academy school 208 | Wigan                            | Academy sponsor led            | 1106             | 10.63                 |                    
          | Test school 154         | Test Local Authority             | Community school               | 436              | 10.63                 |                    
          | Test academy school 470 | Waltham Forest                   | Academy 16-19 converter        | 453              | 10.53                 |                    
          | Test academy school 364 | North East Lincolnshire          | Academy special converter      | 457              | 10.16                 |                    
          | Test school 22          | Cheshire East                    | Voluntary aided school         | 1101             | 9.83                  |                    
          | Test academy school 412 | Cheshire East                    | Academy converter              | 1101             | 9.83                  |                    
          | Test academy school 146 | Cheshire East                    | Academy special converter      | 1101             | 9.83                  |                    
          | Test school 215         | Blackpool                        | Voluntary aided school         | 207              | 9.41                  |                    
          | Test academy school 231 | West Berkshire                   | Academy sponsor led            | 1548             | 9.27                  |                    
          | Test school 208         | Southend-on-Sea                  | Community school               | 210              | 9.13                  |                    
          | Test school 138         | Sefton                           | Voluntary aided school         | 832              | 8.85                  |                    
          | Test academy school 4   | Hillingdon                       | Academy converter              | 176              | 8.38                  |                    
          | Test academy school 228 | Swindon                          | Academy sponsor led            | 422              | 8.12                  |                    
          | Test school 76          | Portsmouth                       | Community school               | 323              | 8.08                  |                    
          | Test academy school 274 | Lambeth                          | Academy converter              | 394              | 8.04                  |                    
          | Test school 118         | Haringey                         | Voluntary aided school         | 203              | 7.81                  |                    
          | Test school 26          | Hounslow                         | Community school               | 321              | 7.47                  |                    
          | Test academy school 469 | Hillingdon                       | Free school 16 to 19           | 369              | 7.38                  |                    
          | Test academy school 200 | Hillingdon                       | Academy sponsor led            | 369              | 7.38                  |                    
          | Test school 70          | Bournemouth Christchurch & Poole | Local authority nursery school | 115              | 7.19                  |                    
          | Test academy school 56  | Trafford                         | Academy sponsor led            | 120              | 7.06                  |                    
          | Test school 97          | Southend-on-Sea                  | Voluntary aided school         | 175              | 6.73                  |                    
          | Test school 228         | Oxfordshire                      | Community school               | 195              | 5.74                  |                    
          | Test school 19          | Leicestershire                   | Local authority nursery school | 121              | 5.26                  |                    
          | Test academy school 176 | Wandsworth                       | Academy sponsor led            | 113              | 3.42                  |                    
          | Test academy school 88  | Portsmouth                       | Academy converter              | 40               | 1.6                   |                    
          | Test school 140         | Bury                             | Foundation special school      | 104              | 0.47                  |                    


    Scenario: Table view should display correct progress bandings when checkbox selected
        Given I am on census page for school with URN '990002'
        And table view is selected
        When I click 'Well above average' school performance
        Then the following is shown for 'School workforce (Full Time Equivalent)'
          | School name             | Local Authority                  | School type                    | Number of pupils | Pupils per staff role | Progress 8 banding |
          | Test school 176         | Test Local Authority             | Voluntary aided school         | 337              | 11.62                 | Well above average |
          | Test academy school 144 | Dudley                           | Free school special            | 403              | 11.19                 |                    |
          | Test academy school 421 | Southwark                        | Free school                    | 221              | 11.05                 |                    |
          | Test academy school 208 | Wigan                            | Academy sponsor led            | 1106             | 10.63                 |                    |
          | Test school 154         | Test Local Authority             | Community school               | 436              | 10.63                 |                    |
          | Test academy school 470 | Waltham Forest                   | Academy 16-19 converter        | 453              | 10.53                 |                    |
          | Test academy school 364 | North East Lincolnshire          | Academy special converter      | 457              | 10.16                 |                    |
          | Test school 22          | Cheshire East                    | Voluntary aided school         | 1101             | 9.83                  |                    |
          | Test academy school 412 | Cheshire East                    | Academy converter              | 1101             | 9.83                  |                    |
          | Test academy school 146 | Cheshire East                    | Academy special converter      | 1101             | 9.83                  |                    |
          | Test school 215         | Blackpool                        | Voluntary aided school         | 207              | 9.41                  |                    |
          | Test academy school 231 | West Berkshire                   | Academy sponsor led            | 1548             | 9.27                  |                    |
          | Test school 208         | Southend-on-Sea                  | Community school               | 210              | 9.13                  |                    |
          | Test school 138         | Sefton                           | Voluntary aided school         | 832              | 8.85                  |                    |
          | Test academy school 4   | Hillingdon                       | Academy converter              | 176              | 8.38                  |                    |
          | Test academy school 228 | Swindon                          | Academy sponsor led            | 422              | 8.12                  |                    |
          | Test school 76          | Portsmouth                       | Community school               | 323              | 8.08                  |                    |
          | Test academy school 274 | Lambeth                          | Academy converter              | 394              | 8.04                  |                    |
          | Test school 118         | Haringey                         | Voluntary aided school         | 203              | 7.81                  |                    |
          | Test school 26          | Hounslow                         | Community school               | 321              | 7.47                  |                    |
          | Test academy school 469 | Hillingdon                       | Free school 16 to 19           | 369              | 7.38                  |                    |
          | Test academy school 200 | Hillingdon                       | Academy sponsor led            | 369              | 7.38                  |                    |
          | Test school 70          | Bournemouth Christchurch & Poole | Local authority nursery school | 115              | 7.19                  |                    |
          | Test academy school 56  | Trafford                         | Academy sponsor led            | 120              | 7.06                  |                    |
          | Test school 97          | Southend-on-Sea                  | Voluntary aided school         | 175              | 6.73                  |                    |
          | Test school 228         | Oxfordshire                      | Community school               | 195              | 5.74                  |                    |
          | Test school 19          | Leicestershire                   | Local authority nursery school | 121              | 5.26                  |                    |
          | Test academy school 176 | Wandsworth                       | Academy sponsor led            | 113              | 3.42                  |                    |
          | Test academy school 88  | Portsmouth                       | Academy converter              | 40               | 1.6                   |                    |
          | Test school 140         | Bury                             | Foundation special school      | 104              | 0.47                  |                    |

