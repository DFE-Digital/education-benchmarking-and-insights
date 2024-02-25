Feature: School compare your costs

    Scenario: Download total expenditure chart
        Given I am on compare your costs page for school with URN '139696'
        When I click on save as image for 'total expenditure'
        Then the 'total expenditure' chart image is downloaded       

    Scenario: Change dimension of total expenditure and change view to table
        Given I am on compare your costs page for school with URN '139696'
        And the 'total expenditure' dimension is '£ per pupil'
        When I change 'total expenditure' dimension to 'actuals'
        Then the 'total expenditure' dimension is 'actuals'

    Scenario: Change dimension in table view for total expenditure
        Given I am on compare your costs page for school with URN '139696'
        And table view is selected
        And the 'total expenditure' dimension is '£ per pupil'
        When I change 'total expenditure' dimension to 'actuals'
        Then the following is shown for 'total expenditure'
          | School name                                     | Local Authority | School type         | Number of pupils | Amount     |
          | St George's Primary School                      | 810             | Academy converter   | 222              | 1380000.00 |
          | Robin Hood Primary And Nursery School           | 314             | Academy converter   | 181              | 1293000.00 |
          | Green Oaks Primary Academy                      | 941             | Academy sponsor led | 217              | 1265000.00 |
          | Good Shepherd Catholic School                   | 331             | Academy converter   | 225              | 1204000.00 |
          | Horninglow Primary: A De Ferrers Trust Academy  | 860             | Academy sponsor led | 204              | 1190000.00 |
          | Braybrook Primary Academy                       | 874             | Academy converter   | 208              | 1186000.00 |
          | St Thomas Cantilupe Cofe Academy                | 884             | Academy sponsor led | 226              | 1171000.00 |
          | St Joseph's Catholic Primary School, Banbury    | 931             | Academy converter   | 204.5            | 1161000.00 |
          | Sandfield Primary School                        | 936             | Academy sponsor led | 210              | 1089000.00 |
          | Elm Road Primary School                         | 873             | Academy converter   | 206              | 1075000.00 |
          | Ashbrook School                                 | 826             | Academy converter   | 176              | 1051000.00 |
          | St Joseph's Catholic Primary School, Moorthorpe | 384             | Academy sponsor led | 199              | 1043000.00 |
          | Wells Free School                               | 886             | Free school         | 183              | 1042000.00 |
          | St Gregory's Catholic Primary School            | 937             | Academy converter   | 202              | 1015000.00 |         
          | St Edward's Catholic Primary School - Kettering | 940             | Academy converter   | 183              | 988000.00  |
        But save as image buttons are hidden

    Scenario: Show all should expand all sections
        Given I am on compare your costs page for school with URN '139696'
        When I click on show all sections
        Then all sections on the page are expanded
        And the show all text changes to hide all sections
        
    Scenario: Change all charts to table view
        Given I am on compare your costs page for school with URN '139696'
        And all sections are shown
        When I click on view as table
        Then all sections on the page are expanded
        And are showing table view
        But save as image buttons are hidden

    Scenario: Hide single section
        Given I am on compare your costs page for school with URN '139696'
        And all sections are shown
        When I click section link for 'non educational support staff'
        Then the section 'non educational support staff' is hidden
