namespace Logobot2_0
{
    partial class MultipleSearchForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewMultiBlotter = new System.Windows.Forms.DataGridView();
            this.ColumnImageBlotter = new System.Windows.Forms.DataGridViewImageColumn();
            this.ColumnDetailsBlotter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonMoveAllMulti = new System.Windows.Forms.Button();
            this.buttonMoveSelectedMulti = new System.Windows.Forms.Button();
            this.labelMultiSearch = new System.Windows.Forms.Label();
            this.listBoxMultiSearch = new System.Windows.Forms.ListBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.toolTipButtonTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMultiBlotter)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewMultiBlotter
            // 
            this.dataGridViewMultiBlotter.AllowUserToAddRows = false;
            this.dataGridViewMultiBlotter.AllowUserToResizeColumns = false;
            this.dataGridViewMultiBlotter.AllowUserToResizeRows = false;
            this.dataGridViewMultiBlotter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewMultiBlotter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMultiBlotter.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnImageBlotter,
            this.ColumnDetailsBlotter});
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewMultiBlotter.DefaultCellStyle = dataGridViewCellStyle15;
            this.dataGridViewMultiBlotter.GridColor = System.Drawing.SystemColors.Window;
            this.dataGridViewMultiBlotter.Location = new System.Drawing.Point(218, 30);
            this.dataGridViewMultiBlotter.Name = "dataGridViewMultiBlotter";
            this.dataGridViewMultiBlotter.RowHeadersVisible = false;
            this.dataGridViewMultiBlotter.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(5);
            this.dataGridViewMultiBlotter.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            this.dataGridViewMultiBlotter.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewMultiBlotter.RowTemplate.DividerHeight = 1;
            this.dataGridViewMultiBlotter.RowTemplate.Height = 60;
            this.dataGridViewMultiBlotter.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMultiBlotter.Size = new System.Drawing.Size(286, 207);
            this.dataGridViewMultiBlotter.TabIndex = 7;
            this.dataGridViewMultiBlotter.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMultiBlotter_CellContentDoubleClick);
            // 
            // ColumnImageBlotter
            // 
            this.ColumnImageBlotter.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnImageBlotter.DefaultCellStyle = dataGridViewCellStyle13;
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
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnDetailsBlotter.DefaultCellStyle = dataGridViewCellStyle14;
            this.ColumnDetailsBlotter.HeaderText = "Details";
            this.ColumnDetailsBlotter.Name = "ColumnDetailsBlotter";
            this.ColumnDetailsBlotter.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnDetailsBlotter.Width = 500;
            // 
            // buttonMoveAllMulti
            // 
            this.buttonMoveAllMulti.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMoveAllMulti.BackgroundImage = global::Logobot2_0.Properties.Resources.move_all;
            this.buttonMoveAllMulti.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMoveAllMulti.Location = new System.Drawing.Point(438, 240);
            this.buttonMoveAllMulti.Name = "buttonMoveAllMulti";
            this.buttonMoveAllMulti.Size = new System.Drawing.Size(31, 31);
            this.buttonMoveAllMulti.TabIndex = 13;
            this.toolTipButtonTip.SetToolTip(this.buttonMoveAllMulti, "Move All to Blotter");
            this.buttonMoveAllMulti.UseVisualStyleBackColor = true;
            this.buttonMoveAllMulti.Click += new System.EventHandler(this.buttonMoveAllMulti_Click);
            // 
            // buttonMoveSelectedMulti
            // 
            this.buttonMoveSelectedMulti.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMoveSelectedMulti.BackgroundImage = global::Logobot2_0.Properties.Resources.move_selected;
            this.buttonMoveSelectedMulti.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMoveSelectedMulti.Location = new System.Drawing.Point(475, 240);
            this.buttonMoveSelectedMulti.Name = "buttonMoveSelectedMulti";
            this.buttonMoveSelectedMulti.Size = new System.Drawing.Size(31, 31);
            this.buttonMoveSelectedMulti.TabIndex = 14;
            this.toolTipButtonTip.SetToolTip(this.buttonMoveSelectedMulti, "Move Selected to Blotter");
            this.buttonMoveSelectedMulti.UseVisualStyleBackColor = true;
            this.buttonMoveSelectedMulti.Click += new System.EventHandler(this.buttonMoveSelectedMulti_Click);
            // 
            // labelMultiSearch
            // 
            this.labelMultiSearch.AutoSize = true;
            this.labelMultiSearch.Location = new System.Drawing.Point(12, 12);
            this.labelMultiSearch.Name = "labelMultiSearch";
            this.labelMultiSearch.Size = new System.Drawing.Size(137, 13);
            this.labelMultiSearch.TabIndex = 15;
            this.labelMultiSearch.Text = "Select an item to see logos:";
            this.labelMultiSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listBoxMultiSearch
            // 
            this.listBoxMultiSearch.FormattingEnabled = true;
            this.listBoxMultiSearch.HorizontalScrollbar = true;
            this.listBoxMultiSearch.Location = new System.Drawing.Point(15, 30);
            this.listBoxMultiSearch.Name = "listBoxMultiSearch";
            this.listBoxMultiSearch.Size = new System.Drawing.Size(186, 199);
            this.listBoxMultiSearch.TabIndex = 16;
            this.listBoxMultiSearch.SelectedIndexChanged += new System.EventHandler(this.listBoxMultiSearch_SelectedIndexChanged);
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(438, 270);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(68, 23);
            this.buttonClose.TabIndex = 17;
            this.buttonClose.Text = "Close";
            this.toolTipButtonTip.SetToolTip(this.buttonClose, "Close");
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // MultipleSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(516, 296);
            this.ControlBox = false;
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.listBoxMultiSearch);
            this.Controls.Add(this.buttonMoveSelectedMulti);
            this.Controls.Add(this.buttonMoveAllMulti);
            this.Controls.Add(this.labelMultiSearch);
            this.Controls.Add(this.dataGridViewMultiBlotter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MultipleSearchForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MultipleSearchForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMultiBlotter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewMultiBlotter;
        private System.Windows.Forms.Button buttonMoveAllMulti;
        private System.Windows.Forms.Button buttonMoveSelectedMulti;
        private System.Windows.Forms.Label labelMultiSearch;
        private System.Windows.Forms.ListBox listBoxMultiSearch;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.DataGridViewImageColumn ColumnImageBlotter;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDetailsBlotter;
        private System.Windows.Forms.ToolTip toolTipButtonTip;
    }
}