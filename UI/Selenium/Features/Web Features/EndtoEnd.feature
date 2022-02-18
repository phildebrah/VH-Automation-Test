@EndtoEndTest
@web

Feature: EndtoEnd
	To book and attend a Hearing

Scenario: End to End test
	Given I log in as "auto_aw.videohearingsofficer_01@hearings.reform.hmcts.net"
	And I want to create a Hearing for
	| Judge  | Interpreters              | Participants              | VHO | Judicial Office Holder |
	| Judge1 | Interpreter1,Interpreter2 | Participant1,Participant2 | VHO | JOH                    |
	And I want to create a hearing with case details
	| Case Number | Case Name          | Case Type | Hearing Type       |
	|             | AutomationCaseName | Civil     | Enforcement Hearing |
	And the hearing has the following schedule details
	| Schedule Date | Duration Hour | Duration Minute |
	|               | 0             | 30              |
	And I want to Assign a Judge with courtroom details
	| Judge or Courtroom Account                 | 
	| auto_aw.judge_01@hearings.reform.hmcts.net |   
	#And I want to create a hearing 
	#| Judge  | Interpreters              | Participants              | VHO | Judicial Office Holder |
	#| Judge1 | Interpreter1,Interpreter2 | Participant1,Participant2 | VHO | JOH                    |
	#And I want to creat a new hearing with Judge, 2 Interpreter, 1 complainant, 1 respondant, 1 VHO, 1 representative
	#When I start a hearing
	#Then all the attendees will be seen

#Scenario: Four participants join hearing
#	Given the first Individual user has progressed to the Waiting Room page
#	And the first Representative user has progressed to the Waiting Room page for the existing hearing
#	And the second Individual user has progressed to the Waiting Room page for the existing hearing
#	And the second Representative user has progressed to the Waiting Room page for the existing hearing
#	And the Judge user has progressed to the Judge Waiting Room page for the existing hearing
#	When the Judge starts the hearing
#	Then the user is on the Countdown page
#	When the countdown finishes
#	Then the Judge is on the Hearing Room page for 1 minutes
#	And the Judge can see the participants
#	And the first Individual can see the other participants
#	And the first Representative can see the other participants
#	And the second Individual can see the other participants
#	And the second Representative can see the other participants
#	When in the Judge's browser
#	And the Judge closes the hearing
#	Then the user is on the Waiting Room page
#	And the hearing status changed to Closed
#	When in the first Individual's browser
#	Then the participants waiting room displays the closed status
