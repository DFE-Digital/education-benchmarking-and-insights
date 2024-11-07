Feature: Benchmarking Report Cards (BRCs)

As a school governor
I want to view my school's benchmarking report card (BRC)
So that I can see how my school is performing from a financial point of view and identify areas for improvement


    Scenario: Download BRC as a PDF
        Given I am on the Benchmarking Report Card page for school with urn '777042'
        When I click on the 'Download Report' button
        Then a PDF version of the BRC should be downloaded

    Scenario: Goto FBIT tool from BRC page
        Given I am on the Benchmarking Report Card page for school with urn '777042'
        When I click on the financial benchmarking and insight tool link under key infromation
        Then I am directed to FBIT landing page.
        
    Scenario: Goto comparison page from BRC page
        Given I am on the Benchmarking Report Card page for school with urn '777042'
        When I click on the financial benchmarking and insight tool link 
        Then I am directed to comparison page for the school with urn '777042'