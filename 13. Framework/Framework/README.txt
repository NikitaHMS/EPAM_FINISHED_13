---------------------------------
The command template to run tests
---------------------------------

dotnet test --filter "TestCategory={type of test}&TestCategory={device}" -e browser={browser} -e device={device}

---------------------------------
Available parameters
---------------------------------

Types of tests: smoke

Browsers: chrome, firefox

Device: desktop, mobile 
//NOTE: mobile device is only available for chrome browser
	 
