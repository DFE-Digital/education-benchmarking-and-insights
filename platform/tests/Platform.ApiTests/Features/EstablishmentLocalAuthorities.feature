Feature: Establishment local authorities endpoints

    Scenario: Sending a valid local authority request
        Given a valid local authority request with id '201'
        When I submit the local authorities request
        Then the local authority result should be ok and have the following values:
          | Field | Value          |
          | Code  | 201            |
          | Name  | City of London |
        And the local authority result should contain the following schools:
          | URN    | SchoolName      | OverallPhase |
          | 990116 | Test school 237 | Primary      |
          | 990134 | Test school 1   | Primary      |

    Scenario: Sending an invalid local authority request should return not found
        Given an invalid local authority request with id '999'
        When I submit the local authorities request
        Then the local authority result should be not found

    Scenario: Sending a valid suggest local authorities request with exact code
        Given a valid local authorities suggest request with searchText '201'
        When I submit the local authorities request
        Then the local authorities suggest result should be ok and have the following values:
          | Field | Value          |
          | Text  | *201*          |
          | Code  | 201            |
          | Name  | City of London |

    Scenario: Sending a valid suggest local authorities request with partial name
        Given a valid local authorities suggest request with searchText 'of'
        When I submit the local authorities request
        Then the local authorities suggest result should be ok and have the following multiple values:
          | Text                       | Name                     | Code |
          | East Riding *of* Yorkshire | East Riding of Yorkshire | 811  |
          | Isles *of* Scilly          | Isles of Scilly          | 420  |
          | City *of* London           | City of London           | 201  |
          | Isle *of* Wight            | Isle of Wight            | 921  |

    Scenario: Sending a valid suggest local authorities request
        Given a valid local authorities suggest request with searchText 'willNotBeFound'
        When I submit the local authorities request
        Then the local authorities suggest result should be empty

    Scenario: Sending an invalid suggest local authorities request returns the validation results
        Given an invalid local authorities suggest request with '<SearchText>' and '<Size>'
        When I submit the local authorities request
        Then the local authorities suggest result should be bad request and have the following validation errors:
          | PropertyName | ErrorMessage             |
          | SearchText   | <SearchTextErrorMessage> |
          | Size         | <SizeErrorMessage>       |

    Examples:
      | SuggesterName | SearchText                                                                                                    | Size | SuggesterNameErrorMessage | SearchTextErrorMessage                                                                   | SizeErrorMessage |
      | suggester     | te                                                                                                            | 5    |                           | The length of 'Search Text' must be at least 3 characters. You entered 2 characters.     |                  |
      | suggester     | 0123456789_0123456789_0123456789_0123456789_0123456789_0123456789_0123456789_0123456789_0123456789_0123456789 | 5    |                           | The length of 'Search Text' must be 100 characters or fewer. You entered 109 characters. |                  |

    @HighNeedsFlagEnabled
    Scenario: Sending a valid local authorities national rank request
        Given a valid local authorities national rank request with sort order 'asc'
        When I submit the local authorities request
        Then the local authorities national rank result should contain the following:
          | Code | Name                   | Value                | Rank |
          | 201  | City of London         | 74.3672386895475819  | 1    |
          | 206  | Islington              | 83.7501920181560312  | 2    |
          | 203  | Greenwich              | 90.5839754210040574  | 3    |
          | 205  | Hammersmith and Fulham | 97.4210777406673857  | 4    |
          | 202  | Camden                 | 97.4536443741161516  | 5    |
          | 210  | Southwark              | 97.4805362985510873  | 6    |
          | 208  | Lambeth                | 98.5012502020260697  | 7    |
          | 207  | Kensington and Chelsea | 100.3117545663038258 | 8    |
          | 209  | Lewisham               | 105.2993762770436369 | 9    |
          | 204  | Hackney                | 109.1145986925620464 | 10   |

    @HighNeedsFlagEnabled
    Scenario: Sending a valid local authorities statistical neighbours request
        Given a valid local authorities statistical neighbours request with id '201'
        When I submit the local authorities request
        Then the local authorities statistical neighbours result should be ok and have the following values:
          | Field | Value          |
          | Code  | 201            |
          | Name  | City of London |
        And the local authorities statistical neighbours result should contain the following neighbours:
          | Code | Name                   | Position |
          | 202  | Camden                 | 1        |
          | 203  | Greenwich              | 2        |
          | 204  | Hackney                | 3        |
          | 205  | Hammersmith and Fulham | 4        |
          | 206  | Islington              | 5        |
          | 207  | Kensington and Chelsea | 6        |
          | 208  | Lambeth                | 7        |
          | 209  | Lewisham               | 8        |
          | 210  | Southwark              | 9        |
          | 211  | Tower Hamlets          | 10       |

    Scenario: Sending a valid local authorities request
        Given a valid local authorities request
        When I submit the local authorities request
        Then the local authorities result should be ok and have the following values:
          | Code | Name                              |
          | 301  | Barking and Dagenham              |
          | 302  | Barnet                            |
          | 370  | Barnsley                          |
          | 800  | Bath and North East Somerset      |
          | 822  | Bedford                           |
          | 303  | Bexley                            |
          | 330  | Birmingham                        |
          | 889  | Blackburn with Darwen             |
          | 890  | Blackpool                         |
          | 350  | Bolton                            |
          | 839  | Bournemouth, Christchurch & Poole |
          | 867  | Bracknell Forest                  |
          | 380  | Bradford                          |
          | 304  | Brent                             |
          | 846  | Brighton and Hove                 |
          | 801  | Bristol                           |
          | 305  | Bromley                           |
          | 825  | Buckinghamshire                   |
          | 351  | Bury                              |
          | 381  | Calderdale                        |
          | 873  | Cambridgeshire                    |
          | 202  | Camden                            |
          | 823  | Central Bedfordshire              |
          | 895  | Cheshire East                     |
          | 896  | Cheshire West & Chester           |
          | 201  | City of London                    |
          | 908  | Cornwall                          |
          | 840  | County Durham                     |
          | 331  | Coventry                          |
          | 306  | Croydon                           |
          | 942  | Cumberland                        |
          | 841  | Darlington                        |
          | 831  | Derby                             |
          | 830  | Derbyshire                        |
          | 878  | Devon                             |
          | 371  | Doncaster                         |
          | 838  | Dorset                            |
          | 332  | Dudley                            |
          | 307  | Ealing                            |
          | 811  | East Riding of Yorkshire          |
          | 845  | East Sussex                       |
          | 308  | Enfield                           |
          | 881  | Essex                             |
          | 390  | Gateshead                         |
          | 916  | Gloucestershire                   |
          | 203  | Greenwich                         |
          | 204  | Hackney                           |
          | 876  | Halton                            |
          | 205  | Hammersmith and Fulham            |
          | 850  | Hampshire                         |
          | 309  | Haringey                          |
          | 310  | Harrow                            |
          | 805  | Hartlepool                        |
          | 311  | Havering                          |
          | 884  | Herefordshire                     |
          | 919  | Hertfordshire                     |
          | 312  | Hillingdon                        |
          | 313  | Hounslow                          |
          | 921  | Isle of Wight                     |
          | 420  | Isles of Scilly                   |
          | 206  | Islington                         |
          | 207  | Kensington and Chelsea            |
          | 886  | Kent                              |
          | 810  | Kingston Upon Hull                |
          | 314  | Kingston upon Thames              |
          | 382  | Kirklees                          |
          | 340  | Knowsley                          |
          | 208  | Lambeth                           |
          | 888  | Lancashire                        |
          | 383  | Leeds                             |
          | 856  | Leicester                         |
          | 855  | Leicestershire                    |
          | 209  | Lewisham                          |
          | 925  | Lincolnshire                      |
          | 341  | Liverpool                         |
          | 821  | Luton                             |
          | 352  | Manchester                        |
          | 887  | Medway                            |
          | 315  | Merton                            |
          | 806  | Middlesbrough                     |
          | 826  | Milton Keynes                     |
          | 391  | Newcastle upon Tyne               |
          | 316  | Newham                            |
          | 926  | Norfolk                           |
          | 812  | North East Lincolnshire           |
          | 813  | North Lincolnshire                |
          | 940  | North Northamptonshire            |
          | 802  | North Somerset                    |
          | 392  | North Tyneside                    |
          | 815  | North Yorkshire                   |
          | 929  | Northumberland                    |
          | 892  | Nottingham                        |
          | 891  | Nottinghamshire                   |
          | 353  | Oldham                            |
          | 931  | Oxfordshire                       |
          | 874  | Peterborough                      |
          | 879  | Plymouth                          |
          | 851  | Portsmouth                        |
          | 870  | Reading                           |
          | 317  | Redbridge                         |
          | 807  | Redcar and Cleveland              |
          | 318  | Richmond upon Thames              |
          | 354  | Rochdale                          |
          | 372  | Rotherham                         |
          | 857  | Rutland                           |
          | 355  | Salford                           |
          | 333  | Sandwell                          |
          | 343  | Sefton                            |
          | 373  | Sheffield                         |
          | 893  | Shropshire                        |
          | 871  | Slough                            |
          | 334  | Solihull                          |
          | 933  | Somerset                          |
          | 803  | South Gloucestershire             |
          | 393  | South Tyneside                    |
          | 852  | Southampton                       |
          | 882  | Southend-on-Sea                   |
          | 210  | Southwark                         |
          | 342  | St. Helens                        |
          | 860  | Staffordshire                     |
          | 356  | Stockport                         |
          | 808  | Stockton-on-Tees                  |
          | 861  | Stoke-on-Trent                    |
          | 935  | Suffolk                           |
          | 394  | Sunderland                        |
          | 936  | Surrey                            |
          | 319  | Sutton                            |
          | 866  | Swindon                           |
          | 357  | Tameside                          |
          | 894  | Telford and Wrekin                |
          | 006  | Test Local Authority              |
          | 883  | Thurrock                          |
          | 880  | Torbay                            |
          | 211  | Tower Hamlets                     |
          | 358  | Trafford                          |
          | 384  | Wakefield                         |
          | 335  | Walsall                           |
          | 320  | Waltham Forest                    |
          | 212  | Wandsworth                        |
          | 877  | Warrington                        |
          | 937  | Warwickshire                      |
          | 869  | West Berkshire                    |
          | 941  | West Northamptonshire             |
          | 938  | West Sussex                       |
          | 213  | Westminster                       |
          | 943  | Westmorland and Furness           |
          | 359  | Wigan                             |
          | 865  | Wiltshire                         |
          | 868  | Windsor and Maidenhead            |
          | 344  | Wirral                            |
          | 872  | Wokingham                         |
          | 336  | Wolverhampton                     |
          | 885  | Worcestershire                    |
          | 816  | York                              |

    Scenario: Sending a valid search local authorities request with la code
        Given a valid local authorities search request with searchText '201' page '1' size '5'
        When I submit the local authorities request
        Then the search local authorities response should be ok and have the following values:
          | TotalResults | Page | PageSize | PageCount |
          | 1            | 1    | 5        | 1         |
        And the results should include the following local authorities:
          | Code | Name           |
          | 201  | City of London |

    Scenario: Sending a valid search local authorities request with search text
        Given a valid local authorities search request with searchText 'and' page '1' size '5'
        When I submit the local authorities request
        Then the search local authorities response should be ok and have the following values:
          | TotalResults | Page | PageSize | PageCount |
          | 9            | 1    | 5        | 2         |
        And the results should include the following local authorities:
          | Code | Name                         |
          | 894  | Telford and Wrekin           |
          | 868  | Windsor and Maidenhead       |
          | 301  | Barking and Dagenham         |
          | 807  | Redcar and Cleveland         |
          | 800  | Bath and North East Somerset |

    Scenario: Sending a valid search local authorities request with order by ascending
        Given a valid local authorities search request with searchText 'and' page '1' size '5' orderByField 'LocalAuthorityNameSortable' orderByValue 'asc'
        When I submit the local authorities request
        Then the search local authorities response should be ok and have the following values:
          | TotalResults | Page | PageSize | PageCount |
          | 9            | 1    | 5        | 2         |
        And the results should include the following local authorities:
          | Code | Name                         |
          | 205  | Hammersmith and Fulham       |
          | 207  | Kensington and Chelsea       |
          | 301  | Barking and Dagenham         |
          | 800  | Bath and North East Somerset |
          | 846  | Brighton and Hove            |

    Scenario: Sending a valid search local authorities request with order by descending
        Given a valid local authorities search request with searchText 'and' page '1' size '5' orderByField 'LocalAuthorityNameSortable' orderByValue 'desc'
        When I submit the local authorities request
        Then the search local authorities response should be ok and have the following values:
          | TotalResults | Page | PageSize | PageCount |
          | 9            | 1    | 5        | 2         |
        And the results should include the following local authorities:
          | Code | Name                    |
          | 207  | Kensington and Chelsea  |
          | 868  | Windsor and Maidenhead  |
          | 943  | Westmorland and Furness |
          | 894  | Telford and Wrekin      |
          | 807  | Redcar and Cleveland    |

    Scenario: Sending a valid search local authorities request
        Given a valid local authorities search request with searchText 'willNotBeFound' page '1' size '5'
        When I submit the local authorities request
        Then the local authorities search result should be empty

    Scenario: Sending an invalid search local authorities request
        Given an invalid local authorities search request
        When I submit the local authorities request
        Then the search local authorities response should be bad request containing validation errors