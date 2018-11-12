using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Drivers
/// </summary>
public class Driver
{
    public virtual int id { get; set; }
    public virtual String License { get; set; }
    public virtual DateTime vigencia { get; set; }
    public Driver()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
}