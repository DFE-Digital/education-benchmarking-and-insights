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

      # todo: replace stubbed values with real

    Scenario: Sending a valid local authorities national rank request
        Given a valid local authorities national rank request with sort order 'asc'
        When I submit the local authorities request
        Then the local authorities national rank result should contain the following:
          | Code | Name                | Value          | Rank |
          | 202  | Local authority 202 | 40.0312918279  | 1    |
          | 201  | Local authority 201 | 43.7717604530  | 2    |
          | 205  | Local authority 205 | 53.1408559476  | 3    |
          | 352  | Local authority 352 | 53.6770777604  | 4    |
          | 349  | Local authority 349 | 53.7686689440  | 5    |
          | 346  | Local authority 346 | 53.8619663731  | 6    |
          | 343  | Local authority 343 | 53.9570295662  | 7    |
          | 354  | Local authority 354 | 53.9973210666  | 8    |
          | 340  | Local authority 340 | 54.0539209819  | 9    |
          | 351  | Local authority 351 | 54.0959661603  | 10   |
          | 337  | Local authority 337 | 54.1527061938  | 11   |
          | 348  | Local authority 348 | 54.1966201506  | 12   |
          | 334  | Local authority 334 | 54.2534540759  | 13   |
          | 345  | Local authority 345 | 54.2993621225  | 14   |
          | 331  | Local authority 331 | 54.3562369981  | 15   |
          | 342  | Local authority 342 | 54.4042758096  | 16   |
          | 353  | Local authority 353 | 54.4183079294  | 17   |
          | 328  | Local authority 328 | 54.4611310325  | 18   |
          | 339  | Local authority 339 | 54.5114499584  | 19   |
          | 350  | Local authority 350 | 54.5261060170  | 20   |
          | 325  | Local authority 325 | 54.5682161681  | 21   |
          | 336  | Local authority 336 | 54.6209787291  | 22   |
          | 347  | Local authority 347 | 54.6363005769  | 23   |
          | 322  | Local authority 322 | 54.6775765355  | 24   |
          | 333  | Local authority 333 | 54.7329621367  | 25   |
          | 344  | Local authority 344 | 54.7489954197  | 26   |
          | 319  | Local authority 319 | 54.7893006389  | 27   |
          | 330  | Local authority 330 | 54.8475065362  | 28   |
          | 341  | Local authority 341 | 54.8643011342  | 29   |
          | 316  | Local authority 316 | 54.9034815936  | 30   |
          | 327  | Local authority 327 | 54.9647251593  | 31   |
          | 338  | Local authority 338 | 54.9823356853  | 32   |
          | 313  | Local authority 313 | 55.0202173655  | 33   |
          | 324  | Local authority 324 | 55.0847387065  | 34   |
          | 335  | Local authority 335 | 55.1032250795  | 35   |
          | 310  | Local authority 310 | 55.1396110095  | 36   |
          | 321  | Local authority 321 | 55.2076760041  | 37   |
          | 332  | Local authority 332 | 55.2271041055  | 38   |
          | 307  | Local authority 307 | 55.2617708995  | 39   |
          | 318  | Local authority 318 | 55.3336747326  | 40   |
          | 329  | Local authority 329 | 55.3541171631  | 41   |
          | 304  | Local authority 304 | 55.3868109419  | 42   |
          | 315  | Local authority 315 | 55.4628822366  | 43   |
          | 326  | Local authority 326 | 55.4844191905  | 44   |
          | 301  | Local authority 301 | 55.5148507606  | 45   |
          | 312  | Local authority 312 | 55.5954564268  | 46   |
          | 323  | Local authority 323 | 55.6181767065  | 47   |
          | 298  | Local authority 298 | 55.6460158378  | 48   |
          | 309  | Local authority 309 | 55.7315667866  | 49   |
          | 320  | Local authority 320 | 55.7555689829  | 50   |
          | 295  | Local authority 295 | 55.7804375877  | 51   |
          | 306  | Local authority 306 | 55.8713954981  | 52   |
          | 317  | Local authority 317 | 55.8967893700  | 53   |
          | 292  | Local authority 292 | 55.9182533334  | 54   |
          | 303  | Local authority 303 | 56.0151387022  | 55   |
          | 314  | Local authority 314 | 56.0420467961  | 56   |
          | 289  | Local authority 289 | 56.0596061450  | 57   |
          | 300  | Local authority 300 | 56.1630079149  | 58   |
          | 311  | Local authority 311 | 56.1915674723  | 59   |
          | 286  | Local authority 286 | 56.2046444828  | 60   |
          | 297  | Local authority 297 | 56.3152316184  | 61   |
          | 308  | Local authority 308 | 56.3455968354  | 62   |
          | 283  | Local authority 283 | 56.3535215663  | 63   |
          | 294  | Local authority 294 | 56.4720570549  | 64   |
          | 305  | Local authority 305 | 56.5044017695  | 65   |
          | 280  | Local authority 280 | 56.5063943622  | 66   |
          | 291  | Local authority 291 | 56.6337522516  | 67   |
          | 277  | Local authority 277 | 56.6634220413  | 68   |
          | 302  | Local authority 302 | 56.6682731572  | 69   |
          | 288  | Local authority 288 | 56.8006083097  | 70   |
          | 274  | Local authority 274 | 56.8247636960  | 71   |
          | 299  | Local authority 299 | 56.8375288192  | 72   |
          | 285  | Local authority 285 | 56.9729419977  | 73   |
          | 271  | Local authority 271 | 56.9905750241  | 74   |
          | 296  | Local authority 296 | 57.0125169153  | 75   |
          | 282  | Local authority 282 | 57.1510986908  | 76   |
          | 268  | Local authority 268 | 57.1610035658  | 77   |
          | 293  | Local authority 293 | 57.1936198970  | 78   |
          | 208  | Local authority 208 | 57.2761820214  | 79   |
          | 279  | Local authority 279 | 57.3354557080  | 80   |
          | 265  | Local authority 265 | 57.3361818975  | 81   |
          | 290  | Local authority 290 | 57.3812591213  | 82   |
          | 262  | Local authority 262 | 57.5162179277  | 83   |
          | 276  | Local authority 276 | 57.5264260998  | 84   |
          | 287  | Local authority 287 | 57.5759002634  | 85   |
          | 259  | Local authority 259 | 57.7011810407  | 86   |
          | 273  | Local authority 273 | 57.7244629472  | 87   |
          | 284  | Local authority 284 | 57.7780596990  | 88   |
          | 256  | Local authority 256 | 57.8910822361  | 89   |
          | 270  | Local authority 270 | 57.9300642312  | 90   |
          | 281  | Local authority 281 | 57.9883120708  | 91   |
          | 253  | Local authority 253 | 58.0858454820  | 92   |
          | 267  | Local authority 267 | 58.1437783336  | 93   |
          | 278  | Local authority 278 | 58.2072993144  | 94   |
          | 250  | Local authority 250 | 58.2852660375  | 95   |
          | 264  | Local authority 264 | 58.3662102118  | 96   |
          | 275  | Local authority 275 | 58.4357414904  | 97   |
          | 247  | Local authority 247 | 58.4889491557  | 98   |
          | 261  | Local authority 261 | 58.5980282657  | 99   |
          | 272  | Local authority 272 | 58.6744498754  | 100  |
          | 244  | Local authority 244 | 58.6962187233  | 101  |
          | 258  | Local authority 258 | 58.8399718510  | 102  |
          | 241  | Local authority 241 | 58.9059789099  | 103  |
          | 269  | Local authority 269 | 58.9243428974  | 104  |
          | 211  | Local authority 211 | 58.9613076022  | 105  |
          | 255  | Local authority 255 | 59.0928592777  | 106  |
          | 238  | Local authority 238 | 59.1165006772  | 107  |
          | 266  | Local authority 266 | 59.1864656896  | 108  |
          | 235  | Local authority 235 | 59.3250849479  | 109  |
          | 252  | Local authority 252 | 59.3575959187  | 110  |
          | 263  | Local authority 263 | 59.4620142895  | 111  |
          | 232  | Local authority 232 | 59.5275170902  | 112  |
          | 249  | Local authority 249 | 59.6351816604  | 113  |
          | 214  | Local authority 214 | 59.6970955538  | 114  |
          | 229  | Local authority 229 | 59.7171556479  | 115  |
          | 260  | Local authority 260 | 59.7523658699  | 116  |
          | 226  | Local authority 226 | 59.8833528430  | 117  |
          | 246  | Local authority 246 | 59.9267162099  | 118  |
          | 217  | Local authority 217 | 59.9931905700  | 119  |
          | 223  | Local authority 223 | 60.0085923082  | 120  |
          | 257  | Local authority 257 | 60.0591168908  | 121  |
          | 220  | Local authority 220 | 60.0630122926  | 122  |
          | 243  | Local authority 243 | 60.2333994582  | 123  |
          | 254  | Local authority 254 | 60.3841317918  | 124  |
          | 240  | Local authority 240 | 60.5565216462  | 125  |
          | 251  | Local authority 251 | 60.7296059061  | 126  |
          | 237  | Local authority 237 | 60.8974334164  | 127  |
          | 248  | Local authority 248 | 61.0981478697  | 128  |
          | 234  | Local authority 234 | 61.2574767340  | 129  |
          | 204  | Local authority 204 | 61.4750960385  | 130  |
          | 245  | Local authority 245 | 61.4928892194  | 131  |
          | 231  | Local authority 231 | 61.6378393040  | 132  |
          | 242  | Local authority 242 | 61.9176326567  | 133  |
          | 228  | Local authority 228 | 62.0392565646  | 134  |
          | 239  | Local authority 239 | 62.3770565102  | 135  |
          | 225  | Local authority 225 | 62.4614002972  | 136  |
          | 236  | Local authority 236 | 62.8770029387  | 137  |
          | 222  | Local authority 222 | 62.9015936462  | 138  |
          | 219  | Local authority 219 | 63.3519893507  | 139  |
          | 233  | Local authority 233 | 63.4248944994  | 140  |
          | 216  | Local authority 216 | 63.7929531758  | 141  |
          | 207  | Local authority 207 | 63.9981442299  | 142  |
          | 230  | Local authority 230 | 64.0303540628  | 143  |
          | 213  | Local authority 213 | 64.1760265937  | 144  |
          | 210  | Local authority 210 | 64.3737264204  | 145  |
          | 227  | Local authority 227 | 64.7061594766  | 146  |
          | 224  | Local authority 224 | 65.4697750342  | 147  |
          | 221  | Local authority 221 | 66.3459332348  | 148  |
          | 218  | Local authority 218 | 67.3712639762  | 149  |
          | 215  | Local authority 215 | 68.6032764977  | 150  |
          | 212  | Local authority 212 | 70.1397180629  | 151  |
          | 209  | Local authority 209 | 72.1669604003  | 152  |
          | 206  | Local authority 206 | 75.1120420820  | 153  |
          | 203  | Local authority 203 | 80.3722975665  | 154  |
          | 200  | Local authority 200 | 113.3032983918 | 155  |

          # todo: replace stubbed values with real

    Scenario: Sending a valid local authorities statistical neighbours request
        Given a valid local authorities statistical neighbours request with id '201'
        When I submit the local authorities request
        Then the local authorities statistical neighbours result should be ok and have the following values:
          | Field | Value          |
          | Code  | 201            |
          | Name  | City of London |
        And the local authorities statistical neighbours result should contain the following neighbours:
          | Code | Name                | Position |
          | 200  | Local authority 200 | 1        |
          | 201  | Local authority 201 | 2        |
          | 202  | Local authority 202 | 3        |
          | 203  | Local authority 203 | 4        |
          | 204  | Local authority 204 | 5        |
          | 205  | Local authority 205 | 6        |
          | 206  | Local authority 206 | 7        |
          | 207  | Local authority 207 | 8        |
          | 208  | Local authority 208 | 9        |
          | 209  | Local authority 209 | 10       |

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