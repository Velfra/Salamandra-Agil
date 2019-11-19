using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesNegocio
{
    public class Pedido
    {
        public int Id { get; set; }
        public Proveedor proveedor { get; set; }
        public DateTime fecha_llegada { get; set; }
        public List<PedidoDetalle> detalle_pedidos = new List<PedidoDetalle>();

        public static List<Pedido> listaPedidos = new List<Pedido>();

        public static void Agregar(Pedido p)
        {
            
            using (SqlConnection con = new SqlConnection(SqlServer.CADENA_CONEXION))
            {
                con.Open();
                
                string textoCMD = "INSERT INTO Pedido (fecha_llegada, Proveedor) output INSERTED.id VALUES (@fecha_llegada, @proveedor)";
                SqlCommand cmd = new SqlCommand(textoCMD, con);

                
                SqlParameter p1 = new SqlParameter("@fecha_llegada", p.fecha_llegada);
                SqlParameter p2 = new SqlParameter("@proveedor", p.proveedor.Id);

                p1.SqlDbType = System.Data.SqlDbType.DateTime;
                p2.SqlDbType = System.Data.SqlDbType.Int;
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);

                int pedido_id = (int)cmd.ExecuteScalar();

                
                foreach (PedidoDetalle dp in p.detalle_pedidos)
                {
                    
                    string textoCMD2 = "INSERT INTO Pedido_Detalle(pedido_id,cantidad, producto) VALUES (@pedido_id, @cantidad, @producto)";
                    SqlCommand cmd2 = new SqlCommand(textoCMD2, con);

                    

                    SqlParameter p3 = new SqlParameter("@pedido_id", pedido_id);
                    SqlParameter p4 = new SqlParameter("@cantidad", dp.cantidad);
                    SqlParameter p5 = new SqlParameter("@producto", dp.producto.Id);

                    cmd2.Parameters.Add(p3);
                    cmd2.Parameters.Add(p4);
                    cmd2.Parameters.Add(p5);

                    cmd2.ExecuteNonQuery();
                }
            }
        }
        public static void Eliminar(Pedido p)
        {
            listaPedidos.Remove(p);
        }

        public static List<Pedido> Obtener()
        {
            return listaPedidos;
        }

        public override string ToString()
        {
            return this.proveedor.RazonSocial;
        }

    }
}
