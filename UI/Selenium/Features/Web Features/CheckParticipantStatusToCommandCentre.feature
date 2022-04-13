@web

Feature:Check participant status
	
Scenario: Check participant status

	Given I log in as "auto_aw.videohearingsofficer_04@hearings.reform.hmcts.net"
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
	| Claimant | Litigant in person     | auto_vw.individual_05@hearings.reform.hmcts.net  |
	| Claimant | Litigant in person     | auto_vw.individual_06@hearings.reform.hmcts.net  |
	| Defendant | Litigant in person    | auto_vw.individual_08@hearings.reform.hmcts.net  |
	And With video Access points details
	| Display Name | Advocate |
	|              |          |		
	And I set any other information
	| Record Hearing | Other information   | 
	|                | This is a test info |
	And I book the hearing
    Then A hearing should be created
	And I log off 
	Then three participants log in to video web
	And  participants have joined the hearing waiting room
	Given I login to VHO in video url as "auto_aw.videohearingsofficer_04@hearings.reform.hmcts.net" for existing hearing
	And I choose from hearing lists
		| Select your hearing lists                  |
		| Birmingham Civil and Family Justice Centre |
	Then the judge starts the hearing
	When the Video Hearings Officer check alerts for this hearing
	Then the Video Hearings Officer should able to see the status 
	And I log off 
	And everyone signs out

	Scenario: Check panel member and other participant status
	Given I log in as "auto_aw.videohearingsofficer_07@hearings.reform.hmcts.net"
	And I select book a hearing
	And I want to create a hearing with case details 
	| Case Number | Case Name              | Case Type | Hearing Type        |
	| AA          | AutomationTestCaseName | Civil     | Enforcement Hearing |
	And the hearing has the following schedule details
	| Schedule Date | Duration Hour | Duration Minute |
	|               | 0             | 30              |
	And I want to Assign a Judge with courtroom details
	| Judge or Courtroom Account                 |
	| auto_aw.judge_07@hearings.reform.hmcts.net |   
	And I want to create a Hearing for
	| Party        | Role               | Id                                               |
	| Claimant     | Litigant in person | auto_vw.individual_04@hearings.reform.hmcts.net  |
	| Panel Member | Panel Member       | auto_aw.panelmember_02@hearings.reform.hmcts.net |
	
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
	And participants have joined the hearing waiting room without Judge
    When the panel member selects Enter consultation room
	Given I login to VHO in video url as "auto_aw.videohearingsofficer_07@hearings.reform.hmcts.net" for existing hearing
	And I choose from hearing lists
		| Select your hearing lists                  |
		| Birmingham Civil and Family Justice Centre |
	When the Video Hearings Officer check alerts for this hearing
	Then the Video Hearings Officer should able to view the status
	And I log off 
	And everyone signs out
