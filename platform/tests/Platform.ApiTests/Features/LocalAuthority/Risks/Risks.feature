Feature: Local Authority Risks

    Scenario Outline: Valid paged school risks request
        Given a paged school risks request with codes:
            | Code    |
            | <code1> |
            | <code2> |
        And parameters:
            | Page   | PageSize   | SortField   | SortOrder   | Phase   |
            | <page> | <pageSize> | <sortField> | <sortOrder> | <phase> |
        When I submit the request
        Then the result should be ok and match the expected output of '<result>'

    Examples:
        | code1 | code2 | page | pageSize | sortField | sortOrder | phase   | result                                                 |
        |   201 | 202   |      | 1        | URN       | ASC       | Primary | TwoCodesSortedByUrnAscendingPrimaryPage1.json          |
        |   201 |       |      |          | Financial | DESC      |         | SingleCodeSortedByFinancialDescendingDefaultPage.json  |
        |   202 | 201   | 2    | 1        |           | ASC       | Primary | TwoCodesSortedByDefaultAscendingPrimaryPage2.json      |
        |   202 |       |      |          | URN       |           |         | SingleCodeSortedByUrnDefaultOrderDefaultPage.json      |
        |   201 | 202   |      | 1        | Financial |           | Primary | TwoCodesSortedByFinancialDefaultOrderPrimaryPage1.json |
        |   202 | 202   |      |          |           | DESC      |         | TwoCodesSortedByDefaultDescendingDefaultPage.json      |

    Scenario Outline: Invalid paged school risks request
        Given a paged school risks request with codes:
            | Code    |
            | <code1> |
            | <code2> |
        And parameters:
            | Page   | PageSize   | SortField   | SortOrder   | Phase   |
            | <page> | <pageSize> | <sortField> | <sortOrder> | <phase> |
        When I submit the request
        Then the result should be bad request and match the expected output of '<result>'

        Examples:
            | code1 | code2 | page | pageSize | sortField | sortOrder | phase   | result                |
            |       |       |    1 |        1 | URN       | ASC       | Primary | InvalidNoCodes.json   |
            | 201   |       |    1 |        1 | invalid   | ASC       | Primary | InvalidSortField.json |
            | 201   |       |    1 |        1 | URN       | invalid   | Primary | InvalidSortOrder.json |
            | 201   |       |    1 |        1 | URN       | ASC       | invalid | InvalidPhase.json     |
            | 201   |       |    1 |        0 | URN       | ASC       | Primary | InvalidPageSize.json  |
            | 201   |       |    0 |        1 | URN       | ASC       | Primary | InvalidPage.json      |

    Scenario Outline: Invalid paged school risks request with too many codes
        Given a paged school risks request with too many codes
        When I submit the request
        Then the result should be bad request and match the expected output of 'InvalidTooManyCodes.json'
