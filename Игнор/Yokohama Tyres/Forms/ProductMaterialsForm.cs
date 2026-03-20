using System;
using System.Drawing;
using System.Windows.Forms;
using Yokohama_Tyres.Models;
using Yokohama_Tyres.Repositories;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Yokohama_Tyres.Forms
{
    public partial class ProductMaterialsForm : Form
    {

        private DataGridView dgvMaterials;
        private ComboBox cmbMaterial;
        private NumericUpDown nudQuantity;
        private Button btnAdd;
        private Button btnRemove;
        private Button btnClose;
        private Label lblProductInfo;
        private Label lblMaterial;
        private Label lblQuantity;
        private Panel panelTop;
        private Panel panelBottom;

        private Product? _product;
        private ProductRepository _productRepo;
        private MaterialRepository _materialRepo;

        public ProductMaterialsForm(Product product)
        {
            InitializeComponent();
            _product = product;
            _productRepo = new ProductRepository();
            _materialRepo = new MaterialRepository();
            SetupForm();
            LoadMaterials();
            LoadProductMaterials();
        }

        private void SetupForm()
        {
            
            this.Text = $"Материалы для продукта: {_product?.Name}";
            this.Size = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;

            
            panelTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.FromArgb(211, 211, 211), 
                Padding = new Padding(10)
            };

            lblProductInfo = new Label
            {
                Text = $"Продукт: {_product?.Name}\n" +
                      $"Артикул: {_product?.Article}\n" +
                      $"Тип: {_product?.ProductType?.TypeName}",
                Font = new Font("Courier New", 10),
                ForeColor = Color.Black,
                Location = new Point(10, 10),
                Size = new Size(400, 60),
                BackColor = Color.Transparent
            };
            panelTop.Controls.Add(lblProductInfo);

            Panel addPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.White,
                Padding = new Padding(10)
            };

            lblMaterial = new Label
            {
                Text = "Материал:",
                Font = new Font("Courier New", 10),
                Location = new Point(20, 25),
                Size = new Size(80, 25),
                TextAlign = ContentAlignment.MiddleRight
            };

            cmbMaterial = new ComboBox
            {
                Location = new Point(110, 25),
                Size = new Size(250, 25),
                Font = new Font("Courier New", 10),
                DropDownStyle = ComboBoxStyle.DropDownList,
                DisplayMember = "MaterialName",
                ValueMember = "MaterialId"
            };

            lblQuantity = new Label
            {
                Text = "Кол-во:",
                Font = new Font("Courier New", 10),
                Location = new Point(370, 25),
                Size = new Size(60, 25),
                TextAlign = ContentAlignment.MiddleRight
            };

            nudQuantity = new NumericUpDown
            {
                Location = new Point(440, 25),
                Size = new Size(60, 25),
                Font = new Font("Courier New", 10),
                Minimum = 1,
                Maximum = 999,
                Value = 1
            };

            btnAdd = new Button
            {
                Text = "Добавить",
                Location = new Point(520, 25),
                Size = new Size(100, 30),
                Font = new Font("Courier New", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(161, 99, 245), 
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.Click += BtnAdd_Click;

            addPanel.Controls.Add(lblMaterial);
            addPanel.Controls.Add(cmbMaterial);
            addPanel.Controls.Add(lblQuantity);
            addPanel.Controls.Add(nudQuantity);
            addPanel.Controls.Add(btnAdd);

            panelBottom = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = Color.FromArgb(211, 211, 211),
                Padding = new Padding(10)
            };

            btnRemove = new Button
            {
                Text = "Удалить материал",
                Location = new Point(10, 15),
                Size = new Size(140, 30),
                Font = new Font("Courier New", 10),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnRemove.Click += BtnRemove_Click;

            btnClose = new Button
            {
                Text = "Закрыть",
                Location = new Point(650, 15),
                Size = new Size(100, 30),
                Font = new Font("Courier New", 10),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnClose.Click += (s, e) => this.Close();

            panelBottom.Controls.Add(btnRemove);
            panelBottom.Controls.Add(btnClose);

            dgvMaterials = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                Font = new Font("Courier New", 9)
            };

        
            dgvMaterials.Columns.Add("MaterialId", "ID");
            dgvMaterials.Columns.Add("MaterialName", "Наименование материала");
            dgvMaterials.Columns.Add("Quantity", "Количество");

            dgvMaterials.Columns["MaterialId"].Width = 50;
            dgvMaterials.Columns["MaterialName"].Width = 400;
            dgvMaterials.Columns["Quantity"].Width = 100;

            this.Controls.Add(dgvMaterials);
            this.Controls.Add(addPanel);
            this.Controls.Add(panelTop);
            this.Controls.Add(panelBottom);
        }

        private void LoadMaterials()
        {
            try
            {
                var materials = _materialRepo.GetAllMaterials();
                cmbMaterial.DataSource = materials;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки материалов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProductMaterials()
        {
            try
            {
                _product = _productRepo.GetProductById(_product.ProductId);

                if (_product?.ProductMaterials == null) return;

                dgvMaterials.Rows.Clear();

                foreach (var pm in _product.ProductMaterials)
                {
                    dgvMaterials.Rows.Add(
                        pm.MaterialId,
                        pm.Material?.MaterialName,
                        pm.Quantity
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки материалов продукта: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            if (cmbMaterial.SelectedItem == null)
            {
                MessageBox.Show("Выберите материал", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var selectedMaterial = (Material)cmbMaterial.SelectedItem;
                int quantity = (int)nudQuantity.Value;

                if (_product?.ProductMaterials?.Any(pm => pm.MaterialId == selectedMaterial.MaterialId) == true)
                {
                    MessageBox.Show("Этот материал уже добавлен к продукту", "Внимание",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

          
                var productMaterial = new ProductMaterial
                {
                    ProductId = _product.ProductId,
                    MaterialId = selectedMaterial.MaterialId,
                    Quantity = quantity
                };

                using (var context = new YokohamaTyresDbContext())
                {
                    context.ProductMaterials.Add(productMaterial);
                    context.SaveChanges();
                }

                LoadProductMaterials(); 

                nudQuantity.Value = 1;

                MessageBox.Show("Материал добавлен", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления материала: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRemove_Click(object? sender, EventArgs e)
        {
            if (dgvMaterials.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите материал для удаления", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvMaterials.SelectedRows[0];
            var materialId = (int)selectedRow.Cells["MaterialId"].Value;
            var materialName = selectedRow.Cells["MaterialName"].Value?.ToString();

            var result = MessageBox.Show($"Удалить материал \"{materialName}\" из продукта?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var context = new YokohamaTyresDbContext())
                    {
                        var productMaterial = context.ProductMaterials
                            .FirstOrDefault(pm => pm.ProductId == _product.ProductId &&
                                                  pm.MaterialId == materialId);

                        if (productMaterial != null)
                        {
                            context.ProductMaterials.Remove(productMaterial);
                            context.SaveChanges();
                        }
                    }

                    LoadProductMaterials();

                    MessageBox.Show("Материал удален", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления материала: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
