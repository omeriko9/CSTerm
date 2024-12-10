namespace CSTerm
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.txtRx = new System.Windows.Forms.TextBox();
            this.txtTx = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtRx
            // 
            this.txtRx.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRx.BackColor = System.Drawing.SystemColors.Window;
            this.txtRx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRx.Font = new System.Drawing.Font("Monotype Corsiva", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtRx.Location = new System.Drawing.Point(12, 38);
            this.txtRx.Multiline = true;
            this.txtRx.Name = "txtRx";
            this.txtRx.ReadOnly = true;
            this.txtRx.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRx.Size = new System.Drawing.Size(719, 351);
            this.txtRx.TabIndex = 0;
            // 
            // txtTx
            // 
            this.txtTx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTx.Location = new System.Drawing.Point(12, 396);
            this.txtTx.Name = "txtTx";
            this.txtTx.Size = new System.Drawing.Size(719, 20);
            this.txtTx.TabIndex = 1;
            this.txtTx.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTx_KeyDown);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(12, 9);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(479, 23);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(575, 9);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(75, 23);
            this.btnSettings.TabIndex = 3;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(656, 9);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 427);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtTx);
            this.Controls.Add(this.txtRx);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "CSTerm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRx;
        private System.Windows.Forms.TextBox txtTx;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnClear;
    }
}

