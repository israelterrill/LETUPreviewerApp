namespace APIDataServer
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageActivities = new System.Windows.Forms.TabPage();
            this.btnSaveActivities = new System.Windows.Forms.Button();
            this.btnImportActivity = new System.Windows.Forms.Button();
            this.btnExportActivity = new System.Windows.Forms.Button();
            this.btnRemoveActivity = new System.Windows.Forms.Button();
            this.dgvActivities = new System.Windows.Forms.DataGridView();
            this.titleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.locationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imageLinkDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.activityBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabPageSchedules = new System.Windows.Forms.TabPage();
            this.btnExportSelectedSchedules = new System.Windows.Forms.Button();
            this.dgvEvents = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.eventBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnSaveSchedules = new System.Windows.Forms.Button();
            this.btnImportSchedule = new System.Windows.Forms.Button();
            this.btnExportSchedules = new System.Windows.Forms.Button();
            this.btnRemoveSchedule = new System.Windows.Forms.Button();
            this.dgvSchedules = new System.Windows.Forms.DataGridView();
            this.scheduleTitleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scheduleDatesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scheduleBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.activityBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.scheduleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabControl.SuspendLayout();
            this.tabPageActivities.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivities)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.activityBindingSource)).BeginInit();
            this.tabPageSchedules.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEvents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedules)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scheduleBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.activityBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scheduleBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageActivities);
            this.tabControl.Controls.Add(this.tabPageSchedules);
            this.tabControl.Location = new System.Drawing.Point(2, -2);
            this.tabControl.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(707, 436);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl.TabIndex = 0;
            // 
            // tabPageActivities
            // 
            this.tabPageActivities.Controls.Add(this.btnSaveActivities);
            this.tabPageActivities.Controls.Add(this.btnImportActivity);
            this.tabPageActivities.Controls.Add(this.btnExportActivity);
            this.tabPageActivities.Controls.Add(this.btnRemoveActivity);
            this.tabPageActivities.Controls.Add(this.dgvActivities);
            this.tabPageActivities.Location = new System.Drawing.Point(4, 22);
            this.tabPageActivities.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPageActivities.Name = "tabPageActivities";
            this.tabPageActivities.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPageActivities.Size = new System.Drawing.Size(699, 410);
            this.tabPageActivities.TabIndex = 0;
            this.tabPageActivities.Text = "Activities";
            this.tabPageActivities.UseVisualStyleBackColor = true;
            // 
            // btnSaveActivities
            // 
            this.btnSaveActivities.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSaveActivities.AutoSize = true;
            this.btnSaveActivities.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveActivities.Location = new System.Drawing.Point(445, 363);
            this.btnSaveActivities.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSaveActivities.Name = "btnSaveActivities";
            this.btnSaveActivities.Size = new System.Drawing.Size(93, 41);
            this.btnSaveActivities.TabIndex = 23;
            this.btnSaveActivities.Text = "Save";
            this.btnSaveActivities.UseVisualStyleBackColor = true;
            // 
            // btnImportActivity
            // 
            this.btnImportActivity.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnImportActivity.AutoSize = true;
            this.btnImportActivity.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportActivity.Location = new System.Drawing.Point(337, 363);
            this.btnImportActivity.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnImportActivity.Name = "btnImportActivity";
            this.btnImportActivity.Size = new System.Drawing.Size(93, 41);
            this.btnImportActivity.TabIndex = 22;
            this.btnImportActivity.Text = "Import";
            this.btnImportActivity.UseVisualStyleBackColor = true;
            this.btnImportActivity.Click += new System.EventHandler(this.btnImportActivity_Click);
            // 
            // btnExportActivity
            // 
            this.btnExportActivity.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnExportActivity.AutoSize = true;
            this.btnExportActivity.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportActivity.Location = new System.Drawing.Point(225, 363);
            this.btnExportActivity.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnExportActivity.Name = "btnExportActivity";
            this.btnExportActivity.Size = new System.Drawing.Size(93, 41);
            this.btnExportActivity.TabIndex = 21;
            this.btnExportActivity.Text = "Export";
            this.btnExportActivity.UseVisualStyleBackColor = true;
            this.btnExportActivity.Click += new System.EventHandler(this.btnExportActivity_Click);
            // 
            // btnRemoveActivity
            // 
            this.btnRemoveActivity.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRemoveActivity.AutoSize = true;
            this.btnRemoveActivity.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveActivity.Location = new System.Drawing.Point(114, 363);
            this.btnRemoveActivity.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnRemoveActivity.Name = "btnRemoveActivity";
            this.btnRemoveActivity.Size = new System.Drawing.Size(93, 41);
            this.btnRemoveActivity.TabIndex = 19;
            this.btnRemoveActivity.Text = "Remove";
            this.btnRemoveActivity.UseVisualStyleBackColor = true;
            this.btnRemoveActivity.Click += new System.EventHandler(this.btnRemoveActivity_Click);
            // 
            // dgvActivities
            // 
            this.dgvActivities.AllowUserToOrderColumns = true;
            this.dgvActivities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvActivities.AutoGenerateColumns = false;
            this.dgvActivities.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvActivities.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvActivities.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvActivities.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.titleDataGridViewTextBoxColumn,
            this.dateDataGridViewTextBoxColumn,
            this.locationDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn,
            this.imageLinkDataGridViewTextBoxColumn});
            this.dgvActivities.DataSource = this.activityBindingSource;
            this.dgvActivities.Location = new System.Drawing.Point(0, 0);
            this.dgvActivities.Name = "dgvActivities";
            this.dgvActivities.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvActivities.Size = new System.Drawing.Size(703, 357);
            this.dgvActivities.TabIndex = 4;
            // 
            // titleDataGridViewTextBoxColumn
            // 
            this.titleDataGridViewTextBoxColumn.DataPropertyName = "Title";
            this.titleDataGridViewTextBoxColumn.HeaderText = "Title";
            this.titleDataGridViewTextBoxColumn.Name = "titleDataGridViewTextBoxColumn";
            // 
            // dateDataGridViewTextBoxColumn
            // 
            this.dateDataGridViewTextBoxColumn.DataPropertyName = "Date";
            this.dateDataGridViewTextBoxColumn.HeaderText = "Date";
            this.dateDataGridViewTextBoxColumn.Name = "dateDataGridViewTextBoxColumn";
            // 
            // locationDataGridViewTextBoxColumn
            // 
            this.locationDataGridViewTextBoxColumn.DataPropertyName = "Location";
            this.locationDataGridViewTextBoxColumn.HeaderText = "Location";
            this.locationDataGridViewTextBoxColumn.Name = "locationDataGridViewTextBoxColumn";
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            // 
            // imageLinkDataGridViewTextBoxColumn
            // 
            this.imageLinkDataGridViewTextBoxColumn.DataPropertyName = "ImageLink";
            this.imageLinkDataGridViewTextBoxColumn.HeaderText = "ImageLink";
            this.imageLinkDataGridViewTextBoxColumn.Name = "imageLinkDataGridViewTextBoxColumn";
            // 
            // activityBindingSource
            // 
            this.activityBindingSource.DataSource = typeof(DataClasses.Activity);
            // 
            // tabPageSchedules
            // 
            this.tabPageSchedules.Controls.Add(this.btnExportSelectedSchedules);
            this.tabPageSchedules.Controls.Add(this.dgvEvents);
            this.tabPageSchedules.Controls.Add(this.btnSaveSchedules);
            this.tabPageSchedules.Controls.Add(this.btnImportSchedule);
            this.tabPageSchedules.Controls.Add(this.btnExportSchedules);
            this.tabPageSchedules.Controls.Add(this.btnRemoveSchedule);
            this.tabPageSchedules.Controls.Add(this.dgvSchedules);
            this.tabPageSchedules.Location = new System.Drawing.Point(4, 22);
            this.tabPageSchedules.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPageSchedules.Name = "tabPageSchedules";
            this.tabPageSchedules.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPageSchedules.Size = new System.Drawing.Size(699, 410);
            this.tabPageSchedules.TabIndex = 1;
            this.tabPageSchedules.Text = "Schedule";
            this.tabPageSchedules.UseVisualStyleBackColor = true;
            // 
            // btnExportSelectedSchedules
            // 
            this.btnExportSelectedSchedules.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnExportSelectedSchedules.AutoSize = true;
            this.btnExportSelectedSchedules.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportSelectedSchedules.Location = new System.Drawing.Point(274, 366);
            this.btnExportSelectedSchedules.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnExportSelectedSchedules.Name = "btnExportSelectedSchedules";
            this.btnExportSelectedSchedules.Size = new System.Drawing.Size(150, 41);
            this.btnExportSelectedSchedules.TabIndex = 30;
            this.btnExportSelectedSchedules.Text = "Export Selected";
            this.btnExportSelectedSchedules.UseVisualStyleBackColor = true;
            this.btnExportSelectedSchedules.Click += new System.EventHandler(this.btnExportSelectedSchedules_Click);
            // 
            // dgvEvents
            // 
            this.dgvEvents.AllowUserToOrderColumns = true;
            this.dgvEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvEvents.AutoGenerateColumns = false;
            this.dgvEvents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEvents.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEvents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5});
            this.dgvEvents.DataSource = this.eventBindingSource;
            this.dgvEvents.Location = new System.Drawing.Point(0, 167);
            this.dgvEvents.Name = "dgvEvents";
            this.dgvEvents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEvents.Size = new System.Drawing.Size(703, 184);
            this.dgvEvents.TabIndex = 29;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Title";
            this.dataGridViewTextBoxColumn2.HeaderText = "Title";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Date";
            this.dataGridViewTextBoxColumn3.HeaderText = "Date";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Location";
            this.dataGridViewTextBoxColumn4.HeaderText = "Location";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn5.HeaderText = "Description";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // eventBindingSource
            // 
            this.eventBindingSource.DataSource = typeof(DataClasses.Event);
            // 
            // btnSaveSchedules
            // 
            this.btnSaveSchedules.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSaveSchedules.AutoSize = true;
            this.btnSaveSchedules.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveSchedules.Location = new System.Drawing.Point(543, 366);
            this.btnSaveSchedules.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSaveSchedules.Name = "btnSaveSchedules";
            this.btnSaveSchedules.Size = new System.Drawing.Size(93, 41);
            this.btnSaveSchedules.TabIndex = 27;
            this.btnSaveSchedules.Text = "Save";
            this.btnSaveSchedules.UseVisualStyleBackColor = true;
            // 
            // btnImportSchedule
            // 
            this.btnImportSchedule.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnImportSchedule.AutoSize = true;
            this.btnImportSchedule.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportSchedule.Location = new System.Drawing.Point(437, 366);
            this.btnImportSchedule.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnImportSchedule.Name = "btnImportSchedule";
            this.btnImportSchedule.Size = new System.Drawing.Size(93, 41);
            this.btnImportSchedule.TabIndex = 26;
            this.btnImportSchedule.Text = "Import";
            this.btnImportSchedule.UseVisualStyleBackColor = true;
            // 
            // btnExportSchedules
            // 
            this.btnExportSchedules.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnExportSchedules.AutoSize = true;
            this.btnExportSchedules.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportSchedules.Location = new System.Drawing.Point(168, 366);
            this.btnExportSchedules.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnExportSchedules.Name = "btnExportSchedules";
            this.btnExportSchedules.Size = new System.Drawing.Size(93, 41);
            this.btnExportSchedules.TabIndex = 25;
            this.btnExportSchedules.Text = "Export";
            this.btnExportSchedules.UseVisualStyleBackColor = true;
            // 
            // btnRemoveSchedule
            // 
            this.btnRemoveSchedule.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRemoveSchedule.AutoSize = true;
            this.btnRemoveSchedule.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveSchedule.Location = new System.Drawing.Point(62, 366);
            this.btnRemoveSchedule.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnRemoveSchedule.Name = "btnRemoveSchedule";
            this.btnRemoveSchedule.Size = new System.Drawing.Size(93, 41);
            this.btnRemoveSchedule.TabIndex = 23;
            this.btnRemoveSchedule.Text = "Remove";
            this.btnRemoveSchedule.UseVisualStyleBackColor = true;
            this.btnRemoveSchedule.Click += new System.EventHandler(this.btnRemoveSchedule_Click);
            // 
            // dgvSchedules
            // 
            this.dgvSchedules.AllowUserToOrderColumns = true;
            this.dgvSchedules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSchedules.AutoGenerateColumns = false;
            this.dgvSchedules.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSchedules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSchedules.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.scheduleTitleDataGridViewTextBoxColumn,
            this.scheduleDatesDataGridViewTextBoxColumn});
            this.dgvSchedules.DataSource = this.scheduleBindingSource1;
            this.dgvSchedules.Location = new System.Drawing.Point(0, 0);
            this.dgvSchedules.Name = "dgvSchedules";
            this.dgvSchedules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSchedules.Size = new System.Drawing.Size(699, 161);
            this.dgvSchedules.TabIndex = 8;
            // 
            // scheduleTitleDataGridViewTextBoxColumn
            // 
            this.scheduleTitleDataGridViewTextBoxColumn.DataPropertyName = "ScheduleTitle";
            this.scheduleTitleDataGridViewTextBoxColumn.HeaderText = "ScheduleTitle";
            this.scheduleTitleDataGridViewTextBoxColumn.Name = "scheduleTitleDataGridViewTextBoxColumn";
            // 
            // scheduleDatesDataGridViewTextBoxColumn
            // 
            this.scheduleDatesDataGridViewTextBoxColumn.DataPropertyName = "ScheduleDates";
            this.scheduleDatesDataGridViewTextBoxColumn.HeaderText = "ScheduleDates";
            this.scheduleDatesDataGridViewTextBoxColumn.Name = "scheduleDatesDataGridViewTextBoxColumn";
            // 
            // scheduleBindingSource1
            // 
            this.scheduleBindingSource1.DataSource = typeof(DataClasses.Schedule);
            // 
            // activityBindingSource1
            // 
            this.activityBindingSource1.DataSource = typeof(DataClasses.Activity);
            // 
            // scheduleBindingSource
            // 
            this.scheduleBindingSource.DataSource = typeof(DataClasses.Schedule);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 467);
            this.Controls.Add(this.tabControl);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "MainForm";
            this.Text = "Preview Schedule Manager";
            this.tabControl.ResumeLayout(false);
            this.tabPageActivities.ResumeLayout(false);
            this.tabPageActivities.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivities)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.activityBindingSource)).EndInit();
            this.tabPageSchedules.ResumeLayout(false);
            this.tabPageSchedules.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEvents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedules)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scheduleBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.activityBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scheduleBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource activityBindingSource;
        private System.Windows.Forms.BindingSource activityBindingSource1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageActivities;
        private System.Windows.Forms.Button btnImportActivity;
        private System.Windows.Forms.Button btnExportActivity;
        private System.Windows.Forms.Button btnRemoveActivity;
        private System.Windows.Forms.DataGridView dgvActivities;
        private System.Windows.Forms.TabPage tabPageSchedules;
        private System.Windows.Forms.DataGridView dgvSchedules;
        private System.Windows.Forms.Button btnImportSchedule;
        private System.Windows.Forms.Button btnExportSchedules;
        private System.Windows.Forms.Button btnRemoveSchedule;
        private System.Windows.Forms.BindingSource scheduleBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn scheduleTitleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn scheduleDatesDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource scheduleBindingSource1;
        private System.Windows.Forms.Button btnSaveActivities;
        private System.Windows.Forms.Button btnSaveSchedules;
        private System.Windows.Forms.DataGridView dgvEvents;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.BindingSource eventBindingSource;
        private System.Windows.Forms.Button btnExportSelectedSchedules;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn locationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn imageLinkDataGridViewTextBoxColumn;
    }
}

