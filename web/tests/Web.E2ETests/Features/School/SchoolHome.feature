Feature: School homepage

	Scenario: Go to contact details page
		Given I am on school homepage for school with urn '101241'
		When I click on school details
		Then the school details page is displayed

	Scenario: Go to compare your costs page
		Given I am on school homepage for school with urn '101241'
		When I click on compare your costs
		Then the compare your costs page is displayed

	Scenario: Go to curriculum and financial planning page
		Given I am on school homepage for school with urn '101241'
		When I click on curriculum and financial planning
		Then the curriculum and financial planning page is displayed
		
	Scenario: Go to benchmark workforce data page 
		Given I am on school homepage for school with urn '101241'
		When I click on benchmark workforce data
		Then the benchmark workforce page is displayed
		
	Scenario: Go to view all spending and costs page
		Given I am on school homepage for school with urn '101241'
		When I click on view all spending and costs
		Then the spending and costs page is displayed