Feature: Local Authority high needs start benchmarking

    Scenario: Can add local authority to comparators using select
        Given JavaScript is 'disabled'
        And I am on local authority high needs start benchmarking for local authority with code '201'
        When I select the first valid item from the select
        And I press the Tab key
        And I press the Enter key
        Then the comparator 'Barking and Dagenham' is added to the comparators

    Scenario: Can add local authority to comparators using autocomplete
        Given JavaScript is 'enabled'
        And I am on local authority high needs start benchmarking for local authority with code '201'
        When I type 'bark' into the input field
        And I press the Enter key
        And I press the Tab key
        And I press the Enter key
        Then the comparator 'Barking and Dagenham' is added to the comparators

    Scenario: Can save the comparator set
        Given I am on local authority high needs start benchmarking for local authority with code '201'
        When I click the Save and continue button
        Then the local authority high needs benchmarking page is displayed