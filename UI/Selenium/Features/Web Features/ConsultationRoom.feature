@ConsultationRoom
@web

Feature: Consultation room 
	Judge invites the participant to join the consultation room

Scenario: Consultation room
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
	| auto_aw.judge_02@hearings.reform.hmcts.net |   
	And I want to create a Hearing for
	| Party     | Role               | Id                                  |
	| Claimant  | Litigant in person | auto_vw.individual_05@hearings.reform.hmcts.net    |
	And With video Access points details
	| Display Name | Advocate |
	|              |          | 
	And I set any other information
	| Record Hearing | Other information   | 
	|                | This is a test info |
	And I book the hearing
    Then A hearing should be created
	And optionally I confirm the booking
	And I log off
	And all participants log in to video web
	And all participants have joined the hearing waiting room
	When the judge selects Enter consultation room
	And judge invites participant into the consultation
	And participant accepts the consultation room invitation
	Then judge checks participant joined the consultation room
	And all participants leave consultation room
	And everyone signs out