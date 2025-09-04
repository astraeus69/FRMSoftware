namespace APIdbConection.Models.Utilidades
{
    public class PersonalCosechaHistorico
    {
        public int IdPersonalCosecha { get; set; }
        public int IdCosecha { get; set; }
        public int IdEmpleado { get; set; }
        public int? Jarras { get; set; }
        public decimal? PrecioJarra { get; set; }
        public DateTime FechaTraspaso { get; set; }
    }
}
