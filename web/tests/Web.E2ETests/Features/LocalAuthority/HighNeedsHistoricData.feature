Feature: Local Authority high needs historic data

    Scenario: Show all should expand all sections on Section 251 tab
        Given I am on 'section 251' high needs history page for local authority with code '201'
        When I click on show all sections on 'section 251'
        Then all sections on 'section 251' tab are expanded
        And the show all text changes to hide all sections on 'section 251'
        And the expected sub categories should be displayed on 'section 251':
          | Sub category                                                              |
          | Total place funding for special schools and AP/PRUs                       |
          | Top up funding (maintained schools, academies, free schools and colleges) |
          | Top up funding (non-maintained and independent schools and colleges)      |
          | SEN support and inclusion services                                        |
          | Alternative provision services                                            |
          | Hospital education services                                               |
          | Therapies and other health related services                               |
          | Primary place funding per pupil                                           |
          | Secondary place funding per pupil                                         |
          | Special place funding per pupil                                           |
          | PRU and alternative provision place funding per pupil                     |
          | Early years top up funding per pupil (maintained)                         |
          | Primary top up funding per pupil (maintained)                             |
          | Secondary top up funding per pupil (maintained)                           |
          | Special top up funding per pupil (maintained)                             |
          | Alternative provision top up funding per pupil (maintained)               |
          | Post-school top up funding per pupil (maintained)                         |
          | Top up funding income per pupil (maintained)                              |
          | Early years top up funding per pupil (non-maintained)                     |
          | Primary top up funding per pupil (non-maintained)                         |
          | Secondary top up funding per pupil (non-maintained)                       |
          | Special top up funding per pupil (non-maintained)                         |
          | Alternative provision top up funding per pupil (non-maintained)           |
          | Post-school top up funding per pupil (non-maintained)                     |
          | Top up funding income per pupil (non-maintained)                          |

    Scenario: Expected categories are displayed on Section 251 tab
        Given I am on 'section 251' high needs history page for local authority with code '201'
        Then the expected categories should be displayed on 'section 251':
          | Category                                                                                                                                                                                     |
          | High needs amount per pupil                                                                                                                                                                  |
          | High needs amount per pupil: place funding split by phase (for mainstream) and type of institution (for specialist provision)                                                                |
          | High needs amount per pupil: top up funding (maintained schools, academies, free schools and colleges) split by phase (for mainstream) and type of institution (for specialist provision)    |
          | High needs amount per pupil: top up funding (non-maintained schools and independent schools and colleges) split by phase (for mainstream) and type of institution (for specialist provision) |

    Scenario Outline: Expected number of charts are displayed
        Given I am on '<tab>' high needs history page for local authority with code '201'
        And all sections are shown on '<tab>'
        Then there should be '<charts>' charts displayed on '<tab>'
        And there should be '<warnings>' warnings displayed on '<tab>'

        Examples:
          | tab         | charts | warnings |
          | section 251 | 25     | 0        |
          | send 2      | 7      | 1        |

    Scenario Outline: Change all charts to table view
        Given I am on '<tab>' high needs history page for local authority with code '201'
        And all sections are shown on '<tab>'
        When I click on view as table on '<tab>' tab
        Then are showing table view on '<tab>' tab

        Examples:
          | tab         |
          | section 251 |
          | send 2      |

    Scenario: Hide single section section 251
        Given I am on 'section 251' high needs history page for local authority with code '201'
        And all sections are shown on 'section 251'
        When I click section link for 'place funding'
        Then the section 'place funding' is hidden

    Scenario Outline: Viewing chart legend
        Given I am on '<tab>' high needs history page for local authority with code '201'
        And all sections are shown on '<tab>'
        Then the '<tab>' tab '<chart>' chart shows the legend '<legend>' using separator ','

        Examples:
          | tab         | chart                                               | legend                       |
          | section 251 | Total place funding for special schools and AP/PRUs | Outturn, Planned expenditure |

    Scenario: Viewing data in table view section 251
        Given I am on 'section 251' high needs history page for local authority with code '204'
        And all sections are shown on 'section 251'
        When I click on view as table on 'section 251' tab
        Then the table on the 'section 251' tab 'Primary place funding per pupil' chart contains:
          | Year         | Outturn | PlannedExpenditure |
          | 2019 to 2020 |         |                    |
          | 2020 to 2021 | £7      |                    |
          | 2021 to 2022 | £9      | £8                 |
          | 2022 to 2023 | £8      | £8                 |
          | 2023 to 2024 | £12     | £8                 |

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

    Scenario: Hide single section send 2
        Given I am on 'send 2' high needs history page for local authority with code '201'
        And all sections are shown on 'send 2'
        When I click section link for 'Placement of pupils aged up to 25 with SEN statement or EHC plan (per 1000 pupils)'
        Then the section 'Placement of pupils aged up to 25 with SEN statement or EHC plan (per 1000 pupils)' is hidden

    Scenario: Viewing data in table view send 2
        Given I am on 'send 2' high needs history page for local authority with code '201'
        When I click on view as table on 'send 2' tab
        Then the table on the 'send 2' tab 'Number aged up to 25 with SEN statement or EHC plan (per 1000 pupils)' chart contains:
          | Year         | Amount |
          | 2019 to 2020 |        |
          | 2020 to 2021 | 13.24  |
          | 2021 to 2022 | 11.52  |
          | 2022 to 2023 | 12.05  |
          | 2023 to 2024 | 13.72  |