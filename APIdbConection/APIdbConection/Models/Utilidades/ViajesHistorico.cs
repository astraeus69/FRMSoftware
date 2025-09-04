namespace APIdbConection.Models.Utilidades
{
    public class ViajesHistorico
    {
        public int IdViaje { get; set; }
        public int IdVehiculo { get; set; }
        public int IdConductor { get; set; }
        public DateOnly FechaSalida { get; set; }
        public int NumSemViaje { get; set; }
        public string EstadoAprobacion { get; set; }
        public DateTime FechaTraspaso { get; set; }
    }
}
