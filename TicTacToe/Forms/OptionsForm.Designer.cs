namespace TicTacToe.Forms
{
    partial class OptionsForm
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
            Options_Label = new Label();
            TurnOff_button = new Button();
            BackToMain_button = new Button();
            ResetHighscoreButton = new Button();
            SuspendLayout();
            // 
            // Options_Label
            // 
            Options_Label.AutoSize = true;
            Options_Label.Font = new Font("Segoe UI", 48F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            Options_Label.ForeColor = SystemColors.ButtonFace;
            Options_Label.Location = new Point(97, 30);
            Options_Label.Name = "Options_Label";
            Options_Label.Size = new Size(271, 86);
            Options_Label.TabIndex = 0;
            Options_Label.Text = "Options";
            Options_Label.Click += label1_Click;
            // 
            // TurnOff_button
            // 
            TurnOff_button.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TurnOff_button.Location = new Point(168, 160);
            TurnOff_button.Name = "TurnOff_button";
            TurnOff_button.Size = new Size(136, 40);
            TurnOff_button.TabIndex = 1;
            TurnOff_button.Text = "Turn off music";
            TurnOff_button.UseVisualStyleBackColor = true;
            TurnOff_button.Click += TurnOff_button_Click;
            // 
            // BackToMain_button
            // 
            BackToMain_button.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BackToMain_button.Location = new Point(168, 308);
            BackToMain_button.Name = "BackToMain_button";
            BackToMain_button.Size = new Size(136, 39);
            BackToMain_button.TabIndex = 3;
            BackToMain_button.Text = "Back to Main Menu";
            BackToMain_button.UseVisualStyleBackColor = true;
            BackToMain_button.Click += BackToMain_button_Click;
            // 
            // ResetHighscoreButton
            // 
            ResetHighscoreButton.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            ResetHighscoreButton.Location = new Point(168, 235);
            ResetHighscoreButton.Name = "ResetHighscoreButton";
            ResetHighscoreButton.Size = new Size(136, 39);
            ResetHighscoreButton.TabIndex = 4;
            ResetHighscoreButton.Text = "Reset Highscore";
            ResetHighscoreButton.UseVisualStyleBackColor = true;
            ResetHighscoreButton.Click += ResetHighScore_Click;
            // 
            // OptionsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightSeaGreen;
            ClientSize = new Size(478, 450);
            Controls.Add(BackToMain_button);
            Controls.Add(TurnOff_button);
            Controls.Add(Options_Label);
            Controls.Add(ResetHighscoreButton);
            Name = "OptionsForm";
            Text = "OptionsForm";
            Load += OptionsForm_Load_1;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Options_Label;
        private Button TurnOff_button;
        private Button BackToMain_button;
        private Button ResetHighscoreButton; // Move the declaration here
    }
}
