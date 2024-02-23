@SchoolHome.feature
Feature: SchoolHome
	School homepage feature

Scenario: Go to contact details page
	Given I am on school homepage for school with urn '139696'
	When I click on school details in resource section
	Then I am navigated to school details page for school with urn '139696'
	
Scenario: Go to compare your costs page
	Given I am on school homepage for school with urn '139696'
	When I click on compare your costs in finance tools section
	Then I am navigated to compare your costs page for school with urn '139696'
	
Scenario: Go to curriculum and financial planning page
	Given I am on school homepage for school with urn '139696'
	When I click on curriculum and financial planning in finance tools section
	Then I am navigated to curriculum and financial planning page for school with urn '139696'
	
	
	