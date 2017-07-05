namespace ScanIt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.txtInvtedFullName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.comboInvitors = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txtInvtedFullName
            // 
            this.txtInvtedFullName.Location = new System.Drawing.Point(154, 47);
            this.txtInvtedFullName.Name = "txtInvtedFullName";
            this.txtInvtedFullName.Size = new System.Drawing.Size(211, 20);
            this.txtInvtedFullName.TabIndex = 0;
            this.txtInvtedFullName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(382, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "שם המוזמן";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(405, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "ברקוד";
            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(154, 82);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(211, 20);
            this.txtBarcode.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(329, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "מזמין";
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Khaki;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(23, 110);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 58);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "הוסף";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // comboInvitors
            // 
            this.comboInvitors.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboInvitors.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboInvitors.FormattingEnabled = true;
            this.comboInvitors.Location = new System.Drawing.Point(154, 147);
            this.comboInvitors.Name = "comboInvitors";
            this.comboInvitors.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comboInvitors.Size = new System.Drawing.Size(211, 21);
            this.comboInvitors.TabIndex = 7;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gold;
            this.ClientSize = new System.Drawing.Size(464, 199);
            this.Controls.Add(this.comboInvitors);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtInvtedFullName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.Text = "הכנס מזמין";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtInvtedFullName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ComboBox comboInvitors;
    }
}