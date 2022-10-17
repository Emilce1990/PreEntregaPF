using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Producto
    {
        public int Id { get; set; }
        public string Descripciones { get; set; }
        public double Costo { get; set; }
        public double PrecioVenta { get; set; }
        public int Stock { get; set; }
        public int IdUsuario { get; set; }

        public Producto()
        {
            Id = 0;
            Descripciones = string.Empty;
            Costo = 0;
            PrecioVenta = 0;
            Stock = 0;
            IdUsuario = 0;
        }




        public void TraerProducto()
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
                Console.WriteLine("---TRAER PRODUCTOS CARGADOS POR USUARIO---");
                int idUsuario = 0;
                Console.WriteLine("Ingrese id del usuario: ");

                try
                {
                    idUsuario = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception error)
                {
                    Console.WriteLine("El Id de Usuario debe ser numerico. " + error.Message);
                }

                connection.Open();

                SqlCommand cmd2 = connection.CreateCommand();
                cmd2.CommandText = "select pr.Id, pr.Descripciones, pr.Costo, pr.PrecioVenta, pr.Stock, us.Id from Producto as pr inner join Usuario as us on pr.IdUsuario = us.Id where us.Id = idUsuario";
                var reader2 = cmd2.ExecuteReader();


                while (reader2.Read())
                {
                    var producto = new Producto();

                    producto.Id = Convert.ToInt32(reader2.GetValue(0));
                    producto.Descripciones = reader2.GetValue(1).ToString();
                    producto.Costo = Convert.ToDouble(reader2.GetValue(2));
                    producto.PrecioVenta = Convert.ToDouble(reader2.GetValue(3));
                    producto.Stock = Convert.ToInt32(reader2.GetValue(4));
                    producto.IdUsuario = Convert.ToInt32(reader2.GetValue(5));

                    listaProductos.Add(producto);

                }
                bool cargoProd = false;

                foreach (var producto in listaProductos)
                {
                    if (producto.IdUsuario == idUsuario)
                    {
                    Console.WriteLine("\t\tProducto cargado por el usuario:");
                    Console.WriteLine("\t\tId de Producto: " + producto.Id);
                    Console.WriteLine("\t\tProducto: " + producto.Descripciones);
                    Console.WriteLine("\t\tCosto: " + producto.Costo);
                    Console.WriteLine("\t\tPrecioVenta:" + producto.PrecioVenta);
                    Console.WriteLine("\t\tStock: " + producto.Stock);
                    Console.WriteLine("\t\tIdUsuario: " + producto.IdUsuario);
                    Console.WriteLine("-----------");
                    cargoProd = true;
                    }
                }
                if (!cargoProd) Console.WriteLine("**El usuario ingresado no ha cargado productos***");
                reader2.Close();
            }

        }

       
        

    }
}