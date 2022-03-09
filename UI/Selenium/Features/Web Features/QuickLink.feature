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
	And I want to join hearing with details
	| Full Name |
	| AA        |
	And I click on signintoHearing 
	And I confirm equipment is working
#	And I switch on camera and continue to the video
#	And I test my equipment 
#	And I make sure camera and microphone working and could see and video clearly
#	And I agree to court rules and declaration
#	And I start a private meeting
#	And I join a private meeting
#
#	
#	
#	And And I click on hearing link to clipboard it should able to copy hearing link 
