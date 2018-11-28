using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

/// <summary>
/// Descripción breve de RecalculoMap
/// </summary>
public class RecalculoMap:ClassMap<Recalculo>
{
    public RecalculoMap()
    {
        Table("Recalculo");
        Schema("Dream");
        Id(x => x.id).Column("Id");
        Map(x => x.Id_Evento).Column("Id_Evento");
        Map(x => x.Procesado).Column("Procesado");
    }
}