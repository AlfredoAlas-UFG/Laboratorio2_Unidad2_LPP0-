using System;
using System.Windows;
using LaboratorioPeriodo2.DTOs;
using LaboratorioPeriodo2.DAOs;

namespace LaboratorioPeriodo2
{
    public partial class AddOrEditProducto : Window
    {
        private ProductoDTO _producto = null;
        private ProductoDAO _daoProducto = new ProductoDAO();

        // Constructor para nuevo producto
        public AddOrEditProducto()
        {
            InitializeComponent();
            btnAgregarEditar.Content = "Agregar";
        }

        // Constructor para editar un producto existente
        public AddOrEditProducto(ProductoDTO producto) : this()
        {
            _producto = producto;
            btnAgregarEditar.Content = "Editar";
            LoadProductoData();
        }

        private void LoadProductoData()
        {
            if (_producto != null)
            {
                txtCodigoProd.Text = _producto.codProducto.ToString();
                txtNombreProd.Text = _producto.nombreProducto;
                txtNombreProv.Text = _producto.nombreProveedor;
                txtPrecioUnit.Text = _producto.precioUnitario.ToString();
                txtUnidades.Text = _producto.unidades.ToString();
                txtCodigoProd.IsEnabled = false; // Evitar edición del código de producto
            }
        }

        private void btnAgregarEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var producto = new ProductoDTO
                {
                    codProducto = int.Parse(txtCodigoProd.Text),
                    nombreProducto = txtNombreProd.Text,
                    nombreProveedor = txtNombreProv.Text,
                    precioUnitario = decimal.Parse(txtPrecioUnit.Text),
                    unidades = int.Parse(txtUnidades.Text)
                };

                if (_producto == null)
                {
                    // Agregar nuevo producto
                    _daoProducto.Add(producto);
                    MessageBox.Show("Producto agregado con éxito.");
                }
                else
                {
                    // Editar producto existente
                    producto.codProducto = _producto.codProducto; // Asegurarse de mantener el mismo código de producto
                    _daoProducto.Edit(producto);
                    MessageBox.Show("Producto editado con éxito.");
                }

                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al intentar agregar/editar el producto: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
