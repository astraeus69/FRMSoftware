using System;
using System.Collections.Generic;

namespace APIdbConection.Models.Movimientos;

public partial class Podas
{
    public int IdPoda { get; set; }

    public int IdPlantacion { get; set; }

    public string TipoPoda { get; set; } = null!;

    public DateTime FechaPoda { get; set; }

    public int NumSemPoda { get; set; }

}
