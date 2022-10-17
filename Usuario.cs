using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public string Mail { get; set; }

        public Usuario()
        {
            Id = 0;
            Nombre = String.Empty;
            Apellido = String.Empty;
            NombreUsuario = String.Empty;
            Contraseña = String.Empty;
            Mail = String.Empty;

        }

        public void TraerUsuario()
        {
            var listaUsuarios = new List<Usuario>();

            SqlConnectionStringBuilder conecctionbuilder = new();
            conecctionbuilder.DataSource = "PC-PC";
            conecctionbuilder.InitialCatalog = "SistemaGestion";
            conecctionbuilder.IntegratedSecurity = true;
            var cs = conecctionbuilder.ConnectionString;

            using (SqlConnection connection = new SqlConnection(cs))
            {
        
             Console.WriteLine("---TRAER USUARIO---");
             Console.WriteLine("Ingrese nombre de usuario: ");
            string nombreUsuIngresado = Console.ReadLine();

            connection.Open();
            SqlCommand cmd1 = connection.CreateCommand();
            cmd1.CommandText = "SELECT * FROM usuario";
            var reader1 = cmd1.ExecuteReader();

            while (reader1.Read())
            {
                var usuarios = new Usuario();
                usuarios.Id = Convert.ToInt32(reader1.GetValue(0));
                usuarios.Nombre = reader1.GetValue(1).ToString();
                usuarios.Apellido = reader1.GetValue(2).ToString();
                usuarios.NombreUsuario = reader1.GetValue(3).ToString();
                usuarios.Contraseña = reader1.GetValue(4).ToString();
                usuarios.Mail = reader1.GetValue(5).ToString();

                listaUsuarios.Add(usuarios);
            }

                bool listoDatos = false;

                foreach (var usuarios in listaUsuarios)
                {   
                    if (nombreUsuIngresado.Equals(usuarios.NombreUsuario))
                    {
                        Console.WriteLine("\t\tEl listado de datos del usuario es:");
                        Console.WriteLine("\t\tId: " + usuarios.Id);
                        Console.WriteLine("\t\tNombre: " + usuarios.Nombre);
                        Console.WriteLine("\t\tApellido: " + usuarios.Apellido);
                        Console.WriteLine("\t\tNombreUsuario: " + usuarios.NombreUsuario);
                        Console.WriteLine("\t\tContraseña: " + usuarios.Contraseña);
                        Console.WriteLine("\t\tMail: " + usuarios.Mail);
                        listoDatos = true;
                    }
                }
                if (!listoDatos) Console.WriteLine("**Nombre de usuario invalido***");
            }
        }


        public void InicioSesion()
        {
            var listaUsuarios = new List<Usuario>();

            SqlConnectionStringBuilder conecctionbuilder = new();
            conecctionbuilder.DataSource = "PC-PC";
            conecctionbuilder.InitialCatalog = "SistemaGestion";
            conecctionbuilder.IntegratedSecurity = true;
            var cs = conecctionbuilder.ConnectionString;

            using (SqlConnection connection = new SqlConnection(cs))
            {

                Console.WriteLine("---INICIO DE SESION---");
                Console.WriteLine("Ingrese Nombre de Usuario: ");
                string nombreUsuIngresado = Console.ReadLine();

                Console.WriteLine("Ingrese Contraseña: ");
                string passIngresada = Console.ReadLine();

                connection.Open();
                SqlCommand cmd1 = connection.CreateCommand();
                cmd1.CommandText = "SELECT Id, Nombre, Apellido, NombreUsuario, Contraseña, Mail FROM usuario";
                var reader1 = cmd1.ExecuteReader();

                while (reader1.Read())
                {
                    var usuarios = new Usuario();

                    usuarios.Id = Convert.ToInt32(reader1.GetValue(0));
                    usuarios.Nombre = reader1.GetValue(1).ToString();
                    usuarios.Apellido = reader1.GetValue(2).ToString();
                    usuarios.NombreUsuario = reader1.GetValue(3).ToString();
                    usuarios.Contraseña = reader1.GetValue(4).ToString();
                    usuarios.Mail = reader1.GetValue(5).ToString();

                    listaUsuarios.Add(usuarios);
                }

                bool loguin = false;

                while (!loguin)
                {
                    foreach (var usuarios in listaUsuarios)
                    {
                        if (nombreUsuIngresado.Equals(usuarios.NombreUsuario) && usuarios.Contraseña.Equals(passIngresada))
                        {
                            Console.WriteLine("\n");
                            Console.WriteLine("Bienvenido " + usuarios.Nombre);
                            Console.WriteLine("\n");
                            Console.WriteLine("Su id es: " + usuarios.Id);
                            Console.WriteLine("Su Nombre es: " + usuarios.Nombre);
                            Console.WriteLine("Su Apellido es: " + usuarios.Apellido);
                            Console.WriteLine("Su NombreUsuario es: " + usuarios.NombreUsuario);
                            Console.WriteLine("Su Contraseña es: " + usuarios.Contraseña);
                            Console.WriteLine("Su Mail es: " + usuarios.Mail);
                           
                            loguin = true;

                        }
                        if (loguin) break;
                    }
                    if (loguin) break;

                    Console.WriteLine("Nombre de usuario o contraseña invalido");
                    Console.WriteLine("\n");
                    Console.WriteLine("Ingrese nombre de usuario: ");
                    nombreUsuIngresado = Console.ReadLine();
                    Console.WriteLine("Ingrese Contraseña: ");
                    passIngresada = Console.ReadLine();
                }

                reader1.Close();
            }
        }




           
    }

}