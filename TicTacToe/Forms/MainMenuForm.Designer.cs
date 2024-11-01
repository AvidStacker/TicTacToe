namespace TicTacToe
{
    partial class MainMenuForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Titel_Main = new Label();
            StartGame_Click = new Button();
            Option_Click = new Button();
            Exit_Click = new Button();
            SuspendLayout();
            // 
            // Titel_Main
            // 
            Titel_Main.AutoSize = true;
            Titel_Main.Font = new Font("Segoe UI Black", 36F, FontStyle.Bold);
            Titel_Main.ForeColor = SystemColors.ButtonHighlight;
            Titel_Main.Location = new Point(238, 41);
            Titel_Main.Name = "Titel_Main";
            Titel_Main.Size = new Size(294, 65);
            Titel_Main.TabIndex = 0;
            Titel_Main.Text = "Tic Tac Toe";
            Titel_Main.Click += label1_Click;
            // 
            // StartGame_Click
            // 
            StartGame_Click.Font = new Font("Segoe UI", 18F, FontStyle.Bold | FontStyle.Italic);
            StartGame_Click.Location = new Point(260, 158);
            StartGame_Click.Name = "StartGame_Click";
            StartGame_Click.Size = new Size(243, 59);
            StartGame_Click.TabIndex = 1;
            StartGame_Click.Text = "Start Game";
            StartGame_Click.UseVisualStyleBackColor = true;
            StartGame_Click.Click += button1_Click;
            // 
            // Option_Click
            // 
            Option_Click.Font = new Font("Segoe UI", 18F, FontStyle.Bold | FontStyle.Italic);
            Option_Click.Location = new Point(260, 269);
            Option_Click.Name = "Option_Click";
            Option_Click.Size = new Size(243, 57);
            Option_Click.TabIndex = 2;
            Option_Click.Text = "Options";
            Option_Click.UseVisualStyleBackColor = true;
            Option_Click.Click += button2_Click;
            // 
            // Exit_Click
            // 
            Exit_Click.Font = new Font("Segoe UI", 18F, FontStyle.Bold | FontStyle.Italic);
            Exit_Click.Location = new Point(260, 394);
            Exit_Click.Name = "Exit_Click";
            Exit_Click.Size = new Size(243, 54);
            Exit_Click.TabIndex = 3;
            Exit_Click.Text = "Exit";
            Exit_Click.UseVisualStyleBackColor = true;
            Exit_Click.Click += button3_Click;
            // 
            // MainMenuForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightSeaGreen;
            ClientSize = new Size(831, 580);
            Controls.Add(Exit_Click);
            Controls.Add(Option_Click);
            Controls.Add(StartGame_Click);
            Controls.Add(Titel_Main);
            Name = "MainMenuForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Tic Tac Toe - Main Menu";
            Load += MainMenuForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label Titel_Main;
        private System.Windows.Forms.Button StartGame_Click;
        private System.Windows.Forms.Button Option_Click;
        private System.Windows.Forms.Button Exit_Click;
    }
}