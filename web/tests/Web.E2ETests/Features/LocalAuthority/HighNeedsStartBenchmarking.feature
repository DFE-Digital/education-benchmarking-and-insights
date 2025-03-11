Feature: Local Authority high needs start benchmarking

    @HighNeedsFlagEnabled
    Scenario: Can add local authority to comparators using select
        Given JavaScript is 'disabled'
        And I am on local authority high needs start benchmarking for local authority with code '201'
        When I select the first valid item from the select
        And I press the Tab key
        And I press the Enter key
        Then the comparator 'Barking and Dagenham' is added to the comparators

    @HighNeedsFlagEnabled
    Scenario: Can add local authority to comparators using autocomplete
        Given JavaScript is 'enabled'
        And I am on local authority high needs start benchmarking for local authority with code '201'
        When I type 'hack' into the input field
        And I press the Enter key
        And I press the Enter key again
        Then the comparator 'Hackney' is added to the comparators

    @HighNeedsFlagEnabled
    Scenario: Can save the comparator set
        Given I am on local authority high needs start benchmarking for local authority with code '201'
        And I type 'lee' into the input field
        And I press the Enter key
        And I press the Enter key again
        When I click the Save and continue button
        Then the local authority high needs benchmarking page is displayed

    @HighNeedsFlagEnabled
    Scenario: Can cancel the comparator set
        Given I am on local authority high needs start benchmarking for local authority with code '201'
        And I type 'south' into the input field
        And I press the Enter key
        And I press the Enter key again
        When I click the cancel button
        Then the local authority high needs dashboard page is displayed