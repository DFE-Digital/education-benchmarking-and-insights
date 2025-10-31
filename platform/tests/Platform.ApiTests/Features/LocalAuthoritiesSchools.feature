Feature: Local authorities schools endpoints

    Scenario: valid finance summary request with dimension Actuals
        Given a local authorities schools finance summary request with code '201' and query parameters:
            | Dimension |
            | Actuals   |
        When I submit the local authorities schools request
        Then the local authorities schools result should be ok and match the expected output of 'LaSchoolsFinanceActuals.json'

    Scenario: valid finance summary request with dimension percentage of income
        Given a local authorities schools finance summary request with code '201' and query parameters:
          | Dimension     |
          | PercentIncome |
        When I submit the local authorities schools request
        Then the local authorities schools result should be ok and match the expected output of 'LaSchoolsFinancePercentIncome.json'

    Scenario: valid finance summary request with dimension percentage of expenditure
        Given a local authorities schools finance summary request with code '201' and query parameters:
          | Dimension          |
          | PercentExpenditure |
        When I submit the local authorities schools request
        Then the local authorities schools result should be ok and match the expected output of 'LaSchoolsFinancePercentExpenditure.json'

    Scenario: valid finance summary request with dimension percentage of per unit
        Given a local authorities schools finance summary request with code '201' and query parameters:
          | Dimension |
          | PerUnit   |
        When I submit the local authorities schools request
        Then the local authorities schools result should be ok and match the expected output of 'LaSchoolsFinancePerUnit.json'

    Scenario: valid finance summary request filtered by NurseryProvision
        Given a local authorities schools finance summary request with code '201' and query parameters:
          | NurseryProvision    |
          | Has Nursery Classes |
        When I submit the local authorities schools request
        Then the local authorities schools result should be ok and match the expected output of 'LaSchoolsFinanceFilteredNursery.json'
        
    Scenario: valid finance summary request filtered by SixthFormProvision
        Given a local authorities schools finance summary request with code '334' and query parameters:
          | SixthFormProvision |
          | Has a sixth form   |
        When I submit the local authorities schools request
        Then the local authorities schools result should be ok and match the expected output of 'LaSchoolsFinanceFilteredSixthForm.json'

    Scenario: valid finance summary request filtered by SpecialClassProvision
        Given a local authorities schools finance summary request with code '334' and query parameters:
          | SpecialClassesProvision |
          | Has Special Classes     |
        When I submit the local authorities schools request
        Then the local authorities schools result should be ok and match the expected output of 'LaSchoolsFinanceFilteredSpecial.json'
        
    Scenario: valid finance summary request filtered by OverallPhase
        Given a local authorities schools finance summary request with code '334' and query parameters:
            | OverallPhase |
            | Secondary    |
        When I submit the local authorities schools request
        Then the local authorities schools result should be ok and match the expected output of 'LaSchoolsFinanceFilteredPhase.json'
        
    Scenario: valid finance summary request sorted as TotalExpenditure DESC 
        Given a local authorities schools finance summary request with code '201' and query parameters:
            | SortField        | SortOrder |
            | TotalExpenditure | DESC      |
        When I submit the local authorities schools request
        Then the local authorities schools result should be ok and match the expected output of 'LaSchoolsFinanceSortedTotalExpDesc.json'
        
    Scenario: invalid finance summary request with invalid limit
        Given a local authorities schools finance summary request with code '201' and query parameters:
            | Limit   |
            | invalid |
        When I submit the local authorities schools request
        Then the local authorities schools result should be bad request and match the expected output in 'LaSchoolsFinanceInvalidLimit.json'

    Scenario: invalid finance summary request with invalid dimension
        Given a local authorities schools finance summary request with code '201' and query parameters:
            | Dimension |
            | invalid   |
        When I submit the local authorities schools request
        Then the local authorities schools result should be bad request and match the expected output in 'LaSchoolsFinanceInvalidDimension.json'

    Scenario: invalid finance summary request with invalid NurseryProvision
    Given a local authorities schools finance summary request with code '201' and query parameters:
            | NurseryProvision |
            | invalid          |
        When I submit the local authorities schools request
        Then the local authorities schools result should be bad request and match the expected output in 'LaSchoolsFinanceInvalidNurseryProvision.json'

    Scenario: invalid finance summary request with invalid SixthFormProvision
    Given a local authorities schools finance summary request with code '201' and query parameters:
            | SixthFormProvision |
            | invalid            |
        When I submit the local authorities schools request
        Then the local authorities schools result should be bad request and match the expected output in 'LaSchoolsFinanceInvalidSixthFormProvision.json'

    Scenario: invalid finance summary request with invalid SpecialClassesProvision
    Given a local authorities schools finance summary request with code '201' and query parameters:
            | SpecialClassesProvision |
            | invalid                 |
        When I submit the local authorities schools request
        Then the local authorities schools result should be bad request and match the expected output in 'LaSchoolsFinanceInvalidSpecialProvision.json'

    Scenario: invalid finance summary request with invalid SortField
    Given a local authorities schools finance summary request with code '201' and query parameters:
            | SortField |
            | invalid   |
        When I submit the local authorities schools request
        Then the local authorities schools result should be bad request and match the expected output in 'LaSchoolsFinanceInvalidSortField.json'

    Scenario: invalid finance summary request with invalid SortOrder
    Given a local authorities schools finance summary request with code '201' and query parameters:
            | SortOrder |
            | invalid   |
        When I submit the local authorities schools request
        Then the local authorities schools result should be bad request and match the expected output in 'LaSchoolsFinanceInvalidSortOrder.json'

    Scenario: invalid finance summary request with invalid OverallPhase
    Given a local authorities schools finance summary request with code '201' and query parameters:
            | OverallPhase |
            | invalid      |
        When I submit the local authorities schools request
        Then the local authorities schools result should be bad request and match the expected output in 'LaSchoolsFinanceInvalidPhase.json'

    Scenario: valid finance summary request with non-existant code
        Given a local authorities schools finance summary request with code 'willNotBeFound' and query parameters:
            | Dimension |
            | Actuals   |
        When I submit the local authorities schools request
        Then the local authorities schools result should be not found
        
    Scenario: valid workforce summary request with dimension Actuals
        Given a local authorities schools workforce summary request with code '201' and query parameters:
            | Dimension |
            | Actuals   |
        When I submit the local authorities schools request
        Then the local authorities schools result should be ok and match the expected output of 'LaSchoolsWorkforceActuals.json'

    Scenario: valid workforce summary request with dimension percentage of pupil
        Given a local authorities schools workforce summary request with code '201' and query parameters:
          | Dimension     |
          | PercentPupil |
        When I submit the local authorities schools request
        Then the local authorities schools result should be ok and match the expected output of 'LaSchoolsWorkforcePercentPupil.json'

    Scenario: valid workforce summary request filtered by NurseryProvision
        Given a local authorities schools workforce summary request with code '201' and query parameters:
          | NurseryProvision    |
          | Has Nursery Classes |
        When I submit the local authorities schools request
        Then the local authorities schools result should be ok and match the expected output of 'LaSchoolsWorkforceFilteredNursery.json'
        
    Scenario: valid workforce summary request filtered by SixthFormProvision
        Given a local authorities schools workforce summary request with code '334' and query parameters:
          | SixthFormProvision |
          | Has a sixth form   |
        When I submit the local authorities schools request
        Then the local authorities schools result should be ok and match the expected output of 'LaSchoolsWorkforceFilteredSixthForm.json'

    Scenario: valid workforce summary request filtered by SpecialClassProvision
        Given a local authorities schools workforce summary request with code '334' and query parameters:
          | SpecialClassesProvision |
          | Has Special Classes     |
        When I submit the local authorities schools request
        Then the local authorities schools result should be ok and match the expected output of 'LaSchoolsWorkforceFilteredSpecial.json'
        
    Scenario: valid workforce summary request filtered by OverallPhase
        Given a local authorities schools workforce summary request with code '334' and query parameters:
            | OverallPhase |
            | Secondary    |
        When I submit the local authorities schools request
        Then the local authorities schools result should be ok and match the expected output of 'LaSchoolsWorkforceFilteredPhase.json'
        
    Scenario: valid workforce summary request sorted as PupilTeacherRatio DESC 
        Given a local authorities schools workforce summary request with code '201' and query parameters:
            | SortField        | SortOrder |
            | PupilTeacherRatio | DESC      |
        When I submit the local authorities schools request
        Then the local authorities schools result should be ok and match the expected output of 'LaSchoolsWorkforceSortedPupilTeacherRatioDesc.json'
        
    Scenario: invalid workforce summary request with invalid limit
        Given a local authorities schools workforce summary request with code '201' and query parameters:
            | Limit   |
            | invalid |
        When I submit the local authorities schools request
        Then the local authorities schools result should be bad request and match the expected output in 'LaSchoolsWorkforceInvalidLimit.json'

    Scenario: invalid workforce summary request with invalid dimension
        Given a local authorities schools workforce summary request with code '201' and query parameters:
            | Dimension |
            | invalid   |
        When I submit the local authorities schools request
        Then the local authorities schools result should be bad request and match the expected output in 'LaSchoolsWorkforceInvalidDimension.json'

    Scenario: invalid workforce summary request with invalid NurseryProvision
    Given a local authorities schools workforce summary request with code '201' and query parameters:
            | NurseryProvision |
            | invalid          |
        When I submit the local authorities schools request
        Then the local authorities schools result should be bad request and match the expected output in 'LaSchoolsWorkforceInvalidNurseryProvision.json'

    Scenario: invalid workforce summary request with invalid SixthFormProvision
    Given a local authorities schools workforce summary request with code '201' and query parameters:
            | SixthFormProvision |
            | invalid            |
        When I submit the local authorities schools request
        Then the local authorities schools result should be bad request and match the expected output in 'LaSchoolsWorkforceInvalidSixthFormProvision.json'

    Scenario: invalid workforce summary request with invalid SpecialClassesProvision
    Given a local authorities schools workforce summary request with code '201' and query parameters:
            | SpecialClassesProvision |
            | invalid                 |
        When I submit the local authorities schools request
        Then the local authorities schools result should be bad request and match the expected output in 'LaSchoolsWorkforceInvalidSpecialProvision.json'

    Scenario: invalid workforce summary request with invalid SortField
    Given a local authorities schools workforce summary request with code '201' and query parameters:
            | SortField |
            | invalid   |
        When I submit the local authorities schools request
        Then the local authorities schools result should be bad request and match the expected output in 'LaSchoolsWorkforceInvalidSortField.json'

    Scenario: invalid workforce summary request with invalid SortOrder
    Given a local authorities schools workforce summary request with code '201' and query parameters:
            | SortOrder |
            | invalid   |
        When I submit the local authorities schools request
        Then the local authorities schools result should be bad request and match the expected output in 'LaSchoolsWorkforceInvalidSortOrder.json'

    Scenario: invalid workforce summary request with invalid OverallPhase
    Given a local authorities schools workforce summary request with code '201' and query parameters:
            | OverallPhase |
            | invalid      |
        When I submit the local authorities schools request
        Then the local authorities schools result should be bad request and match the expected output in 'LaSchoolsWorkforceInvalidPhase.json'

    Scenario: valid workforce summary request with non-existant code
        Given a local authorities schools workforce summary request with code 'willNotBeFound' and query parameters:
            | Dimension |
            | Actuals   |
        When I submit the local authorities schools request
        Then the local authorities schools result should be not found