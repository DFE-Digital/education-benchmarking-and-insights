Feature: School HomePage
	School homepage feature

Scenario: Go to contact details page
	Given I am on school homepage for school with urn '139696'
	When I click on school details in resource section
	Then I am navigated to school details page for school with urn '139696'