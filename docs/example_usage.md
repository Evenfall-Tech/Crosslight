var inputCSharp = ... // AssemblyUtility.LoadAssembly("CS.dll").FindInterface<IInputLanguage>().CreateInstance();
inputCSharp.Configuration["MergeModules"] = "true";

var outputCPP = ... // IOutputLanguage
outputCPP.Configuration["SeparateHeaders"] = "true";

var intermediateOptimizer = ... // IIntermediateLanguage
intermediateOptimizer.Configuration["Verbose"] = "true";
intermediateOptimizer.Configuration["AddCopyright"] = "false";

var validatorAttributes = ... // IIntermediateLanguage

var builder = Pipeline.CreateBuilder();
builder.Languages
	.AddLanguage(inputCSharp)
	.AddLanguage(intermediateOptimizer)
	.AddLanguage(outputCPP);
builder.Validators
	.AddLanguage(validatorAttributes);

builder.Configuration["AddCopyright"] = "true"; // Local config for language overrides global config for builder

builder.Logging. ...

var inputFile = ... // FileSystem.PhysicalFile("Main.cs");
var inputDirectory = ... // FileSystem.Directory("./Source/");

var pipeline = builder.Build();
pipeline
	.AddInputFile(inputFile)
	.AddInputDirectory(inputDirectory, recursive=true);

pipeline.Execute();
var output = pipeline.Output; // Already written to disk at this point
