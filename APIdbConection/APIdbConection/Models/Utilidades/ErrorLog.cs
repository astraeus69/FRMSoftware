namespace APIdbConection.Models.Utilidades
{
    public class ErrorLog
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? ErrorMessage { get; set; }
        public string? ErrorProcedure { get; set; }
        public string? ErrorLine { get; set; }
        public DateTime ErrorTime { get; set; }
    }

}
