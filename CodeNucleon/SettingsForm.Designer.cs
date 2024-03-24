namespace CodeNucleon
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.betaCheckBox = new System.Windows.Forms.CheckBox();
            this.applyBtn = new System.Windows.Forms.Button();
            this.discordrpcCheckBox = new System.Windows.Forms.CheckBox();
            this.fpsLabel = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // betaCheckBox
            // 
            this.betaCheckBox.AutoSize = true;
            this.betaCheckBox.Location = new System.Drawing.Point(13, 24);
            this.betaCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.betaCheckBox.Name = "betaCheckBox";
            this.betaCheckBox.Size = new System.Drawing.Size(99, 21);
            this.betaCheckBox.TabIndex = 0;
            this.betaCheckBox.Text = "Beta Mode";
            this.betaCheckBox.UseVisualStyleBackColor = true;
            // 
            // applyBtn
            // 
            this.applyBtn.Location = new System.Drawing.Point(13, 296);
            this.applyBtn.Margin = new System.Windows.Forms.Padding(4);
            this.applyBtn.Name = "applyBtn";
            this.applyBtn.Size = new System.Drawing.Size(100, 30);
            this.applyBtn.TabIndex = 1;
            this.applyBtn.Text = "Apply";
            this.applyBtn.UseVisualStyleBackColor = true;
            this.applyBtn.Click += new System.EventHandler(this.applyBtn_Click);
            this.applyBtn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.settingsForm_KeyDown);
            // 
            // discordrpcCheckBox
            // 
            this.discordrpcCheckBox.AutoSize = true;
            this.discordrpcCheckBox.Checked = true;
            this.discordrpcCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.discordrpcCheckBox.Location = new System.Drawing.Point(13, 53);
            this.discordrpcCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.discordrpcCheckBox.Name = "discordrpcCheckBox";
            this.discordrpcCheckBox.Size = new System.Drawing.Size(115, 21);
            this.discordrpcCheckBox.TabIndex = 2;
            this.discordrpcCheckBox.Text = "Discord RPC";
            this.discordrpcCheckBox.UseVisualStyleBackColor = true;
            // 
            // fpsLabel
            // 
            this.fpsLabel.AutoSize = true;
            this.fpsLabel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.fpsLabel.Font = new System.Drawing.Font("Source Code Pro Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fpsLabel.Location = new System.Drawing.Point(7, 258);
            this.fpsLabel.Name = "fpsLabel";
            this.fpsLabel.Size = new System.Drawing.Size(56, 17);
            this.fpsLabel.TabIndex = 3;
            this.fpsLabel.Text = "label1";
            // 
            // richTextBox1
            // 
            this.richTextBox1.AcceptsTab = true;
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBox1.Location = new System.Drawing.Point(118, 329);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox1.ShortcutsEnabled = false;
            this.richTextBox1.Size = new System.Drawing.Size(10, 10);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            this.richTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.settingsForm_KeyDown);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(127, 339);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.fpsLabel);
            this.Controls.Add(this.discordrpcCheckBox);
            this.Controls.Add(this.applyBtn);
            this.Controls.Add(this.betaCheckBox);
            this.Font = new System.Drawing.Font("Source Code Pro Black", 9.75F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.settingsForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox betaCheckBox;
        private System.Windows.Forms.Button applyBtn;
        private System.Windows.Forms.CheckBox discordrpcCheckBox;
        private System.Windows.Forms.Label fpsLabel;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}