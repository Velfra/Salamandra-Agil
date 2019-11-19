using ClasesNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlStock
{
    public partial class frmPedidoProducto : Form
    {
        Pedido pedido;
        public string modo;
        public frmPedidoProducto()
        {
            InitializeComponent();
        }

        private void frmPedido_Load(object sender, EventArgs e)
        {


            dtgDetallePedido.AutoGenerateColumns = true;
            cmbProducto.DataSource = Producto.ObtenerProductos();
            cmbProveedor.DataSource = Proveedor.ObtenerProveedores();
            cmbProducto.SelectedItem = null;
            cmbProveedor.SelectedItem = null;
            pedido = new Pedido();



        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            PedidoDetalle pd = new PedidoDetalle();
            pd.cantidad = Convert.ToDouble(txtCantidad.Text);
            pd.producto = (Producto)cmbProducto.SelectedItem;
            pedido.detalle_pedidos.Add(pd);
            ActualizarDataGrid();

            Limpiar();
        }


        private void ActualizarDataGrid()
        {
            dtgDetallePedido.DataSource = null;
            dtgDetallePedido.DataSource = pedido.detalle_pedidos;

        }

        private void Limpiar()
        {
            txtCantidad.Text = "0";
            cmbProducto.SelectedItem = null;
            


        }

       

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            PedidoDetalle pd = (PedidoDetalle)dtgDetallePedido.CurrentRow.DataBoundItem;
            pedido.detalle_pedidos.Remove(pd);
            ActualizarDataGrid();

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            pedido.fecha_llegada = dtpFechaLlegada.Value.Date;
            pedido.proveedor = (Proveedor)cmbProveedor.SelectedItem;

            Pedido.Agregar(pedido);
            MessageBox.Show("El pedido ha sido guardado con éxito");
            Limpiar();
            dtgDetallePedido.DataSource = null;
            dtpFechaLlegada.Value = System.DateTime.Now;
            cmbProveedor.SelectedItem = null;
            pedido = new Pedido();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (this.dtgDetallePedido.SelectedRows.Count == 0)
            {
                MessageBox.Show("Favor seleccione una fila");
            }
            else
            {
                modo = "EDITAR";
                
                dtpFechaLlegada.Focus();
            }
        }

        //private void dtgDetallePedido_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    Producto producto = (Producto)dtgDetallePedido.CurrentRow.DataBoundItem;
        //    txtCantidad.Text = Convert.ToString(producto.Cantidad);

        //    dtpFechaLlegada.Value = producto.FechaPedido;

        //    cmbProducto.SelectedItem = producto.Nombre;
        //    cmbProveedor.SelectedItem = producto.Proveedor;


        //}
    }
}

