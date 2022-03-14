@EndtoEndTest
@web

Feature: Joining Hearing By Quick Link
	
Scenario: Copy Quick Link Test
	Given I log in video url as "auto_aw.videohearingsofficer_01@hearings.reform.hmcts.net"
	And I choose from hearing lists
		| Select your hearing lists                  |
		| Birmingham Civil and Family Justice Centre |
	When I click on view hearings
	Then I should naviagte to Hearing list page
	And I click on copy hearing id to clipboard it should able to copy
	And And I click on copy joining by phone details to clipboard it should able to copy 
    And I click on link to join by quick link details to clipboard it should able to open on new browser 

Scenario: Open hearing list
	Given I log in hearing url "auto_aw.videohearingsofficer_03@hearings.reform.hmcts.net"
	Then I want to join hearing with details
	| Full Name       |
	| Michael Jackson |
	And I click on signintoHearing 
	And I get ready for the hearing
	And I confirm equipment is working
	And I make sure camera and microphone switched on
	And I continue to watch the video
	And Testing your equipment 
	And Checking was your camera working
	And Checking was your microphone working
	And Checking were the image and sound clear
	And I agree to court rules 
	And I confirm declaration

	
	
