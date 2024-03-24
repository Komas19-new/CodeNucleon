using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ScintillaNET;

namespace CodeNucleon
{
    public partial class Form1 : Form
    {
        private Process pythonProcess;
        private Process batchProcess;
        private string tempFilePath;
        private Panel panelOverlay;
        // Define the Konami code sequence
        private readonly Keys[] easterEgg1 = { Keys.Up, Keys.Up, Keys.Down, Keys.Down, Keys.Left, Keys.Right, Keys.Left, Keys.Right, Keys.B, Keys.A };
        // Index to keep track of the current position in the Konami code sequence
        private int easterEgg1Index = 0;

        public Form1()
        {
            Console.WriteLine("Initializing Form1");
            InitializeComponent();
            InitializeScintilla();
            this.Resize += MainForm_Resize;
            runBtn.Visible = Properties.Settings.Default.BetaMode;
            Console.WriteLine("Form1 Initialized");
        }

        private void InitializeScintilla()
        {
            Console.WriteLine("Initializing Scintilla");
            // Set up Scintilla properties
            scintilla1.Margins[0].Width = 40; // Set margin width for line numbers

            // Set lexer based on the language ComboBox selection
            languageComboBox.SelectedIndexChanged += (sender, e) =>
            {
                Console.WriteLine("Language ComboBox SelectedIndexChanged event fired");
                string language = languageComboBox.SelectedItem.ToString();
                if (Properties.Settings.Default.BetaMode)
                {
                    Console.WriteLine("BetaMode is enabled");
                    SetScintillaLexer(language);
                }
            };

            // Set up lexer styles
            scintilla1.Styles[ScintillaNET.Style.Default].Font = "Source Code Pro Black";
            scintilla1.Styles[ScintillaNET.Style.Default].Size = 10;
            scintilla1.Styles[ScintillaNET.Style.Default].Bold = true;
            scintilla1.Styles[ScintillaNET.Style.Default].BackColor = Color.White;
            scintilla1.Styles[ScintillaNET.Style.Default].ForeColor = Color.Black;
            scintilla1.AllowDrop = true;
            scintilla1.DragDrop += code_DragDrop;
            scintilla1.DragEnter += code_DragEnter;

            // Enable syntax highlighting
            scintilla1.LexerName = "cpp"; // Use C++ lexer as a default
            Console.WriteLine("Scintilla Initialized");
        }

