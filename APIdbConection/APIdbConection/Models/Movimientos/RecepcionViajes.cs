using System;

namespace APIdbConection.Models.Movimientos;

public partial class RecepcionViajes
{
    public int IdRecepcion { get; set; }

    public int IdViaje { get; set; }

    public DateOnly FechaRecepcion { get; set; }

    public int NumSemRecepcion { get; set; }

    public TimeOnly HoraRecepcion { get; set; }

    public TimeOnly HoraInspeccion { get; set; }
}
