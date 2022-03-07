@EndtoEndTest
@web

Feature: Quick Link
	
Scenario: Quick Link Test
	Given I log in video url as "auto_aw.videohearingsofficer_01@hearings.reform.hmcts.net"
	And I choose from hearing lists
		| Select your hearing lists                  |
		| Birmingham Civil and Family Justice Centre |
	When I click on view hearings
	Then I should naviagte to Hearing list page
	And I click on link copy id to clipboard it should able to copy
    And I click on link to join by quick link details to clipboard it should able to open on new browser
	And And I click on hearing link to clipboard it should able to copy hearing link 
