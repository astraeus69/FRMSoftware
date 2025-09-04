using System;
using System.Collections.Generic;

namespace APIdbConection.Models.Catalogos;

public partial class Cultivos
{
    public int IdCultivo { get; set; }

    public string TipoBerry { get; set; } = null!;

    public string Variedad { get; set; } = null!;
}
