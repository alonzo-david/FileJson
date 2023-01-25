using System;
using System.Collections.Generic;

namespace FileJson.Models;

public partial class Empresa
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Pais { get; set; }

    public virtual List<Sucursal> Sucursales { get; } = new List<Sucursal>();
}
