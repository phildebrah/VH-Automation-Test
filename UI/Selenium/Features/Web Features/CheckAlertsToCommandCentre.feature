@web

Feature:Check alerts are sent correctly to command centre
	
Scenario: Alert Failed self test - Incomplete Test

	Given I log in as "auto_aw.videohearingsofficer_03@hearings.reform.hmcts.net"
	And I select book a hearing
	And I want to create a hearing with case details 
	| Case Number | Case Name              | Case Type | Hearing Type        |
	| AA          | AutomationTestCaseName | Civil     | Enforcement Hearing |
	And the hearing has the following schedule details
	| Schedule Date | Duration Hour | Duration Minute |
	|               | 0             | 30              |
	And I want to Assign a Judge with courtroom details
	| Judge or Courtroom Account                 |
	| auto_aw.judge_03@hearings.reform.hmcts.net |   
	And I want to create a Hearing for
	| Party    | Role        | Id                                               |
	| Claimant | Witness     | auto_vw.individual_05@hearings.reform.hmcts.net  |
	| Claimant | Interpreter | auto_vw.interpreter_07@hearings.reform.hmcts.net | 
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
	And first participant has progressed to waiting room without completing self test
	And the Video Hearings Officer has progressed to the VHO Hearing List page for the existing hearing
	When the Video Hearings Officer check alerts for this hearing
	Then the the Video Hearings Officer see the alert Failed self-test (Incomplete Test) participant F & L name


Scenario: Alert Failed self test - No to Camera

	Given I log in as "auto_aw.videohearingsofficer_03@hearings.reform.hmcts.net"
	And I select book a hearing
	And I want to create a hearing with case details 
	| Case Number | Case Name              | Case Type | Hearing Type        |
	| AA          | AutomationTestCaseName | Civil     | Enforcement Hearing |
	And the hearing has the following schedule details
	| Schedule Date | Duration Hour | Duration Minute |
	|               | 0             | 30              |
	And I want to Assign a Judge with courtroom details
	| Judge or Courtroom Account                 |
	| auto_aw.judge_03@hearings.reform.hmcts.net |   
	And I want to create a Hearing for
	| Party    | Role        | Id                                               |
	| Claimant | Witness     | auto_vw.individual_05@hearings.reform.hmcts.net  |
	| Claimant | Interpreter | auto_vw.interpreter_07@hearings.reform.hmcts.net | 
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
	And the first participant has answered No to Was your camera working? question in self-test 
	And the Video Hearings Officer has progressed to the VHO Hearing List page for the existing hearing
	When the Video Hearings Officer check alerts for this hearing
	Then the the Video Hearings Officer see the alert Failed self-test (Camera) participant F & L name

	Scenario: Alert Failed self test - No to Microphone

	Given I log in as "auto_aw.videohearingsofficer_03@hearings.reform.hmcts.net"
	And I select book a hearing
	And I want to create a hearing with case details 
	| Case Number | Case Name              | Case Type | Hearing Type        |
	| AA          | AutomationTestCaseName | Civil     | Enforcement Hearing |
	And the hearing has the following schedule details
	| Schedule Date | Duration Hour | Duration Minute |
	|               | 0             | 30              |
	And I want to Assign a Judge with courtroom details
	| Judge or Courtroom Account                 |
	| auto_aw.judge_03@hearings.reform.hmcts.net |   
	And I want to create a Hearing for
	| Party    | Role        | Id                                               |
	| Claimant | Witness     | auto_vw.individual_05@hearings.reform.hmcts.net  |
	| Claimant | Interpreter | auto_vw.interpreter_07@hearings.reform.hmcts.net | 
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
	And the first participant has answered No to Was your microphone working? question in self-test
	And the Video Hearings Officer has progressed to the VHO Hearing List page for the existing hearing
	When the Video Hearings Officer check alerts for this hearing
	Then the the Video Hearings Officer see the alert Failed self-test (Microphone) participant F & L name


	Scenario: Alert Failed self test - No to Video clearly

	Given I log in as "auto_aw.videohearingsofficer_03@hearings.reform.hmcts.net"
	And I select book a hearing
	And I want to create a hearing with case details 
	| Case Number | Case Name              | Case Type | Hearing Type        |
	| AA          | AutomationTestCaseName | Civil     | Enforcement Hearing |
	And the hearing has the following schedule details
	| Schedule Date | Duration Hour | Duration Minute |
	|               | 0             | 30              |
	And I want to Assign a Judge with courtroom details
	| Judge or Courtroom Account                 |
	| auto_aw.judge_03@hearings.reform.hmcts.net |   
	And I want to create a Hearing for
	| Party    | Role        | Id                                               |
	| Claimant | Witness     | auto_vw.individual_05@hearings.reform.hmcts.net  |
	| Claimant | Interpreter | auto_vw.interpreter_07@hearings.reform.hmcts.net | 
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
	And the first participant has answered No to Could you see and hear the video clearly? question in self-test
	And the Video Hearings Officer has progressed to the VHO Hearing List page for the existing hearing
	When the Video Hearings Officer check alerts for this hearing
	Then the the Video Hearings Officer see the alert Failed self-test (Video) participant F & L name

	
	Scenario: Alert Failed self test - Navigates back

	Given I log in as "auto_aw.videohearingsofficer_03@hearings.reform.hmcts.net"
	And I select book a hearing
	And I want to create a hearing with case details 
	| Case Number | Case Name              | Case Type | Hearing Type        |
	| AA          | AutomationTestCaseName | Civil     | Enforcement Hearing |
	And the hearing has the following schedule details
	| Schedule Date | Duration Hour | Duration Minute |
	|               | 0             | 30              |
	And I want to Assign a Judge with courtroom details
	| Judge or Courtroom Account                 |
	| auto_aw.judge_03@hearings.reform.hmcts.net |   
	And I want to create a Hearing for
	| Party    | Role        | Id                                               |
	| Claimant | Witness     | auto_vw.individual_05@hearings.reform.hmcts.net  |
	| Claimant | Interpreter | auto_vw.interpreter_07@hearings.reform.hmcts.net | 
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
	And the first participant has progressed to the Waiting Room page after completing self-test
	And the first participant navigates back to the hearing-list page
	And the Video Hearings Officer has progressed to the VHO Hearing List page for the existing hearing
	When the Video Hearings Officer check alerts for this hearing
	Then the the Video Hearings Officer see the alert Disconnected participant F & L name	