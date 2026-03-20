using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Yokohama_Tyres.Repositories;
using Yokohama.Models;
namespace Yokohama_Tyres.Forms;

public partial class MainForm : Form
{

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

        this.button1Add.Click += BtnAdd_Click;
        this.button1Edit.Click += BtnEdit_Click;
        this.button1Delete.Click += BtnDelete_Click;
        this.button1Matherials.Click += BtnMaterials_Click;
        this.textBox1Search.TextChanged += TxtSearch_TextChanged;
        this.comboBox1Type.SelectedIndexChanged += CmbFilterType_SelectedIndexChanged;

        this.FormClosed += (s, e) => Application.Exit();
    }

    private void SetupForm()
    {
   
        this.Text = "Управление продукцией - Yokohama Tyres";
        this.Size = new Size(1300, 700);
        this.StartPosition = FormStartPosition.CenterScreen;

  
        label1.Font = new Font("Courier New", 20, FontStyle.Bold);
        label1.ForeColor = Color.Black;
        label1.Location = new Point(20, 20);
        label1.Size = new Size(400, 40);
        label1.Text = "Каталог продукции";

        label2.Text = "Пользователь:";
        label2.Font = new Font("Courier New", 10);
        label2.Location = new Point(20, 60);

        labelInfoUser.Text = $"{_currentUser?.FullName} ({_currentUser?.Role})";
        labelInfoUser.Font = new Font("Courier New", 10);
        labelInfoUser.Location = new Point(120, 60);
        labelInfoUser.Size = new Size(300, 25);
        labelInfoUser.ForeColor = Color.Black;

        label3.Text = "Поиск:";
        label3.Font = new Font("Courier New", 10);
        label3.Location = new Point(600, 25);

        textBox1Search.Font = new Font("Courier New", 10);
        textBox1Search.Size = new Size(200, 25);
        textBox1Search.Location = new Point(660, 22);

        label4.Text = "Тип:";
        label4.Font = new Font("Courier New", 10);
        label4.Location = new Point(600, 55);

        comboBox1Type.Font = new Font("Courier New", 10);
        comboBox1Type.DropDownStyle = ComboBoxStyle.DropDownList;
        comboBox1Type.Location = new Point(660, 52);
        comboBox1Type.Size = new Size(150, 25);
        comboBox1Type.Items.Clear();
        comboBox1Type.Items.Add("Все");
        comboBox1Type.Items.Add("Колесо");
        comboBox1Type.Items.Add("Шина");
        comboBox1Type.Items.Add("Диск");
        comboBox1Type.Items.Add("Запаска");
        comboBox1Type.SelectedIndex = 0;

        dataGridView1A.AllowUserToAddRows = false;
        dataGridView1A.AllowUserToDeleteRows = false;
        dataGridView1A.ReadOnly = true;
        dataGridView1A.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dataGridView1A.MultiSelect = false;
        dataGridView1A.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dataGridView1A.BackgroundColor = Color.White;
        dataGridView1A.Font = new Font("Courier New", 9);
        dataGridView1A.RowTemplate.Height = 60;

        ConfigureDataGridViewColumns();

        button1Add.Font = new Font("Courier New", 10, FontStyle.Bold);
        button1Add.BackColor = Color.FromArgb(161, 99, 245);
        button1Add.ForeColor = Color.White;
        button1Add.FlatStyle = FlatStyle.Flat;
        button1Add.FlatAppearance.BorderSize = 0;
        button1Add.Location = new Point(20, 620);
        button1Add.Size = new Size(100, 40);

        button1Edit.Font = new Font("Courier New", 10);
        button1Edit.BackColor = Color.White;
        button1Edit.FlatStyle = FlatStyle.Flat;
        button1Edit.Location = new Point(130, 620);
        button1Edit.Size = new Size(120, 40);

        button1Delete.Font = new Font("Courier New", 10);
        button1Delete.BackColor = Color.White;
        button1Delete.FlatStyle = FlatStyle.Flat;
        button1Delete.Location = new Point(260, 620);
        button1Delete.Size = new Size(100, 40);

        button1Matherials.Font = new Font("Courier New", 10);
        button1Matherials.BackColor = Color.FromArgb(161, 99, 245);
        button1Matherials.ForeColor = Color.White;
        button1Matherials.FlatStyle = FlatStyle.Flat;
        button1Matherials.FlatAppearance.BorderSize = 0;
        button1Matherials.Location = new Point(370, 620);
        button1Matherials.Size = new Size(120, 40);

        if (_currentUser?.Role == "Agent")
        {
            button1Add.Enabled = false;
            button1Edit.Enabled = false;
            button1Delete.Enabled = false;
            button1Matherials.Enabled = false;
        }

        imageList = new ImageList();
        imageList.ImageSize = new Size(50, 50);
        imageList.ColorDepth = ColorDepth.Depth32Bit;
    }

    private void ConfigureDataGridViewColumns()
    {
        dataGridView1A.Columns.Clear();

        dataGridView1A.Columns.Add("ProductId", "ID");
        dataGridView1A.Columns.Add("Name", "Наименование");
        dataGridView1A.Columns.Add("Article", "Артикул");
        dataGridView1A.Columns.Add("ProductType", "Тип");
        dataGridView1A.Columns.Add("MinPrice", "Мин. цена");
        dataGridView1A.Columns.Add("PeopleCount", "Кол-во чел.");
        dataGridView1A.Columns.Add("WorkshopNumber", "Цех");

        DataGridViewImageColumn imageColumn = new DataGridViewImageColumn
        {
            Name = "Image",
            HeaderText = "Фото",
            Width = 80,
            ImageLayout = DataGridViewImageCellLayout.Zoom
        };
        dataGridView1A.Columns.Add(imageColumn);


        dataGridView1A.Columns["ProductId"].Width = 50;
        dataGridView1A.Columns["Name"].Width = 250;
        dataGridView1A.Columns["Article"].Width = 100;
        dataGridView1A.Columns["ProductType"].Width = 100;
        dataGridView1A.Columns["MinPrice"].Width = 100;
        dataGridView1A.Columns["PeopleCount"].Width = 80;
        dataGridView1A.Columns["WorkshopNumber"].Width = 80;
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
        dataGridView1A.Rows.Clear();
        dataGridView1A.Refresh();
        imageList.Images.Clear();

        string imagesRoot = Application.StartupPath;

        foreach (var product in products)
        {
            int imageIndex = -1;

            if (!string.IsNullOrWhiteSpace(product.ImagePath) &&
                !product.ImagePath.Contains("нет", StringComparison.OrdinalIgnoreCase) &&
                !product.ImagePath.Contains("не указано", StringComparison.OrdinalIgnoreCase) &&
                !product.ImagePath.Contains("отсутствует", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    string relativePath = product.ImagePath.TrimStart('\\', '/');
                    string fullPath = Path.Combine(imagesRoot, relativePath);

                    System.Diagnostics.Debug.WriteLine($"Путь: {fullPath}");

                    if (File.Exists(fullPath))
                    {
                        using (var fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            using (var imgTemp = Image.FromStream(fs))
                            {
                                var bmp = new Bitmap(imgTemp);
                                imageList.Images.Add(bmp);
                                imageIndex = imageList.Images.Count - 1;
                            }
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"Файл НЕ найден: {fullPath}");
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Ошибка загрузки: {product.ImagePath}");
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    imageIndex = -1;
                }
            }

            int rowIndex = dataGridView1A.Rows.Add(
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
                dataGridView1A.Rows[rowIndex].Cells["Image"].Value = imageList.Images[imageIndex];
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
            var searchText = textBox1Search.Text.ToLower();
            var filterType = comboBox1Type.SelectedItem?.ToString();

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
        if (dataGridView1A.SelectedRows.Count == 0)
        {
            MessageBox.Show("Выберите продукт для редактирования", "Внимание",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var selectedRow = dataGridView1A.SelectedRows[0];
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
        if (dataGridView1A.SelectedRows.Count == 0)
        {
            MessageBox.Show("Выберите продукт для удаления", "Внимание",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var selectedRow = dataGridView1A.SelectedRows[0];
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
        if (dataGridView1A.SelectedRows.Count == 0)
        {
            MessageBox.Show("Выберите продукт для просмотра материалов", "Внимание",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var selectedRow = dataGridView1A.SelectedRows[0];
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
