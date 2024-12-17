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

    Scenario: Sending a valid school average across comparator set census history request with dimension Total
        Given a school average across comparator set census history request with urn '990000' and dimension 'Total'
        When I submit the insights census request
        Then the census history result should be ok and contain:
          | Year | Term         | URN    | TotalPupils | Workforce | WorkforceHeadcount | Teachers | SeniorLeadership | TeachingAssistant | NonClassroomSupportStaff | AuxiliaryStaff | PercentTeacherWithQualifiedStatus |
          | 2022 | 2021 to 2022 | 990000 | 268.633333  | 47.8      | 56.233333          | 1.133333 | 1                | 25.2              | 50.633333                | 3.7            | 1.033333                          |

    Scenario: Sending a valid school average across comparator set census history request with dimension HeadcountPerFte
        Given a school average across comparator set census history request with urn '990000' and dimension 'HeadcountPerFte'
        When I submit the insights census request
        Then the census history result should be ok and contain:
          | Year | Term         | URN    | TotalPupils | Workforce             | WorkforceHeadcount    | Teachers              | SeniorLeadership      | TeachingAssistant      | AuxiliaryStaff        | PercentTeacherWithQualifiedStatus |
          | 2022 | 2021 to 2022 | 990000 | 268.633333  | 1.2454354735846555320 | 1.0000000000000000000 | 0.0090565029104613964 | 0.1168831168831168830 | 25.2000000000000000000 | 1.0492063492063492063 | 1.033333                          |

    Scenario: Sending a valid school average across comparator set census history request with dimension PercentWorkforce
        Given a school average across comparator set census history request with urn '990000' and dimension 'PercentWorkforce'
        When I submit the insights census request
        Then the census history result should be ok and contain:
          | Year | Term         | URN    | TotalPupils | Workforce              | WorkforceHeadcount     | Teachers               | SeniorLeadership      | TeachingAssistant    | AuxiliaryStaff        | PercentTeacherWithQualifiedStatus |
          | 2022 | 2021 to 2022 | 990000 | 268.633333  | 100.000000000000000000 | 124.543547358465553203 | 490.534045780128272232 | 50.793106165302500259 | 3.884367879357122409 | 10.195699860404623272 | 1.033333                          |

    Scenario: Sending a valid school average across comparator set census history request with dimension PupilsPerStaffRole
        Given a school average across comparator set census history request with urn '990000' and dimension 'PupilsPerStaffRole'
        When I submit the insights census request
        Then the census history result should be ok and contain:
          | Year | Term         | URN    | TotalPupils | Workforce             | WorkforceHeadcount    | Teachers              | SeniorLeadership       | TeachingAssistant       | AuxiliaryStaff         | PercentTeacherWithQualifiedStatus |
          | 2022 | 2021 to 2022 | 990000 | 268.633333  | 7.2173904580668970625 | 5.8673804361619042791 | 2.2898520957463768590 | 14.8307257448787915306 | 268.6333333333333333333 | 79.4421957671957671957 | 1.033333                          |

    Scenario: Sending an invalid school average across comparator set census history request
        Given a school average across comparator set census history request with urn '990000' and dimension 'invalid'
        When I submit the insights census request
        Then the census result should be bad request

    Scenario: Sending a valid school national average census history request with dimension Total, phase Primary, financeType Maintained
        Given a school average across comparator set census history request with dimension 'Total', phase 'Primary', financeType 'Maintained'
        When I submit the insights census request
        Then the census history result should be ok and contain:
          | Year | Term         | TotalPupils | Workforce | WorkforceHeadcount | Teachers | TeachingAssistant | NonClassroomSupportStaff | AuxiliaryStaff | PercentTeacherWithQualifiedStatus |
          | 2018 | 2017 to 2018 |             |           |                    |          |                   |                          |                |                                   |
          | 2019 | 2018 to 2019 |             |           |                    |          |                   |                          |                |                                   |
          | 2020 | 2019 to 2020 |             |           |                    |          |                   |                          |                |                                   |
          | 2021 | 2020 to 2021 |             |           |                    |          |                   |                          |                |                                   |
          | 2022 | 2021 to 2022 | 308.019801  | 36.80693  | 47.376237          | 1.163366 | 16.430693         | 49.861386                | 3.643564       | 1.024752                          |

    Scenario: Sending a valid school national average census history request with dimension HeadcountPerFte, phase Primary, financeType Maintained
        Given a school average across comparator set census history request with dimension 'HeadcountPerFte', phase 'Primary', financeType 'Maintained'
        When I submit the insights census request
        Then the census history result should be ok and contain:
          | Year | Term         | TotalPupils | Workforce             | WorkforceHeadcount    | Teachers              | TeachingAssistant      | AuxiliaryStaff        | PercentTeacherWithQualifiedStatus |
          | 2018 | 2017 to 2018 |             |                       |                       |                       |                        |                       |                                   |
          | 2019 | 2018 to 2019 |             |                       |                       |                       |                        |                       |                                   |
          | 2020 | 2019 to 2020 |             |                       |                       |                       |                        |                       |                                   |
          | 2021 | 2020 to 2021 |             |                       |                       |                       |                        |                       |                                   |
          | 2022 | 2021 to 2022 | 308.019801  | 1.2992935951022364958 | 1.0000000000000000000 | 0.0088508541685603504 | 16.3910891089108910891 | 1.0766867043847241867 | 1.024752                          |

    Scenario: Sending a valid school national average census history request with dimension PercentWorkforce, phase Primary, financeType Maintained
        Given a school average across comparator set census history request with dimension 'PercentWorkforce', phase 'Primary', financeType 'Maintained'
        When I submit the insights census request
        Then the census history result should be ok and contain:
          | Year | Term         | TotalPupils | Workforce              | WorkforceHeadcount     | Teachers               | TeachingAssistant    | AuxiliaryStaff       | PercentTeacherWithQualifiedStatus |
          | 2018 | 2017 to 2018 |             |                        |                        |                        |                      |                      |                                   |
          | 2019 | 2018 to 2019 |             |                        |                        |                        |                      |                      |                                   |
          | 2020 | 2019 to 2020 |             |                        |                        |                        |                      |                      |                                   |
          | 2021 | 2020 to 2021 |             |                        |                        |                        |                      |                      |                                   |
          | 2022 | 2021 to 2022 | 308.019801  | 100.000000000000000000 | 129.929359510223649589 | 422.090092731097664571 | 3.234154307234142991 | 9.787430203141995055 | 1.024752                          |

    Scenario: Sending a valid school national average census history request with dimension PupilsPerStaffRole, phase Primary, financeType Maintained
        Given a school average across comparator set census history request with dimension 'PupilsPerStaffRole', phase 'Primary', financeType 'Maintained'
        When I submit the insights census request
        Then the census history result should be ok and contain:
          | Year | Term         | TotalPupils | Workforce             | WorkforceHeadcount    | Teachers              | TeachingAssistant       | AuxiliaryStaff         | PercentTeacherWithQualifiedStatus |
          | 2018 | 2017 to 2018 |             |                       |                       |                       |                         |                        |                                   |
          | 2019 | 2018 to 2019 |             |                       |                       |                       |                         |                        |                                   |
          | 2020 | 2019 to 2020 |             |                       |                       |                       |                         |                        |                                   |
          | 2021 | 2020 to 2021 |             |                       |                       |                       |                         |                        |                                   |
          | 2022 | 2021 to 2022 | 308.019801  | 8.4625263705554415226 | 6.6432385984097354055 | 2.4337378342010643450 | 307.3490099009900990099 | 99.4556793179317931793 | 1.024752                          |

    Scenario: Sending a valid school national average census history request with dimension Total, phase Secondary, financeType Academy
        Given a school average across comparator set census history request with dimension 'Total', phase 'Secondary', financeType 'Academy'
        When I submit the insights census request
        Then the census history result should be ok and contain:
          | Year | Term         | TotalPupils | Workforce | WorkforceHeadcount | Teachers | TeachingAssistant | NonClassroomSupportStaff | AuxiliaryStaff | PercentTeacherWithQualifiedStatus |
          | 2018 | 2017 to 2018 |             |           |                    |          |                   |                          |                |                                   |
          | 2019 | 2018 to 2019 |             |           |                    |          |                   |                          |                |                                   |
          | 2020 | 2019 to 2020 |             |           |                    |          |                   |                          |                |                                   |
          | 2021 | 2020 to 2021 |             |           |                    |          |                   |                          |                |                                   |
          | 2022 | 2021 to 2022 | 371.210000  | 47.000000 | 58.190000          | 1.200000 | 22.040000         | 49.850000                | 3.900000       | 1.020000                          |

    Scenario: Sending a valid school national average census history request with dimension HeadcountPerFte, phase Special, financeType Academy
        Given a school average across comparator set census history request with dimension 'HeadcountPerFte', phase 'Special', financeType 'Academy'
        When I submit the insights census request
        Then the census history result should be ok and contain:
          | Year | Term         | TotalPupils | Workforce             | WorkforceHeadcount    | Teachers              | TeachingAssistant      | NonClassroomSupportStaff | AuxiliaryStaff        | PercentTeacherWithQualifiedStatus |
          | 2018 | 2017 to 2018 |             |                       |                       |                       |                        |                          |                       |                                   |
          | 2019 | 2018 to 2019 |             |                       |                       |                       |                        |                          |                       |                                   |
          | 2020 | 2019 to 2020 |             |                       |                       |                       |                        |                          |                       |                                   |
          | 2021 | 2020 to 2021 |             |                       |                       |                       |                        |                          |                       |                                   |
          | 2022 | 2021 to 2022 | 348.829787  | 1.2830559767184912978 | 1.0000000000000000000 | 0.0088612061184511927 | 24.2340425531914893617 |                          | 1.0878465506125080593 | 1.074468                          |

    Scenario: Sending an invalid school average across comparator set census history request with invalid dimension, phase Primary, financeType Maintained
        Given a school average across comparator set census history request with dimension 'invalid', phase 'Primary', financeType 'Maintained'
        When I submit the insights census request
        Then the census result should be bad request

    Scenario: Sending an invalid school average across comparator set census history request with dimension Total, invalid phase, financeType Maintained
        Given a school average across comparator set census history request with dimension 'Total', phase 'invalid', financeType 'Maintained'
        When I submit the insights census request
        Then the census result should be bad request

    Scenario: Sending an invalid school average across comparator set census history request with dimension Total, phase Primary, invalid financeType
        Given a school average across comparator set census history request with dimension 'Total', phase 'Primary', financeType 'invalid'
        When I submit the insights census request
        Then the census result should be bad request