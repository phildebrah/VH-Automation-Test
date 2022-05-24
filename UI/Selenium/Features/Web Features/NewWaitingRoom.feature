#@web
#@DeviceTest
#Feature: NewWaitingRoom
#
#Check all waiting room statuses are correct
#
#Scenario: Waiting Room Statuses
#
#Given That I am booking a hearing
#	| Case Number | Case Name              | Case Type | Hearing Type        |
#	| AA          | AutomationTestCaseName | Civil     | Enforcement Hearing |
#	And the hearing is scheduled as
#	| Schedule Date | Duration Hour | Duration Minute |
#	|               | 0             | 30              |
#	And the hearing has a judge assigned
#	| Judge or Courtroom Account                 |
#	| auto_aw.judge_02@hearings.reform.hmcts.net |   
#	And the hearing has a number of participants have been added
#	| Party     | Role               | Id                                                  |
#	| Claimant  | Litigant in person | auto_vw.individual_05@hearings.reform.hmcts.net     |
#	| Claimant  | Representative     | auto_vw.representative_01@hearings.reform.hmcts.net |
#	| Defendant | Litigant in person | auto_vw.individual_06@hearings.reform.hmcts.net     |
#	And the hearing video access point has been set
#	| Display Name | Advocate |
#	|              |          |
#	And the hearing has more information
#	| Record Hearing | Other information   |
#	|                | This is a test info |
#	And the hearing is booked and confirmed
#	And all participants log in to video web
#	And the judge signs into the hearing
#	Then assert the judge sees the correct status for each participant as NOT SIGNED IN
#	And assert the judge and participant's status change as each participant join the waiting room
#	And the judge starts the hearing and checks that all participants have joined the hearing room
#	When the judge pauses the hearing
#	And each have their status as CONNECTED and Available on judge's and participants screen respectively
#	And status should show Disconnected and Unavailable for judge and participants respectively when a participant logs off or close browser