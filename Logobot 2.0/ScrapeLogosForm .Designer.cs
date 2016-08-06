namespace Logobot2_0
{
    partial class ScrapeLogosForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewScrapeLogosBlotter = new System.Windows.Forms.DataGridView();
            this.ColumnImageBlotter = new System.Windows.Forms.DataGridViewImageColumn();
            this.ColumnDetailsBlotter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonMoveAll = new System.Windows.Forms.Button();
            this.buttonMoveSelected = new System.Windows.Forms.Button();
            this.labelScrapeLogos = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.toolTipButtonTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewScrapeLogosBlotter)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewScrapeLogosBlotter
            // 
            this.dataGridViewScrapeLogosBlotter.AllowUserToAddRows = false;
            this.dataGridViewScrapeLogosBlotter.AllowUserToResizeColumns = false;
            this.dataGridViewScrapeLogosBlotter.AllowUserToResizeRows = false;
            this.dataGridViewScrapeLogosBlotter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewScrapeLogosBlotter.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewScrapeLogosBlotter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewScrapeLogosBlotter.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnImageBlotter,
            this.ColumnDetailsBlotter});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewScrapeLogosBlotter.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewScrapeLogosBlotter.GridColor = System.Drawing.SystemColors.Window;
            this.dataGridViewScrapeLogosBlotter.Location = new System.Drawing.Point(6, 30);
            this.dataGridViewScrapeLogosBlotter.Name = "dataGridViewScrapeLogosBlotter";
            this.dataGridViewScrapeLogosBlotter.RowHeadersVisible = false;
            this.dataGridViewScrapeLogosBlotter.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(5);
            this.dataGridViewScrapeLogosBlotter.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            this.dataGridViewScrapeLogosBlotter.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewScrapeLogosBlotter.RowTemplate.DividerHeight = 1;
            this.dataGridViewScrapeLogosBlotter.RowTemplate.Height = 60;
            this.dataGridViewScrapeLogosBlotter.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewScrapeLogosBlotter.Size = new System.Drawing.Size(286, 195);
            this.dataGridViewScrapeLogosBlotter.TabIndex = 7;
            this.dataGridViewScrapeLogosBlotter.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewScrapeLogosBlotter_CellContentDoubleClick);
            // 
            // ColumnImageBlotter
            // 
            this.ColumnImageBlotter.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnImageBlotter.DefaultCellStyle = dataGridViewCellStyle2;
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnDetailsBlotter.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnDetailsBlotter.HeaderText = "Details";
            this.ColumnDetailsBlotter.Name = "ColumnDetailsBlotter";
            this.ColumnDetailsBlotter.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnDetailsBlotter.Width = 500;
            // 
            // buttonMoveAll
            // 
            this.buttonMoveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMoveAll.BackgroundImage = global::Logobot2_0.Properties.Resources.move_all;
            this.buttonMoveAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMoveAll.Location = new System.Drawing.Point(224, 231);
            this.buttonMoveAll.Name = "buttonMoveAll";
            this.buttonMoveAll.Size = new System.Drawing.Size(31, 31);
            this.buttonMoveAll.TabIndex = 13;
            this.toolTipButtonTip.SetToolTip(this.buttonMoveAll, "Move All to Blotter");
            this.buttonMoveAll.UseVisualStyleBackColor = true;
            this.buttonMoveAll.Click += new System.EventHandler(this.buttonMoveAll_Click);
            // 
            // buttonMoveSelected
            // 
            this.buttonMoveSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMoveSelected.BackgroundImage = global::Logobot2_0.Properties.Resources.move_selected;
            this.buttonMoveSelected.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMoveSelected.Location = new System.Drawing.Point(261, 231);
            this.buttonMoveSelected.Name = "buttonMoveSelected";
            this.buttonMoveSelected.Size = new System.Drawing.Size(31, 31);
            this.buttonMoveSelected.TabIndex = 14;
            this.toolTipButtonTip.SetToolTip(this.buttonMoveSelected, "Move Selected to Blotter");
            this.buttonMoveSelected.UseVisualStyleBackColor = true;
            this.buttonMoveSelected.Click += new System.EventHandler(this.buttonMoveSelected_Click);
            // 
            // labelScrapeLogos
            // 
            this.labelScrapeLogos.AutoSize = true;
            this.labelScrapeLogos.Location = new System.Drawing.Point(4, 9);
            this.labelScrapeLogos.Name = "labelScrapeLogos";
            this.labelScrapeLogos.Size = new System.Drawing.Size(106, 13);
            this.labelScrapeLogos.TabIndex = 15;
            this.labelScrapeLogos.Text = "Scraped logo results:";
            this.labelScrapeLogos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(224, 266);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(68, 23);
            this.buttonClose.TabIndex = 17;
            this.buttonClose.Text = "Close";
            this.toolTipButtonTip.SetToolTip(this.buttonClose, "Close");
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // ScrapeLogosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(300, 291);
            this.ControlBox = false;
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonMoveSelected);
            this.Controls.Add(this.buttonMoveAll);
            this.Controls.Add(this.labelScrapeLogos);
            this.Controls.Add(this.dataGridViewScrapeLogosBlotter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ScrapeLogosForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ScrapeLogosForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewScrapeLogosBlotter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewScrapeLogosBlotter;
        private System.Windows.Forms.Button buttonMoveAll;
        private System.Windows.Forms.Button buttonMoveSelected;
        private System.Windows.Forms.Label labelScrapeLogos;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.DataGridViewImageColumn ColumnImageBlotter;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDetailsBlotter;
        private System.Windows.Forms.ToolTip toolTipButtonTip;
    }
}