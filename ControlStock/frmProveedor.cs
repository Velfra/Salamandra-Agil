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
    public partial class frmProveedor : Form
    {
        string modo;
        public frmProveedor()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            var p = ObtenerDatosFormulario();


            if (modo == "AGREGAR")
            {
                Proveedor.AgregarProveedor(p);
            }
            else if (modo == "EDITAR")
            {

                if (this.lstProveedor.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Favor seleccione una fila");
                }

                else
                {
                    int indice = lstProveedor.SelectedIndex;
                    Proveedor.EditarProveedor(indice, p);
                    ActualizarListaProveedores();
                }


            }

            LimpiarFormulario();
            ActualizarListaProveedores();
            BloquearFormulario();


        }


        private Proveedor ObtenerDatosFormulario()
        {
            Proveedor proveedor = new Proveedor();
            proveedor.RazonSocial = txtNombreProveedor.Text;
            proveedor.Direccion = txtDireccion.Text;
            proveedor.Contacto = txtTelefono.Text;
            proveedor.Email = txtMail.Text;

            return proveedor;

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            modo = "AGREGAR";
            LimpiarFormulario();
            DesbloquearFormularios();
            txtNombreProveedor.Focus();
        }

        private void DesbloquearFormularios()
        {
            txtNombreProveedor.Enabled = true;
            txtDireccion.Enabled = true;
            txtTelefono.Enabled = true;
            txtMail.Enabled = true;

            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            btnLimpiar.Enabled = true;

            lstProveedor.Enabled = false;
            btnAgregar.Enabled = false;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;


        }

        
        private void LimpiarFormulario()
        {
            txtNombreProveedor.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            txtMail.Text = "";
        }


        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (this.lstProveedor.SelectedItems.Count == 0)
            {
                MessageBox.Show("Favor seleccione una fila");
            }
            else
            {
                modo = "EDITAR";
                DesbloquearFormularios();
                txtNombreProveedor.Focus();
            }
        }

        private void frmProveedor_Load(object sender, EventArgs e)
        {
            ActualizarListaProveedores();
            BloquearFormulario();
        }

        private void ActualizarListaProveedores()
        {
            lstProveedor.DataSource = null;
            lstProveedor.DataSource = Proveedor.ObtenerProveedores();
        }


        private void BloquearFormulario()
        {
            txtNombreProveedor.Enabled = false;
            txtDireccion.Enabled = false;
            txtTelefono.Enabled = false;
            txtMail.Enabled = false;

            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            btnLimpiar.Enabled = false;

            lstProveedor.Enabled = true;
            btnAgregar.Enabled = true;
            btnEliminar.Enabled = true;
            btnModificar.Enabled = true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (this.lstProveedor.SelectedItems.Count == 0)
            {
                MessageBox.Show("Favor seleccione una fila");
            }
            else
            {
                Proveedor p = (Proveedor)lstProveedor.SelectedItem;
                Proveedor.EliminarProveedor(p);
                ActualizarListaProveedores();
                LimpiarFormulario();
            }
        }

        //private void lstProveedor_Click(object sender, EventArgs e)
        //{
        //    Proveedor p = (Proveedor)lstProveedor.SelectedItem;

        //    if (p != null)
        //    {
        //        txtTelefono.Text = p.Telefono;
        //        txtDireccion.Text = p.Direccion;
        //        txtMail.Text = p.Email;
        //        txtNombreProveedor.Text = p.NombreProveedor;
        //    }
        //}

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            BloquearFormulario();
        }

        private void lstProveedor_DoubleClick(object sender, EventArgs e)
        {
            Proveedor p = (Proveedor)lstProveedor.SelectedItem;

            if (p != null)
            {
                txtTelefono.Text = p.Contacto;
                txtDireccion.Text = p.Direccion;
                txtMail.Text = p.Email;
                txtNombreProveedor.Text = p.RazonSocial;
            } 

            tbcPrincipal.SelectedIndex = 0;
        }
    }
}
