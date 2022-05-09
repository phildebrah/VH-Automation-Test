@HearingRoomControls
@web
@DeviceTest

Feature: Hearing room controls
	Mute & unmute, raise a hand & lower hand, switch camera off

Scenario: Hearing Room Controls
	Given I log in as "auto_aw.videohearingsofficer_01@hearings.reform.hmcts.net"
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
	And all participants have joined the hearing waiting room with SkipToWaitingRoom
	And the judge starts the hearing
	And the judge checks that all participants have joined the hearing room
	And the the participants microphone are all muted and locked when the judge mutes them
	And the participants microphones are unmuted when the judge unmutes them
	And all participants are redirected to the waiting room when the judge pauses the hearing
	And all participants are redirected to the hearing room when the judge resumes the video hearing
	And when a participant raises their hand, it shows on the judge's screen
	And the judge can lower participants hands
    When the participant switches off their camera, the judge can see it on the screen
	When the participant shares their screen, everyone should be able to see the shared screen
	And the judge can open and close the participant panel
	And the judge can open and close the chat panel
	And the judge can send a message to a VHO using via hearing room chat panel
	Then the judge closes the hearing
	And everyone signs out