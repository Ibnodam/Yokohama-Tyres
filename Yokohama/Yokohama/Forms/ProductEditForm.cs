using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Yokohama.Models;
using Yokohama_Tyres.Repositories;

namespace Yokohama_Tyres.Forms
{
    public partial class ProductEditForm : Form
    {
        private Product? _product;
        private ProductRepository _productRepo;
        private bool _isEditMode;
        private string _selectedImagePath = "";

        public ProductEditForm(Product? product = null)
        {
            InitializeComponent();
            _product = product;
            _productRepo = new ProductRepository();
            _isEditMode = product != null;

            this.Text = _isEditMode ? "Редактирование продукции" : "Добавление продукции";

            LoadProductTypes();

            if (_isEditMode)
                LoadProductData();
            string iconPath = Path.Combine(Application.StartupPath, "Resources", "Icons", "app.ico");
            if (File.Exists(iconPath))
            {
                this.Icon = new Icon(iconPath);
            }
            this.button1AddPic.Click += BtnBrowseImage_Click;
            this.button1Save.Click += BtnSave_Click;
            this.button2Cancel.Click += (s, e) => this.Close();
        }

        private void LoadProductTypes()
        {
            try
            {
                using (var context = new YokohamaTyresDbContext())
                {
                    var types = context.ProductTypes.OrderBy(t => t.TypeName).ToList();
                    comboBox1Type.DataSource = types;
                    comboBox1Type.DisplayMember = "TypeName";
                    comboBox1Type.ValueMember = "ProductTypeId";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки типов продукции: {ex.Message}", "Ошибка");
            }
        }



        private void LoadProductData()
        {
            if (_product == null) return;

            textBox1Name.Text = _product.Name;
            textBox7Articul.Text = _product.Article;
            comboBox1Type.SelectedValue = _product.ProductTypeId;
            textBox6MinPrice.Text = _product.MinPriceForAgent?.ToString();
            textBox4CountOfPeople.Text = _product.PeopleCount?.ToString();
            textBox3WorkshopNumber.Text = _product.WorkshopNumber?.ToString();

    
            if (!string.IsNullOrEmpty(_product.ImagePath))
            {
                _selectedImagePath = _product.ImagePath;

                string cleanPath = _product.ImagePath.TrimStart('\\', '/');
                string fullPath = Path.Combine(Application.StartupPath, cleanPath);

                System.Diagnostics.Debug.WriteLine($"Загрузка существующего: {fullPath}");

                if (File.Exists(fullPath))
                {
                    try
                    {
                        pictureBox1.Image = Image.FromFile(fullPath);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Ошибка загрузки: {ex.Message}");
                    }
                }
            }
        }

        private void LoadImageToPictureBox(string imagePath)
        {
            try
            {
                string fullPath = Path.Combine(Application.StartupPath, imagePath);
                if (File.Exists(fullPath))
                {
                    pictureBox1.Image = Image.FromFile(fullPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки изображения: {ex.Message}");
            }
        }


        private void BtnBrowseImage_Click(object? sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.FilterIndex = 1;
                openFileDialog.Title = "Выберите изображение";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {

                        string productsDir = Path.Combine(Application.StartupPath, "products");

                        if (!Directory.Exists(productsDir))
                            Directory.CreateDirectory(productsDir);

                     
                        string fileName = $"{Guid.NewGuid()}{Path.GetExtension(openFileDialog.FileName)}";
                        string destPath = Path.Combine(productsDir, fileName);

      
                        File.Copy(openFileDialog.FileName, destPath, true);


                        _selectedImagePath = $"\\products\\{fileName}";

                  
                        System.Diagnostics.Debug.WriteLine($"Файл сохранен: {destPath}");
                        System.Diagnostics.Debug.WriteLine($"Путь для БД: {_selectedImagePath}");
                        System.Diagnostics.Debug.WriteLine($"Файл существует: {File.Exists(destPath)}");

                        if (pictureBox1.Image != null)
                        {
                            pictureBox1.Image.Dispose();
                        }
                        pictureBox1.Image = Image.FromFile(destPath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при копировании изображения: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private void BtnSave_Click(object? sender, EventArgs e)
        {
         
            if (string.IsNullOrWhiteSpace(textBox1Name.Text))
            {
                MessageBox.Show("Введите наименование продукции", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox7Articul.Text))
            {
                MessageBox.Show("Введите артикул", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBox1Type.SelectedItem == null)
            {
                MessageBox.Show("Выберите тип продукции", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            try
            {
                var product = _isEditMode ? _product : new Product();
                if (product == null) return;

                product.Name = textBox1Name.Text.Trim();
                product.Article = textBox7Articul.Text.Trim();
                product.ProductTypeId = (int)comboBox1Type.SelectedValue;

 
                product.ImagePath = _selectedImagePath;

                if (_isEditMode)
                    _productRepo.UpdateProduct(product);
                else
                    _productRepo.AddProduct(product);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}