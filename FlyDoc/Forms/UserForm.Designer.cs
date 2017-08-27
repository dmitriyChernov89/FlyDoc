namespace FlyDoc.Forms
{
    partial class UserForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.tbxUserName = new System.Windows.Forms.TextBox();
            this.tbxPC = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxDepartment = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxNote = new System.Windows.Forms.CheckBox();
            this.checkBoxSchedule = new System.Windows.Forms.CheckBox();
            this.checkBoxPhonebook = new System.Windows.Forms.CheckBox();
            this.checkBoxConfig = new System.Windows.Forms.CheckBox();
            this.checkBoxApprovedN = new System.Windows.Forms.CheckBox();
            this.checkBoxApprovedSB = new System.Windows.Forms.CheckBox();
            this.checkBoxApprovedDir = new System.Windows.Forms.CheckBox();
            this.tbxMail = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 416);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(389, 64);
            this.panel1.TabIndex = 10;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(286, 17);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(81, 30);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Відмінити";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(170, 17);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(81, 30);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Зберегти";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // tbxUserName
            // 
            this.tbxUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbxUserName.Location = new System.Drawing.Point(127, 68);
            this.tbxUserName.Name = "tbxUserName";
            this.tbxUserName.Size = new System.Drawing.Size(212, 22);
            this.tbxUserName.TabIndex = 3;
            // 
            // tbxPC
            // 
            this.tbxPC.CausesValidation = false;
            this.tbxPC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbxPC.Location = new System.Drawing.Point(127, 33);
            this.tbxPC.Name = "tbxPC";
            this.tbxPC.Size = new System.Drawing.Size(212, 22);
            this.tbxPC.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "UserName";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "PC";
            // 
            // cbxDepartment
            // 
            this.cbxDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDepartment.FormattingEnabled = true;
            this.cbxDepartment.Location = new System.Drawing.Point(127, 144);
            this.cbxDepartment.Name = "cbxDepartment";
            this.cbxDepartment.Size = new System.Drawing.Size(212, 21);
            this.cbxDepartment.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Відділ";
            // 
            // checkBoxNote
            // 
            this.checkBoxNote.AutoSize = true;
            this.checkBoxNote.Location = new System.Drawing.Point(127, 204);
            this.checkBoxNote.Name = "checkBoxNote";
            this.checkBoxNote.Size = new System.Drawing.Size(76, 17);
            this.checkBoxNote.TabIndex = 6;
            this.checkBoxNote.Text = "allow Note";
            this.checkBoxNote.UseVisualStyleBackColor = true;
            // 
            // checkBoxSchedule
            // 
            this.checkBoxSchedule.AutoSize = true;
            this.checkBoxSchedule.Location = new System.Drawing.Point(127, 227);
            this.checkBoxSchedule.Name = "checkBoxSchedule";
            this.checkBoxSchedule.Size = new System.Drawing.Size(98, 17);
            this.checkBoxSchedule.TabIndex = 7;
            this.checkBoxSchedule.Text = "allow Schedule";
            this.checkBoxSchedule.UseVisualStyleBackColor = true;
            // 
            // checkBoxPhonebook
            // 
            this.checkBoxPhonebook.AutoSize = true;
            this.checkBoxPhonebook.Location = new System.Drawing.Point(127, 250);
            this.checkBoxPhonebook.Name = "checkBoxPhonebook";
            this.checkBoxPhonebook.Size = new System.Drawing.Size(108, 17);
            this.checkBoxPhonebook.TabIndex = 8;
            this.checkBoxPhonebook.Text = "allow Phonebook";
            this.checkBoxPhonebook.UseVisualStyleBackColor = true;
            // 
            // checkBoxConfig
            // 
            this.checkBoxConfig.AutoSize = true;
            this.checkBoxConfig.Location = new System.Drawing.Point(127, 273);
            this.checkBoxConfig.Name = "checkBoxConfig";
            this.checkBoxConfig.Size = new System.Drawing.Size(83, 17);
            this.checkBoxConfig.TabIndex = 9;
            this.checkBoxConfig.Text = "allow Config";
            this.checkBoxConfig.UseVisualStyleBackColor = true;
            // 
            // checkBoxApprovedN
            // 
            this.checkBoxApprovedN.AutoSize = true;
            this.checkBoxApprovedN.Location = new System.Drawing.Point(127, 297);
            this.checkBoxApprovedN.Name = "checkBoxApprovedN";
            this.checkBoxApprovedN.Size = new System.Drawing.Size(125, 17);
            this.checkBoxApprovedN.TabIndex = 11;
            this.checkBoxApprovedN.Text = "allow ApprovedNach";
            this.checkBoxApprovedN.UseVisualStyleBackColor = true;
            // 
            // checkBoxApprovedSB
            // 
            this.checkBoxApprovedSB.AutoSize = true;
            this.checkBoxApprovedSB.Location = new System.Drawing.Point(127, 321);
            this.checkBoxApprovedSB.Name = "checkBoxApprovedSB";
            this.checkBoxApprovedSB.Size = new System.Drawing.Size(113, 17);
            this.checkBoxApprovedSB.TabIndex = 12;
            this.checkBoxApprovedSB.Text = "allow ApprovedSB";
            this.checkBoxApprovedSB.UseVisualStyleBackColor = true;
            // 
            // checkBoxApprovedDir
            // 
            this.checkBoxApprovedDir.AutoSize = true;
            this.checkBoxApprovedDir.Location = new System.Drawing.Point(127, 345);
            this.checkBoxApprovedDir.Name = "checkBoxApprovedDir";
            this.checkBoxApprovedDir.Size = new System.Drawing.Size(112, 17);
            this.checkBoxApprovedDir.TabIndex = 13;
            this.checkBoxApprovedDir.Text = "allow ApprovedDir";
            this.checkBoxApprovedDir.UseVisualStyleBackColor = true;
            // 
            // tbxMail
            // 
            this.tbxMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbxMail.Location = new System.Drawing.Point(127, 106);
            this.tbxMail.Name = "tbxMail";
            this.tbxMail.Size = new System.Drawing.Size(212, 22);
            this.tbxMail.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Mail";
            // 
            // UserForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(389, 480);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbxMail);
            this.Controls.Add(this.checkBoxApprovedDir);
            this.Controls.Add(this.checkBoxApprovedSB);
            this.Controls.Add(this.checkBoxApprovedN);
            this.Controls.Add(this.checkBoxConfig);
            this.Controls.Add(this.checkBoxPhonebook);
            this.Controls.Add(this.checkBoxSchedule);
            this.Controls.Add(this.checkBoxNote);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbxDepartment);
            this.Controls.Add(this.tbxUserName);
            this.Controls.Add(this.tbxPC);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserForm";
            this.Text = "UserForm";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox tbxUserName;
        private System.Windows.Forms.TextBox tbxPC;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxDepartment;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxNote;
        private System.Windows.Forms.CheckBox checkBoxSchedule;
        private System.Windows.Forms.CheckBox checkBoxPhonebook;
        private System.Windows.Forms.CheckBox checkBoxConfig;
        private System.Windows.Forms.CheckBox checkBoxApprovedN;
        private System.Windows.Forms.CheckBox checkBoxApprovedSB;
        private System.Windows.Forms.CheckBox checkBoxApprovedDir;
        private System.Windows.Forms.TextBox tbxMail;
        private System.Windows.Forms.Label label4;
    }
}