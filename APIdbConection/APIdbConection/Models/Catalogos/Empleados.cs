using System;
using System.Collections.Generic;

namespace APIdbConection.Models.Catalogos;

public partial class Empleados
{
    public int IdEmpleado { get; set; }

    public string Nombre { get; set; } = null!;

    public string Departamento { get; set; } = null!;

    public string Puesto { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Colonia { get; set; } = null!;

    public string Cp { get; set; } = null!;

    public string Municipio { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Estatus { get; set; } = null!;
}
