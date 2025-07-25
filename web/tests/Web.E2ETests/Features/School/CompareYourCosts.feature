Feature: School compare your costs

    Scenario: Download total expenditure chart
        Given I am on compare your costs page for school with URN '777042'
        When I click on save as image for 'total expenditure'
        Then the 'total expenditure' chart image is downloaded

    Scenario: Copy total expenditure chart
        Given I am on compare your costs page for school with URN '777042'
        When I click on copy image for 'total expenditure'
        Then the 'total expenditure' chart image is copied

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
          | Test school 154         | Test Local Authority    | Community school                   | 174              | £1,545,387  |
          | Test school 243         | Lambeth                 | Voluntary aided school             | 174              | £1,545,387  |
          | Test school 149         | Barnsley                | Pupil referral unit                | 232              | £1,540,100  |
          | Test academy school 12  | Hounslow                | Academy sponsor led                | 232              | £1,540,100  |
          | Test academy school 466 | Haringey                | Academy 16-19 converter            | 232              | £1,540,100  |
          | Test school 263         | Redbridge               | Community school                   | 114              | £1,487,825  |
          | Test academy school 435 | Bradford                | Academy sponsor led                | 204              | £1,424,986  |
          | Test academy school 333 | Hampshire               | Free schools alternative provision | 190              | £1,179,475  |
        But save as image buttons are hidden
        And copy image buttons are hidden
        And the save chart images button is visible

    Scenario: Table view for total expenditure for school(s) with part-year data
        Given I am on compare your costs page for school with URN '777045'
        And table view is selected
        And the 'total expenditure' dimension is '£ per pupil'
        Then the following is shown for 'total expenditure'
          | School name                                                                                                               | Local Authority                  | School type                    | Number of pupils | Amount  |
          | Test academy school 273                                                                                                   | Kensington and Chelsea           | Academy converter              | 114              | £13,051 |
          | Test school 88                                                                                                            | Slough                           | Community school               | 134              | £9,916  |
          | Test part year with pupil and without building comparator\n!\nWarning\nThis school only has 3 months of data available.   | Bromley                          | Pupil referral unit            | 260              | £9,068  |
          | Test school 197                                                                                                           | Windsor and Maidenhead           | Community school               | 167              | £9,050  |
          | Test academy school 470                                                                                                   | Waltham Forest                   | Academy 16-19 converter        | 167              | £9,050  |
          | Test academy school 77                                                                                                    | Hartlepool                       | Academy converter              | 1040             | £9,023  |
          | Test academy school 469                                                                                                   | Hillingdon                       | Free school 16 to 19           | 235              | £8,981  |
          | Test school 198                                                                                                           | West Berkshire                   | Community school               | 174              | £8,882  |
          | Test academy school 255                                                                                                   | Stockton-on-Tees                 | Academy converter              | 174              | £8,882  |
          | Test school 181                                                                                                           | Dorset                           | Voluntary aided school         | 399              | £8,584  |
          | Test school 87                                                                                                            | Reading                          | Foundation school              | 206              | £8,224  |
          | Test school 124                                                                                                           | Newham                           | Voluntary aided school         | 769              | £8,089  |
          | Test academy school 82                                                                                                    | Bournemouth Christchurch & Poole | Academy converter              | 991              | £8,088  |
          | Test academy school 53                                                                                                    | Salford                          | Free school                    | 407              | £8,028  |
          | Test school 260                                                                                                           | Kingston upon Thames             | Community school               | 853              | £7,880  |
          | Test academy school 450                                                                                                   | Surrey                           | Academy 16-19 converter        | 367              | £7,703  |
          | Test Part year school with pupil and builiding comparators\n!\nWarning\nThis school only has 10 months of data available. | Bracknell Forest                 | Foundation school              | 214              | £7,470  |
          | Test school 205                                                                                                           | Plymouth                         | Community school               | 196              | £7,385  |
          | Test academy school 20                                                                                                    | Waltham Forest                   | Academy converter              | 449              | £7,356  |
          | Test academy school 98                                                                                                    | Southwark                        | Academy converter              | 449              | £7,356  |
          | Test academy school 244                                                                                                   | Islington                        | Academy converter              | 446              | £7,342  |
          | Test school 68                                                                                                            | Derbyshire                       | Local authority nursery school | 339              | £7,318  |
          | Test academy school 37                                                                                                    | North Lincolnshire               | Academy converter              | 339              | £7,318  |
          | Test school 270                                                                                                           | Solihull                         | Voluntary aided school         | 230              | £7,281  |
          | Test school 237                                                                                                           | City of London                   | Voluntary aided school         | 231              | £6,918  |
          | Test academy school 465                                                                                                   | Enfield                          | Academy 16-19 converter        | 231              | £6,918  |
          | Test academy school 375                                                                                                   | Reading                          | Academy special sponsor led    | 232              | £6,814  |
          | Test school 132                                                                                                           | Solihull                         | Community school               | 418              | £6,676  |
          | Test academy school 32                                                                                                    | Stockton-on-Tees                 | Academy converter              | 418              | £6,676  |
          | Test academy school 160                                                                                                   | West Sussex                      | Academy special converter      | 190              | £6,208  |

    Scenario: Benchmarking for school with missing comparators does not display comparators
        Given I am on compare your costs page for missing comparator school with URN '990754'
        Then the benchmarking charts are not displayed

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
        And copy image buttons are hidden
        And the save chart images button is visible

    Scenario: Hide single section
        Given I am on compare your costs page for school with URN '777042'
        And all sections are shown
        When I click section link for 'non educational support staff'
        Then the section 'non educational support staff' is hidden

    Scenario: View additional details upon hover
        Given I am on compare your costs page for school with URN '777042'
        When I hover over a chart bar
        Then additional information is displayed
        And additional information contains
          | Item             | Value            |
          | Local authority  | Redbridge        |
          | School type      | Community school |
          | Number of pupils | 114              |

    Scenario: View additional details upon hover for part-year school
        Given I am on compare your costs page for part year school with URN '777045'
        When I hover over the nth chart bar 2
        Then additional information is displayed
        And additional information contains
          | Item             | Value               |
          | Local authority  | Bromley             |
          | School type      | Pupil referral unit |
          | Number of pupils | 260                 |
        And additional information shows part year warning for 3 months

    Scenario: Warning icon displayed in chart for part-year school
        Given I am on compare your costs page for part year school with URN '777045'
        And the 'total expenditure' dimension is '£ per pupil'
        Then the nth chart bar 2 displays the establishment name 'Test part year with pupil and without building comparator'
        And the nth chart bar 2 displays the warning icon

    Scenario: Clicking school name in chart directs to homepage
        Given I am on compare your costs page for school with URN '777042'
        When I select the school name on the chart
        Then I am navigated to selected school home page

    Scenario: Tabbing to and selecting school name in chart directs to homepage
        Given I am on compare your costs page for school with URN '777042'
        When I tab to the school name on the chart
        And I press the Enter key when focused on the school name
        Then I am navigated to selected school home page

    Scenario: Tabbing to school name in chart displays tooltip
        Given I am on compare your costs page for school with URN '777042'
        When I tab to the school name on the chart
        Then I can view the associated tooltip

    Scenario: View Catering staff and services using Gross figures
        Given I am on compare your costs page for school with URN '777042'
        And table view is selected
        And Section 'catering staff' is visible
        And the 'catering staff' dimension is 'actuals'
        Then the following is shown for 'catering staff'
          | School name             | Local Authority         | School type                        | Number of pupils | Amount   |
          | Test academy school 353 | Bolton                  | Academy special converter          | 1326             | £274,213 |
          | Test academy school 444 | Lincolnshire            | Academy sponsor led                | 991              | £162,523 |
          | Test academy school 365 | North Lincolnshire      | Academy special sponsor led        | 407              | £130,448 |
          | Test academy school 411 | Telford and Wrekin      | Academy sponsor led                | 399              | £95,978  |
          | Test school 263         | Redbridge               | Community school                   | 114              | £89,788  |
          | Test academy school 147 | Cheshire West & Chester | Academy special converter          | 418              | £84,186  |
          | Test school 7           | Dorset                  | Local authority nursery school     | 317              | £78,876  |
          | Test academy school 111 | Reading                 | Free schools alternative provision | 317              | £78,876  |
          | Test academy school 414 | Cornwall                | Academy sponsor led                | 317              | £78,876  |
          | Test school 26          | Hounslow                | Community school                   | 769              | £73,448  |
          | Test academy school 407 | Blackpool               | Academy converter                  | 367              | £67,969  |
          | Test academy school 191 | Bexley                  | Free school 16 to 19               | 367              | £67,969  |
          | Test academy school 246 | Sunderland              | Academy converter                  | 236              | £61,038  |
          | Test academy school 133 | Shropshire              | Academy special converter          | 260              | £59,263  |
          | Test school 48          | South Tyneside          | Community school                   | 883              | £56,633  |
          | Test academy school 456 | Westmorland and Furness | Academy 16-19 converter            | 339              | £54,988  |
          | Test academy school 498 | Islington               | Academy 16-19 converter            | 216              | £52,373  |
          | Test school 102         | Hammersmith and Fulham  | Community school                   | 212              | £51,070  |
          | Test academy school 162 | West Northamptonshire   | Academy special converter          | 231              | £50,014  |
          | Test academy school 465 | Enfield                 | Academy 16-19 converter            | 231              | £50,014  |
          | Test academy school 496 | Hackney                 | Academy 16 to 19 sponsor led       | 191              | £48,949  |
          | Test academy school 469 | Hillingdon              | Free school 16 to 19               | 235              | £46,965  |
          | Test school 158         | Newcastle upon Tyne     | Community school                   | 206              | £46,029  |
          | Test academy school 435 | Bradford                | Academy sponsor led                | 204              | £44,110  |
          | Test academy school 333 | Hampshire               | Free schools alternative provision | 190              | £42,269  |
          | Test school 149         | Barnsley                | Pupil referral unit                | 232              | £41,903  |
          | Test academy school 12  | Hounslow                | Academy sponsor led                | 232              | £41,903  |
          | Test academy school 466 | Haringey                | Academy 16-19 converter            | 232              | £41,903  |
          | Test school 154         | Test Local Authority    | Community school                   | 174              | £41,338  |
          | Test school 243         | Lambeth                 | Voluntary aided school             | 174              | £41,338  |

    Scenario: View Catering staff and services using Net figures
        Given I am on compare your costs page for school with URN '777042'
        And table view is selected
        And Section 'catering staff' is visible
        And the 'catering staff' dimension is 'actuals'
        When I click on display as Net
        Then the following is shown for 'catering staff'
          | School name             | Local Authority         | School type                        | Number of pupils | Amount   |
          | Test academy school 353 | Bolton                  | Academy special converter          | 1326             | £358,159 |
          | Test academy school 444 | Lincolnshire            | Academy sponsor led                | 991              | £162,523 |
          | Test academy school 365 | North Lincolnshire      | Academy special sponsor led        | 407              | £159,854 |
          | Test academy school 411 | Telford and Wrekin      | Academy sponsor led                | 399              | £109,578 |
          | Test academy school 162 | West Northamptonshire   | Academy special converter          | 231              | £99,307  |
          | Test academy school 465 | Enfield                 | Academy 16-19 converter            | 231              | £99,307  |
          | Test school 263         | Redbridge               | Community school                   | 114              | £95,795  |
          | Test academy school 147 | Cheshire West & Chester | Academy special converter          | 418              | £91,847  |
          | Test school 7           | Dorset                  | Local authority nursery school     | 317              | £89,783  |
          | Test academy school 111 | Reading                 | Free schools alternative provision | 317              | £89,783  |
          | Test academy school 414 | Cornwall                | Academy sponsor led                | 317              | £89,783  |
          | Test academy school 246 | Sunderland              | Academy converter                  | 236              | £77,919  |
          | Test academy school 407 | Blackpool               | Academy converter                  | 367              | £74,137  |
          | Test academy school 191 | Bexley                  | Free school 16 to 19               | 367              | £74,137  |
          | Test school 26          | Hounslow                | Community school                   | 769              | £73,448  |
          | Test academy school 133 | Shropshire              | Academy special converter          | 260              | £70,053  |
          | Test academy school 456 | Westmorland and Furness | Academy 16-19 converter            | 339              | £69,488  |
          | Test academy school 498 | Islington               | Academy 16-19 converter            | 216              | £63,191  |
          | Test school 102         | Hammersmith and Fulham  | Community school                   | 212              | £59,232  |
          | Test academy school 469 | Hillingdon              | Free school 16 to 19               | 235              | £58,290  |
          | Test academy school 496 | Hackney                 | Academy 16 to 19 sponsor led       | 191              | £57,188  |
          | Test school 48          | South Tyneside          | Community school                   | 883              | £56,633  |
          | Test academy school 333 | Hampshire               | Free schools alternative provision | 190              | £55,255  |
          | Test academy school 435 | Bradford                | Academy sponsor led                | 204              | £53,473  |
          | Test school 158         | Newcastle upon Tyne     | Community school                   | 206              | £50,814  |
          | Test school 154         | Test Local Authority    | Community school                   | 174              | £47,932  |
          | Test school 243         | Lambeth                 | Voluntary aided school             | 174              | £47,932  |
          | Test school 149         | Barnsley                | Pupil referral unit                | 232              | £41,903  |
          | Test academy school 12  | Hounslow                | Academy sponsor led                | 232              | £41,903  |
          | Test academy school 466 | Haringey                | Academy 16-19 converter            | 232              | £41,903  |

    Scenario Outline: View comparators for part year school
        Given I am on compare your costs page for part year school with URN '<URN>'
        When I click on sets of similar school link
        Then I am taken to comparators page
        And pupil cost comparators are <PupilComparators>
        And building cost comparators are <BuildingComparators>

        Examples:
          | URN    | PupilComparators | BuildingComparators |
          | 777043 | not null         | not null            |
          | 777045 | not null         | null                |

    Scenario: schools having 0 for a cost category in comparators set shouldn't display in the chart
        Given I am on compare your costs page for school with URN '777042'
        And table view is selected
        And Section 'Teaching and teaching support staff' is visible
        Then the following is shown in 'Teaching and teaching support staff' sub category 'Supply teaching staff costs'
          | School name             | Local Authority         | School type             | Number of pupils | Amount |
          | Test academy school 456 | Westmorland and Furness | Academy 16-19 converter | 339              | £30.36 |
          | Test school 102         | Hammersmith and Fulham  | Community school        | 212              | £0.00  |
        And the message stating reason for less schools is visible in 'Supply teaching staff costs' section

    Scenario: Charts have correct dimension options
        Given I am on compare your costs page for school with URN '777042'
        Then all sections on the page have the correct dimension options:
          | Chart name                     | Options                                                               |
          | TotalExpenditure               | £ per pupil, actuals, percentage of income                            |
          | TeachingAndTeachingSupplyStaff | £ per pupil, actuals, percentage of expenditure, percentage of income |
          | NonEducationalSupportStaff     | £ per pupil, actuals, percentage of expenditure, percentage of income |
          | EducationalSupplies            | £ per pupil, actuals, percentage of expenditure, percentage of income |
          | EducationalIct                 | £ per pupil, actuals, percentage of expenditure, percentage of income |
          | Premises                       | £ per m², actuals, percentage of expenditure, percentage of income    |
          | Utilities                      | £ per m², actuals, percentage of expenditure, percentage of income    |
          | AdministrativeSupplies         | £ per pupil, actuals, percentage of expenditure, percentage of income |
          | CateringStaffAndServices       | £ per pupil, actuals, percentage of expenditure, percentage of income |
          | Other                          | £ per pupil, actuals, percentage of expenditure, percentage of income |

    Scenario: Save chart images button opens modal
        Given I am on compare your costs page for school with URN '777042'
        Then the save chart images button is visible
        When I click the save chart images button
        Then the save chart images modal is visible

    Scenario: Table view for 'per unit' total premises staff and service costs should display correct decimal places
        Given I am on compare your costs page for school with URN '777054'
        And table view is selected
        When I click on show all sections
        And I change 'total premises staff and service costs' dimension to '£ per m²'
        Then the following is shown for 'total premises staff and service costs'
          | School name                                                                                                         | Local Authority                  | School type                        | Number of pupils | Amount  |
          | Test academy school 250                                                                                             | North Somerset                   | Academy sponsor led                | 184              | £159.41 |
          | Test part year with no pupil and builiding comprator\n!\nWarning\nThis school only has 11 months of data available. | Wandsworth                       | Community special school           | 227              | £114.45 |
          | Test school 56                                                                                                      | Redcar and Cleveland             | Voluntary aided school             | 270              | £92.75  |
          | Test school 101                                                                                                     | Hackney                          | Pupil referral unit                | 190              | £91.97  |
          | Test academy school 405                                                                                             | Lancashire                       | Academy sponsor led                | 335              | £74.19  |
          | Test academy school 427                                                                                             | Kingston upon Thames             | Academy converter                  | 167              | £70.40  |
          | Test school 142                                                                                                     | Oldham                           | Community special school           | 449              | £67.36  |
          | Test academy school 418                                                                                             | Kensington and Chelsea           | Academy converter                  | 232              | £64.63  |
          | Test school 234                                                                                                     | West Northamptonshire            | Community school                   | 236              | £61.90  |
          | Test academy school 419                                                                                             | Lambeth                          | Academy sponsor led                | 236              | £61.90  |
          | Test academy school 167                                                                                             | Greenwich                        | Free school                        | 303              | £57.89  |
          | Test academy school 41                                                                                              | Bedford                          | Academy converter                  | 230              | £55.30  |
          | Test school 70                                                                                                      | Bournemouth Christchurch & Poole | Local authority nursery school     | 407              | £53.87  |
          | Test school 183                                                                                                     | County Durham                    | Voluntary aided school             | 450              | £53.06  |
          | Test school 143                                                                                                     | Rochdale                         | Community special school           | 446              | £47.84  |
          | Test school 88                                                                                                      | Slough                           | Community school                   | 134              | £46.79  |
          | Test academy school 78                                                                                              | Derbyshire                       | Academy sponsor led                | 1326             | £46.03  |
          | Test academy school 483                                                                                             | Bolton                           | Academy 16-19 converter            | 1326             | £46.03  |
          | Test school 158                                                                                                     | Newcastle upon Tyne              | Community school                   | 206              | £44.07  |
          | Test school 269                                                                                                     | Dudley                           | Community school                   | 407              | £43.87  |
          | Test school 133                                                                                                     | Walsall                          | Community school                   | 367              | £43.83  |
          | Test academy school 364                                                                                             | North East Lincolnshire          | Academy special converter          | 367              | £43.83  |
          | Test academy school 410                                                                                             | Shropshire                       | Academy sponsor led                | 216              | £43.43  |
          | Test academy school 28                                                                                              | Liverpool                        | Academy converter                  | 191              | £43.36  |
          | Test academy school 310                                                                                             | Waltham Forest                   | Free schools alternative provision | 1135             | £34.01  |
          | Test school 55                                                                                                      | Middlesbrough                    | Voluntary aided school             | 991              | £32.16  |
          | Test academy school 444                                                                                             | Lincolnshire                     | Academy sponsor led                | 991              | £32.16  |
          | Test school 124                                                                                                     | Newham                           | Voluntary aided school             | 769              | £29.55  |
          | Test academy school 136                                                                                             | Merton                           | Academy special converter          | 1040             | £13.09  |
          | Test academy school 134                                                                                             | Hounslow                         | Academy special converter          | 883              | £0.19   |

    Scenario: Clicking download button downloads .zip file
        Given I am on compare your costs page for school with URN '777042'
        When I click on download data
        Then the file 'comparison-777042.zip' is downloaded

    Scenario: Cost codes are displayed for maintained school
        Given I am on compare your costs page for school with URN '777042'
        Then all sections on the page have the correct cost codes:
          | Chart name                                               | Cost codes                                                       |
          | Total teaching and teaching support staff costs          | E01, E02, E26, E03, E27                                          |
          | Teaching staff costs                                     | E01                                                              |
          | Supply teaching staff costs                              | E02                                                              |
          | Educational consultancy costs                            | E27                                                              |
          | Educational support staff costs                          | E03                                                              |
          | Agency supply teaching staff costs                       | E26                                                              |
          | Total non-educational support staff costs                | E05, E07, E28a                                                   |
          | Administrative and clerical staff costs                  | E05                                                              |
          | Other staff costs                                        | E07                                                              |
          | Professional services (non-curriculum) costs             | E28a                                                             |
          | Total educational supplies costs                         | E21, E19                                                         |
          | Examination fees costs                                   | E21                                                              |
          | Learning resources (not ICT equipment) costs             | E19                                                              |
          | Educational learning resources costs                     | E20A, E20B, E20C, E20E, E20F, E20G                               |
          | Total premises staff and service costs                   | E14, E12, E18, E04                                               |
          | Cleaning and caretaking costs                            | E14                                                              |
          | Maintenance of premises costs                            | E12                                                              |
          | Other occupation costs                                   | E18                                                              |
          | Premises staff costs                                     | E04                                                              |
          | Total utilities costs                                    | E16, E15                                                         |
          | Energy costs                                             | E16                                                              |
          | Water and sewerage costs                                 | E15                                                              |
          | Administrative supplies (Non-educational)                | E22, E20D                                                        |
          | Total catering costs (gross)                             | E06, E25                                                         |
          | Catering staff costs                                     | E06                                                              |
          | Catering supplies costs                                  | E25                                                              |
          | Total other costs                                        | E30, E13, E08, E29, E23, E28b, E17, E24, E09, E11, E10, E31, E32 |
          | Other insurance premiums costs                           | E23                                                              |
          | Direct revenue financing costs                           | E30                                                              |
          | Ground maintenance costs                                 | E13                                                              |
          | Indirect employee expenses                               | E08                                                              |
          | Interest charges for loan and bank                       | E29                                                              |
          | PFI charges                                              | E28b                                                             |
          | Rent and rates costs                                     | E17                                                              |
          | Special facilities costs                                 | E24                                                              |
          | Staff development and training costs                     | E09                                                              |
          | Staff-related insurance costs                            | E11                                                              |
          | Supply teacher insurance costs                           | E10                                                              |
          | Community focused school staff (maintained schools only) | E31                                                              |
          | Community focused school costs (maintained schools only) | E32                                                              |

    Scenario: Cost codes are displayed for academy
        Given I am on compare your costs page for school with URN '990250'
        Then all sections on the page have the correct cost codes:
          | Chart name                                      | Cost codes                                                                                                    |
          | Total teaching and teaching support staff costs | BAE010, BAE020, BAE240, BAE030, BAE230, % of central services                                                 |
          | Teaching staff costs                            | BAE010, % of central services                                                                                 |
          | Supply teaching staff costs                     | BAE020, % of central services                                                                                 |
          | Educational consultancy costs                   | BAE230, % of central services                                                                                 |
          | Educational support staff costs                 | BAE030, % of central services                                                                                 |
          | Agency supply teaching staff costs              | BAE240, % of central services                                                                                 |
          | Total non-educational support staff costs       | BAE040, BAE260, BAE070, BAE300, % of central services                                                         |
          | Administrative and clerical staff costs         | BAE040, % of central services                                                                                 |
          | Auditors costs                                  | BAE260, % of central services                                                                                 |
          | Other staff costs                               | BAE070, % of central services                                                                                 |
          | Professional services (non-curriculum) costs    | BAE300, % of central services                                                                                 |
          | Total educational supplies costs                | BAE220, BAE200, % of central services                                                                         |
          | Examination fees costs                          | BAE220, % of central services                                                                                 |
          | Learning resources (not ICT equipment) costs    | BAE200, % of central services                                                                                 |
          | Educational learning resources costs            | BAE210, % of central services                                                                                 |
          | Total premises staff and service costs          | BAE130, BAE120, BAE180, BAE050, % of central services                                                         |
          | Cleaning and caretaking costs                   | BAE130, % of central services                                                                                 |
          | Maintenance of premises costs                   | BAE120, % of central services                                                                                 |
          | Other occupation costs                          | BAE180, % of central services                                                                                 |
          | Premises staff costs                            | BAE050, % of central services                                                                                 |
          | Total utilities costs                           | BAE150, BAE140, % of central services                                                                         |
          | Energy costs                                    | BAE150, % of central services                                                                                 |
          | Water and sewerage costs                        | BAE140, % of central services                                                                                 |
          | Administrative supplies (Non-educational)       | BAE280, % of central services                                                                                 |
          | Total catering costs (gross)                    | BAE060, BAE250, % of central services                                                                         |
          | Catering staff costs                            | BAE060, % of central services                                                                                 |
          | Catering supplies costs                         | BAE250, % of central services                                                                                 |
          | Total other costs                               | BAE290, BAE320, BAE080, BAE320, BAE270, BAE310, BAE160, BAE190, BAE090, BAE110, BAE100, % of central services |
          | Direct revenue financing costs                  | BAE290, % of central services                                                                                 |
          | Ground maintenance costs                        | BAE320, % of central services                                                                                 |
          | Indirect employee expenses                      | BAE080, % of central services                                                                                 |
          | Interest charges for loan and bank              | BAE320, % of central services                                                                                 |
          | PFI charges                                     | BAE310, % of central services                                                                                 |
          | Rent and rates costs                            | BAE160, % of central services                                                                                 |
          | Special facilities costs                        | BAE190, % of central services                                                                                 |
          | Staff development and training costs            | BAE090, % of central services                                                                                 |
          | Staff-related insurance costs                   | BAE110, % of central services                                                                                 |
          | Supply teacher insurance costs                  | BAE100, % of central services                                                                                 |