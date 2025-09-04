namespace APIdbConection.Models.Movimientos
{
    public class VentasDetallesCompletos
    {
        public int IdVenta { get; set; }
        public DateOnly FechaFacturacion { get; set; }
        public decimal TotalVenta { get; set; }
        public decimal PrecioDolar { get; set; }

        // Datos de viaje
        public int IdViaje { get; set; }
        public DateOnly FechaSalida { get; set; }
        public int NumSemViaje { get; set; }

        // Rancho
        public int IdRancho { get; set; }
        public string? NombreRancho { get; set; }
        public string? NumeroRancho { get; set; }

        // Cultivo
        public int IdCultivo { get; set; }
        public string? TipoBerry { get; set; }
        public string? Variedad { get; set; }

        // Datos de trima
        public int IdTarima { get; set; }
        public int CantidadCajasViaje { get; set; }
        public int Licencia { get; set; }
        public decimal KilosProcesoViaje { get; set; }

        public DateTime FechaRecepcion { get; set; }
        public decimal PrecioVentaTarima { get; set; }
    }
}
