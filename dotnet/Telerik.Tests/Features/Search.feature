Feature: Search

Scenario Outline: Search should return correct results
	Given I am on the search page
	When I search for <string>
	Then I should see <link> in results

Examples:
	| string        | link                                      |
	| kendo license | https://www.telerik.com/purchase/kendo-ui |
	| careers       | https://www.telerik.com/company/careers   |