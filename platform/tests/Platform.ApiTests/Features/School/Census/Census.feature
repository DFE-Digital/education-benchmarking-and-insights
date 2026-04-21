Feature: School Census

  Scenario: Get senior leadership data with valid URNs and Dimension
    Given a valid senior leadership request with URNs '777042,654321' and dimension 'Total'
    When I submit the school census request
    Then the 'senior leadership' result should be 'ok' and match the expected output in 'senior-leadership.json'

  Scenario: Get senior leadership data with valid URNs and no Dimension (default)
    Given a valid senior leadership request with URNs '777042,654321' and no dimension
    When I submit the school census request
    Then the 'senior leadership' result should be 'ok' and match the expected output in 'senior-leadership-default.json'

  Scenario Outline: Get senior leadership data returns bad request for invalid parameters
    Given an invalid senior leadership request with <parameter>
    When I submit the school census request
    Then the 'senior leadership' result should be 'bad request' and match the expected output in '<expected_file>'

    Examples:
      | parameter             | expected_file                        |
      | invalid api version   | bad-request-invalid-version-senior.json |
      | missing URNs          | bad-request-missing-urns-senior.json    |
      | unrecognized dimension| bad-request-invalid-dimension-senior.json |

  Scenario: Get school census with valid URN
    Given a valid school census request with URN '777042'
    When I submit the school census request
    Then the 'school census' result should be 'ok' and match the expected output in 'school-census.json'

  Scenario Outline: Get school census returns bad request or not found
    Given an invalid school census request with <parameter>
    When I submit the school census request
    Then the 'school census' result should be '<status>' and match the expected output in '<expected_file>'

    Examples:
      | parameter           | status      | expected_file                    |
      | invalid api version | bad request | bad-request-invalid-version-single.json |
      | non-existent URN    | not found   | not-found-single.json            |

  Scenario: Get school census history with valid parameters
    Given a valid school census history request with URN '777042', category 'WorkforceFte', and dimension 'Total'
    When I submit the school census request
    Then the 'school census history' result should be 'ok' and match the expected output in 'school-census-history.json'

  Scenario Outline: Get school census history returns bad request or not found
    Given an invalid school census history request with <parameter>
    When I submit the school census request
    Then the 'school census history' result should be '<status>' and match the expected output in '<expected_file>'

    Examples:
      | parameter             | status      | expected_file                       |
      | invalid api version   | bad request | bad-request-invalid-version-history.json |
      | unrecognized category | bad request | bad-request-invalid-category-history.json |
      | unrecognized dimension| bad request | bad-request-invalid-dimension-history.json |
      | non-existent URN      | not found   | not-found-history.json              |

  Scenario: Get comparator set average census history with valid parameters
    Given a valid comparator set average census history request with URN '777042', category 'WorkforceFte', and dimension 'Total'
    When I submit the school census request
    Then the 'comparator set average history' result should be 'ok' and match the expected output in 'comparator-set-average.json'

  Scenario Outline: Get comparator set average census history returns bad request or not found
    Given an invalid comparator set average census history request with <parameter>
    When I submit the school census request
    Then the 'comparator set average history' result should be '<status>' and match the expected output in '<expected_file>'

    Examples:
      | parameter             | status      | expected_file                       |
      | invalid api version   | bad request | bad-request-invalid-version-comparator.json |
      | unrecognized category | bad request | bad-request-invalid-category-comparator.json |
      | unrecognized dimension| bad request | bad-request-invalid-dimension-comparator.json |
      | non-existent URN      | not found   | not-found-comparator.json           |

  Scenario: Get national average census history with valid parameters
    Given a valid national average census history request with dimension 'Total', phase 'Primary', and financeType 'Maintained'
    When I submit the school census request
    Then the 'national average history' result should be 'ok' and match the expected output in 'national-average.json'

  Scenario: Get national average census history yields no data
    Given a valid national average census history request with criteria yielding no data
    When I submit the school census request
    Then the 'national average history' result should be 'ok' and match the expected output in 'national-average-empty.json'

  Scenario Outline: Get national average census history returns bad request
    Given an invalid national average census history request with <parameter>
    When I submit the school census request
    Then the 'national average history' result should be 'bad request' and match the expected output in '<expected_file>'

    Examples:
      | parameter               | expected_file                          |
      | invalid api version     | bad-request-invalid-version-national.json |
      | unrecognized dimension  | bad-request-invalid-dimension-national.json |
      | unrecognized phase      | bad-request-invalid-phase-national.json |
      | unrecognized financeType| bad-request-invalid-finance-type-national.json |

  Scenario: Get school census collection with valid URNs
    Given a valid census collection request with URNs '777042,654321', category 'WorkforceFte', and dimension 'Total'
    When I submit the school census request
    Then the 'census collection' result should be 'ok' and match the expected output in 'census-collection-urns.json'

  Scenario: Get school census collection with valid CompanyNumber and Phase
    Given a valid census collection request with CompanyNumber '12345678', phase 'Primary', category 'WorkforceFte', and dimension 'Total'
    When I submit the school census request
    Then the 'census collection' result should be 'ok' and match the expected output in 'census-collection-company.json'

  Scenario: Get school census collection with valid LaCode and Phase
    Given a valid census collection request with LaCode '123', phase 'Primary', category 'WorkforceFte', and dimension 'Total'
    When I submit the school census request
    Then the 'census collection' result should be 'ok' and match the expected output in 'census-collection-lacode.json'

  Scenario: Get school census collection yields no results
    Given a valid census collection request yielding no results
    When I submit the school census request
    Then the 'census collection' result should be 'ok' and match the expected output in 'census-collection-empty.json'

  Scenario Outline: Get school census collection returns bad request
    Given an invalid census collection request with <parameter>
    When I submit the school census request
    Then the 'census collection' result should be 'bad request' and match the expected output in '<expected_file>'

    Examples:
      | parameter                              | expected_file                                  |
      | invalid api version                    | bad-request-invalid-version-collection.json       |
      | missing URNs, CompanyNumber, and LaCode| bad-request-missing-urns-company-lacode-collection.json |
      | both CompanyNumber and LaCode          | bad-request-company-and-lacode-collection.json    |
      | missing phase                          | bad-request-missing-phase-collection.json         |
      | unrecognized category                  | bad-request-invalid-category-collection.json      |
      | unrecognized dimension                 | bad-request-invalid-dimension-collection.json     |
