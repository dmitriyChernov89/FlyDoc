namespace FlyDoc.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btexit = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btphone = new System.Windows.Forms.Button();
            this.btwork = new System.Windows.Forms.Button();
            this.btschedule = new System.Windows.Forms.Button();
            this.btnotes = new System.Windows.Forms.Button();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tpgNotes = new System.Windows.Forms.TabPage();
            this.dgvNotes = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmiGridCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiGridCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiGridEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiGridDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.panelFindNotes = new System.Windows.Forms.Panel();
            this.tpgSchedule = new System.Windows.Forms.TabPage();
            this.dgvSchedule = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tpgPhoneBook = new System.Windows.Forms.TabPage();
            this.dgvPhonebook = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tpgConfig = new System.Windows.Forms.TabPage();
            this.tabControlConfig = new System.Windows.Forms.TabControl();
            this.tpgUsers = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btnFindPC = new System.Windows.Forms.Button();
            this.tbxFindPC = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.tpgNoteTemplates = new System.Windows.Forms.TabPage();
            this.dgvNoteTemplates = new System.Windows.Forms.DataGridView();
            this.tpgDepartments = new System.Windows.Forms.TabPage();
            this.dgvDepartments = new System.Windows.Forms.DataGridView();
            this.btnFindDocByNumber = new System.Windows.Forms.Button();
            this.tbxFindDocNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbDepartmentFilter = new System.Windows.Forms.ComboBox();
            this.chkCEO = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDeleteDepartmentFilter = new System.Windows.Forms.Button();
            this.panelControl = new System.Windows.Forms.Panel();
            this.btconfig = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnNew = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.datePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.datePickerStart = new System.Windows.Forms.DateTimePicker();
            this.chkCFO = new System.Windows.Forms.CheckBox();
            this.refreshDBTimer = new System.Windows.Forms.Timer(this.components);
            this.tabControlMain.SuspendLayout();
            this.tpgNotes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNotes)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.tpgSchedule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedule)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tpgPhoneBook.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhonebook)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tpgConfig.SuspendLayout();
            this.tabControlConfig.SuspendLayout();
            this.tpgUsers.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.tpgNoteTemplates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNoteTemplates)).BeginInit();
            this.tpgDepartments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartments)).BeginInit();
            this.panelControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // btexit
            // 
            resources.ApplyResources(this.btexit, "btexit");
            this.btexit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btexit.ImageList = this.imageList1;
            this.btexit.Name = "btexit";
            this.btexit.UseVisualStyleBackColor = true;
            this.btexit.Click += new System.EventHandler(this.btexit_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Settings-50.png");
            this.imageList1.Images.SetKeyName(1, "Add Row-64.png");
            this.imageList1.Images.SetKeyName(2, "Administrative_Tools-50.ico");
            this.imageList1.Images.SetKeyName(3, "Calendar-50_1_ (1).ico");
            this.imageList1.Images.SetKeyName(4, "Document-50.ico");
            this.imageList1.Images.SetKeyName(5, "Exit_Sign-50.ico");
            this.imageList1.Images.SetKeyName(6, "Office_Phone-50.ico");
            this.imageList1.Images.SetKeyName(7, "Overtime-50.ico");
            this.imageList1.Images.SetKeyName(8, "PDF-50.ico");
            this.imageList1.Images.SetKeyName(9, "Print-50.ico");
            // 
            // btphone
            // 
            resources.ApplyResources(this.btphone, "btphone");
            this.btphone.ImageList = this.imageList1;
            this.btphone.Name = "btphone";
            this.btphone.UseVisualStyleBackColor = true;
            this.btphone.Click += new System.EventHandler(this.btphone_Click);
            // 
            // btwork
            // 
            resources.ApplyResources(this.btwork, "btwork");
            this.btwork.ImageList = this.imageList1;
            this.btwork.Name = "btwork";
            this.btwork.UseVisualStyleBackColor = true;
            this.btwork.Click += new System.EventHandler(this.btwork_Click);
            // 
            // btschedule
            // 
            resources.ApplyResources(this.btschedule, "btschedule");
            this.btschedule.ImageList = this.imageList1;
            this.btschedule.Name = "btschedule";
            this.btschedule.UseVisualStyleBackColor = true;
            this.btschedule.Click += new System.EventHandler(this.btschedule_Click);
            // 
            // btnotes
            // 
            resources.ApplyResources(this.btnotes, "btnotes");
            this.btnotes.ImageList = this.imageList1;
            this.btnotes.Name = "btnotes";
            this.btnotes.UseVisualStyleBackColor = true;
            this.btnotes.Click += new System.EventHandler(this.btnotes_Click);
            // 
            // tabControlMain
            // 
            resources.ApplyResources(this.tabControlMain, "tabControlMain");
            this.tabControlMain.Controls.Add(this.tpgNotes);
            this.tabControlMain.Controls.Add(this.tpgSchedule);
            this.tabControlMain.Controls.Add(this.tpgPhoneBook);
            this.tabControlMain.Controls.Add(this.tpgConfig);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.SelectedIndexChanged += new System.EventHandler(this.tabControlMain_SelectedIndexChanged);
            // 
            // tpgNotes
            // 
            this.tpgNotes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tpgNotes.Controls.Add(this.dgvNotes);
            this.tpgNotes.Controls.Add(this.panelFindNotes);
            resources.ApplyResources(this.tpgNotes, "tpgNotes");
            this.tpgNotes.Name = "tpgNotes";
            this.tpgNotes.UseVisualStyleBackColor = true;
            // 
            // dgvNotes
            // 
            this.dgvNotes.AllowUserToAddRows = false;
            this.dgvNotes.AllowUserToDeleteRows = false;
            this.dgvNotes.AllowUserToOrderColumns = true;
            this.dgvNotes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNotes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNotes.ContextMenuStrip = this.contextMenuStrip1;
            resources.ApplyResources(this.dgvNotes, "dgvNotes");
            this.dgvNotes.MultiSelect = false;
            this.dgvNotes.Name = "dgvNotes";
            this.dgvNotes.ReadOnly = true;
            this.dgvNotes.RowHeadersVisible = false;
            this.dgvNotes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNotes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNotes_CellContentClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmiGridCreate,
            this.cmiGridCopy,
            this.cmiGridEdit,
            this.cmiGridDelete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // cmiGridCreate
            // 
            this.cmiGridCreate.Image = global::FlyDoc.Properties.Resources.Add_Row_64;
            this.cmiGridCreate.Name = "cmiGridCreate";
            resources.ApplyResources(this.cmiGridCreate, "cmiGridCreate");
            this.cmiGridCreate.Click += new System.EventHandler(this.cmiGridCreate_Click);
            // 
            // cmiGridCopy
            // 
            this.cmiGridCopy.Image = global::FlyDoc.Properties.Resources.Replicate_Rows_48;
            this.cmiGridCopy.Name = "cmiGridCopy";
            resources.ApplyResources(this.cmiGridCopy, "cmiGridCopy");
            this.cmiGridCopy.Click += new System.EventHandler(this.cmiGridCopy_Click);
            // 
            // cmiGridEdit
            // 
            this.cmiGridEdit.Image = global::FlyDoc.Properties.Resources.Edit_Row_64;
            this.cmiGridEdit.Name = "cmiGridEdit";
            resources.ApplyResources(this.cmiGridEdit, "cmiGridEdit");
            this.cmiGridEdit.Click += new System.EventHandler(this.cmiGridEdit_Click);
            // 
            // cmiGridDelete
            // 
            this.cmiGridDelete.Image = global::FlyDoc.Properties.Resources.Delete_Row_64;
            this.cmiGridDelete.Name = "cmiGridDelete";
            resources.ApplyResources(this.cmiGridDelete, "cmiGridDelete");
            this.cmiGridDelete.Click += new System.EventHandler(this.cmiGridDelete_Click);
            // 
            // panelFindNotes
            // 
            this.panelFindNotes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.panelFindNotes, "panelFindNotes");
            this.panelFindNotes.Name = "panelFindNotes";
            // 
            // tpgSchedule
            // 
            this.tpgSchedule.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tpgSchedule.Controls.Add(this.dgvSchedule);
            this.tpgSchedule.Controls.Add(this.panel1);
            resources.ApplyResources(this.tpgSchedule, "tpgSchedule");
            this.tpgSchedule.Name = "tpgSchedule";
            this.tpgSchedule.UseVisualStyleBackColor = true;
            // 
            // dgvSchedule
            // 
            this.dgvSchedule.AllowUserToAddRows = false;
            this.dgvSchedule.AllowUserToDeleteRows = false;
            this.dgvSchedule.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSchedule.ContextMenuStrip = this.contextMenuStrip1;
            resources.ApplyResources(this.dgvSchedule, "dgvSchedule");
            this.dgvSchedule.MultiSelect = false;
            this.dgvSchedule.Name = "dgvSchedule";
            this.dgvSchedule.ReadOnly = true;
            this.dgvSchedule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.groupBox4);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.textBox2);
            this.groupBox4.Controls.Add(this.label3);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            resources.ApplyResources(this.textBox2, "textBox2");
            this.textBox2.Name = "textBox2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // tpgPhoneBook
            // 
            this.tpgPhoneBook.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tpgPhoneBook.Controls.Add(this.dgvPhonebook);
            this.tpgPhoneBook.Controls.Add(this.panel2);
            resources.ApplyResources(this.tpgPhoneBook, "tpgPhoneBook");
            this.tpgPhoneBook.Name = "tpgPhoneBook";
            this.tpgPhoneBook.UseVisualStyleBackColor = true;
            // 
            // dgvPhonebook
            // 
            this.dgvPhonebook.AllowUserToAddRows = false;
            this.dgvPhonebook.AllowUserToDeleteRows = false;
            this.dgvPhonebook.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhonebook.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgvPhonebook, "dgvPhonebook");
            this.dgvPhonebook.MultiSelect = false;
            this.dgvPhonebook.Name = "dgvPhonebook";
            this.dgvPhonebook.ReadOnly = true;
            this.dgvPhonebook.RowHeadersVisible = false;
            this.dgvPhonebook.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.groupBox6);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.button4);
            this.groupBox6.Controls.Add(this.textBox3);
            this.groupBox6.Controls.Add(this.label5);
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            // 
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            resources.ApplyResources(this.textBox3, "textBox3");
            this.textBox3.Name = "textBox3";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // tpgConfig
            // 
            this.tpgConfig.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tpgConfig.Controls.Add(this.tabControlConfig);
            resources.ApplyResources(this.tpgConfig, "tpgConfig");
            this.tpgConfig.Name = "tpgConfig";
            this.tpgConfig.UseVisualStyleBackColor = true;
            // 
            // tabControlConfig
            // 
            this.tabControlConfig.Controls.Add(this.tpgUsers);
            this.tabControlConfig.Controls.Add(this.tpgNoteTemplates);
            this.tabControlConfig.Controls.Add(this.tpgDepartments);
            resources.ApplyResources(this.tabControlConfig, "tabControlConfig");
            this.tabControlConfig.Name = "tabControlConfig";
            this.tabControlConfig.SelectedIndex = 0;
            this.tabControlConfig.SelectedIndexChanged += new System.EventHandler(this.tabControlConfig_SelectedIndexChanged);
            // 
            // tpgUsers
            // 
            this.tpgUsers.Controls.Add(this.panel3);
            this.tpgUsers.Controls.Add(this.dgvUsers);
            resources.ApplyResources(this.tpgUsers, "tpgUsers");
            this.tpgUsers.Name = "tpgUsers";
            this.tpgUsers.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.groupBox8);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btnFindPC);
            this.groupBox8.Controls.Add(this.tbxFindPC);
            this.groupBox8.Controls.Add(this.label7);
            resources.ApplyResources(this.groupBox8, "groupBox8");
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.TabStop = false;
            // 
            // btnFindPC
            // 
            resources.ApplyResources(this.btnFindPC, "btnFindPC");
            this.btnFindPC.Name = "btnFindPC";
            this.btnFindPC.UseVisualStyleBackColor = true;
            this.btnFindPC.Click += new System.EventHandler(this.btnFindPC_Click);
            // 
            // tbxFindPC
            // 
            resources.ApplyResources(this.tbxFindPC, "tbxFindPC");
            this.tbxFindPC.Name = "tbxFindPC";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // dgvUsers
            // 
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.dgvUsers, "dgvUsers");
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
            this.dgvUsers.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvUsers.MultiSelect = false;
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // tpgNoteTemplates
            // 
            this.tpgNoteTemplates.Controls.Add(this.dgvNoteTemplates);
            resources.ApplyResources(this.tpgNoteTemplates, "tpgNoteTemplates");
            this.tpgNoteTemplates.Name = "tpgNoteTemplates";
            this.tpgNoteTemplates.UseVisualStyleBackColor = true;
            // 
            // dgvNoteTemplates
            // 
            this.dgvNoteTemplates.AllowUserToAddRows = false;
            this.dgvNoteTemplates.AllowUserToDeleteRows = false;
            this.dgvNoteTemplates.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNoteTemplates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNoteTemplates.ContextMenuStrip = this.contextMenuStrip1;
            resources.ApplyResources(this.dgvNoteTemplates, "dgvNoteTemplates");
            this.dgvNoteTemplates.MultiSelect = false;
            this.dgvNoteTemplates.Name = "dgvNoteTemplates";
            this.dgvNoteTemplates.ReadOnly = true;
            this.dgvNoteTemplates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // tpgDepartments
            // 
            this.tpgDepartments.Controls.Add(this.dgvDepartments);
            resources.ApplyResources(this.tpgDepartments, "tpgDepartments");
            this.tpgDepartments.Name = "tpgDepartments";
            this.tpgDepartments.UseVisualStyleBackColor = true;
            // 
            // dgvDepartments
            // 
            this.dgvDepartments.AllowUserToAddRows = false;
            this.dgvDepartments.AllowUserToDeleteRows = false;
            this.dgvDepartments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDepartments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDepartments.ContextMenuStrip = this.contextMenuStrip1;
            resources.ApplyResources(this.dgvDepartments, "dgvDepartments");
            this.dgvDepartments.MultiSelect = false;
            this.dgvDepartments.Name = "dgvDepartments";
            this.dgvDepartments.ReadOnly = true;
            this.dgvDepartments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // btnFindDocByNumber
            // 
            resources.ApplyResources(this.btnFindDocByNumber, "btnFindDocByNumber");
            this.btnFindDocByNumber.Name = "btnFindDocByNumber";
            this.btnFindDocByNumber.UseVisualStyleBackColor = true;
            this.btnFindDocByNumber.Click += new System.EventHandler(this.btnFindDocByNumber_Click);
            // 
            // tbxFindDocNumber
            // 
            resources.ApplyResources(this.tbxFindDocNumber, "tbxFindDocNumber");
            this.tbxFindDocNumber.Name = "tbxFindDocNumber";
            this.tbxFindDocNumber.TextChanged += new System.EventHandler(this.tbxFindDocNumber_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cbDepartmentFilter
            // 
            this.cbDepartmentFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDepartmentFilter.FormattingEnabled = true;
            resources.ApplyResources(this.cbDepartmentFilter, "cbDepartmentFilter");
            this.cbDepartmentFilter.Name = "cbDepartmentFilter";
            // 
            // chkCEO
            // 
            resources.ApplyResources(this.chkCEO, "chkCEO");
            this.chkCEO.Checked = true;
            this.chkCEO.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkCEO.Name = "chkCEO";
            this.chkCEO.ThreeState = true;
            this.chkCEO.UseVisualStyleBackColor = true;
            this.chkCEO.CheckedChanged += new System.EventHandler(this.chkCEO_CheckedChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // btnDeleteDepartmentFilter
            // 
            resources.ApplyResources(this.btnDeleteDepartmentFilter, "btnDeleteDepartmentFilter");
            this.btnDeleteDepartmentFilter.Name = "btnDeleteDepartmentFilter";
            this.btnDeleteDepartmentFilter.UseVisualStyleBackColor = true;
            this.btnDeleteDepartmentFilter.Click += new System.EventHandler(this.btnDeleteDepartmentFilter_Click);
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.btnotes);
            this.panelControl.Controls.Add(this.btschedule);
            this.panelControl.Controls.Add(this.btexit);
            this.panelControl.Controls.Add(this.btconfig);
            this.panelControl.Controls.Add(this.btwork);
            this.panelControl.Controls.Add(this.btphone);
            resources.ApplyResources(this.panelControl, "panelControl");
            this.panelControl.Name = "panelControl";
            this.panelControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelControl_MouseMove);
            // 
            // btconfig
            // 
            resources.ApplyResources(this.btconfig, "btconfig");
            this.btconfig.ImageList = this.imageList1;
            this.btconfig.Name = "btconfig";
            this.btconfig.UseVisualStyleBackColor = true;
            this.btconfig.Click += new System.EventHandler(this.btconfig_Click);
            // 
            // btnNew
            // 
            resources.ApplyResources(this.btnNew, "btnNew");
            this.btnNew.Name = "btnNew";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnCopy
            // 
            resources.ApplyResources(this.btnCopy, "btnCopy");
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnEdit
            // 
            resources.ApplyResources(this.btnEdit, "btnEdit");
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // datePickerEnd
            // 
            resources.ApplyResources(this.datePickerEnd, "datePickerEnd");
            this.datePickerEnd.MaxDate = new System.DateTime(2030, 12, 31, 0, 0, 0, 0);
            this.datePickerEnd.MinDate = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
            this.datePickerEnd.Name = "datePickerEnd";
            this.datePickerEnd.Value = new System.DateTime(2017, 4, 26, 0, 0, 0, 0);
            // 
            // datePickerStart
            // 
            resources.ApplyResources(this.datePickerStart, "datePickerStart");
            this.datePickerStart.MaxDate = new System.DateTime(2030, 12, 31, 0, 0, 0, 0);
            this.datePickerStart.MinDate = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
            this.datePickerStart.Name = "datePickerStart";
            this.datePickerStart.Value = new System.DateTime(2017, 4, 26, 0, 0, 0, 0);
            this.datePickerStart.ValueChanged += new System.EventHandler(this.datePickerStart_ValueChanged);
            // 
            // chkCFO
            // 
            resources.ApplyResources(this.chkCFO, "chkCFO");
            this.chkCFO.Checked = true;
            this.chkCFO.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkCFO.Name = "chkCFO";
            this.chkCFO.ThreeState = true;
            this.chkCFO.UseVisualStyleBackColor = true;
            // 
            // refreshDBTimer
            // 
            //this.refreshDBTimer.Interval = 5000;
            //this.refreshDBTimer.Tick += new System.EventHandler(this.refreshDBTimer_Tick);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.chkCFO);
            this.Controls.Add(this.btnFindDocByNumber);
            this.Controls.Add(this.tbxFindDocNumber);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbDepartmentFilter);
            this.Controls.Add(this.chkCEO);
            this.Controls.Add(this.btnDeleteDepartmentFilter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.datePickerEnd);
            this.Controls.Add(this.datePickerStart);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.panelControl);
            this.Controls.Add(this.tabControlMain);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.FlyDoc_Load);
            this.tabControlMain.ResumeLayout(false);
            this.tpgNotes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNotes)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tpgSchedule.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedule)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tpgPhoneBook.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhonebook)).EndInit();
            this.panel2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.tpgConfig.ResumeLayout(false);
            this.tabControlConfig.ResumeLayout(false);
            this.tpgUsers.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.tpgNoteTemplates.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNoteTemplates)).EndInit();
            this.tpgDepartments.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartments)).EndInit();
            this.panelControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btexit;
        private System.Windows.Forms.Button btphone;
        private System.Windows.Forms.Button btwork;
        private System.Windows.Forms.Button btschedule;
        private System.Windows.Forms.Button btnotes;
        private System.Windows.Forms.Button btconfig;
        private System.Windows.Forms.ImageList imageList1;
        public System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tpgNotes;
        private System.Windows.Forms.TabPage tpgSchedule;
        private System.Windows.Forms.Panel panelControl;
        private System.Windows.Forms.TabPage tpgPhoneBook;
        private System.Windows.Forms.TabPage tpgConfig;
        public System.Windows.Forms.Panel panelFindNotes;
        public System.Windows.Forms.Button btnFindDocByNumber;
        public System.Windows.Forms.TextBox tbxFindDocNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button btnDeleteDepartmentFilter;
        public System.Windows.Forms.CheckBox chkCEO;
        public System.Windows.Forms.DataGridView dgvNotes;
        public System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.DataGridView dgvSchedule;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.DataGridView dgvPhonebook;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.ComboBox cbDepartmentFilter;
        private System.Windows.Forms.TabControl tabControlConfig;
        private System.Windows.Forms.TabPage tpgUsers;
        private System.Windows.Forms.TabPage tpgNoteTemplates;
        private System.Windows.Forms.DataGridView dgvNoteTemplates;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button btnFindPC;
        private System.Windows.Forms.TextBox tbxFindPC;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.TabPage tpgDepartments;
        private System.Windows.Forms.DataGridView dgvDepartments;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cmiGridCreate;
        private System.Windows.Forms.ToolStripMenuItem cmiGridCopy;
        private System.Windows.Forms.ToolStripMenuItem cmiGridEdit;
        private System.Windows.Forms.ToolStripMenuItem cmiGridDelete;
        public System.Windows.Forms.Button btnNew;
        public System.Windows.Forms.Button btnCopy;
        public System.Windows.Forms.Button btnEdit;
        public System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.DateTimePicker datePickerEnd;
        public System.Windows.Forms.DateTimePicker datePickerStart;
        public System.Windows.Forms.CheckBox chkCFO;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button button4;
        public System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer refreshDBTimer;
    }
}

