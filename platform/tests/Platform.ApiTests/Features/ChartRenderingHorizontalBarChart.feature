Feature: Chart rendering horizontal bar chart endpoint

    Scenario: Sending a valid single horizontal bar chart request returns the correct HTML
        Given a single horizontal bar chart request with accept header 'application/json', highlighted item '123456', sort 'asc', width '500', bar height '20' and the following data:
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
        When I submit the horizontal bar chart request
        Then the chart response should contain a single chart with the expected properties:
          | Id | RectCount | HighlightIndex | Width | Height |
          |    | 10        | 9              | 500   | 242    |

    Scenario: Sending a valid single horizontal bar chart request with accept header image/svg+xml returns the correct HTML only
        Given a single horizontal bar chart request with accept header 'image/svg+xml', highlighted item '123456', sort 'asc', width '500', bar height '20' and the following data:
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
        When I submit the horizontal bar chart request
        Then the response should be SVG with the expected properties:
          | RectCount | HighlightIndex | Width | Height |
          | 10        | 9              | 500   | 242    |

    Scenario: Sending a valid multiple horizontal bar chart request returns the correct HTML
        Given multiple horizontal bar chart requests with the following data:
          | Id | Highlight | Sort | Width | BarHeight | Data                                                                                                                                                                                                                                                                                                                                                                                                                          |
          | 1  | 123456    | asc  | 500   | 20        | [ { "Key": "123450", "Value": "1000001" }, { "Key": "123451", "Value": "1000111" }, { "Key": "123452", "Value": "1000011" }, { "Key": "123453", "Value": "1011111" }, { "Key": "123454", "Value": "1000000" }, { "Key": "123455", "Value": "1000000"  }, { "Key": "123456", "Value": "1111111" }, { "Key": "123457", "Value": "1001001" }, { "Key": "123458", "Value": "1010001" }, { "Key": "123459", "Value": "1100001" } ] |
          | 2  | 123456    | desc | 400   | 20        | [ { "Key": "123456", "Value": "1111111" }, { "Key": "123457", "Value": "1001001" }, { "Key": "123458", "Value": "1010001" }, { "Key": "123459", "Value": "1100001" } ]                                                                                                                                                                                                                                                        |
        When I submit the horizontal bar chart request
        Then the chart response should contain multiple charts with the expected properties:
          | Id | RectCount | HighlightIndex | Width | Height |
          | 1  | 10        | 9              | 500   | 242    |
          | 2  | 4         | 0              | 400   | 122    |

    Scenario: Sending an invalid single horizontal bar chart request without data returns bad request
        Given a single horizontal bar chart request with accept header 'application/json', highlighted item '123456', sort 'asc', width '500', bar height '20' and the following data:
          | Key | Value |
        When I submit the horizontal bar chart request
        Then the chart response should be bad request with the following errors:
          | Error              |
          | Missing chart data |

    Scenario: Sending an invalid multiple horizontal bar chart request without Ids returns bad request
        Given multiple horizontal bar chart requests with the following data:
          | Id | Highlight | Sort | Width | BarHeight | Data                                                                                                                                                                                                                                                                                                                                                                                                                          |
          |    | 123456    | asc  | 500   | 20        | [ { "Key": "123450", "Value": "1000001" }, { "Key": "123451", "Value": "1000111" }, { "Key": "123452", "Value": "1000011" }, { "Key": "123453", "Value": "1011111" }, { "Key": "123454", "Value": "1000000" }, { "Key": "123455", "Value": "1000000"  }, { "Key": "123456", "Value": "1111111" }, { "Key": "123457", "Value": "1001001" }, { "Key": "123458", "Value": "1010001" }, { "Key": "123459", "Value": "1100001" } ] |
          |    | 123456    | desc | 400   | 20        | [ { "Key": "123456", "Value": "1111111" }, { "Key": "123457", "Value": "1001001" }, { "Key": "123458", "Value": "1010001" }, { "Key": "123459", "Value": "1100001" } ]                                                                                                                                                                                                                                                        |
        When I submit the horizontal bar chart request
        Then the chart response should be bad request with the following errors:
          | Error                           |
          | Missing id for chart at index 0 |
          | Missing id for chart at index 1 |

    Scenario: Sending an invalid multiple horizontal bar chart request without data returns bad request
        Given multiple horizontal bar chart requests with the following data:
          | Id | Highlight | Sort | Width | BarHeight | Data |
          | 1  | 123456    | asc  | 500   | 20        | [ ]  |
          | 2  | 123456    | desc | 400   | 20        | [ ]  |
        When I submit the horizontal bar chart request
        Then the chart response should be bad request with the following errors:
          | Error                    |
          | Missing chart data for 1 |
          | Missing chart data for 2 |

    Scenario: Sending an invalid multiple horizontal bar chart request without chart definitions returns bad request
        Given multiple horizontal bar chart requests with the following data:
          | Id | Highlight | Sort | Width | Height | Data |
        When I submit the horizontal bar chart request
        Then the chart response should be bad request with the following errors:
          | Error                     |
          | Missing chart definitions |