using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesNegocio
{
    public class Proveedor
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string Contacto { get; set; }
        public string Email { get; set; }

        public static List<Proveedor> listaProveedores = new List<Proveedor>();

        public static void AgregarProveedor(Proveedor p)
        {
           
            using (SqlConnection con = new SqlConnection(SqlServer.CADENA_CONEXION))

            {
                con.Open(); 
                string textoCmd = "insert into Proveedor (RazonSocial, Direccion, Contacto, Email) VALUES (@RazonSocial, @Direccion, @Contacto, @Email)";
                SqlCommand cmd = new SqlCommand(textoCmd, con);

                //PARAMETROS
                SqlParameter p1 = new SqlParameter("@RazonSocial", p.RazonSocial);
                SqlParameter p2 = new SqlParameter("@Direccion", p.Direccion);
                SqlParameter p3 = new SqlParameter("@Contacto", p.Contacto);
                SqlParameter p4 = new SqlParameter("@Email", p.Email);

                
                p1.SqlDbType = SqlDbType.VarChar;
                p2.SqlDbType = SqlDbType.VarChar;
                p3.SqlDbType = SqlDbType.VarChar;
                p4.SqlDbType = SqlDbType.VarChar;

                
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);

                cmd.ExecuteNonQuery();

            }



        }
        public static void EliminarProveedor(Proveedor p)
        {
           
            using (SqlConnection con = new SqlConnection(SqlServer.CADENA_CONEXION))

            {
                con.Open();
                string SENTENCIA_SQL = "delete from Proveedor where Id = @Id";

                SqlCommand cmd = new SqlCommand(SENTENCIA_SQL, con);
                SqlParameter p5 = new SqlParameter("@Id", p.Id);
                p5.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(p5);

                cmd.ExecuteNonQuery();
            }
        }

        public static void EditarProveedor(int index, Proveedor p)
        {
           
            using (SqlConnection con = new SqlConnection(SqlServer.CADENA_CONEXION))
            {
                con.Open();
                string textoCMD = "UPDATE Proveedor SET RazonSocial = @razonSocial, Direccion = @direccion, Contacto = @contacto, Email = @email where Id = @Id";

                SqlCommand cmd = new SqlCommand(textoCMD, con);

               
                SqlParameter p1 = new SqlParameter("@razonSocial", p.RazonSocial);
                SqlParameter p2 = new SqlParameter("@direccion", p.Direccion);
                SqlParameter p3 = new SqlParameter("@contacto", p.Contacto);
                SqlParameter p4 = new SqlParameter("@email", p.Email);
                SqlParameter p5 = new SqlParameter("@Id", p.Id);

               
                p1.SqlDbType = SqlDbType.VarChar;
                p2.SqlDbType = SqlDbType.VarChar;
                p3.SqlDbType = SqlDbType.VarChar;
                p4.SqlDbType = SqlDbType.VarChar;
                p5.SqlDbType = SqlDbType.Int;

                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p5);

                cmd.ExecuteNonQuery();
            }
        }

        public static Proveedor ObtenerProveedor(int id)
        {
            Proveedor proveedor = null;

            if (listaProveedores.Count == 0)
            {
                Proveedor.ObtenerProveedores();
            }

            foreach (Proveedor p in listaProveedores)
            {
                if (p.Id == id)
                {
                    proveedor = p;
                    break;
                }
            }

            return proveedor;
        }

        public static List<Proveedor> ObtenerProveedores()
        {


            Proveedor proveedor;
            listaProveedores.Clear();
            using (SqlConnection con = new SqlConnection(SqlServer.CADENA_CONEXION))

            {
                con.Open();
                string textoCMD = "Select * from Proveedor";

                SqlCommand cmd = new SqlCommand(textoCMD, con);

                SqlDataReader elLectorDeDatos = cmd.ExecuteReader();

                while (elLectorDeDatos.Read())
                {
                    proveedor = new Proveedor();
                    proveedor.Id = elLectorDeDatos.GetInt32(0);
                    proveedor.RazonSocial = elLectorDeDatos.GetString(1);
                    proveedor.Direccion = elLectorDeDatos.GetString(2);
                    proveedor.Contacto = elLectorDeDatos.GetString(3);
                    proveedor.Email = elLectorDeDatos.GetString(4);

                    listaProveedores.Add(proveedor);
                }

                return listaProveedores;

            }

        }

        public override string ToString()
        {
            
            return RazonSocial;
        }


    }
}
