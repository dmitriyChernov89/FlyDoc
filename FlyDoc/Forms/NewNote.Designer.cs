namespace FlyDoc.Forms
{
    partial class NewNote
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
            this.tbNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbDepartment = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSaveToDB = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbNoteTemplate = new System.Windows.Forms.ComboBox();
            this.dtpDateCreate = new System.Windows.Forms.DateTimePicker();
            this.dgvTable = new System.Windows.Forms.DataGridView();
            this.tbHeadDir = new System.Windows.Forms.TextBox();
            this.tbHeadNach = new System.Windows.Forms.TextBox();
            this.tbBodyUp = new System.Windows.Forms.TextBox();
            this.tbBodyDown = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbAvtor = new System.Windows.Forms.TextBox();
            this.btnApprDir = new System.Windows.Forms.Button();
            this.btnApprComdir = new System.Windows.Forms.Button();
            this.btnApprSB = new System.Windows.Forms.Button();
            this.btnApprSBNach = new System.Windows.Forms.Button();
            this.btnApprNach = new System.Windows.Forms.Button();
            this.btnApprKasa = new System.Windows.Forms.Button();
            this.btnApprASU = new System.Windows.Forms.Button();
            this.btnApprBuh = new System.Windows.Forms.Button();
            this.btnApprSklad = new System.Windows.Forms.Button();
            this.btnApprEnerg = new System.Windows.Forms.Button();
            this.btnApprDostavka = new System.Windows.Forms.Button();
            this.btnApprFin = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.grpApprove = new System.Windows.Forms.GroupBox();
            this.btnApprAvtor = new System.Windows.Forms.Button();
            this.labelApprAll = new System.Windows.Forms.Label();
            this.sendPrint = new System.Windows.Forms.PrintDialog();
            this.btnEditApprovers = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).BeginInit();
            this.grpApprove.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbNumber
            // 
            this.tbNumber.Enabled = false;
            this.tbNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.tbNumber.Location = new System.Drawing.Point(436, 150);
            this.tbNumber.Name = "tbNumber";
            this.tbNumber.Size = new System.Drawing.Size(140, 26);
            this.tbNumber.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(310, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Відділ:";
            // 
            // cbDepartment
            // 
            this.cbDepartment.Enabled = false;
            this.cbDepartment.FormattingEnabled = true;
            this.cbDepartment.Location = new System.Drawing.Point(355, 12);
            this.cbDepartment.Name = "cbDepartment";
            this.cbDepartment.Size = new System.Drawing.Size(140, 21);
            this.cbDepartment.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(96, 123);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Відміна";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSaveToDB
            // 
            this.btnSaveToDB.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSaveToDB.Location = new System.Drawing.Point(15, 123);
            this.btnSaveToDB.Name = "btnSaveToDB";
            this.btnSaveToDB.Size = new System.Drawing.Size(75, 23);
            this.btnSaveToDB.TabIndex = 7;
            this.btnSaveToDB.Text = "Записати";
            this.btnSaveToDB.UseVisualStyleBackColor = true;
            this.btnSaveToDB.Click += new System.EventHandler(this.btnSaveToDB_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Шаблон:";
            // 
            // cbNoteTemplate
            // 
            this.cbNoteTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNoteTemplate.FormattingEnabled = true;
            this.cbNoteTemplate.Location = new System.Drawing.Point(66, 12);
            this.cbNoteTemplate.Name = "cbNoteTemplate";
            this.cbNoteTemplate.Size = new System.Drawing.Size(238, 21);
            this.cbNoteTemplate.TabIndex = 9;
            // 
            // dtpDateCreate
            // 
            this.dtpDateCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtpDateCreate.Enabled = false;
            this.dtpDateCreate.Location = new System.Drawing.Point(15, 493);
            this.dtpDateCreate.MaxDate = new System.DateTime(2030, 1, 1, 0, 0, 0, 0);
            this.dtpDateCreate.MinDate = new System.DateTime(2017, 5, 1, 0, 0, 0, 0);
            this.dtpDateCreate.Name = "dtpDateCreate";
            this.dtpDateCreate.Size = new System.Drawing.Size(120, 20);
            this.dtpDateCreate.TabIndex = 10;
            this.dtpDateCreate.Value = new System.DateTime(2017, 5, 29, 0, 0, 0, 0);
            // 
            // dgvTable
            // 
            this.dgvTable.AllowUserToOrderColumns = true;
            this.dgvTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTable.Location = new System.Drawing.Point(15, 291);
            this.dgvTable.Name = "dgvTable";
            this.dgvTable.Size = new System.Drawing.Size(887, 129);
            this.dgvTable.TabIndex = 11;
            this.dgvTable.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvTable_DataError);
            // 
            // tbHeadDir
            // 
            this.tbHeadDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHeadDir.Enabled = false;
            this.tbHeadDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.tbHeadDir.Location = new System.Drawing.Point(507, 13);
            this.tbHeadDir.Multiline = true;
            this.tbHeadDir.Name = "tbHeadDir";
            this.tbHeadDir.Size = new System.Drawing.Size(395, 67);
            this.tbHeadDir.TabIndex = 12;
            // 
            // tbHeadNach
            // 
            this.tbHeadNach.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHeadNach.Enabled = false;
            this.tbHeadNach.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.tbHeadNach.Location = new System.Drawing.Point(507, 79);
            this.tbHeadNach.Multiline = true;
            this.tbHeadNach.Name = "tbHeadNach";
            this.tbHeadNach.Size = new System.Drawing.Size(395, 67);
            this.tbHeadNach.TabIndex = 13;
            // 
            // tbBodyUp
            // 
            this.tbBodyUp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBodyUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.tbBodyUp.Location = new System.Drawing.Point(15, 181);
            this.tbBodyUp.Multiline = true;
            this.tbBodyUp.Name = "tbBodyUp";
            this.tbBodyUp.Size = new System.Drawing.Size(887, 112);
            this.tbBodyUp.TabIndex = 14;
            // 
            // tbBodyDown
            // 
            this.tbBodyDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBodyDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.tbBodyDown.Location = new System.Drawing.Point(15, 417);
            this.tbBodyDown.Multiline = true;
            this.tbBodyDown.Name = "tbBodyDown";
            this.tbBodyDown.Size = new System.Drawing.Size(887, 70);
            this.tbBodyDown.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label2.Location = new System.Drawing.Point(241, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(189, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "Службова записка №:";
            // 
            // tbAvtor
            // 
            this.tbAvtor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAvtor.Location = new System.Drawing.Point(700, 496);
            this.tbAvtor.Multiline = true;
            this.tbAvtor.Name = "tbAvtor";
            this.tbAvtor.Size = new System.Drawing.Size(202, 22);
            this.tbAvtor.TabIndex = 17;
            // 
            // btnApprDir
            // 
            this.btnApprDir.BackColor = System.Drawing.SystemColors.Control;
            this.btnApprDir.Location = new System.Drawing.Point(7, 18);
            this.btnApprDir.Name = "btnApprDir";
            this.btnApprDir.Size = new System.Drawing.Size(75, 23);
            this.btnApprDir.TabIndex = 18;
            this.btnApprDir.Tag = "ApprDir";
            this.btnApprDir.Text = "Директор";
            this.btnApprDir.UseVisualStyleBackColor = false;
            this.btnApprDir.Click += new System.EventHandler(this.btnApprField_Click);
            // 
            // btnApprComdir
            // 
            this.btnApprComdir.Location = new System.Drawing.Point(88, 18);
            this.btnApprComdir.Name = "btnApprComdir";
            this.btnApprComdir.Size = new System.Drawing.Size(75, 23);
            this.btnApprComdir.TabIndex = 19;
            this.btnApprComdir.Tag = "ApprComdir";
            this.btnApprComdir.Text = "Комерц.";
            this.btnApprComdir.UseVisualStyleBackColor = true;
            this.btnApprComdir.Click += new System.EventHandler(this.btnApprField_Click);
            // 
            // btnApprSB
            // 
            this.btnApprSB.Location = new System.Drawing.Point(250, 18);
            this.btnApprSB.Name = "btnApprSB";
            this.btnApprSB.Size = new System.Drawing.Size(75, 23);
            this.btnApprSB.TabIndex = 21;
            this.btnApprSB.Tag = "ApprSB";
            this.btnApprSB.Text = "Інспектор";
            this.btnApprSB.UseVisualStyleBackColor = true;
            this.btnApprSB.Click += new System.EventHandler(this.btnApprField_Click);
            // 
            // btnApprSBNach
            // 
            this.btnApprSBNach.Location = new System.Drawing.Point(169, 18);
            this.btnApprSBNach.Name = "btnApprSBNach";
            this.btnApprSBNach.Size = new System.Drawing.Size(75, 23);
            this.btnApprSBNach.TabIndex = 20;
            this.btnApprSBNach.Tag = "ApprSBNach";
            this.btnApprSBNach.Text = "Нач. СБ";
            this.btnApprSBNach.UseVisualStyleBackColor = true;
            this.btnApprSBNach.Click += new System.EventHandler(this.btnApprField_Click);
            // 
            // btnApprNach
            // 
            this.btnApprNach.Location = new System.Drawing.Point(412, 18);
            this.btnApprNach.Name = "btnApprNach";
            this.btnApprNach.Size = new System.Drawing.Size(75, 23);
            this.btnApprNach.TabIndex = 23;
            this.btnApprNach.Tag = "ApprNach";
            this.btnApprNach.Text = "Нач.торг.";
            this.btnApprNach.UseVisualStyleBackColor = true;
            this.btnApprNach.Click += new System.EventHandler(this.btnApprField_Click);
            // 
            // btnApprKasa
            // 
            this.btnApprKasa.Location = new System.Drawing.Point(331, 18);
            this.btnApprKasa.Name = "btnApprKasa";
            this.btnApprKasa.Size = new System.Drawing.Size(75, 23);
            this.btnApprKasa.TabIndex = 22;
            this.btnApprKasa.Tag = "ApprKasa";
            this.btnApprKasa.Text = "Ст. касир";
            this.btnApprKasa.UseVisualStyleBackColor = true;
            this.btnApprKasa.Click += new System.EventHandler(this.btnApprField_Click);
            // 
            // btnApprASU
            // 
            this.btnApprASU.Location = new System.Drawing.Point(412, 47);
            this.btnApprASU.Name = "btnApprASU";
            this.btnApprASU.Size = new System.Drawing.Size(75, 23);
            this.btnApprASU.TabIndex = 29;
            this.btnApprASU.Tag = "ApprASU";
            this.btnApprASU.Text = "АСУ";
            this.btnApprASU.UseVisualStyleBackColor = true;
            this.btnApprASU.Click += new System.EventHandler(this.btnApprField_Click);
            // 
            // btnApprBuh
            // 
            this.btnApprBuh.Location = new System.Drawing.Point(331, 47);
            this.btnApprBuh.Name = "btnApprBuh";
            this.btnApprBuh.Size = new System.Drawing.Size(75, 23);
            this.btnApprBuh.TabIndex = 28;
            this.btnApprBuh.Tag = "ApprBuh";
            this.btnApprBuh.Text = "Бухгалтер";
            this.btnApprBuh.UseVisualStyleBackColor = true;
            this.btnApprBuh.Click += new System.EventHandler(this.btnApprField_Click);
            // 
            // btnApprSklad
            // 
            this.btnApprSklad.Location = new System.Drawing.Point(250, 47);
            this.btnApprSklad.Name = "btnApprSklad";
            this.btnApprSklad.Size = new System.Drawing.Size(75, 23);
            this.btnApprSklad.TabIndex = 27;
            this.btnApprSklad.Tag = "ApprSklad";
            this.btnApprSklad.Text = "Склад";
            this.btnApprSklad.UseVisualStyleBackColor = true;
            this.btnApprSklad.Click += new System.EventHandler(this.btnApprField_Click);
            // 
            // btnApprEnerg
            // 
            this.btnApprEnerg.Location = new System.Drawing.Point(169, 47);
            this.btnApprEnerg.Name = "btnApprEnerg";
            this.btnApprEnerg.Size = new System.Drawing.Size(75, 23);
            this.btnApprEnerg.TabIndex = 26;
            this.btnApprEnerg.Tag = "ApprEnerg";
            this.btnApprEnerg.Text = "Енергетик";
            this.btnApprEnerg.UseVisualStyleBackColor = true;
            this.btnApprEnerg.Click += new System.EventHandler(this.btnApprField_Click);
            // 
            // btnApprDostavka
            // 
            this.btnApprDostavka.Location = new System.Drawing.Point(88, 47);
            this.btnApprDostavka.Name = "btnApprDostavka";
            this.btnApprDostavka.Size = new System.Drawing.Size(75, 23);
            this.btnApprDostavka.TabIndex = 25;
            this.btnApprDostavka.Tag = "ApprDostavka";
            this.btnApprDostavka.Text = "Доставка";
            this.btnApprDostavka.UseVisualStyleBackColor = true;
            this.btnApprDostavka.Click += new System.EventHandler(this.btnApprField_Click);
            // 
            // btnApprFin
            // 
            this.btnApprFin.Location = new System.Drawing.Point(7, 47);
            this.btnApprFin.Name = "btnApprFin";
            this.btnApprFin.Size = new System.Drawing.Size(75, 23);
            this.btnApprFin.TabIndex = 24;
            this.btnApprFin.Tag = "ApprFin";
            this.btnApprFin.Text = "Фінансист";
            this.btnApprFin.UseVisualStyleBackColor = true;
            this.btnApprFin.Click += new System.EventHandler(this.btnApprField_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(177, 123);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 30;
            this.btnPrint.Text = "Друк";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(258, 123);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(75, 23);
            this.btnHelp.TabIndex = 31;
            this.btnHelp.Text = "Довідка";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // grpApprove
            // 
            this.grpApprove.Controls.Add(this.btnEditApprovers);
            this.grpApprove.Controls.Add(this.btnApprASU);
            this.grpApprove.Controls.Add(this.btnApprBuh);
            this.grpApprove.Controls.Add(this.btnApprSklad);
            this.grpApprove.Controls.Add(this.btnApprEnerg);
            this.grpApprove.Controls.Add(this.btnApprDostavka);
            this.grpApprove.Controls.Add(this.btnApprFin);
            this.grpApprove.Controls.Add(this.btnApprNach);
            this.grpApprove.Controls.Add(this.btnApprKasa);
            this.grpApprove.Controls.Add(this.btnApprSB);
            this.grpApprove.Controls.Add(this.btnApprSBNach);
            this.grpApprove.Controls.Add(this.btnApprComdir);
            this.grpApprove.Controls.Add(this.btnApprDir);
            this.grpApprove.Location = new System.Drawing.Point(8, 39);
            this.grpApprove.Name = "grpApprove";
            this.grpApprove.Size = new System.Drawing.Size(493, 78);
            this.grpApprove.TabIndex = 32;
            this.grpApprove.TabStop = false;
            this.grpApprove.Text = "Затвердження";
            // 
            // btnApprAvtor
            // 
            this.btnApprAvtor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApprAvtor.Location = new System.Drawing.Point(619, 496);
            this.btnApprAvtor.Name = "btnApprAvtor";
            this.btnApprAvtor.Size = new System.Drawing.Size(75, 23);
            this.btnApprAvtor.TabIndex = 30;
            this.btnApprAvtor.Tag = "ApprAvtor";
            this.btnApprAvtor.Text = "Затвердити";
            this.btnApprAvtor.UseVisualStyleBackColor = true;
            this.btnApprAvtor.Click += new System.EventHandler(this.btnApprField_Click);
            // 
            // labelApprAll
            // 
            this.labelApprAll.AutoSize = true;
            this.labelApprAll.BackColor = System.Drawing.Color.Lime;
            this.labelApprAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.labelApprAll.Location = new System.Drawing.Point(582, 152);
            this.labelApprAll.Name = "labelApprAll";
            this.labelApprAll.Size = new System.Drawing.Size(156, 24);
            this.labelApprAll.TabIndex = 33;
            this.labelApprAll.Text = "ЗАТВЕРДЖЕНО";
            this.labelApprAll.Visible = false;
            // 
            // sendPrint
            // 
            this.sendPrint.UseEXDialog = true;
            // 
            // btnEditApprovers
            // 
            this.btnEditApprovers.Image = global::FlyDoc.Properties.Resources._3dot;
            this.btnEditApprovers.Location = new System.Drawing.Point(466, -1);
            this.btnEditApprovers.Name = "btnEditApprovers";
            this.btnEditApprovers.Size = new System.Drawing.Size(27, 17);
            this.btnEditApprovers.TabIndex = 30;
            this.btnEditApprovers.UseVisualStyleBackColor = true;
            this.btnEditApprovers.Click += new System.EventHandler(this.btnEditApprovers_Click);
            // 
            // NewNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 525);
            this.Controls.Add(this.labelApprAll);
            this.Controls.Add(this.tbBodyDown);
            this.Controls.Add(this.btnApprAvtor);
            this.Controls.Add(this.grpApprove);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.tbAvtor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbBodyUp);
            this.Controls.Add(this.tbHeadNach);
            this.Controls.Add(this.tbHeadDir);
            this.Controls.Add(this.dgvTable);
            this.Controls.Add(this.dtpDateCreate);
            this.Controls.Add(this.cbNoteTemplate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSaveToDB);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cbDepartment);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbNumber);
            this.Name = "NewNote";
            this.Text = "Добавити новий рядок";
            this.Load += new System.EventHandler(this.NewNote_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).EndInit();
            this.grpApprove.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbDepartment;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSaveToDB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbNoteTemplate;
        private System.Windows.Forms.DateTimePicker dtpDateCreate;
        private System.Windows.Forms.DataGridView dgvTable;
        private System.Windows.Forms.TextBox tbHeadDir;
        private System.Windows.Forms.TextBox tbHeadNach;
        private System.Windows.Forms.TextBox tbBodyUp;
        private System.Windows.Forms.TextBox tbBodyDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbAvtor;
        private System.Windows.Forms.Button btnApprDir;
        private System.Windows.Forms.Button btnApprComdir;
        private System.Windows.Forms.Button btnApprSB;
        private System.Windows.Forms.Button btnApprSBNach;
        private System.Windows.Forms.Button btnApprNach;
        private System.Windows.Forms.Button btnApprKasa;
        private System.Windows.Forms.Button btnApprASU;
        private System.Windows.Forms.Button btnApprBuh;
        private System.Windows.Forms.Button btnApprSklad;
        private System.Windows.Forms.Button btnApprEnerg;
        private System.Windows.Forms.Button btnApprDostavka;
        private System.Windows.Forms.Button btnApprFin;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.GroupBox grpApprove;
        private System.Windows.Forms.Button btnApprAvtor;
        private System.Windows.Forms.Label labelApprAll;
        private System.Windows.Forms.PrintDialog sendPrint;
        private System.Windows.Forms.Button btnEditApprovers;
    }
}