@web
Feature: Accessibility
	In order to ensure video web is accessible to all users
	As a service
	I want to check each page for potential accessibility issues@Smoketest

@Accessibility
Scenario: Dashboard page accessibility
	Given I'm on the "Dashboard" page
	Then the page should be accessible

@Accessibility
Scenario: Hearing Details page accessibility
	Given I'm on the "Hearing Details" page
	Then the page should be accessible

@Accessibility
Scenario: Hearing Schedule page accessibility
	Given I'm on the "Hearing Schedule" page
	Then the page should be accessible

@Accessibility
Scenario: AssignJudge page accessibility
	Given I'm on the "AssignJudge" page
	Then the page should be accessible

@Accessibility
Scenario: Participants page accessibility
	Given I'm on the "Participants" page
	Then the page should be accessible

@Accessibility
Scenario: Video Access Points page accessibility
	Given I'm on the "Video Access Points" page
	Then the page should be accessible

@Accessibility
Scenario: Other Information page accessibility
	Given I'm on the "Other Information" page
	Then the page should be accessible

@Accessibility
Scenario: Summary page accessibility
	Given I'm on the "Summary" page
	Then the page should be accessible

@Accessibility
Scenario: Booking Confirmation page accessibility
	Given I'm on the "Booking Confirmation" page
	Then the page should be accessible

@Accessibility
Scenario: Booking Details page accessibility
	Given I'm on the "Booking Details" page
	Then the page should be accessible

@Accessibility
Scenario: Booking List page accessibility
	Given I'm on the "Booking List" page
	Then the page should be accessible  
	
@Accessibility
Scenario: Questionnaire page accessibility
	Given I'm on the "Questionnaire" page
	Then the page should be accessible  
	
@Accessibility
Scenario: Get-audio-file page accessibility
	Given I'm on the "Get-Audio-File" page
	Then the page should be accessible

@Accessibility
Scenario: change-password page accessibility
	Given I'm on the "Change-User-Password" page
	Then the page should be accessible

@Accessibility
Scenario: Delete-participant page accessibility
	Given I'm on the "Delete-User-Account" page
	Then the page should be accessible
@Accessibility
Scenario: Edit-participant-name page accessibility
	Given I'm on the "Edit-Participant-Name" page
	Then the page should be accessible