        private void SetScintillaLexer(string language)
        {
            Console.WriteLine("Setting Scintilla Lexer: " + language);
            switch (language)
            {
                case "Lua":
                    Console.WriteLine("Setting Scintilla Lexer to Lua");
                    scintilla1.LexerName = "lua";
                    scintilla1.Styles[ScintillaNET.Style.Lua.Comment].ForeColor = Color.Green;
                    break;
                case "C++":
                case "C#":
                case "C":
                case "java":
                    Console.WriteLine("Setting Scintilla Lexer to C++");
                    scintilla1.LexerName = "cpp";
                    scintilla1.Styles[ScintillaNET.Style.Cpp.Comment].ForeColor = Color.Green;
                    scintilla1.Styles[ScintillaNET.Style.Cpp.CommentLine].ForeColor = Color.Green;
                    scintilla1.Styles[ScintillaNET.Style.Cpp.Word].ForeColor = Color.Blue;
                    break;
                case "Python":
                    Console.WriteLine("Setting Scintilla Lexer to Python");
                    scintilla1.LexerName = "python";
                    scintilla1.Styles[ScintillaNET.Style.Python.CommentLine].ForeColor = Color.Green;
                    scintilla1.Styles[ScintillaNET.Style.Python.Number].ForeColor = Color.Red;
                    scintilla1.Styles[ScintillaNET.Style.Python.String].ForeColor = Color.DarkOrange;
                    break;
                case "JavaScript":
                case "TypeScript":
                    Console.WriteLine("Setting Scintilla Lexer to JavaScript");
                    scintilla1.LexerName = "javascript";
                    scintilla1.Styles[ScintillaNET.Style.JavaScript.CommentLine].ForeColor = Color.Green;
                    scintilla1.Styles[ScintillaNET.Style.JavaScript.Number].ForeColor = Color.Red;
                    scintilla1.Styles[ScintillaNET.Style.JavaScript.SingleString].ForeColor = Color.DarkOrange;
                    scintilla1.Styles[ScintillaNET.Style.JavaScript.StringEol].ForeColor = Color.DarkOrange;
                    scintilla1.Styles[ScintillaNET.Style.JavaScript.DoubleString].ForeColor = Color.DarkOrange;
                    scintilla1.Styles[ScintillaNET.Style.JavaScript.SingleString].ForeColor = Color.DarkOrange;
                    break;
                case "HTML":
                    Console.WriteLine("Setting Scintilla Lexer to HTML");
                    scintilla1.LexerName = "html";
                    scintilla1.Styles[ScintillaNET.Style.Html.Comment].ForeColor = Color.Green;
                    scintilla1.Styles[ScintillaNET.Style.Html.Attribute].ForeColor = Color.Blue;
                    break;
                case "CSS":
                    Console.WriteLine("Setting Scintilla Lexer to CSS");
                    scintilla1.LexerName = "css";
                    scintilla1.Styles[ScintillaNET.Style.Css.Comment].ForeColor = Color.Green;
                    scintilla1.Styles[ScintillaNET.Style.Css.Default].ForeColor = Color.Blue;
                    break;
                case "PHP":
                    Console.WriteLine("Setting Scintilla Lexer to PHP");
                    scintilla1.LexerName = "phpscript";
                    scintilla1.Styles[ScintillaNET.Style.PhpScript.Comment].ForeColor = Color.Green;
                    scintilla1.Styles[ScintillaNET.Style.PhpScript.Variable].ForeColor = Color.Red;
                    scintilla1.Styles[ScintillaNET.Style.PhpScript.SimpleString].ForeColor = Color.DarkOrange;
                    scintilla1.Styles[ScintillaNET.Style.PhpScript.HString].ForeColor = Color.DarkOrange;
                    break;
                case "Ruby":
                    Console.WriteLine("Setting Scintilla Lexer to Ruby");
                    scintilla1.LexerName = "ruby";
                    scintilla1.Styles[ScintillaNET.Style.Ruby.CommentLine].ForeColor = Color.Green;
                    scintilla1.Styles[ScintillaNET.Style.Ruby.ClassName].ForeColor = Color.Blue;
                    break;
                case "Swift":
                    Console.WriteLine("Setting Scintilla Lexer to Swift");
                    scintilla1.LexerName = "swift";
                    scintilla1.Styles[ScintillaNET.Style.Cpp.CommentLine].ForeColor = Color.Green;
                    scintilla1.Styles[ScintillaNET.Style.Cpp.Number].ForeColor = Color.Red;
                    scintilla1.Styles[ScintillaNET.Style.Cpp.String].ForeColor = Color.DarkOrange;
                    break;
                case "Go":
                    Console.WriteLine("Setting Scintilla Lexer to Go");
                    scintilla1.LexerName = "go";
                    scintilla1.Styles[ScintillaNET.Style.Cpp.CommentLine].ForeColor = Color.Green;
                    scintilla1.Styles[ScintillaNET.Style.Cpp.Number].ForeColor = Color.Red;
                    scintilla1.Styles[ScintillaNET.Style.Cpp.String].ForeColor = Color.DarkOrange;
                    break;
                case "SQL":
                    Console.WriteLine("Setting Scintilla Lexer to SQL");
                    scintilla1.LexerName = "sql";
                    scintilla1.Styles[ScintillaNET.Style.Sql.Comment].ForeColor = Color.Green;
                    scintilla1.Styles[ScintillaNET.Style.Sql.String].ForeColor = Color.DarkOrange;
                    break;
                case "XML":
                    Console.WriteLine("Setting Scintilla Lexer to XML");
                    scintilla1.LexerName = "xml";
                    scintilla1.Styles[ScintillaNET.Style.Xml.Comment].ForeColor = Color.Green;
                    scintilla1.Styles[ScintillaNET.Style.Xml.Attribute].ForeColor = Color.Blue;
                    break;
                case "JSON":
                    Console.WriteLine("Setting Scintilla Lexer to JSON");
                    scintilla1.LexerName = "json";
                    scintilla1.Styles[ScintillaNET.Style.Json.BlockComment].ForeColor = Color.Green;
                    scintilla1.Styles[ScintillaNET.Style.Json.LineComment].ForeColor = Color.Green;
                    scintilla1.Styles[ScintillaNET.Style.Json.Number].ForeColor = Color.Red;
                    scintilla1.Styles[ScintillaNET.Style.Json.String].ForeColor = Color.DarkOrange;
                    break;
                case "Batch":
                    Console.WriteLine("Setting Scintilla Lexer to Batch");
                    scintilla1.LexerName = "batch";
                    scintilla1.Styles[ScintillaNET.Style.Batch.Comment].ForeColor = Color.Green;
                    break;
                case "Text":
                    Console.WriteLine("Setting Scintilla Lexer to Text");
                    scintilla1.LexerName = "null";
                    break;
                default:
                    // Set default lexer for unrecognized languages
                    Console.WriteLine("Setting Scintilla Lexer to Default (null)");
                    scintilla1.LexerName = "null";
                    break;
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            Console.WriteLine("MainForm Resized");
            int textBoxWidth = this.ClientSize.Width - 20;
            int textBoxHeight = this.ClientSize.Height - 50;
            // Update the size of the codeTextBox to match the size of the MainForm
            scintilla1.Size = new Size(textBoxWidth, textBoxHeight);
        }


        private void saveBtn1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Save Button Clicked");
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            // Add filters for supported programming languages
            saveFileDialog.Filter = "Lua Files (*.lua)|*.lua|" +
                                    "C++ Files (*.cpp;*.h)|*.cpp;*.h|" +
                                    "C Files (*.c;*.h)|*.c;*.h|" +
                                    "C# Files (*.cs)|*.cs|" +
                                    "Python Files (*.py)|*.py|" +
                                    "JavaScript Files (*.js)|*.js|" +
                                    "TypeScript Files (*.ts)|*.ts|" +
                                    "Java Files (*.java)|*.java|" +
                                    "HTML Files (*.html;*.htm)|*.html;*.htm|" +
                                    "CSS Files (*.css)|*.css|" +
                                    "PHP Files (*.php)|*.php|" +
                                    "Ruby Files (*.rb)|*.rb|" +
                                    "Swift Files (*.swift)|*.swift|" +
                                    "Go Files (*.go)|*.go|" +
                                    "SQL Files (*.sql)|*.sql|" +
                                    "XML Files (*.xml)|*.xml|" +
                                    "JSON Files (*.json)|*.json|" +
                                    "Batch Files (*.cmd;*.bat)|*.cmd;*.bat|" +
                                    "Text Files (*.txt)|*.txt|" +
                                    "All Files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                string textToSave = scintilla1.Text;

                File.WriteAllText(filePath, textToSave);
                MessageBox.Show("File saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine("File saved successfully!");
            }
        }

