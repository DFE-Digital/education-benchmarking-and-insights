Feature: Insights census endpoints

    Scenario: Sending a valid school census dimension request
        Given a valid school census dimension request
        When I submit the insights census request
        Then the census dimensions result should be ok and contain:
          | Dimension          |
          | HeadcountPerFte    |
          | Total              |
          | PercentWorkforce   |
          | PupilsPerStaffRole |

    Scenario: Sending a valid school census category request
        Given a valid school census category request
        When I submit the insights census request
        Then the census categories result should be ok and contain:
          | Category                    |
          | WorkforceFte                |
          | TeachersFte                 |
          | SeniorLeadershipFte         |
          | TeachingAssistantsFte       |
          | NonClassroomSupportStaffFte |
          | AuxiliaryStaffFte           |
          | WorkforceHeadcount          |
          | TeachersQualified           |

    Scenario: Sending a valid school census request with category and dimension
        Given a valid school census request with urn '990000', category 'WorkforceFte' and dimension 'Total'
        When I submit the insights census request
        Then the census result should be ok and contain:
          | Field                             | Value                  |
          | AuxiliaryStaff                    |                        |
          | LAName                            | Bedford                |
          | NonClassroomSupportStaff          |                        |
          | PercentTeacherWithQualifiedStatus |                        |
          | SchoolName                        | Test school 176        |
          | SchoolType                        | Voluntary aided school |
          | SeniorLeadership                  |                        |
          | Teachers                          |                        |
          | TeachingAssistant                 |                        |
          | TotalPupils                       | 337.00                 |
          | URN                               | 990000                 |
          | Workforce                         | 29.00                  |
          | WorkforceHeadcount                |                        |

    Scenario: Sending a valid school census request with dimension
        Given a valid school census request with urn '990000', category '' and dimension 'Total'
        When I submit the insights census request
        Then the census result should be ok and contain:
          | Field                             | Value                  |
          | AuxiliaryStaff                    | 3.00                   |
          | LAName                            | Bedford                |
          | NonClassroomSupportStaff          | 0.00                   |
          | PercentTeacherWithQualifiedStatus | 1.00                   |
          | SchoolName                        | Test school 176        |
          | SchoolType                        | Voluntary aided school |
          | SeniorLeadership                  | 16.00                  |
          | Teachers                          | 116.00                 |
          | TeachingAssistant                 | 1.00                   |
          | TotalPupils                       | 337.00                 |
          | URN                               | 990000                 |
          | Workforce                         | 29.00                  |
          | WorkforceHeadcount                | 33.00                  |

    Scenario: Sending an invalid school census request
        Given an invalid school census request with urn '000000'
        When I submit the insights census request
        Then the census result should be not found

    Scenario: Sending a valid school census history request
        Given a valid school census history request with urn '990000'
        When I submit the insights census request
        Then the census history result should be ok and contain:
          | Year | Term         | AuxiliaryStaff | NonClassroomSupportStaff | PercentTeacherWithQualifiedStatus | SeniorLeadership | Teachers | TeachingAssistant | TotalPupils | URN    | Workforce | WorkforceHeadcount |
          | 2018 | 2017 to 2018 |                |                          |                                   |                  |          |                   |             | 990000 |           |                    |
          | 2019 | 2018 to 2019 |                |                          |                                   |                  |          |                   |             | 990000 |           |                    |
          | 2020 | 2019 to 2020 |                |                          |                                   |                  |          |                   |             | 990000 |           |                    |
          | 2021 | 2020 to 2021 |                |                          |                                   |                  |          |                   |             | 990000 |           |                    |
          | 2022 | 2021 to 2022 | 3.00           | 0.00                     | 1.00                              | 16.00            | 116.00   | 1.00              | 337.00      | 990000 | 29.00     | 33.00              |

    Scenario: Sending a valid school census query request with URNs
        Given a valid school census query request with urns:
          | Urn    |
          | 990000 |
          | 990001 |
          | 990002 |
        When I submit the insights census request
        Then the census query result should be ok and contain:
          | SchoolName      | SchoolType                     | LAName        | AuxiliaryStaff | NonClassroomSupportStaff | PercentTeacherWithQualifiedStatus | SeniorLeadership | Teachers | TeachingAssistant | TotalPupils | URN    | Workforce | WorkforceHeadcount |
          | Test school 176 | Voluntary aided school         | Bedford       | 3.00           | 0.00                     | 1.00                              | 16.00            | 116.00   | 1.00              | 337.00      | 990000 | 29.00     | 33.00              |
          | Test school 241 | Voluntary aided school         | Islington     | 3.00           | 0.00                     | 1.00                              | 11.00            | 147.00   | 1.00              | 164.00      | 990001 | 27.00     | 39.00              |
          | Test school 224 | Local authority nursery school | Isle of Wight | 2.00           | 0.00                     | 1.00                              | 4.00             | 136.00   | 1.00              | 86.00       | 990002 | 15.00     | 20.00              |

    Scenario: Sending a valid school census query request with company number and phase
        Given a valid school census query request with company number '8104190' and phase 'Secondary'
        When I submit the insights census request
        Then the census query result should be ok and contain:
          | SchoolName             | SchoolType        | LAName                 | AuxiliaryStaff | NonClassroomSupportStaff | PercentTeacherWithQualifiedStatus | SeniorLeadership | Teachers | TeachingAssistant | TotalPupils | URN    | Workforce | WorkforceHeadcount |
          | Test academy school 87 | Academy converter | City of London         | 4.00           | 0.00                     | 1.00                              | 25.00            | 149.00   | 2.00              | 271.00      | 777051 | 42.00     | 62.00              |
          | Test academy school 90 | Academy converter | Camden                 | 2.00           | 0.00                     | 1.00                              | 5.00             | 133.00   | 1.00              | 136.00      | 777052 | 37.00     | 50.00              |
          | Test academy school 91 | Academy converter | Greenwich              | 3.00           | 0.00                     | 1.00                              | 24.00            | 112.00   | 1.00              | 386.00      | 777053 | 31.00     | 35.00              |
          | Test academy school 92 | Free school       | Hackney                | 3.00           | 0.00                     | 1.00                              | 24.00            | 112.00   | 1.00              | 386.00      | 777054 | 31.00     | 35.00              |
          | Test academy school 93 | Academy converter | Hammersmith and Fulham | 5.00           | 0.00                     | 1.00                              | 19.00            | 128.00   | 1.00              | 350.00      | 777055 | 45.00     | 58.00              |
          | Test academy school 94 | Academy converter | Islington              | 5.00           | 0.00                     | 1.00                              | 20.00            | 113.00   | 1.00              | 389.00      | 777056 | 36.00     | 41.00              |

    Scenario: Sending a valid school census query request with LA code and phase
        Given a valid school census query request with LA code '205' and phase 'Secondary'
        When I submit the insights census request
        Then the census query result should be ok and contain:
          | SchoolName             | SchoolType        | LAName                 | AuxiliaryStaff | NonClassroomSupportStaff | PercentTeacherWithQualifiedStatus | SeniorLeadership | Teachers | TeachingAssistant | TotalPupils | URN    | Workforce | WorkforceHeadcount |
          | Test academy school 93 | Academy converter | Hammersmith and Fulham | 5.00           | 0.00                     | 1.00                              | 19.00            | 128.00   | 1.00              | 350.00      | 777055 | 45.00     | 58.00              |