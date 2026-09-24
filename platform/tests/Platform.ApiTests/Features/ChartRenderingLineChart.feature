Feature: Chart rendering line chart endpoint

    Scenario: Sending a valid single line chart request with no optional properties
        Given a 'single' line chart request with accept header 'image/svg+xml' and request input from 'ValidSingleRequest.json'
        When I submit the request
        Then the response should be 'ok', contain 'an SVG document' and match the expected output of 'ValidSingleResult.svg'

    Scenario: Sending a valid single line chart request with value dots
        Given a 'single' line chart request with accept header 'image/svg+xml' and request input from 'ValidSingleRequestValueDots.json'
        When I submit the request
        Then the response should be 'ok', contain 'an SVG document' and match the expected output of 'ValidSingleResultValueDots.svg'

    Scenario: Sending a valid single line chart request with value labels
        Given a 'single' line chart request with accept header 'image/svg+xml' and request input from 'ValidSingleRequestValueLabels.json'
        When I submit the request
        Then the response should be 'ok', contain 'an SVG document' and match the expected output of 'ValidSingleResultValueLabels.svg'

    Scenario: Sending a valid single line chart request with x axis label
        Given a 'single' line chart request with accept header 'image/svg+xml' and request input from 'ValidSingleRequestXAxisLabel.json'
        When I submit the request
        Then the response should be 'ok', contain 'an SVG document' and match the expected output of 'ValidSingleResultXAxisLabel.svg'

    Scenario: Sending a valid single line chart request with value dots, value labels, and x axis label
        Given a 'single' line chart request with accept header 'image/svg+xml' and request input from 'ValidSingleRequestValueDotsValueLabelsXAxisLabel.json'
        When I submit the request
        Then the response should be 'ok', contain 'an SVG document' and match the expected output of 'ValidSingleResultValueDotsValueLabelsXAxisLabel.svg'

    Scenario: Sending a valid multiple line chart request with no optional properties
        Given a 'multiple' line chart request with accept header 'application/json' and request input from 'ValidMultipleRequest.json'
        When I submit the request
        Then the response should be 'ok', contain 'a json array' and match the expected output of 'ValidMultipleResult.json'

    Scenario: Sending a valid multiple line chart request with value dots
        Given a 'multiple' line chart request with accept header 'application/json' and request input from 'ValidMultipleRequestValueDots.json'
        When I submit the request
        Then the response should be 'ok', contain 'a json array' and match the expected output of 'ValidMultipleResultValueDots.json'

    Scenario: Sending a valid multiple line chart request with value labels
        Given a 'multiple' line chart request with accept header 'application/json' and request input from 'ValidMultipleRequestValueLabels.json'
        When I submit the request
        Then the response should be 'ok', contain 'a json array' and match the expected output of 'ValidMultipleResultValueLabels.json'

    Scenario: Sending a valid multiple line chart request with x axis label
        Given a 'multiple' line chart request with accept header 'application/json' and request input from 'ValidMultipleRequestXAxisLabel.json'
        When I submit the request
        Then the response should be 'ok', contain 'a json array' and match the expected output of 'ValidMultipleResultXAxisLabel.json'

    Scenario: Sending a valid multiple line chart request with value dots, value labels, and x axis label
        Given a 'multiple' line chart request with accept header 'application/json' and request input from 'ValidMultipleRequestValueDotsValueLabelsXAxisLabel.json'
        When I submit the request
        Then the response should be 'ok', contain 'a json array' and match the expected output of 'ValidMultipleResultValueDotsValueLabelsXAxisLabel.json'

    Scenario: Sending an invalid multiple line chart request without Ids returns bad request
        Given a 'multiple' line chart request with accept header 'application/json' and request input from 'InvalidMultipleRequestNoIds.json'
        When I submit the request
        Then the response should be 'bad request', contain 'a json object' and match the expected output of 'InvalidMultipleResultNoIds.json'
