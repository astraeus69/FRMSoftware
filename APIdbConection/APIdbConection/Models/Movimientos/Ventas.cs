using System;

namespace APIdbConection.Models.Movimientos
{
    public partial class Ventas
    {
        public int IdVenta { get; set; }
        public DateOnly FechaFacturacion { get; set; }
        public decimal Total { get; set; }
        public decimal PrecioDolar { get; set; }
    }
}
