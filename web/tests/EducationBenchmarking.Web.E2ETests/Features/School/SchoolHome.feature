Feature: School Homepage

	Scenario: Go to contact details page
		Given I am on school homepage for school with urn '139696'
		When I click on school details in resource section
		Then the school details page is displayed

	Scenario: Go to compare your costs page
		Given I am on school homepage for school with urn '139696'
		When I click on compare your costs
		Then the compare your costs page is displayed

	Scenario: Go to curriculum and financial planning page
		Given I am on school homepage for school with urn '139696'
		When I click on curriculum and financial planning
		Then the curriculum and financial planning start page is displayed
		
	Scenario: Go to benchmark workforce data page 
		Given I am on school homepage for school with urn '139696'
		When I click on benchmark workforce data
		Then the benchmark workforce page is displayed