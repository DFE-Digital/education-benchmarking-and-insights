Feature: School historic data

	Scenario: Change dimension of total spending and costs
		Given I am on 'spending' history page for school with URN '101241'
		When I change 'spending' dimension to '£ per pupil'
		Then the 'spending' dimension is '£ per pupil'
		
	Scenario: Show all should expand all sections
		Given I am on 'spending' history page for school with URN '101241'
		When I click on show all sections on 'spending' tab
		Then all sections on 'spending' tab are expanded
		And the show all text changes to hide all sections on 'spending' tab
		And all 'spending' sub categories are displayed on the page
		
 
	Scenario: Change all charts to table view
		Given I am on 'spending' history page for school with URN '101241'
		And all sections are shown
		When I click on view as table
		Then all sections on the page are expanded
		And are showing table view
		
		
	Scenario: Hide single section
		Given I am on 'spending' history page for school with URN '101241'
		And all sections are shown
		When I click section link for 'non educational support staff'
		Then the section 'non educational support staff' is hidden