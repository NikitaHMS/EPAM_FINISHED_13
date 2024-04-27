---------------------------------
The command template to run tests
---------------------------------

dotnet test --filter TestCategory={type of test} -e browser={browser} --logger html --results-directory .\TestResults

---------------------------------
Available parameters
---------------------------------

Types of tests: smoke

Browsers: chrome, firefox
