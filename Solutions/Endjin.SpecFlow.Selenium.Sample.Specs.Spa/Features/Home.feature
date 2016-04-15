Feature: Home
	In order to access the site
	As a user
	I want to be able to be able to go to the homepage

Scenario: Go to single page application views using Website Navigation Map
	Given I go to the home page
	When I click the Spa1 link
	Then I should be taken to the spaView1 page
