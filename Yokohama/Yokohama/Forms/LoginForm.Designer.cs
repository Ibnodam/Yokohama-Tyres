namespace Yokohama_Tyres.Forms;

partial class LoginForm
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
        label1 = new Label();
        textBox1Login = new TextBox();
        button1Enter = new Button();
        label2 = new Label();
        label3 = new Label();
        textBox2Pswd = new TextBox();
        button1Exit = new Button();
        SuspendLayout();

        label1.AutoSize = true;
        label1.Location = new Point(129, 96);
        label1.Name = "label1";
        label1.Size = new Size(32, 15);
        label1.TabIndex = 0;
        label1.Text = "Вход";

        textBox1Login.Location = new Point(104, 140);
        textBox1Login.Name = "textBox1Login";
        textBox1Login.Size = new Size(130, 23);
        textBox1Login.TabIndex = 1;

        button1Enter.Location = new Point(40, 263);
        button1Enter.Name = "button1Enter";
        button1Enter.Size = new Size(75, 31);
        button1Enter.TabIndex = 2;
        button1Enter.Text = "Войти";
        button1Enter.UseVisualStyleBackColor = true;
        label2.AutoSize = true;
        label2.Location = new Point(40, 143);
        label2.Name = "label2";
        label2.Size = new Size(44, 15);
        label2.TabIndex = 3;
        label2.Text = "Логин:";

        label3.AutoSize = true;
        label3.Location = new Point(40, 181);
        label3.Name = "label3";
        label3.Size = new Size(52, 15);
        label3.TabIndex = 4;
        label3.Text = "Пароль:";

        textBox2Pswd.Location = new Point(104, 181);
        textBox2Pswd.Name = "textBox2Pswd";
        textBox2Pswd.Size = new Size(130, 23);
        textBox2Pswd.TabIndex = 5;

        button1Exit.Location = new Point(159, 263);
        button1Exit.Name = "button1Exit";
        button1Exit.Size = new Size(75, 31);
        button1Exit.TabIndex = 6;
        button1Exit.Text = "Выход";
        button1Exit.UseVisualStyleBackColor = true;

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(279, 339);
        Controls.Add(button1Exit);
        Controls.Add(textBox2Pswd);
        Controls.Add(label3);
        Controls.Add(label2);
        Controls.Add(button1Enter);
        Controls.Add(textBox1Login);
        Controls.Add(label1);
        Name = "LoginForm";
        Text = "Авторизация";
        ResumeLayout(false);
        PerformLayout();
    }



    private Label label1;
    private TextBox textBox1Login;
    private Button button1Enter;
    private Label label2;
    private Label label3;
    private TextBox textBox2Pswd;
    private Button button1Exit;
}

