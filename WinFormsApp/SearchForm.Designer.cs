namespace WinFormsApp
{
    partial class SearchForm
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
            this.label1 = new Label();
            this.txtSearchText = new TextBox();
            this.label2 = new Label();
            this.clbSearchProperties = new CheckedListBox();
            this.btnSearch = new Button();
            this.btnCancel = new Button();
            this.dataGridViewSearchResults = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)this.dataGridViewSearchResults).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(111, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Текст для поиска:";
            // 
            // txtSearchText
            // 
            this.txtSearchText.Location = new Point(129, 12);
            this.txtSearchText.Name = "txtSearchText";
            this.txtSearchText.Size = new Size(200, 23);
            this.txtSearchText.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new Size(130, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Свойства для поиска:";
            // 
            // clbSearchProperties
            // 
            this.clbSearchProperties.FormattingEnabled = true;
            this.clbSearchProperties.Location = new Point(12, 62);
            this.clbSearchProperties.Name = "clbSearchProperties";
            this.clbSearchProperties.Size = new Size(200, 94);
            this.clbSearchProperties.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new Point(12, 162);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new Size(100, 30);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Искать";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += btnSearch_Click;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new Point(118, 162);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(100, 30);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += btnCancel_Click;
            // 
            // dataGridViewSearchResults
            // 
            this.dataGridViewSearchResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSearchResults.Location = new Point(12, 198);
            this.dataGridViewSearchResults.Name = "dataGridViewSearchResults";
            this.dataGridViewSearchResults.RowTemplate.Height = 25;
            this.dataGridViewSearchResults.Size = new Size(776, 240);
            this.dataGridViewSearchResults.TabIndex = 6;
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new Size(7, 15);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 450);
            this.Controls.Add(this.dataGridViewSearchResults);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.clbSearchProperties);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSearchText);
            this.Controls.Add(this.label1);
            this.Name = "SearchForm";
            this.Text = "SearchForm";
            ((System.ComponentModel.ISupportInitialize)this.dataGridViewSearchResults).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtSearchText;
        private Label label2;
        private CheckedListBox clbSearchProperties;
        private Button btnSearch;
        private Button btnCancel;
        private DataGridView dataGridViewSearchResults;
    }
}
