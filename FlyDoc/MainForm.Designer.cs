namespace FlyDoc
{
    partial class FlyDoc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlyDoc));
            this.btexit = new System.Windows.Forms.Button();
            this.btphone = new System.Windows.Forms.Button();
            this.btwork = new System.Windows.Forms.Button();
            this.btschedule = new System.Windows.Forms.Button();
            this.btnotes = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.cbApproed = new System.Windows.Forms.CheckBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.datePickerStart = new System.Windows.Forms.DateTimePicker();
            this.datePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.cbDepartment = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btconfig = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btexit
            // 
            resources.ApplyResources(this.btexit, "btexit");
            this.btexit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btexit.Name = "btexit";
            this.btexit.UseVisualStyleBackColor = true;
            this.btexit.Click += new System.EventHandler(this.btexit_Click);
            // 
            // btphone
            // 
            this.btphone.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btphone, "btphone");
            this.btphone.Name = "btphone";
            this.btphone.UseVisualStyleBackColor = true;
            this.btphone.Click += new System.EventHandler(this.btphone_Click_1);
            // 
            // btwork
            // 
            this.btwork.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btwork, "btwork");
            this.btwork.Name = "btwork";
            this.btwork.UseVisualStyleBackColor = true;
            this.btwork.Click += new System.EventHandler(this.btwork_Click);
            // 
            // btschedule
            // 
            this.btschedule.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btschedule, "btschedule");
            this.btschedule.Name = "btschedule";
            this.btschedule.UseVisualStyleBackColor = true;
            this.btschedule.Click += new System.EventHandler(this.btschedule_Click);
            // 
            // btnotes
            // 
            this.btnotes.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnotes, "btnotes");
            this.btnotes.Name = "btnotes";
            this.btnotes.UseVisualStyleBackColor = true;
            this.btnotes.Click += new System.EventHandler(this.btnotes_Click);
            // 
            // dataGridView1
            // 
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick_1);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Settings-50.png");
            // 
            // cbApproed
            // 
            resources.ApplyResources(this.cbApproed, "cbApproed");
            this.cbApproed.Name = "cbApproed";
            this.cbApproed.UseVisualStyleBackColor = true;
            this.cbApproed.CheckedChanged += new System.EventHandler(this.cbApproed_CheckedChanged);
            // 
            // btnNew
            // 
            resources.ApplyResources(this.btnNew, "btnNew");
            this.btnNew.Name = "btnNew";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnEdit
            // 
            resources.ApplyResources(this.btnEdit, "btnEdit");
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnView
            // 
            resources.ApplyResources(this.btnView, "btnView");
            this.btnView.Name = "btnView";
            this.btnView.UseVisualStyleBackColor = true;
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
            // datePickerEnd
            // 
            resources.ApplyResources(this.datePickerEnd, "datePickerEnd");
            this.datePickerEnd.MaxDate = new System.DateTime(2030, 12, 31, 0, 0, 0, 0);
            this.datePickerEnd.MinDate = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
            this.datePickerEnd.Name = "datePickerEnd";
            this.datePickerEnd.Value = new System.DateTime(2017, 4, 26, 0, 0, 0, 0);
            // 
            // cbDepartment
            // 
            this.cbDepartment.FormattingEnabled = true;
            resources.ApplyResources(this.cbDepartment, "cbDepartment");
            this.cbDepartment.Name = "cbDepartment";
            this.cbDepartment.SelectedIndexChanged += new System.EventHandler(this.cbDepartment_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // btconfig
            // 
            resources.ApplyResources(this.btconfig, "btconfig");
            this.btconfig.ImageList = this.imageList1;
            this.btconfig.Name = "btconfig";
            this.btconfig.UseVisualStyleBackColor = true;
            this.btconfig.Click += new System.EventHandler(this.btconfig_Click);
            // 
            // FlyDoc
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.CancelButton = this.btexit;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbDepartment);
            this.Controls.Add(this.datePickerEnd);
            this.Controls.Add(this.datePickerStart);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.cbApproed);
            this.Controls.Add(this.btconfig);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnotes);
            this.Controls.Add(this.btschedule);
            this.Controls.Add(this.btwork);
            this.Controls.Add(this.btphone);
            this.Controls.Add(this.btexit);
            this.Name = "FlyDoc";
            this.Load += new System.EventHandler(this.FlyDoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox cbApproed;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.DateTimePicker datePickerStart;
        private System.Windows.Forms.DateTimePicker datePickerEnd;
        private System.Windows.Forms.ComboBox cbDepartment;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

