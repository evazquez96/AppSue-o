using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;


/// <summary>
/// Descripción breve de DriversMap
/// </summary>
public class DriversMap:ClassMap<Driver>
{
    public DriversMap()
    {
        Table("Driver");
        Schema("Drivers");
        Id(x => x.id).Column("Id");
        Map(x => x.License).Column("License");
        Map(x => x.vigencia).Column("License_Expiration");
        
    }
}