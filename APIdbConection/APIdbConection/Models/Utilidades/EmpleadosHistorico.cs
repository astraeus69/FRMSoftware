namespace APIdbConection.Models.Utilidades
{
    public class EmpleadosHistorico
    {
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Departamento { get; set; }
        public string Puesto { get; set; }
        public string Direccion { get; set; }
        public string Colonia { get; set; }
        public string CP { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string Telefono { get; set; }
        public string Estatus { get; set; }
        public DateTime FechaTraspaso { get; set; }
    }
}
