@US3-US4
Feature: US3andUS4ErrorSummaryValidation
	With error file upload, info about each supply 
	should be displayed.

Background: 
	Given I am Logged on successfully on internal website page
	When I click measure processing link
	Then file upload page should display

Scenario: Error Records Validation Summary then reject
	When I upload a valid file containing MRNs from different suppliers: "Rejected"
	Then upload should successfully display such messages with option to continue: "Rejected"
	When I click continue
	Then validation summary of successfully measure uploads against suppliers will display with Reject buttons:"Rejected"
	When I click reject button
	Then file status should update to rejection with option to start again

	##Approve and Notify logic has changed will re-implement
#Scenario: Error Records Validation Summary then approve & notify
#    When I upload a valid file containing MRNs from different suppliers: "Approved"
#	Then upload should successfully display such messages with option to continue: "Approved"
#	When I click continue
#	Then validation summary of successfully measure uploads against suppliers will display with Approve&Notify and Reject buttons:"Approved"
#	When I click approve and notify button
#	Then file status should update to approved with option to start again
