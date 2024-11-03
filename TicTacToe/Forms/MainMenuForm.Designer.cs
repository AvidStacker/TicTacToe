namespace TicTacToe
{
    partial class MainMenuForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.Titel_Main = new System.Windows.Forms.Label();
            this.StartGame_Click = new System.Windows.Forms.Button();
            this.Option_Click = new System.Windows.Forms.Button();
            this.Exit_Click = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Titel_Main
            // 
            this.Titel_Main.AutoSize = true;
            this.Titel_Main.Font = new System.Drawing.Font("Segoe UI Black", 36F, System.Drawing.FontStyle.Bold);
            this.Titel_Main.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Titel_Main.Location = new System.Drawing.Point(238, 41);
            this.Titel_Main.Name = "Titel_Main";
            this.Titel_Main.Size = new System.Drawing.Size(294, 65);
            this.Titel_Main.TabIndex = 0;
            this.Titel_Main.Text = "Tic Tac Toe";
            this.Titel_Main.Click += new System.EventHandler(this.label1_Click);
            // 
            // StartGame_Click
            // 
            this.StartGame_Click.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
            this.StartGame_Click.Location = new System.Drawing.Point(260, 158);
            this.StartGame_Click.Name = "StartGame_Click";
            this.StartGame_Click.Size = new System.Drawing.Size(243, 59);
            this.StartGame_Click.TabIndex = 1;
            this.StartGame_Click.Text = "Start Game";
            this.StartGame_Click.UseVisualStyleBackColor = true;
            this.StartGame_Click.Click += new System.EventHandler(this.button1_Click);
            // 
            // Option_Click
            // 
            this.Option_Click.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
            this.Option_Click.Location = new System.Drawing.Point(260, 269);
            this.Option_Click.Name = "Option_Click";
            this.Option_Click.Size = new System.Drawing.Size(243, 57);
            this.Option_Click.TabIndex = 2;
            this.Option_Click.Text = "Options";
            this.Option_Click.UseVisualStyleBackColor = true;
            this.Option_Click.Click += new System.EventHandler(this.button2_Click);
            // 
            // Exit_Click
            // 
            this.Exit_Click.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
            this.Exit_Click.Location = new System.Drawing.Point(260, 394);
            this.Exit_Click.Name = "Exit_Click";
            this.Exit_Click.Size = new System.Drawing.Size(243, 54);
            this.Exit_Click.TabIndex = 3;
            this.Exit_Click.Text = "Exit";
            this.Exit_Click.UseVisualStyleBackColor = true;
            this.Exit_Click.Click += new System.EventHandler(this.button3_Click);
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.ClientSize = new System.Drawing.Size(831, 580);
            this.Controls.Add(this.Exit_Click);
            this.Controls.Add(this.Option_Click);
            this.Controls.Add(this.StartGame_Click);
            this.Controls.Add(this.Titel_Main);
            this.Name = "MainMenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tic Tac Toe - Main Menu";
            this.Load += new System.EventHandler(this.MainMenuForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label Titel_Main;
        private System.Windows.Forms.Button StartGame_Click;
        private System.Windows.Forms.Button Option_Click;
        private System.Windows.Forms.Button Exit_Click;
    }
}
