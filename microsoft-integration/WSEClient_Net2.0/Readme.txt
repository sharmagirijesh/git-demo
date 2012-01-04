Overview
--------

This is a reference .NET client that shows how to integrate with the Earthport Merchant API web service using .NET 2.0 and Microsoft Web Services Enhamcements (WSE) 3.0.  All code is included in a Microsoft Visual Studio 2008 solution.


Required Software
-----------------

- Microsoft Visual Studio 2005 (and .NET Framework 2.0)
- Web Services Enhancements (WSE) 3.0 for Microsoft .NET Redistributable Runtime
  http://www.microsoft.com/downloads/details.aspx?FamilyID=9e59c3fb-e7bc-4d91-908e-411a8d329f3d&displaylang=en


Contents
--------

The VS 2005 solution consists of two projects:

- MerchantAPI (a class library project that encapsulates the plumbing code to communicate with the web service) and
- SampleClient (a console application that communicates with the web service using the MerchantAPI)


Getting Started
---------------

- Download and install the WSE 3.0 Redistributable Runtime
- Open the solution (WCFClient.sln) in Visual Studio 2005
- Change the URL, username and password in ServiceProxy constructor in SampleClient\Program.cs and MerchantAPI.Test\ServiceTests.cs to the values supplied by Earthport
- In MerchantAPI/ServiceMetadata/wsdl, edit the MerchantAPI wsdl to change the @URL@ token to the URL supplied by Earthport.
- Set SampleClient as the startup project
- Run the solution

Running the Nant script
-----------------------
- In order to run the nant script you will require:
	-nant v0.86-beta1 [http://sourceforge.net/projects/nant/files/nant/0.86-beta1/]
	-NUnit v2.4.8 [http://sourceforge.net/projects/nunit/files/NUnit%20Version%202/NUnit-2.4.8-net-2.0.msi/download]
	-Microsoft Visual Studio 9.0

- Edit the 'default.build' file and change the property names in the target name="init" to match your environment.
- Open a Visual Studio 2008 command prompt and change the directory to the WSEClient_Net2.0 installation directory.
- Run 'nant' and this will by default run the build-sample target which will produce the following files:
	- SampleEPSClient2.0.exe (Test client executable)
	- Microsoft.Web.Services3.dll (MS WS3 libraries)
	- nunit.framework.dll (NUnit libraries)
	- Earthport.MerchantAPI.dll (Core Earthport dll)
	- MerchantAPI.Test.dll (NUnit test driver)

- You can run 'nant clean' to delete the generated files.
- You can run 'nant test' to run the NUnit tests against the configured environment.
	