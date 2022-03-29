Feature: BookingList

This test runs an a basic e2e test for book a hearing
And then it checks booking list contains newly created hearing

@web
Scenario: Check booking list contains expected values
	Given I have a booked hearing
	When I navigate to booking list page
	Then The booking should contain expected values

@web
Scenario: VHO Bookings Search by Case Number
	Given I have a booked hearing
	When I navigate to booking list page
	And the VHO search for the booking by case number
	Then the booking is retrieved
	And VHO selects booking
	And the VHO is on the Booking Details page
