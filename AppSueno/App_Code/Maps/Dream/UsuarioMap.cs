using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de UsuarioMap
/// </summary>
public class UsuarioMap:ClassMap<Usuario>
{
    public UsuarioMap()
    {
        Schema("Catalogos");
        Table("Usuario");
        Id(x => x.Id).Column("Id");
        Map(x => x.nombre).Column("nombre");
        Map(x => x.Driver_Type_Id).Column("Driver_Type_Id");
        Map(x => x.Group_Id).Column("Group_Id");
        Map(x => x.activo).Column("activo");
    }
}