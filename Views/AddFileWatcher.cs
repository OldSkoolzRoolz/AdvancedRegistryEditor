// Project Name: AdvancedRegistryEditor
// Adapted  and expanded from https://github.com/giladreich/RegistryEditor
// File Name: AddFileWatcher.cs
// Author:  Kyle Crowder
// Github:  OldSkoolzRoolz
// Distributed under Open Source License
// Do not remove file headers






using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Windows.RegistryEditor.Views;
/// <summary>
///     Represents the form for adding a file watcher in the AdvancedRegistryEditor application.
/// </summary>
public partial class AddFileWatcher : Form
{

    /// <summary>
    ///     /// Initializes a new instance of the <see cref="AddFileWatcher" /> class.
    /// </summary>
    public AddFileWatcher()
    {
        InitializeComponent();
    }






    /// <summary>
    ///     /// Handles the click event for the "Add File Watcher" button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void button1_Click(object sender, EventArgs e)
    {
        // Validate file path
        if (string.IsNullOrWhiteSpace(tb_FilePath.Text))
        {
            _ = MessageBox.Show("File path cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return;
        }

        // Validate action code
        if (string.IsNullOrWhiteSpace(tb_action.Text))
        {
            _ = MessageBox.Show("Action code cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return;
        }

        (bool isValid, List<Diagnostic> diagnostics) = ValidateActionWithRoslyn(tb_action.Text);

        if (!isValid)
        {
            string errorMessages = string.Join(Environment.NewLine, diagnostics.Select(d => d.ToString()));
            _ = MessageBox.Show($"Invalid action code:\n{errorMessages}", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Proceed with further logic if both validations pass
        // ...
    }






    private void button2_Click(object sender, EventArgs e)
    {
        openFileDialog1.ShowHiddenFiles = true;
        openFileDialog1.Filter = "All Files (*.*)|*.*";
        openFileDialog1.Title = "Select a file to watch";
        openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        openFileDialog1.FileName = string.Empty; // Clear previous selection
        _ = openFileDialog1.ShowDialog();

        if (!string.IsNullOrWhiteSpace(openFileDialog1.FileName))
        {
            tb_FilePath.Text = openFileDialog1.FileName;
        }
    }






    /// <summary>
    ///     Validates the provided C# code to ensure it represents a valid <see cref="Action{T}" />
    ///     where T is a custom type <c>FileChangeInfo</c>. This validation is performed using Roslyn analyzers.
    /// </summary>
    /// <param name="actionCode">
    ///     The C# code to validate. For example:
    ///     <c>info => MessageBox.Show($"File changed: {info.Path}, Type: {info.ChangeType}")</c>.
    /// </param>
    /// <returns>
    ///     A tuple containing:
    ///     <list type="bullet">
    ///         <item>
    ///             <term>
    ///                 <c>isValid</c>
    ///             </term>
    ///             <description>A boolean indicating whether the provided code is valid.</description>
    ///         </item>
    ///         <item>
    ///             <term>
    ///                 <c>diagnostics</c>
    ///             </term>
    ///             <description>
    ///                 A list of <see cref="Microsoft.CodeAnalysis.Diagnostic" /> objects representing any validation
    ///                 errors.
    ///             </description>
    ///         </item>
    ///     </list>
    /// </returns>
    /// <remarks>
    ///     This method dynamically compiles the provided code snippet using Roslyn, injecting a definition
    ///     for the <c>FileChangeInfo</c> type to validate the code in the context of an <c>Action&lt;FileChangeInfo&gt;</c>.
    /// </remarks>
    private (bool isValid, List<Diagnostic> diagnostics) ValidateActionWithRoslyn(string actionCode)
    {
        // Inject FileChangeInfo definition for Roslyn validation
        string code = $@"
using System;
using System.Windows.Forms;
public class FileChangeInfo {{
    public string Path {{ get; set; }} = string.Empty;
    public System.IO.WatcherChangeTypes ChangeType {{ get; set; }}
    public string? OldPath {{ get; set; }}
}}
class Dummy {{
    public void Test() {{
        Action<FileChangeInfo> act = {actionCode};
    }}
}}}}";

        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code, new CSharpParseOptions(LanguageVersion.CSharp13));
        PortableExecutableReference[] references = new[]
        {
                    MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(Action).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(MessageBox).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(WatcherChangeTypes).Assembly.Location)
        };

        CSharpCompilation compilation = CSharpCompilation.Create("ActionValidation", new[] { syntaxTree }, references, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        List<Diagnostic> diagnostics = compilation.GetDiagnostics().Where(d => d.Severity == DiagnosticSeverity.Error).ToList();

        return (diagnostics.Count == 0, diagnostics);
    }

}