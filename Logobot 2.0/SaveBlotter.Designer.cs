namespace Logobot2_0
{
    partial class frmSaveBlotter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSaveBlotter));
            this.labelBlotterList = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxSaveBlotterName = new System.Windows.Forms.TextBox();
            this.listBoxSavedBlotterUpdate = new System.Windows.Forms.ListBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.labelSaveBlotterName = new System.Windows.Forms.Label();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelBlotterList
            // 
            this.labelBlotterList.AutoSize = true;
            this.labelBlotterList.Location = new System.Drawing.Point(12, 81);
            this.labelBlotterList.Name = "labelBlotterList";
            this.labelBlotterList.Size = new System.Drawing.Size(225, 13);
            this.labelBlotterList.TabIndex = 0;
            this.labelBlotterList.Text = "Select one of your blotters to update or delete:";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(205, 55);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxSaveBlotterName
            // 
            this.textBoxSaveBlotterName.Location = new System.Drawing.Point(12, 29);
            this.textBoxSaveBlotterName.Name = "textBoxSaveBlotterName";
            this.textBoxSaveBlotterName.Size = new System.Drawing.Size(268, 20);
            this.textBoxSaveBlotterName.TabIndex = 2;
            this.textBoxSaveBlotterName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSaveBlotterName_KeyDown);
            // 
            // listBoxSavedBlotterUpdate
            // 
            this.listBoxSavedBlotterUpdate.FormattingEnabled = true;
            this.listBoxSavedBlotterUpdate.HorizontalScrollbar = true;
            this.listBoxSavedBlotterUpdate.Location = new System.Drawing.Point(12, 100);
            this.listBoxSavedBlotterUpdate.Name = "listBoxSavedBlotterUpdate";
            this.listBoxSavedBlotterUpdate.Size = new System.Drawing.Size(268, 147);
            this.listBoxSavedBlotterUpdate.TabIndex = 3;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(205, 280);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 4;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // labelSaveBlotterName
            // 
            this.labelSaveBlotterName.AutoSize = true;
            this.labelSaveBlotterName.Location = new System.Drawing.Point(12, 9);
            this.labelSaveBlotterName.Name = "labelSaveBlotterName";
            this.labelSaveBlotterName.Size = new System.Drawing.Size(99, 13);
            this.labelSaveBlotterName.TabIndex = 5;
            this.labelSaveBlotterName.Text = "Save Blotter Name:";
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(205, 254);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(75, 23);
            this.buttonUpdate.TabIndex = 6;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(124, 254);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 7;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // frmSaveBlotter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 314);
            this.ControlBox = false;
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.labelSaveBlotterName);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.listBoxSavedBlotterUpdate);
            this.Controls.Add(this.textBoxSaveBlotterName);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.labelBlotterList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSaveBlotter";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmSaveBlotter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelBlotterList;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxSaveBlotterName;
        private System.Windows.Forms.ListBox listBoxSavedBlotterUpdate;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label labelSaveBlotterName;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonDelete;
    }
}