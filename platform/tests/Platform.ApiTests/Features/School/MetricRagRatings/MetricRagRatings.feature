Feature: School MetricRagRatings

    Scenario: A successful summary request with valid LaCode
        Given a valid summary request with LaCode '202'
        When I submit the School MetricRagRatings request
        Then the School MetricRagRatings result should be OK and match the expected output in 'summary-lacode-202.json'

    Scenario: A successful summary request with valid Urns
        Given a valid summary request with Urns:
            | Urn    |
            | 141014 |
            | 141015 |
        When I submit the School MetricRagRatings request
        Then the School MetricRagRatings result should be OK and match the expected output in 'summary-urns-141014-141015.json'

    Scenario: A successful summary request with valid CompanyNumber
        Given a valid summary request with CompanyNumber '06222851'
        When I submit the School MetricRagRatings request
        Then the School MetricRagRatings result should be OK and match the expected output in 'summary-companynumber-06222851.json'

    Scenario: A successful summary request with Urns and OverallPhase
        Given a valid summary request with Urns and OverallPhase 'Primary':
            | Urn    |
            | 141014 |
            | 141015 |
        When I submit the School MetricRagRatings request
        Then the School MetricRagRatings result should be OK and match the expected output in 'summary-urns-phase-primary.json'

    Scenario Outline: A summary request with validation errors
        Given a summary request with <OverallPhase>, <LaCode>, <Urns> and <CompanyNumber>
        When I submit the School MetricRagRatings request
        Then the School MetricRagRatings result should be Bad Request and match the expected output in '<ExpectedJson>'

        Examples:
            | OverallPhase | LaCode | Urns            | CompanyNumber | ExpectedJson                                   |
            | InvalidPhase |        | 141014,141015   |               | summary-invalid-overallphase-error.json        |
            |              |        |                 |               | summary-missing-identifiers-error.json         |

    Scenario: A summary request with unsupported API version
        Given a valid summary request with LaCode '202'
        And an unsupported API version '2.0'
        When I submit the School MetricRagRatings request
        Then the School MetricRagRatings result should be Bad Request and match the expected problem details in 'summary-unsupported-version.json'


    Scenario: A successful details request with Urns
        Given a valid details request with Urns:
            | Urn    |
            | 141014 |
        When I submit the School MetricRagRatings request
        Then the School MetricRagRatings result should be OK and match the expected output in 'details-urns-141014.json'

    Scenario: A successful details request with CompanyNumber
        Given a valid details request with CompanyNumber '06222851'
        When I submit the School MetricRagRatings request
        Then the School MetricRagRatings result should be OK and match the expected output in 'details-companynumber-06222851.json'

    Scenario: A successful details request with Urns, Categories and Statuses
        Given a valid details request with Categories 'Teaching and Teaching support staff,Educational ICT' and Statuses 'red,green' for Urns:
            | Urn    |
            | 141014 |
        When I submit the School MetricRagRatings request
        Then the School MetricRagRatings result should be OK and match the expected output in 'details-urns-categories-statuses.json'

    Scenario Outline: A details request with validation errors
        Given a details request with <Categories>, <Statuses>, <Urns> and <CompanyNumber>
        When I submit the School MetricRagRatings request
        Then the School MetricRagRatings result should be Bad Request and match the expected output in '<ExpectedJson>'

        Examples:
            | Categories   | Statuses | Urns   | CompanyNumber | ExpectedJson                             |
            | InvalidCat   |          | 141014 |               | details-invalid-categories-error.json    |
            |              | Invalid  | 141014 |               | details-invalid-statuses-error.json      |
            |              |          |        |               | details-missing-identifiers-error.json   |

    Scenario: A details request with unsupported API version
        Given a valid details request with Urns:
            | Urn    |
            | 141014 |
        And an unsupported API version '2.0'
        When I submit the School MetricRagRatings request
        Then the School MetricRagRatings result should be Bad Request and match the expected problem details in 'details-unsupported-version.json'
