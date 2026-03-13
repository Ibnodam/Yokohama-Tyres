using System;
using System.Drawing;
using System.Windows.Forms;
using Yokohama_Tyres.Models;
using Yokohama_Tyres.Repositories;
using System.Linq;
using System.IO;

namespace Yokohama_Tyres.Forms
{
    public partial class ProductEditForm : Form
    {

        private TextBox txtName;
        private TextBox txtArticle;
        private ComboBox cmbProductType;
        private TextBox txtMinPrice;
        private TextBox txtPeopleCount;
        private TextBox txtWorkshop;
        private TextBox txtImagePath;
        private Button btnBrowseImage;
        private Button btnSave;
        private Button btnCancel;
        private Label lblName;
        private Label lblArticle;
        private Label lblProductType;
        private Label lblMinPrice;
        private Label lblPeopleCount;
        private Label lblWorkshop;
        private Label lblImagePath;
        private Panel panelMain;

        private Product? _product;
        private ProductRepository _productRepo;
        private bool _isEditMode;

        public ProductEditForm(Product? product = null)
        {
            InitializeComponent();
            _product = product;
            _productRepo = new ProductRepository();
            _isEditMode = product != null;
            SetupForm();
            LoadProductTypes();
            if (_isEditMode)
                LoadProductData();
        }

