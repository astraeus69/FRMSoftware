using System;
using System.Collections.Generic;

namespace APIdbConection.Models.Catalogos;

public partial class Viveros
{
    public int IdVivero { get; set; }

    public string NombreVivero { get; set; } = null!;

    public string CodigoVivero { get; set; } = null!;
}
