using System;
using System.Drawing;
using System.Windows.Forms;
using Yokohama_Tyres.Repositories;
using Yokohama_Tyres.Models;
using System.Linq;
using System.IO;

namespace Yokohama_Tyres.Forms;

public partial class MainForm : Form
{
    private DataGridView dgvProducts;
    private Panel topPanel;
    private Panel bottomPanel;
    private Button btnAdd;
    private Button btnEdit;
    private Button btnDelete;
    private Button btnMaterials;
    private Button btnRefresh;
    private Button btnExit;
    private TextBox txtSearch;
    private Label lblSearch;
    private ComboBox cmbFilterType;
    private Label lblFilter;
    private Label lblTitle;
    private Label lblUserInfo;
    private ImageList imageList;

    private ProductRepository _productRepo;
    private User? _currentUser;

    public MainForm(User user)
    {
        InitializeComponent();
        _currentUser = user;
        _productRepo = new ProductRepository();
        SetupForm();
        LoadProducts();

        string iconPath = Path.Combine(Application.StartupPath, "Resources", "Icons", "app.ico");
        if (File.Exists(iconPath))
        {
            this.Icon = new Icon(iconPath);
        }

        this.FormClosed += (s, e) => Application.Exit();
    }

    private void SetupForm()
    {
        this.Text = "Управление продукцией - Yokohama Tyres";
        this.Size = new Size(1300, 700);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.BackColor = Color.White;

        topPanel = new Panel
        {
            Dock = DockStyle.Top,
            Height = 100,
            BackColor = Color.FromArgb(211, 211, 211)
        };

        lblTitle = new Label
        {
            Text = "Каталог продукции",
            Font = new Font("Courier New", 20, FontStyle.Bold),
            ForeColor = Color.Black,
            Location = new Point(20, 20),
            Size = new Size(400, 40),
            BackColor = Color.Transparent
        };

        lblUserInfo = new Label
        {
            Text = $"Пользователь: {_currentUser?.FullName} ({_currentUser?.Role})",
            Font = new Font("Courier New", 10),
            ForeColor = Color.Black,
            Location = new Point(20, 60),
            Size = new Size(400, 25),
            BackColor = Color.Transparent
        };

        topPanel.Controls.Add(lblTitle);
        topPanel.Controls.Add(lblUserInfo);

        bottomPanel = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 80,
            BackColor = Color.FromArgb(211, 211, 211)
        };

        Panel searchPanel = new Panel
        {
            Dock = DockStyle.Top,
            Height = 60,
            BackColor = Color.White,
            Padding = new Padding(10)
        };

        lblSearch = new Label
        {
            Text = "Поиск:",
            Font = new Font("Courier New", 10),
            Location = new Point(20, 18),
            Size = new Size(60, 25)
        };

        txtSearch = new TextBox
        {
            Location = new Point(90, 15),
            Size = new Size(250, 25),
            Font = new Font("Courier New", 10)
        };
        txtSearch.TextChanged += TxtSearch_TextChanged;

        lblFilter = new Label
        {
            Text = "Тип:",
            Font = new Font("Courier New", 10),
            Location = new Point(360, 18),
            Size = new Size(50, 25)
        };

