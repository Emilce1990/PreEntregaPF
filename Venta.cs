using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Venta
    {
        public int Id { get; set; }
        public string Comentario { get; set; }

        public Venta()
        {
            Id = 0;
            Comentario = string.Empty;
        }

        public void TraerVentas()
        {
            var listaProductos = new List<Producto>();

            SqlConnectionStringBuilder conecctionbuilder = new();
            conecctionbuilder.DataSource = "PC-PC";
            conecctionbuilder.InitialCatalog = "SistemaGestion";
            conecctionbuilder.IntegratedSecurity = true;
            var cs = conecctionbuilder.ConnectionString;

            using (SqlConnection connection = new SqlConnection(cs))
            {

                Console.WriteLine("\n");
                Console.WriteLine("---TRAER VENTAS---");

                connection.Open();

                SqlCommand cmd2 = connection.CreateCommand();
                cmd2.CommandText = "select pr.Id, pr.Descripciones, pr.Costo, pr.PrecioVenta, pr.Stock from ProductoVendido as pv inner join Producto as pr on pr.Id = pv.Id";
                var reader2 = cmd2.ExecuteReader();


                while (reader2.Read())
                {
                    var producto = new Producto();

                    producto.Id = Convert.ToInt32(reader2.GetValue(0));
                    producto.Descripciones = reader2.GetValue(1).ToString();
                    producto.Costo = Convert.ToDouble(reader2.GetValue(2));
                    producto.PrecioVenta = Convert.ToDouble(reader2.GetValue(3));
                    producto.Stock = Convert.ToInt32(reader2.GetValue(4));

                    listaProductos.Add(producto);

                }

                bool listoVentas = false; ;

                foreach (var producto in listaProductos)
                {
                    Console.WriteLine("\t\tId de Producto: " + producto.Id);
                    Console.WriteLine("\t\tProducto: " + producto.Descripciones);
                    Console.WriteLine("\t\tCosto: " + producto.Costo);
                    Console.WriteLine("\t\tPrecioVenta:" + producto.PrecioVenta);
                    Console.WriteLine("\t\tStock: " + producto.Stock);
                    Console.WriteLine("-----------");
                    listoVentas = true;

                }
                if (!listoVentas) Console.WriteLine("**No hubo ventas de productos***");
                reader2.Close();
            }

        }

    }
}

