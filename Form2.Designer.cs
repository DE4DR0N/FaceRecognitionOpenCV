namespace CW_OOP_Test1
{
    partial class Form2
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
            this.brwsBttn = new System.Windows.Forms.Button();
            this.previewBox = new System.Windows.Forms.PictureBox();
            this.addPerson = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
            this.SuspendLayout();
            // 
            // brwsBttn
            // 
            this.brwsBttn.Location = new System.Drawing.Point(12, 331);
            this.brwsBttn.Name = "brwsBttn";
            this.brwsBttn.Size = new System.Drawing.Size(368, 50);
            this.brwsBttn.TabIndex = 0;
            this.brwsBttn.Text = "Browse";
            this.brwsBttn.UseVisualStyleBackColor = true;
            this.brwsBttn.Click += new System.EventHandler(this.brwsBttn_Click);
            // 
            // previewBox
            // 
            this.previewBox.Location = new System.Drawing.Point(12, 12);
            this.previewBox.Name = "previewBox";
            this.previewBox.Size = new System.Drawing.Size(368, 313);
            this.previewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.previewBox.TabIndex = 1;
            this.previewBox.TabStop = false;
            // 
            // addPerson
            // 
            this.addPerson.Location = new System.Drawing.Point(12, 433);
            this.addPerson.Name = "addPerson";
            this.addPerson.Size = new System.Drawing.Size(368, 27);
            this.addPerson.TabIndex = 2;
            this.addPerson.Text = "Add person";
            this.addPerson.UseVisualStyleBackColor = true;
            this.addPerson.Click += new System.EventHandler(this.addPerson_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(65, 405);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(315, 22);
            this.textBox1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 411);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Name:";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 472);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.addPerson);
            this.Controls.Add(this.previewBox);
            this.Controls.Add(this.brwsBttn);
            this.Name = "Form2";
            this.Text = "Add new person";
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button brwsBttn;
        private System.Windows.Forms.PictureBox previewBox;
        private System.Windows.Forms.Button addPerson;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
    }
}