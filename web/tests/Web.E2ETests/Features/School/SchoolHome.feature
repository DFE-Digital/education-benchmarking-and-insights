Feature: School homepage

    Background:
        Given I am on the service home
        And I am not logged in

    Scenario: Go to contact details page
        Given I am on school homepage for school with urn '777042'
        When I click on school details
        Then the school details page is displayed

    Scenario: Go to compare your costs page
        Given I am on school homepage for school with urn '777042'
        When I click on compare your costs
        Then the compare your costs page is displayed

    Scenario: Go to curriculum and financial planning page
        Given I am on school homepage for school with urn '777042'
        And I have signed in with organisation '01: FBIT TEST - Community School (Open)'
        When I click on curriculum and financial planning
        Then the curriculum and financial planning page is displayed

    Scenario: Go to benchmark census data page
        Given I am on school homepage for school with urn '777042'
        When I click on benchmark census data
        Then the benchmark census page is displayed

    Scenario: Go to Financial Benchmarking Insights Summary page
        Given I am on school homepage for school with urn '777042'
        When I click on Financial Benchmarking Insights Summary
        Then the Financial Benchmarking Insights Summary page is displayed

    Scenario: Go to view all spending and costs page
        Given I am on school homepage for school with urn '777042'
        When I click on view all spending and costs
        Then the spending and costs page is displayed

    Scenario: Goto view historic data page
        Given I am on school homepage for school with urn '777042'
        When I click on view historic data
        Then the historic data page is displayed

    Scenario: Goto find ways to spend less page
        Given I am on school homepage for school with urn '777042'
        When I click on find ways to spend less
        Then the commercial resources page is displayed

    Scenario: Goto compare your costs page for part year school
        Given I am on part year school homepage for school with urn '777043'
        When I click on compare your costs
        Then the compare your costs page is displayed for part year

    Scenario: Top priority categories have the correct RAG commentary
        Given I am on school homepage for school with urn '777042'
        Then the RAG commentary for each priority category is
          | Name                                | Commentary                                                                              |
          | Teaching and Teaching support staff | High priority Spends £6,315 per pupil — Spending is higher than 99% of similar schools. |
          | Non-educational support staff       | High priority Spends £845 per pupil — Spending is higher than 95.7% of similar schools. |
          | Administrative supplies             | High priority Spends £429 per pupil — Spending is higher than 99% of similar schools.   |

    Scenario: RAG guidance is displayed
        Given I am on school homepage for school with urn '777042'
        Then the RAG guidance is visible