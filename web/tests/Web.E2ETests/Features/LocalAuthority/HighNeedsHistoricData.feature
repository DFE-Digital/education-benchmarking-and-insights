Feature: Local Authority high needs historic data

    @HighNeedsFlagEnabled
    Scenario: Show all should expand all sections on Section 251 tab
        Given I am on 'section 251' high needs history page for local authority with code '201'
        When I click on show all sections on 'section 251'
        Then all sections on 'section 251' tab are expanded
        And the show all text changes to hide all sections on 'section 251'
        And the expected sub categories should be displayed on 'section 251':
          | Sub category                                                                   |
          | Total place funding for special schools and AP/PRUs                            |
          | Top up funding (maintained schools, academies, free schools and colleges)      |
          | Top up funding (non-maintained and independent schools and colleges)           |
          | SEN support and inclusion services                                             |
          | Alternative provision services                                                 |
          | Hospital education services                                                    |
          | Therapies and other health related services                                    |
          | Primary place funding per head 2-18 population                                 |
          | Secondary place funding per head 2-18 population                               |
          | Special place funding per head 2-18 population                                 |
          | PRU and alternative provision place funding per head 2-18                      |
          | Early years top up funding per head 2-18 population (maintained)               |
          | Primary top up funding per head 2-18 population (maintained)                   |
          | Secondary top up funding per head 2-18 population (maintained)                 |
          | Special top up funding per head 2-18 population (maintained)                   |
          | Alternative provision top up funding per head 2-18 population (maintained)     |
          | Post-school top up funding per head 2-18 population (maintained)               |
          | Top up funding income per head 2-18 population (maintained)                    |
          | Early years top up funding per head 2-18 population (non-maintained)           |
          | Primary top up funding per head 2-18 population (non-maintained)               |
          | Secondary top up funding per head 2-18 population (non-maintained)             |
          | Special top up funding per head 2-18 population (non-maintained)               |
          | Alternative provision top up funding per head 2-18 population (non-maintained) |
          | Post-school top up funding per head 2-18 population (non-maintained)           |
          | Top up funding income per head 2-18 population (non-maintained)                |

    @HighNeedsFlagEnabled
    Scenario: Expected categories are displayed on Section 251 tab
        Given I am on 'section 251' high needs history page for local authority with code '201'
        Then the expected categories should be displayed on 'section 251':
          | Category                                                                                                                                                                                                          |
          | High needs amount per head of 2 to 18 population                                                                                                                                                                  |
          | High needs amount per head of 2 to 18 population: place funding split by phase (for mainstream) and type of institution (for specialist provision)                                                                |
          | High needs amount per head of 2 to 18 population: top up funding (maintained schools, academies, free schools and colleges) split by phase (for mainstream) and type of institution (for specialist provision)    |
          | High needs amount per head of 2 to 18 population: top up funding (non-maintained schools and independent schools and colleges) split by phase (for mainstream) and type of institution (for specialist provision) |

    @HighNeedsFlagEnabled
    Scenario Outline: Expected number of charts are displayed
        Given I am on '<tab>' high needs history page for local authority with code '201'
        And all sections are shown on '<tab>'
        Then there should be '<charts>' charts displayed on '<tab>'
        And there should be '<warnings>' warnings displayed on '<tab>'

        Examples:
          | tab         | charts | warnings |
          | section 251 | 25     | 0        |
          | send 2      | 7      | 1        |

    @HighNeedsFlagEnabled
    Scenario Outline: Change all charts to table view
        Given I am on '<tab>' high needs history page for local authority with code '201'
        And all sections are shown on '<tab>'
        When I click on view as table on '<tab>' tab
        Then are showing table view on '<tab>' tab

        Examples:
          | tab         |
          | section 251 |
          | send 2      |

    @HighNeedsFlagEnabled
    Scenario: Hide single section section 251
        Given I am on 'section 251' high needs history page for local authority with code '201'
        And all sections are shown on 'section 251'
        When I click section link for 'place funding'
        Then the section 'place funding' is hidden

    @HighNeedsFlagEnabled
    Scenario Outline: Viewing chart legend
        Given I am on '<tab>' high needs history page for local authority with code '201'
        And all sections are shown on '<tab>'
        Then the '<tab>' tab '<chart>' chart shows the legend '<legend>' using separator ','

        Examples:
          | tab         | chart                                               | legend                       |
          | section 251 | Total place funding for special schools and AP/PRUs | Outturn, Planned expenditure |

    @HighNeedsFlagEnabled
    Scenario: Viewing data in table view section 251
        Given I am on 'section 251' high needs history page for local authority with code '204'
        And all sections are shown on 'section 251'
        When I click on view as table on 'section 251' tab
        Then the table on the 'section 251' tab 'Primary place funding per head 2-18 population' chart contains:
          | Year         | Outturn | PlannedExpenditure |
          | 2019 to 2020 |         |                    |
          | 2020 to 2021 | £6.44   |                    |
          | 2021 to 2022 | £8.82   | £7.74              |
          | 2022 to 2023 | £8.02   | £7.82              |
          | 2023 to 2024 | £11.38  | £7.82              |

    @HighNeedsFlagEnabled
    Scenario: Show all should expand all sections on Send 2 tab
        Given I am on 'send 2' high needs history page for local authority with code '201'
        When I click on show all sections on 'send 2'
        Then all sections on 'send 2' tab are expanded
        And the show all text changes to hide all sections on 'send 2'
        And the expected sub categories should be displayed on 'send 2':
          | Sub category                                    |
          | Mainstream schools or academies                 |
          | Resourced provision or SEN units                |
          | Maintained special schools or special academies |
          | NMSS or independent schools                     |
          | Hospital schools or alternative provisions      |
          | Post 16                                         |
          | Other                                           |

    @HighNeedsFlagEnabled
    Scenario: Hide single section send 2
        Given I am on 'send 2' high needs history page for local authority with code '201'
        And all sections are shown on 'send 2'
        When I click section link for 'Placement of pupils aged up to 25 with SEN statement or EHC plan (per 1000 2 to 18 population)'
        Then the section 'Placement of pupils aged up to 25 with SEN statement or EHC plan (per 1000 2 to 18 population)' is hidden

    @HighNeedsFlagEnabled
    Scenario: Viewing data in table view send 2
        Given I am on 'send 2' high needs history page for local authority with code '201'
        When I click on view as table on 'send 2' tab
        Then the table on the 'send 2' tab 'Number aged up to 25 with SEN statement or EHC plan (per 1000 of 2 to 18 population)' chart contains:
          | Year         | Amount |
          | 2019 to 2020 |        |
          | 2020 to 2021 | 11.94  |
          | 2021 to 2022 | 10.82  |
          | 2022 to 2023 | 11.64  |
          | 2023 to 2024 | 13     |