namespace APIdbConection.Models.Movimientos
{
    public class LlavesConRancho
    {
        public int IdLlave { get; set; }
        public int IdRancho { get; set; }
        public string NombreLlave { get; set; } = null!;
        public decimal SuperficieHa { get; set; }
        public decimal SuperficieAcres { get; set; }
        public int CantidadTuneles { get; set; }
        public string Disponibilidad { get; set; } = null!;

        // Datos del rancho asociado
        public string NombreRancho { get; set; } = null!;
        public string NumeroRancho { get; set; } = null!;

    }
}
