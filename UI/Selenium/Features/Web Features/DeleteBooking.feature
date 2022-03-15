Feature: DeleteBooking

This test runs an a basic e2e test for book a hearing
And then it deletes the hearing

@web
@tag1
Scenario: Delete a confirmed hearing
	Given I have a booked hearing
	When I delete the booking
	Then The booking should be deleted
