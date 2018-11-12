using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de FechasMap
/// </summary>
public class FechasMap : ClassMap<Fechas>
{
    public FechasMap()
    {
        Table("Dreams");
        Schema("Dream");
        Id(x => x.id).Column("id").GeneratedBy.Assigned();
        Map(x => x.fecha_fin).Column("fecha_fin");
    }
}