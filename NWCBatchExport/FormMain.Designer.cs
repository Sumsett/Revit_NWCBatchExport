namespace NWCBatchExport
{
    partial class FormMain
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPathRvt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxPathNWC = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_RemovingLinks = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button_openRvtFolder = new System.Windows.Forms.Button();
            this.button_openNwcFolder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 576);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(397, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Экспорт";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 30);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(397, 20);
            this.textBox1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 10);
            this.label2.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Название вида для экспорта";
            // 
            // textBoxPathRvt
            // 
            this.textBoxPathRvt.Location = new System.Drawing.Point(15, 80);
            this.textBoxPathRvt.Name = "textBoxPathRvt";
            this.textBoxPathRvt.Size = new System.Drawing.Size(307, 20);
            this.textBoxPathRvt.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 60);
            this.label3.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Путь до \".rvt\" файлов";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 110);
            this.label4.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Путь до \".nwc\" файлов";
            // 
            // textBoxPathNWC
            // 
            this.textBoxPathNWC.Location = new System.Drawing.Point(15, 130);
            this.textBoxPathNWC.Name = "textBoxPathNWC";
            this.textBoxPathNWC.Size = new System.Drawing.Size(307, 20);
            this.textBoxPathNWC.TabIndex = 8;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(15, 180);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(397, 338);
            this.textBox4.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 160);
            this.label1.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Отчет";
            // 
            // button_RemovingLinks
            // 
            this.button_RemovingLinks.Location = new System.Drawing.Point(15, 547);
            this.button_RemovingLinks.Name = "button_RemovingLinks";
            this.button_RemovingLinks.Size = new System.Drawing.Size(397, 23);
            this.button_RemovingLinks.TabIndex = 12;
            this.button_RemovingLinks.Text = "Удалить все связи в файлах";
            this.button_RemovingLinks.UseVisualStyleBackColor = true;
            this.button_RemovingLinks.Click += new System.EventHandler(this.button_RemovingLinks_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(15, 524);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(203, 17);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "Выгружать геометрию помещений";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button_openRvtFolder
            // 
            this.button_openRvtFolder.Location = new System.Drawing.Point(328, 79);
            this.button_openRvtFolder.Name = "button_openRvtFolder";
            this.button_openRvtFolder.Size = new System.Drawing.Size(84, 23);
            this.button_openRvtFolder.TabIndex = 14;
            this.button_openRvtFolder.Text = "Выбрать";
            this.button_openRvtFolder.UseVisualStyleBackColor = true;
            this.button_openRvtFolder.Click += new System.EventHandler(this.button_openRvtFolder_Click);
            // 
            // button_openNwcFolder
            // 
            this.button_openNwcFolder.Location = new System.Drawing.Point(328, 129);
            this.button_openNwcFolder.Name = "button_openNwcFolder";
            this.button_openNwcFolder.Size = new System.Drawing.Size(84, 23);
            this.button_openNwcFolder.TabIndex = 15;
            this.button_openNwcFolder.Text = "Выбрать";
            this.button_openNwcFolder.UseVisualStyleBackColor = true;
            this.button_openNwcFolder.Click += new System.EventHandler(this.button_openNwcFolder_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 611);
            this.Controls.Add(this.button_openNwcFolder);
            this.Controls.Add(this.button_openRvtFolder);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button_RemovingLinks);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBoxPathNWC);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxPathRvt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Пакетный экспорт NWC";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPathRvt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxPathNWC;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_RemovingLinks;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button_openRvtFolder;
        private System.Windows.Forms.Button button_openNwcFolder;
    }
}