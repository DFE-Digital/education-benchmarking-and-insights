Feature: View Trust comparator set

    Background:
        Given I am on the service home
        And I have signed in with organisation '010: FBIT TEST - Multi-Academy Trust (Open)'
        And I am on compare by page for trust with company number '10074054'

    Scenario: Can view comparator trusts data including central spending
        When I select the option By Name and click continue
        And I select the trust with company number '00000001' from suggester
        And I click the choose trust button
        And I select the trust with company number '8076374' from suggester
        And I click the choose trust button
        And I click the create set button
        And I click on view as table
        Then the table for 'Total expenditure' contains the following:
          | TrustName                | TotalAmount | SchoolAmount | CentralAmount |
          | FBIT Multi Academy Trust | £18,519     | £18,519      | £0            |
          | Test Company/Trust 309   | £9,807      | £9,807       | £0            |
          | Test Company/Trust 108   | £7,684      | £7,684       | £0            |

    Scenario: Can view comparator trusts data excluding central spending
        When I select the option By Name and click continue
        And I select the trust with company number '00000001' from suggester
        And I click the choose trust button
        And I select the trust with company number '8076374' from suggester
        And I click the choose trust button
        And I click the create set button
        And I click on view as table
        And I click on exclude central spending
        Then the table for 'Total expenditure' contains the following:
          | TrustName                | TotalAmount |
          | FBIT Multi Academy Trust | £18,519     |
          | Test Company/Trust 309   | £9,807      |
          | Test Company/Trust 108   | £7,684      |