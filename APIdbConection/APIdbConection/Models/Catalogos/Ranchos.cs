using System;
using System.Collections.Generic;

namespace APIdbConection.Models.Catalogos;

public partial class Ranchos
{
    public int IdRancho { get; set; }

    public string NombreRancho { get; set; } = null!;

    public string NumeroRancho { get; set; } = null!;

    public decimal SuperficieHa { get; set; }

    public decimal SuperficieAcres { get; set; }

    public string Direccion { get; set; } = null!;

    public string Cp { get; set; } = null!;

    public string Municipio { get; set; } = null!;

    public string Estado { get; set; } = null!;

}
