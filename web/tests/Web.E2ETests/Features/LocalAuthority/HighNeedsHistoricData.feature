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
          | Alternative provision place funding per head 2-18 population                   |
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
          | Category                                                                      |
          | High needs amount per head 2-18 population                                    |
          | Place funding                                                                 |
          | Top up funding (maintained schools, academies, free schools and colleges)     |
          | Top up funding (non-maintained schools, and independent schools and colleges) |

    @HighNeedsFlagEnabled
    Scenario Outline: Expected number of charts are displayed
        Given I am on '<tab>' high needs history page for local authority with code '201'
        And all sections are shown on '<tab>'
        Then there should be '<charts>' charts displayed on '<tab>'

        Examples:
          | tab         | charts |
          | section 251 | 25     |

    @HighNeedsFlagEnabled
    Scenario Outline: Change all charts to table view
        Given I am on '<tab>' high needs history page for local authority with code '201'
        And all sections are shown on '<tab>'
        When I click on view as table on '<tab>' tab
        Then are showing table view on '<tab>' tab

        Examples:
          | tab         |
          | section 251 |

    @HighNeedsFlagEnabled
    Scenario: Hide single section
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
          | tab         | chart                                               | legend          |
          | section 251 | Total place funding for special schools and AP/PRUs | actual, planned |

    @HighNeedsFlagEnabled
    Scenario: Viewing data in table view
        Given I am on 'section 251' high needs history page for local authority with code '201'
        And all sections are shown on 'section 251'
        When I click on view as table on 'section 251' tab
        Then the table on the 'section 251' tab 'Primary place funding per head 2-18 population' chart contains:
          | Year         | Actual     | Planned    |
          | 2020 to 2021 | £1,002,043 | £1,102,043 |
          | 2021 to 2022 | £1,002,044 | £1,102,044 |
          | 2022 to 2023 | £1,002,045 | £1,102,045 |
          | 2023 to 2024 | £1,002,046 | £1,102,046 |