using System;
using System.Collections.Generic;

namespace APIdbConection.Models.Movimientos;

public partial class Viajes
{
    public int IdViaje { get; set; }

    public int IdVehiculo { get; set; }

    public int IdConductor { get; set; }

    public DateOnly FechaSalida { get; set; }

    public int NumSemViaje { get; set; }

    public string EstadoAprobacion { get; set; } = null!;

}
