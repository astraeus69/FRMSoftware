using System;

namespace APIdbConection.Models.Movimientos
{
    public partial class VentasDetalles
    {
        public int IdVenta { get; set; }
        public int IdTarima { get; set; }
        public decimal PrecioVentaTarima { get; set; }
        public DateTime FechaRecepcion { get; set; }
    }
}
