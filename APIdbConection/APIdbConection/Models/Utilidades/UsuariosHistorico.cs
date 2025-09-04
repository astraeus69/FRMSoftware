namespace APIdbConection.Models.Utilidades
{
    public class UsuariosHistorico
    {
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }
        public string Estatus { get; set; }
        public DateTime FechaTraspaso { get; set; }
    }
}
