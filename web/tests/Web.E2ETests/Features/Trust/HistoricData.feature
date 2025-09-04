Feature: Trust historic data

    Scenario Outline: Change dimension of history data
        Given I am on '<tab>' history page for trust with company number '04464331'
        When I change '<tab>' dimension to '<dimension>'
        Then the '<tab>' dimension is '<dimension>'

        Examples:
          | tab      | dimension   |
          | spending | £ per pupil |
          | income   | £ per pupil |
          | balance  | £ per pupil |

    Scenario Outline: Show all should expand all sections
        Given I am on '<tab>' history page for trust with company number '04464331'
        When I click on show all sections on '<tab>'
        Then all sections on '<tab>' tab are expanded
        And the show all text changes to hide all sections on '<tab>'
        And all '<tab>' sub categories are displayed on the page

        Examples:
          | tab      |
          | spending |
          | income   |

    Scenario Outline: Expected number of charts are displayed
        Given I am on '<tab>' history page for trust with company number '04464331'
        And all sections are shown on '<tab>'
        Then there should be '<charts>' charts displayed on '<tab>'
        And there should be '<warnings>' warnings displayed on '<tab>'

        Examples:
          | tab      | charts | warnings |
          | spending | 35     | 5        |
          | income   | 11     | 5        |
          | balance  | 2      | 0        |

    Scenario Outline: Change all charts to table view
        Given I am on '<tab>' history page for trust with company number '04464331'
        And all sections are shown on '<tab>'
        When I click on view as table on '<tab>' tab
        Then are showing table view on '<tab>' tab

        Examples:
          | tab      |
          | spending |
          | income   |
          | balance  |

    Scenario: Hide single section
        Given I am on 'spending' history page for trust with company number '04464331'
        And all sections are shown on 'spending'
        When I click section link for 'non educational support staff'
        Then the section 'non educational support staff' is hidden

    Scenario: Change Total expenditure chart to table view
        Given I am on 'spending' history page for trust with company number '04464331'
        When I change 'spending' dimension to 'actuals'
        And I click on view as table on 'spending' tab
        Then the table on the 'spending' tab 'Total expenditure' chart contains:
          | Year         | Amount     |
          | 2017 to 2018 |            |
          | 2018 to 2019 |            |
          | 2019 to 2020 |            |
          | 2020 to 2021 |            |
          | 2021 to 2022 | £3,103,198 |

    Scenario: Change Total expenditure chart to table view and dimension set to per unit
        Given I am on 'spending' history page for trust with company number '04464331'
        When I change 'spending' dimension to '£ per pupil'
        And I click on view as table on 'spending' tab
        Then the table on the 'spending' tab 'Total expenditure' chart contains:
          | Year         | Amount |
          | 2017 to 2018 |        |
          | 2018 to 2019 |        |
          | 2019 to 2020 |        |
          | 2020 to 2021 |        |
          | 2021 to 2022 | £6,927 |