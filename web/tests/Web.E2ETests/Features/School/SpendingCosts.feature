Feature: School spending and costs

    Scenario: View how we choose similar school details
        Given I am on spending and costs page for school with URN '101241'
        And the order of charts is
          | Name                               |
          | Teaching and teaching supply staff |
          | Administrative supplies            |
          | Catering staff and services        |
          | Educational ICT                    |
          | Educational supplies               |
          | Non-educational support staff      |
          | Other                              |
          | Premises staff and services        |
          | Utilities                          |
        When I click on how we choose similar schools
        Then the details section is expanded

    Scenario Outline: Click on view all links for each chart
        Given I am on spending and costs page for school with URN '118168'
        And the order of charts is
          | Name                               |
          | Teaching and teaching supply staff |
          | Administrative supplies            |
          | Catering staff and services        |
          | Educational ICT                    |
          | Educational supplies               |
          | Non-educational support staff      |
          | Other                              |
          | Premises staff and services        |
          | Utilities                          |
        When I click on '<Chart>' 
        Then I am directed to compare your costs page
        And the accordion  '<Example>'is expanded
