// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Microsoft.Extensions.Configuration;
using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;

if (args.FirstOrDefault(_ => _ == "-h" || _ == "-?" || _ == "--help") != null)
{
    DisplayHelp();
}
else
{
    try
    {
        var options = GetOptions(GetConfiguration(args));
        var generatedcode = await GenerateCode(options.InputFile, options.NameSpace, options.ClassName);
        if (string.IsNullOrEmpty(options.OutputFile))
        {
            Console.WriteLine(generatedcode);
        }
        else
        {
            File.WriteAllText(options.OutputFile, generatedcode);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
        Console.WriteLine();
        DisplayHelp();
    }
}

static void DisplayHelp() => Console.WriteLine(
@"Options:
    -h
        Display this help
    -f filename
        filename is the file containing the JSON schema
    -n namespace
        namespace will be used as the namespace in the generated code
    -c classname
        classname will be the name of the root object from the generated code
    -o filename
        output file name is provided, otherwise written to STDOUT");

static IConfiguration GetConfiguration(string[] args) => new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", true)
    .AddEnvironmentVariables()
    .AddCommandLine(args, new Dictionary<string, string>
    {
        { "-f", "FileName" },
        { "-n", "NameSpace" },
        { "-c", "ClassName" },
        { "-o", "Output" }
    }).Build();

(string InputFile, string NameSpace, string ClassName, string? OutputFile) GetOptions(IConfiguration configuration) =>
    (configuration["FileName"], configuration["NameSpace"], configuration["ClassName"], configuration["Output"]);

static async Task<string> GenerateCode(string filename, string @namespace, string classname)
{
    var schema = await JsonSchema.FromFileAsync(filename);
    var generator = new CSharpGenerator(schema);
    generator.Settings.JsonLibrary = CSharpJsonLibrary.SystemTextJson;
    generator.Settings.Namespace = @namespace;
    return generator.GenerateFile(classname);
}
