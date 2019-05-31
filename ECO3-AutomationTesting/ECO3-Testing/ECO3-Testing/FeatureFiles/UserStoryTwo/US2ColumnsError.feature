@US2
Feature: US2ColumnsError
	All data in columns of error files,
	should be checked to be correct.

Background: 
	Given I am Logged on successfully on internal website page
	When I click measure processing link
	Then file upload page should display

Scenario: Misspelt Columns
    When I upload a file with misspelt column
    Then I should see an error message: "File Rejected: The file's columns' headers are incorrectly formed"
	Then File will not be processed: "Misspelt Column"
	Then Continue button is not displayed

Scenario: Missing Column
    When I upload a file with missing column
    Then I should see an error message: "File Rejected: The file's columns' headers are incorrectly formed"
	Then File will not be processed: "Missing Column"
	Then Continue button is not displayed

Scenario: Invalid Supplier
	When I upload a file with invalid supplier
	Then I should see error messages with downloadable link : 4,"Invalid Supplier"
	When I click the link error file is downloaded with useful message about the download : "Invalid Supplier"
	Then File will be processed with record created in Database: "Invalid Supplier"

Scenario: MRN doesn't exist
    When I upload a file that MRN doesn't exist
	Then I should see error messages with downloadable link : 4,"MRN doesn't exist"
	When I click the link error file is downloaded with useful message about the download : "MRN doesn't exist"
	Then File will be processed with record created in Database: "Invalid MRN"

Scenario: MRN not linked to Supplier
	When I upload a file where MRN is not linked with supplier
	Then I should see error messages with downloadable link : 4,"MRN not linked to supplier"
	When I click the link error file is downloaded with useful message about the download : "MRN not linked to supplier"
	Then File will be processed with record created in Database: "Mrn not linked"

Scenario: Invalid field name
    When I upload a file with invalid field
	Then I should see error messages with downloadable link : 4,"Invalid field"
	When I click the link error file is downloaded with useful message about the download : "Invalid field"
	Then File will be processed with record created in Database: "Invalid Field"

Scenario: Invalid Error message and description, empty observed value,mismatched error,mismatched test number and name
    When I upload a file with all possible errors
	Then I should see error messages with downloadable link : 4,"All errors"
	When I click the link error file is downloaded with useful message about the download : "All errors"
	Then File will be processed with record created in Database: "All errors"



	
