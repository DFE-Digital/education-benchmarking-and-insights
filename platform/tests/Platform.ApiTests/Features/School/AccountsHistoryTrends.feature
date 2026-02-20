Feature: School Accounts History Trends

    Scenario: valid balance national average history request
        Given a balance national average history request with query parameters:
          | ParameterName    | ParameterValue    |
          | <ParameterName1> | <ParameterValue1> |
          | <ParameterName2> | <ParameterValue2> |
          | <ParameterName3> | <ParameterValue3> |
        When I submit the request
        Then the 'balance national average history' result should be 'ok' and match the expected output in '<Result>'

    Examples:
      | Result                                                                   | ParameterName1 | ParameterValue1    | ParameterName2 | ParameterValue2 | ParameterName3 | ParameterValue3 |
      | BalanceNationalAverageHistoryActualsPrimaryMaintained.json               | Dimension      | Actuals            | Phase          | Primary         | FinanceType    | Maintained      |
      | BalanceNationalAverageHistoryPercentIncomeSecondaryAcademy.json          | Dimension      | PercentIncome      | Phase          | Secondary       | FinanceType    | Academy         |
      | BalanceNationalAverageHistoryPercentExpenditureAllThroughMaintained.json | Dimension      | PercentExpenditure | Phase          | All-Through     | FinanceType    | Maintained      |
      | BalanceNationalAverageHistoryPercentIncomePost16Academy.json             | Dimension      | PerUnit            | Phase          | Post-16         | FinanceType    | Academy         |

    Scenario: invalid balance national average history request
        Given a balance national average history request with query parameters:
          | ParameterName    | ParameterValue    |
          | <ParameterName1> | <ParameterValue1> |
          | <ParameterName2> | <ParameterValue2> |
          | <ParameterName3> | <ParameterValue3> |
        When I submit the request
        Then the 'balance national average history' result should be 'bad request' and match the expected output in '<Result>'

    Examples:
      | Result                                               | ParameterName1 | ParameterValue1    | ParameterName2 | ParameterValue2 | ParameterName3 | ParameterValue3 |
      | BalanceNationalAverageHistoryInvalidDimension.json   | Dimension      | Invalid            | Phase          | Primary         | FinanceType    | Maintained      |
      | BalanceNationalAverageHistoryInvalidPhase.json       | Dimension      | PercentIncome      | Phase          | Invalid         | FinanceType    | Academy         |
      | BalanceNationalAverageHistoryInvalidFinanceType.json | Dimension      | PercentExpenditure | Phase          | All-Through     | FinanceType    | Invalid         |
      
    Scenario: valid income national average history request
        Given an income national average history request with query parameters:
          | ParameterName    | ParameterValue    |
          | <ParameterName1> | <ParameterValue1> |
          | <ParameterName2> | <ParameterValue2> |
          | <ParameterName3> | <ParameterValue3> |
        When I submit the request
        Then the 'income national average history' result should be 'ok' and match the expected output in '<Result>'

    Examples:
      | Result                                                                  | ParameterName1 | ParameterValue1    | ParameterName2 | ParameterValue2 | ParameterName3 | ParameterValue3 |
      | IncomeNationalAverageHistoryActualsPrimaryMaintained.json               | Dimension      | Actuals            | Phase          | Primary         | FinanceType    | Maintained      |
      | IncomeNationalAverageHistoryPercentIncomeSecondaryAcademy.json          | Dimension      | PercentIncome      | Phase          | Secondary       | FinanceType    | Academy         |
      | IncomeNationalAverageHistoryPercentExpenditureAllThroughMaintained.json | Dimension      | PercentExpenditure | Phase          | All-Through     | FinanceType    | Maintained      |
      | IncomeNationalAverageHistoryPercentIncomePost16Academy.json             | Dimension      | PerUnit            | Phase          | Post-16         | FinanceType    | Academy         |

    Scenario: invalid income national average history request
        Given an income national average history request with query parameters:
          | ParameterName    | ParameterValue    |
          | <ParameterName1> | <ParameterValue1> |
          | <ParameterName2> | <ParameterValue2> |
          | <ParameterName3> | <ParameterValue3> |
        When I submit the request
        Then the 'income national average history' result should be 'bad request' and match the expected output in '<Result>'

    Examples:
      | Result                                              | ParameterName1 | ParameterValue1    | ParameterName2 | ParameterValue2 | ParameterName3 | ParameterValue3 |
      | IncomeNationalAverageHistoryInvalidDimension.json   | Dimension      | Invalid            | Phase          | Primary         | FinanceType    | Maintained      |
      | IncomeNationalAverageHistoryInvalidPhase.json       | Dimension      | PercentIncome      | Phase          | Invalid         | FinanceType    | Academy         |
      | IncomeNationalAverageHistoryInvalidFinanceType.json | Dimension      | PercentExpenditure | Phase          | All-Through     | FinanceType    | Invalid         |
      
    Scenario: valid balance comparator set average history request
        Given a balance comparator set average history request with URN '777042' and dimension '<Dimension>'
        When I submit the request
        Then the 'balance comparator set average history' result should be 'ok' and match the expected output in '<Result>'

    Examples:
      | Result                                          | Dimension          |
      | BalanceSetAverageHistoryActuals.json            | Actuals            |
      | BalanceSetAverageHistoryPercentExpenditure.json | PercentExpenditure |
      | BalanceSetAverageHistoryPercentIncome.json      | PercentIncome      |
      | BalanceSetAverageHistoryPerUnit.json            | PerUnit            |

    Scenario: invalid balance comparator set average history request
        Given a balance comparator set average history request with URN '777042' and dimension 'Invalid'
        When I submit the request
        Then the 'balance comparator set average history' result should be 'bad request' and match the expected output in 'BalanceSetAverageHistoryInvalidDimension.json'
      
    Scenario: valid income comparator set average history request
        Given an income comparator set average history request with URN '777042' and dimension '<Dimension>'
        When I submit the request
        Then the 'income comparator set average history' result should be 'ok' and match the expected output in '<Result>'

    Examples:
      | Result                                         | Dimension          |
      | IncomeSetAverageHistoryActuals.json            | Actuals            |
      | IncomeSetAverageHistoryPercentExpenditure.json | PercentExpenditure |
      | IncomeSetAverageHistoryPercentIncome.json      | PercentIncome      |
      | IncomeSetAverageHistoryPerUnit.json            | PerUnit            |

    Scenario: invalid income comparator set average history request
        Given an income comparator set average history request with URN '777042' and dimension 'Invalid'
        When I submit the request
        Then the 'income comparator set average history' result should be 'bad request' and match the expected output in 'IncomeSetAverageHistoryInvalidDimension.json'