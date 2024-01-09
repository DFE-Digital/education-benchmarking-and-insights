Feature: Establishment local authorities endpoints

    Scenario: Get local authority
        Given a valid local authority request with id '123'
        When I submit the local authorities request
        Then the local authority result should be ok

    Scenario: Query local authorities
        Given a valid local authorities query request
        When I submit the local authorities request
        Then the local authorities query result should be ok

    Scenario: Search local authorities
        Given a valid local authorities search request
        When I submit the local authorities request
        Then the local authorities search result should be ok

    Scenario: Suggest local authorities
        Given a valid local authorities suggest request
        When I submit the local authorities request
        Then the local authorities suggest result should be ok