SpecflowWebApiHelpers
=====================

This is a reusable library for creating reusable specflow steps.

It is using xunit as the unit testing framework.

At the moment it is targeting testing any webapi.

It currently has the following steps:

###1) Setting the host and port:

"Given I make a new request to <host> on port <port>"

###2) Setting the path of the url:

"Given the path is <path>"

###3) Adding a dynamic fieldset of query parameters to the query string:

"Given I add the following fields as query parameters" (add table)

###4) Invoking the actual http request:

"When the request has compeleted"

###5) Giving a basic test on the status code returned:

"Then the status code should be <status code>"

##Using these steps in your own scenarioss
To use this with your own scenarios e.g. test the response body etc, the http response is
available in the "ScenarioContext" in the form of a "HttpResponseMessage".

To retrieve the response from the context, use the following code:

```
var response = ScenarioContext.Current.Get<HttpResponseMessage>(CURRENTRESPONSE);
```

SpecflowWebApiHelpers - Example
=====================
```
Feature: HttpFeature
	In order to make an http request
	As a webapi tester
	I want to be able to call the webapi generically in all my tests
	

Scenario: Test HTTP call
	Given I make a new request to www.google.com on port 9999
	And the path is api/googlepath
	And I add the following fields as query parameters
	| key   | value         |
	| A   | ValueA            |
	| B   | ValueB |
	| C | ValueC       |
	When the request has compeleted
	Then the status code should be 200
	
```	
	
