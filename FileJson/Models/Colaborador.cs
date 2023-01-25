using System;
using System.Collections.Generic;

namespace FileJson.Models;

public partial class Colaborador
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Cui { get; set; } = null!;

    public int IdSucursal { get; set; }

    public virtual Sucursal IdSucursalNavigation { get; set; } = null!;
}
