@US1-1 @Duplicate 
Feature: DuplicateFileUpload
	if a measure has been uploaded successful before,
	When it's uploaded again, there should be an option 
	to continue or discontinue

Scenario:01 Upload measure file
	Given I am Logged on successfully on internal website page
	When I click measure processing link
	When I upload a correct measure file 
	Then upload should be successful displaying such messages with option to continue 
	Then File record will contain filename, uploaded username and creation timestamp: "Initial Upload"
	Then I choose the discontinue upload

Scenario:02-1 Duplicate discontinued
    Given I am on file upload page
	And I upload a file that has been uploaded successfully before
	Then I should see a message : "Warning: A file with this name has already been uploaded. Do you wish to continue?"
	Then I choose the discontinue upload
	Then upload will not be done and file record will not be created again

Scenario:03 Duplicate continued
    Given I am on file upload page
	And I upload a file that has been uploaded successfully before
	Then I should see a message : "Warning: A file with this name has already been uploaded. Do you wish to continue?"
	When I choose the continue option
	Then upload should be successful displaying such messages with option to continue
	When I click continue
	Then File record will contain filename, uploaded username and creation timestamp: "Duplicate Upload"
	Then Reject button will be displayed
	