        cmbFilterType = new ComboBox
        {
            Location = new Point(420, 15),
            Size = new Size(150, 25),
            Font = new Font("Courier New", 10),
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        cmbFilterType.Items.Add("Все");
        cmbFilterType.Items.Add("Колесо");
        cmbFilterType.Items.Add("Шина");
        cmbFilterType.Items.Add("Диск");
        cmbFilterType.Items.Add("Запаска");
        cmbFilterType.SelectedIndex = 0;
        cmbFilterType.SelectedIndexChanged += CmbFilterType_SelectedIndexChanged;

        searchPanel.Controls.Add(lblSearch);
        searchPanel.Controls.Add(txtSearch);
        searchPanel.Controls.Add(lblFilter);
        searchPanel.Controls.Add(cmbFilterType);

        btnRefresh = new Button
        {
            Text = "Обновить",
            Location = new Point(20, 20),
            Size = new Size(100, 40),
            Font = new Font("Courier New", 10),
            BackColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        btnRefresh.Click += BtnRefresh_Click;

        btnAdd = new Button
        {
            Text = "Добавить",
            Location = new Point(140, 20),
            Size = new Size(100, 40),
            Font = new Font("Courier New", 10, FontStyle.Bold),
            BackColor = Color.FromArgb(161, 99, 245),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        btnAdd.FlatAppearance.BorderSize = 0;
        btnAdd.Click += BtnAdd_Click;

        btnEdit = new Button
        {
            Text = "Редактировать",
            Location = new Point(260, 20),
            Size = new Size(120, 40),
            Font = new Font("Courier New", 10),
            BackColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        btnEdit.Click += BtnEdit_Click;

        btnDelete = new Button
        {
            Text = "Удалить",
            Location = new Point(400, 20),
            Size = new Size(100, 40),
            Font = new Font("Courier New", 10),
            BackColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        btnDelete.Click += BtnDelete_Click;

        btnMaterials = new Button
        {
            Text = "Материалы",
            Location = new Point(520, 20),
            Size = new Size(120, 40),
            Font = new Font("Courier New", 10),
            BackColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        btnMaterials.Click += BtnMaterials_Click;

        btnExit = new Button
        {
            Text = "Выход",
            Location = new Point(1150, 20),
            Size = new Size(100, 40),
            Font = new Font("Courier New", 10),
            BackColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        btnExit.Click += (s, e) => Application.Exit();

        bottomPanel.Controls.Add(btnRefresh);
        bottomPanel.Controls.Add(btnAdd);
        bottomPanel.Controls.Add(btnEdit);
        bottomPanel.Controls.Add(btnDelete);
        bottomPanel.Controls.Add(btnMaterials);
        bottomPanel.Controls.Add(btnExit);

        imageList = new ImageList();
        imageList.ImageSize = new Size(50, 50);
        imageList.ColorDepth = ColorDepth.Depth32Bit;

        dgvProducts = new DataGridView
        {
            Dock = DockStyle.Fill,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            ReadOnly = true,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            BackgroundColor = Color.White,
            Font = new Font("Courier New", 9),
            RowTemplate = { Height = 60 }
        };

        dgvProducts.Columns.Add("ProductId", "ID");
        dgvProducts.Columns.Add("Name", "Наименование");
        dgvProducts.Columns.Add("Article", "Артикул");
        dgvProducts.Columns.Add("ProductType", "Тип");
        dgvProducts.Columns.Add("MinPrice", "Мин. цена");
        dgvProducts.Columns.Add("PeopleCount", "Кол-во чел.");
        dgvProducts.Columns.Add("WorkshopNumber", "Цех");

        DataGridViewImageColumn imageColumn = new DataGridViewImageColumn
        {
            Name = "Image",
            HeaderText = "Фото",
            Width = 80,
            ImageLayout = DataGridViewImageCellLayout.Zoom
        };
        dgvProducts.Columns.Add(imageColumn);

        dgvProducts.Columns["ProductId"].Width = 50;
        dgvProducts.Columns["Name"].Width = 250;
        dgvProducts.Columns["Article"].Width = 100;
        dgvProducts.Columns["ProductType"].Width = 100;
        dgvProducts.Columns["MinPrice"].Width = 100;
        dgvProducts.Columns["PeopleCount"].Width = 80;
        dgvProducts.Columns["WorkshopNumber"].Width = 80;

        this.Controls.Add(dgvProducts);
        this.Controls.Add(searchPanel);
        this.Controls.Add(topPanel);
        this.Controls.Add(bottomPanel);

        if (_currentUser?.Role == "Agent")
        {
            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnMaterials.Enabled = false;
        }
    }

    private void LoadProducts()
    {
        try
        {
            var products = _productRepo.GetAllProducts();
            DisplayProducts(products);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void DisplayProducts(List<Product> products)
    {
        dgvProducts.Rows.Clear();
        imageList.Images.Clear();

        string imagesRoot = Path.Combine(Application.StartupPath, "Resources");

        foreach (var product in products)
        {
            int imageIndex = -1;

            if (!string.IsNullOrEmpty(product.ImagePath) &&
                !product.ImagePath.Contains("нет", StringComparison.OrdinalIgnoreCase) &&
                !product.ImagePath.Contains("не указано", StringComparison.OrdinalIgnoreCase) &&
                !product.ImagePath.Contains("отсутствует", StringComparison.OrdinalIgnoreCase))
            {
                string relativePath = product.ImagePath.TrimStart('\\', '/');
                string fullPath = Path.Combine(imagesRoot, relativePath);
                Console.WriteLine($"ID {product.ProductId} -> {fullPath}");
                Console.WriteLine(File.Exists(fullPath));

                if (File.Exists(fullPath))
                {
                    try
                    {
                        using (var fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            using (var tempImg = Image.FromStream(fs))
                            {
                                var img = new Bitmap(tempImg);
                                imageList.Images.Add(img);
                                imageIndex = imageList.Images.Count - 1;
                            }
                        }
                    }
                    catch
                    {
                        imageIndex = -1;
                    }
                }
            }

            int rowIndex = dgvProducts.Rows.Add(
                product.ProductId,
                product.Name,
                product.Article,
                product.ProductType?.TypeName ?? "Не указан",
                product.MinPriceForAgent?.ToString("N2") + " ₽",
                product.PeopleCount,
                product.WorkshopNumber
            );

            if (imageIndex >= 0)
            {
                dgvProducts.Rows[rowIndex].Cells["Image"].Value = imageList.Images[imageIndex];
            }
        }
    }

    private void TxtSearch_TextChanged(object? sender, EventArgs e)
    {
        FilterProducts();
    }

    private void CmbFilterType_SelectedIndexChanged(object? sender, EventArgs e)
    {
        FilterProducts();
    }

    private void FilterProducts()
    {
        try
        {
            var allProducts = _productRepo.GetAllProducts();
            var searchText = txtSearch.Text.ToLower();
            var filterType = cmbFilterType.SelectedItem?.ToString();

            var filtered = allProducts.Where(p =>
                (string.IsNullOrEmpty(searchText) ||
                 p.Name.ToLower().Contains(searchText) ||
                 p.Article.ToLower().Contains(searchText)) &&
                (filterType == "Все" || filterType == null ||
                 p.ProductType?.TypeName == filterType)
            ).ToList();

            DisplayProducts(filtered);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка фильтрации: {ex.Message}", "Ошибка");
        }
    }

    private void BtnRefresh_Click(object? sender, EventArgs e)
    {
        LoadProducts();
    }

    private void BtnAdd_Click(object? sender, EventArgs e)
    {
        using (var editForm = new ProductEditForm())
        {
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadProducts();
            }
        }
    }

    private void BtnEdit_Click(object? sender, EventArgs e)
    {
        if (dgvProducts.SelectedRows.Count == 0)
        {
            MessageBox.Show("Выберите продукт для редактирования", "Внимание",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var selectedRow = dgvProducts.SelectedRows[0];
        var productId = (int)selectedRow.Cells["ProductId"].Value;

        try
        {
            var product = _productRepo.GetProductById(productId);
            if (product != null)
            {
                using (var editForm = new ProductEditForm(product))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadProducts();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка загрузки продукта: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnDelete_Click(object? sender, EventArgs e)
    {
        if (dgvProducts.SelectedRows.Count == 0)
        {
            MessageBox.Show("Выберите продукт для удаления", "Внимание",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var selectedRow = dgvProducts.SelectedRows[0];
        var productId = (int)selectedRow.Cells["ProductId"].Value;
        var productName = selectedRow.Cells["Name"].Value?.ToString();

        var result = MessageBox.Show($"Удалить продукт \"{productName}\"?",
            "Подтверждение удаления",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
            try
            {
                _productRepo.DeleteProduct(productId);
                LoadProducts();
                MessageBox.Show("Продукт удален", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void BtnMaterials_Click(object? sender, EventArgs e)
    {
        if (dgvProducts.SelectedRows.Count == 0)
        {
            MessageBox.Show("Выберите продукт для просмотра материалов", "Внимание",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var selectedRow = dgvProducts.SelectedRows[0];
        var productId = (int)selectedRow.Cells["ProductId"].Value;

        try
        {
            var product = _productRepo.GetProductById(productId);
            if (product != null)
            {
                using (var materialsForm = new ProductMaterialsForm(product))
                {
                    materialsForm.ShowDialog();
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка загрузки продукта: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        imageList?.Images.Clear();
        imageList?.Dispose();
        base.OnFormClosed(e);
    }
}
















//using System;
//using System.Drawing;
//using System.Windows.Forms;
//using Yokohama_Tyres.Repositories;
//using Yokohama_Tyres.Models;
//using System.Linq;
//using System.IO;

//namespace Yokohama_Tyres.Forms;

//public partial class MainForm : Form
//{
//    private DataGridView dgvProducts;
//    private Panel topPanel;
//    private Panel bottomPanel;
//    private Button btnAdd;
//    private Button btnEdit;
//    private Button btnDelete;
//    private Button btnMaterials;
//    private Button btnRefresh;
//    private Button btnExit;
//    private TextBox txtSearch;
//    private Label lblSearch;
//    private ComboBox cmbFilterType;
//    private Label lblFilter;
//    private Label lblTitle;
//    private Label lblUserInfo;
//    private ImageList imageList;

//    private ProductRepository _productRepo;
//    private User? _currentUser;

//    public MainForm(User user)
//    {
//        InitializeComponent();
//        _currentUser = user;
//        _productRepo = new ProductRepository();
//        SetupForm();
//        LoadProducts();

//        string iconPath = Path.Combine(Application.StartupPath, "Resources", "Icons", "app.ico");
//        if (File.Exists(iconPath))
//        {
//            this.Icon = new Icon(iconPath);
//        }

//        this.FormClosed += (s, e) => Application.Exit();
//    }

//    private void SetupForm()
//    {
//        this.Text = "Управление продукцией - Yokohama Tyres";
//        this.Size = new Size(1300, 700);
//        this.StartPosition = FormStartPosition.CenterScreen;
//        this.BackColor = Color.White;

//        topPanel = new Panel
//        {
//            Dock = DockStyle.Top,
//            Height = 100,
//            BackColor = Color.FromArgb(211, 211, 211)
//        };

//        lblTitle = new Label
//        {
//            Text = "Каталог продукции",
//            Font = new Font("Courier New", 20, FontStyle.Bold),
//            ForeColor = Color.Black,
//            Location = new Point(20, 20),
//            Size = new Size(400, 40),
//            BackColor = Color.Transparent
//        };

//        lblUserInfo = new Label
//        {
//            Text = $"Пользователь: {_currentUser?.FullName} ({_currentUser?.Role})",
//            Font = new Font("Courier New", 10),
//            ForeColor = Color.Black,
//            Location = new Point(20, 60),
//            Size = new Size(400, 25),
//            BackColor = Color.Transparent
//        };

//        topPanel.Controls.Add(lblTitle);
//        topPanel.Controls.Add(lblUserInfo);

//        bottomPanel = new Panel
//        {
//            Dock = DockStyle.Bottom,
//            Height = 80,
//            BackColor = Color.FromArgb(211, 211, 211)
//        };

//        Panel searchPanel = new Panel
//        {
//            Dock = DockStyle.Top,
//            Height = 60,
//            BackColor = Color.White,
//            Padding = new Padding(10)
//        };

//        lblSearch = new Label
//        {
//            Text = "Поиск:",
//            Font = new Font("Courier New", 10),
//            Location = new Point(20, 18),
//            Size = new Size(60, 25)
//        };

//        txtSearch = new TextBox
//        {
//            Location = new Point(90, 15),
//            Size = new Size(250, 25),
//            Font = new Font("Courier New", 10)
//        };
//        txtSearch.TextChanged += TxtSearch_TextChanged;

//        lblFilter = new Label
//        {
//            Text = "Тип:",
//            Font = new Font("Courier New", 10),
//            Location = new Point(360, 18),
//            Size = new Size(50, 25)
//        };

//        cmbFilterType = new ComboBox
//        {
//            Location = new Point(420, 15),
//            Size = new Size(150, 25),
//            Font = new Font("Courier New", 10),
//            DropDownStyle = ComboBoxStyle.DropDownList
//        };
//        cmbFilterType.Items.Add("Все");
//        cmbFilterType.Items.Add("Колесо");
//        cmbFilterType.Items.Add("Шина");
//        cmbFilterType.Items.Add("Диск");
//        cmbFilterType.Items.Add("Запаска");
//        cmbFilterType.SelectedIndex = 0;
//        cmbFilterType.SelectedIndexChanged += CmbFilterType_SelectedIndexChanged;

//        searchPanel.Controls.Add(lblSearch);
//        searchPanel.Controls.Add(txtSearch);
//        searchPanel.Controls.Add(lblFilter);
//        searchPanel.Controls.Add(cmbFilterType);

//        btnRefresh = new Button
//        {
//            Text = "Обновить",
//            Location = new Point(20, 20),
//            Size = new Size(100, 40),
//            Font = new Font("Courier New", 10),
//            BackColor = Color.White,
//            FlatStyle = FlatStyle.Flat
//        };
//        btnRefresh.Click += BtnRefresh_Click;

//        btnAdd = new Button
//        {
//            Text = "Добавить",
//            Location = new Point(140, 20),
//            Size = new Size(100, 40),
//            Font = new Font("Courier New", 10, FontStyle.Bold),
//            BackColor = Color.FromArgb(161, 99, 245),
//            ForeColor = Color.White,
//            FlatStyle = FlatStyle.Flat
//        };
//        btnAdd.FlatAppearance.BorderSize = 0;
//        btnAdd.Click += BtnAdd_Click;

//        btnEdit = new Button
//        {
//            Text = "Редактировать",
//            Location = new Point(260, 20),
//            Size = new Size(120, 40),
//            Font = new Font("Courier New", 10),
//            BackColor = Color.White,
//            FlatStyle = FlatStyle.Flat
//        };
//        btnEdit.Click += BtnEdit_Click;

//        btnDelete = new Button
//        {
//            Text = "Удалить",
//            Location = new Point(400, 20),
//            Size = new Size(100, 40),
//            Font = new Font("Courier New", 10),
//            BackColor = Color.White,
//            FlatStyle = FlatStyle.Flat
//        };
//        btnDelete.Click += BtnDelete_Click;

//        btnMaterials = new Button
//        {
//            Text = "Материалы",
//            Location = new Point(520, 20),
//            Size = new Size(120, 40),
//            Font = new Font("Courier New", 10),
//            BackColor = Color.White,
//            FlatStyle = FlatStyle.Flat
//        };
//        btnMaterials.Click += BtnMaterials_Click;

//        btnExit = new Button
//        {
//            Text = "Выход",
//            Location = new Point(1150, 20),
//            Size = new Size(100, 40),
//            Font = new Font("Courier New", 10),
//            BackColor = Color.White,
//            FlatStyle = FlatStyle.Flat
//        };
//        btnExit.Click += (s, e) => Application.Exit();

//        bottomPanel.Controls.Add(btnRefresh);
//        bottomPanel.Controls.Add(btnAdd);
//        bottomPanel.Controls.Add(btnEdit);
//        bottomPanel.Controls.Add(btnDelete);
//        bottomPanel.Controls.Add(btnMaterials);
//        bottomPanel.Controls.Add(btnExit);

//        imageList = new ImageList();
//        imageList.ImageSize = new Size(50, 50);
//        imageList.ColorDepth = ColorDepth.Depth32Bit;

//        dgvProducts = new DataGridView
//        {
//            Dock = DockStyle.Fill,
//            AllowUserToAddRows = false,
//            AllowUserToDeleteRows = false,
//            ReadOnly = true,
//            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
//            MultiSelect = false,
//            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
//            BackgroundColor = Color.White,
//            Font = new Font("Courier New", 9),
//            RowTemplate = { Height = 60 }
//        };

//        dgvProducts.Columns.Add("ProductId", "ID");
//        dgvProducts.Columns.Add("Name", "Наименование");
//        dgvProducts.Columns.Add("Article", "Артикул");
//        dgvProducts.Columns.Add("ProductType", "Тип");
//        dgvProducts.Columns.Add("MinPrice", "Мин. цена");
//        dgvProducts.Columns.Add("PeopleCount", "Кол-во чел.");
//        dgvProducts.Columns.Add("WorkshopNumber", "Цех");

//        DataGridViewImageColumn imageColumn = new DataGridViewImageColumn
//        {
//            Name = "Image",
//            HeaderText = "Фото",
//            Width = 80,
//            ImageLayout = DataGridViewImageCellLayout.Zoom
//        };
//        dgvProducts.Columns.Add(imageColumn);

//        dgvProducts.Columns["ProductId"].Width = 50;
//        dgvProducts.Columns["Name"].Width = 250;
//        dgvProducts.Columns["Article"].Width = 100;
//        dgvProducts.Columns["ProductType"].Width = 100;
//        dgvProducts.Columns["MinPrice"].Width = 100;
//        dgvProducts.Columns["PeopleCount"].Width = 80;
//        dgvProducts.Columns["WorkshopNumber"].Width = 80;

//        this.Controls.Add(dgvProducts);
//        this.Controls.Add(searchPanel);
//        this.Controls.Add(topPanel);
//        this.Controls.Add(bottomPanel);

//        if (_currentUser?.Role == "Agent")
//        {
//            btnAdd.Enabled = false;
//            btnEdit.Enabled = false;
//            btnDelete.Enabled = false;
//            btnMaterials.Enabled = false;
//        }
//    }

//    private void LoadProducts()
//    {
//        try
//        {
//            var products = _productRepo.GetAllProducts();
//            DisplayProducts(products);
//        }
//        catch (Exception ex)
//        {
//            MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
//                MessageBoxButtons.OK, MessageBoxIcon.Error);
//        }
//    }

//    private void DisplayProducts(List<Product> products)
//    {
//        dgvProducts.Rows.Clear();
//        imageList.Images.Clear();

//        // 🔧 Базовая папка: bin/Debug/net8.0-windows/Resources
//        string imagesRoot = Path.Combine(Application.StartupPath, "Resources");

//        foreach (var product in products)
//        {
//            int imageIndex = -1;

//            // Проверяем, что путь не пустой и не содержит "заглушек"
//            if (!string.IsNullOrEmpty(product.ImagePath) &&
//                !product.ImagePath.Contains("нет", StringComparison.OrdinalIgnoreCase) &&
//                !product.ImagePath.Contains("не указано", StringComparison.OrdinalIgnoreCase) &&
//                !product.ImagePath.Contains("отсутствует", StringComparison.OrdinalIgnoreCase))
//            {
//                // 🔧 Убираем начальные \ или / и добавляем Resources
//                string relativePath = product.ImagePath.TrimStart('\\', '/');
//                string fullPath = Path.Combine(imagesRoot, relativePath);
//                Console.WriteLine($"ID {product.ProductId} -> {fullPath}");
//                Console.WriteLine(File.Exists(fullPath));

//                if (File.Exists(fullPath))
//                {
//                    try
//                    {
//                        // 🔧 Клонирование через Bitmap — изображение становится независимым от файла
//                        using (var fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read))
//                        {
//                            using (var tempImg = Image.FromStream(fs))
//                            {
//                                var img = new Bitmap(tempImg);
//                                imageList.Images.Add(img);
//                                imageIndex = imageList.Images.Count - 1;
//                            }
//                        }
//                    }
//                    catch
//                    {
//                        // Тихо игнорируем ошибки — просто не показываем картинку
//                        imageIndex = -1;
//                    }
//                }
//                // 🔥 Никаких MessageBox! Если файла нет — просто пропускаем
//            }

//            // Заполняем строку данными
//            int rowIndex = dgvProducts.Rows.Add(
//                product.ProductId,
//                product.Name,
//                product.Article,
//                product.ProductType?.TypeName ?? "Не указан",
//                product.MinPriceForAgent?.ToString("N2") + " ₽",
//                product.PeopleCount,
//                product.WorkshopNumber
//            );

//            // Если изображение загрузилось — ставим его в ячейку
//            if (imageIndex >= 0)
//            {
//                dgvProducts.Rows[rowIndex].Cells["Image"].Value = imageList.Images[imageIndex];
//            }
//            // Иначе ячейка останется пустой (без ошибок и крестиков)
//        }
//    }

//    //private void DisplayProducts(List<Product> products)
//    //{
//    //    dgvProducts.Rows.Clear();
//    //    imageList.Images.Clear();

//    //    foreach (var product in products)
//    //    {
//    //        int imageIndex = -1;

//    //        if (!string.IsNullOrEmpty(product.ImagePath) &&
//    //            !product.ImagePath.Contains("нет") &&
//    //            !product.ImagePath.Contains("не указано") &&
//    //            !product.ImagePath.Contains("отсутствует"))
//    //        {
//    //            string fullPath = product.ImagePath;
//    //            if (!Path.IsPathRooted(product.ImagePath))
//    //            {
//    //                fullPath = Path.Combine(Application.StartupPath, product.ImagePath);
//    //            }

//    //            if (File.Exists(fullPath))
//    //            {
//    //                try
//    //                {
//    //                    using (var fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
//    //                    {
//    //                        var img = Image.FromStream(fs);
//    //                        imageList.Images.Add(img);
//    //                        imageIndex = imageList.Images.Count - 1;
//    //                    }
//    //                }
//    //                catch
//    //                {
//    //                    imageIndex = -1;
//    //                }
//    //            }
//    //        }

//    //        dgvProducts.Rows.Add(
//    //            product.ProductId,
//    //            product.Name,
//    //            product.Article,
//    //            product.ProductType?.TypeName ?? "Не указан",
//    //            product.MinPriceForAgent?.ToString("N2") + " ₽",
//    //            product.PeopleCount,
//    //            product.WorkshopNumber
//    //        );

//    //        if (imageIndex >= 0)
//    //        {
//    //            dgvProducts.Rows[dgvProducts.Rows.Count - 1].Cells["Image"].Value = imageList.Images[imageIndex];
//    //        }
//    //    }
//    //}

//    private void TxtSearch_TextChanged(object? sender, EventArgs e)
//    {
//        FilterProducts();
//    }

//    private void CmbFilterType_SelectedIndexChanged(object? sender, EventArgs e)
//    {
//        FilterProducts();
//    }

//    private void FilterProducts()
//    {
//        try
//        {
//            var allProducts = _productRepo.GetAllProducts();
//            var searchText = txtSearch.Text.ToLower();
//            var filterType = cmbFilterType.SelectedItem?.ToString();

//            var filtered = allProducts.Where(p =>
//                (string.IsNullOrEmpty(searchText) ||
//                 p.Name.ToLower().Contains(searchText) ||
//                 p.Article.ToLower().Contains(searchText)) &&
//                (filterType == "Все" || filterType == null ||
//                 p.ProductType?.TypeName == filterType)
//            ).ToList();

//            DisplayProducts(filtered);
//        }
//        catch (Exception ex)
//        {
//            MessageBox.Show($"Ошибка фильтрации: {ex.Message}", "Ошибка");
//        }
//    }

//    private void BtnRefresh_Click(object? sender, EventArgs e)
//    {
//        LoadProducts();
//    }

//    private void BtnAdd_Click(object? sender, EventArgs e)
//    {
//        using (var editForm = new ProductEditForm())
//        {
//            if (editForm.ShowDialog() == DialogResult.OK)
//            {
//                LoadProducts();
//            }
//        }
//    }

//    private void BtnEdit_Click(object? sender, EventArgs e)
//    {
//        if (dgvProducts.SelectedRows.Count == 0)
//        {
//            MessageBox.Show("Выберите продукт для редактирования", "Внимание",
//                MessageBoxButtons.OK, MessageBoxIcon.Warning);
//            return;
//        }

//        var selectedRow = dgvProducts.SelectedRows[0];
//        var productId = (int)selectedRow.Cells["ProductId"].Value;

//        try
//        {
//            var product = _productRepo.GetProductById(productId);
//            if (product != null)
//            {
//                using (var editForm = new ProductEditForm(product))
//                {
//                    if (editForm.ShowDialog() == DialogResult.OK)
//                    {
//                        LoadProducts();
//                    }
//                }
//            }
//        }
//        catch (Exception ex)
//        {
//            MessageBox.Show($"Ошибка загрузки продукта: {ex.Message}", "Ошибка",
//                MessageBoxButtons.OK, MessageBoxIcon.Error);
//        }
//    }

//    private void BtnDelete_Click(object? sender, EventArgs e)
//    {
//        if (dgvProducts.SelectedRows.Count == 0)
//        {
//            MessageBox.Show("Выберите продукт для удаления", "Внимание",
//                MessageBoxButtons.OK, MessageBoxIcon.Warning);
//            return;
//        }

//        var selectedRow = dgvProducts.SelectedRows[0];
//        var productId = (int)selectedRow.Cells["ProductId"].Value;
//        var productName = selectedRow.Cells["Name"].Value?.ToString();

//        var result = MessageBox.Show($"Удалить продукт \"{productName}\"?",
//            "Подтверждение удаления",
//            MessageBoxButtons.YesNo,
//            MessageBoxIcon.Question);

//        if (result == DialogResult.Yes)
//        {
//            try
//            {
//                _productRepo.DeleteProduct(productId);
//                LoadProducts();
//                MessageBox.Show("Продукт удален", "Успех",
//                    MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка",
//                    MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//    }

//    private void BtnMaterials_Click(object? sender, EventArgs e)
//    {
//        if (dgvProducts.SelectedRows.Count == 0)
//        {
//            MessageBox.Show("Выберите продукт для просмотра материалов", "Внимание",
//                MessageBoxButtons.OK, MessageBoxIcon.Warning);
//            return;
//        }

//        var selectedRow = dgvProducts.SelectedRows[0];
//        var productId = (int)selectedRow.Cells["ProductId"].Value;

//        try
//        {
//            var product = _productRepo.GetProductById(productId);
//            if (product != null)
//            {
//                using (var materialsForm = new ProductMaterialsForm(product))
//                {
//                    materialsForm.ShowDialog();
//                }
//            }
//        }
//        catch (Exception ex)
//        {
//            MessageBox.Show($"Ошибка загрузки продукта: {ex.Message}", "Ошибка",
//                MessageBoxButtons.OK, MessageBoxIcon.Error);
//        }
//    }

//    protected override void OnFormClosed(FormClosedEventArgs e)
//    {
//        imageList?.Images.Clear();
//        imageList?.Dispose();
//        base.OnFormClosed(e);
//    }
//}


















////using System;
////using System.Drawing;
////using System.Windows.Forms;
////using Yokohama_Tyres.Repositories;
////using Yokohama_Tyres.Models;
////using System.Linq;

////namespace Yokohama_Tyres.Forms;

////public partial class MainForm : Form
////{
////    // Элементы управления
////    private DataGridView dgvProducts;
////    private Panel topPanel;
////    private Panel bottomPanel;
////    private Button btnAdd;
////    private Button btnEdit;
////    private Button btnDelete;
////    private Button btnMaterials;
////    private Button btnRefresh;
////    private Button btnExit;
////    private TextBox txtSearch;
////    private Label lblSearch;
////    private ComboBox cmbFilterType;
////    private Label lblFilter;
////    private Label lblTitle;
////    private Label lblUserInfo;

////    // Репозитории
////    private ProductRepository _productRepo;
////    private User? _currentUser;



////    public MainForm(User user)
////    {
////        InitializeComponent();
////        _currentUser = user;
////        _productRepo = new ProductRepository();
////        SetupForm();
////        LoadProducts();

////        string iconPath = Path.Combine(Application.StartupPath, "Resources", "Icons", "app.ico");
////        if (File.Exists(iconPath))
////        {
////            this.Icon = new Icon(iconPath);
////        }

////        this.FormClosed += (s, e) => Application.Exit();
////    }

////    private void SetupForm()
////    {
////        // Настройки формы
////        this.Text = "Управление продукцией - Yokohama Tyres";
////        this.Size = new Size(1200, 700);
////        this.StartPosition = FormStartPosition.CenterScreen;
////        this.BackColor = Color.White;

////        // Верхняя панель с заголовком
////        topPanel = new Panel
////        {
////            Dock = DockStyle.Top,
////            Height = 100,
////            BackColor = Color.FromArgb(211, 211, 211) // #D3D3D3
////        };

////        // Заголовок
////        lblTitle = new Label
////        {
////            Text = "Каталог продукции",
////            Font = new Font("Courier New", 20, FontStyle.Bold),
////            ForeColor = Color.Black,
////            Location = new Point(20, 20),
////            Size = new Size(400, 40),
////            BackColor = Color.Transparent
////        };

////        // Информация о пользователе
////        lblUserInfo = new Label
////        {
////            Text = $"Пользователь: {_currentUser?.FullName} ({_currentUser?.Role})",
////            Font = new Font("Courier New", 10),
////            ForeColor = Color.Black,
////            Location = new Point(20, 60),
////            Size = new Size(400, 25),
////            BackColor = Color.Transparent
////        };

////        topPanel.Controls.Add(lblTitle);
////        topPanel.Controls.Add(lblUserInfo);

////        // Нижняя панель с кнопками
////        bottomPanel = new Panel
////        {
////            Dock = DockStyle.Bottom,
////            Height = 80,
////            BackColor = Color.FromArgb(211, 211, 211) // #D3D3D3
////        };

////        // Панель поиска и фильтрации (под верхней панелью)
////        Panel searchPanel = new Panel
////        {
////            Dock = DockStyle.Top,
////            Height = 60,
////            BackColor = Color.White,
////            Padding = new Padding(10)
////        };

////        lblSearch = new Label
////        {
////            Text = "Поиск:",
////            Font = new Font("Courier New", 10),
////            Location = new Point(20, 18),
////            Size = new Size(60, 25)
////        };

////        txtSearch = new TextBox
////        {
////            Location = new Point(90, 15),
////            Size = new Size(250, 25),
////            Font = new Font("Courier New", 10)
////        };
////        txtSearch.TextChanged += TxtSearch_TextChanged;

////        lblFilter = new Label
////        {
////            Text = "Тип:",
////            Font = new Font("Courier New", 10),
////            Location = new Point(360, 18),
////            Size = new Size(50, 25)
////        };

////        cmbFilterType = new ComboBox
////        {
////            Location = new Point(420, 15),
////            Size = new Size(150, 25),
////            Font = new Font("Courier New", 10),
////            DropDownStyle = ComboBoxStyle.DropDownList
////        };
////        cmbFilterType.Items.Add("Все");
////        cmbFilterType.Items.Add("Колесо");
////        cmbFilterType.Items.Add("Шина");
////        cmbFilterType.Items.Add("Диск");
////        cmbFilterType.Items.Add("Запаска");
////        cmbFilterType.SelectedIndex = 0;
////        cmbFilterType.SelectedIndexChanged += CmbFilterType_SelectedIndexChanged;

////        searchPanel.Controls.Add(lblSearch);
////        searchPanel.Controls.Add(txtSearch);
////        searchPanel.Controls.Add(lblFilter);
////        searchPanel.Controls.Add(cmbFilterType);

////        // Кнопки в нижней панели
////        btnRefresh = new Button
////        {
////            Text = "Обновить",
////            Location = new Point(20, 20),
////            Size = new Size(100, 40),
////            Font = new Font("Courier New", 10),
////            BackColor = Color.White,
////            FlatStyle = FlatStyle.Flat
////        };
////        btnRefresh.Click += BtnRefresh_Click;

////        btnAdd = new Button
////        {
////            Text = "Добавить",
////            Location = new Point(140, 20),
////            Size = new Size(100, 40),
////            Font = new Font("Courier New", 10, FontStyle.Bold),
////            BackColor = Color.FromArgb(161, 99, 245), // #A163F5
////            ForeColor = Color.White,
////            FlatStyle = FlatStyle.Flat
////        };
////        btnAdd.FlatAppearance.BorderSize = 0;
////        btnAdd.Click += BtnAdd_Click;

////        btnEdit = new Button
////        {
////            Text = "Редактировать",
////            Location = new Point(260, 20),
////            Size = new Size(120, 40),
////            Font = new Font("Courier New", 10),
////            BackColor = Color.White,
////            FlatStyle = FlatStyle.Flat
////        };
////        btnEdit.Click += BtnEdit_Click;

////        btnDelete = new Button
////        {
////            Text = "Удалить",
////            Location = new Point(400, 20),
////            Size = new Size(100, 40),
////            Font = new Font("Courier New", 10),
////            BackColor = Color.White,
////            FlatStyle = FlatStyle.Flat
////        };
////        btnDelete.Click += BtnDelete_Click;

////        btnMaterials = new Button
////        {
////            Text = "Материалы",
////            Location = new Point(520, 20),
////            Size = new Size(120, 40),
////            Font = new Font("Courier New", 10),
////            BackColor = Color.White,
////            FlatStyle = FlatStyle.Flat
////        };
////        btnMaterials.Click += BtnMaterials_Click;

////        btnExit = new Button
////        {
////            Text = "Выход",
////            Location = new Point(1050, 20),
////            Size = new Size(100, 40),
////            Font = new Font("Courier New", 10),
////            BackColor = Color.White,
////            FlatStyle = FlatStyle.Flat
////        };
////        btnExit.Click += (s, e) => Application.Exit();

////        bottomPanel.Controls.Add(btnRefresh);
////        bottomPanel.Controls.Add(btnAdd);
////        bottomPanel.Controls.Add(btnEdit);
////        bottomPanel.Controls.Add(btnDelete);
////        bottomPanel.Controls.Add(btnMaterials);
////        bottomPanel.Controls.Add(btnExit);

////        // DataGridView для списка продукции
////        dgvProducts = new DataGridView
////        {
////            Dock = DockStyle.Fill,
////            AllowUserToAddRows = false,
////            AllowUserToDeleteRows = false,
////            ReadOnly = true,
////            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
////            MultiSelect = false,
////            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
////            BackgroundColor = Color.White,
////            Font = new Font("Courier New", 9)
////        };

////        // Настройка столбцов
////        dgvProducts.Columns.Add("ProductId", "ID");
////        dgvProducts.Columns.Add("Name", "Наименование");
////        dgvProducts.Columns.Add("Article", "Артикул");
////        dgvProducts.Columns.Add("ProductType", "Тип");
////        dgvProducts.Columns.Add("MinPrice", "Мин. цена");
////        dgvProducts.Columns.Add("PeopleCount", "Кол-во чел.");
////        dgvProducts.Columns.Add("WorkshopNumber", "Цех");

////        // Настройка ширины столбцов
////        dgvProducts.Columns["ProductId"].Width = 50;
////        dgvProducts.Columns["Name"].Width = 300;
////        dgvProducts.Columns["Article"].Width = 100;
////        dgvProducts.Columns["ProductType"].Width = 100;
////        dgvProducts.Columns["MinPrice"].Width = 100;
////        dgvProducts.Columns["PeopleCount"].Width = 80;
////        dgvProducts.Columns["WorkshopNumber"].Width = 80;

////        // Добавляем все на форму
////        this.Controls.Add(dgvProducts);
////        this.Controls.Add(searchPanel);
////        this.Controls.Add(topPanel);
////        this.Controls.Add(bottomPanel);

////        // Ограничения по ролям
////        if (_currentUser?.Role == "Agent")
////        {
////            // Агент может только смотреть
////            btnAdd.Enabled = false;
////            btnEdit.Enabled = false;
////            btnDelete.Enabled = false;
////            btnMaterials.Enabled = false;
////        }


////    }

////    private void LoadProducts()
////    {
////        try
////        {
////            var products = _productRepo.GetAllProducts();
////            DisplayProducts(products);
////        }
////        catch (Exception ex)
////        {
////            MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
////                MessageBoxButtons.OK, MessageBoxIcon.Error);
////        }
////    }

////    private void DisplayProducts(List<Product> products)
////    {
////        dgvProducts.Rows.Clear();

////        foreach (var product in products)
////        {
////            dgvProducts.Rows.Add(
////                product.ProductId,
////                product.Name,
////                product.Article,
////                product.ProductType?.TypeName ?? "Не указан",
////                product.MinPriceForAgent?.ToString("N2") + " ₽",
////                product.PeopleCount,
////                product.WorkshopNumber
////            );
////        }
////    }

////    private void TxtSearch_TextChanged(object? sender, EventArgs e)
////    {
////        FilterProducts();
////    }

////    private void CmbFilterType_SelectedIndexChanged(object? sender, EventArgs e)
////    {
////        FilterProducts();
////    }

////    private void FilterProducts()
////    {
////        try
////        {
////            var allProducts = _productRepo.GetAllProducts();
////            var searchText = txtSearch.Text.ToLower();
////            var filterType = cmbFilterType.SelectedItem?.ToString();

////            var filtered = allProducts.Where(p =>
////                (string.IsNullOrEmpty(searchText) ||
////                 p.Name.ToLower().Contains(searchText) ||
////                 p.Article.ToLower().Contains(searchText)) &&
////                (filterType == "Все" || filterType == null ||
////                 p.ProductType?.TypeName == filterType)
////            ).ToList();

////            DisplayProducts(filtered);
////        }
////        catch (Exception ex)
////        {
////            MessageBox.Show($"Ошибка фильтрации: {ex.Message}", "Ошибка");
////        }
////    }

////    private void BtnRefresh_Click(object? sender, EventArgs e)
////    {
////        LoadProducts();
////    }

////    private void BtnAdd_Click(object? sender, EventArgs e)
////    {
////        using (var editForm = new ProductEditForm())
////        {
////            if (editForm.ShowDialog() == DialogResult.OK)
////            {
////                LoadProducts(); // Перезагружаем список
////            }
////        }
////    }

////    private void BtnEdit_Click(object? sender, EventArgs e)
////    {
////        if (dgvProducts.SelectedRows.Count == 0)
////        {
////            MessageBox.Show("Выберите продукт для редактирования", "Внимание",
////                MessageBoxButtons.OK, MessageBoxIcon.Warning);
////            return;
////        }

////        var selectedRow = dgvProducts.SelectedRows[0];
////        var productId = (int)selectedRow.Cells["ProductId"].Value;

////        try
////        {
////            var product = _productRepo.GetProductById(productId);
////            if (product != null)
////            {
////                using (var editForm = new ProductEditForm(product))
////                {
////                    if (editForm.ShowDialog() == DialogResult.OK)
////                    {
////                        LoadProducts(); // Перезагружаем список
////                    }
////                }
////            }
////        }
////        catch (Exception ex)
////        {
////            MessageBox.Show($"Ошибка загрузки продукта: {ex.Message}", "Ошибка",
////                MessageBoxButtons.OK, MessageBoxIcon.Error);
////        }
////    }

////    private void BtnDelete_Click(object? sender, EventArgs e)
////    {
////        if (dgvProducts.SelectedRows.Count == 0)
////        {
////            MessageBox.Show("Выберите продукт для удаления", "Внимание",
////                MessageBoxButtons.OK, MessageBoxIcon.Warning);
////            return;
////        }

////        var selectedRow = dgvProducts.SelectedRows[0];
////        var productId = (int)selectedRow.Cells["ProductId"].Value;
////        var productName = selectedRow.Cells["Name"].Value?.ToString();

////        var result = MessageBox.Show($"Удалить продукт \"{productName}\"?",
////            "Подтверждение удаления",
////            MessageBoxButtons.YesNo,
////            MessageBoxIcon.Question);

////        if (result == DialogResult.Yes)
////        {
////            try
////            {
////                _productRepo.DeleteProduct(productId);
////                LoadProducts();
////                MessageBox.Show("Продукт удален", "Успех",
////                    MessageBoxButtons.OK, MessageBoxIcon.Information);
////            }
////            catch (Exception ex)
////            {
////                MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка",
////                    MessageBoxButtons.OK, MessageBoxIcon.Error);
////            }
////        }
////    }

////    private void BtnMaterials_Click(object? sender, EventArgs e)
////    {
////        if (dgvProducts.SelectedRows.Count == 0)
////        {
////            MessageBox.Show("Выберите продукт для просмотра материалов", "Внимание",
////                MessageBoxButtons.OK, MessageBoxIcon.Warning);
////            return;
////        }

////        var selectedRow = dgvProducts.SelectedRows[0];
////        var productId = (int)selectedRow.Cells["ProductId"].Value;

////        try
////        {
////            var product = _productRepo.GetProductById(productId);
////            if (product != null)
////            {
////                using (var materialsForm = new ProductMaterialsForm(product))
////                {
////                    materialsForm.ShowDialog();
////                }
////            }
////        }
////        catch (Exception ex)
////        {
////            MessageBox.Show($"Ошибка загрузки продукта: {ex.Message}", "Ошибка",
////                MessageBoxButtons.OK, MessageBoxIcon.Error);
////        }
////    }
////}