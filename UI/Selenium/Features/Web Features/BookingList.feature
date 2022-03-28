Feature: BookingList

This test runs an a basic e2e test for book a hearing
And then it checks booking list contains newly created hearing

@web
Scenario: Check booking list contains expected values
	Given I have a booked hearing
	When I navigate to booking list page
	Then The booking should contain expected values