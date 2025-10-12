Feature: Trust Financial Benchmarking Insights Summary
    
    Scenario: Can display View Financial Benchmarking Insights Summary for Trusts
        Given I am on the trust insights page for company number '08104190'
        Then the page should display correctly
        
    Scenario: Key information section link works correctly
        Given I am on the trust insights page for company number '08104190'
        When I click the View more insights in FBIT link
        Then I am taken to the correct Trust home page
        
    Scenario: Spending priorities section link works correctly
        Given I am on the trust insights page for company number '08104190'
        When I click the View all spending priorities at this trust
        Then I am taken to the correct Trust spending and costs page
        
    Scenario: Spending at schools section link works correctly for the spending priorities count
        Given I am on the trust insights page for company number '08104190'
        When I click a link for a spending priorities count
        Then I am taken to the correct school spending and costs page
        
    Scenario: Spending at schools section section link works correctly
        Given I am on the trust insights page for company number '08104190'
        When I click the View more information about schools at this trust link
        Then I am taken to the correct Trust home page