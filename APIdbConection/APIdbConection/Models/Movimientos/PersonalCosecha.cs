using System;
using System.Collections.Generic;

namespace APIdbConection.Models.Movimientos;

public partial class PersonalCosecha
{
    public int IdPersonalCosecha { get; set; }
    public int IdCosecha { get; set; }
    public int IdEmpleado { get; set; }
    public int? Jarras { get; set; }
    public decimal? PrecioJarra { get; set; }
}
