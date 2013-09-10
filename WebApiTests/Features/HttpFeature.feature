Feature: HttpFeature
	In order to make an http request
	As a webapi tester
	I want to be able to call the webapi generically in all my tests
	

Scenario: Test HTTP call
	Given I make a new request to localhost on port 9999
	And the path is api/voicecontinuity
	And I add the following fields as query parameters
	| key   | value         |
	| cli   | +1            |
	| ddi   | +441623299981 |
	| stage | initial       |
	When the request has compeleted
	Then the status code should be 200
	
	
	
