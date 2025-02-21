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
          | 102  | LA 102 | 40.0312918279  | 0    |
          | 101  | LA 101 | 43.7717604530  | 1    |
          | 105  | LA 105 | 53.1408559476  | 2    |
          | 252  | LA 252 | 53.6770777604  | 3    |
          | 249  | LA 249 | 53.7686689440  | 4    |
          | 246  | LA 246 | 53.8619663731  | 5    |
          | 243  | LA 243 | 53.9570295662  | 6    |
          | 254  | LA 254 | 53.9973210666  | 7    |
          | 240  | LA 240 | 54.0539209819  | 8    |
          | 251  | LA 251 | 54.0959661603  | 9    |
          | 237  | LA 237 | 54.1527061938  | 10   |
          | 248  | LA 248 | 54.1966201506  | 11   |
          | 234  | LA 234 | 54.2534540759  | 12   |
          | 245  | LA 245 | 54.2993621225  | 13   |
          | 231  | LA 231 | 54.3562369981  | 14   |
          | 242  | LA 242 | 54.4042758096  | 15   |
          | 253  | LA 253 | 54.4183079294  | 16   |
          | 228  | LA 228 | 54.4611310325  | 17   |
          | 239  | LA 239 | 54.5114499584  | 18   |
          | 250  | LA 250 | 54.5261060170  | 19   |
          | 225  | LA 225 | 54.5682161681  | 20   |
          | 236  | LA 236 | 54.6209787291  | 21   |
          | 247  | LA 247 | 54.6363005769  | 22   |
          | 222  | LA 222 | 54.6775765355  | 23   |
          | 233  | LA 233 | 54.7329621367  | 24   |
          | 244  | LA 244 | 54.7489954197  | 25   |
          | 219  | LA 219 | 54.7893006389  | 26   |
          | 230  | LA 230 | 54.8475065362  | 27   |
          | 241  | LA 241 | 54.8643011342  | 28   |
          | 216  | LA 216 | 54.9034815936  | 29   |
          | 227  | LA 227 | 54.9647251593  | 30   |
          | 238  | LA 238 | 54.9823356853  | 31   |
          | 213  | LA 213 | 55.0202173655  | 32   |
          | 224  | LA 224 | 55.0847387065  | 33   |
          | 235  | LA 235 | 55.1032250795  | 34   |
          | 210  | LA 210 | 55.1396110095  | 35   |
          | 221  | LA 221 | 55.2076760041  | 36   |
          | 232  | LA 232 | 55.2271041055  | 37   |
          | 207  | LA 207 | 55.2617708995  | 38   |
          | 218  | LA 218 | 55.3336747326  | 39   |
          | 229  | LA 229 | 55.3541171631  | 40   |
          | 204  | LA 204 | 55.3868109419  | 41   |
          | 215  | LA 215 | 55.4628822366  | 42   |
          | 226  | LA 226 | 55.4844191905  | 43   |
          | 201  | LA 201 | 55.5148507606  | 44   |
          | 212  | LA 212 | 55.5954564268  | 45   |
          | 223  | LA 223 | 55.6181767065  | 46   |
          | 198  | LA 198 | 55.6460158378  | 47   |
          | 209  | LA 209 | 55.7315667866  | 48   |
          | 220  | LA 220 | 55.7555689829  | 49   |
          | 195  | LA 195 | 55.7804375877  | 50   |
          | 206  | LA 206 | 55.8713954981  | 51   |
          | 217  | LA 217 | 55.8967893700  | 52   |
          | 192  | LA 192 | 55.9182533334  | 53   |
          | 203  | LA 203 | 56.0151387022  | 54   |
          | 214  | LA 214 | 56.0420467961  | 55   |
          | 189  | LA 189 | 56.0596061450  | 56   |
          | 200  | LA 200 | 56.1630079149  | 57   |
          | 211  | LA 211 | 56.1915674723  | 58   |
          | 186  | LA 186 | 56.2046444828  | 59   |
          | 197  | LA 197 | 56.3152316184  | 60   |
          | 208  | LA 208 | 56.3455968354  | 61   |
          | 183  | LA 183 | 56.3535215663  | 62   |
          | 194  | LA 194 | 56.4720570549  | 63   |
          | 205  | LA 205 | 56.5044017695  | 64   |
          | 180  | LA 180 | 56.5063943622  | 65   |
          | 191  | LA 191 | 56.6337522516  | 66   |
          | 177  | LA 177 | 56.6634220413  | 67   |
          | 202  | LA 202 | 56.6682731572  | 68   |
          | 188  | LA 188 | 56.8006083097  | 69   |
          | 174  | LA 174 | 56.8247636960  | 70   |
          | 199  | LA 199 | 56.8375288192  | 71   |
          | 185  | LA 185 | 56.9729419977  | 72   |
          | 171  | LA 171 | 56.9905750241  | 73   |
          | 196  | LA 196 | 57.0125169153  | 74   |
          | 182  | LA 182 | 57.1510986908  | 75   |
          | 168  | LA 168 | 57.1610035658  | 76   |
          | 193  | LA 193 | 57.1936198970  | 77   |
          | 108  | LA 108 | 57.2761820214  | 78   |
          | 179  | LA 179 | 57.3354557080  | 79   |
          | 165  | LA 165 | 57.3361818975  | 80   |
          | 190  | LA 190 | 57.3812591213  | 81   |
          | 162  | LA 162 | 57.5162179277  | 82   |
          | 176  | LA 176 | 57.5264260998  | 83   |
          | 187  | LA 187 | 57.5759002634  | 84   |
          | 159  | LA 159 | 57.7011810407  | 85   |
          | 173  | LA 173 | 57.7244629472  | 86   |
          | 184  | LA 184 | 57.7780596990  | 87   |
          | 156  | LA 156 | 57.8910822361  | 88   |
          | 170  | LA 170 | 57.9300642312  | 89   |
          | 181  | LA 181 | 57.9883120708  | 90   |
          | 153  | LA 153 | 58.0858454820  | 91   |
          | 167  | LA 167 | 58.1437783336  | 92   |
          | 178  | LA 178 | 58.2072993144  | 93   |
          | 150  | LA 150 | 58.2852660375  | 94   |
          | 164  | LA 164 | 58.3662102118  | 95   |
          | 175  | LA 175 | 58.4357414904  | 96   |
          | 147  | LA 147 | 58.4889491557  | 97   |
          | 161  | LA 161 | 58.5980282657  | 98   |
          | 172  | LA 172 | 58.6744498754  | 99   |
          | 144  | LA 144 | 58.6962187233  | 100  |
          | 158  | LA 158 | 58.8399718510  | 101  |
          | 141  | LA 141 | 58.9059789099  | 102  |
          | 169  | LA 169 | 58.9243428974  | 103  |
          | 111  | LA 111 | 58.9613076022  | 104  |
          | 155  | LA 155 | 59.0928592777  | 105  |
          | 138  | LA 138 | 59.1165006772  | 106  |
          | 166  | LA 166 | 59.1864656896  | 107  |
          | 135  | LA 135 | 59.3250849479  | 108  |
          | 152  | LA 152 | 59.3575959187  | 109  |
          | 163  | LA 163 | 59.4620142895  | 110  |
          | 132  | LA 132 | 59.5275170902  | 111  |
          | 149  | LA 149 | 59.6351816604  | 112  |
          | 114  | LA 114 | 59.6970955538  | 113  |
          | 129  | LA 129 | 59.7171556479  | 114  |
          | 160  | LA 160 | 59.7523658699  | 115  |
          | 126  | LA 126 | 59.8833528430  | 116  |
          | 146  | LA 146 | 59.9267162099  | 117  |
          | 117  | LA 117 | 59.9931905700  | 118  |
          | 123  | LA 123 | 60.0085923082  | 119  |
          | 157  | LA 157 | 60.0591168908  | 120  |
          | 120  | LA 120 | 60.0630122926  | 121  |
          | 143  | LA 143 | 60.2333994582  | 122  |
          | 154  | LA 154 | 60.3841317918  | 123  |
          | 140  | LA 140 | 60.5565216462  | 124  |
          | 151  | LA 151 | 60.7296059061  | 125  |
          | 137  | LA 137 | 60.8974334164  | 126  |
          | 148  | LA 148 | 61.0981478697  | 127  |
          | 134  | LA 134 | 61.2574767340  | 128  |
          | 104  | LA 104 | 61.4750960385  | 129  |
          | 145  | LA 145 | 61.4928892194  | 130  |
          | 131  | LA 131 | 61.6378393040  | 131  |
          | 142  | LA 142 | 61.9176326567  | 132  |
          | 128  | LA 128 | 62.0392565646  | 133  |
          | 139  | LA 139 | 62.3770565102  | 134  |
          | 125  | LA 125 | 62.4614002972  | 135  |
          | 136  | LA 136 | 62.8770029387  | 136  |
          | 122  | LA 122 | 62.9015936462  | 137  |
          | 119  | LA 119 | 63.3519893507  | 138  |
          | 133  | LA 133 | 63.4248944994  | 139  |
          | 116  | LA 116 | 63.7929531758  | 140  |
          | 107  | LA 107 | 63.9981442299  | 141  |
          | 130  | LA 130 | 64.0303540628  | 142  |
          | 113  | LA 113 | 64.1760265937  | 143  |
          | 110  | LA 110 | 64.3737264204  | 144  |
          | 127  | LA 127 | 64.7061594766  | 145  |
          | 124  | LA 124 | 65.4697750342  | 146  |
          | 121  | LA 121 | 66.3459332348  | 147  |
          | 118  | LA 118 | 67.3712639762  | 148  |
          | 115  | LA 115 | 68.6032764977  | 149  |
          | 112  | LA 112 | 70.1397180629  | 150  |
          | 109  | LA 109 | 72.1669604003  | 151  |
          | 106  | LA 106 | 75.1120420820  | 152  |
          | 103  | LA 103 | 80.3722975665  | 153  |
          | 100  | LA 100 | 113.3032983918 | 154  |