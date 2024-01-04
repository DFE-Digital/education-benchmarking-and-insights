Feature: CompareYourCosts page is showing correct data

#    Background:
#        Given I am on service landing page
#        When I click start now
#        Then I am on find organization page
#        When I type 'the wells free school' in the search bar and click it
#        And I click continue
#        Then I am taken to school home page
#        When I click compare your costs CTA
#        Then I am taken to compare your costs page

    Scenario: download total expenditure chart
        Given I am on compare your costs page for school with URN '139696'
        When i click on save as image for total expenditure
        Then chart image is downloaded

    Scenario: change dimension of total expenditure and change view to table
        Given I am on compare your costs page for school with URN '139696'
        And the dimension in dimension dropdown is '£ per pupil'
        When I change total expenditure dimension to 'actuals'
        Then the chart should be updated

    Scenario: Change dimension in table view for total expenditure
        Given I am on compare your costs page for school with URN '139696'
        And I click on view as table
        And the dimension in dimension dropdown is '£ per pupil'
        When I change total expenditure dimension to 'actuals'
        Then the following is showing in the Total expenditure
          | SchoolName                                      | LocalAuthority | SchoolType          | NumberOfPupils | Amount  |
          | Good Shepherd Catholic School                   | 331            | Academy converter   | 225            | 5351.11 |
          | Sandfield Primary School                        | 936            | Academy sponsor led | 210            | 5185.71 |
          | St Edward's Catholic Primary School - Kettering | 940            | Academy converter   | 183            | 5398.91 |
          | Ashbrook School                                 | 826            | Academy converter   | 176            | 5971.59 |
          | St Joseph's Catholic Primary School, Moorthorpe | 384            | Academy sponsor led | 199            | 5241.21 |
          | St Joseph's Catholic Primary School, Banbury    | 931            | Academy converter   | 204.5          | 5677.26 |
          | Wells Free School                               | 886            | Free school         | 183            | 5693.99 |
          | Braybrook Primary Academy                       | 874            | Academy converter   | 208            | 5701.92 |
          | St Thomas Cantilupe Cofe Academy                | 884            | Academy sponsor led | 226            | 5181.42 |
          | Robin Hood Primary And Nursery School           | 314            | Academy converter   | 181            | 7143.65 |
          | St Gregory's Catholic Primary School            | 937            | Academy converter   | 202            | 5024.75 |
          | Elm Road Primary School                         | 873            | Academy converter   | 206            | 5218.45 |
          | Horninglow Primary: A De Ferrers Trust Academy  | 860            | Academy sponsor led | 204            | 5833.33 |
          | Green Oaks Primary Academy                      | 941            | Academy sponsor led | 217            | 5829.49 |
          | St George's Primary School                      | 810            | Academy converter   | 222            | 6216.22 |
        And Save as image CTA is not showing

    Scenario: Show all CTA should expand all accordions
        Given I am on compare your costs page for school with URN '139696'
        When I click on Show all sections
        Then all accordions on the page are expanded
        And Save as image CTA is visible

    Scenario: change all charts to table view
        Given I am on compare your costs page for school with URN '139696'
        And I click on show all sections
        When I click on view as table
        Then all accordions are showing table view
        And Save as image CTA is visible

    Scenario: Hide single accordion in table view
        Given I am on compare your costs page for school with URN '139696'
        And I click on show all sections
        And I click on view as table
        When I click hide for non educational support staff
        Then the accordion non educational support staff is collapsed

    Scenario: Hide single accordion in chart view
        Given I am on compare your costs page for school with URN '139696'
        And I click on show all sections
        When I click hide for teaching and teaching support staff
        Then the accordion teaching and teaching support staff is collapsed

    Scenario: hide all sections closes all accordions
        Given I am on compare your costs page for school with URN '139696'
        And I click on show all sections
        When I click on hide all sections
        Then all accordions on the page are collapsed