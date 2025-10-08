Feature: Trust benchmark IT spending

    Background:
        Given I am on the service home
        And I have signed in with organisation '010: FBIT TEST - Multi-Academy Trust (Open)'
        And I have no previous comparators selected for company number '10192252'
        And I am on IT spend for trust with company number '10192252'
        When I select the option By Name and click continue
        And I select the trust with company number '00000001' from suggester
        And I click the choose trust button
        And I select the trust with company number '10259334' from suggester
        And I click the choose trust button
        And I click the create set button

    Scenario: Can view IT spending
        Then the IT spend for trust page for company number '10192252' is displayed
        And I should see the following IT spend charts on the page:
          | Chart Title                                    |
          | ICT costs: Administration software and systems |
          | ICT costs: Connectivity                        |
          | ICT costs: IT learning resources               |
          | ICT costs: IT support                          |
          | ICT costs: Laptops, desktops and tablets       |
          | ICT costs: Onsite servers                      |
          | ICT costs: Other hardware                      |

    Scenario: Can view comparator trusts
        When I click on view comparator set
        Then the comparator set page is displayed

    Scenario: Benchmark IT spending page directs to login page when not logged in
        Given I am not logged in
        When When I navigate to the trust Benchmark IT spending URL with company number '10192252'
        Then I should be redirected to the sign-in page
        
    Scenario: Clicking trust name in chart directs to homepage
        Given I am on it spend page for trust with company number '10192252'
        When I click on the trust name on the chart
        Then I am navigated to selected trust home page
    
    Scenario: Save chart images button opens modal and starts download
        Given I am on it spend page for trust with company number '10192252'
        Then the save chart images button is visible
        When I click the save chart images button
        Then the save chart images modal is visible
        And the 'benchmark-it-spending-10192252.zip' file is downloaded

    Scenario: Can view IT spending page for claim user
        Given I am not logged in
        And I have signed in with organisation '013: FBIT TEST - Single-Academy Trust 1 (Open)'
        When I am on it spend page for trust with company number '00000002'
        Then I should see the following IT spend charts on the page:
          | Chart Title                                    |
          | ICT costs: Administration software and systems |
          | ICT costs: Connectivity                        |
          | ICT costs: IT learning resources               |
          | ICT costs: IT support                          |
          | ICT costs: Laptops, desktops and tablets       |
          | ICT costs: Onsite servers                      |
          | ICT costs: Other hardware                      |
        And I should see the following IT spend forecast charts on the page:
          | Chart Title                                    |
          | ICT costs: Administration software and systems |
          | ICT costs: Connectivity                        |
          | ICT costs: IT learning resources               |
          | ICT costs: IT support                          |
          | ICT costs: Laptops, desktops and tablets       |
          | ICT costs: Onsite servers                      |
          | ICT costs: Other hardware                      |
          
    Scenario: Can view IT spending tables
        Given I am not logged in
        And I have signed in with organisation '013: FBIT TEST - Single-Academy Trust 1 (Open)'
        When I am on it spend page for trust with company number '00000002'
        When I click to view results as 'Table'
        And I click Apply filters
        Then I should see the following IT spend tables on the page:
          | Table Title                                    |
          | ICT costs: Administration software and systems |
          | ICT costs: Connectivity                        |
          | ICT costs: IT learning resources               |
          | ICT costs: IT support                          |
          | ICT costs: Laptops, desktops and tablets       |
          | ICT costs: Onsite servers                      |
          | ICT costs: Other hardware                      |