        private void SetupForm()
        {
          
            this.Text = _isEditMode ? "Редактирование продукции" : "Добавление продукции";
            this.Size = new Size(500, 450);
            this.StartPosition = FormStartPosition.CenterParent;
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

            int yPos = 20;
            int labelWidth = 120;
            int controlWidth = 250;
            int spacing = 35;

            lblName = CreateLabel("Наименование:", new Point(30, yPos));
            txtName = CreateTextBox(new Point(160, yPos - 3), controlWidth);
            panelMain.Controls.Add(lblName);
            panelMain.Controls.Add(txtName);

        
            yPos += spacing;
            lblArticle = CreateLabel("Артикул:", new Point(30, yPos));
            txtArticle = CreateTextBox(new Point(160, yPos - 3), controlWidth);
            panelMain.Controls.Add(lblArticle);
            panelMain.Controls.Add(txtArticle);

      
            yPos += spacing;
            lblProductType = CreateLabel("Тип:", new Point(30, yPos));
            cmbProductType = new ComboBox
            {
                Location = new Point(160, yPos - 3),
                Size = new Size(controlWidth, 25),
                Font = new Font("Courier New", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            panelMain.Controls.Add(lblProductType);
            panelMain.Controls.Add(cmbProductType);

        
            yPos += spacing;
            lblMinPrice = CreateLabel("Мин. цена:", new Point(30, yPos));
            txtMinPrice = CreateTextBox(new Point(160, yPos - 3), controlWidth);
            panelMain.Controls.Add(lblMinPrice);
            panelMain.Controls.Add(txtMinPrice);

          
            yPos += spacing;
            lblPeopleCount = CreateLabel("Кол-во чел.:", new Point(30, yPos));
            txtPeopleCount = CreateTextBox(new Point(160, yPos - 3), controlWidth);
            panelMain.Controls.Add(lblPeopleCount);
            panelMain.Controls.Add(txtPeopleCount);

            yPos += spacing;
            lblWorkshop = CreateLabel("Номер цеха:", new Point(30, yPos));
            txtWorkshop = CreateTextBox(new Point(160, yPos - 3), controlWidth);
            panelMain.Controls.Add(lblWorkshop);
            panelMain.Controls.Add(txtWorkshop);

          
            yPos += spacing;
            lblImagePath = CreateLabel("Изображение:", new Point(30, yPos));
            txtImagePath = CreateTextBox(new Point(160, yPos - 3), controlWidth - 80);
            txtImagePath.ReadOnly = true;

            btnBrowseImage = new Button
            {
                Text = "Обзор",
                Location = new Point(160 + controlWidth - 70, yPos - 3),
                Size = new Size(70, 25),
                Font = new Font("Courier New", 8),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnBrowseImage.Click += BtnBrowseImage_Click;

            panelMain.Controls.Add(lblImagePath);
            panelMain.Controls.Add(txtImagePath);
            panelMain.Controls.Add(btnBrowseImage);


            btnSave = new Button
            {
                Text = "Сохранить",
                Location = new Point(160, yPos + spacing + 10),
                Size = new Size(100, 35),
                Font = new Font("Courier New", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(161, 99, 245), // #A163F5
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button
            {
                Text = "Отмена",
                Location = new Point(280, yPos + spacing + 10),
                Size = new Size(100, 35),
                Font = new Font("Courier New", 10),
                BackColor = Color.White,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.FlatAppearance.BorderSize = 1;
            btnCancel.Click += (s, e) => this.Close();

            panelMain.Controls.Add(btnSave);
            panelMain.Controls.Add(btnCancel);

            this.Controls.Add(panelMain);
        }

        private Label CreateLabel(string text, Point location)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Courier New", 10),
                Location = location,
                Size = new Size(120, 25),
                TextAlign = ContentAlignment.MiddleRight,
                BackColor = Color.Transparent
            };
        }

        private TextBox CreateTextBox(Point location, int width)
        {
            return new TextBox
            {
                Location = location,
                Size = new Size(width, 25),
                Font = new Font("Courier New", 10)
            };
        }

        private void LoadProductTypes()
        {
            try
            {
                using (var context = new YokohamaTyresDbContext())
                {
                    var types = context.ProductTypes.OrderBy(t => t.TypeName).ToList();
                    cmbProductType.DataSource = types;
                    cmbProductType.DisplayMember = "TypeName";
                    cmbProductType.ValueMember = "ProductTypeId";
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

            txtName.Text = _product.Name;
            txtArticle.Text = _product.Article;
            cmbProductType.SelectedValue = _product.ProductTypeId;
            txtMinPrice.Text = _product.MinPriceForAgent?.ToString();
            txtPeopleCount.Text = _product.PeopleCount?.ToString();
            txtWorkshop.Text = _product.WorkshopNumber?.ToString();
            txtImagePath.Text = _product.ImagePath;
        }

        private void BtnBrowseImage_Click(object? sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.FilterIndex = 1;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string productsDir = Path.Combine(Application.StartupPath, "Resources", "images", "products");
                        if (!Directory.Exists(productsDir))
                            Directory.CreateDirectory(productsDir);

                        string fileName = $"{Guid.NewGuid()}{Path.GetExtension(openFileDialog.FileName)}";
                        string destPath = Path.Combine(productsDir, fileName);

                        File.Copy(openFileDialog.FileName, destPath, true);

                        txtImagePath.Text = Path.Combine("Resources", "images", "products", fileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}");
                    }
                }
            }
        }


        private void BtnSave_Click(object? sender, EventArgs e)
        {
 
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите наименование продукции", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtArticle.Text))
            {
                MessageBox.Show("Введите артикул", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbProductType.SelectedItem == null)
            {
                MessageBox.Show("Выберите тип продукции", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
         
                var product = _isEditMode ? _product : new Product();

                if (product == null) return;

                product.Name = txtName.Text.Trim();
                product.Article = txtArticle.Text.Trim();
                product.ProductTypeId = (int)cmbProductType.SelectedValue;

                if (decimal.TryParse(txtMinPrice.Text, out decimal minPrice))
                    product.MinPriceForAgent = minPrice;
                else
                    product.MinPriceForAgent = null;

                if (int.TryParse(txtPeopleCount.Text, out int peopleCount))
                    product.PeopleCount = peopleCount;
                else
                    product.PeopleCount = null;

                if (int.TryParse(txtWorkshop.Text, out int workshop))
                    product.WorkshopNumber = workshop;
                else
                    product.WorkshopNumber = null;

                product.ImagePath = txtImagePath.Text;

            
                if (_isEditMode)
                {
                    _productRepo.UpdateProduct(product);
                    MessageBox.Show("Продукт обновлен", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _productRepo.AddProduct(product);
                    MessageBox.Show("Продукт добавлен", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

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