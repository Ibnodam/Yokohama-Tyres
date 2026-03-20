namespace Yokohama_Tyres.Forms
{
    partial class MainForm
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
            label1 = new Label();
            label2 = new Label();
            labelInfoUser = new Label();
            label3 = new Label();
            textBox1Search = new TextBox();
            label4 = new Label();
            comboBox1Type = new ComboBox();
            dataGridView1A = new DataGridView();
            button1Add = new Button();
            button1Edit = new Button();
            button1Delete = new Button();
            button1Matherials = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1A).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(53, 25);
            label1.Name = "label1";
            label1.Size = new Size(113, 15);
            label1.TabIndex = 0;
            label1.Text = "Каталог продукции";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(53, 53);
            label2.Name = "label2";
            label2.Size = new Size(90, 15);
            label2.TabIndex = 1;
            label2.Text = "Пользователь: ";
            // 
            // labelInfoUser
            // 
            labelInfoUser.AutoSize = true;
            labelInfoUser.Location = new Point(149, 53);
            labelInfoUser.Name = "labelInfoUser";
            labelInfoUser.Size = new Size(68, 15);
            labelInfoUser.TabIndex = 2;
            labelInfoUser.Text = "Не указано";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(286, 25);
            label3.Name = "label3";
            label3.Size = new Size(45, 15);
            label3.TabIndex = 3;
            label3.Text = "Поиск:";
            // 
            // textBox1Search
            // 
            textBox1Search.Location = new Point(337, 22);
            textBox1Search.Name = "textBox1Search";
            textBox1Search.Size = new Size(100, 23);
            textBox1Search.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(286, 53);
            label4.Name = "label4";
            label4.Size = new Size(31, 15);
            label4.TabIndex = 5;
            label4.Text = "Тип:";
            // 
            // comboBox1Type
            // 
            comboBox1Type.FormattingEnabled = true;
            comboBox1Type.Location = new Point(337, 53);
            comboBox1Type.Name = "comboBox1Type";
            comboBox1Type.Size = new Size(121, 23);
            comboBox1Type.TabIndex = 6;
            // 
            // dataGridView1A
            // 
            dataGridView1A.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1A.Location = new Point(53, 100);
            dataGridView1A.Name = "dataGridView1A";
            dataGridView1A.Size = new Size(1110, 412);
            dataGridView1A.TabIndex = 7;
            // 
            // button1Add
            // 
            button1Add.Location = new Point(51, 522);
            button1Add.Name = "button1Add";
            button1Add.Size = new Size(75, 23);
            button1Add.TabIndex = 8;
            button1Add.Text = "Добавить";
            button1Add.UseVisualStyleBackColor = true;
            // 
            // button1Edit
            // 
            button1Edit.Location = new Point(132, 522);
            button1Edit.Name = "button1Edit";
            button1Edit.Size = new Size(101, 23);
            button1Edit.TabIndex = 9;
            button1Edit.Text = "Редактировать";
            button1Edit.UseVisualStyleBackColor = true;
            // 
            // button1Delete
            // 
            button1Delete.Location = new Point(239, 522);
            button1Delete.Name = "button1Delete";
            button1Delete.Size = new Size(75, 23);
            button1Delete.TabIndex = 10;
            button1Delete.Text = "Удалить";
            button1Delete.UseVisualStyleBackColor = true;
            // 
            // button1Matherials
            // 
            button1Matherials.BackColor = Color.FromArgb(192, 0, 192);
            button1Matherials.ForeColor = SystemColors.ControlText;
            button1Matherials.Location = new Point(419, 518);
            button1Matherials.Name = "button1Matherials";
            button1Matherials.Size = new Size(98, 31);
            button1Matherials.TabIndex = 11;
            button1Matherials.Text = "Материалы";
            button1Matherials.UseVisualStyleBackColor = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 567);
            Controls.Add(button1Matherials);
            Controls.Add(button1Delete);
            Controls.Add(button1Edit);
            Controls.Add(button1Add);
            Controls.Add(dataGridView1A);
            Controls.Add(comboBox1Type);
            Controls.Add(label4);
            Controls.Add(textBox1Search);
            Controls.Add(label3);
            Controls.Add(labelInfoUser);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "MainForm";
            Text = "Товары";
            ((System.ComponentModel.ISupportInitialize)dataGridView1A).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label labelInfoUser;
        private Label label3;
        private TextBox textBox1Search;
        private Label label4;
        private ComboBox comboBox1Type;
        private DataGridView dataGridView1A;
        private Button button1Add;
        private Button button1Edit;
        private Button button1Delete;
        private Button button1Matherials;
    }
}