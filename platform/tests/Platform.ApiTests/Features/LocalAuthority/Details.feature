Feature: Local Authority Details
    
    Scenario: valid request for a single Local Authority
        Given a get request for a single Local Authority with code '201'
        When I submit the request
        Then the result should be ok and match the expected output of 'ValidSingleLa.json'
        
    Scenario: not found request for a single Local Authority
        Given a get request for a single Local Authority with code 'willNotBeFound'
        When I submit the request
        Then the result should be not found