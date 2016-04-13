Feature: Register
	In order to access the site
	As a user
	I want to be able to register

Scenario: Register valid user
	Given I go to the register page
	And I enter a valid email address
	And I enter a valid password
	And I enter a matching confirm password
	When I register
	Then I should be taken to the home page
	And I should be logged in
	And the welcome message should contain my email address