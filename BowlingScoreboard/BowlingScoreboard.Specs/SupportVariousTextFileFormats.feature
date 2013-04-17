Feature: Support various text file formats
	In order to account the scores of bowling games
	As a bowling consultant
	I want to be able to have support for different text file formats

Scenario: Support one column two shots per player format
	Given I want to load a file called "scores-1c2spp.txt"
	And I think its located at the path "./test_files/"
	And it has one column two shots per player at a time format
	When it loads 
	Then the application should accept the file

Scenario: Support two columns two shots per player format
	Given I want to load a file called "scores-2c2spp.txt"
	And I think its located at the path "./test_files/"
	And it has two columns two shots per player format
	When it loads
	Then the application should load the file