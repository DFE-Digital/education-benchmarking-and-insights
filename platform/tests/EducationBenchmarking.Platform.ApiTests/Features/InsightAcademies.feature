Feature: InsightsAcademies
	Insights Academies endpoints

	Scenario: Sending a valid academy request
		Given a valid academy request with urn '139137'
		When I submit the academies request
		Then the academies result should be ok
		
	Scenario: Sending an invalid academy request should return not found
		Given a invalid academy request
		When I submit the academies request
		Then the academies result should be not found