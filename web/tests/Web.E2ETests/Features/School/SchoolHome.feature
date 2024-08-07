﻿Feature: School homepage

	Scenario: Go to contact details page
		Given I am on school homepage for school with urn '777042'
		When I click on school details
		Then the school details page is displayed

	Scenario: Go to compare your costs page
		Given I am on school homepage for school with urn '777042'
		When I click on compare your costs
		Then the compare your costs page is displayed
		
	Scenario: Go to curriculum and financial planning page
		Given I am on school homepage for school with urn '777042'
		When I click on curriculum and financial planning
		Then the curriculum and financial planning page is displayed
		
	Scenario: Go to benchmark census data page 
		Given I am on school homepage for school with urn '777042'
		When I click on benchmark census data
		Then the benchmark census page is displayed
		
	Scenario: Go to view all spending and costs page
		Given I am on school homepage for school with urn '777042'
		When I click on view all spending and costs
		Then the spending and costs page is displayed
		
	Scenario: Goto view historic data page
		Given I am on school homepage for school with urn '777042'
		When I click on view historic data
		Then the historic data page is displayed
		
	Scenario: Goto find ways to spend less page
		Given I am on school homepage for school with urn '777042'
		When I click on find ways to spend less
		Then the commercial resources page is displayed