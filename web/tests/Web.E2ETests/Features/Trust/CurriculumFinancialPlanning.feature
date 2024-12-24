Feature: Trust curriculum and financial planning

    Background:
        Given I am on the service home
        And I have signed in with organisation '010: FBIT TEST - Multi-Academy Trust (Open)'

    Scenario: Show all should expand all sections
        Given I am on Curriculum and financial planning page for trust with company number '00000001'
        When I click on show all sections
        Then all accordions are expanded
        And following is shown for 'Contact Ratio'
          | School                  | Financial year | Contact ratio | Rating |
          | Test academy school 187 | 2023 - 2024      | 0.01          | Red    |
          | Test academy school 168 | 2023 - 2024      | 0.00          | Red    |
        And following is shown for 'Average class size'
          | School                  | Financial year | Average class size | Rating |
          | Test academy school 187 | 2023 - 2024      | 83.33              | Red    |
          | Test academy school 168 | 2023 - 2024      | 33.33              | Red    |
        And following is shown for 'In-year balance'
          | School                  | Financial year | In-year balance | Rating |
          | Test academy school 187 | 2023 - 2024      | -£85,189        | Red    |
          | Test academy school 168 | 2023 - 2024      | £70,466         | Green  |