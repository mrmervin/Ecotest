@UsersPrivilege
Feature: Users Privilege

@15449
Scenario Outline: Permissions for View Measure Processing Errors screen (Basic User)
	Given I am Logged on successfully on internal website page as a '<user>'
	When I navigate to the home page
	Then I should not be able to see "<subHeader>"
	Then I should be able to clear the cache
	Examples: 
	| user  | subHeader          |
	| Basic | Measure Processing |

@15449
Scenario Outline: Permissions for View Measure Processing Errors screen (Internal Admin User)
	Given I am Logged on successfully on internal website page as a '<user>'
	When I navigate to the '<subHeader>'
	Then I should be able to see '<sublink_1>' link
	Then I should not be able to see the '<subLink_2>'
	Then I should be able to clear the cache
	Examples: 
	| user           | subHeader          | sublink_1                      | subLink_2                           |
	| InternalAdmin  | Measure Processing | View Measure Processing Errors | Start Measure Processing for ECO2/t |
	| InternalAdmin  | Measure Processing | View Measure Processing Errors | Start Measure Processing for ECO3   |

@15449
Scenario Outline: Permissions for View Measure Processing Errors screen (Advanced User)
	Given I am Logged on successfully on internal website page as a '<user>'
	When I navigate to the '<subHeader>'
	Then I should be able to see '<sublink_1>' link
	Then I should be able to see '<subLink_2>' link
	Then I should be able to clear the cache
	Examples: 
	| user     | subHeader          | sublink_1                      | subLink_2                           |
	| Advanced | Measure Processing | View Measure Processing Errors | Start Measure Processing for ECO2/t |
    | Advanced | Measure Processing | View Measure Processing Errors | Start Measure Processing for ECO3   |

@15449
Scenario Outline: Permissions for View Measure Processing Errors screen (Standard User)
	Given I am Logged on successfully on internal website page as a '<user>'
	When I navigate to the '<subHeader>'
	Then I should be able to see '<sublink_1>' link
	Then I should be able to see '<subLink_2>' link
	Then I should be able to clear the cache
	Examples: 
	| user     | subHeader          | sublink_1                      | subLink_2                           |
	| Standard | Measure Processing | View Measure Processing Errors | Start Measure Processing for ECO2/t |
	| Standard | Measure Processing | View Measure Processing Errors | Start Measure Processing for ECO3   |
	

@15450 @15970
Scenario Outline: Permissions for Start Measure Processing screen (Standard & Advanced User)
	Given I am Logged on successfully on internal website page as a '<user>'
	When I hit '<startProcessDeepLink>' deep link
	Then I should be able to navigate to the '<page>'
	Then I should be able to clear the cache
	When I log on successfully to internal website page as a '<user2>'
	And I hit '<startProcessDeepLink>' deep link
	Then I should be able to navigate to the '<page>'
	Then the screen title should be '<screenTitle>'
	Then I should be able to clear the cache
	Examples: 
	| user     | startProcessDeepLink                     | user2    | page                     | screenTitle                       |
	| Standard | /MeasureProcessing/Start?ecoVersion=ECO2 | Advanced | Start Measure Processing | Start Measure Processing for ECO2 |
	| Standard | /MeasureProcessing/Start?ecoVersion=ECO3 | Advanced | Start Measure Processing | Start Measure Processing for ECO3 |

@15451
Scenario Outline: Permissions for Upload Measure Processing Error File screen (Internal Admin User)
	Given I am Logged on successfully on internal website page as a '<user>'
	When I navigate to the '<subHeader>'
	Then I should not be able to see the '<subLink_2>'
	Then I should be able to clear the cache
	Examples: 
	| user           | subHeader          |  subLink_2                           |
	| InternalAdmin  | Measure Processing | Upload Measure Processing Error File |


@15451
Scenario Outline: Permissions for Upload Measure Processing Error File screen (Advanced User)
	Given I am Logged on successfully on internal website page as a '<user>'
	When I navigate to the '<subHeader>'
	Then I should be able to see '<subLink_1>' link
	When I click on the '<subLink_1>'
	Then I should be able to navigate to the '<page>'
	Then I should be able to clear the cache
	Examples: 
	| user     | subHeader          | subLink_1                            | page                             |
	| Advanced | Measure Processing | Upload Measure Processing Error File | Upload Measure Processing Errors |

@15451
Scenario Outline: Permissions for Upload Measure Processing Error File screen (Standard User)
	Given I am Logged on successfully on internal website page as a '<user>'
	When I navigate to the '<subHeader>'
	Then I should be able to see '<subLink_1>' link
	When I click on the '<subLink_1>'
	Then I should be able to navigate to the '<page>'
	Then I should be able to clear the cache
	Examples: 
	| user     | subHeader          | subLink_1                            | page                             |
	| Standard | Measure Processing | Upload Measure Processing Error File | Upload Measure Processing Errors |



@15452
Scenario Outline: Permissions for View Measure Processing Error Files screen (Internal Admin User)
	Given I am Logged on successfully on internal website page as a '<user>'
	When I navigate to the '<subHeader>'
	Then I should not be able to see the '<subLink_2>'
    Then I should be able to clear the cache
	Examples: 
	| user          | subHeader          |  subLink_2                          |
	| InternalAdmin | Measure Processing | View Measure Processing Error Files |


@15452
Scenario Outline: Permissions for View Measure Processing Error Files screen (Advanced User)
	Given I am Logged on successfully on internal website page as a '<user>'
	When I navigate to the '<subHeader>'
	Then I should be able to see '<subLink_1>' link
	When I click on the '<subLink_1>'
	Then I should be able to navigate to the '<page>'
    Then I should be able to clear the cache
	Examples: 
	| user     | subHeader          | subLink_1                           | page                           |
	| Advanced | Measure Processing | View Measure Processing Error Files | Measure Processing Error Files |

@15452
Scenario Outline: Permissions for View Measure Processing Error Files screen (Standard User)
	Given I am Logged on successfully on internal website page as a '<user>'
	When I navigate to the '<subHeader>'
	Then I should be able to see '<subLink_1>' link
	When I click on the '<subLink_1>'
	Then I should be able to navigate to the '<page>'
    Then I should be able to clear the cache
	Examples: 
	| user     | subHeader          | subLink_1                           | page                           |
	| Standard | Measure Processing | View Measure Processing Error Files | Measure Processing Error Files |
