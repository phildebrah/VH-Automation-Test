#@web
#@DeviceTest
#
#Feature: Check participant status in command centre
#	
#Scenario: Check different individual participant status
#	Given I log in as "auto_aw.videohearingsofficer_10@hearings.reform.hmcts.net"
#	And I select book a hearing
#	And I want to create a hearing with case details 
#	| Case Number | Case Name              | Case Type | Hearing Type        |
#	| AA          | AutomationTestCaseName | Civil     | Enforcement Hearing |
#	And the hearing has the following schedule details
#	| Schedule Date | Duration Hour | Duration Minute |
#	|               | 0             | 30              |
#	And I want to Assign a Judge with courtroom details
#	| Judge or Courtroom Account                 |
#	| auto_aw.judge_03@hearings.reform.hmcts.net |   
#	And I want to create a Hearing for
#	| Party     | Role               | Id                                              |
#	| Claimant  | Litigant in person | auto_vw.individual_05@hearings.reform.hmcts.net |
#	| Claimant  | Litigant in person | auto_vw.individual_06@hearings.reform.hmcts.net |
#	| Defendant | Litigant in person | auto_vw.individual_08@hearings.reform.hmcts.net |
#	And With video Access points details
#	| Display Name | Advocate |
#	|              |          |		
#	And I set any other information
#	| Record Hearing | Other information   | 
#	|                | This is a test info |
#	And I book the hearing
#    Then A hearing should be created
#	And I log off 
#	And all participants log in to video web
#	And  participants have joined the hearing waiting room
#	Then the judge starts the hearing
#	When Video Hearing Officer logs into video web as "auto_aw.videohearingsofficer_11@hearings.reform.hmcts.net"
#	And selects hearing venue in the venue list
#	And selects current hearing
#	Then the Video Hearings Officer should able to see the status 
#	And everyone signs out
#
#Scenario: Check panel member and participant status
#	Given I log in as "auto_aw.videohearingsofficer_13@hearings.reform.hmcts.net"
#	And I select book a hearing
#	And I want to create a hearing with case details 
#	| Case Number | Case Name              | Case Type | Hearing Type        |
#	| AA          | AutomationTestCaseName | Civil     | Enforcement Hearing |
#	And the hearing has the following schedule details
#	| Schedule Date | Duration Hour | Duration Minute |
#	|               | 0             | 30              |
#	And I want to Assign a Judge with courtroom details
#	| Judge or Courtroom Account                 |
#	| auto_aw.judge_07@hearings.reform.hmcts.net |   
#	And I want to create a Hearing for
#	| Party        | Role               | Id                                               |
#	| Claimant     | Litigant in person | auto_vw.individual_04@hearings.reform.hmcts.net  |
#	| Panel Member | Panel Member       | auto_aw.panelmember_02@hearings.reform.hmcts.net |	
#	And With video Access points details
#	| Display Name | Advocate |
#	|              |          |		
#	And I set any other information
#	| Record Hearing | Other information   | 
#	|                | This is a test info |
#	And I book the hearing
#    Then A hearing should be created
#	And I log off 
#	And all participants log in to video web
#	And participants have joined the hearing waiting room without Judge
#    When the panel member selects Enter consultation room
#	When Video Hearing Officer logs into video web as "auto_aw.videohearingsofficer_12@hearings.reform.hmcts.net"
#	And selects hearing venue in the venue list
#	And selects current hearing
#	Then the Video Hearings Officer should able to view the status
#	And everyone signs out
