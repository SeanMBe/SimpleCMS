@f2
Feature: Add blog post
	In order to create new content on my blog
	As a blogger
	I want to add a new post

	Scenario: When I add a blog post with title and body
		Given I am on Home
		And I follow "Posts"
		And I follow "New Record"
		And I enter "Title of first blog post" into "Title"
		And I enter "Body of first blog post" into "Body"
		And I select "Bilbo Bagins" from "AuthorId"
		When I press "Create"
		Then I should see "Title of first blog post"
		And I should see "Body of first blog post"
		And I should see "Bilbo Bagins"
		