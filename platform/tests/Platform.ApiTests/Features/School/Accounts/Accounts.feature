Feature: School Accounts

    Scenario: valid IT spending request
        Given an IT spending request with dimension '<Dimension>'
        When I submit the request
        Then the 'it spending' result should be 'ok' and match the expected output in '<Result>'

    Examples:
      | Result                            | Dimension          |
      | ItSpendingActuals.json            | Actuals            |
      | ItSpendingPercentExpenditure.json | PercentExpenditure |

    Scenario: valid IT spending request without dimension
        Given an IT spending request without dimension
        When I submit the request
        Then the 'it spending' result should be 'ok' and match the expected output in 'ItSpendingNoDimension.json'

    Scenario: invalid IT spending request
        Given an IT spending request with dimension 'Invalid'
        When I submit the request
        Then the 'it spending' result should be 'bad request' and match the expected output in 'ItSpendingInvalidDimension.json'

    Scenario: valid income request
        Given an income request with URN '777042'
        When I submit the request
        Then the 'income' result should be 'ok' and match the expected output in 'Income.json'

    Scenario: not found income request
        Given an income request with URN '000000'
        When I submit the request
        Then the 'income' result should be 'not found' and match the expected output in 'IncomeNotFound.json'

    Scenario: valid income history request
        Given an income history request with URN '777042' and dimension '<Dimension>'
        When I submit the request
        Then the 'income history' result should be 'ok' and match the expected output in '<Result>'

    Examples:
      | Result                             | Dimension          |
      | IncomeHistoryActuals.json          | Actuals            |
      | IncomeHistoryPercentIncome.json    | PercentIncome      |

    Scenario: invalid income history request
        Given an income history request with URN '777042' and dimension 'Invalid'
        When I submit the request
        Then the 'income history' result should be 'bad request' and match the expected output in 'IncomeHistoryInvalidDimension.json'

    Scenario: not found income history request
        Given an income history request with URN '000000' and dimension 'Actuals'
        When I submit the request
        Then the 'income history' result should be 'not found' and match the expected output in 'IncomeHistoryNotFound.json'

    Scenario: valid balance request
        Given a balance request with URN '777042'
        When I submit the request
        Then the 'balance' result should be 'ok' and match the expected output in 'Balance.json'

    Scenario: not found balance request
        Given a balance request with URN '000000'
        When I submit the request
        Then the 'balance' result should be 'not found' and match the expected output in 'BalanceNotFound.json'

    Scenario: valid balance history request
        Given a balance history request with URN '777042'
        When I submit the request
        Then the 'balance history' result should be 'ok' and match the expected output in 'BalanceHistory.json'

    Scenario: not found balance history request
        Given a balance history request with URN '000000'
        When I submit the request
        Then the 'balance history' result should be 'not found' and match the expected output in 'BalanceHistoryNotFound.json'

    Scenario: valid expenditure request with query parameters
        Given an expenditure request with query parameters:
          | ParameterName    | ParameterValue    |
          | <ParameterName1> | <ParameterValue1> |
          | <ParameterName2> | <ParameterValue2> |
          | <ParameterName3> | <ParameterValue3> |
        When I submit the request
        Then the 'expenditure' result should be 'ok' and match the expected output in '<Result>'

    Examples:
      | Result                        | ParameterName1 | ParameterValue1 | ParameterName2 | ParameterValue2 | ParameterName3 | ParameterValue3 |
      | ExpenditureUrns.json          | Urns           | 777042          | Dimension      | Actuals         | Category       |                 |
      | ExpenditureCompanyNumber.json | CompanyNumber  | 12345678        | Phase          | Primary         | Dimension      | Actuals         |
      | ExpenditureLaCode.json        | LaCode         | 111             | Phase          | Secondary       | Dimension      | Actuals         |

    Scenario: invalid expenditure request with query parameters
        Given an expenditure request with query parameters:
          | ParameterName    | ParameterValue    |
          | <ParameterName1> | <ParameterValue1> |
          | <ParameterName2> | <ParameterValue2> |
          | <ParameterName3> | <ParameterValue3> |
        When I submit the request
        Then the 'expenditure' result should be 'bad request' and match the expected output in '<Result>'

    Examples:
      | Result                             | ParameterName1 | ParameterValue1 | ParameterName2 | ParameterValue2 | ParameterName3 | ParameterValue3 |
      | ExpenditureMissingRequired.json    | Dimension      | Actuals         | Category       |                 | Phase          |                 |
      | ExpenditureBothCoAndLa.json        | CompanyNumber  | 12345678        | LaCode         | 111             | Phase          | Primary         |
      | ExpenditureInvalidCategory.json    | Urns           | 777042          | Category       | Invalid         | Dimension      | Actuals         |

    Scenario: valid school expenditure request
        Given a school expenditure request with URN '777042', category '<Category>', and dimension '<Dimension>'
        When I submit the request
        Then the 'school expenditure' result should be 'ok' and match the expected output in '<Result>'

    Examples:
      | Result                                         | Category                       | Dimension          |
      | SchoolExpenditureTeachingStaffActuals.json     | TeachingTeachingSupportStaff   | Actuals            |
      | SchoolExpenditureTotalExpenditurePerUnit.json  | TotalExpenditure               | PerUnit            |

    Scenario: invalid school expenditure request
        Given a school expenditure request with URN '777042', category 'Invalid', and dimension 'Actuals'
        When I submit the request
        Then the 'school expenditure' result should be 'bad request' and match the expected output in 'SchoolExpenditureInvalidCategory.json'

    Scenario: not found school expenditure request
        Given a school expenditure request with URN '000000', category 'TeachingTeachingSupportStaff', and dimension 'Actuals'
        When I submit the request
        Then the 'school expenditure' result should be 'not found' and match the expected output in 'SchoolExpenditureNotFound.json'

    Scenario: valid school expenditure history request
        Given a school expenditure history request with URN '777042', category '<Category>', and dimension '<Dimension>'
        When I submit the request
        Then the 'school expenditure history' result should be 'ok' and match the expected output in '<Result>'

    Examples:
      | Result                                                | Category                       | Dimension          |
      | SchoolExpenditureHistoryTeachingStaffActuals.json     | TeachingTeachingSupportStaff   | Actuals            |
      | SchoolExpenditureHistoryTotalExpenditurePerUnit.json  | TotalExpenditure               | PerUnit            |

    Scenario: invalid school expenditure history request
        Given a school expenditure history request with URN '777042', category 'Invalid', and dimension 'Actuals'
        When I submit the request
        Then the 'school expenditure history' result should be 'bad request' and match the expected output in 'SchoolExpenditureHistoryInvalidCategory.json'

    Scenario: not found school expenditure history request
        Given a school expenditure history request with URN '000000', category 'TeachingTeachingSupportStaff', and dimension 'Actuals'
        When I submit the request
        Then the 'school expenditure history' result should be 'not found' and match the expected output in 'SchoolExpenditureHistoryNotFound.json'

    Scenario: valid expenditure comparator set average history request
        Given an expenditure comparator set average history request with URN '777042', category '<Category>', and dimension '<Dimension>'
        When I submit the request
        Then the 'expenditure comparator set average history' result should be 'ok' and match the expected output in '<Result>'

    Examples:
      | Result                                                     | Category                       | Dimension          |
      | ExpenditureSetAverageHistoryTeachingStaffActuals.json      | TeachingTeachingSupportStaff   | Actuals            |
      | ExpenditureSetAverageHistoryTotalExpenditurePerUnit.json   | TotalExpenditure               | PerUnit            |

    Scenario: invalid expenditure comparator set average history request
        Given an expenditure comparator set average history request with URN '777042', category 'Invalid', and dimension 'Actuals'
        When I submit the request
        Then the 'expenditure comparator set average history' result should be 'bad request' and match the expected output in 'ExpenditureSetAverageHistoryInvalidCategory.json'

    Scenario: not found expenditure comparator set average history request
        Given an expenditure comparator set average history request with URN '000000', category 'TeachingTeachingSupportStaff', and dimension 'Actuals'
        When I submit the request
        Then the 'expenditure comparator set average history' result should be 'not found' and match the expected output in 'ExpenditureSetAverageHistoryNotFound.json'

    Scenario: valid expenditure national average history request
        Given an expenditure national average history request with query parameters:
          | ParameterName    | ParameterValue    |
          | <ParameterName1> | <ParameterValue1> |
          | <ParameterName2> | <ParameterValue2> |
          | <ParameterName3> | <ParameterValue3> |
          | <ParameterName4> | <ParameterValue4> |
        When I submit the request
        Then the 'expenditure national average history' result should be 'ok' and match the expected output in '<Result>'

    Examples:
      | Result                                                  | ParameterName1 | ParameterValue1 | ParameterName2 | ParameterValue2 | ParameterName3 | ParameterValue3 | ParameterName4 | ParameterValue4                |
      | ExpenditureNationalAverageHistoryActualsPrimary.json    | Dimension      | Actuals         | Phase          | Primary         | FinanceType    | Maintained      | Category       | TeachingTeachingSupportStaff   |

    Scenario: invalid expenditure national average history request
        Given an expenditure national average history request with query parameters:
          | ParameterName    | ParameterValue    |
          | <ParameterName1> | <ParameterValue1> |
          | <ParameterName2> | <ParameterValue2> |
          | <ParameterName3> | <ParameterValue3> |
          | <ParameterName4> | <ParameterValue4> |
        When I submit the request
        Then the 'expenditure national average history' result should be 'bad request' and match the expected output in '<Result>'

    Examples:
      | Result                                              | ParameterName1 | ParameterValue1 | ParameterName2 | ParameterValue2 | ParameterName3 | ParameterValue3 | ParameterName4 | ParameterValue4                |
      | ExpenditureNationalAverageHistoryInvalidPhase.json  | Dimension      | Actuals         | Phase          | Invalid         | FinanceType    | Maintained      | Category       | TeachingTeachingSupportStaff   |
