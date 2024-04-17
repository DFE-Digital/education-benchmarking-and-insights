@ignore
Feature: Establishment trusts endpoints

    Scenario: Sending a valid trust request
        Given a valid trust request with id '123'
        When I submit the trusts request
        Then the trust result should be ok

    Scenario: Sending a valid query trusts request
        Given a valid trusts query request
        When I submit the trusts request
        Then the trusts query result should be ok

    Scenario: Sending a valid search trusts request
        Given a valid trusts search request
        When I submit the trusts request
        Then the trusts search result should be ok

    Scenario: Sending a valid suggest trusts request
        Given a valid trusts suggest request
        When I submit the trusts request
        Then the trusts suggest result should be:
          | Text                                 | Name                               | CompanyNumber |
          | *Trusted* Schools' Partnership       | Trusted Schools' Partnership       | 9617166       |
          | Equals *Trust*                       | Equals Trust                       | 10279606      |
          | Central Learning Partnership *Trust* | Central Learning Partnership Trust | 7827368       |
          | Suffolk Academies *Trust*            | Suffolk Academies Trust            | 9702333       |
          | The CSIA *Trust*                     | The CSIA Trust                     | 7551989       |
          | The Praxis *Trust*                   | The Praxis Trust                   | 7972070       |
          | Durrington Multi Academy *Trust*     | Durrington Multi Academy Trust     | 8895870       |
          | Haileybury Academy *Trust*           | Haileybury Academy Trust           | 9659808       |
          | Edmonton Academy *Trust*             | Edmonton Academy Trust             | 10311383      |
          | The Mast Academy *Trust*             | The Mast Academy Trust             | 10357163      |

    Scenario: Sending an invalid suggest trusts request returns the validation results
        Given an invalid trusts suggest request
        When I submit the trusts request
        Then the trusts suggest result should have the follow validation errors:
          | PropertyName  | ErrorMessage                                 |
          | SuggesterName | 'Suggester Name' must not be empty.          |
          | SearchText    | 'Search Text' must not be empty.             |
          | Size          | 'Size' must be greater than or equal to '5'. |