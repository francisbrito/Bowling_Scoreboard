Feature: Read a text file
	In order to account the scores of bowling games
	As a bowling consultant
	I want to be able to read the scores from a text file

Scenario: Read a text file that exists from local file system
	Given I want to load a file called "scores.txt"
	And I think its located at the directory "./test_files/" in my local file system
	And it exists
	When I press load
	Then the application should load the file

Scenario: Read a text file that doesnt exists from local file system
	Given I want to load a file called "unexistent.txt"
	And I think its located at the directory "./test_files/" in my local file system
	But it doesnt exists
	When I press load
	Then the application should notify me the file doesnt exists

Scenario: Read a text file that exists from a network share
	Given I want to load a file called "net_scores.txt"
	And I think its located at the directory "/test_files/" in a network share host called "//10.0.0.1"
	And it exists
	When I press load
	Then the application should load the file

Scenario: Read a text file that doesnt exists from a network share
	Given I want to load a file called "net_unexistent.txt"
	And I think its located at the directory "/test_files/" in a network share host called "//10.0.0.1"
	But it doesnt exists
	When I press load
	Then the application should notify me the file doesnt exists

Scenario: Read a text file that exists from an internet server
	Given I want to load a file called "inet_scores.txt"
	And I think its located at the directory "/Bowling_Scoreboard/test_files/" in a  internet server called "frxbr.github.com"
	And it doesnt exists
	When I press load
	Then the application should load the file

Scenario: Read a text file that doesnt exists from an internet server
	Given I want to load a file called "404.txt"
	And I think its located at the directory "/Bowling_Scoreboard/test_files/" in a  internet server called "frxbr.github.com"
	But it doesnt exists
	When I press load
	Then the application should notify me the file doesnt exists
