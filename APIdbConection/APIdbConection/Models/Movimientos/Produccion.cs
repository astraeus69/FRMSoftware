using System;
using System.Collections.Generic;

namespace APIdbConection.Models.Movimientos;

public partial class Produccion
{
    public int IdProduccion { get; set; }
    public int IdCosecha { get; set; }
    public string TipoCaja { get; set; } = string.Empty;
    public int CantidadCajas { get; set; }
    public decimal KilosProceso { get; set; }

}
