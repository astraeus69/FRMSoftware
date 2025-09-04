namespace APIdbConection.Models.Login
{
    public class ValidarContrasenaRequest
    {
        public int IdUsuario { get; set; }
        public string ContrasenaActual { get; set; } = string.Empty;
    }
}
