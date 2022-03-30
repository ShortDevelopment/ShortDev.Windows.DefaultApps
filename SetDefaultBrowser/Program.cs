using ShortDev.Windows.DefaultApps;
using ShortDev.Windows.DefaultApps.Browser;
using Spectre.Console;
using System;
using System.IO;

#if !DEBUG
try
{
#endif

AnsiConsole.WriteLine($"Default browser: {BrowserSettings.DefaultBrowserAssociationId}");
AnsiConsole.WriteLine($"Default browser: {BrowserSettings.DefaultBrowserId}");
AnsiConsole.WriteLine($"Default browser: {BrowserSettings.DefaultBrowserCommand}");

// =================

// No Effect
// BrowserSettings.SetDefault("Firefox-308046B0AF4A39CB");

// =================

string fileName = "AppAssociations.xml";
AppAssociation.ExportToXml(fileName);

AnsiConsole.WriteLine($"Modify \"{fileName}\" as follows:");
AnsiConsole.Write(new TextPath(Path.Combine(Directory.GetCurrentDirectory(), fileName)));
AnsiConsole.WriteLine();

AnsiConsole.WriteLine(@"<Association Identifier=""http"" ProgId=""ChromeHtml"" ApplicationName=""chrome"" />");
AnsiConsole.WriteLine(@"<Association Identifier=""https"" ProgId=""ChromeHtml"" ApplicationName=""chrome"" />");
AnsiConsole.WriteLine(@"<Association Identifier="".htm"" ProgId=""ChromeHtml"" ApplicationName=""chrome"" />");
AnsiConsole.WriteLine(@"<Association Identifier="".html"" ProgId=""ChromeHtml"" ApplicationName=""chrome"" />");
AnsiConsole.WriteLine(@"<Association Identifier=""microsoft-edge"" ProgId=""ChromeHtml"" ApplicationName=""chrome"" />");

Console.WriteLine();
Console.WriteLine("Press enter to continue ...");
Console.ReadLine();

AppAssociation.ApplyXml("AppAssociations.xml");

AnsiConsole.WriteLine();
AnsiConsole.WriteLine("Success!!");

#if !DEBUG
}
catch (Exception ex)
{
    AnsiConsole.WriteException(ex);
}
#endif

AnsiConsole.WriteLine();
AnsiConsole.WriteLine("Press enter to exit ...");
Console.ReadLine();