using System.Collections;

// See https://aka.ms/new-console-template for more information
string environment = Environment.GetEnvironmentVariable("environment", EnvironmentVariableTarget.Process);
var a = Environment.CommandLine;
var b = Environment.GetEnvironmentVariables();
//var c = Environment.GetFolderPath();
var d = Environment.ProcessId;
var e = Environment.CurrentDirectory;
var f = Environment.UserDomainName;
//Console.WriteLine($"environment = {environment}");
foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
    Console.WriteLine("  {0} = {1}", de.Key, de.Value);