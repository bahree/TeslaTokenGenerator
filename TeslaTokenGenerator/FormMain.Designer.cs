namespace TeslaTokenGenerator
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.buttonGetToken = new System.Windows.Forms.Button();
            this.groupBoxLogin = new System.Windows.Forms.GroupBox();
            this.checkBoxShowPassword = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelEmail = new System.Windows.Forms.Label();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.groupBoxAccessToken = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxTokenExpiry = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxRefreshToken = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxTokenCreation = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxToken = new System.Windows.Forms.TextBox();
            this.buttonClipboardCopy = new System.Windows.Forms.Button();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonClear = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonGenerateCuRL = new System.Windows.Forms.Button();
            this.buttonTestToken = new System.Windows.Forms.Button();
            this.buttonRevokeToken = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBoxCopyAllTokenDetails = new System.Windows.Forms.CheckBox();
            this.groupBoxLogin.SuspendLayout();
            this.groupBoxAccessToken.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonGetToken
            // 
            this.buttonGetToken.Location = new System.Drawing.Point(26, 272);
            this.buttonGetToken.Name = "buttonGetToken";
            this.buttonGetToken.Size = new System.Drawing.Size(225, 50);
            this.buttonGetToken.TabIndex = 2;
            this.buttonGetToken.Text = "Generate Token";
            this.toolTip1.SetToolTip(this.buttonGetToken, "Get access token");
            this.buttonGetToken.UseVisualStyleBackColor = true;
            this.buttonGetToken.Click += new System.EventHandler(this.buttonGetToken_Click);
            // 
            // groupBoxLogin
            // 
            this.groupBoxLogin.Controls.Add(this.checkBoxShowPassword);
            this.groupBoxLogin.Controls.Add(this.label1);
            this.groupBoxLogin.Controls.Add(this.textBoxPassword);
            this.groupBoxLogin.Controls.Add(this.labelEmail);
            this.groupBoxLogin.Controls.Add(this.textBoxEmail);
            this.groupBoxLogin.Location = new System.Drawing.Point(26, 78);
            this.groupBoxLogin.Name = "groupBoxLogin";
            this.groupBoxLogin.Size = new System.Drawing.Size(720, 188);
            this.groupBoxLogin.TabIndex = 1;
            this.groupBoxLogin.TabStop = false;
            this.groupBoxLogin.Text = "Login Details";
            // 
            // checkBoxShowPassword
            // 
            this.checkBoxShowPassword.AutoSize = true;
            this.checkBoxShowPassword.Location = new System.Drawing.Point(157, 137);
            this.checkBoxShowPassword.Name = "checkBoxShowPassword";
            this.checkBoxShowPassword.Size = new System.Drawing.Size(197, 29);
            this.checkBoxShowPassword.TabIndex = 4;
            this.checkBoxShowPassword.Text = "Show Password";
            this.toolTip1.SetToolTip(this.checkBoxShowPassword, "Toggle to show password in clear text");
            this.checkBoxShowPassword.UseVisualStyleBackColor = true;
            this.checkBoxShowPassword.CheckedChanged += new System.EventHandler(this.checkBoxShowPassword_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Password:";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(157, 90);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(547, 31);
            this.textBoxPassword.TabIndex = 3;
            this.toolTip1.SetToolTip(this.textBoxPassword, "Tesla Login password");
            this.textBoxPassword.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxPassword_Validating);
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(21, 46);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(71, 25);
            this.labelEmail.TabIndex = 0;
            this.labelEmail.Text = "Email:";
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Location = new System.Drawing.Point(157, 46);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(547, 31);
            this.textBoxEmail.TabIndex = 1;
            this.toolTip1.SetToolTip(this.textBoxEmail, "Tesla Login Email");
            this.textBoxEmail.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxEmail_Validating);
            // 
            // groupBoxAccessToken
            // 
            this.groupBoxAccessToken.Controls.Add(this.label4);
            this.groupBoxAccessToken.Controls.Add(this.textBoxTokenExpiry);
            this.groupBoxAccessToken.Controls.Add(this.label5);
            this.groupBoxAccessToken.Controls.Add(this.textBoxRefreshToken);
            this.groupBoxAccessToken.Controls.Add(this.label2);
            this.groupBoxAccessToken.Controls.Add(this.textBoxTokenCreation);
            this.groupBoxAccessToken.Controls.Add(this.label3);
            this.groupBoxAccessToken.Controls.Add(this.textBoxToken);
            this.groupBoxAccessToken.Location = new System.Drawing.Point(26, 359);
            this.groupBoxAccessToken.Name = "groupBoxAccessToken";
            this.groupBoxAccessToken.Size = new System.Drawing.Size(1018, 248);
            this.groupBoxAccessToken.TabIndex = 5;
            this.groupBoxAccessToken.TabStop = false;
            this.groupBoxAccessToken.Text = "Access Token";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(75, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 25);
            this.label4.TabIndex = 4;
            this.label4.Text = "Expires:";
            // 
            // textBoxTokenExpiry
            // 
            this.textBoxTokenExpiry.Location = new System.Drawing.Point(186, 140);
            this.textBoxTokenExpiry.Name = "textBoxTokenExpiry";
            this.textBoxTokenExpiry.ReadOnly = true;
            this.textBoxTokenExpiry.Size = new System.Drawing.Size(547, 31);
            this.textBoxTokenExpiry.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 194);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(159, 25);
            this.label5.TabIndex = 6;
            this.label5.Text = "Refresh Token:";
            // 
            // textBoxRefreshToken
            // 
            this.textBoxRefreshToken.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxRefreshToken.Location = new System.Drawing.Point(186, 194);
            this.textBoxRefreshToken.Name = "textBoxRefreshToken";
            this.textBoxRefreshToken.ReadOnly = true;
            this.textBoxRefreshToken.Size = new System.Drawing.Size(801, 31);
            this.textBoxRefreshToken.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Created:";
            // 
            // textBoxTokenCreation
            // 
            this.textBoxTokenCreation.Location = new System.Drawing.Point(186, 90);
            this.textBoxTokenCreation.Name = "textBoxTokenCreation";
            this.textBoxTokenCreation.ReadOnly = true;
            this.textBoxTokenCreation.Size = new System.Drawing.Size(547, 31);
            this.textBoxTokenCreation.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(87, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "Token:";
            // 
            // textBoxToken
            // 
            this.textBoxToken.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxToken.Location = new System.Drawing.Point(186, 40);
            this.textBoxToken.Name = "textBoxToken";
            this.textBoxToken.ReadOnly = true;
            this.textBoxToken.Size = new System.Drawing.Size(801, 31);
            this.textBoxToken.TabIndex = 1;
            // 
            // buttonClipboardCopy
            // 
            this.buttonClipboardCopy.Enabled = false;
            this.buttonClipboardCopy.Location = new System.Drawing.Point(26, 625);
            this.buttonClipboardCopy.Name = "buttonClipboardCopy";
            this.buttonClipboardCopy.Size = new System.Drawing.Size(225, 50);
            this.buttonClipboardCopy.TabIndex = 6;
            this.buttonClipboardCopy.Text = "Copy to Clipboard";
            this.toolTip1.SetToolTip(this.buttonClipboardCopy, "Copy token to clipboard, post which you can paste these whever you want");
            this.buttonClipboardCopy.UseVisualStyleBackColor = true;
            this.buttonClipboardCopy.Click += new System.EventHandler(this.buttonClipboardCopy_Click);
            // 
            // statusStripMain
            // 
            this.statusStripMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStripMain.Location = new System.Drawing.Point(0, 705);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(1076, 37);
            this.statusStripMain.TabIndex = 13;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(238, 32);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(273, 272);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(225, 50);
            this.buttonClear.TabIndex = 3;
            this.buttonClear.Text = "Clear";
            this.toolTip1.SetToolTip(this.buttonClear, "Clear all the data including the generated token");
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonGenerateCuRL
            // 
            this.buttonGenerateCuRL.Location = new System.Drawing.Point(521, 272);
            this.buttonGenerateCuRL.Name = "buttonGenerateCuRL";
            this.buttonGenerateCuRL.Size = new System.Drawing.Size(225, 50);
            this.buttonGenerateCuRL.TabIndex = 4;
            this.buttonGenerateCuRL.Text = "Generate cURL";
            this.toolTip1.SetToolTip(this.buttonGenerateCuRL, "Generate a cURL message and copy it to the clipboard");
            this.buttonGenerateCuRL.UseVisualStyleBackColor = true;
            this.buttonGenerateCuRL.Click += new System.EventHandler(this.buttonGenerateCuRL_Click);
            // 
            // buttonTestToken
            // 
            this.buttonTestToken.Enabled = false;
            this.buttonTestToken.Location = new System.Drawing.Point(273, 625);
            this.buttonTestToken.Name = "buttonTestToken";
            this.buttonTestToken.Size = new System.Drawing.Size(225, 50);
            this.buttonTestToken.TabIndex = 7;
            this.buttonTestToken.Text = "Validate New Token";
            this.toolTip1.SetToolTip(this.buttonTestToken, "Test token generated");
            this.buttonTestToken.UseVisualStyleBackColor = true;
            this.buttonTestToken.Click += new System.EventHandler(this.buttonTestToken_Click);
            // 
            // buttonRevokeToken
            // 
            this.buttonRevokeToken.Location = new System.Drawing.Point(521, 625);
            this.buttonRevokeToken.Name = "buttonRevokeToken";
            this.buttonRevokeToken.Size = new System.Drawing.Size(225, 50);
            this.buttonRevokeToken.TabIndex = 8;
            this.buttonRevokeToken.Text = "Revoke Token";
            this.toolTip1.SetToolTip(this.buttonRevokeToken, "Get access token");
            this.buttonRevokeToken.UseVisualStyleBackColor = true;
            this.buttonRevokeToken.Click += new System.EventHandler(this.buttonRevokeToken_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1076, 40);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem_Exit,
            this.aboutToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(64, 36);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem_Exit
            // 
            this.exitToolStripMenuItem_Exit.Name = "exitToolStripMenuItem_Exit";
            this.exitToolStripMenuItem_Exit.Size = new System.Drawing.Size(179, 38);
            this.exitToolStripMenuItem_Exit.Text = "E&xit";
            this.exitToolStripMenuItem_Exit.Click += new System.EventHandler(this.exitToolStripMenuItemExit_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(179, 38);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // checkBoxCopyAllTokenDetails
            // 
            this.checkBoxCopyAllTokenDetails.AutoSize = true;
            this.checkBoxCopyAllTokenDetails.Checked = true;
            this.checkBoxCopyAllTokenDetails.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCopyAllTokenDetails.Location = new System.Drawing.Point(767, 637);
            this.checkBoxCopyAllTokenDetails.Name = "checkBoxCopyAllTokenDetails";
            this.checkBoxCopyAllTokenDetails.Size = new System.Drawing.Size(262, 29);
            this.checkBoxCopyAllTokenDetails.TabIndex = 9;
            this.checkBoxCopyAllTokenDetails.Text = "Copy All Token Details";
            this.checkBoxCopyAllTokenDetails.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 742);
            this.Controls.Add(this.checkBoxCopyAllTokenDetails);
            this.Controls.Add(this.buttonRevokeToken);
            this.Controls.Add(this.buttonTestToken);
            this.Controls.Add(this.buttonGenerateCuRL);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.buttonClipboardCopy);
            this.Controls.Add(this.groupBoxAccessToken);
            this.Controls.Add(this.groupBoxLogin);
            this.Controls.Add(this.buttonGetToken);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "Tesla AccessToken Generator";
            this.groupBoxLogin.ResumeLayout(false);
            this.groupBoxLogin.PerformLayout();
            this.groupBoxAccessToken.ResumeLayout(false);
            this.groupBoxAccessToken.PerformLayout();
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonGetToken;
        private System.Windows.Forms.GroupBox groupBoxLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.CheckBox checkBoxShowPassword;
        private System.Windows.Forms.GroupBox groupBoxAccessToken;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxTokenCreation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxToken;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxRefreshToken;
        private System.Windows.Forms.Button buttonClipboardCopy;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonGenerateCuRL;
        private System.Windows.Forms.Button buttonTestToken;
        private System.Windows.Forms.Button buttonRevokeToken;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem_Exit;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxTokenExpiry;
        private System.Windows.Forms.CheckBox checkBoxCopyAllTokenDetails;
    }
}

