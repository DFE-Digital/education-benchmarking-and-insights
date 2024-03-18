Feature: Find organisation 

Scenario: Goto school homepage
	Given I am on find organisation page
	When I click continue after searching for '118167'
	Then the school homepage is displayed