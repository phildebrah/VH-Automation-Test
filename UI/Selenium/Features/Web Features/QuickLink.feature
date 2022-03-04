
@EndtoEndTest
@web

Feature: Quick Link
	To book and attend a Hearing

Scenario: Quick Link Test
	Given I log in as "https://vh-video-web-dev.hearings.reform.hmcts.net"
	And I choose from hearing lists
        |Select your hearing lists|
        |Birmingham Civil and Family Justice Centre|
    And  click on view hearing
    When  Iam on hearings page
    And  I mouse over on hearing link copy id to clipboard  it should copy
    And  I mouse over on hearing link and click on copy join by quick link details to clipboard it should open on new browser
    And  I mouse over on hearing link and click on copy joining  by phone details to clipboard
