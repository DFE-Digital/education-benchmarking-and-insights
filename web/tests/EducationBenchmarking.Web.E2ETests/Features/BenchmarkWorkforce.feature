Feature: SchoolWorkforce
Benchmark the workforce page is showing correct data

    Scenario: download school workforce chart
        Given I am on workforce page for school with URN '139696'
        When i click on save as image for school workforce
        Then school workforce chart image is downloaded

    Scenario: change dimension of school workforce
        Given I am on workforce page for school with URN '139696'
        When I change school workforce dimension to 'pupils per staff role'
        Then the dimension in school workforce dimension dropdown is 'pupils per staff role'

    Scenario Outline: Change dimension in table view for Total number of teachers
        Given I am on workforce page for school with URN '139696'
        And I click on view as table on workforce page
        When I change Total number of teachers dimension to '<dimension>'
        Then the following header in the Total number of teachers table
          | School name | Local Authority | School type | Number of pupils | <ColumnHeader> |

        Examples:
          | dimension               | ColumnHeader          |
          | total                   | Count                 |
          | headcount per FTE       | Ratio                 |
          | percentage of workforce | Percentage            |
          | pupils per staff role   | Pupils per staff role |

    Scenario: Change view from charts to table
        Given I am on workforce page for school with URN '139696'
        When I click on view as table on workforce page
        Then the table view is showing on workforce page
        When I click on view as chart on workforce page
        Then chart view is showing on workforce page