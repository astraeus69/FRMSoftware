namespace APIdbConection.Models.Login
{
    public class LoginResponse
    {
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Rol { get; set; }
        public string Nombre { get; set; }
        public string Token { get; set; }
    }

}
