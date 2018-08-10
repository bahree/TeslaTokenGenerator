namespace TeslaTokenGenerator {
    partial class FormRevokeToken {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.buttonRevoke = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxToken = new System.Windows.Forms.TextBox();
            this.buttonTestToken = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.textBoxNote = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonRevoke
            // 
            this.buttonRevoke.Location = new System.Drawing.Point(380, 108);
            this.buttonRevoke.Name = "buttonRevoke";
            this.buttonRevoke.Size = new System.Drawing.Size(225, 48);
            this.buttonRevoke.TabIndex = 3;
            this.buttonRevoke.Text = "Revoke Token";
            this.buttonRevoke.UseVisualStyleBackColor = true;
            this.buttonRevoke.Click += new System.EventHandler(this.buttonRevoke_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "Token:";
            // 
            // textBoxToken
            // 
            this.textBoxToken.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxToken.Location = new System.Drawing.Point(137, 53);
            this.textBoxToken.Name = "textBoxToken";
            this.textBoxToken.Size = new System.Drawing.Size(801, 31);
            this.textBoxToken.TabIndex = 1;
            // 
            // buttonTestToken
            // 
            this.buttonTestToken.Location = new System.Drawing.Point(137, 106);
            this.buttonTestToken.Name = "buttonTestToken";
            this.buttonTestToken.Size = new System.Drawing.Size(225, 50);
            this.buttonTestToken.TabIndex = 2;
            this.buttonTestToken.Text = "Validate Token";
            this.buttonTestToken.UseVisualStyleBackColor = true;
            this.buttonTestToken.Click += new System.EventHandler(this.buttonTestToken_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(621, 108);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(225, 48);
            this.buttonClose.TabIndex = 4;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // textBoxNote
            // 
            this.textBoxNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNote.Location = new System.Drawing.Point(137, 186);
            this.textBoxNote.Multiline = true;
            this.textBoxNote.Name = "textBoxNote";
            this.textBoxNote.ReadOnly = true;
            this.textBoxNote.Size = new System.Drawing.Size(782, 62);
            this.textBoxNote.TabIndex = 5;
            this.textBoxNote.TabStop = false;
            this.textBoxNote.Text = "NOTE: The revoke token still doesn\'t revoke it. Either the Tesla API has changed " +
    "or there is a bug on their end.";
            // 
            // FormRevokeToken
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(955, 271);
            this.Controls.Add(this.textBoxNote);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonTestToken);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxToken);
            this.Controls.Add(this.buttonRevoke);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormRevokeToken";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Revoke Token";
            this.Load += new System.EventHandler(this.FormRevokeToken_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRevoke;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxToken;
        private System.Windows.Forms.Button buttonTestToken;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.TextBox textBoxNote;
    }
}