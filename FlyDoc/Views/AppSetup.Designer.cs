namespace FlyDoc.Views
{
    partial class AppSetup
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppSetup));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageUsers = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Department = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Notes = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Schedule = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Phone = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Config = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tabPageNoteTemplates = new System.Windows.Forms.TabPage();
            this.contextMenuStripUsers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.stripMenuItemDelUser = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPageUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.contextMenuStripUsers.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageUsers);
            this.tabControl1.Controls.Add(this.tabPageNoteTemplates);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(797, 356);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageUsers
            // 
            this.tabPageUsers.Controls.Add(this.button3);
            this.tabPageUsers.Controls.Add(this.button2);
            this.tabPageUsers.Controls.Add(this.btnDel);
            this.tabPageUsers.Controls.Add(this.dgvUsers);
            this.tabPageUsers.Location = new System.Drawing.Point(4, 22);
            this.tabPageUsers.Name = "tabPageUsers";
            this.tabPageUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageUsers.Size = new System.Drawing.Size(789, 330);
            this.tabPageUsers.TabIndex = 0;
            this.tabPageUsers.Text = "Користувачі";
            this.tabPageUsers.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(624, 299);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(504, 299);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnDel
            // 
            this.btnDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnDel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDel.ImageIndex = 1;
            this.btnDel.ImageList = this.imageList1;
            this.btnDel.Location = new System.Drawing.Point(9, 285);
            this.btnDel.Name = "btnDel";
            this.btnDel.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnDel.Size = new System.Drawing.Size(131, 37);
            this.btnDel.TabIndex = 1;
            this.btnDel.Text = "Видалити запис";
            this.btnDel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // dgvUsers
            // 
            this.dgvUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.PC,
            this.UserName,
            this.Department,
            this.Notes,
            this.Schedule,
            this.Phone,
            this.Config});
            this.dgvUsers.ContextMenuStrip = this.contextMenuStripUsers;
            this.dgvUsers.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvUsers.Location = new System.Drawing.Point(3, 3);
            this.dgvUsers.MultiSelect = false;
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.Size = new System.Drawing.Size(783, 150);
            this.dgvUsers.TabIndex = 0;
            this.dgvUsers.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvUsers_UserDeletingRow);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.FillWeight = 25F;
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // PC
            // 
            this.PC.DataPropertyName = "PC";
            this.PC.HeaderText = "PC";
            this.PC.Name = "PC";
            // 
            // UserName
            // 
            this.UserName.DataPropertyName = "UserName";
            this.UserName.HeaderText = "UserName";
            this.UserName.Name = "UserName";
            // 
            // Department
            // 
            this.Department.DataPropertyName = "Department";
            this.Department.HeaderText = "Department";
            this.Department.Name = "Department";
            // 
            // Notes
            // 
            this.Notes.DataPropertyName = "Notes";
            this.Notes.FillWeight = 70F;
            this.Notes.HeaderText = "Notes";
            this.Notes.Name = "Notes";
            // 
            // Schedule
            // 
            this.Schedule.DataPropertyName = "Schedule";
            this.Schedule.FillWeight = 70F;
            this.Schedule.HeaderText = "Schedule";
            this.Schedule.Name = "Schedule";
            // 
            // Phone
            // 
            this.Phone.DataPropertyName = "Phone";
            this.Phone.FillWeight = 70F;
            this.Phone.HeaderText = "Phone";
            this.Phone.Name = "Phone";
            // 
            // Config
            // 
            this.Config.DataPropertyName = "Config";
            this.Config.FillWeight = 70F;
            this.Config.HeaderText = "Gonfig";
            this.Config.Name = "Config";
            // 
            // tabPageNoteTemplates
            // 
            this.tabPageNoteTemplates.Location = new System.Drawing.Point(4, 22);
            this.tabPageNoteTemplates.Name = "tabPageNoteTemplates";
            this.tabPageNoteTemplates.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNoteTemplates.Size = new System.Drawing.Size(789, 330);
            this.tabPageNoteTemplates.TabIndex = 1;
            this.tabPageNoteTemplates.Text = "Шаблони сл.зап.";
            this.tabPageNoteTemplates.UseVisualStyleBackColor = true;
            // 
            // contextMenuStripUsers
            // 
            this.contextMenuStripUsers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripMenuItemDelUser});
            this.contextMenuStripUsers.Name = "contextMenuStripUsers";
            this.contextMenuStripUsers.Size = new System.Drawing.Size(185, 26);
            this.contextMenuStripUsers.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripUsers_Opening);
            // 
            // stripMenuItemDelUser
            // 
            this.stripMenuItemDelUser.Image = ((System.Drawing.Image)(resources.GetObject("stripMenuItemDelUser.Image")));
            this.stripMenuItemDelUser.Name = "stripMenuItemDelUser";
            this.stripMenuItemDelUser.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.stripMenuItemDelUser.Size = new System.Drawing.Size(184, 22);
            this.stripMenuItemDelUser.Text = "видалити рядок";
            this.stripMenuItemDelUser.Click += new System.EventHandler(this.stripMenuItemDelUser_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Data Recovery-50.png");
            this.imageList1.Images.SetKeyName(1, "Eraser-50.png");
            this.imageList1.Images.SetKeyName(2, "Esc-50.png");
            this.imageList1.Images.SetKeyName(3, "Settings-50.png");
            // 
            // AppSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 356);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AppSetup";
            this.Text = "Налаштування";
            this.Load += new System.EventHandler(this.AppSetup_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.contextMenuStripUsers.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageUsers;
        private System.Windows.Forms.TabPage tabPageNoteTemplates;
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn PC;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserName;
        private System.Windows.Forms.DataGridViewComboBoxColumn Department;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Notes;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Schedule;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Phone;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Config;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripUsers;
        private System.Windows.Forms.ToolStripMenuItem stripMenuItemDelUser;
        private System.Windows.Forms.ImageList imageList1;
    }
}