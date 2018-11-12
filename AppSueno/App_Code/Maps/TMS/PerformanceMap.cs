using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

/// <summary>
/// Descripción breve de PerformanceMap
/// </summary>
public class PerformanceMap:ClassMap<Performance>
{
    public PerformanceMap()
    {
        Table("Performance");
        Schema("Vehicles");
        Id(x => x.Id).Column("Id");
        Map(x => x.Name).Column("Name");
    }
}