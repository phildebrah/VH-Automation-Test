Create a reference document in html varient

Solution Structure

	Database - a project to test data using a database, path is ..\datatests\database
	RestSharp - a project to test API's using RestSharp path is ..\api\restsharp
	UISelenium - a project to test UI using Selenium, the path is ..\UI\UISelenium (Depending on the complexity of project, Business Logic can be placed in another project and it can be referred to Test Project)
	TestFramework - core functionality to support all types of testing with logging, reporting and metrics
	



Directory Structure
There are a number of directories to support this framework and organise for different types of testing (insert an image of the testing pyramid).

Root

	TestLibrary - Core functionality for all testing

	DataTests - Tests that will check for valid data within files and Databases etc

	Packages - A single source for all imported package libraries - to be used to avoid different projects using their own version of the same library

	TestDataBuilder - a project library to generate files / database data for the purpose of a test.  (Could be a project within the framework or independent)

	Api - all testing done against any kind of API

		RestSharp - Any testing against a REST API using RestSharp

		SOAP - any testing against a SOAP call

	UI - All UI testing - As UI testing is fragile, subject to frequent change and can fail due to environmental conditions, this should be the lowest priority level of testing, API and data Testing will guarantee that the UI dependencies are functional.

		Playwright - Not supported at this time but may be in the future

		Selenium - Library to support all common features of Selenium testing, including Pages that are used in multiple websites supported here.

			Actions - Like Steps but these are the actions to be tested - in BDD: "WHEN i click on the send email button"

			CommonPageElements - ie Banners, navigation panes etc.

			Features - the tests to be performed.

			Pages - web pages to be tested - THIS ONLY DESCRIBES THE WEB ELEMENTS UNDER TEST (CommonPageElements should be the same)

			Steps - Any actions taken to get to the point where the test is Executed - in BDD "Given I start a browser", etc

			Website(s) - to segregate common testing when there are a number of websites to be tested with the same common structure.
