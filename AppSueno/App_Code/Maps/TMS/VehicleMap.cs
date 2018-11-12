using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

/// <summary>
/// Descripción breve de VehicleMap
/// </summary>
public class VehicleMap:ClassMap<Vehicle>
{
    public VehicleMap()
    {
        Table("Vehicle");
        Schema("Vehicles");
        Id(x => x.alias).Column("Alias");
        Map(x => x.tag).Column("Tag");
        Map(x => x.Performance_Type).Column("Performance_Type");
    }
}