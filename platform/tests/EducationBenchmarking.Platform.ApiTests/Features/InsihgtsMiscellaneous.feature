Feature: InsightsMiscellaneous
Insights miscellaneous endpoints

Scenario: Sending a valid finance year request
	Given a valid finance year request
	When I submit the finance year request
	Then the finance year result should be ok