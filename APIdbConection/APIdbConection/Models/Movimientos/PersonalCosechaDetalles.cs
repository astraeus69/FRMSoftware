namespace APIdbConection.Models.Movimientos
{
    public class PersonalCosechaDetalles
    {
        // Datos de jarras
        public int IdPersonalCosecha { get; set; }
        public int Jarras { get; set; }
        public decimal PrecioJarra { get; set; }


        // Cosecha
        public int IdCosecha { get; set; }
        public DateTime FechaCosecha { get; set; }
        public int NumSemCosecha { get; set; }


        // Datos del Cultivo
        public int IdCultivo { get; set; }
        public string? TipoBerry { get; set; }
        public string? Variedad { get; set; }


        // Datos del empleado
        public int IdEmpleado { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Estatus { get; set; }
    }
}
