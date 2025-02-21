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
          | Code | Name   | Value          | Rank |
          | 202  | LA 202 | 40.0312918279  | 0    |
          | 201  | LA 201 | 43.7717604530  | 1    |
          | 205  | LA 205 | 53.1408559476  | 2    |
          | 352  | LA 352 | 53.6770777604  | 3    |
          | 349  | LA 349 | 53.7686689440  | 4    |
          | 346  | LA 346 | 53.8619663731  | 5    |
          | 343  | LA 343 | 53.9570295662  | 6    |
          | 354  | LA 354 | 53.9973210666  | 7    |
          | 340  | LA 340 | 54.0539209819  | 8    |
          | 351  | LA 351 | 54.0959661603  | 9    |
          | 337  | LA 337 | 54.1527061938  | 10   |
          | 348  | LA 348 | 54.1966201506  | 11   |
          | 334  | LA 334 | 54.2534540759  | 12   |
          | 345  | LA 345 | 54.2993621225  | 13   |
          | 331  | LA 331 | 54.3562369981  | 14   |
          | 342  | LA 342 | 54.4042758096  | 15   |
          | 353  | LA 353 | 54.4183079294  | 16   |
          | 328  | LA 328 | 54.4611310325  | 17   |
          | 339  | LA 339 | 54.5114499584  | 18   |
          | 350  | LA 350 | 54.5261060170  | 19   |
          | 325  | LA 325 | 54.5682161681  | 20   |
          | 336  | LA 336 | 54.6209787291  | 21   |
          | 347  | LA 347 | 54.6363005769  | 22   |
          | 322  | LA 322 | 54.6775765355  | 23   |
          | 333  | LA 333 | 54.7329621367  | 24   |
          | 344  | LA 344 | 54.7489954197  | 25   |
          | 319  | LA 319 | 54.7893006389  | 26   |
          | 330  | LA 330 | 54.8475065362  | 27   |
          | 341  | LA 341 | 54.8643011342  | 28   |
          | 316  | LA 316 | 54.9034815936  | 29   |
          | 327  | LA 327 | 54.9647251593  | 30   |
          | 338  | LA 338 | 54.9823356853  | 31   |
          | 313  | LA 313 | 55.0202173655  | 32   |
          | 324  | LA 324 | 55.0847387065  | 33   |
          | 335  | LA 335 | 55.1032250795  | 34   |
          | 310  | LA 310 | 55.1396110095  | 35   |
          | 321  | LA 321 | 55.2076760041  | 36   |
          | 332  | LA 332 | 55.2271041055  | 37   |
          | 307  | LA 307 | 55.2617708995  | 38   |
          | 318  | LA 318 | 55.3336747326  | 39   |
          | 329  | LA 329 | 55.3541171631  | 40   |
          | 304  | LA 304 | 55.3868109419  | 41   |
          | 315  | LA 315 | 55.4628822366  | 42   |
          | 326  | LA 326 | 55.4844191905  | 43   |
          | 301  | LA 301 | 55.5148507606  | 44   |
          | 312  | LA 312 | 55.5954564268  | 45   |
          | 323  | LA 323 | 55.6181767065  | 46   |
          | 298  | LA 298 | 55.6460158378  | 47   |
          | 309  | LA 309 | 55.7315667866  | 48   |
          | 320  | LA 320 | 55.7555689829  | 49   |
          | 295  | LA 295 | 55.7804375877  | 50   |
          | 306  | LA 306 | 55.8713954981  | 51   |
          | 317  | LA 317 | 55.8967893700  | 52   |
          | 292  | LA 292 | 55.9182533334  | 53   |
          | 303  | LA 303 | 56.0151387022  | 54   |
          | 314  | LA 314 | 56.0420467961  | 55   |
          | 289  | LA 289 | 56.0596061450  | 56   |
          | 300  | LA 300 | 56.1630079149  | 57   |
          | 311  | LA 311 | 56.1915674723  | 58   |
          | 286  | LA 286 | 56.2046444828  | 59   |
          | 297  | LA 297 | 56.3152316184  | 60   |
          | 308  | LA 308 | 56.3455968354  | 61   |
          | 283  | LA 283 | 56.3535215663  | 62   |
          | 294  | LA 294 | 56.4720570549  | 63   |
          | 305  | LA 305 | 56.5044017695  | 64   |
          | 280  | LA 280 | 56.5063943622  | 65   |
          | 291  | LA 291 | 56.6337522516  | 66   |
          | 277  | LA 277 | 56.6634220413  | 67   |
          | 302  | LA 302 | 56.6682731572  | 68   |
          | 288  | LA 288 | 56.8006083097  | 69   |
          | 274  | LA 274 | 56.8247636960  | 70   |
          | 299  | LA 299 | 56.8375288192  | 71   |
          | 285  | LA 285 | 56.9729419977  | 72   |
          | 271  | LA 271 | 56.9905750241  | 73   |
          | 296  | LA 296 | 57.0125169153  | 74   |
          | 282  | LA 282 | 57.1510986908  | 75   |
          | 268  | LA 268 | 57.1610035658  | 76   |
          | 293  | LA 293 | 57.1936198970  | 77   |
          | 208  | LA 208 | 57.2761820214  | 78   |
          | 279  | LA 279 | 57.3354557080  | 79   |
          | 265  | LA 265 | 57.3361818975  | 80   |
          | 290  | LA 290 | 57.3812591213  | 81   |
          | 262  | LA 262 | 57.5162179277  | 82   |
          | 276  | LA 276 | 57.5264260998  | 83   |
          | 287  | LA 287 | 57.5759002634  | 84   |
          | 259  | LA 259 | 57.7011810407  | 85   |
          | 273  | LA 273 | 57.7244629472  | 86   |
          | 284  | LA 284 | 57.7780596990  | 87   |
          | 256  | LA 256 | 57.8910822361  | 88   |
          | 270  | LA 270 | 57.9300642312  | 89   |
          | 281  | LA 281 | 57.9883120708  | 90   |
          | 253  | LA 253 | 58.0858454820  | 91   |
          | 267  | LA 267 | 58.1437783336  | 92   |
          | 278  | LA 278 | 58.2072993144  | 93   |
          | 250  | LA 250 | 58.2852660375  | 94   |
          | 264  | LA 264 | 58.3662102118  | 95   |
          | 275  | LA 275 | 58.4357414904  | 96   |
          | 247  | LA 247 | 58.4889491557  | 97   |
          | 261  | LA 261 | 58.5980282657  | 98   |
          | 272  | LA 272 | 58.6744498754  | 99   |
          | 244  | LA 244 | 58.6962187233  | 100  |
          | 258  | LA 258 | 58.8399718510  | 101  |
          | 241  | LA 241 | 58.9059789099  | 102  |
          | 269  | LA 269 | 58.9243428974  | 103  |
          | 211  | LA 211 | 58.9613076022  | 104  |
          | 255  | LA 255 | 59.0928592777  | 105  |
          | 238  | LA 238 | 59.1165006772  | 106  |
          | 266  | LA 266 | 59.1864656896  | 107  |
          | 235  | LA 235 | 59.3250849479  | 108  |
          | 252  | LA 252 | 59.3575959187  | 109  |
          | 263  | LA 263 | 59.4620142895  | 110  |
          | 232  | LA 232 | 59.5275170902  | 111  |
          | 249  | LA 249 | 59.6351816604  | 112  |
          | 214  | LA 214 | 59.6970955538  | 113  |
          | 229  | LA 229 | 59.7171556479  | 114  |
          | 260  | LA 260 | 59.7523658699  | 115  |
          | 226  | LA 226 | 59.8833528430  | 116  |
          | 246  | LA 246 | 59.9267162099  | 117  |
          | 217  | LA 217 | 59.9931905700  | 118  |
          | 223  | LA 223 | 60.0085923082  | 119  |
          | 257  | LA 257 | 60.0591168908  | 120  |
          | 220  | LA 220 | 60.0630122926  | 121  |
          | 243  | LA 243 | 60.2333994582  | 122  |
          | 254  | LA 254 | 60.3841317918  | 123  |
          | 240  | LA 240 | 60.5565216462  | 124  |
          | 251  | LA 251 | 60.7296059061  | 125  |
          | 237  | LA 237 | 60.8974334164  | 126  |
          | 248  | LA 248 | 61.0981478697  | 127  |
          | 234  | LA 234 | 61.2574767340  | 128  |
          | 204  | LA 204 | 61.4750960385  | 129  |
          | 245  | LA 245 | 61.4928892194  | 130  |
          | 231  | LA 231 | 61.6378393040  | 131  |
          | 242  | LA 242 | 61.9176326567  | 132  |
          | 228  | LA 228 | 62.0392565646  | 133  |
          | 239  | LA 239 | 62.3770565102  | 134  |
          | 225  | LA 225 | 62.4614002972  | 135  |
          | 236  | LA 236 | 62.8770029387  | 136  |
          | 222  | LA 222 | 62.9015936462  | 137  |
          | 219  | LA 219 | 63.3519893507  | 138  |
          | 233  | LA 233 | 63.4248944994  | 139  |
          | 216  | LA 216 | 63.7929531758  | 140  |
          | 207  | LA 207 | 63.9981442299  | 141  |
          | 230  | LA 230 | 64.0303540628  | 142  |
          | 213  | LA 213 | 64.1760265937  | 143  |
          | 210  | LA 210 | 64.3737264204  | 144  |
          | 227  | LA 227 | 64.7061594766  | 145  |
          | 224  | LA 224 | 65.4697750342  | 146  |
          | 221  | LA 221 | 66.3459332348  | 147  |
          | 218  | LA 218 | 67.3712639762  | 148  |
          | 215  | LA 215 | 68.6032764977  | 149  |
          | 212  | LA 212 | 70.1397180629  | 150  |
          | 209  | LA 209 | 72.1669604003  | 151  |
          | 206  | LA 206 | 75.1120420820  | 152  |
          | 203  | LA 203 | 80.3722975665  | 153  |
          | 200  | LA 200 | 113.3032983918 | 154  |