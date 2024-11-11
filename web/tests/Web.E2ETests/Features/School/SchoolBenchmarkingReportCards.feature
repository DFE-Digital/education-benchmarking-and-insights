Feature: School Benchmarking Report Cards

As a school governor
I want to view my school's benchmarking report card (BRC)
So that I can see how my school is performing from a financial point of view and identify areas for improvement

    Scenario: Print page as PDF
        Given I am on the Benchmarking Report Card page for school with urn '777042'
        When I click on the 'Print Page' button
        Then the print page dialog should be displayed

    Scenario: Go to school home page from BRC page
        Given I am on the Benchmarking Report Card page for school with urn '777042'
        When I click on the financial benchmarking and insight tool link under introduction
        Then I am directed to school home page for the school with urn '777042'

    Scenario: Go to school comparison page from BRC page
        Given I am on the Benchmarking Report Card page for school with urn '777042'
        When I click on the financial benchmarking and insight tool link under key information
        Then I am directed to school comparison page for the school with urn '777042'