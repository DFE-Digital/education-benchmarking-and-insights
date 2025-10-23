Feature: Local authorities schools endpoints

    Scenario: valid request with dimension Actuals
        Given a local authorities schools request with code '201' and query parameters:
            | Dimension |
            | Actuals   |
        When I submit the local authorities schools request
        Then the local authorities schools result should be ok and match the expected output of 'LaSchoolsFinanceActuals.json'

    Scenario: valid request with dimension percentage of income
        Given a local authorities schools request with code '201' and query parameters:
          | Dimension     |
          | PercentIncome |
        When I submit the local authorities schools request
        Then the local authorities schools result should be ok and match the expected output of 'LaSchoolsFinancePercentIncome.json'

    Scenario: valid request with dimension percentage of expenditure
        Given a local authorities schools request with code '201' and query parameters:
          | Dimension          |
          | PercentExpenditure |
        When I submit the local authorities schools request
        Then the local authorities schools result should be ok and match the expected output of 'LaSchoolsFinancePercentExpenditure.json'

    Scenario: valid request with dimension percentage of per unit
        Given a local authorities schools request with code '201' and query parameters:
          | Dimension |
          | PerUnit   |
        When I submit the local authorities schools request
        Then the local authorities schools result should be ok and match the expected output of 'LaSchoolsFinancePerUnit.json'

    Scenario: valid request filtered by NurseryProvision
        Given a local authorities schools request with code '201' and query parameters:
          | NurseryProvision    |
          | Has Nursery Classes |
        When I submit the local authorities schools request
        Then the local authorities schools result should be ok and match the expected output of 'LaSchoolsFinanceFilteredNursery.json'
        
    Scenario: valid request filtered by SixthFormProvision
        Given a local authorities schools request with code '334' and query parameters:
          | SixthFormProvision |
          | Has a sixth form   |
        When I submit the local authorities schools request
        Then the local authorities schools result should be ok and match the expected output of 'LaSchoolsFinanceFilteredSixthForm.json'

    Scenario: valid request filtered by SpecialClassProvision
        Given a local authorities schools request with code '334' and query parameters:
          | SpecialClassesProvision |
          | Has Special Classes     |
        When I submit the local authorities schools request
        Then the local authorities schools result should be ok and match the expected output of 'LaSchoolsFinanceFilteredSpecial.json'
        
    Scenario: valid request filtered by OverallPhase
        Given a local authorities schools request with code '334' and query parameters:
            | OverallPhase |
            | Secondary    |
        When I submit the local authorities schools request
        Then the local authorities schools result should be ok and match the expected output of 'LaSchoolsFinanceFilteredPhase.json'
        
    Scenario: valid request sorted as TotalExpenditure DESC 
        Given a local authorities schools request with code '201' and query parameters:
            | SortField        | SortOrder |
            | TotalExpenditure | DESC      |
        When I submit the local authorities schools request
        Then the local authorities schools result should be ok and match the expected output of 'LaSchoolsFinanceSortedTotalExpDesc.json'
        
    Scenario: invalid request with invalid limit
        Given a local authorities schools request with code '201' and query parameters:
            | Limit   |
            | invalid |
        When I submit the local authorities schools request
        Then the local authorities schools result should be bad request and match the expected output in 'LaSchoolsFinanceInvalidLimit.json'

    Scenario: invalid request with invalid dimension
        Given a local authorities schools request with code '201' and query parameters:
            | Dimension |
            | invalid   |
        When I submit the local authorities schools request
        Then the local authorities schools result should be bad request and match the expected output in 'LaSchoolsFinanceInvalidDimension.json'

    Scenario: invalid request with invalid NurseryProvision
    Given a local authorities schools request with code '201' and query parameters:
            | NurseryProvision |
            | invalid          |
        When I submit the local authorities schools request
        Then the local authorities schools result should be bad request and match the expected output in 'LaSchoolsFinanceInvalidNurseryProvision.json'

    Scenario: invalid request with invalid SixthFormProvision
    Given a local authorities schools request with code '201' and query parameters:
            | SixthFormProvision |
            | invalid            |
        When I submit the local authorities schools request
        Then the local authorities schools result should be bad request and match the expected output in 'LaSchoolsFinanceInvalidSixthFormProvision.json'

    Scenario: invalid request with invalid SpecialClassesProvision
    Given a local authorities schools request with code '201' and query parameters:
            | SpecialClassesProvision |
            | invalid                 |
        When I submit the local authorities schools request
        Then the local authorities schools result should be bad request and match the expected output in 'LaSchoolsFinanceInvalidSpecialProvision.json'

    Scenario: invalid request with invalid SortField
    Given a local authorities schools request with code '201' and query parameters:
            | SortField |
            | invalid   |
        When I submit the local authorities schools request
        Then the local authorities schools result should be bad request and match the expected output in 'LaSchoolsFinanceInvalidSortField.json'

    Scenario: invalid request with invalid SortOrder
    Given a local authorities schools request with code '201' and query parameters:
            | SortOrder |
            | invalid   |
        When I submit the local authorities schools request
        Then the local authorities schools result should be bad request and match the expected output in 'LaSchoolsFinanceInvalidSortOrder.json'

    Scenario: invalid request with invalid OverallPhase
    Given a local authorities schools request with code '201' and query parameters:
            | OverallPhase |
            | invalid      |
        When I submit the local authorities schools request
        Then the local authorities schools result should be bad request and match the expected output in 'LaSchoolsFinanceInvalidPhase.json'

    Scenario: valid request with non-existant code
        Given a local authorities schools request with code 'willNotBeFound' and query parameters:
            | Dimension |
            | Actuals   |
        When I submit the local authorities schools request
        Then the local authorities schools result should be not found
