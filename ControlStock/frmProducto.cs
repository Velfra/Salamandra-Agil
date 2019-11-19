using ClasesNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlStock
{
    public partial class frmProducto : Form
    {
        DataSet resultados = new DataSet();
        DataView mifiltro;
        string modo;
        public frmProducto()
        {
            InitializeComponent();
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = "";
            nudCantidad.Value = 0;
            cboCategoria.SelectedItem = null;
            cboProveedor.SelectedItem = null;
            nudPrecioCosto.Value = 0;

        }

        private void DesbloquearFormulario()
        {
            txtNombre.Enabled = true;
            nudCantidad.Enabled = true;
            cboCategoria.Enabled = true;
            cboProveedor.Enabled = true;
            nudPrecioCosto.Enabled = true;

            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            btnLimpiar.Enabled = true;

            btnAgregar.Enabled = false;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
        }
        private void BloquearFormulario()
        {
            txtNombre.Enabled = false;
            nudCantidad.Enabled = false;
            cboCategoria.Enabled = false;
            cboProveedor.Enabled = false;
            nudPrecioCosto.Enabled = false;

            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            btnLimpiar.Enabled = false;

            
            btnAgregar.Enabled = true;
            btnEliminar.Enabled = true;
            btnModificar.Enabled = true;
        }

        private Producto ObtenerDatosFormulario()
        {
            Producto producto = new Producto();
            producto.Nombre = txtNombre.Text;
            producto.Categoria = (Categoria)cboCategoria.SelectedValue;
            producto.Cantidad = (int)nudCantidad.Value;
            producto.PrecioCompra = (double)(nudPrecioCosto.Value);
            producto.Proveedor = (Proveedor)cboProveedor.SelectedValue;

            return producto;

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            var p = ObtenerDatosFormulario();


            if (modo == "AGREGAR")
            {
                Producto.AgregarProducto(p);
            }
            else if (modo == "EDITAR")
            {

                if (this.dgvProducto.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Favor seleccione una fila");
                }

                else
                {
                    int indice = Convert.ToInt32(dgvProducto.SelectedRows.Count);
                    Producto.EditarProducto(p, indice);
                    ActualizarListaProductos();
                }

            }

            LimpiarFormulario();
            ActualizarListaProductos();
            BloquearFormulario();
        }

        private void ActualizarListaProductos()
        {
            dgvProducto.DataSource = null;
            dgvProducto.DataSource = Producto.ObtenerProductos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (this.dgvProducto.SelectedRows.Count == 0)
            {
                MessageBox.Show("Favor seleccione una fila");
            }
            else
            {
                Producto p = (Producto)dgvProducto.CurrentRow.DataBoundItem;
                Producto.EliminarProducto(p);
                ActualizarListaProductos();
                LimpiarFormulario();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            BloquearFormulario();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (this.dgvProducto.SelectedRows.Count == 0)
            {
                MessageBox.Show("Favor seleccione una fila");
            }
            else
            {

                modo = "EDITAR";
                DesbloquearFormulario();
                txtNombre.Focus();
            }
        }

        private void frmProducto_Load(object sender, EventArgs e)
        {
            ActualizarListaProductos();
            cboProveedor.DataSource = Proveedor.ObtenerProveedores();
            cboCategoria.DataSource = Categoria.ObtenerCategorias();
            cboCategoria.SelectedItem = null;
            cboProveedor.SelectedItem = null;
            BloquearFormulario();
            this.leer_datos("Select Producto.Nombre,cantidad,Categoria.Nombre,PrecioCompra,Proveedor.RazonSocial,Fecha_pedido from Producto INNER JOIN Proveedor On Proveedor.id = Producto.Proveedor INNER JOIN Categoria On Categoria.id = Producto.Categoria", ref resultados, "producto");
            this.mifiltro = ((DataTable)resultados.Tables["producto"]).DefaultView;
            this.dgvProducto.DataSource = mifiltro;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            modo = "AGREGAR";
            LimpiarFormulario();
            DesbloquearFormulario();
            txtNombre.Focus();
        }
      
        private void leer_datos(string query, ref DataSet dstPrincipal, string tabla)
        {
            using (SqlConnection con = new SqlConnection(SqlServer.CADENA_CONEXION))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dstPrincipal, tabla);
                da.Dispose();
                con.Close();

            }
        }
        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            string salidaDatos = "";
            string[] palabrasBusqueda = this.txtBuscar.Text.Split(' ');
            foreach (string palabra in palabrasBusqueda)
            {
                if (salidaDatos.Length == 0)
                {
                    salidaDatos = "(Nombre Like '%" + txtBuscar.Text + "%')";
                }
                else
                {
                    salidaDatos += "(Nombre Like '%" + txtBuscar.Text + "%')";
                }
            }
            this.mifiltro.RowFilter = salidaDatos;
        }

        private void dgvProducto_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Producto p = (Producto)dgvProducto.CurrentRow.DataBoundItem;

            if (p != null)
            {
                txtNombre.Text = p.Nombre;
                cboCategoria.SelectedItem = p.Categoria.Nombre;
                cboProveedor.SelectedItem = p.Proveedor.RazonSocial;
                nudCantidad.Value = p.Cantidad;
                nudPrecioCosto.Value = (decimal)p.PrecioCompra;

            }

            tbcProducto.SelectedIndex = 0;
        }

    }
}
