Feature: School homepage

    Scenario: Go to contact details page
        Given I am on school homepage for school with urn '777042'
        When I click on school details
        Then the school details page is displayed

    Scenario: Go to compare your costs page
        Given I am on school homepage for school with urn '777042'
        When I click on compare your costs
        Then the compare your costs page is displayed

    Scenario: Go to curriculum and financial planning page
        Given I am on school homepage for school with urn '777042'
        When I click on curriculum and financial planning
        Then the curriculum and financial planning page is displayed

    Scenario: Go to benchmark census data page
        Given I am on school homepage for school with urn '777042'
        When I click on benchmark census data
        Then the benchmark census page is displayed

    Scenario: Go to view all spending and costs page
        Given I am on school homepage for school with urn '777042'
        When I click on view all spending and costs
        Then the spending and costs page is displayed

    Scenario: Goto view historic data page
        Given I am on school homepage for school with urn '777042'
        When I click on view historic data
        Then the historic data page is displayed

    Scenario: Goto find ways to spend less page
        Given I am on school homepage for school with urn '777042'
        When I click on find ways to spend less
        Then the commercial resources page is displayed

    Scenario: visit pages for school that is closed down during the academic year
        Given I am on school homepage for school with urn 'urn here'
        And I can see the school closed down banner
        And Rag rating is not displayed on the page
        When I visit the '<pages>'
        Then I can see the school closed down banner

    Examples:
      | keyword                 |
      | contact details page    |
      | compare your costs page |
      | add more pages here     |

    Scenario: visit pages for school that has converted to an academy
        Given I am on school homepage for school with urn 'urn here'
        And I can see the part year school banner
        And Rag rating is not displayed on the page
        When I visit the '<pages>'
        Then I can see the part year school banner

    Examples:
      | keyword                 |
      | contact details page    |
      | compare your costs page |
      | add more pages here     |

    Scenario: visit pages for newly established school
        Given I am on school homepage for school with urn 'urn here'
        And I can see the part year school banner
        And Rag rating is not displayed on the page
        When I visit the '<pages>'
        Then I can see the part year school banner

    Examples:
      | keyword                 |
      | contact details page    |
      | compare your costs page |
      | add more pages here     |

    Scenario: Goto compare your costs page for part year school which does not have FSM, SEN and CDC data
        Given I am on school homepage for school with urn 'urn here'
        When I click on compare your costs
        Then the compare your costs page is displayed
        And there are no school or building comparators

    Scenario: Goto compare your costs page for part year school which have FSM, SEN and CDC data
        Given I am on school homepage for school with urn 'urn here'
        When I click on compare your costs
        Then the compare your costs page is displayed
        And there are school and building comparator created