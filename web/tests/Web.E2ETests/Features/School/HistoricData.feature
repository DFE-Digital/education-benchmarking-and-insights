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

    @HistoricalTrendsFlagEnabled
    Scenario Outline: Change dimension of history data when historical trends flag enabled
        Given I am on '<tab>' history page for school with URN '777042'
        And all sections are shown on '<tab>'
        When I change '<tab>' dimension to '<dimension>'
        Then the '<tab>' dimension is '<dimension>'
        And the '<tab>' tab '<chart>' chart shows the legend '<legend>' using separator ','

        Examples:
          | tab      | dimension         | chart                                   | legend                                                                         |
          | spending | actuals           | Total expenditure                       | national average across phase type, average across comparator set, actuals     |
          | spending | £ per pupil       | Total expenditure                       | national average across phase type, average across comparator set, £ per pupil |
          | spending | £ per pupil       | Total premises staff and services costs | national average across phase type, average across comparator set, £ per m²    |
          | income   | £ per pupil       | Total income                            |                                                                                |
          | balance  | £ per pupil       | In-year balance                         |                                                                                |
          | census   | headcount per FTE | Pupils on roll                          |                                                                                |

    @HistoricalTrendsFlagEnabled
    Scenario: Change Total expenditure chart to table view when historical trends flag enabled
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

    @HistoricalTrendsFlagEnabled
    Scenario: Change Total expenditure chart to table view when historical trends flag enabled and dimension set to per unit
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