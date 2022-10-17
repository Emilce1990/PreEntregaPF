
using ConsoleApp1;
using System;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using System.Security.Cryptography;


var listaUsuarios = new List<Usuario>();
var listaProductos = new List<Producto>();
var listaProductosVendido = new List<ProductoVendido>();
var listaVenta = new List<Venta>();

SqlConnectionStringBuilder conecctionbuilder = new();
conecctionbuilder.DataSource = "PC-PC";
conecctionbuilder.InitialCatalog = "SistemaGestion";
conecctionbuilder.IntegratedSecurity = true;
var cs = conecctionbuilder.ConnectionString;

using (SqlConnection connection = new SqlConnection(cs))
{
    Usuario usuario = new Usuario();
    usuario.TraerUsuario();
    
    Producto producto = new Producto();
    producto.TraerProducto();

    ProductoVendido productoVendUs = new ProductoVendido();
    productoVendUs.TarerProductosVendidos();

    Venta ventas = new Venta();
    ventas.TraerVentas();

    usuario.InicioSesion();
}
