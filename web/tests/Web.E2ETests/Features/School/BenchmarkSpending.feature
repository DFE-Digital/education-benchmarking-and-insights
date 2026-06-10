@SchoolComparisonFilterEnabled
Feature: School benchmark spending

    This feature tests the new Benchmark Spending view, which is enabled when the SchoolComparisonFilter feature flag is enabled.
    Scenarios are high-level E2E tests for the new filtered view with table and chart modes.

    Scenario: Clicking download chart images button downloads .zip file
        Given I am on benchmark spending page for school with URN '777042'
        Then the save chart images button is visible
        When I click the save chart images button
        Then the save chart images modal is visible
        And the 'comparison-777042.zip' file is downloaded

    Scenario: Table view for total expenditure for school(s) with part-year data
        Given I am on benchmark spending page for part year school with URN '777045'
        And table view is selected for benchmark spending
        Then the following benchmark spending table is shown for 'Total expenditure'
          | School name                                                                              | Local Authority                  | School type                    | Number of pupils | Amount  |
          | Test academy school 273                                                                  | Kensington and Chelsea           | Academy converter              | 114              | £13,051 |
          | Test school 88                                                                           | Slough                           | Community school               | 134              | £9,916  |
          | Test part year with pupil and without building comparator\n\nOnly has 3 months of data   | Bromley                          | Pupil referral unit            | 260              | £9,068  |
          | Test school 197                                                                          | Windsor and Maidenhead           | Community school               | 167              | £9,050  |
          | Test academy school 470                                                                  | Waltham Forest                   | Academy 16-19 converter        | 167              | £9,050  |
          | Test academy school 77                                                                   | Hartlepool                       | Academy converter              | 1,040            | £9,023  |
          | Test academy school 469                                                                  | Hillingdon                       | Free school 16 to 19           | 235              | £8,981  |
          | Test school 198                                                                          | West Berkshire                   | Community school               | 174              | £8,882  |
          | Test academy school 255                                                                  | Stockton-on-Tees                 | Academy converter              | 174              | £8,882  |
          | Test school 181                                                                          | Dorset                           | Voluntary aided school         | 399              | £8,584  |
          | Test school 87                                                                           | Reading                          | Foundation school              | 206              | £8,224  |
          | Test school 124                                                                          | Newham                           | Voluntary aided school         | 769              | £8,089  |
          | Test academy school 82                                                                   | Bournemouth Christchurch & Poole | Academy converter              | 991              | £8,088  |
          | Test academy school 53                                                                   | Salford                          | Free school                    | 407              | £8,028  |
          | Test school 260                                                                          | Kingston upon Thames             | Community school               | 853              | £7,880  |
          | Test academy school 450                                                                  | Surrey                           | Academy 16-19 converter        | 367              | £7,703  |
          | Test Part year school with pupil and builiding comparators\n\nOnly has 10 months of data | Bracknell Forest                 | Foundation school              | 214              | £7,470  |
          | Test school 205                                                                          | Plymouth                         | Community school               | 196              | £7,385  |
          | Test academy school 20                                                                   | Waltham Forest                   | Academy converter              | 449              | £7,356  |
          | Test academy school 98                                                                   | Southwark                        | Academy converter              | 449              | £7,356  |
          | Test academy school 244                                                                  | Islington                        | Academy converter              | 446              | £7,342  |
          | Test school 68                                                                           | Derbyshire                       | Local authority nursery school | 339              | £7,318  |
          | Test academy school 37                                                                   | North Lincolnshire               | Academy converter              | 339              | £7,318  |
          | Test school 270                                                                          | Solihull                         | Voluntary aided school         | 230              | £7,281  |
          | Test school 237                                                                          | City of London                   | Voluntary aided school         | 231              | £6,918  |
          | Test academy school 465                                                                  | Enfield                          | Academy 16-19 converter        | 231              | £6,918  |
          | Test academy school 375                                                                  | Reading                          | Academy special sponsor led    | 232              | £6,814  |
          | Test school 132                                                                          | Solihull                         | Community school               | 418              | £6,676  |
          | Test academy school 32                                                                   | Stockton-on-Tees                 | Academy converter              | 418              | £6,676  |
          | Test academy school 160                                                                  | West Sussex                      | Academy special converter      | 190              | £6,208  |

    Scenario: Benchmarking for school with missing comparators does not display comparators
        Given I am on benchmark spending page for missing comparator school with URN '990754'
        Then the benchmark spending comparison charts and tables are not displayed

    Scenario: View additional details upon hover
        Given I am on benchmark spending page for school with URN '777042'
        When I hover over a benchmark spending chart bar
        Then additional benchmark spending information is displayed
        And additional benchmark spending information contains
          | Item             | Value            |
          | Local authority  | Redbridge        |
          | School type      | Community school |
          | Number of pupils | 114              |

    Scenario: Clicking school name in chart directs to homepage
        Given I am on benchmark spending page for school with URN '777042'
        When I select the school name on the benchmark spending chart
        Then I am navigated to selected school home page

    Scenario: Tabbing to and selecting school name in chart directs to homepage
        Given I am on benchmark spending page for school with URN '777042'
        When I tab to the school name on the benchmark spending chart
        And I press the Enter key when focused on the benchmark spending school name
        Then I am navigated to selected school home page

    Scenario: Tabbing to school name in chart displays tooltip
        Given I am on benchmark spending page for school with URN '777042'
        When I tab to the school name on the benchmark spending chart
        Then I can view the benchmark spending tooltip

    Scenario Outline: View comparators for part year school
        Given I am on benchmark spending page for part year school with URN '<URN>'
        When I click on benchmark spending comparators link
        Then I am taken to comparators page
        And pupil cost comparators are <PupilComparators>
        And building cost comparators are <BuildingComparators>

        Examples:
          | URN    | PupilComparators | BuildingComparators |
          | 777043 | not null         | not null            |
          | 777045 | not null         | null                |

    Scenario: Table view should not display progress bandings when checkbox not selected
        Given I am on benchmark spending page for school with URN '990002'
        And table view is selected for benchmark spending
        Then the following benchmark spending table is shown for 'Total expenditure'
          | School name             | Local Authority                  | School type                    | Number of pupils | Amount   |
          | Test school 22          | Cheshire East                    | Voluntary aided school         | 17               | £112,411 |
          | Test academy school 231 | West Berkshire                   | Academy sponsor led            | 17               | £112,411 |
          | Test academy school 274 | Lambeth                          | Academy converter              | 37               | £50,829  |
          | Test academy school 144 | Dudley                           | Free school special            | 37               | £50,829  |
          | Test school 208         | Southend-on-Sea                  | Community school               | 883              | £10,121  |
          | Test school 140         | Bury                             | Foundation special school      | 317              | £9,705   |
          | Test school 118         | Haringey                         | Voluntary aided school         | 260              | £9,068   |
          | Test academy school 470 | Waltham Forest                   | Academy 16-19 converter        | 167              | £9,050   |
          | Test academy school 469 | Hillingdon                       | Free school 16 to 19           | 235              | £8,981   |
          | Test school 154         | Test Local Authority             | Community school               | 174              | £8,882   |
          | Test school 138         | Sefton                           | Voluntary aided school         | 399              | £8,584   |
          | Test school 26          | Hounslow                         | Community school               | 769              | £8,089   |
          | Test academy school 4   | Hillingdon                       | Academy converter              | 769              | £8,089   |
          | Test academy school 228 | Swindon                          | Academy sponsor led            | 991              | £8,088   |
          | Test school 70          | Bournemouth Christchurch & Poole | Local authority nursery school | 407              | £8,028   |
          | Test school 215         | Blackpool                        | Voluntary aided school         | 853              | £7,880   |
          | Test academy school 146 | Cheshire East                    | Academy special converter      | 335              | £7,754   |
          | Test academy school 364 | North East Lincolnshire          | Academy special converter      | 367              | £7,703   |
          | Test school 76          | Portsmouth                       | Community school               | 212              | £7,487   |
          | Test academy school 200 | Hillingdon                       | Academy sponsor led            | 446              | £7,342   |
          | Test school 228         | Oxfordshire                      | Community school               | 339              | £7,318   |
          | Test academy school 412 | Cheshire East                    | Academy converter              | 339              | £7,318   |
          | Test school 97          | Southend-on-Sea                  | Voluntary aided school         | 1,045             | £7,302   |
          | Test academy school 208 | Wigan                            | Academy sponsor led            | 210              | £6,990   |
          | Test academy school 176 | Wandsworth                       | Academy sponsor led            | 204              | £6,985   |
          | Test academy school 421 | Southwark                        | Free school                    | 231              | £6,918   |
          | Test academy school 56  | Trafford                         | Academy sponsor led            | 232              | £6,814   |
          | Test school 176         | Test Local Authority             | Voluntary aided school         | 418              | £6,676   |
          | Test academy school 88  | Portsmouth                       | Academy converter              | 418              | £6,676   |
          | Test school 19          | Leicestershire                   | Local authority nursery school | 190              | £6,208   |

    Scenario: Table view should display correct progress bandings when checkbox selected
        Given I am on benchmark spending page for school with URN '990002'
        And table view is selected for benchmark spending
        When I click on benchmark spending 'Well above average' school performance
        Then the following benchmark spending table is shown for 'Total expenditure'
          | School name             | Local Authority                  | School type                    | Number of pupils | Amount   | Progress 8 banding |
          | Test school 22          | Cheshire East                    | Voluntary aided school         | 17               | £112,411 |                    |
          | Test academy school 231 | West Berkshire                   | Academy sponsor led            | 17               | £112,411 |                    |
          | Test academy school 274 | Lambeth                          | Academy converter              | 37               | £50,829  |                    |
          | Test academy school 144 | Dudley                           | Free school special            | 37               | £50,829  |                    |
          | Test school 208         | Southend-on-Sea                  | Community school               | 883              | £10,121  |                    |
          | Test school 140         | Bury                             | Foundation special school      | 317              | £9,705   |                    |
          | Test school 118         | Haringey                         | Voluntary aided school         | 260              | £9,068   |                    |
          | Test academy school 470 | Waltham Forest                   | Academy 16-19 converter        | 167              | £9,050   |                    |
          | Test academy school 469 | Hillingdon                       | Free school 16 to 19           | 235              | £8,981   |                    |
          | Test school 154         | Test Local Authority             | Community school               | 174              | £8,882   |                    |
          | Test school 138         | Sefton                           | Voluntary aided school         | 399              | £8,584   |                    |
          | Test school 26          | Hounslow                         | Community school               | 769              | £8,089   |                    |
          | Test academy school 4   | Hillingdon                       | Academy converter              | 769              | £8,089   |                    |
          | Test academy school 228 | Swindon                          | Academy sponsor led            | 991              | £8,088   |                    |
          | Test school 70          | Bournemouth Christchurch & Poole | Local authority nursery school | 407              | £8,028   |                    |
          | Test school 215         | Blackpool                        | Voluntary aided school         | 853              | £7,880   |                    |
          | Test academy school 146 | Cheshire East                    | Academy special converter      | 335              | £7,754   |                    |
          | Test academy school 364 | North East Lincolnshire          | Academy special converter      | 367              | £7,703   |                    |
          | Test school 76          | Portsmouth                       | Community school               | 212              | £7,487   |                    |
          | Test academy school 200 | Hillingdon                       | Academy sponsor led            | 446              | £7,342   |                    |
          | Test school 228         | Oxfordshire                      | Community school               | 339              | £7,318   |                    |
          | Test academy school 412 | Cheshire East                    | Academy converter              | 339              | £7,318   |                    |
          | Test school 97          | Southend-on-Sea                  | Voluntary aided school         | 1,045             | £7,302   |                    |
          | Test academy school 208 | Wigan                            | Academy sponsor led            | 210              | £6,990   |                    |
          | Test academy school 176 | Wandsworth                       | Academy sponsor led            | 204              | £6,985   |                    |
          | Test academy school 421 | Southwark                        | Free school                    | 231              | £6,918   |                    |
          | Test academy school 56  | Trafford                         | Academy sponsor led            | 232              | £6,814   |                    |
          | Test school 176         | Test Local Authority             | Voluntary aided school         | 418              | £6,676   | Well above average |
          | Test academy school 88  | Portsmouth                       | Academy converter              | 418              | £6,676   |                    |
          | Test school 19          | Leicestershire                   | Local authority nursery school | 190              | £6,208   |                    |
