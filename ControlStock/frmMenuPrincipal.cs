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
    public partial class frmMenuPrincipal : Form
    {
        public frmMenuPrincipal()
        {
            InitializeComponent();
        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProveedor frmProv = new frmProveedor();
            frmProv.Show();
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProducto frmProdu = new frmProducto();
            frmProdu.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCategoria frmCateg = new frmCategoria();
            frmCateg.Show();
        }

        private void pedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPedidoProducto frmPedido = new frmPedidoProducto();
            frmPedido.Show();
        }
    }
}
