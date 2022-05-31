// See https://aka.ms/new-console-template for more information
string environment = Environment.GetEnvironmentVariable("environment", EnvironmentVariableTarget.Process);
Console.WriteLine($"environment = {environment}");
