These are the code samples for the book "Stylish F#", by Kit Eason (Apress 2018).

Using the Samples
=================

Most of the samples come in the form of F# scripts (.fsx files).

If using Visual Studio Code you should:

- Run VS Code
- Install the 'Ionide-fsharp' extension if not already installed.
- Open the folder containing the samples.
- Open the .fsx file you want to use.
- Press CTRL+SHIFT+P to bring up the Command Palette.
- Type 'FSI: Start' to run F# Interactive.
- In the editor, select the range of lines you want to try out. 
  Typically don't include the module heading (e.g. module Chapter01_01 =).
- Press ALT+ENTER to send the lines to F# Interactive.
- You can call a function in the code you sent to F# Interactive.
  Type its name and any arguments directly into the terminal,
  followed by ';;'.

e.g.

> checkRate 99.0;;
val it : float = 99.0

If using Visual Studio you should:

- Ensure that F# is installed using the Visual Studio installer.
- Run Visual Studio.
- Open the .fsx file you want to use.
- In the editor, select the range of lines you want to try out. 
  Typically don't include the module heading (e.g. module Chapter01_01 =).
- Press ALT+ENTER or use Right Click -> Execute in Interactive 
  to send the lines to F# Interactive.
- You can call a function in the code you sent to F# Interactive
  by typing its name and any arguments directly into the F# Interactive
  window, followed by ';;'.

e.g.

> checkRate 99.0;;
val it : float = 99.0

For Chapters 10 and 12 there is are compilable F# projects.

To use these:

If using Visual Studio Code:

- Run VS Code.
- Install the 'Ionide-fsharp' extension if not already installed.
- Install the 'OmniSharp' extension if not already installed.
- Select File -> Open Folder and navigate into the appropriate folder,
  e.g. Ch12\InappropriateCollectionType. Select the one same-named folder that
  is in this folder, e.g. InappropriateCollectionType, and click 'Select Folder'.
- Press CTRL+SHIFT+B and click 'build'.
- Open the Debugging tab.
- In the dropdown beside the green triangle, select .NET Core Launch (Console).
- Click the green triangle or press F5 to run.

If using Visual Studio:

- Ensure that F# is installed using the Visual Studio installer.
- Run Visual Studio.
- Open the appropriate solution - 
  e.g. Ch12\InappropriateCollectionType\InappropriateCollectionType.sln.
- Select the 'Release' configuration from the dropdown. 
  (This is essential for the Chapter 12 solutions, the others can be run in Debug.)
- Click 'Run'.

Notes:

When running from Visual Studio code, you may need to click the red 'Stop' button
when a run completes.

The Chapter 12 projects may take some time to run as the benchmarks are quite
intensive.

The console color coding for the MassDownloadAsync project does not currently work
when running in Visual Studio Code - i.e. all output is shown in monochrome. This appears
to be a VS Code issue. 

