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












	And I select book a hearing
	And I want to create a hearing with case details
	| Case Number | Case Name              | Case Type | Hearing Type        |
	| AA          | AutomationTestCaseName | Civil     | Enforcement Hearing |
	And the hearing has the following schedule details
	| Schedule Date | Duration Hour | Duration Minute |
	|               | 0             | 30              |
	And I want to Assign a Judge with courtroom details
	| Judge or Courtroom Account                 |
	| auto_aw.judge_02@hearings.reform.hmcts.net |   
	And I want to create a Hearing for
	| Party     | Role               | Id                                  |
	| Claimant  | Litigant in person | auto_vw.individual_05@hearings.reform.hmcts.net    |
	| Claimant  | Representative     | auto_vw.representative_01@hearings.reform.hmcts.net |
	| Defendant | Litigant in person | auto_vw.individual_06@hearings.reform.hmcts.net    |
	| Defendant | Solicitor          | auto_vw.representative_02@hearings.reform.hmcts.net |

	And With video Access points details
	| Display Name | Advocate |
	|              |          |
	And I set any other information
	| Record Hearing | Other information   |
	|                | This is a test info |
	And I book the hearing
	Then A hearing should be created
	And I log off
	And all participants log in to video web
	And all participants have joined the hearing waiting room
	And the judge starts the hearing
	And the judge checks that all participants have joined the hearing room
	Then the judge closes the hearing
	And everyone signs out

