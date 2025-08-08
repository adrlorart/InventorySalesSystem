using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySalesSystem
{
    internal class Entidades
    {
        public class Venta
        {
            public int Id_Venta { get; set; }
            public int Id_Cliente { get; set; }
            public DateTime Fecha { get; set; }
            public decimal Total { get; set; }
            public DateTime Fecha_Entrega { get; set; }
        }
        public class DetalleVenta
        {
            public int Id_Detalle { get; set; }
            public int Id_Venta { get; set; }
            public int Id_Producto { get; set; }
            public int Cantidad { get; set; }
            public decimal Precio_Unit { get; set; }
        }
        public class Producto
        {
            public int Id_Producto { get; set; }
            public string Nombre { get; set; }
            public string Categoria { get; set; }
            public string Descrip { get; set; }
            public decimal Precio_unit { get; set; }
            public int Exist { get; set; }
            
        }
        public class Cliente
        {
            public int Id_cliente { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Email { get; set; }
            public string Telefono { get; set; }
            public string Direccion { get; set; }
        }

    }
}
