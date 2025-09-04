namespace APIdbConection.Models.Movimientos
{
    public class ReplantesDetalles
    {
        // Datos del Replante
        public int IdReplante { get; set; }
        public int? CantidadReplante { get; set; }
        public DateTime FechaReplante { get; set; } = DateTime.Now;
        public int NumSemReplante { get; set; }


        // Datos de la Plantación
        public int IdPlantacion { get; set; }
        public DateTime FechaPlantacion { get; set; }
        public int NumSemPlantacion { get; set; }
        public string? EstatusPlantacion { get; set; }

        // Datos del Cultivo
        public int IdCultivo { get; set; }
        public string? TipoBerry { get; set; }
        public string? Variedad { get; set; }

        // Datos de la Llave
        public int IdLlave { get; set; }
        public string? NombreLlave { get; set; }
        public decimal? SuperficieHa { get; set; }
        public decimal? SuperficieAcres { get; set; }
        public string? Disponibilidad { get; set; }

        // Datos del Rancho
        public int IdRancho { get; set; }
        public string? NombreRancho { get; set; }
        public string? NumeroRancho { get; set; }

        // Datos del Vivero
        public int IdVivero { get; set; }
        public string? NombreVivero { get; set; }
        public string? CodigoVivero { get; set; }

    }

}
