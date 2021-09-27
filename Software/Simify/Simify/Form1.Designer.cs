
namespace Simify
{
    partial class Form1
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
            this.btnOn = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.cboxPorts = new System.Windows.Forms.ComboBox();
            this.btnOff = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOn
            // 
            this.btnOn.Location = new System.Drawing.Point(481, 195);
            this.btnOn.Name = "btnOn";
            this.btnOn.Size = new System.Drawing.Size(75, 23);
            this.btnOn.TabIndex = 0;
            this.btnOn.Text = "On";
            this.btnOn.UseVisualStyleBackColor = true;
            this.btnOn.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(40, 30);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // cboxPorts
            // 
            this.cboxPorts.FormattingEnabled = true;
            this.cboxPorts.Location = new System.Drawing.Point(142, 30);
            this.cboxPorts.Name = "cboxPorts";
            this.cboxPorts.Size = new System.Drawing.Size(121, 24);
            this.cboxPorts.TabIndex = 2;
            // 
            // btnOff
            // 
            this.btnOff.Location = new System.Drawing.Point(588, 193);
            this.btnOff.Name = "btnOff";
            this.btnOff.Size = new System.Drawing.Size(93, 25);
            this.btnOff.TabIndex = 3;
            this.btnOff.Text = "Off";
            this.btnOff.UseVisualStyleBackColor = true;
            this.btnOff.Click += new System.EventHandler(this.btnOff_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnOff);
            this.Controls.Add(this.cboxPorts);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnOn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOn;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cboxPorts;
        private System.Windows.Forms.Button btnOff;
    }
}

