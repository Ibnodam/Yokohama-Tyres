namespace Yokohama_Tyres.Forms
{
    partial class ProductEditForm
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
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            textBox1Name = new TextBox();
            textBox3WorkshopNumber = new TextBox();
            textBox4CountOfPeople = new TextBox();
            textBox6MinPrice = new TextBox();
            textBox7Articul = new TextBox();
            comboBox1Type = new ComboBox();
            pictureBox1 = new PictureBox();
            button1AddPic = new Button();
            button1Save = new Button();
            button2Cancel = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(45, 60);
            label1.Name = "label1";
            label1.Size = new Size(93, 15);
            label1.TabIndex = 0;
            label1.Text = "Наименование:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(79, 100);
            label2.Name = "label2";
            label2.Size = new Size(56, 15);
            label2.TabIndex = 1;
            label2.Text = "Артикул:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(104, 137);
            label3.Name = "label3";
            label3.Size = new Size(31, 15);
            label3.TabIndex = 2;
            label3.Text = "Тип:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(68, 171);
            label4.Name = "label4";
            label4.Size = new Size(67, 15);
            label4.TabIndex = 3;
            label4.Text = "Мин. цена:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(63, 213);
            label5.Name = "label5";
            label5.Size = new Size(72, 15);
            label5.TabIndex = 4;
            label5.Text = "Кол-во чел:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(60, 248);
            label6.Name = "label6";
            label6.Size = new Size(75, 15);
            label6.TabIndex = 5;
            label6.Text = "Номер цеха:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(60, 381);
            label7.Name = "label7";
            label7.Size = new Size(83, 15);
            label7.TabIndex = 6;
            label7.Text = "Изображение";
            // 
            // textBox1Name
            // 
            textBox1Name.Location = new Point(166, 57);
            textBox1Name.Name = "textBox1Name";
            textBox1Name.Size = new Size(121, 23);
            textBox1Name.TabIndex = 7;
            // 
            // textBox3WorkshopNumber
            // 
            textBox3WorkshopNumber.Location = new Point(166, 245);
            textBox3WorkshopNumber.Name = "textBox3WorkshopNumber";
            textBox3WorkshopNumber.Size = new Size(121, 23);
            textBox3WorkshopNumber.TabIndex = 9;
            // 
            // textBox4CountOfPeople
            // 
            textBox4CountOfPeople.Location = new Point(166, 210);
            textBox4CountOfPeople.Name = "textBox4CountOfPeople";
            textBox4CountOfPeople.Size = new Size(121, 23);
            textBox4CountOfPeople.TabIndex = 10;
            // 
            // textBox6MinPrice
            // 
            textBox6MinPrice.Location = new Point(166, 171);
            textBox6MinPrice.Name = "textBox6MinPrice";
            textBox6MinPrice.Size = new Size(121, 23);
            textBox6MinPrice.TabIndex = 12;
            // 
            // textBox7Articul
            // 
            textBox7Articul.Location = new Point(166, 100);
            textBox7Articul.Name = "textBox7Articul";
            textBox7Articul.Size = new Size(121, 23);
            textBox7Articul.TabIndex = 13;
            // 
            // comboBox1Type
            // 
            comboBox1Type.FormattingEnabled = true;
            comboBox1Type.Location = new Point(166, 137);
            comboBox1Type.Name = "comboBox1Type";
            comboBox1Type.Size = new Size(121, 23);
            comboBox1Type.TabIndex = 14;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.ActiveCaption;
            pictureBox1.Cursor = Cursors.No;
            pictureBox1.Location = new Point(166, 293);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(121, 103);
            pictureBox1.TabIndex = 15;
            pictureBox1.TabStop = false;
            // 
            // button1AddPic
            // 
            button1AddPic.Location = new Point(305, 369);
            button1AddPic.Name = "button1AddPic";
            button1AddPic.Size = new Size(93, 31);
            button1AddPic.TabIndex = 16;
            button1AddPic.Text = "Обзор";
            button1AddPic.UseVisualStyleBackColor = true;
            // 
            // button1Save
            // 
            button1Save.Location = new Point(60, 488);
            button1Save.Name = "button1Save";
            button1Save.Size = new Size(93, 31);
            button1Save.TabIndex = 17;
            button1Save.Text = "Сохранить";
            button1Save.UseVisualStyleBackColor = true;
            // 
            // button2Cancel
            // 
            button2Cancel.Location = new Point(166, 488);
            button2Cancel.Name = "button2Cancel";
            button2Cancel.Size = new Size(93, 31);
            button2Cancel.TabIndex = 18;
            button2Cancel.Text = "Отмена";
            button2Cancel.UseVisualStyleBackColor = true;
            // 
            // ProductEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(478, 556);
            Controls.Add(button2Cancel);
            Controls.Add(button1Save);
            Controls.Add(button1AddPic);
            Controls.Add(pictureBox1);
            Controls.Add(comboBox1Type);
            Controls.Add(textBox7Articul);
            Controls.Add(textBox6MinPrice);
            Controls.Add(textBox4CountOfPeople);
            Controls.Add(textBox3WorkshopNumber);
            Controls.Add(textBox1Name);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "ProductEditForm";
            Text = "ProductEditForm";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox textBox1Name;
        private TextBox textBox3WorkshopNumber;
        private TextBox textBox4CountOfPeople;
        private TextBox textBox6MinPrice;
        private TextBox textBox7Articul;
        private ComboBox comboBox1Type;
        private PictureBox pictureBox1;
        private Button button1AddPic;
        private Button button1Save;
        private Button button2Cancel;
    }
}