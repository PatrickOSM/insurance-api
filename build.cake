#addin nuget:?package=Cake.Coverlet

var target = Argument("target", "Test");
var configuration = Argument("configuration", "Debug");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////


Task("Build")
    .Does(() =>
{
    DotNetCoreBuild("Insurance.API.sln", new DotNetCoreBuildSettings
    {
        Configuration = configuration,
    });
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
	var coverletSettings = new CoverletSettings {
        CollectCoverage = true,
        CoverletOutputFormat = CoverletOutputFormat.opencover | CoverletOutputFormat.json,
        MergeWithFile = MakeAbsolute(new DirectoryPath("./coverage.json")).FullPath,
        CoverletOutputDirectory = MakeAbsolute(new DirectoryPath(@"./coverage")).FullPath
    };
	
	Coverlet(
        "./tests/Insurance.Api.IntegrationTests/bin/Debug/net6.0/Insurance.Api.IntegrationTests.dll", 
        "./tests/Insurance.Api.IntegrationTests/Insurance.Api.IntegrationTests.csproj", 
        coverletSettings);
		
	Coverlet(
        "./tests/Insurance.Api.UnitTests/bin/Debug/net6.0/Insurance.Api.UnitTests.dll", 
        "./tests/Insurance.Api.UnitTests/Insurance.Api.UnitTests.csproj", 
        coverletSettings);
	
});

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);