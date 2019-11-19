using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesNegocio
{
    //public enum Categoria { Limpieza, Marron, Rojo, Azul, Dorado, Marfil }
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public Categoria Categoria { get; set; }
        public double PrecioCompra { get; set; }
        public Proveedor Proveedor { get; set; }
        public DateTime FechaPedido { get; set; }

        public static List<Producto> listaProducto = new List<Producto>();


        public static void AgregarProducto(Producto p)
        {
            using (SqlConnection con = new SqlConnection(SqlServer.CADENA_CONEXION))

            {
                con.Open();
                string textoCmd = "insert into Producto (Nombre, Cantidad, Categoria, PrecioCompra, Proveedor, Fecha_pedido) VALUES (@Nombre, @Cantidad, @Categoria, @PrecioCompra, @Proveedor, @Fecha_pedido)";
                SqlCommand cmd = new SqlCommand(textoCmd, con);
                cmd = p.ObtenerParametros(cmd);
                cmd.ExecuteNonQuery();
            }
        }

        public static void EliminarProducto(Producto p)
        {
            using (SqlConnection con = new SqlConnection(SqlServer.CADENA_CONEXION))

            {
                con.Open();
                string SENTENCIA_SQL = "delete from Producto where Id = @Id";

                SqlCommand cmd = new SqlCommand(SENTENCIA_SQL, con);
                SqlParameter p5 = new SqlParameter("@Id", p.Id);
                p5.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(p5);

                cmd.ExecuteNonQuery();
            }
        }

        public static void EditarProducto(Producto p, int indice)
        {

            using (SqlConnection con = new SqlConnection(SqlServer.CADENA_CONEXION))
            {
                con.Open();
                string textoCMD = "UPDATE Producto SET Nombre = @Nombre, Cantidad = @Cantidad, Categoria = @Categoria, PrecioCompra = @PrecioCompra, Proveedor = @proveedor, Fecha_pedido = @Fecha_pedido where Id = @Id";

                SqlCommand cmd = new SqlCommand(textoCMD, con);
                cmd = p.ObtenerParametros(cmd, true);
                cmd.ExecuteNonQuery();

            }
        }

        public override string ToString()
        {
            return this.Nombre;
        }

        public static List<Producto> ObtenerProductos()
        {
            Producto producto;
           
            listaProducto.Clear();
            using (SqlConnection con = new SqlConnection(SqlServer.CADENA_CONEXION))
            {
                con.Open();
                string textoCMD = "Select * from Producto";
                SqlCommand cmd = new SqlCommand(textoCMD, con);
                SqlDataReader elLectorDeDatos = cmd.ExecuteReader();

                while (elLectorDeDatos.Read())
                {
                    producto = new Producto();
                    producto.Id = elLectorDeDatos.GetInt32(0);
                    producto.Nombre = elLectorDeDatos.GetString(1);
                    producto.Cantidad = elLectorDeDatos.GetInt32(2);
                    producto.Categoria = Categoria.ObtenerCategoria(elLectorDeDatos.GetInt32(3));
                    producto.PrecioCompra = elLectorDeDatos.GetDouble(4);
                    producto.Proveedor = Proveedor.ObtenerProveedor(elLectorDeDatos.GetInt32(5));
                    producto.FechaPedido = elLectorDeDatos.GetDateTime(6);


                    listaProducto.Add(producto);
                }
                return listaProducto;
            }
        }

        

        private SqlCommand ObtenerParametros(SqlCommand cmd, Boolean id = false)

        {
            SqlParameter p1 = new SqlParameter("@Nombre", this.Nombre);
            SqlParameter p2 = new SqlParameter("@Cantidad", this.Cantidad);
            SqlParameter p3 = new SqlParameter("@Categoria", this.Categoria.Id);
            SqlParameter p4 = new SqlParameter("@PrecioCompra", this.PrecioCompra);
            SqlParameter p5 = new SqlParameter("@Proveedor", this.Proveedor.Id);
            SqlParameter p6 = new SqlParameter("@Fecha_pedido", DateTime.Today);


            p1.SqlDbType = SqlDbType.VarChar;
            p2.SqlDbType = SqlDbType.Int;
            p3.SqlDbType = SqlDbType.Int;
            p4.SqlDbType = SqlDbType.Float;
            p5.SqlDbType = SqlDbType.Int;
            p6.SqlDbType = SqlDbType.DateTime;


            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
            cmd.Parameters.Add(p6);


            if (id == true)
            {
                cmd = ObtenerParametrosId(cmd);
            }
            return cmd;
        }

        

        private SqlCommand ObtenerParametrosId(SqlCommand cmd)
        {

            SqlParameter p9 = new SqlParameter("@id", this.Id);
            p9.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(p9);
            return cmd;
        }

        public static List<Producto> BuscarProducto()
        {
            Producto producto;
            listaProducto.Clear();
            using (SqlConnection con = new SqlConnection(SqlServer.CADENA_CONEXION))

            {
                con.Open();
                string textoCMD = "Select * from Proveedor";

                SqlCommand cmd = new SqlCommand(textoCMD, con);

                SqlDataReader elLectorDeDatos = cmd.ExecuteReader();

                while (elLectorDeDatos.Read())
                {
                    producto = new Producto();
                    producto.Id = elLectorDeDatos.GetInt32(0);
                    producto.Nombre = elLectorDeDatos.GetString(1);
                    
                    listaProducto.Add(producto);
                }

                return listaProducto;

            }

        }

    }
}
