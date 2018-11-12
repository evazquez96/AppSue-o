using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

/// <summary>
/// Descripción breve de ShipmentMap
/// </summary>
public class ShipmentMap:ClassMap<Shipment>
{
    public ShipmentMap()
    {
        Table("Shipment");
        Schema("Shipping");
        Id(x => x.Id).Column("Id");
        Map(x => x.External_Id).Column("External_Id");
        Map(x => x.Operation_Type).Column("Operation_Type");
    }
}