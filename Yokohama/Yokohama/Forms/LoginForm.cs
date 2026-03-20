using System;
using System.Drawing;
using System.Windows.Forms;
using Yokohama_Tyres.Repositories;
using System.IO;

namespace Yokohama_Tyres.Forms;

public partial class LoginForm : Form
{
    public LoginForm()
    {
        InitializeComponent();

       
        SetupForm();

        string iconPath = Path.Combine(Application.StartupPath, "Resources", "Icons", "app.ico");
        if (File.Exists(iconPath))
        {
            this.Icon = new Icon(iconPath);
        }

        this.button1Enter.Click += BtnLogin_Click;
        this.button1Exit.Click += (s, e) => Application.Exit();

        this.AcceptButton = button1Enter;

        this.textBox2Pswd.PasswordChar = '*';

    }

    private void SetupForm()
    {
        this.Text = "Авторизация - Yokohama Tyres";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;

        this.BackColor = Color.White;

        label1.Font = new Font("Courier New", 16, FontStyle.Bold);
        label1.ForeColor = Color.Black;
        label2.Font = new Font("Courier New", 10);
        label2.TextAlign = ContentAlignment.MiddleRight;

        label3.Font = new Font("Courier New", 10);
        label3.TextAlign = ContentAlignment.MiddleRight;

        textBox1Login.Font = new Font("Courier New", 10);

        textBox2Pswd.Font = new Font("Courier New", 10);

        button1Enter.Font = new Font("Courier New", 10, FontStyle.Bold);
        button1Enter.BackColor = Color.FromArgb(161, 99, 245);
        button1Enter.ForeColor = Color.White;
        button1Enter.FlatStyle = FlatStyle.Flat;
        button1Enter.FlatAppearance.BorderSize = 0;

        button1Exit.Font = new Font("Courier New", 10);
        button1Exit.BackColor = Color.White;
        button1Exit.ForeColor = Color.Black;
        button1Exit.FlatStyle = FlatStyle.Flat;
        button1Exit.FlatAppearance.BorderSize = 1;
    }

    private void BtnLogin_Click(object? sender, EventArgs e)
    {
        string username = textBox1Login.Text.Trim();
        string password = textBox2Pswd.Text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            MessageBox.Show("Введите логин и пароль!",
                          "Ошибка",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Warning);
            return;
        }

        try
        {
            var userRepo = new UserRepository();

            if (userRepo.ValidateUser(username, password, out var user))
            {
                var mainForm = new MainForm(user);
                mainForm.FormClosed += (s, args) => this.Close();
                this.Hide();
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!",
                              "Ошибка",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
                textBox2Pswd.Clear();
                textBox2Pswd.Focus();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}",
                          "Ошибка",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
        }
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
    
    }
}
