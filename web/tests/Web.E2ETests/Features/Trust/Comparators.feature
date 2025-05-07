Feature: View Trust comparator set

    Background:
        Given I am on the service home
        And I have signed in with organisation '010: FBIT TEST - Multi-Academy Trust (Open)'
        And I am on compare by page for trust with company number '10074054'
        When I select the option By Name and click continue
        And I select the trust with company number '00000001' from suggester
        And I click the choose trust button
        And I select the trust with company number '08076374' from suggester
        And I click the choose trust button
        And I click the create set button

    Scenario: Can view comparator trusts data including central spending
        When I click on view as table
        Then the table for 'Total expenditure' contains the following:
          | TrustName                | TotalAmount | SchoolAmount | CentralAmount |
          | FBIT Multi Academy Trust | £18,519     | £18,519      | £0.00         |
          | Test Company/Trust 309   | £9,807      | £9,807       | £0.00         |
          | Test Company/Trust 108   | £7,684      | £7,684       | £0.00         |

    Scenario: Can view comparator trusts data excluding central spending
        When I click on view as table
        And I click on exclude central spending
        Then the table for 'Total expenditure' contains the following:
          | TrustName                | TotalAmount |
          | FBIT Multi Academy Trust | £18,519     |
          | Test Company/Trust 309   | £9,807      |
          | Test Company/Trust 108   | £7,684      |

    Scenario: Charts have correct dimension options
        Then all sections on the page have the correct dimension options:
          | Chart name                     | Options                                                               |
          | TotalExpenditure               | £ per pupil, actuals, percentage of income                            |
          | TeachingAndTeachingSupplyStaff | £ per pupil, actuals, percentage of expenditure, percentage of income |
          | NonEducationalSupportStaff     | £ per pupil, actuals, percentage of expenditure, percentage of income |
          | EducationalSupplies            | £ per pupil, actuals, percentage of expenditure, percentage of income |
          | EducationalIct                 | £ per pupil, actuals, percentage of expenditure, percentage of income |
          | Premises                       | £ per m², actuals, percentage of expenditure, percentage of income    |
          | Utilities                      | £ per m², actuals, percentage of expenditure, percentage of income    |
          | AdministrativeSupplies         | £ per pupil, actuals, percentage of expenditure, percentage of income |
          | CateringStaffAndServices       | £ per pupil, actuals, percentage of expenditure, percentage of income |
          | Other                          | £ per pupil, actuals, percentage of expenditure, percentage of income |