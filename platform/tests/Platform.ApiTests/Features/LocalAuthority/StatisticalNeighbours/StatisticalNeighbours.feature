Feature: Local Authority Statistical Neighbours

    Scenario: valid request for local authority statistical neighbours
        Given a get request for local authority statistical neighbours with code '201'
        When I submit the request
        Then the result should be ok and match the expected output of 'ValidStatisticalNeighbours.json'

    Scenario Outline: not found request for local authority statistical neighbours
        Given a get request for local authority statistical neighbours with code '<code>'
        When I submit the request
        Then the result should be not found

        Examples:
          | code |
          | 999  |
          | abc  |
          | 12   |
          | 1234 |

    Scenario: request for local authority statistical neighbours with unsupported api version
        Given a get request for local authority statistical neighbours with code '201' and api version '2.0'
        When I submit the request
        Then the result should be bad request and match the expected output of 'UnsupportedApiVersion.json'
