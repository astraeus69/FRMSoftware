using System;
using System.Collections.Generic;

namespace APIdbConection.Models.Movimientos;

public partial class Cosechas
{
    public int IdCosecha { get; set; }
    public int IdPlantacion { get; set; }
    public DateTime FechaCosecha { get; set; }
    public int NumSemCosecha { get; set; }

}
