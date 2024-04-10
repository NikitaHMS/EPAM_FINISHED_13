---------------------------------
The command template to run tests
---------------------------------

dotnet test --filter "TestCategory={type of test}&TestCategory={environment}" -e browser={browser} -e environment={environment}

---------------------------------
Available parameters
---------------------------------

Types of tests: smoke

Browsers: chrome, firefox

Environments: desktop, mobile 
//NOTE: mobile environment is only available for chrome browser
	 
