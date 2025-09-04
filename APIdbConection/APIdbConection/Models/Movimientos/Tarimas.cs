using System;
using System.Collections.Generic;

namespace APIdbConection.Models.Movimientos;

public partial class Tarimas
{
    public int IdTarima { get; set; }
    public int IdProduccion { get; set; }
    public int IdViaje { get; set; }
    public int CantidadCajasViaje { get; set; }
    public int Licencia { get; set; }
    public decimal KilosProcesoViaje { get; set; }

}
