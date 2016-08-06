using System.Windows.Forms;

namespace Logobot2_0
{

    partial class LogobotPaneMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
  
        #region Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogobotPaneMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageUser = new System.Windows.Forms.TabPage();
            this.imageListPane = new System.Windows.Forms.ImageList(this.components);
            this.labelUser = new System.Windows.Forms.Label();
            this.treeViewUser = new System.Windows.Forms.TreeView();
            this.dataGridViewUser = new System.Windows.Forms.DataGridView();
            this.dataGridViewUserImageColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewUserDetailsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageScreen = new System.Windows.Forms.TabPage();
            this.treeViewScreen = new System.Windows.Forms.TreeView();
            this.labelScreen = new System.Windows.Forms.Label();
            this.dataGridViewScreen = new System.Windows.Forms.DataGridView();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageSearch = new System.Windows.Forms.TabPage();
            this.textBoxPoweredBy = new System.Windows.Forms.TextBox();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.dataGridViewSearch = new System.Windows.Forms.DataGridView();
            this.ColumnImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.ColumnDetails = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControlBlotter = new System.Windows.Forms.TabControl();
            this.tabPageBlotter = new System.Windows.Forms.TabPage();
            this.dataGridViewBlotter = new System.Windows.Forms.DataGridView();
            this.ColumnImageBlotter = new System.Windows.Forms.DataGridViewImageColumn();
            this.ColumnDetailsBlotter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolTipButtonTip = new System.Windows.Forms.ToolTip(this.components);
            this.imageListTreeViews = new System.Windows.Forms.ImageList(this.components);
            this.backgroundWorkerSearch = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerScreen = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerUser = new System.ComponentModel.BackgroundWorker();
            this.openFileDialogSearch = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorkerMultiSearch = new System.ComponentModel.BackgroundWorker();
            this.buttonFillSelectedLogos = new System.Windows.Forms.Button();
            this.buttonFillLogos = new System.Windows.Forms.Button();
            this.buttonArrange = new System.Windows.Forms.Button();
            this.buttonCopyToSlide = new System.Windows.Forms.Button();
            this.buttonClearAll = new System.Windows.Forms.Button();
            this.buttonClearSelected = new System.Windows.Forms.Button();
            this.buttonUserRefresh = new System.Windows.Forms.Button();
            this.buttonUserBack = new System.Windows.Forms.Button();
            this.buttonUser = new System.Windows.Forms.Button();
            this.buttonMoveAllUser = new System.Windows.Forms.Button();
            this.buttonMoveSelectedUser = new System.Windows.Forms.Button();
            this.buttonScreenBack = new System.Windows.Forms.Button();
            this.buttonScreen = new System.Windows.Forms.Button();
            this.buttonMoveAllScreen = new System.Windows.Forms.Button();
            this.buttonMoveSelectedScreen = new System.Windows.Forms.Button();
            this.buttonSearchOpenFile = new System.Windows.Forms.Button();
            this.buttonMoreSearch = new System.Windows.Forms.Button();
            this.buttonMoveAll = new System.Windows.Forms.Button();
            this.buttonMoveSelected = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPageUser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUser)).BeginInit();
            this.tabPageScreen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewScreen)).BeginInit();
            this.tabPageSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSearch)).BeginInit();
            this.tabControlBlotter.SuspendLayout();
            this.tabPageBlotter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBlotter)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageUser);
            this.tabControl1.Controls.Add(this.tabPageScreen);
            this.tabControl1.Controls.Add(this.tabPageSearch);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(324, 252);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageUser
            // 
            this.tabPageUser.BackColor = System.Drawing.Color.White;
            this.tabPageUser.Controls.Add(this.buttonUserRefresh);
            this.tabPageUser.Controls.Add(this.buttonUserBack);
            this.tabPageUser.Controls.Add(this.buttonUser);
            this.tabPageUser.Controls.Add(this.buttonMoveAllUser);
            this.tabPageUser.Controls.Add(this.buttonMoveSelectedUser);
            this.tabPageUser.Controls.Add(this.labelUser);
            this.tabPageUser.Controls.Add(this.treeViewUser);
            this.tabPageUser.Controls.Add(this.dataGridViewUser);
            this.tabPageUser.Location = new System.Drawing.Point(4, 22);
            this.tabPageUser.Name = "tabPageUser";
            this.tabPageUser.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageUser.Size = new System.Drawing.Size(316, 226);
            this.tabPageUser.TabIndex = 0;
            this.tabPageUser.Text = "My Logobot";
            // 
            // imageListPane
            // 
            this.imageListPane.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListPane.ImageStream")));
            this.imageListPane.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListPane.Images.SetKeyName(0, "angellist.png");
            this.imageListPane.Images.SetKeyName(1, "angellist.png");
            this.imageListPane.Images.SetKeyName(2, "CBInsights.jpg");
            this.imageListPane.Images.SetKeyName(3, "crunchbase.jpg");
            this.imageListPane.Images.SetKeyName(4, "linkedin.png");
            this.imageListPane.Images.SetKeyName(5, "mattermark.jpg");
            this.imageListPane.Images.SetKeyName(6, "salesforce.png");
            this.imageListPane.Images.SetKeyName(7, "account.png");
            // 
            // labelUser
            // 
            this.labelUser.AutoSize = true;
            this.labelUser.Location = new System.Drawing.Point(3, 11);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(225, 13);
            this.labelUser.TabIndex = 2;
            this.labelUser.Text = "Select a blotter and press Show Logos button:";
            this.labelUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // treeViewUser
            // 
            this.treeViewUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewUser.Location = new System.Drawing.Point(3, 32);
            this.treeViewUser.Name = "treeViewUser";
            this.treeViewUser.Size = new System.Drawing.Size(306, 156);
            this.treeViewUser.TabIndex = 1;
            // 
            // dataGridViewUser
            // 
            this.dataGridViewUser.AllowUserToAddRows = false;
            this.dataGridViewUser.AllowUserToResizeColumns = false;
            this.dataGridViewUser.AllowUserToResizeRows = false;
            this.dataGridViewUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUser.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewUserImageColumn,
            this.dataGridViewUserDetailsColumn});
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewUser.DefaultCellStyle = dataGridViewCellStyle14;
            this.dataGridViewUser.GridColor = System.Drawing.SystemColors.Window;
            this.dataGridViewUser.Location = new System.Drawing.Point(3, 32);
            this.dataGridViewUser.Name = "dataGridViewUser";
            this.dataGridViewUser.RowHeadersVisible = false;
            this.dataGridViewUser.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(5);
            this.dataGridViewUser.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            this.dataGridViewUser.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewUser.RowTemplate.DividerHeight = 1;
            this.dataGridViewUser.RowTemplate.Height = 60;
            this.dataGridViewUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewUser.Size = new System.Drawing.Size(306, 156);
            this.dataGridViewUser.TabIndex = 8;
            this.dataGridViewUser.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewUser_CellContentDoubleClick);
            // 
            // dataGridViewUserImageColumn
            // 
            this.dataGridViewUserImageColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewUserImageColumn.DefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewUserImageColumn.HeaderText = "Logo";
            this.dataGridViewUserImageColumn.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.dataGridViewUserImageColumn.Name = "dataGridViewUserImageColumn";
            this.dataGridViewUserImageColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewUserImageColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewUserImageColumn.Width = 150;
            // 
            // dataGridViewUserDetailsColumn
            // 
            this.dataGridViewUserDetailsColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewUserDetailsColumn.DefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridViewUserDetailsColumn.HeaderText = "Details";
            this.dataGridViewUserDetailsColumn.Name = "dataGridViewUserDetailsColumn";
            this.dataGridViewUserDetailsColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewUserDetailsColumn.Width = 500;
            // 
            // tabPageScreen
            // 
            this.tabPageScreen.Controls.Add(this.treeViewScreen);
            this.tabPageScreen.Controls.Add(this.labelScreen);
            this.tabPageScreen.Controls.Add(this.dataGridViewScreen);
            this.tabPageScreen.Controls.Add(this.buttonScreenBack);
            this.tabPageScreen.Controls.Add(this.buttonScreen);
            this.tabPageScreen.Controls.Add(this.buttonMoveAllScreen);
            this.tabPageScreen.Controls.Add(this.buttonMoveSelectedScreen);
            this.tabPageScreen.Location = new System.Drawing.Point(4, 22);
            this.tabPageScreen.Name = "tabPageScreen";
            this.tabPageScreen.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageScreen.Size = new System.Drawing.Size(316, 226);
            this.tabPageScreen.TabIndex = 1;
            this.tabPageScreen.Text = "Logo Sources";
            this.tabPageScreen.UseVisualStyleBackColor = true;
            // 
            // treeViewScreen
            // 
            this.treeViewScreen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewScreen.Location = new System.Drawing.Point(3, 32);
            this.treeViewScreen.Name = "treeViewScreen";
            this.treeViewScreen.Size = new System.Drawing.Size(306, 156);
            this.treeViewScreen.TabIndex = 0;
            this.treeViewScreen.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewScreen_NodeMouseDoubleClick);
            // 
            // labelScreen
            // 
            this.labelScreen.AutoSize = true;
            this.labelScreen.Location = new System.Drawing.Point(3, 11);
            this.labelScreen.Name = "labelScreen";
            this.labelScreen.Size = new System.Drawing.Size(236, 13);
            this.labelScreen.TabIndex = 1;
            this.labelScreen.Text = "Select categories and press Show Logos button:";
            this.labelScreen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dataGridViewScreen
            // 
            this.dataGridViewScreen.AllowUserToAddRows = false;
            this.dataGridViewScreen.AllowUserToResizeColumns = false;
            this.dataGridViewScreen.AllowUserToResizeRows = false;
            this.dataGridViewScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewScreen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewScreen.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewImageColumn1,
            this.dataGridViewTextBoxColumn1});
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewScreen.DefaultCellStyle = dataGridViewCellStyle17;
            this.dataGridViewScreen.GridColor = System.Drawing.SystemColors.Window;
            this.dataGridViewScreen.Location = new System.Drawing.Point(3, 32);
            this.dataGridViewScreen.Name = "dataGridViewScreen";
            this.dataGridViewScreen.RowHeadersVisible = false;
            this.dataGridViewScreen.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(5);
            this.dataGridViewScreen.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            this.dataGridViewScreen.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewScreen.RowTemplate.DividerHeight = 1;
            this.dataGridViewScreen.RowTemplate.Height = 60;
            this.dataGridViewScreen.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewScreen.Size = new System.Drawing.Size(306, 156);
            this.dataGridViewScreen.TabIndex = 8;
            this.dataGridViewScreen.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewScreen_CellContentDoubleClick);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridViewImageColumn1.DefaultCellStyle = dataGridViewCellStyle15;
            this.dataGridViewImageColumn1.HeaderText = "Logo";
            this.dataGridViewImageColumn1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewImageColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle16;
            this.dataGridViewTextBoxColumn1.HeaderText = "Details";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.Width = 500;
            // 
            // tabPageSearch
            // 
            this.tabPageSearch.Controls.Add(this.textBoxPoweredBy);
            this.tabPageSearch.Controls.Add(this.textBoxSearch);
            this.tabPageSearch.Controls.Add(this.dataGridViewSearch);
            this.tabPageSearch.Controls.Add(this.buttonSearchOpenFile);
            this.tabPageSearch.Controls.Add(this.buttonMoreSearch);
            this.tabPageSearch.Controls.Add(this.buttonMoveAll);
            this.tabPageSearch.Controls.Add(this.buttonMoveSelected);
            this.tabPageSearch.Controls.Add(this.buttonSearch);
            this.tabPageSearch.Location = new System.Drawing.Point(4, 22);
            this.tabPageSearch.Name = "tabPageSearch";
            this.tabPageSearch.Size = new System.Drawing.Size(316, 226);
            this.tabPageSearch.TabIndex = 2;
            this.tabPageSearch.Text = "Logo Search";
            this.tabPageSearch.UseVisualStyleBackColor = true;
            // 
            // textBoxPoweredBy
            // 
            this.textBoxPoweredBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPoweredBy.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxPoweredBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPoweredBy.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBoxPoweredBy.Location = new System.Drawing.Point(106, 203);
            this.textBoxPoweredBy.Multiline = true;
            this.textBoxPoweredBy.Name = "textBoxPoweredBy";
            this.textBoxPoweredBy.Size = new System.Drawing.Size(90, 15);
            this.textBoxPoweredBy.TabIndex = 10;
            this.textBoxPoweredBy.Text = "Powered by Google\r\n";
            this.textBoxPoweredBy.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearch.Location = new System.Drawing.Point(6, 8);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(235, 20);
            this.textBoxSearch.TabIndex = 1;
            this.textBoxSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSearch_KeyDown);
            // 
            // dataGridViewSearch
            // 
            this.dataGridViewSearch.AllowUserToAddRows = false;
            this.dataGridViewSearch.AllowUserToResizeColumns = false;
            this.dataGridViewSearch.AllowUserToResizeRows = false;
            this.dataGridViewSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnImage,
            this.ColumnDetails});
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewSearch.DefaultCellStyle = dataGridViewCellStyle20;
            this.dataGridViewSearch.GridColor = System.Drawing.SystemColors.Window;
            this.dataGridViewSearch.Location = new System.Drawing.Point(6, 38);
            this.dataGridViewSearch.Name = "dataGridViewSearch";
            this.dataGridViewSearch.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dataGridViewSearch.RowHeadersVisible = false;
            this.dataGridViewSearch.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewSearch.RowsDefaultCellStyle = dataGridViewCellStyle21;
            this.dataGridViewSearch.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(5);
            this.dataGridViewSearch.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            this.dataGridViewSearch.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewSearch.RowTemplate.DividerHeight = 1;
            this.dataGridViewSearch.RowTemplate.Height = 60;
            this.dataGridViewSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSearch.Size = new System.Drawing.Size(303, 150);
            this.dataGridViewSearch.TabIndex = 5;
            this.dataGridViewSearch.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSearch_CellContentDoubleClick);
            // 
            // ColumnImage
            // 
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.ColumnImage.DefaultCellStyle = dataGridViewCellStyle18;
            this.ColumnImage.HeaderText = "Logo";
            this.ColumnImage.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.ColumnImage.Name = "ColumnImage";
            this.ColumnImage.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnImage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColumnImage.Width = 150;
            // 
            // ColumnDetails
            // 
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.ColumnDetails.DefaultCellStyle = dataGridViewCellStyle19;
            this.ColumnDetails.HeaderText = "Details";
            this.ColumnDetails.Name = "ColumnDetails";
            this.ColumnDetails.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnDetails.Width = 500;
            // 
            // tabControlBlotter
            // 
            this.tabControlBlotter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlBlotter.Controls.Add(this.tabPageBlotter);
            this.tabControlBlotter.Location = new System.Drawing.Point(3, 261);
            this.tabControlBlotter.Name = "tabControlBlotter";
            this.tabControlBlotter.SelectedIndex = 0;
            this.tabControlBlotter.Size = new System.Drawing.Size(324, 239);
            this.tabControlBlotter.TabIndex = 1;
            // 
            // tabPageBlotter
            // 
            this.tabPageBlotter.AllowDrop = true;
            this.tabPageBlotter.Controls.Add(this.buttonFillSelectedLogos);
            this.tabPageBlotter.Controls.Add(this.buttonFillLogos);
            this.tabPageBlotter.Controls.Add(this.buttonArrange);
            this.tabPageBlotter.Controls.Add(this.buttonCopyToSlide);
            this.tabPageBlotter.Controls.Add(this.buttonClearAll);
            this.tabPageBlotter.Controls.Add(this.buttonClearSelected);
            this.tabPageBlotter.Controls.Add(this.dataGridViewBlotter);
            this.tabPageBlotter.Location = new System.Drawing.Point(4, 22);
            this.tabPageBlotter.Name = "tabPageBlotter";
            this.tabPageBlotter.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBlotter.Size = new System.Drawing.Size(316, 213);
            this.tabPageBlotter.TabIndex = 0;
            this.tabPageBlotter.Text = "Logo Blotter";
            this.tabPageBlotter.UseVisualStyleBackColor = true;
            // 
            // dataGridViewBlotter
            // 
            this.dataGridViewBlotter.AllowDrop = true;
            this.dataGridViewBlotter.AllowUserToAddRows = false;
            this.dataGridViewBlotter.AllowUserToResizeColumns = false;
            this.dataGridViewBlotter.AllowUserToResizeRows = false;
            this.dataGridViewBlotter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewBlotter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBlotter.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnImageBlotter,
            this.ColumnDetailsBlotter});
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewBlotter.DefaultCellStyle = dataGridViewCellStyle22;
            this.dataGridViewBlotter.GridColor = System.Drawing.SystemColors.Window;
            this.dataGridViewBlotter.Location = new System.Drawing.Point(6, 6);
            this.dataGridViewBlotter.Name = "dataGridViewBlotter";
            this.dataGridViewBlotter.RowHeadersVisible = false;
            this.dataGridViewBlotter.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(5);
            this.dataGridViewBlotter.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            this.dataGridViewBlotter.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewBlotter.RowTemplate.DividerHeight = 1;
            this.dataGridViewBlotter.RowTemplate.Height = 60;
            this.dataGridViewBlotter.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewBlotter.Size = new System.Drawing.Size(303, 170);
            this.dataGridViewBlotter.TabIndex = 6;
            this.dataGridViewBlotter.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBlotter_CellContentDoubleClick);
            this.dataGridViewBlotter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridViewBlotter_MouseDown);
            // 
            // ColumnImageBlotter
            // 
            this.ColumnImageBlotter.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnImageBlotter.DefaultCellStyle = dataGridViewCellStyle18;
            this.ColumnImageBlotter.HeaderText = "Logo";
            this.ColumnImageBlotter.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.ColumnImageBlotter.Name = "ColumnImageBlotter";
            this.ColumnImageBlotter.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnImageBlotter.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColumnImageBlotter.Width = 150;
            // 
            // ColumnDetailsBlotter
            // 
            this.ColumnDetailsBlotter.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnDetailsBlotter.DefaultCellStyle = dataGridViewCellStyle19;
            this.ColumnDetailsBlotter.HeaderText = "Details";
            this.ColumnDetailsBlotter.Name = "ColumnDetailsBlotter";
            this.ColumnDetailsBlotter.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnDetailsBlotter.Width = 500;
            // 
            // imageListTreeViews
            // 
            this.imageListTreeViews.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTreeViews.ImageStream")));
            this.imageListTreeViews.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTreeViews.Images.SetKeyName(0, "account_small.png");
            this.imageListTreeViews.Images.SetKeyName(1, "CBInsights_small.jpg");
            this.imageListTreeViews.Images.SetKeyName(2, "crunchbase_small.jpg");
            this.imageListTreeViews.Images.SetKeyName(3, "folder_small.png");
            this.imageListTreeViews.Images.SetKeyName(4, "world_small.png");
            // 
            // backgroundWorkerSearch
            // 
            this.backgroundWorkerSearch.WorkerReportsProgress = true;
            this.backgroundWorkerSearch.WorkerSupportsCancellation = true;
            this.backgroundWorkerSearch.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerSearch_DoWork);
            this.backgroundWorkerSearch.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerSearch_RunWorkerCompleted);
            // 
            // backgroundWorkerScreen
            // 
            this.backgroundWorkerScreen.WorkerReportsProgress = true;
            this.backgroundWorkerScreen.WorkerSupportsCancellation = true;
            this.backgroundWorkerScreen.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerScreen_DoWork);
            this.backgroundWorkerScreen.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerScreen_RunWorkerCompleted);
            // 
            // backgroundWorkerUser
            // 
            this.backgroundWorkerUser.WorkerReportsProgress = true;
            this.backgroundWorkerUser.WorkerSupportsCancellation = true;
            this.backgroundWorkerUser.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerUser_DoWork);
            this.backgroundWorkerUser.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerUser_RunWorkerCompleted);
            // 
            // openFileDialogSearch
            // 
            this.openFileDialogSearch.Filter = "Excel  (*.xls,*.xlsx, *.xlsm, *.xlsb)|*.xls;*.xlsx; *.xlsm; *.xlsb";
            // 
            // backgroundWorkerMultiSearch
            // 
            this.backgroundWorkerMultiSearch.WorkerReportsProgress = true;
            this.backgroundWorkerMultiSearch.WorkerSupportsCancellation = true;
            this.backgroundWorkerMultiSearch.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerMultiSearch_DoWork);
            this.backgroundWorkerMultiSearch.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerMultiSearch_RunWorkerCompleted);
            // 
            // buttonFillSelectedLogos
            // 
            this.buttonFillSelectedLogos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonFillSelectedLogos.BackColor = System.Drawing.SystemColors.Window;
            this.buttonFillSelectedLogos.BackgroundImage = global::Logobot2_0.Properties.Resources.Fill_Selected;
            this.buttonFillSelectedLogos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonFillSelectedLogos.Location = new System.Drawing.Point(75, 179);
            this.buttonFillSelectedLogos.Name = "buttonFillSelectedLogos";
            this.buttonFillSelectedLogos.Size = new System.Drawing.Size(31, 31);
            this.buttonFillSelectedLogos.TabIndex = 13;
            this.toolTipButtonTip.SetToolTip(this.buttonFillSelectedLogos, "Fill arrangement shape with only selected\r\n logos in the blotter.\r\n\r\n\r\n");
            this.buttonFillSelectedLogos.UseVisualStyleBackColor = false;
            this.buttonFillSelectedLogos.Click += new System.EventHandler(this.buttonFillSelectedLogos_Click);
            // 
            // buttonFillLogos
            // 
            this.buttonFillLogos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonFillLogos.BackColor = System.Drawing.SystemColors.Window;
            this.buttonFillLogos.BackgroundImage = global::Logobot2_0.Properties.Resources.Fill_All;
            this.buttonFillLogos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonFillLogos.Location = new System.Drawing.Point(40, 179);
            this.buttonFillLogos.Name = "buttonFillLogos";
            this.buttonFillLogos.Size = new System.Drawing.Size(31, 31);
            this.buttonFillLogos.TabIndex = 12;
            this.toolTipButtonTip.SetToolTip(this.buttonFillLogos, "Fill arrangement shape with all logos in the blotter.\r\n\r\n\r\n");
            this.buttonFillLogos.UseVisualStyleBackColor = false;
            this.buttonFillLogos.Click += new System.EventHandler(this.buttonFillLogos_Click);
            // 
            // buttonArrange
            // 
            this.buttonArrange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonArrange.BackColor = System.Drawing.SystemColors.Window;
            this.buttonArrange.BackgroundImage = global::Logobot2_0.Properties.Resources.DrawShape;
            this.buttonArrange.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonArrange.Location = new System.Drawing.Point(5, 179);
            this.buttonArrange.Name = "buttonArrange";
            this.buttonArrange.Size = new System.Drawing.Size(31, 31);
            this.buttonArrange.TabIndex = 11;
            this.toolTipButtonTip.SetToolTip(this.buttonArrange, "Draw logo arrangement shape.\r\n\r\n");
            this.buttonArrange.UseVisualStyleBackColor = false;
            this.buttonArrange.Click += new System.EventHandler(this.buttonArrange_Click);
            // 
            // buttonCopyToSlide
            // 
            this.buttonCopyToSlide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCopyToSlide.BackColor = System.Drawing.Color.White;
            this.buttonCopyToSlide.BackgroundImage = global::Logobot2_0.Properties.Resources.copy;
            this.buttonCopyToSlide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonCopyToSlide.Location = new System.Drawing.Point(110, 179);
            this.buttonCopyToSlide.Name = "buttonCopyToSlide";
            this.buttonCopyToSlide.Size = new System.Drawing.Size(31, 31);
            this.buttonCopyToSlide.TabIndex = 10;
            this.toolTipButtonTip.SetToolTip(this.buttonCopyToSlide, "Copy selected logos to slide\r\n");
            this.buttonCopyToSlide.UseVisualStyleBackColor = false;
            this.buttonCopyToSlide.Click += new System.EventHandler(this.buttonCopyToSlide_Click);
            // 
            // buttonClearAll
            // 
            this.buttonClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClearAll.BackColor = System.Drawing.SystemColors.Window;
            this.buttonClearAll.BackgroundImage = global::Logobot2_0.Properties.Resources.delete_all1;
            this.buttonClearAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonClearAll.Location = new System.Drawing.Point(244, 179);
            this.buttonClearAll.Name = "buttonClearAll";
            this.buttonClearAll.Size = new System.Drawing.Size(31, 31);
            this.buttonClearAll.TabIndex = 9;
            this.toolTipButtonTip.SetToolTip(this.buttonClearAll, "Clear All\r\n");
            this.buttonClearAll.UseVisualStyleBackColor = false;
            this.buttonClearAll.Click += new System.EventHandler(this.buttonClearAll_Click);
            // 
            // buttonClearSelected
            // 
            this.buttonClearSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClearSelected.BackColor = System.Drawing.SystemColors.Window;
            this.buttonClearSelected.BackgroundImage = global::Logobot2_0.Properties.Resources.delete_selected1;
            this.buttonClearSelected.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonClearSelected.Location = new System.Drawing.Point(278, 179);
            this.buttonClearSelected.Name = "buttonClearSelected";
            this.buttonClearSelected.Size = new System.Drawing.Size(31, 31);
            this.buttonClearSelected.TabIndex = 8;
            this.toolTipButtonTip.SetToolTip(this.buttonClearSelected, "Clear Selected\r\n");
            this.buttonClearSelected.UseVisualStyleBackColor = false;
            this.buttonClearSelected.Click += new System.EventHandler(this.buttonClearSelected_Click);
            // 
            // buttonUserRefresh
            // 
            this.buttonUserRefresh.BackColor = System.Drawing.SystemColors.Window;
            this.buttonUserRefresh.BackgroundImage = global::Logobot2_0.Properties.Resources.reset;
            this.buttonUserRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonUserRefresh.ImageList = this.imageListPane;
            this.buttonUserRefresh.Location = new System.Drawing.Point(39, 192);
            this.buttonUserRefresh.Name = "buttonUserRefresh";
            this.buttonUserRefresh.Size = new System.Drawing.Size(30, 30);
            this.buttonUserRefresh.TabIndex = 14;
            this.toolTipButtonTip.SetToolTip(this.buttonUserRefresh, "Refresh Saved Blotters");
            this.buttonUserRefresh.UseVisualStyleBackColor = false;
            this.buttonUserRefresh.Click += new System.EventHandler(this.buttonUserRefresh_Click);
            // 
            // buttonUserBack
            // 
            this.buttonUserBack.BackgroundImage = global::Logobot2_0.Properties.Resources.back;
            this.buttonUserBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonUserBack.Location = new System.Drawing.Point(39, 192);
            this.buttonUserBack.Name = "buttonUserBack";
            this.buttonUserBack.Size = new System.Drawing.Size(30, 30);
            this.buttonUserBack.TabIndex = 13;
            this.toolTipButtonTip.SetToolTip(this.buttonUserBack, "Back to Categories");
            this.buttonUserBack.UseVisualStyleBackColor = true;
            this.buttonUserBack.Visible = false;
            this.buttonUserBack.Click += new System.EventHandler(this.buttonUserBack_Click);
            // 
            // buttonUser
            // 
            this.buttonUser.BackColor = System.Drawing.SystemColors.Window;
            this.buttonUser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonUser.BackgroundImage")));
            this.buttonUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonUser.Location = new System.Drawing.Point(3, 192);
            this.buttonUser.Name = "buttonUser";
            this.buttonUser.Size = new System.Drawing.Size(30, 30);
            this.buttonUser.TabIndex = 10;
            this.toolTipButtonTip.SetToolTip(this.buttonUser, "Show Logos");
            this.buttonUser.UseVisualStyleBackColor = false;
            this.buttonUser.Click += new System.EventHandler(this.buttonUser_Click);
            // 
            // buttonMoveAllUser
            // 
            this.buttonMoveAllUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMoveAllUser.BackgroundImage = global::Logobot2_0.Properties.Resources.move_all;
            this.buttonMoveAllUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMoveAllUser.Location = new System.Drawing.Point(244, 192);
            this.buttonMoveAllUser.Name = "buttonMoveAllUser";
            this.buttonMoveAllUser.Size = new System.Drawing.Size(31, 31);
            this.buttonMoveAllUser.TabIndex = 11;
            this.toolTipButtonTip.SetToolTip(this.buttonMoveAllUser, "Move All to Blotter");
            this.buttonMoveAllUser.UseVisualStyleBackColor = true;
            this.buttonMoveAllUser.Visible = false;
            this.buttonMoveAllUser.Click += new System.EventHandler(this.buttonMoveAllUser_Click);
            // 
            // buttonMoveSelectedUser
            // 
            this.buttonMoveSelectedUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMoveSelectedUser.BackgroundImage = global::Logobot2_0.Properties.Resources.move_selected;
            this.buttonMoveSelectedUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMoveSelectedUser.Location = new System.Drawing.Point(278, 192);
            this.buttonMoveSelectedUser.Name = "buttonMoveSelectedUser";
            this.buttonMoveSelectedUser.Size = new System.Drawing.Size(31, 31);
            this.buttonMoveSelectedUser.TabIndex = 12;
            this.toolTipButtonTip.SetToolTip(this.buttonMoveSelectedUser, "Move Selected\r\n to Blotter");
            this.buttonMoveSelectedUser.UseVisualStyleBackColor = true;
            this.buttonMoveSelectedUser.Visible = false;
            this.buttonMoveSelectedUser.Click += new System.EventHandler(this.buttonMoveSelectedUser_Click);
            // 
            // buttonScreenBack
            // 
            this.buttonScreenBack.BackColor = System.Drawing.SystemColors.Window;
            this.buttonScreenBack.BackgroundImage = global::Logobot2_0.Properties.Resources.back;
            this.buttonScreenBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonScreenBack.Location = new System.Drawing.Point(39, 192);
            this.buttonScreenBack.Name = "buttonScreenBack";
            this.buttonScreenBack.Size = new System.Drawing.Size(30, 30);
            this.buttonScreenBack.TabIndex = 9;
            this.toolTipButtonTip.SetToolTip(this.buttonScreenBack, "Back to Categories");
            this.buttonScreenBack.UseVisualStyleBackColor = false;
            this.buttonScreenBack.Visible = false;
            this.buttonScreenBack.Click += new System.EventHandler(this.buttonScreenBack_Click);
            // 
            // buttonScreen
            // 
            this.buttonScreen.BackColor = System.Drawing.SystemColors.Window;
            this.buttonScreen.BackgroundImage = global::Logobot2_0.Properties.Resources.show_logos;
            this.buttonScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonScreen.Location = new System.Drawing.Point(3, 192);
            this.buttonScreen.Name = "buttonScreen";
            this.buttonScreen.Size = new System.Drawing.Size(30, 30);
            this.buttonScreen.TabIndex = 2;
            this.toolTipButtonTip.SetToolTip(this.buttonScreen, "Show Logos");
            this.buttonScreen.UseVisualStyleBackColor = false;
            this.buttonScreen.Click += new System.EventHandler(this.buttonScreen_Click);
            // 
            // buttonMoveAllScreen
            // 
            this.buttonMoveAllScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMoveAllScreen.BackgroundImage = global::Logobot2_0.Properties.Resources.move_all;
            this.buttonMoveAllScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMoveAllScreen.Location = new System.Drawing.Point(244, 192);
            this.buttonMoveAllScreen.Name = "buttonMoveAllScreen";
            this.buttonMoveAllScreen.Size = new System.Drawing.Size(31, 31);
            this.buttonMoveAllScreen.TabIndex = 6;
            this.toolTipButtonTip.SetToolTip(this.buttonMoveAllScreen, "Move All to Blotter");
            this.buttonMoveAllScreen.UseVisualStyleBackColor = true;
            this.buttonMoveAllScreen.Visible = false;
            this.buttonMoveAllScreen.Click += new System.EventHandler(this.buttonMoveAllScreen_Click);
            // 
            // buttonMoveSelectedScreen
            // 
            this.buttonMoveSelectedScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMoveSelectedScreen.BackgroundImage = global::Logobot2_0.Properties.Resources.move_selected;
            this.buttonMoveSelectedScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMoveSelectedScreen.Location = new System.Drawing.Point(278, 192);
            this.buttonMoveSelectedScreen.Name = "buttonMoveSelectedScreen";
            this.buttonMoveSelectedScreen.Size = new System.Drawing.Size(31, 31);
            this.buttonMoveSelectedScreen.TabIndex = 7;
            this.toolTipButtonTip.SetToolTip(this.buttonMoveSelectedScreen, "Move Selected\r\n to Blotter");
            this.buttonMoveSelectedScreen.UseVisualStyleBackColor = true;
            this.buttonMoveSelectedScreen.Visible = false;
            this.buttonMoveSelectedScreen.Click += new System.EventHandler(this.buttonMoveSelectedScreen_Click);
            // 
            // buttonSearchOpenFile
            // 
            this.buttonSearchOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSearchOpenFile.BackgroundImage = global::Logobot2_0.Properties.Resources.folder_2;
            this.buttonSearchOpenFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonSearchOpenFile.Location = new System.Drawing.Point(278, 4);
            this.buttonSearchOpenFile.Name = "buttonSearchOpenFile";
            this.buttonSearchOpenFile.Size = new System.Drawing.Size(30, 30);
            this.buttonSearchOpenFile.TabIndex = 9;
            this.toolTipButtonTip.SetToolTip(this.buttonSearchOpenFile, "Select file to search many logos.");
            this.buttonSearchOpenFile.UseVisualStyleBackColor = true;
            this.buttonSearchOpenFile.Click += new System.EventHandler(this.buttonSearchOpenFile_Click);
            // 
            // buttonMoreSearch
            // 
            this.buttonMoreSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonMoreSearch.BackgroundImage = global::Logobot2_0.Properties.Resources.plus;
            this.buttonMoreSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMoreSearch.Location = new System.Drawing.Point(6, 194);
            this.buttonMoreSearch.Name = "buttonMoreSearch";
            this.buttonMoreSearch.Size = new System.Drawing.Size(31, 31);
            this.buttonMoreSearch.TabIndex = 8;
            this.toolTipButtonTip.SetToolTip(this.buttonMoreSearch, "More Results");
            this.buttonMoreSearch.UseVisualStyleBackColor = true;
            this.buttonMoreSearch.Visible = false;
            this.buttonMoreSearch.Click += new System.EventHandler(this.buttonMoreSearch_Click);
            // 
            // buttonMoveAll
            // 
            this.buttonMoveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMoveAll.BackgroundImage = global::Logobot2_0.Properties.Resources.move_all;
            this.buttonMoveAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMoveAll.Location = new System.Drawing.Point(244, 192);
            this.buttonMoveAll.Name = "buttonMoveAll";
            this.buttonMoveAll.Size = new System.Drawing.Size(31, 31);
            this.buttonMoveAll.TabIndex = 6;
            this.toolTipButtonTip.SetToolTip(this.buttonMoveAll, "Move All to Blotter");
            this.buttonMoveAll.UseVisualStyleBackColor = true;
            this.buttonMoveAll.Click += new System.EventHandler(this.buttonMoveAll_Click);
            // 
            // buttonMoveSelected
            // 
            this.buttonMoveSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMoveSelected.BackgroundImage = global::Logobot2_0.Properties.Resources.move_selected;
            this.buttonMoveSelected.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMoveSelected.Location = new System.Drawing.Point(278, 192);
            this.buttonMoveSelected.Name = "buttonMoveSelected";
            this.buttonMoveSelected.Size = new System.Drawing.Size(31, 31);
            this.buttonMoveSelected.TabIndex = 7;
            this.toolTipButtonTip.SetToolTip(this.buttonMoveSelected, "Move Selected\r\n to Blotter");
            this.buttonMoveSelected.UseVisualStyleBackColor = true;
            this.buttonMoveSelected.Click += new System.EventHandler(this.buttonMoveSelected_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSearch.BackgroundImage = global::Logobot2_0.Properties.Resources.search;
            this.buttonSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonSearch.Location = new System.Drawing.Point(244, 4);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(30, 30);
            this.buttonSearch.TabIndex = 2;
            this.toolTipButtonTip.SetToolTip(this.buttonSearch, "Search");
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // LogobotPaneMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(330, 500);
            this.Controls.Add(this.tabControlBlotter);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "LogobotPaneMain";
            this.Text = "Logobot";
            this.toolTipButtonTip.SetToolTip(this, "\r\n");
            this.ADXCloseButtonClick += new AddinExpress.PP.ADXCloseButtonClickEventHandler(this.LogobotPaneMain_ADXCloseButtonClick);
            this.Load += new System.EventHandler(this.LogobotPaneMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageUser.ResumeLayout(false);
            this.tabPageUser.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUser)).EndInit();
            this.tabPageScreen.ResumeLayout(false);
            this.tabPageScreen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewScreen)).EndInit();
            this.tabPageSearch.ResumeLayout(false);
            this.tabPageSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSearch)).EndInit();
            this.tabControlBlotter.ResumeLayout(false);
            this.tabPageBlotter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBlotter)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageScreen;
        private System.Windows.Forms.TabPage tabPageSearch;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.DataGridView dataGridViewSearch;
        private System.Windows.Forms.TabControl tabControlBlotter;
        private System.Windows.Forms.TabPage tabPageBlotter;
        private System.Windows.Forms.Button buttonMoveAll;
        private System.Windows.Forms.Button buttonMoveSelected;
        private System.Windows.Forms.Button buttonMoveAllScreen;
        private System.Windows.Forms.Button buttonMoveSelectedScreen;
        private System.Windows.Forms.DataGridView dataGridViewBlotter;
        private System.Windows.Forms.ToolTip toolTipButtonTip;
        private System.Windows.Forms.Button buttonClearAll;
        private System.Windows.Forms.Button buttonClearSelected;
        private System.Windows.Forms.DataGridViewImageColumn ColumnImageBlotter;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDetailsBlotter;
        private System.Windows.Forms.TreeView treeViewScreen;
        private System.Windows.Forms.Button buttonScreen;
        private System.Windows.Forms.Label labelScreen;
        private System.Windows.Forms.DataGridView dataGridViewScreen;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.Button buttonScreenBack;
        private System.Windows.Forms.DataGridViewImageColumn ColumnImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDetails;
        private System.Windows.Forms.ImageList imageListPane;
        private DataGridView dataGridViewUser;
        private DataGridViewImageColumn dataGridViewUserImageColumn;
        private DataGridViewTextBoxColumn dataGridViewUserDetailsColumn;
        private TabPage tabPageUser;
        private Button buttonUserBack;
        private Button buttonUser;
        private Button buttonMoveAllUser;
        private Button buttonMoveSelectedUser;
        private Label labelUser;
        private TreeView treeViewUser;
        private ImageList imageListTreeViews;
        private Button buttonCopyToSlide;
        private System.ComponentModel.BackgroundWorker backgroundWorkerSearch;
        private System.ComponentModel.BackgroundWorker backgroundWorkerScreen;
        private Button buttonUserRefresh;
        private System.ComponentModel.BackgroundWorker backgroundWorkerUser;
        private Button buttonMoreSearch;
        private Button buttonSearchOpenFile;
        private OpenFileDialog openFileDialogSearch;
        private System.ComponentModel.BackgroundWorker backgroundWorkerMultiSearch;
        private Button buttonArrange;
        private Button buttonFillLogos;
        private TextBox textBoxPoweredBy;
        private Button buttonFillSelectedLogos;
    }
}
