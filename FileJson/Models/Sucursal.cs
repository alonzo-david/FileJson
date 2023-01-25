using System;
using System.Collections.Generic;

namespace FileJson.Models;

public partial class Sucursal
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public int IdEmpresa { get; set; }

    public virtual List<Colaborador> Colaboradores { get; } = new List<Colaborador>();

    public virtual Empresa IdEmpresaNavigation { get; set; } = null!;
}
