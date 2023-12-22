Feature: CompareYourCosts

	School HomePage is showing correct data

Feature: Expenditure Benchmarking for Schools

	Scenario: Benchmarking a School's Expenditure
		Given I am benchmarking a school's expenditure for school 'urn'
		When I navigate to the school's expenditure benchmarking page
		Then the expenditure benchmarking page is displayed
		And the page is showing the total expenditure benchmark chart for the school

	Scenario: Viewing Total Expenditure Benchmarking Chart
		Given I am on a school's expenditure benchmarking page for school 'urn'
		When the total expenditure benchmarking chart is displayed
		Then the benchmark school name is highlighted on the chart
		And the data shown is £ per pupil
		And the x-axis has dynamic length depending on the highest value to be displayed
		And the x-axis has the title per pupil
		And all the comparator schools are shown on the y-axis
		And colours used are dark blue