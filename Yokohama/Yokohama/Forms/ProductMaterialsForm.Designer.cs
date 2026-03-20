namespace Yokohama_Tyres.Forms
{
    partial class ProductMaterialsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            label4Pro = new Label();
            label3Arti = new Label();
            label2Ty = new Label();
            comboBox1Matherial = new ComboBox();
            button1Add = new Button();
            numericUpDown1 = new NumericUpDown();
            button1Delete = new Button();
            button1Cancel = new Button();
            dataGridView1M = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1M).BeginInit();
            SuspendLayout();

            label4Pro.AutoSize = true;
            label4Pro.Location = new Point(78, 14);
            label4Pro.Name = "label4Pro";
            label4Pro.Size = new Size(56, 15);
            label4Pro.TabIndex = 0;
            label4Pro.Text = "Продукт:";

            label3Arti.AutoSize = true;
            label3Arti.Location = new Point(78, 39);
            label3Arti.Name = "label3Arti";
            label3Arti.Size = new Size(56, 15);
            label3Arti.TabIndex = 1;
            label3Arti.Text = "Артикул:";

            label2Ty.AutoSize = true;
            label2Ty.Location = new Point(78, 64);
            label2Ty.Name = "label2Ty";
            label2Ty.Size = new Size(31, 15);
            label2Ty.TabIndex = 2;
            label2Ty.Text = "Тип:";

            comboBox1Matherial.FormattingEnabled = true;
            comboBox1Matherial.Location = new Point(12, 95);
            comboBox1Matherial.Name = "comboBox1Matherial";
            comboBox1Matherial.Size = new Size(200, 23);
            comboBox1Matherial.TabIndex = 3;

            button1Add.Location = new Point(330, 95);
            button1Add.Name = "button1Add";
            button1Add.Size = new Size(75, 23);
            button1Add.TabIndex = 4;
            button1Add.Text = "Добавить";
            button1Add.UseVisualStyleBackColor = true;

            numericUpDown1.Location = new Point(218, 95);
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(106, 23);
            numericUpDown1.TabIndex = 5;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });

            button1Delete.Location = new Point(12, 411);
            button1Delete.Name = "button1Delete";
            button1Delete.Size = new Size(111, 23);
            button1Delete.TabIndex = 6;
            button1Delete.Text = "Удалить материал";
            button1Delete.UseVisualStyleBackColor = true;

            button1Cancel.Location = new Point(549, 415);
            button1Cancel.Name = "button1Cancel";
            button1Cancel.Size = new Size(75, 23);
            button1Cancel.TabIndex = 7;
            button1Cancel.Text = "Закрыть";
            button1Cancel.UseVisualStyleBackColor = true;

            dataGridView1M.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1M.Location = new Point(12, 135);
            dataGridView1M.Name = "dataGridView1M";
            dataGridView1M.Size = new Size(612, 270);
            dataGridView1M.TabIndex = 8;

            label1.AutoSize = true;
            label1.Location = new Point(16, 14);
            label1.Name = "label1";
            label1.Size = new Size(56, 15);
            label1.TabIndex = 9;
            label1.Text = "Продукт:";

            label2.AutoSize = true;
            label2.Location = new Point(16, 39);
            label2.Name = "label2";
            label2.Size = new Size(56, 15);
            label2.TabIndex = 10;
            label2.Text = "Артикул:";

            label3.AutoSize = true;
            label3.Location = new Point(16, 64);
            label3.Name = "label3";
            label3.Size = new Size(31, 15);
            label3.TabIndex = 11;
            label3.Text = "Тип:";

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(636, 450);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dataGridView1M);
            Controls.Add(button1Cancel);
            Controls.Add(button1Delete);
            Controls.Add(numericUpDown1);
            Controls.Add(button1Add);
            Controls.Add(comboBox1Matherial);
            Controls.Add(label2Ty);
            Controls.Add(label3Arti);
            Controls.Add(label4Pro);
            Name = "ProductMaterialsForm";
            Text = "ProductMaterialsForm";
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1M).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }



        private Label label4Pro;
        private Label label3Arti;
        private Label label2Ty;
        private ComboBox comboBox1Matherial;
        private Button button1Add;
        private NumericUpDown numericUpDown1;
        private Button button1Delete;
        private Button button1Cancel;
        private DataGridView dataGridView1M;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}