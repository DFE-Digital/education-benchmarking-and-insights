﻿Feature: School Financial Benchmarking Insights Summary

As a school governor
I want to view my school's Financial Benchmarking Insights Summary
So that I can see how my school is performing from a financial point of view and identify areas for improvement

    Scenario: View Financial Benchmarking Insights Summary
        Given I am on the Financial Benchmarking Insights Summary page for school with urn '777042'
        Then I should see the following boxes displayed under Key Information about school
          | Name            | Value    |
          | In year balance | £27,196  |
          | Revenue reserve | £162,440 |
          | School phase    | Primary  |
        And I should see the following boxes displayed under Spend in priority areas
          | Name                                | Tag           | Value                                                  |
          | Teaching and teaching support staff | High priority | £6,315 per pupil; higher than 99% of similar schools.  |
          | Non-educational support staff       | High priority | £845 per pupil; higher than 95.7% of similar schools. |
          | Administrative supplies             | High priority | £429 per pupil; higher than 99% of similar schools.    |
        And I should see the following top 3 spending priorities for my school under Other top spending priorities
          | Name                        | Tag           | Value                                                  |
          | Educational supplies        | High priority | £407 per pupil; higher than 99% of similar schools.    |
          | Catering staff and supplies | High priority | £363 per pupil; higher than 92.3% of similar schools. |
          | Premises staff and services | High priority | £93 per sq. metre; higher than 99% of similar schools. |
        And I should see the following boxes displayed under Pupil and workforce metrics
          | Name                                   | Value                                      | Comparison                                                                  |
          | Pupil-to-teacher metric                | 2.73\n\nPupils per teacher                 | Similar schools range from 0.3 to 8.59 pupils per teacher.                  |
          | Pupil-to-senior leadership role metric | 18.42\n\nPupils per senior leadership role | Similar schools range from 4.26 to 34.33 pupils per senior leadership role. |
        And the print page cta is visible
        And the response should be OK

    Scenario: View Financial Benchmarking Insights Summary for school with missing data
        Given I am on the Financial Benchmarking Insights Summary page for school with urn '990754' with missing RAG and census data
        Then I should see the following boxes displayed under Key Information about school
          | Name            | Value     |
          | In year balance | £95,414   |
          | Revenue reserve | £212,906  |
          | School phase    | Secondary |
        And I should see the warning message under Spend in priority areas
        And I should see the warning message under Other top spending priorities
        And I should see the warning message under Pupil and workforce metrics
        And the print page cta is visible
        And the response should be OK

    Scenario: Print page as PDF
        Given I am on the Financial Benchmarking Insights Summary page for school with urn '777042'
        When I click on the 'Print Page' button
        Then the print page dialog should be displayed

    Scenario: Go to school home page from Financial Benchmarking Insights Summary page
        Given I am on the Financial Benchmarking Insights Summary page for school with urn '777042'
        When I click on the financial benchmarking and insight tool link under introduction
        Then I am directed to school home page for the school with urn '777042'

    Scenario: Go to school comparison page from Financial Benchmarking Insights Summary page
        Given I am on the Financial Benchmarking Insights Summary page for school with urn '777042'
        When I click on the financial benchmarking and insight tool link under key information
        Then I am directed to school comparison page for the school with urn '777042'

    Scenario: Go to school census page from Financial Benchmarking Insights Summary page
        Given I am on the Financial Benchmarking Insights Summary page for school with urn '777042'
        When I click on the financial benchmarking and insight tool link under pupil and workforce metrics
        Then I am directed to school census page for the school with urn '777042'

    Scenario: View Financial Benchmarking Insights Summary as a non-lead federated school
        Given I am on the Financial Benchmarking Insights Summary page for unavailable school with urn '777045'
        Then the 'this is a non lead school in a federation' warning message should be displayed
        And the response should be NotFound

    Scenario: View Financial Benchmarking Insights Summary as a part-year school
        Given I am on the Financial Benchmarking Insights Summary page for unavailable school with urn '777043'
        Then the 'this school does not have data for the entire period' warning message should be displayed
        And the response should be NotFound