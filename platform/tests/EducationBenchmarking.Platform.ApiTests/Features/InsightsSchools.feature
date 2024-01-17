Feature: InsightsSchools
	Insights school endpoints

	Scenario: Sending a valid school expenditure request
		Given a valid schools expenditure request with urn '139696' and '139697'
		When I submit the schools expenditure request
		Then the school expenditure result should be ok
		
	Scenario: Sending a valid schools expenditure request for the first page
		Given a valid school expenditure request with page '1' and urn '119376'
		When I submit the schools expenditure request
		Then the schools expenditure result should be page '1' with '10' page size
        
	Scenario: Sending a valid query schools request for the second page
		Given a valid school expenditure request with page '2' and urn '119376'
		When I submit the schools expenditure request
		Then the schools expenditure result should be page '2' with '10' page size
        
	Scenario: Sending a valid query schools request for page size 5
		Given a valid school expenditure request with size '5' and urn '119376'
		When I submit the schools expenditure request
		Then the schools expenditure result should be page '1' with '5' page size
			