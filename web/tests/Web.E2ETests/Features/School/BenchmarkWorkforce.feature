Feature: School benchmark workforce data

    Scenario: Download school workforce chart
        Given I am on workforce page for school with URN '139696'
        When I click on save as image for 'school workforce'
        Then the 'school workforce' chart image is downloaded

    Scenario: Change dimension of school workforce
        Given I am on workforce page for school with URN '139696'
        When I change 'school workforce' dimension to 'pupils per staff role'
        Then the 'school workforce' dimension is 'pupils per staff role'

    Scenario Outline: Change dimension in table view for total number of teachers
        Given I am on workforce page for school with URN '139696'
        And table view is selected
        When I change 'total number of teachers' dimension to '<Dimension>'
        Then the following headers are displayed for 'total number of teachers'
          | School name | Local Authority | School type | Number of pupils | <Headers> |

        Examples:
          | Dimension               | Headers               |
          | total                   | Count                 |
          | headcount per FTE       | Ratio                 |
          | percentage of workforce | Percentage            |
          | pupils per staff role   | Pupils per staff role |

    Scenario: Change chart view to table view
        Given I am on workforce page for school with URN '139696'
        When I click on view as table
        Then the table view is showing
        But save as image buttons are hidden

    Scenario: Change table view to chart view
        Given I am on workforce page for school with URN '139696'
        And table view is selected
        When I click on view as chart
        Then chart view is showing
        And save as image buttons are displayed

    Scenario Outline: Checking the charts dimension dropdown items
        Given I am on workforce page for school with URN '139696'
        When I click the dimension for '<Chart>'
        Then the '<Chart>' dimension is 'pupils per staff role'
        And the dimension has '<Options>' for '<Chart>'

        Examples:
          | Chart                        | Options                                                                  |
          | school workforce             | total, headcount per FTE, pupils per staff role                          |
          | total number of teachers     | total, headcount per FTE, percentage of workforce, pupils per staff role |
          | senior leadership            | total, headcount per FTE, percentage of workforce, pupils per staff role |
          | teaching assistant           | total, headcount per FTE, percentage of workforce, pupils per staff role |
          | non class room support staff | total, headcount per FTE, percentage of workforce, pupils per staff role |
          | auxiliary staff              | total, headcount per FTE, percentage of workforce, pupils per staff role |
          | school workforce headcount   | total, pupils per staff role                                             |

    Scenario: View how we choose similar school details
        Given I am on workforce page for school with URN '139696'
        When I click on how we choose similar schools
        Then the details section is expanded