using System;
using System.Collections.Generic;

namespace APIdbConection.Models.Catalogos;

public partial class Vehiculos
{
    public int IdVehiculo { get; set; }

    public string? Placas { get; set; }

    public string? Modelo { get; set; }

    public string? Marca { get; set; }
}
