Feature: School historic data

    Scenario Outline: Change dimension of history data
        Given I am on '<tab>' history page for school with URN '777042'
        When I change '<tab>' dimension to '<dimension>'
        Then the '<tab>' dimension is '<dimension>'

        Examples:
          | tab      | dimension         |
          | spending | £ per pupil       |
          | income   | £ per pupil       |
          | balance  | £ per pupil       |
          | census   | headcount per FTE |

    Scenario Outline: Show all should expand all sections
        Given I am on '<tab>' history page for school with URN '777042'
        When I click on show all sections on '<tab>'
        Then all sections on '<tab>' tab are expanded
        And the show all text changes to hide all sections on '<tab>'
        And all '<tab>' sub categories are displayed on the page

        Examples:
          | tab      |
          | spending |
          | income   |

    Scenario Outline: Expected number of charts are displayed
        Given I am on '<tab>' history page for school with URN '777042'
        And all sections are shown on '<tab>'
        Then there should be '<charts>' charts displayed on '<tab>'
        And there should be '<warnings>' warnings displayed on '<tab>'

        Examples:
          | tab      | charts | warnings |
          | spending | 33     | 9        |
          | income   | 11     | 6        |
          | balance  | 2      | 0        |
          | census   | 8      | 1        |

    Scenario Outline: Change all charts to table view
        Given I am on '<tab>' history page for school with URN '777042'
        And all sections are shown on '<tab>'
        When I click on view as table on '<tab>' tab
        Then are showing table view on '<tab>' tab

        Examples:
          | tab      |
          | spending |
          | income   |
          | balance  |
          | census   |

    Scenario: Hide single section
        Given I am on 'spending' history page for school with URN '777042'
        And all sections are shown on 'spending'
        When I click section link for 'non educational support staff'
        Then the section 'non educational support staff' is hidden

    Scenario Outline: Change dimension of history data sets legend
        Given I am on '<tab>' history page for school with URN '777042'
        And all sections are shown on '<tab>'
        When I change '<tab>' dimension to '<dimension>'
        Then the '<tab>' dimension is '<dimension>'
        And the '<tab>' tab '<chart>' chart shows the legend '<legend>' using separator ','

        Examples:
          | tab      | dimension         | chart                                   | legend                                                                               |
          | spending | actuals           | Total expenditure                       | national average across phase type, average across comparator set, actuals           |
          | spending | £ per pupil       | Total expenditure                       | national average across phase type, average across comparator set, £ per pupil       |
          | spending | £ per pupil       | Total premises staff and services costs | national average across phase type, average across comparator set, £ per m²          |
          | income   | £ per pupil       | Total income                            |                                                                                      |
          | balance  | £ per pupil       | In-year balance                         |                                                                                      |
          | census   | total             | Pupils on roll                          | national average across phase type, average across comparator set, total             |
          | census   | headcount per FTE | Pupils on roll                          | national average across phase type, average across comparator set, total             |
          | census   | headcount per FTE | School workforce (full time equivalent) | national average across phase type, average across comparator set, headcount per FTE |

    Scenario: Change Total expenditure chart to table view
        Given I am on 'spending' history page for school with URN '777042'
        When I change 'spending' dimension to 'actuals'
        And I click on view as table on 'spending' tab
        Then the table on the 'spending' tab 'Total expenditure' chart contains:
          | Year         | Amount     | Average across comparator set | National average across phase type |
          | 2017 to 2018 |            |                               |                                    |
          | 2018 to 2019 |            |                               |                                    |
          | 2019 to 2020 |            |                               |                                    |
          | 2020 to 2021 | £3,157,259 |                               | £3,128,083                         |
          | 2021 to 2022 | £1,587,223 | £2,929,445                    | £3,586,859                         |

    Scenario: Change Total expenditure chart to table view when dimension set to per unit
        Given I am on 'spending' history page for school with URN '777042'
        When I change 'spending' dimension to '£ per pupil'
        And I click on view as table on 'spending' tab
        Then the table on the 'spending' tab 'Total expenditure' chart contains:
          | Year         | Amount | Average across comparator set | National average across phase type |
          | 2017 to 2018 |        |                               |                                    |
          | 2018 to 2019 |        |                               |                                    |
          | 2019 to 2020 |        |                               |                                    |
          | 2020 to 2021 | £7,208 |                               | £10,704                            |
          | 2021 to 2022 | £7,487 | £8,127                        | £11,018                            |

    Scenario: Change Pupils on roll census chart to table view when dimension set to headcount per FTE
        Given I am on 'census' history page for school with URN '777042'
        When I change 'census' dimension to 'headcount per FTE'
        And I click on view as table on 'census' tab
        Then the table on the 'census' tab 'Pupils on roll' chart contains:
          | Year         | Total | Average across comparator set | National average across phase type |
          | 2017 to 2018 |       |                               |                                    |
          | 2018 to 2019 |       |                               |                                    |
          | 2019 to 2020 |       |                               |                                    |
          | 2020 to 2021 |       |                               |                                    |
          | 2021 to 2022 | 350   | 310.73                        | 308.02                             |

    Scenario: Change School workforce (full time equivalent) census chart to table view when dimension set to per unit
        Given I am on 'census' history page for school with URN '777042'
        When I change 'census' dimension to 'headcount per FTE'
        And I click on view as table on 'census' tab
        Then the table on the 'census' tab 'School workforce (full time equivalent)' chart contains:
          | Year         | Ratio | Average across comparator set | National average across phase type |
          | 2017 to 2018 |       |                               |                                    |
          | 2018 to 2019 |       |                               |                                    |
          | 2019 to 2020 |       |                               |                                    |
          | 2020 to 2021 |       |                               |                                    |
          | 2021 to 2022 | 1.29  | 1.28                          | 1.3                                |

    Scenario: Change Pupils on roll census chart to table view when dimension set to percentage of workforce
        Given I am on 'census' history page for school with URN '777042'
        When I change 'census' dimension to 'percentage of workforce'
        And I click on view as table on 'census' tab
        Then the table on the 'census' tab 'Pupils on roll' chart contains:
          | Year         | Total | Average across comparator set | National average across phase type |
          | 2017 to 2018 |       |                               |                                    |
          | 2018 to 2019 |       |                               |                                    |
          | 2019 to 2020 |       |                               |                                    |
          | 2020 to 2021 |       |                               |                                    |
          | 2021 to 2022 | 350   | 310.73                        | 308.02                             |

    Scenario: Change Total number of teachers (full time equivalent) census chart to table view when dimension set to percentage of workforce
        Given I am on 'census' history page for school with URN '777042'
        When I change 'census' dimension to 'percentage of workforce'
        And I click on view as table on 'census' tab
        Then the table on the 'census' tab 'Total number of teachers (full time equivalent)' chart contains:
          | Year         | Percentage | Average across comparator set | National average across phase type |
          | 2017 to 2018 |            |                               |                                    |
          | 2018 to 2019 |            |                               |                                    |
          | 2019 to 2020 |            |                               |                                    |
          | 2020 to 2021 |            |                               |                                    |
          | 2021 to 2022 | 284.4%     | 444.2%                        | 422.1%                             |