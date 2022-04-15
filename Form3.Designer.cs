
namespace Nonogram
{
    partial class Form3
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
            this.textBox = new System.Windows.Forms.TextBox();
            this.chooseDirectoryButton = new System.Windows.Forms.Button();
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.refreshButton = new System.Windows.Forms.Button();
            this.loadPuzzleButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(20, 20);
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.Size = new System.Drawing.Size(610, 23);
            this.textBox.TabIndex = 0;
            // 
            // chooseDirectoryButton
            // 
            this.chooseDirectoryButton.Location = new System.Drawing.Point(650, 20);
            this.chooseDirectoryButton.Name = "chooseDirectoryButton";
            this.chooseDirectoryButton.Size = new System.Drawing.Size(105, 23);
            this.chooseDirectoryButton.TabIndex = 1;
            this.chooseDirectoryButton.Text = "Choose directory";
            this.chooseDirectoryButton.UseVisualStyleBackColor = true;
            this.chooseDirectoryButton.Click += new System.EventHandler(this.chooseDirectoryButton_Click);
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView.FullRowSelect = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(20, 60);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(735, 305);
            this.listView.TabIndex = 2;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Title";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Width";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Height";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Difficulty";
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(20, 380);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(105, 23);
            this.refreshButton.TabIndex = 3;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // loadPuzzleButton
            // 
            this.loadPuzzleButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.loadPuzzleButton.Location = new System.Drawing.Point(650, 380);
            this.loadPuzzleButton.Name = "loadPuzzleButton";
            this.loadPuzzleButton.Size = new System.Drawing.Size(105, 23);
            this.loadPuzzleButton.TabIndex = 4;
            this.loadPuzzleButton.Text = "Load puzzle";
            this.loadPuzzleButton.UseVisualStyleBackColor = true;
            this.loadPuzzleButton.Click += new System.EventHandler(this.loadPuzzleButton_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 411);
            this.Controls.Add(this.loadPuzzleButton);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.chooseDirectoryButton);
            this.Controls.Add(this.textBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form3";
            this.Text = "Choose one of the puzzles";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button chooseDirectoryButton;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Button loadPuzzleButton;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}