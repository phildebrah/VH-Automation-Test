@DummyHearing
@web

Feature: DummyHearing
	To book and attend a Hearing

Scenario: End to End test
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
	| Claimant  | Litigant in person | auto_vw.individual_05@gmail.com    |
	| Claimant  | Representative     | auto_vw.representative_01@gmail.com |
	| Defendant | Litigant in person | auto_vw.individual_06@gmail.com   |
	| Defendant | Solicitor          | auto_vw.representative_02@gmail.com |

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
