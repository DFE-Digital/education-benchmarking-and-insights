Feature: Chart rendering horizontal bar chart endpoint

    Scenario: Sending a valid single horizontal bar chart request returns the correct HTML for currency values
        Given a single horizontal bar chart request with accept header 'application/json', highlighted item '123456', sort 'asc', width '500', bar height '20', id 'test-uuid', valueType 'currency' and the following data:
          | Key    | Value   |
          | 123450 | 1000001 |
          | 123451 | 1000111 |
          | 123452 | 1000011 |
          | 123453 | 1011111 |
          | 123454 | 1000000 |
          | 123455 | 1000000 |
          | 123456 | 1111111 |
          | 123457 | 1001001 |
          | 123458 | 1010001 |
          | 123459 | 1100001 |
          | 123460 | 0       |
          | 123461 |         |
          | 123462 | 123.45  |
          | 123463 | 67.89   |
        When I submit the horizontal bar chart request
        Then the response should be ok, contain a JSON object and match the expected output of 'HorizontalBarChartValidSingleCurrency.json'

    Scenario: Sending a valid single horizontal bar chart request returns the correct HTML for percentage value
        Given a single horizontal bar chart request with accept header 'application/json', highlighted item '123456', sort 'asc', width '500', bar height '20', id 'test-uuid', valueType 'percent' and the following data:
          | Key    | Value  |
          | 123450 | 100    |
          | 123451 | 99     |
          | 123452 | 80     |
          | 123453 | 55     |
          | 123454 | 44     |
          | 123455 | 1      |
          | 123456 | 67     |
          | 123457 | 89     |
          | 123458 | 48     |
          | 123459 | 32     |
          | 123460 | 0      |
          | 123461 |        |
          | 123462 | 123.45 |
          | 123463 | 67.89  |
        When I submit the horizontal bar chart request
        Then the response should be ok, contain a JSON object and match the expected output of 'HorizontalBarChartValidSinglePercent.json'

    Scenario: Sending a valid single horizontal bar chart request with accept header image/svg+xml returns the correct HTML only
        Given a single horizontal bar chart request with accept header 'image/svg+xml', highlighted item '123456', sort 'asc', width '500', bar height '20', id 'test-uuid', valueType 'currency' and the following data:
          | Key    | Value   |
          | 123450 | 1000001 |
          | 123451 | 1000111 |
          | 123452 | 1000011 |
          | 123453 | 1011111 |
          | 123454 | 1000000 |
          | 123455 | 1000000 |
          | 123456 | 1111111 |
          | 123457 | 1001001 |
          | 123458 | 1010001 |
          | 123459 | 1100001 |
          | 123460 | 0       |
          | 123461 |         |
          | 123462 | 123.45  |
          | 123463 | 67.89   |
        When I submit the horizontal bar chart request
        Then the response should be ok, contain an SVG document and match the expected output of 'HorizontalBarChartValidSingleCurrency.svg'

    Scenario: Sending a valid single horizontal bar chart request with missing data label and accept header image/svg+xml returns the correct HTML only
        Given a single horizontal bar chart request with accept header 'image/svg+xml' and request input from 'HorizontalBarChartValidSingleCurrencyMissingDataLabelRequest.json'
        When I submit the horizontal bar chart request
        Then the response should be ok, contain an SVG document and match the expected output of 'HorizontalBarChartValidSingleCurrencyMissingDataLabel.svg'

    Scenario: Sending a valid single horizontal bar chart request with custom domain and accept header image/svg+xml returns the correct HTML only
        Given a single horizontal bar chart request with accept header 'image/svg+xml' and request input from 'HorizontalBarChartValidSingleCurrencyCustomDomainRequest.json'
        When I submit the horizontal bar chart request
        Then the response should be ok, contain an SVG document and match the expected output of 'HorizontalBarChartValidSingleCurrencyCustomDomain.svg'

    Scenario: Sending a valid single horizontal bar chart request without SI units with accept header image/svg+xml returns the correct HTML only
        Given a single horizontal bar chart request with accept header 'image/svg+xml', highlighted item '123456', sort 'asc', width '500', bar height '20', id 'test-uuid', valueType 'currency' and the following data:
          | Key    | Value  |
          | 123450 | 100    |
          | 123451 | 999    |
          | 123452 | 80     |
          | 123453 | 55     |
          | 123454 | 44     |
          | 123455 | 1      |
          | 123456 | 67     |
          | 123457 | 89     |
          | 123458 | 48     |
          | 123459 | 32     |
          | 123460 | 0      |
          | 123461 |        |
          | 123462 | 123.45 |
          | 123463 | 67.89  |
        When I submit the horizontal bar chart request
        Then the response should be ok, contain an SVG document and match the expected output of 'HorizontalBarChartValidSingleCurrencyNoSiUnits.svg'

    Scenario: Sending a valid multiple horizontal bar chart request returns the correct HTML for currency values
        Given multiple horizontal bar chart requests with the following data:
          | Id | Highlight | Sort | Width | BarHeight | ValueType | Data                                                                                                                                                                                                                                                                                                                                                                                                                          |
          | 1  | 123456    | asc  | 500   | 20        | currency  | [ { "Key": "123450", "Value": "1000001" }, { "Key": "123451", "Value": "1000111" }, { "Key": "123452", "Value": "1000011" }, { "Key": "123453", "Value": "1011111" }, { "Key": "123454", "Value": "1000000" }, { "Key": "123455", "Value": "1000000"  }, { "Key": "123456", "Value": "1111111" }, { "Key": "123457", "Value": "1001001" }, { "Key": "123458", "Value": "1010001" }, { "Key": "123459", "Value": "1100001" } ] |
          | 2  | 123456    | desc | 400   | 20        | currency  | [ { "Key": "123456", "Value": "1111111" }, { "Key": "123457", "Value": "1001001" }, { "Key": "123458", "Value": "1010001" }, { "Key": "123459", "Value": "1100001" } ]                                                                                                                                                                                                                                                        |
        When I submit the horizontal bar chart request
        Then the response should be ok, contain a JSON array and match the expected output of 'HorizontalBarChartValidMultipleCurrency.json'

    Scenario: Sending a valid multiple horizontal bar chart request returns the correct HTML for percentage values
        Given multiple horizontal bar chart requests with the following data:
          | Id | Highlight | Sort | Width | BarHeight | ValueType | Data                                                                                                                                                                                                                                                                                                                                                                         |
          | 1  | 123456    | asc  | 500   | 20        | percent   | [ { "Key": "123450", "Value": "100" }, { "Key": "123451", "Value": "50" }, { "Key": "123452", "Value": "60" }, { "Key": "123453", "Value": "22" }, { "Key": "123454", "Value": "70" }, { "Key": "123455", "Value": "81"  }, { "Key": "123456", "Value": "65" }, { "Key": "123457", "Value": "44" }, { "Key": "123458", "Value": "55" }, { "Key": "123459", "Value": "23" } ] |
          | 2  | 123456    | desc | 400   | 20        | percent   | [ { "Key": "123456", "Value": "90" }, { "Key": "123457", "Value": "50" }, { "Key": "123458", "Value": "75" }, { "Key": "123459", "Value": "33" } ]                                                                                                                                                                                                                           |
        When I submit the horizontal bar chart request
        Then the response should be ok, contain a JSON array and match the expected output of 'HorizontalBarChartValidMultiplePercent.json'

    Scenario: Sending an invalid single horizontal bar chart request without data returns bad request
        Given a single horizontal bar chart request with accept header 'application/json', highlighted item '123456', sort 'asc', width '500', bar height '20', id 'test-uuid', valueType 'currency' and the following data:
          | Key | Value |
        When I submit the horizontal bar chart request
        Then the chart response should be bad request, contain a JSON object and match the expected output of 'HorizontalBarChartInvalidSingleNoData.json'

    Scenario: Sending an invalid multiple horizontal bar chart request without Ids returns bad request
        Given multiple horizontal bar chart requests with the following data:
          | Id | Highlight | Sort | Width | BarHeight | ValueType | Data                                                                                                                                                                                                                                                                                                                                                                                                                          |
          |    | 123456    | asc  | 500   | 20        | currency  | [ { "Key": "123450", "Value": "1000001" }, { "Key": "123451", "Value": "1000111" }, { "Key": "123452", "Value": "1000011" }, { "Key": "123453", "Value": "1011111" }, { "Key": "123454", "Value": "1000000" }, { "Key": "123455", "Value": "1000000"  }, { "Key": "123456", "Value": "1111111" }, { "Key": "123457", "Value": "1001001" }, { "Key": "123458", "Value": "1010001" }, { "Key": "123459", "Value": "1100001" } ] |
          |    | 123456    | desc | 400   | 20        | currency  | [ { "Key": "123456", "Value": "1111111" }, { "Key": "123457", "Value": "1001001" }, { "Key": "123458", "Value": "1010001" }, { "Key": "123459", "Value": "1100001" } ]                                                                                                                                                                                                                                                        |
        When I submit the horizontal bar chart request
        Then the chart response should be bad request, contain a JSON object and match the expected output of 'HorizontalBarChartInvalidMultipleNoId.json'

    Scenario: Sending an invalid multiple horizontal bar chart request without data returns bad request
        Given multiple horizontal bar chart requests with the following data:
          | Id | Highlight | Sort | Width | BarHeight | ValueType | Data |
          | 1  | 123456    | asc  | 500   | 20        | currency  | [ ]  |
          | 2  | 123456    | desc | 400   | 20        | currency  | [ ]  |
        When I submit the horizontal bar chart request
        Then the chart response should be bad request, contain a JSON object and match the expected output of 'HorizontalBarChartInvalidMultipleNoData.json'

    Scenario: Sending an invalid multiple horizontal bar chart request without chart definitions returns bad request
        Given multiple horizontal bar chart requests with the following data:
          | Id | Highlight | Sort | Width | Height | ValueType | Data |
        When I submit the horizontal bar chart request
        Then the chart response should be bad request, contain a JSON object and match the expected output of 'HorizontalBarChartInvalidMultipleNoChartDefinitions.json'