        private void settingsBtn_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Settings Button Clicked");
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
            Console.WriteLine("Settings Form Closed");
        }


        private void openBtn_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Open Button Clicked");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            // Add filters for supported programming languages
            // Add filters for supported programming languages
            openFileDialog.Filter = "Lua Files (*.lua)|*.lua|" +
                                    "C++ Files (*.cpp;*.h)|*.cpp;*.h|" +
                                    "C Files (*.c;*.h)|*.c;*.h|" +
                                    "C# Files (*.cs)|*.cs|" +
                                    "Python Files (*.py)|*.py|" +
                                    "JavaScript Files (*.js)|*.js|" +
                                    "TypeScript Files (*.ts)|*.ts|" +
                                    "Java Files (*.java)|*.java|" +
                                    "HTML Files (*.html;*.htm)|*.html;*.htm|" +
                                    "CSS Files (*.css)|*.css|" +
                                    "PHP Files (*.php)|*.php|" +
                                    "Ruby Files (*.rb)|*.rb|" +
                                    "Swift Files (*.swift)|*.swift|" +
                                    "Go Files (*.go)|*.go|" +
                                    "SQL Files (*.sql)|*.sql|" +
                                    "XML Files (*.xml)|*.xml|" +
                                    "JSON Files (*.json)|*.json|" +
                                    "Batch Files (*.cmd;*.bat)|*.cmd;*.bat|" +
                                    "Text Files (*.txt)|*.txt|" +
                                    "All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;

