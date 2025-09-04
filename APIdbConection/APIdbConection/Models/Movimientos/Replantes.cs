using System;
using System.Collections.Generic;

namespace APIdbConection.Models.Movimientos;

public partial class Replantes
{
    public int IdReplante { get; set; }

    public int IdPlantacion { get; set; }

    public int IdCultivo { get; set; }

    public int IdVivero { get; set; }

    public int CantidadReplante { get; set; }

    public DateTime FechaReplante { get; set; }

    public int NumSemReplante { get; set; }

}
