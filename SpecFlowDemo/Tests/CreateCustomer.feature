Feature: Create-Customer
	We want to test Create-Customer feature

Scenario: Triger Create Customer Logic App With Valid Request - Expectation: New Customer Is Created 
	When I send a POST request with 'Valid Request' scenario to trigger the 'Create-Customer' logic app
	And I wait '5' seconds for 'Create-Customer' logic app execution to complete
	Then I can verify the following events for 'Create-Customer' logic app
	| StepName																		| Status    |
	| manual																		| Succeeded |
	| Parse_JSON																	| Succeeded |
	| HTTP																			| Succeeded |
	| Send-Email																	| Succeeded |

Scenario: Triger Create Customer Logic App With Invalid Request - Expectation: Customer Is Not Created 
	When I send a POST request with 'Invalid Request' scenario to trigger the 'Create-Customer' logic app
	And I wait '5' seconds for 'Create-Customer' logic app execution to complete
	Then I can verify the following events for 'Create-Customer' logic app
	| StepName																		| Status    |
	| manual																		| Succeeded |
	| Parse_JSON																	| Failed	|
	| HTTP																			| Skipped   |
	| Send-Email																	| Skipped   |