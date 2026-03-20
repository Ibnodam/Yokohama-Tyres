using System;
using System.Drawing;
using System.Windows.Forms;
using Yokohama_Tyres.Repositories;
using System.IO;

namespace Yokohama_Tyres.Forms;

public partial class LoginForm : Form
{
    private TextBox txtUsername;
    private TextBox txtPassword;
    private Button btnLogin;
    private Button btnExit;
    private Label lblTitle;
    private Label lblUsername;
    private Label lblPassword;
    private Panel panelMain;

    public LoginForm()
    {
        InitializeComponent();
        SetupForm();

        string iconPath = Path.Combine(Application.StartupPath, "Resources", "Icons", "app.ico");
        if (File.Exists(iconPath))
        {
            this.Icon = new Icon(iconPath);
        }
    }

    private void SetupForm()
    {
        this.Text = "Авторизация - Yokohama Tyres";
        this.Size = new Size(500, 400);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.BackColor = Color.White;

        panelMain = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(20),
            BackColor = Color.FromArgb(211, 211, 211)
        };

        lblTitle = new Label
        {
            Text = "Вход в систему",
            Font = new Font("Courier New", 16, FontStyle.Bold),
            ForeColor = Color.Black,
            TextAlign = ContentAlignment.MiddleCenter,
            Location = new Point(0, 50),
            Size = new Size(480, 40),
            BackColor = Color.Transparent
        };

        lblUsername = new Label
        {
            Text = "Логин:",
            Font = new Font("Courier New", 10),
            Location = new Point(50, 120),
            Size = new Size(80, 25),
            BackColor = Color.Transparent,
            TextAlign = ContentAlignment.MiddleRight
        };

        txtUsername = new TextBox
        {
            Location = new Point(140, 120),
            Size = new Size(180, 25),
            Font = new Font("Courier New", 10)
        };

        lblPassword = new Label
        {
            Text = "Пароль:",
            Font = new Font("Courier New", 10),
            Location = new Point(50, 160),
            Size = new Size(80, 25),
            BackColor = Color.Transparent,
            TextAlign = ContentAlignment.MiddleRight
        };

        txtPassword = new TextBox
        {
            Location = new Point(140, 160),
            Size = new Size(180, 25),
            Font = new Font("Courier New", 10),
            PasswordChar = '*'
        };

        btnLogin = new Button
        {
            Text = "Войти",
            Location = new Point(140, 220),
            Size = new Size(85, 35),
            Font = new Font("Courier New", 10, FontStyle.Bold),
            BackColor = Color.FromArgb(161, 99, 245),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        btnLogin.FlatAppearance.BorderSize = 0;
        btnLogin.Click += BtnLogin_Click;

        btnExit = new Button
        {
            Text = "Выход",
            Location = new Point(235, 220),
            Size = new Size(85, 35),
            Font = new Font("Courier New", 10),
            BackColor = Color.White,
            ForeColor = Color.Black,
            FlatStyle = FlatStyle.Flat
        };
        btnExit.FlatAppearance.BorderSize = 1;
        btnExit.Click += (s, e) => Application.Exit();

        panelMain.Controls.Add(lblTitle);
        panelMain.Controls.Add(lblUsername);
        panelMain.Controls.Add(txtUsername);
        panelMain.Controls.Add(lblPassword);
        panelMain.Controls.Add(txtPassword);
        panelMain.Controls.Add(btnLogin);
        panelMain.Controls.Add(btnExit);

        this.Controls.Add(panelMain);
        this.AcceptButton = btnLogin;
    }

    private void BtnLogin_Click(object? sender, EventArgs e)
    {
        string username = txtUsername.Text.Trim();
        string password = txtPassword.Text;

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
                txtPassword.Clear();
                txtPassword.Focus();
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
}
