Feature: SimpleApiFeature
	
	Simple GET function for a perticular endpoint

@mytag
Scenario: Simple Status Check
	Given The user send an GEt call to endpoiint 'nl/3825'
	Then  The user should receive a status code of OK


