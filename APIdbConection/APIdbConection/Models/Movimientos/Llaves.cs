using System;
using System.Collections.Generic;

namespace APIdbConection.Models.Movimientos;

public partial class Llaves
{
    public int IdLlave { get; set; }

    public int IdRancho { get; set; }

    public string NombreLlave { get; set; } = null!;

    public decimal SuperficieHa { get; set; }

    public decimal SuperficieAcres { get; set; }

    public int CantidadTuneles { get; set; }

    public string Disponibilidad { get; set; } = null!;

}
