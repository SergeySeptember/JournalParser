namespace JournalParserCore
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            button = new RoundButton();
            label = new Label();
            pictureBoxClose = new PictureBox();
            linkToGit = new Label();
            labelLogo = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBoxClose).BeginInit();
            SuspendLayout();
            // 
            // button
            // 
            button.BackColor = Color.Transparent;
            button.Location = new Point(64, 84);
            button.Name = "button";
            button.Size = new Size(75, 23);
            button.TabIndex = 0;
            button.Text = "Click Me";
            button.UseVisualStyleBackColor = false;
            button.Click += ButtonBrowseFileClick;
            // 
            // label
            // 
            label.AutoSize = true;
            label.BackColor = Color.Transparent;
            label.Font = new Font("Microsoft YaHei", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label.Location = new Point(52, 57);
            label.Name = "label";
            label.Size = new Size(98, 17);
            label.TabIndex = 1;
            label.Text = "Select Excel File";
            // 
            // pictureBoxClose
            // 
            pictureBoxClose.BackColor = Color.Transparent;
            pictureBoxClose.Image = Properties.Resources.close_icon;
            pictureBoxClose.Location = new Point(176, 12);
            pictureBoxClose.Name = "pictureBoxClose";
            pictureBoxClose.Size = new Size(17, 17);
            pictureBoxClose.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxClose.TabIndex = 2;
            pictureBoxClose.TabStop = false;
            pictureBoxClose.Click += PictureBoxCloseClick;
            // 
            // linkToGit
            // 
            linkToGit.AutoSize = true;
            linkToGit.BackColor = Color.Transparent;
            linkToGit.ForeColor = Color.DimGray;
            linkToGit.Location = new Point(42, 134);
            linkToGit.Name = "linkToGit";
            linkToGit.Size = new Size(118, 15);
            linkToGit.TabIndex = 3;
            linkToGit.Text = "by Sergey September";
            linkToGit.Click += LinkToGitClick;
            // 
            // labelLogo
            // 
            labelLogo.AutoSize = true;
            labelLogo.BackColor = Color.Transparent;
            labelLogo.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Point);
            labelLogo.ForeColor = Color.Gainsboro;
            labelLogo.Location = new Point(20, 7);
            labelLogo.Name = "labelLogo";
            labelLogo.Size = new Size(153, 26);
            labelLogo.TabIndex = 4;
            labelLogo.Text = "Journal Parser";
            labelLogo.MouseDown += LabelLogoMouseDown;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Fon;
            ClientSize = new Size(203, 160);
            Controls.Add(labelLogo);
            Controls.Add(linkToGit);
            Controls.Add(pictureBoxClose);
            Controls.Add(label);
            Controls.Add(button);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Journal Parser";
            MouseDown += MainFormMouseDown;
            ((System.ComponentModel.ISupportInitialize)pictureBoxClose).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RoundButton button;
        private Label label;
        private PictureBox pictureBoxClose;
        private Label linkToGit;
        private Label labelLogo;
    }
}