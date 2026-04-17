Feature: Local Authority Statistical Neighbours

    Scenario: valid request for local authority statistical neighbours
        Given a get request for local authority statistical neighbours with code '201'
        When I submit the request
        Then the result should be ok and match the expected output of 'ValidStatisticalNeighbours.json'

    Scenario: not found request for local authority statistical neighbours
        Given a get request for local authority statistical neighbours with code '999'
        When I submit the request
        Then the result should be not found

    Scenario: invalid request for local authority statistical neighbours with non-numeric code
        Given a get request for local authority statistical neighbours with code 'abc'
        When I submit the request
        Then the result should be not found

    Scenario: invalid request for local authority statistical neighbours with too short code
        Given a get request for local authority statistical neighbours with code '12'
        When I submit the request
        Then the result should be not found

    Scenario: invalid request for local authority statistical neighbours with too long code
        Given a get request for local authority statistical neighbours with code '1234'
        When I submit the request
        Then the result should be not found
