﻿Feature: Find organisation

    Scenario: Goto school homepage
        Given I am on find organisation page
        And 'school' organisation type is selected
        When I select the school with urn '990095' from suggester
        When I click Continue
        Then the school homepage is displayed