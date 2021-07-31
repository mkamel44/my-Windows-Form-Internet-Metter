namespace WindowsFormsApplication1
{
    partial class Form3
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
        	System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
        	this.label1 = new System.Windows.Forms.Label();
        	this.button1 = new System.Windows.Forms.Button();
        	this.SuspendLayout();
        	// 
        	// label1
        	// 
        	this.label1.AutoSize = true;
        	this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.label1.ForeColor = System.Drawing.Color.Olive;
        	this.label1.Location = new System.Drawing.Point(22, 11);
        	this.label1.Name = "label1";
        	this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
        	this.label1.Size = new System.Drawing.Size(399, 133);
        	this.label1.TabIndex = 0;
        	this.label1.Text = "هذا البرنامج من انتاج محمد كامل بن كمال الدين كامل\r\n\r\nرقم الهاتف 00963933656429\r\n" +
        	"\r\nرقم الهاتف 00963956302923\r\n\r\nالبريد الالكتروني mhamadkamel@hotmail.com";
        	this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        	// 
        	// button1
        	// 
        	this.button1.Location = new System.Drawing.Point(179, 151);
        	this.button1.Name = "button1";
        	this.button1.Size = new System.Drawing.Size(75, 23);
        	this.button1.TabIndex = 1;
        	this.button1.Text = "اغلاق";
        	this.button1.UseVisualStyleBackColor = true;
        	this.button1.Click += new System.EventHandler(this.Button1Click);
        	// 
        	// Form3
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.ClientSize = new System.Drawing.Size(433, 186);
        	this.Controls.Add(this.button1);
        	this.Controls.Add(this.label1);
        	this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        	this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        	this.MaximizeBox = false;
        	this.MinimizeBox = false;
        	this.Name = "Form3";
        	this.ShowInTaskbar = false;
        	this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        	this.Text = "حول البرنامج";
        	this.ResumeLayout(false);
        	this.PerformLayout();
        }
        private System.Windows.Forms.Button button1;

        #endregion

        private System.Windows.Forms.Label label1;
    }
}