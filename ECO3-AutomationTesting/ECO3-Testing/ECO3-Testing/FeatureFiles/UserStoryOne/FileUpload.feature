@US1 
Feature: FileFormat
	When uploading files for Mearsure processing
	The format should be in accepted format i.e. CSV

Background: 
	Given I am Logged on successfully on internal website page
	When I click measure processing link
	Then file upload page should display

Scenario: Upload pdf document
	When I upload a pdf file
	Then I should see an error message: "File Rejected: Please provide a CSV file."
	Then File will not be processed: "Pdf"

Scenario: Upload Excel document
	When I upload an excel file
	Then I should see an error message: "File Rejected: Please provide a CSV file."
	Then File will not be processed: "Excel"

Scenario: Upload Word document
	When I upload a word file
	Then I should see an error message: "File Rejected: Please provide a CSV file."
	Then File will not be processed: "Word"

Scenario: Upload Blank File
	When I upload a blank valid file
	Then I should see an error message: "File Rejected: The file is empty"
	Then File will not be processed: "Blank"

Scenario: Upload File with Blank row but with Headers
     When I upload a file with headers but no row
     Then validation of file done is with no data
	 Then File will be processed with record created in Database: "Blank Row"

Scenario: Wrong naming convention
	When I upload a file that is with wrong naming convention
	Then I should see an error message: "File Rejected:  Please ensure the filename meets the required naming convention which is MP_YYMMDD_MONTHYYYY_Measure_Processing_Errors.csv"
	Then File will not be processed: "Wrong Naming"

