Feature: Insights income endpoints

    Scenario: Sending a valid school income request with category
        Given a valid school income request with urn '990000'
        When I submit the insights income request
        Then the school income response should be ok, contain a JSON object and match the expected output of 'SchoolIncome.json'

    Scenario: Sending a valid school expenditure request with bad URN
        Given a valid school income request with urn '0000000'
        When I submit the insights income request
        Then the school income result should be not found

    Scenario: Sending a valid school income history request
        Given a valid school income history request with urn '990000'
        When I submit the insights income request
        Then the school income response should be ok, contain a JSON object and match the expected output of 'SchoolIncomeHistory.json'
        
    Scenario: Sending a valid trust income history request
        Given a valid trust income history request with company number '10192252'
        When I submit the insights income request
        Then the trust income response should be ok, contain a JSON object and match the expected output of 'TrustIncomeHistory.json'
        