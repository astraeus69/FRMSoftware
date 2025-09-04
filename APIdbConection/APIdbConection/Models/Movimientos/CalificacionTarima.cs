using System;
using System.Collections.Generic;

namespace APIdbConection.Models.Movimientos;

public partial class CalificacionTarima
{
    public int IdCalificacion { get; set; }

    public int IdTarima { get; set; }

    public string EstadoAprobacion { get; set; } = null!;

}
