using System;
using System.Linq;
using System.Windows.Forms;
using Yokohama.Models;
using Yokohama_Tyres.Repositories;

namespace Yokohama_Tyres.Forms
{
    public partial class ProductMaterialsForm : Form
    {
        private Product? _product;
        private ProductRepository _productRepo;
        private MaterialRepository _materialRepo;

        public ProductMaterialsForm(Product product)
        {


            InitializeComponent();
            _product = product;
            _productRepo = new ProductRepository();
            _materialRepo = new MaterialRepository();

            this.Text = $"Материалы для продукта: {_product?.Name}";

            SetupDataGridView();

            LoadMaterials();
            LoadProductMaterials();

            string iconPath = Path.Combine(Application.StartupPath, "Resources", "Icons", "app.ico");
            if (File.Exists(iconPath))
            {
                this.Icon = new Icon(iconPath);
            }

            this.button1Add.Click += BtnAdd_Click;
            this.button1Delete.Click += BtnRemove_Click;
            this.button1Cancel.Click += (s, e) => this.Close();
        }

        private void SetupDataGridView()
        {
            dataGridView1M.Columns.Clear();

            dataGridView1M.Columns.Add("MaterialId", "ID");
            dataGridView1M.Columns.Add("MaterialName", "Наименование материала");
            dataGridView1M.Columns.Add("Quantity", "Количество");

            dataGridView1M.Columns["MaterialId"].Width = 50;
            dataGridView1M.Columns["MaterialName"].Width = 400;
            dataGridView1M.Columns["Quantity"].Width = 100;

            dataGridView1M.AllowUserToAddRows = false;
            dataGridView1M.AllowUserToDeleteRows = false;
            dataGridView1M.ReadOnly = true;
            dataGridView1M.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1M.MultiSelect = false;
            dataGridView1M.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView1M.RowHeadersVisible = false;
        }

        private void LoadMaterials()
        {
            try
            {
                var materials = _materialRepo.GetAllMaterials();
                comboBox1Matherial.DataSource = materials;
                comboBox1Matherial.DisplayMember = "MaterialName";
                comboBox1Matherial.ValueMember = "MaterialId";
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

                dataGridView1M.Rows.Clear();

                if (_product?.ProductMaterials != null)
                {
                    foreach (var pm in _product.ProductMaterials)
                    {
                        dataGridView1M.Rows.Add(
                            pm.MaterialId,
                            pm.Material?.MaterialName ?? "Неизвестно",
                            pm.Quantity
                        );
                    }
                }

                label4Pro.Text = _product?.Name ?? "Не указан";
                label3Arti.Text = _product?.Article ?? "Не указан";
                label2Ty.Text = _product?.ProductType?.TypeName ?? "Не указан";

                System.Diagnostics.Debug.WriteLine($"Загружено материалов: {dataGridView1M.Rows.Count}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки материалов продукта: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            if (comboBox1Matherial.SelectedItem == null)
            {
                MessageBox.Show("Выберите материал", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var selectedMaterial = (Material)comboBox1Matherial.SelectedItem;
                int quantity = (int)numericUpDown1.Value;

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

                numericUpDown1.Value = 1;

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
            if (dataGridView1M.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите материал для удаления", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dataGridView1M.SelectedRows[0];
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
