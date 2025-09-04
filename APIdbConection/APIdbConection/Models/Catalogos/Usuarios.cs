using System;
using System.Collections.Generic;

namespace APIdbConection.Models.Catalogos;

public partial class Usuarios
{
    public int IdUsuario { get; set; }

    public string Usuario { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public string Estatus { get; set; } = null!;
}
