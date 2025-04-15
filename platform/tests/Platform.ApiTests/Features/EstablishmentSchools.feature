Feature: Establishment schools endpoints

    Scenario: Sending a valid school request
        Given a valid school request with id '777042'
        When I submit the schools request
        Then the school result should be ok and have the following values:
          | Field      | Value           |
          | URN        | 777042          |
          | SchoolName | Test school 102 |

    Scenario: Sending an invalid school request should return not found
        Given an invalid school request with id '999999'
        When I submit the schools request
        Then the school result should be not found

    Scenario: Sending a valid suggest schools request with exact URN
        Given a valid schools suggest request with searchText '777042'
        When I submit the schools request
        Then the school suggest result should be ok and have the following values:
          | Field      | Value           |
          | Text       | *777042*        |
          | URN        | 777042          |
          | SchoolName | Test school 102 |

    Scenario: Sending a valid suggest schools request with partial name
        Given a valid schools suggest request with searchText 'test'
        When I submit the schools request
        Then the schools suggest result should be ok and have the following multiple values:
          | Text                     | SchoolName             | URN    |
          | *Test* academy school 92 | Test academy school 92 | 777054 |
          | *Test* school 249        | Test school 249        | 990007 |
          | *Test* school 68         | Test school 68         | 990014 |
          | *Test* school 219        | Test school 219        | 990018 |
          | *Test* school 26         | Test school 26         | 990031 |

    Scenario: Sending a valid suggest schools request
        Given a valid schools suggest request with searchText 'willNotBeFound'
        When I submit the schools request
        Then the schools suggest result should be empty

    Scenario: Sending an invalid suggest schools request returns the validation results
        Given an invalid schools suggest request with '<SearchText>' and '<Size>'
        When I submit the schools request
        Then the schools suggest result should be bad request and have the following validation errors:
          | PropertyName  | ErrorMessage                |
          | SearchText    | <SearchTextErrorMessage>    |
          | Size          | <SizeErrorMessage>          |

    Examples:
      | SuggesterName | SearchText                                                                                                    | Size | SuggesterNameErrorMessage           | SearchTextErrorMessage                                                                   | SizeErrorMessage                             |
      | suggester     | te                                                                                                            | 5    |                                     | The length of 'Search Text' must be at least 3 characters. You entered 2 characters.     |                                              |
      | suggester     | 0123456789_0123456789_0123456789_0123456789_0123456789_0123456789_0123456789_0123456789_0123456789_0123456789 | 5    |                                     | The length of 'Search Text' must be 100 characters or fewer. You entered 109 characters. |                                              |

    Scenario: Sending a valid search schools request with URN
        Given a valid schools search request with searchText '777042' page '1' size '5'
        When I submit the schools request
        Then the search schools response should be ok and have the following values:  
          | TotalResults | Page | PageSize | PageCount |
          | 1            | 1    | 5        | 1         |
        And the results should include the following schools:
          | Urn    | SchoolName      | AddressStreet | AddressLocality | AddressLine3 | AddressTown | AddressCounty | AddressPostcode | OverallPhase | PeriodCoveredByReturn | TotalPupils |
          | 777042 | Test school 102 | address 5     |                 |              | London      |               | ABC127          | Primary      | 12                    | 212         |

    Scenario: Sending a valid search schools request with search text
        Given a valid schools search request with searchText 'Test' page '1' size '5'
        When I submit the schools request
        Then the search schools response should be ok and have the following values:  
          | TotalResults | Page | PageSize | PageCount |
          | 763            | 1    | 5        | 153     |
        And the results should include the following schools:
          | Urn    | SchoolName             | AddressStreet | AddressLocality | AddressLine3 | AddressTown | AddressCounty | AddressPostcode | OverallPhase | PeriodCoveredByReturn | TotalPupils |
          | 777054 | Test academy school 92 | address 281   | Brixton         |              | London      |               | ABC403          | Secondary    | 3                     | 216         |
          | 990007 | Test school 249        | address 167   | White City      |              | London      |               | ABC289          | Special      | 12                    | 134         |
          | 990014 | Test school 68         | address 243   |                 | Brixton      | London      |               | ABC365          | Nursery      | 12                    | 339         |
          | 990018 | Test school 219        | address 134   | Hackney         |              | London      |               | ABC256          | Secondary    | 12                    | 37          |
          | 990031 | Test school 26         | address 179   | Upper Street    |              | London      |               | ABC301          | Primary      | 12                    | 769         |

    Scenario: Sending a valid search schools request with a single filter
        Given I have the following filters:
          | Field        | Value   |
          | OverallPhase | Primary |
        And a valid schools search request with searchText 'Test' page '1' size '5'
        When I submit the schools request
        Then the search schools response should be ok and have the following values:  
          | TotalResults | Page | PageSize | PageCount |
          | 299          | 1    | 5        | 60       |
        And the results should include the following schools:
          | Urn    | SchoolName     | AddressStreet | AddressLocality         | AddressLine3                         | AddressTown | AddressCounty | AddressPostcode | OverallPhase | PeriodCoveredByReturn | TotalPupils |
          | 990031 | Test school 26 | address 179   | Upper Street            |                                      | London      |               | ABC301          | Primary      | 12                    | 769         |
          | 990104 | Test school 64 | address 239   | Kensington and Chelsea  | Our Lady of Victories Primary School | London      |               | ABC361          | Primary      | 12                    | 191         |
          | 990111 | Test school 94 | address 272   | Lambeth                 | Kennington                           | London      |               | ABC394          | Primary      | 12                    | 1040        |
          | 990155 | Test school 81 | address 258   | Brixton                 |                                      | London      |               | ABC380          | Primary      | 12                    | 303         |
          | 990163 | Test school 47 | address 220   |                         |                                      | London      |               | ABC342          | Primary      | 12                    | 260         |

    Scenario: Sending a valid search schools request with many filters
        Given I have the following filters:
          | Field        | Value     |
          | OverallPhase | Primary   |
          | OverallPhase | Secondary |
        And a valid schools search request with searchText 'Test' page '1' size '5'
        When I submit the schools request
        Then the search schools response should be ok and have the following values:  
          | TotalResults | Page | PageSize | PageCount |
          | 424          | 1    | 5        | 85       |
        And the results should include the following schools:
          | Urn    | SchoolName             | AddressStreet | AddressLocality        | AddressLine3                         | AddressTown | AddressCounty | AddressPostcode | OverallPhase | PeriodCoveredByReturn | TotalPupils |
          | 777054 | Test academy school 92 | address 281   | Brixton                |                                      | London      |               | ABC403          | Secondary    | 3                     | 216         |
          | 990018 | Test school 219        | address 134   | Hackney                |                                      | London      |               | ABC256          | Secondary    | 12                    | 37          |
          | 990031 | Test school 26         | address 179   | Upper Street           |                                      | London      |               | ABC301          | Primary      | 12                    | 769         |
          | 990104 | Test school 64         | address 239   | Kensington and Chelsea | Our Lady of Victories Primary School | London      |               | ABC361          | Primary      | 12                    | 191         |
          | 990111 | Test school 94         | address 272   | Lambeth                | Kennington                           | London      |               | ABC394          | Primary      | 12                    | 1040        |

    Scenario: Sending a valid search schools request with order by ascending
        Given a valid schools search request with searchText 'Test' page '1' size '5' orderByField 'SchoolNameSortable' orderByValue 'asc'
        When I submit the schools request
        Then the search schools response should be ok and have the following values:  
          | TotalResults | Page | PageSize | PageCount |
          | 763            | 1    | 5        | 153     |
        And the results should include the following schools:  
          | Urn    | SchoolName                                                 | AddressStreet | AddressLocality | AddressLine3 | AddressTown | AddressCounty      | AddressPostcode | OverallPhase | PeriodCoveredByReturn | TotalPupils |
          | 777043 | Test Part year school with pupil and builiding comparators | address 261   |                 |              | London      |                    | ABC383          | Primary      | 10                    | 214         |
          | 777047 | Test Post -16 Academy of Excellence                        | Academy House | 322 High Street | Stratford    | London      |                    | X11 11X         | Post-16      | 12                    | 380         |
          | 990324 | Test academy school 1                                      | address 293   | Brixton         |              | Manchester  | Greater Manchester | ABC415          | Primary      | 12                    | 270         |
          | 990372 | Test academy school 10                                     | address 291   | Brixton         |              | Manchester  | Greater Manchester | ABC413          | Primary      | 12                    | 114         |
          | 990335 | Test academy school 100                                    | address 289   | Brixton         |              | Manchester  | Greater Manchester | ABC411          | Secondary    | 12                    | 37          |

    Scenario: Sending a valid search schools request with order by descending
        Given a valid schools search request with searchText 'Test' page '1' size '5' orderByField 'SchoolNameSortable' orderByValue 'desc'
        When I submit the schools request
        Then the search schools response should be ok and have the following values:  
          | TotalResults | Page | PageSize | PageCount |
          | 763            | 1    | 5        | 153     |
        And the results should include the following schools:
          | Urn    | SchoolName                           | AddressStreet | AddressLocality     | AddressLine3 | AddressTown    | AddressCounty | AddressPostcode | OverallPhase | PeriodCoveredByReturn | TotalPupils |
          | 990754 | Test school with missing census data | address 754   | Chalk Road          |              | Rotherham      |               | ABC754          | Secondary    | 12                    |             |
          | 990035 | Test school 98                       | address 276   | Camberwell New Road |              | London         |               | ABC398          | Primary      | 12                    | 769         |
          | 990170 | Test school 97                       | address 275   | Stockwell           |              | London         |               | ABC397          | Primary      | 12                    | 1045        |
          | 990095 | Test school 96                       | address 274   | Clapham             |              |                |               | ABC396          | Primary      | 12                    | 1135        |
          | 990219 | Test school 95                       | address 273   | North Brixton       |              | London         |               | ABC395          | Primary      | 12                    | 1326        |

    Scenario: Sending a valid search schools request with many filters and order by ascending
        Given I have the following filters:
          | Field        | Value       |
          | OverallPhase | Primary     |
          | OverallPhase | Secondary   |
          | OverallPhase | All-through |
        Given a valid schools search request with searchText 'Test' page '1' size '5' orderByField 'SchoolNameSortable' orderByValue 'asc'
        When I submit the schools request
        Then the search schools response should be ok and have the following values:  
          | TotalResults | Page | PageSize | PageCount |
          | 500            | 1    | 5        | 100     |
        And the results should include the following schools:
          | Urn    | SchoolName                                                 | AddressStreet | AddressLocality     | AddressLine3 | AddressTown | AddressCounty         | AddressPostcode | OverallPhase | PeriodCoveredByReturn | TotalPupils |
          | 777043 | Test Part year school with pupil and builiding comparators | address 261   |                     |              | London      |                       | ABC383          | Primary      | 10                    | 214         |
          | 990324 | Test academy school 1                                      | address 293   | Brixton             |              | Manchester  | Greater Manchester    | ABC415          | Primary      | 12                    | 270         |
          | 990372 | Test academy school 10                                     | address 291   | Brixton             |              | Manchester  | Greater Manchester    | ABC413          | Primary      | 12                    | 114         |
          | 990335 | Test academy school 100                                    | address 289   | Brixton             |              | Manchester  | Greater Manchester    | ABC411          | Secondary    | 12                    | 37          |
          | 990395 | Test academy school 11                                     | address 294   | Camberwell New Road |              | London      |                       | ABC416          | Primary      | 12                    | 339         |

    Scenario: Sending a valid search schools request
        Given a valid schools search request with searchText 'willNotBeFound' page '1' size '5'
        When I submit the schools request
        Then the schools search result should be empty
        
    Scenario: Sending an invalid search schools request
        Given an invalid schools search request
        When I submit the schools request
        Then the search schools response should be bad request containing validation errors