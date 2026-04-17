Feature: Local Authority Details
    
    Scenario Outline: valid request for a single Local Authority
        Given a get request for a single Local Authority with code '<Code>'
        When I submit the request
        Then the result should be ok and match the expected output of '<Result>'

    Examples:
      | Code | Result               |
      | 201  | ValidSingleLa.json   |
      | 334  | ValidSingleLa334.json |
        
    Scenario: not found request for a single Local Authority
        Given a get request for a single Local Authority with code 'willNotBeFound'
        When I submit the request
        Then the result should be not found

    Scenario: valid request for a collection of Local Authorities
        Given a get request for a collection of Local Authorities
        When I submit the request
        Then the collection result should be ok and match the expected output of 'ValidLaCollection.json'