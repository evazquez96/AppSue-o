using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Recalculo
/// </summary>
public class Recalculo
{
    public virtual int id { get; set; }
    public virtual int Id_Evento { get; set; }
    public virtual int Procesado { get; set; }

    public Recalculo()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
}