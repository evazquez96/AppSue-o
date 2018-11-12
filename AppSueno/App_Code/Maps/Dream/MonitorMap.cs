using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

/// <summary>
/// Descripción breve de MonitorMap
/// </summary>
public class MonitorMap : ClassMap<Monitor>
{
    public MonitorMap()
    {
        Table("Monitor");
        Schema("Dream");
        Id(x => x.Id).Column("Id");
        Map(x => x.Usuario_Id).Column("Usuario_Id");
        Map(x => x.Actividad_Actual_Id).Column("Actividad_Actual_Id");
        Map(x => x.Fecha_Inicio).Column("Fecha_Inicio");
        Map(x => x.Porcentaje_Amarillo).Column("Porcentaje_Amarillo");
        Map(x => x.Porcentaje_Rojo).Column("Porcentaje_Rojo");
        Map(x => x.Porcentaje_Verde).Column("Porcentaje_Verde");
        Map(x => x.Tiempo_Actual).Column("Tiempo_Actual");
        Map(x => x.Tiempo_Activo).Column("Tiempo_Activo");
        Map(x => x.Tiempo_Descanso).Column("Tiempo_Descanso");
        Map(x => x.Tiempo_Sueno).Column("Tiempo_Sueno");
        Map(x => x.Tiempo_Inactivo).Column("Tiempo_Inactivo");
        Map(x => x.Color_Id).Column("Color_Id");
        Map(x => x.Tiempo_Plan).Column("Tiempo_Plan");
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
}