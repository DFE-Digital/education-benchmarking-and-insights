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

    Scenario: Table view displays previous, current, and future year data
        Given I am on the IT spend page for trust with company number '10074054'
        When I select table view
        Then I should see a single table with previous, current, and future year data
        And the table should display "Data not available" for current and future year data of other trusts

    Scenario: Save chart images button opens modal and starts download
        Given I am on it spend page for school with URN '777042'
        Then the save chart images button is visible
        When I click the save chart images button
        Then the save chart images modal is visible
        And the 'benchmark-it-spending-777042.zip' file is downloaded

        
    Scenario: Can view IT spending page for claim user
        Given the IT spend for trust page for company number '10074054' is displayed
        When When I navigate to the trust Benchmark IT spending URL for company number '00000001'
        Then I should see the following IT spend charts on the page:
          | Chart Title                              |
          | Administration software and systems E20D |
          | Connectivity E20A                        |
          | IT learning resources E20C               |
          | IT support E20G                          |
          | Laptops, desktops and tablets E20E       |
          | Onsite servers E20B                      |
          | Other hardware E20F                      |
        And the chart count under each category is two. 