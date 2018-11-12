using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Vehicle
/// </summary>
public class Vehicle
{
    public virtual String alias { get; set; }
    public virtual String tag { get; set; }
    public virtual int Performance_Type { get; set; }
    public Vehicle()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
}