            Console.WriteLine("OpenFileDialog Filter set");

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine("Dialog result is OK");
                string fileName = openFileDialog.FileName;
                string fileContent = File.ReadAllText(fileName);
                scintilla1.Text = fileContent;

                Console.WriteLine("File content loaded");

                // Determine the language based on the file extension
                string extension = Path.GetExtension(fileName);
                string language = GetLanguageFromExtension(extension);

                // Select the language in the ComboBox
                if (!string.IsNullOrEmpty(language))
                {
                    int index = languageComboBox.Items.IndexOf(language);
                    if (index != -1)
                        languageComboBox.SelectedIndex = index;
                }
            }
        }

        private string GetLanguageFromExtension(string extension)
        {
            switch (extension)
            {
                case ".lua":
                    Console.WriteLine("Detected Lua file extension");
                    return "Lua";
                case ".cpp":
                    Console.WriteLine("Detected C++ file extension");
                    return "C++";
                case ".cs":
                    Console.WriteLine("Detected C# file extension");
                    return "C#";
                case ".c":
                    Console.WriteLine("Detected C file extension");
                    return "C";
                case ".h":
                    Console.WriteLine("Detected C Header file extension");
                    return "C";
                case ".py":
                    Console.WriteLine("Detected Python file extension");
                    return "Python";
                case ".js":
                    Console.WriteLine("Detected JavaScript file extension");
                    return "JavaScript";
                case ".ts":
                    Console.WriteLine("Detected TypeScript file extension");
                    return "TypeScript";
                case ".java":
                    Console.WriteLine("Detected Java file extension");
                    return "Java";
                case ".html":
                    Console.WriteLine("Detected HTML file extension");
                    return "HTML";
                case ".css":
                    Console.WriteLine("Detected CSS file extension");
                    return "CSS";
                case ".php":
                    Console.WriteLine("Detected PHP file extension");
                    return "PHP";
                case ".rb":
                    Console.WriteLine("Detected Ruby file extension");
                    return "Ruby";
                case ".swift":
                    Console.WriteLine("Detected Swift file extension");
                    return "Swift";
                case ".go":
                    Console.WriteLine("Detected Go file extension");
                    return "Go";
                case ".sql":
                    Console.WriteLine("Detected SQL file extension");
                    return "SQL";
                case ".xml":
                    Console.WriteLine("Detected XML file extension");
                    return "XML";
                case ".json":
                    Console.WriteLine("Detected JSON file extension");
                    return "JSON";
                case ".bat":
                    Console.WriteLine("Detected Batch file extension");
                    return "Batch";
                case ".cmd":
                    Console.WriteLine("Detected CMD file extension");
                    return "Batch";
                case ".txt":
                    Console.WriteLine("Detected Text file extension");
                    return "Text";
                default:
                    Console.WriteLine("Unknown file extension or no extension detected");
                    return null; // Unknown language or no extension
            }
        }

        private void ExecutePythonCode(string pythonCode)
        {
            Console.WriteLine("Executing Python code");
            // Save the Python code to a temporary file
            tempFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.py");
            File.WriteAllText(tempFilePath, pythonCode);

            try
            {
                // Execute the Python file using the Python interpreter
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "python3",
                    Arguments = tempFilePath,
                    UseShellExecute = false,
                    CreateNoWindow = false
                };

                pythonProcess = Process.Start(startInfo);
                Console.WriteLine("Python code executed");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error executing Python code: " + ex.Message);
                MessageBox.Show("Error executing Python code: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExecuteBatchCode(string batchCode)
        {
            Console.WriteLine("Executing Batch code");
            // Save the Batch code to a temporary file
            tempFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.bat");
            File.WriteAllText(tempFilePath, batchCode);

            try
            {
                // Execute the Batch file using the CMD shell
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "cmd",
                    Arguments = tempFilePath,
                    UseShellExecute = false,
                    CreateNoWindow = false
                };

                batchProcess = Process.Start(startInfo);
                Console.WriteLine("Batch code executed");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error executing Batch code: " + ex.Message);
                MessageBox.Show("Error executing Batch code: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void runBtn_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Run Button Clicked");
            if (languageComboBox.SelectedIndex != 4)
            {
                Console.WriteLine("Selected language is not Python");
                MessageBox.Show("Currently, only Python is supported to be run by CodeNucleon.");
            }
            else
            {
                string pythonCode = scintilla1.Text;
                Console.WriteLine("Python code to be executed: " + pythonCode);
                ExecutePythonCode(pythonCode);
            }
        }

        private void MainForm_Closing(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine("MainForm Closing");
            // Check if the Python process is running
            if (pythonProcess != null && !pythonProcess.HasExited)
            {
                Console.WriteLine("Python process is still running, terminating...");
                // Terminate the Python process
                pythonProcess.Kill();
                Console.WriteLine("Python process terminated");
            }

            // Check if the Batch process is running
            if (batchProcess != null && !batchProcess.HasExited)
            {
                Console.WriteLine("Batch process is still running, terminating...");
                // Terminate the Batch process
                batchProcess.Kill();
                Console.WriteLine("Batch process terminated");
            }
            // Delete the temporary file if it exists
            if (File.Exists(tempFilePath))
            {
                Console.WriteLine("Temporary file exists, deleting...");
                File.Delete(tempFilePath);
                Console.WriteLine("Temporary file deleted");
            }
            else
            {
                Console.WriteLine("Temporary file does not exist");
            }
        }

        private void code_DragEnter(object sender, DragEventArgs e)
        {
            Console.WriteLine("Drag entered the code editor");
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                Console.WriteLine("Data format: File drop");
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                Console.WriteLine("Data format: Unsupported");
            }
        }

        private void code_DragDrop(object sender, DragEventArgs e)
        {
            Console.WriteLine("Drag dropped into the code editor");
            // Check if the drop occurred over the RichTextBox
            Point clientPoint = scintilla1.PointToClient(new Point(e.X, e.Y));
            if (scintilla1.ClientRectangle.Contains(clientPoint))
            {
                Console.WriteLine("Drop occurred within the code editor boundaries");
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    Console.WriteLine($"Number of files dropped: {files.Length}");
                    string filePath = files[0]; // Assuming only one file is dropped
                    Console.WriteLine("File path dropped: " + filePath);
                    string fileExtension = Path.GetExtension(filePath).ToLower();

                    // Set the language based on the file extension
                    string language = GetLanguageFromExtension(fileExtension);

                    // Select the language in the ComboBox
                    if (!string.IsNullOrEmpty(language))
                    {
                        int index = languageComboBox.Items.IndexOf(language);
                        if (index != -1)
                            languageComboBox.SelectedIndex = index;
                    }

                    // Open and display the file content in the code editor
                    scintilla1.Text = File.ReadAllText(filePath);
                    Console.WriteLine("File content loaded into the code editor");
                }
            }
            else
            {
                Console.WriteLine("Drop occurred outside the code editor boundaries");
            }
        }

        private void SetLanguage(string language)
        {
            Console.WriteLine("Setting language to: " + language);
            int index = languageComboBox.Items.IndexOf(language);
            if (index != -1)
            {
                languageComboBox.SelectedIndex = index;
                SetScintillaLexer(language);
            }
        }

        private void code_TextChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Code editor text changed");
            string text = scintilla1.Text.ToLower(); // Convert text to lowercase for case-insensitive matching
            // Detect language based on keyword patterns
            if (text.Contains("class") || text.Contains("namespace") || text.Contains("using") || text.Contains("public"))
            {
                SetLanguage("C#");
            }
            else if (text.Contains("def") || text.Contains("import") || text.Contains("print") || text.Contains("for") || text.Contains("while") || text.Contains("async") || text.Contains("if __name__ == \"__main__\":"))
            {
                SetLanguage("Python");
            }
            else if (text.Contains("local") || text.Contains("function") || text.Contains("end"))
            {
                SetLanguage("Lua");
            }
            else if (text.Contains("int") || text.Contains("float") || text.Contains("double") || text.Contains("void") || text.Contains("include"))
            {
                SetLanguage("C++");
            }
            else if (text.Contains("#include") || text.Contains("cout") || text.Contains("cin") || text.Contains("namespace"))
            {
                SetLanguage("C++");
            }
            else if (text.Contains("printf") || text.Contains("scanf") || text.Contains("main"))
            {
                SetLanguage("C");
            }
            else if (text.Contains("var") || text.Contains("let") || text.Contains("const") || text.Contains("function") || text.Contains("=>") || text.Contains("async"))
            {
                SetLanguage("JavaScript");
            }
            else if (text.Contains("class") || text.Contains("extends") || text.Contains("static") || text.Contains("void") || text.Contains("main"))
            {
                SetLanguage("Java");
            }
            else if (text.Contains("<html>") || text.Contains("<head>") || text.Contains("<body>") || text.Contains("<div>") || text.Contains("<span>") || text.Contains("<p>") || text.Contains("<a ") || text.Contains("<img "))
            {
                SetLanguage("HTML");
            }
            else if (text.Contains("{") || text.Contains("}") || text.Contains(":") || text.Contains(";") || text.Contains("#") || text.Contains("body") || text.Contains("div") || text.Contains("p") || text.Contains("h1") || text.Contains("h2"))
            {
                SetLanguage("CSS");
            }
            else if (text.Contains("<?php") || text.Contains("?>") || text.Contains("echo") || text.Contains("$") || text.Contains("$_") || text.Contains("->"))
            {
                SetLanguage("PHP");
            }
            else if (text.Contains("def") || text.Contains("class") || text.Contains("module") || text.Contains("require") || text.Contains("puts") || text.Contains("end"))
            {
                SetLanguage("Ruby");
            }
            else if (text.Contains("import SwiftUI") || text.Contains("struct") || text.Contains("var") || text.Contains("let") || text.Contains("func") || text.Contains("print") || text.Contains("init") || text.Contains("body"))
            {
                SetLanguage("Swift");
            }
            else if (text.Contains("package") || text.Contains("import") || text.Contains("func") || text.Contains("var") || text.Contains("const") || text.Contains("fmt") || text.Contains("main"))
            {
                SetLanguage("Go");
            }
            else if (text.Contains("SELECT") || text.Contains("FROM") || text.Contains("WHERE") || text.Contains("JOIN") || text.Contains("INSERT INTO") || text.Contains("UPDATE") || text.Contains("DELETE FROM") || text.Contains("CREATE TABLE"))
            {
                SetLanguage("SQL");
            }
            else if (text.Contains("<xml>") || text.Contains("<element>") || text.Contains("<attribute>") || text.Contains("<value>") || text.Contains("<tag>") || text.Contains("<root>") || text.Contains("<data>"))
            {
                SetLanguage("XML");
            }
            else if (text.Contains("{") || text.Contains("[") || text.Contains(":") || text.Contains(",") || text.Contains("\"") || text.Contains("true") || text.Contains("false") || text.Contains("null"))
            {
                SetLanguage("JSON");
            }
            else if (text.Contains("@echo") || text.Contains("echo") || text.Contains("set") || text.Contains("if") || text.Contains("for") || text.Contains("goto") || text.Contains("pause") || text.Contains("rem"))
            {
                SetLanguage("Batch");
            }
            else
            {
                SetLanguage("Text");
            }
        }


        private void languageComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("Language ComboBox KeyDown event triggered");
            // Check if the pressed key matches the next key in the Konami code sequence
            if (e.KeyCode == easterEgg1[easterEgg1Index])
            {
                Console.WriteLine("Correct key pressed: " + e.KeyCode);
                easterEgg1Index++;

                // If the entire Konami code sequence is entered correctly
                if (easterEgg1Index == easterEgg1.Length)
                {
                    easterEgg1Index = 0; // Reset index for next input

                    // Trigger your Easter egg or special feature here
                    Properties.Settings.Default.InterestingItSeemsLikeYouLookedAtTheCodeOrTheSettingsFileHeyyyy = !Properties.Settings.Default.InterestingItSeemsLikeYouLookedAtTheCodeOrTheSettingsFileHeyyyy;
                    Properties.Settings.Default.Save();
                    Console.WriteLine("Easter egg activated! Settings changed.");
                    MessageBox.Show("It seems you have found the easter egg.. Funny, have some FPS in the Settings app forever until you do this again. Hehe :)");
                }
            }
            else
            {
                Console.WriteLine("Incorrect key pressed or out of sequence: " + e.KeyCode);
                // Reset index if incorrect key is pressed
                easterEgg1Index = 0;
            }
        }
    }
}


