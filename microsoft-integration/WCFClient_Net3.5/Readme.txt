Overview
--------

This is a reference .NET client that shows how to integrate with the Earthport Merchant API web service using Windows Communication Foundation (WCF).  All code is included in a Microsoft Visual Studio 2008 solution.


Required Software
-----------------

- Microsoft Visual Studio 2008 (and .NET Framework 3.5)


Contents
--------

The VS 2008 solution consists of two projects:

- MerchantAPI (a class library project that encapsulates the plumbing code to communicate with the web service) and
- SampleClient (a console application that communicates with the web service using the MerchantAPI)


Getting Started
---------------

- Open the solution (WCFClient.sln) in Visual Studio 2008
- Change the URL, username and password in ServiceProxy constructor in SampleClient\Program.cs and MerchantAPI.Test\ServiceTest.cs to the values supplied by Earthport
- Set SampleClient as the startup project
- In MerchantAPI/ServiceMetadata/wsdl, edit the MerchantAPI wsdl to change the @URL@ token to the URL supplied by Earthport.
- In MerchantAPI.Test, edit the App.config to change the @URL@ token to the URL supplied by Earthport.
- In SampleClient, edit the App.config to change the @URL@ token to the URL supplied by Earthport.
- Run the solution

Running the Nant script
-----------------------
- In order to run the nant script you will require:
	-nant v0.86-beta1 [http://sourceforge.net/projects/nant/files/nant/0.86-beta1/]
	-NUnit v2.4.8 [http://sourceforge.net/projects/nunit/files/NUnit%20Version%202/NUnit-2.4.8-net-2.0.msi/download]
	-Microsoft Visual Studio 9.0

- Edit the 'default.build' file and change the property names in the target name="init" to match your environment.
- Open a Visual Studio 2008 command prompt and change the directory to the WCFClient_Net3.5 installation directory.
- Run 'nant' and this will by default run the build-sample target which will produce the following files:
	- SampleEPSClient3.5.exe (Test client executable)
	- Earthport.MerchantAPI.dll (Core Earthport dll)
	- SamplesEPSClient3.5.exe.config (Configuration file)
	- MerchantAPI.Test.dll (NUnit test driver)
	- nunit.framework.ddl (NUnit core libraries)

- You can run 'nant clean' to delete the generated files.
- You can run 'nant test' to run the NUnit tests against the configured environment.
	