Feature: Local Authority Maintained Schools

    Scenario: valid finance summary request
        Given a finance summary request with code '<Code>' and query parameters:
          | ParameterName    | ParameterValue    |
          | <ParameterName1> | <ParameterValue1> |
          | <ParameterName2> | <ParameterValue2> |
        When I submit the request
        Then the result should be ok and match the expected output of '<Result>'

    Examples:
      | Code | Result                                  | ParameterName1          | ParameterValue1     | ParameterName2 | ParameterValue2 |
      | 201  | LaSchoolsFinanceActuals.json            | Dimension               | Actuals             |                |                 |
      | 201  | LaSchoolsFinancePercentIncome.json      | Dimension               | PercentIncome       |                |                 |
      | 201  | LaSchoolsFinancePercentExpenditure.json | Dimension               | PercentExpenditure  |                |                 |
      | 201  | LaSchoolsFinancePerUnit.json            | Dimension               | PerUnit             |                |                 |
      | 201  | LaSchoolsFinanceFilteredNursery.json    | NurseryProvision        | Has Nursery Classes |                |                 |
      | 334  | LaSchoolsFinanceFilteredSixthForm.json  | SixthFormProvision      | Has a sixth form    |                |                 |
      | 334  | LaSchoolsFinanceFilteredSpecial.json    | SpecialClassesProvision | Has Special Classes |                |                 |
      | 334  | LaSchoolsFinanceFilteredPhase.json      | OverallPhase            | Secondary           |                |                 |
      | 201  | LaSchoolsFinanceSortedTotalExpDesc.json | SortField               | TotalExpenditure    | SortOrder      |  DESC           |
        

    Scenario: invalid finance summary request
        Given a finance summary request with code '<Code>' and query parameters:
          | ParameterName    | ParameterValue    |
          | <ParameterName1> | <ParameterValue1> |
        When I submit the request
        Then the result should be bad request and match the expected output in '<Result>'

    Examples:
      | Code | Result                                         | ParameterName1          | ParameterValue1 |  
      | 201  | LaSchoolsFinanceInvalidLimit.json              | Limit                   | invalid         |
      | 201  | LaSchoolsFinanceInvalidDimension.json          | Dimension               | invalid         |
      | 201  | LaSchoolsFinanceInvalidNurseryProvision.json   | NurseryProvision        | invalid         |
      | 201  | LaSchoolsFinanceInvalidSixthFormProvision.json | SixthFormProvision      | invalid         |
      | 201  | LaSchoolsFinanceInvalidSpecialProvision.json   | SpecialClassesProvision | invalid         |
      | 201  | LaSchoolsFinanceInvalidSortField.json          | SortField               | invalid         |
      | 201  | LaSchoolsFinanceInvalidSortOrder.json          | SortOrder               | invalid         |
      | 201  | LaSchoolsFinanceInvalidPhase.json              | OverallPhase            | invalid         |                     
      
    Scenario: non found finance summary request 
        Given a finance summary request with code 'willNotBeFound'
        When I submit the request
        Then the result should be not found
      
    Scenario: valid workforce summary request
        Given a workforce summary request with code '<Code>' and query parameters:
          | ParameterName    | ParameterValue    |
          | <ParameterName1> | <ParameterValue1> |
          | <ParameterName2> | <ParameterValue2> |
        When I submit the request
        Then the result should be ok and match the expected output of '<Result>'

    Examples:
      | Code | Result                                             | ParameterName1          | ParameterValue1     | ParameterName2 | ParameterValue2 |
      | 201  | LaSchoolsWorkforceActuals.json                     | Dimension               | Actuals             |                |                 |
      | 201  | LaSchoolsWorkforcePercentPupil.json                | Dimension               | PercentPupil        |                |                 |
      | 201  | LaSchoolsWorkforceFilteredNursery.json             | NurseryProvision        | Has Nursery Classes |                |                 |
      | 334  | LaSchoolsWorkforceFilteredSixthForm.json           | SixthFormProvision      | Has a sixth form    |                |                 |
      | 334  | LaSchoolsWorkforceFilteredSpecial.json             | SpecialClassesProvision | Has Special Classes |                |                 |
      | 334  | LaSchoolsWorkforceFilteredPhase.json               | OverallPhase            | Secondary           |                |                 |
      | 201  | LaSchoolsWorkforceSortedPupilTeacherRatioDesc.json | SortField               | PupilTeacherRatio   | SortOrder      | DESC            |

    Scenario: invalid workforce summary request
        Given a workforce summary request with code '<Code>' and query parameters:
          | ParameterName    | ParameterValue    |
          | <ParameterName1> | <ParameterValue1> |
        When I submit the request
        Then the result should be bad request and match the expected output in '<Result>'

    Examples:
      | Code | Result                                           | ParameterName1          | ParameterValue1 |  
      | 201  | LaSchoolsWorkforceInvalidLimit.json              | Limit                   | invalid         |
      | 201  | LaSchoolsWorkforceInvalidDimension.json          | Dimension               | invalid         |
      | 201  | LaSchoolsWorkforceInvalidNurseryProvision.json   | NurseryProvision        | invalid         |
      | 201  | LaSchoolsWorkforceInvalidSixthFormProvision.json | SixthFormProvision      | invalid         |
      | 201  | LaSchoolsWorkforceInvalidSpecialProvision.json   | SpecialClassesProvision | invalid         |
      | 201  | LaSchoolsWorkforceInvalidSortField.json          | SortField               | invalid         |
      | 201  | LaSchoolsWorkforceInvalidSortOrder.json          | SortOrder               | invalid         |
      | 201  | LaSchoolsWorkforceInvalidPhase.json              | OverallPhase            | invalid         |

    Scenario: non found workforce summary request 
        Given a workforce summary request with code 'willNotBeFound'
        When I submit the request
        Then the result should be not found