Feature: check the accessibility of a single page

Scenario: Check the accessibility of landing page
Given I open the page with the url https://schools-financial-benchmarking.service.gov.uk/
When I check the accessibility of the page 
Then there are no accessibility issues