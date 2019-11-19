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
    public partial class frmCategoria : Form
    {
        string modo;
        public frmCategoria()
        {
            InitializeComponent();
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            var p = ObtenerDatosFormulario();


            if (modo == "AGREGAR")
            {
                Categoria.AgregarCategoria(p);
            }
            else if (modo == "EDITAR")
            {

                if (this.lstCategoria.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Favor seleccione una fila");
                }

                else
                {
                    int indice = lstCategoria.SelectedIndex;
                    Categoria.EditarCategoria(p, indice);
                    ActualizarListaCategorias();
                }


            }

            LimpiarFormulario();
            ActualizarListaCategorias();
            BloquearFormulario();


        }


        private Categoria ObtenerDatosFormulario()
        {
            Categoria categoria = new Categoria();
            categoria.Nombre = txtNombre.Text;
            categoria.Descripcion = txtDescripcion.Text;
            

            return categoria;

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            modo = "AGREGAR";
            LimpiarFormulario();
            DesbloquearFormularios();
            txtNombre.Focus();
        }

        private void DesbloquearFormularios()
        {
            txtNombre.Enabled = true;
            txtDescripcion.Enabled = true;
            

            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            btnLimpiar.Enabled = true;

            lstCategoria.Enabled = false;
            btnAgregar.Enabled = false;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;


        }


        private void LimpiarFormulario()
        {
            txtNombre.Text = "";
            txtDescripcion.Text = "";
           
        }


        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (this.lstCategoria.SelectedItems.Count == 0)
            {
                MessageBox.Show("Favor seleccione una fila");
            }
            else
            {
                modo = "EDITAR";
                DesbloquearFormularios();
                txtNombre.Focus();
            }
        }

        private void frmCategoria_Load(object sender, EventArgs e)
        {
            ActualizarListaCategorias();
            BloquearFormulario();
        }

        private void ActualizarListaCategorias()
        {
            lstCategoria.DataSource = null;
            lstCategoria.DataSource = Categoria.ObtenerCategorias();
        }


        private void BloquearFormulario()
        {
            txtNombre.Enabled = false;
            txtDescripcion.Enabled = false;
           

            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            btnLimpiar.Enabled = false;

            lstCategoria.Enabled = true;
            btnAgregar.Enabled = true;
            btnEliminar.Enabled = true;
            btnModificar.Enabled = true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (this.lstCategoria.SelectedItems.Count == 0)
            {
                MessageBox.Show("Favor seleccione una fila");
            }
            else
            {
                Categoria p = (Categoria)lstCategoria.SelectedItem;
                Categoria.EliminarCategoria(p);
                ActualizarListaCategorias();
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

        private void lstCategoria_DoubleClick(object sender, EventArgs e)
        {
            Categoria p = (Categoria)lstCategoria.SelectedItem;

            if (p != null)
            {
                
                txtDescripcion.Text = p.Descripcion;
                
                txtNombre.Text = p.Nombre;
            }

            tbcCategoria.SelectedIndex = 0;
        }
    }
}

