namespace WindowsFormsApplication1
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.textBox_code = new System.Windows.Forms.TextBox();
            this.dataGridView_commands = new System.Windows.Forms.DataGridView();
            this.dataGridView_result = new System.Windows.Forms.DataGridView();
            this.button_auto = new System.Windows.Forms.Button();
            this.button_next = new System.Windows.Forms.Button();
            this.button_find = new System.Windows.Forms.Button();
            this.label_commandDesc = new System.Windows.Forms.Label();
            this.label_command = new System.Windows.Forms.Label();
            this.textBox_commandDesc = new System.Windows.Forms.TextBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadBinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadHexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveBinFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveHexFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cSVFileNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultCSVToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.enableDatabaseEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableFileEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.listBox_commands = new System.Windows.Forms.ListBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.comboBox_printerType = new System.Windows.Forms.ComboBox();
            this.label_currentPosition = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_commands)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_result)).BeginInit();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_code
            // 
            this.textBox_code.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_code.HideSelection = false;
            this.textBox_code.Location = new System.Drawing.Point(0, 0);
            this.textBox_code.MaxLength = 32767000;
            this.textBox_code.Multiline = true;
            this.textBox_code.Name = "textBox_code";
            this.textBox_code.ReadOnly = true;
            this.textBox_code.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_code.Size = new System.Drawing.Size(237, 252);
            this.textBox_code.TabIndex = 0;
            this.textBox_code.Text = resources.GetString("textBox_code.Text");
            this.textBox_code.Leave += new System.EventHandler(this.TextBox_code_Leave);
            // 
            // dataGridView_commands
            // 
            this.dataGridView_commands.AllowUserToAddRows = false;
            this.dataGridView_commands.AllowUserToDeleteRows = false;
            this.dataGridView_commands.AllowUserToOrderColumns = true;
            this.dataGridView_commands.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_commands.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView_commands.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView_commands.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_commands.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_commands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_commands.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView_commands.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_commands.MultiSelect = false;
            this.dataGridView_commands.Name = "dataGridView_commands";
            this.dataGridView_commands.ReadOnly = true;
            this.dataGridView_commands.RowHeadersVisible = false;
            this.dataGridView_commands.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_commands.Size = new System.Drawing.Size(788, 285);
            this.dataGridView_commands.TabIndex = 2;
            // 
            // dataGridView_result
            // 
            this.dataGridView_result.AllowUserToAddRows = false;
            this.dataGridView_result.AllowUserToDeleteRows = false;
            this.dataGridView_result.AllowUserToOrderColumns = true;
            this.dataGridView_result.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_result.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView_result.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView_result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_result.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView_result.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_result.MultiSelect = false;
            this.dataGridView_result.Name = "dataGridView_result";
            this.dataGridView_result.ReadOnly = true;
            this.dataGridView_result.RowHeadersVisible = false;
            this.dataGridView_result.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_result.Size = new System.Drawing.Size(543, 132);
            this.dataGridView_result.TabIndex = 3;
            // 
            // button_auto
            // 
            this.button_auto.Location = new System.Drawing.Point(370, 0);
            this.button_auto.Margin = new System.Windows.Forms.Padding(2);
            this.button_auto.Name = "button_auto";
            this.button_auto.Size = new System.Drawing.Size(58, 24);
            this.button_auto.TabIndex = 10;
            this.button_auto.Text = "Auto";
            this.button_auto.UseVisualStyleBackColor = true;
            this.button_auto.Click += new System.EventHandler(this.Button_auto_Click);
            // 
            // button_next
            // 
            this.button_next.Location = new System.Drawing.Point(308, 0);
            this.button_next.Margin = new System.Windows.Forms.Padding(2);
            this.button_next.Name = "button_next";
            this.button_next.Size = new System.Drawing.Size(58, 24);
            this.button_next.TabIndex = 11;
            this.button_next.Text = "Next";
            this.button_next.UseVisualStyleBackColor = true;
            this.button_next.Click += new System.EventHandler(this.Button_next_Click);
            // 
            // button_find
            // 
            this.button_find.Location = new System.Drawing.Point(246, 0);
            this.button_find.Margin = new System.Windows.Forms.Padding(2);
            this.button_find.Name = "button_find";
            this.button_find.Size = new System.Drawing.Size(58, 24);
            this.button_find.TabIndex = 12;
            this.button_find.Text = "Find";
            this.button_find.UseVisualStyleBackColor = true;
            this.button_find.Click += new System.EventHandler(this.Button_find_Click);
            // 
            // label_commandDesc
            // 
            this.label_commandDesc.AutoSize = true;
            this.label_commandDesc.Location = new System.Drawing.Point(3, 0);
            this.label_commandDesc.Name = "label_commandDesc";
            this.label_commandDesc.Size = new System.Drawing.Size(60, 13);
            this.label_commandDesc.TabIndex = 16;
            this.label_commandDesc.Text = "Description";
            // 
            // label_command
            // 
            this.label_command.AutoSize = true;
            this.label_command.Location = new System.Drawing.Point(3, 0);
            this.label_command.Name = "label_command";
            this.label_command.Size = new System.Drawing.Size(54, 13);
            this.label_command.TabIndex = 15;
            this.label_command.Text = "Command";
            // 
            // textBox_commandDesc
            // 
            this.textBox_commandDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_commandDesc.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_commandDesc.Location = new System.Drawing.Point(3, 15);
            this.textBox_commandDesc.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.textBox_commandDesc.Multiline = true;
            this.textBox_commandDesc.Name = "textBox_commandDesc";
            this.textBox_commandDesc.ReadOnly = true;
            this.textBox_commandDesc.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_commandDesc.Size = new System.Drawing.Size(346, 94);
            this.textBox_commandDesc.TabIndex = 14;
            this.textBox_commandDesc.TabStop = false;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(792, 24);
            this.menuStrip.TabIndex = 25;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadBinToolStripMenuItem,
            this.loadHexToolStripMenuItem,
            this.saveBinFileToolStripMenuItem,
            this.saveHexFileToolStripMenuItem,
            this.LoadCSVToolStripMenuItem,
            this.saveCSVToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadBinToolStripMenuItem
            // 
            this.loadBinToolStripMenuItem.Name = "loadBinToolStripMenuItem";
            this.loadBinToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.loadBinToolStripMenuItem.Text = "Load binary file";
            this.loadBinToolStripMenuItem.Click += new System.EventHandler(this.LoadBinToolStripMenuItem_Click);
            // 
            // loadHexToolStripMenuItem
            // 
            this.loadHexToolStripMenuItem.Name = "loadHexToolStripMenuItem";
            this.loadHexToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.loadHexToolStripMenuItem.Text = "Load HEX text file";
            this.loadHexToolStripMenuItem.Click += new System.EventHandler(this.LoadHexToolStripMenuItem_Click);
            // 
            // saveBinFileToolStripMenuItem
            // 
            this.saveBinFileToolStripMenuItem.Name = "saveBinFileToolStripMenuItem";
            this.saveBinFileToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.saveBinFileToolStripMenuItem.Text = "Save binary file";
            this.saveBinFileToolStripMenuItem.Click += new System.EventHandler(this.SaveBinFileToolStripMenuItem_Click);
            // 
            // saveHexFileToolStripMenuItem
            // 
            this.saveHexFileToolStripMenuItem.Name = "saveHexFileToolStripMenuItem";
            this.saveHexFileToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.saveHexFileToolStripMenuItem.Text = "Save HEX text file";
            this.saveHexFileToolStripMenuItem.Click += new System.EventHandler(this.SaveHexFileToolStripMenuItem_Click);
            // 
            // LoadCSVToolStripMenuItem
            // 
            this.LoadCSVToolStripMenuItem.Name = "LoadCSVToolStripMenuItem";
            this.LoadCSVToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.LoadCSVToolStripMenuItem.Text = "Load database";
            this.LoadCSVToolStripMenuItem.Click += new System.EventHandler(this.LoadCSVToolStripMenuItem_Click);
            // 
            // saveCSVToolStripMenuItem
            // 
            this.saveCSVToolStripMenuItem.Name = "saveCSVToolStripMenuItem";
            this.saveCSVToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.saveCSVToolStripMenuItem.Text = "Save database";
            this.saveCSVToolStripMenuItem.Click += new System.EventHandler(this.SaveCSVToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cSVFileNameToolStripMenuItem,
            this.enableDatabaseEditToolStripMenuItem,
            this.enableFileEditToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.aboutToolStripMenuItem.Text = "Settings";
            // 
            // cSVFileNameToolStripMenuItem
            // 
            this.cSVFileNameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultCSVToolStripTextBox});
            this.cSVFileNameToolStripMenuItem.Name = "cSVFileNameToolStripMenuItem";
            this.cSVFileNameToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.cSVFileNameToolStripMenuItem.Text = "Default CSV database";
            // 
            // defaultCSVToolStripTextBox
            // 
            this.defaultCSVToolStripTextBox.Name = "defaultCSVToolStripTextBox";
            this.defaultCSVToolStripTextBox.Size = new System.Drawing.Size(100, 23);
            this.defaultCSVToolStripTextBox.Text = "esc_pos_commands.csv";
            this.defaultCSVToolStripTextBox.Leave += new System.EventHandler(this.DefaultCSVToolStripTextBox_Leave);
            // 
            // enableDatabaseEditToolStripMenuItem
            // 
            this.enableDatabaseEditToolStripMenuItem.Name = "enableDatabaseEditToolStripMenuItem";
            this.enableDatabaseEditToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.enableDatabaseEditToolStripMenuItem.Text = "Enable database edit";
            this.enableDatabaseEditToolStripMenuItem.Click += new System.EventHandler(this.EnableDatabaseEditToolStripMenuItem_Click);
            // 
            // enableFileEditToolStripMenuItem
            // 
            this.enableFileEditToolStripMenuItem.Name = "enableFileEditToolStripMenuItem";
            this.enableFileEditToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.enableFileEditToolStripMenuItem.Text = "Enable file edit";
            this.enableFileEditToolStripMenuItem.Click += new System.EventHandler(this.EnableFileEditToolStripMenuItem_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenFileDialog_FileOk);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.SaveFileDialog_FileOk);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.textBox_code);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(792, 256);
            this.splitContainer1.SplitterDistance = 241;
            this.splitContainer1.TabIndex = 26;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.dataGridView_result);
            this.splitContainer3.Size = new System.Drawing.Size(547, 256);
            this.splitContainer3.SplitterDistance = 116;
            this.splitContainer3.TabIndex = 17;
            // 
            // splitContainer4
            // 
            this.splitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.listBox_commands);
            this.splitContainer4.Panel1.Controls.Add(this.label_command);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.label_commandDesc);
            this.splitContainer4.Panel2.Controls.Add(this.textBox_commandDesc);
            this.splitContainer4.Size = new System.Drawing.Size(547, 116);
            this.splitContainer4.SplitterDistance = 187;
            this.splitContainer4.TabIndex = 0;
            // 
            // listBox_commands
            // 
            this.listBox_commands.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox_commands.FormattingEnabled = true;
            this.listBox_commands.HorizontalScrollbar = true;
            this.listBox_commands.Location = new System.Drawing.Point(3, 17);
            this.listBox_commands.Name = "listBox_commands";
            this.listBox_commands.Size = new System.Drawing.Size(177, 69);
            this.listBox_commands.TabIndex = 3;
            this.listBox_commands.SelectedIndexChanged += new System.EventHandler(this.ListBox_commands_SelectedIndexChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 24);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dataGridView_commands);
            this.splitContainer2.Size = new System.Drawing.Size(792, 549);
            this.splitContainer2.SplitterDistance = 256;
            this.splitContainer2.TabIndex = 27;
            // 
            // comboBox_printerType
            // 
            this.comboBox_printerType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_printerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_printerType.FormattingEnabled = true;
            this.comboBox_printerType.Location = new System.Drawing.Point(440, 1);
            this.comboBox_printerType.Name = "comboBox_printerType";
            this.comboBox_printerType.Size = new System.Drawing.Size(340, 21);
            this.comboBox_printerType.TabIndex = 28;
            // 
            // label_currentPosition
            // 
            this.label_currentPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_currentPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_currentPosition.Location = new System.Drawing.Point(123, 3);
            this.label_currentPosition.Name = "label_currentPosition";
            this.label_currentPosition.Size = new System.Drawing.Size(118, 19);
            this.label_currentPosition.TabIndex = 29;
            this.label_currentPosition.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.label_currentPosition);
            this.Controls.Add(this.comboBox_printerType);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.button_find);
            this.Controls.Add(this.button_auto);
            this.Controls.Add(this.button_next);
            this.Controls.Add(this.menuStrip);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "Form1";
            this.Text = "ESC/POS Parser";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_commands)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_result)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_code;
        private System.Windows.Forms.DataGridView dataGridView_commands;
        private System.Windows.Forms.DataGridView dataGridView_result;
        private System.Windows.Forms.Button button_auto;
        private System.Windows.Forms.Button button_next;
        private System.Windows.Forms.Button button_find;
        private System.Windows.Forms.Label label_commandDesc;
        private System.Windows.Forms.Label label_command;
        private System.Windows.Forms.TextBox textBox_commandDesc;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadBinToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadHexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveBinFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveHexFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoadCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cSVFileNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox defaultCSVToolStripTextBox;
        private System.Windows.Forms.ToolStripMenuItem enableDatabaseEditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableFileEditToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ComboBox comboBox_printerType;
        private System.Windows.Forms.Label label_currentPosition;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.ListBox listBox_commands;
    }
}

