using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesNegocio
{
   public class Categoria
    {
        public int Id { get; set; }
        
        public string Nombre { get; set; }
        
        public string Descripcion { get; set; }

        public static List<Categoria> listaCategoria = new List<Categoria>();


        public static void AgregarCategoria(Categoria c)
        {
            using (SqlConnection con = new SqlConnection(SqlServer.CADENA_CONEXION))

            {
                con.Open();
                string textoCmd = "insert into Categoria (Nombre,Descripcion) VALUES (@Nombre, @Descripcion)";
                SqlCommand cmd = new SqlCommand(textoCmd, con);
                cmd = c.ObtenerParametros(cmd);
                cmd.ExecuteNonQuery();
            }
        }

        public static void EliminarCategoria(Categoria c)
        {
            using (SqlConnection con = new SqlConnection(SqlServer.CADENA_CONEXION))

            {
                con.Open();
                string SENTENCIA_SQL = "delete from Categoria where Id = @Id";

                SqlCommand cmd = new SqlCommand(SENTENCIA_SQL, con);
                SqlParameter p5 = new SqlParameter("@Id", c.Id);
                p5.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(p5);

                cmd.ExecuteNonQuery();
            }
        }

        public static void EditarCategoria(Categoria c, int indice)
        {

            using (SqlConnection con = new SqlConnection(SqlServer.CADENA_CONEXION))
            {
                con.Open();
                string textoCMD = "UPDATE Categoria SET Nombre = @Nombre, Descripcion = @Descripcion where Id = @Id";

                SqlCommand cmd = new SqlCommand(textoCMD, con);
                cmd = c.ObtenerParametros(cmd, true);
                cmd.ExecuteNonQuery();

            }
        }

        public override string ToString()
        {
            return this.Nombre;
        }

        public static List<Categoria> ObtenerCategorias()
        {
            Categoria categoria;
            listaCategoria.Clear();
            using (SqlConnection con = new SqlConnection(SqlServer.CADENA_CONEXION))
            {
                con.Open();
                string textoCMD = "Select * from Categoria";
                SqlCommand cmd = new SqlCommand(textoCMD, con);
                SqlDataReader elLectorDeDatos = cmd.ExecuteReader();

                while (elLectorDeDatos.Read())
                {
                    categoria = new Categoria();
                    categoria.Id = elLectorDeDatos.GetInt32(0);
                   
                    categoria.Nombre = elLectorDeDatos.GetString(1);
                    
                    categoria.Descripcion = elLectorDeDatos.GetString(2);

                    listaCategoria.Add(categoria);

                }
                return listaCategoria;
            }
        }

        private SqlCommand ObtenerParametros(SqlCommand cmd, Boolean id = false)

        {
            
            SqlParameter p1 = new SqlParameter("@Nombre", this.Nombre);
           
            SqlParameter p2 = new SqlParameter("@Descripcion", this.Descripcion);
         
            
            p1.SqlDbType = SqlDbType.VarChar;
            
            p2.SqlDbType = SqlDbType.VarChar;
            
       
            cmd.Parameters.Add(p1);
            
            cmd.Parameters.Add(p2);
           
            if (id == true)
            {
                cmd = ObtenerParametrosId(cmd);
            }
            return cmd;
        }

        private SqlCommand ObtenerParametrosId(SqlCommand cmd)
        {

            SqlParameter p3 = new SqlParameter("@id", this.Id);
            p3.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(p3);
            return cmd;
        }

        public static Categoria ObtenerCategoria(int id)
        {
            Categoria categoria = null;

            if (listaCategoria.Count == 0)
            {
                Categoria.ObtenerCategorias();
            }

            foreach (Categoria c in listaCategoria)
            {
                if (c.Id == id)
                {
                    categoria = c;
                    break;
                }
            }

            return categoria;
        }

    }
}
