namespace TransfertBDD
{
    partial class PopUpClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopUpClient));
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.clientText = new DevExpress.XtraEditors.TextEdit();
            this.AnnulButton = new DevExpress.XtraEditors.SimpleButton();
            this.OKButton = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.clientText.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Office 2007 Silver";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(253, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Entrez le nom du nouveau Client : ";
            // 
            // clientText
            // 
            this.clientText.Location = new System.Drawing.Point(16, 55);
            this.clientText.Name = "clientText";
            this.clientText.Size = new System.Drawing.Size(249, 23);
            this.clientText.TabIndex = 1;
            // 
            // AnnulButton
            // 
            this.AnnulButton.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.AnnulButton.Image = ((System.Drawing.Image)(resources.GetObject("AnnulButton.Image")));
            this.AnnulButton.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.AnnulButton.Location = new System.Drawing.Point(54, 84);
            this.AnnulButton.Name = "AnnulButton";
            this.AnnulButton.Size = new System.Drawing.Size(40, 40);
            this.AnnulButton.TabIndex = 3;
            this.AnnulButton.Click += new System.EventHandler(this.AnnulButton_Click);
            // 
            // OKButton
            // 
            this.OKButton.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.OKButton.Image = ((System.Drawing.Image)(resources.GetObject("OKButton.Image")));
            this.OKButton.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.OKButton.Location = new System.Drawing.Point(179, 84);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(40, 40);
            this.OKButton.TabIndex = 4;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // PopUpClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 135);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.AnnulButton);
            this.Controls.Add(this.clientText);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PopUpClient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PopUpClient";
            ((System.ComponentModel.ISupportInitialize)(this.clientText.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit clientText;
        private DevExpress.XtraEditors.SimpleButton AnnulButton;
        private DevExpress.XtraEditors.SimpleButton OKButton;
    }
}