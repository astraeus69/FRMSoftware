namespace APIdbConection.Models.Utilidades
{
    public class VehiculosHistorico
    {
        public int IdVehiculo { get; set; }
        public string Placas { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public DateTime FechaTraspaso { get; set; }
    }
}
