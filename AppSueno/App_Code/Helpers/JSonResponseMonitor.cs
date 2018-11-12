using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de JSonResponse
/// </summary>
public class JSonResponseMonitor
{
    public virtual List<Usuario> users { get; set; }
    public virtual Porcentajes porcentaje { get; set; }

    public JSonResponseMonitor()
    {

    }
}