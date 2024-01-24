Feature: SchoolWorkforce
Benchmark the workforce page is showing correct data

    Scenario: download school workforce chart
        Given I am on workforce page for school with URN '139696'
        When i click on save as image for school workforce
        Then school workforce chart image is downloaded

    Scenario: change dimension of school workforce
        Given I am on workforce page for school with URN '139696'
        When I change school workforce dimension to 'pupils per staff role'
        Then the dimension in 'SchoolWorkforce' dimension dropdown is 'pupils per staff role'

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
        And Save as image CTAs are not visible on workforce page
        When I click on view as chart on workforce page
        Then chart view is showing on workforce page
        
    Scenario Outline: Checking the charts dimension dropdown items
        Given I am on workforce page for school with URN '139696'
        When I click the dimension dropdown for '<chartName>'
        Then the dimension in '<chartName>' dimension dropdown is 'pupils per staff role'
        And the '<DropdownOptions>' are showing in the dimension dropdown for '<chartName>'

        Examples:
          | chartName                | DropdownOptions                                                          |
          | SchoolWorkforce          | total, headcount per FTE, pupils per staff role                          |
          | TotalNumberOfTeacher     | total, headcount per FTE, percentage of workforce, pupils per staff role |
          | SeniorLeadership         | total, headcount per FTE, percentage of workforce, pupils per staff role |
          | TeachingAssistant        | total, headcount per FTE, percentage of workforce, pupils per staff role |
          | NonClassRoomSupportStaff | total, headcount per FTE, percentage of workforce, pupils per staff role |
          | AuxiliaryStaff           | total, headcount per FTE, percentage of workforce, pupils per staff role |
          | SchoolWorkforceHeadcount | total, percentage of workforce, pupils per staff role                    |

