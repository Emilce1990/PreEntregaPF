using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class ProductoVendido
    {
        public int Id { get; set; }
        public int Stock { get; set; }
        public int IdProducto { get; set; }
        public int IdVenta { get; set; }

        public ProductoVendido()
        {
            Id = 0;
            Stock = 0;
            IdProducto = 0;
            IdVenta = 0;
        }

        public void TarerProductosVendidos()
        {
            var listaProductos = new List<Producto>();
            var listaUsuario = new List<Usuario>();

            SqlConnectionStringBuilder conecctionbuilder = new();
            conecctionbuilder.DataSource = "PC-PC";
            conecctionbuilder.InitialCatalog = "SistemaGestion";
            conecctionbuilder.IntegratedSecurity = true;
            var cs = conecctionbuilder.ConnectionString;

            using (SqlConnection connection = new SqlConnection(cs))
            {
                Console.WriteLine("\n");
                Console.WriteLine("---TRAER PRODUCTOS VENDIDOS POR USUARIO---");
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
                SqlCommand cmd3 = connection.CreateCommand();
                cmd3.CommandText = "select pr.Id, pr.Descripciones, pr.Costo, pr.PrecioVenta, pr.Stock, IdUsuario from ProductoVendido as pv inner join Producto as pr on pr.Id = pv.Id where IdUsuario = idUsuario";

                var reader3 = cmd3.ExecuteReader();

                while (reader3.Read())
                {
                    var producto = new Producto();
                    var usuario = new Usuario();

                    producto.Id = Convert.ToInt32(reader3.GetValue(0));
                    producto.Descripciones = reader3.GetValue(1).ToString();
                    producto.Costo = Convert.ToInt32(reader3.GetValue(2));
                    producto.PrecioVenta = Convert.ToInt32(reader3.GetValue(3));
                    producto.Stock = Convert.ToInt32(reader3.GetValue(4));
                    usuario.Id = Convert.ToInt32(reader3.GetValue(5));

                    listaProductos.Add(producto);
                    listaUsuario.Add(usuario);
                }



                bool vendidoPorUs = false; 

                foreach (var usuario in listaUsuario)
                {
                    if (idUsuario == usuario.Id)
                    {
                        vendidoPorUs = true;
                        Console.WriteLine("----Los productos vendidos por el usuario fueron:");
                    } 

                     foreach (var producto in listaProductos)
                    {
                        if (vendidoPorUs && idUsuario == producto.IdUsuario)
                        {
                            Console.WriteLine("\t\tId: " + producto.Id);
                            Console.WriteLine("\t\tDescripcion: " + producto.Descripciones);
                            Console.WriteLine("\t\tCosto: " + producto.Costo);
                            Console.WriteLine("\t\tPrecio venta: " + producto.PrecioVenta);
                            Console.WriteLine("\t\tStock: " + producto.Stock);
                            Console.WriteLine("-----------");
                        }
                    }
                    if (vendidoPorUs) break;
                }
                
                if (vendidoPorUs && idUsuario > 1) Console.WriteLine("**El usuario no tiene ventas***");

                reader3.Close();

            }

        }

        
    }
}