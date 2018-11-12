using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Monitor
/// </summary>
public class Monitor
{

    public virtual int Id { get; set; }
    public virtual int Usuario_Id { get; set; }
    public virtual int Actividad_Actual_Id { get; set; }
    public virtual DateTime Fecha_Inicio { get; set; }
    public virtual String Tiempo_Actual {get;set;}
    public virtual decimal Porcentaje_Rojo { get; set; }
    public virtual decimal Porcentaje_Amarillo { get; set; }
    public virtual decimal Porcentaje_Verde { get; set; }
    public virtual String Tiempo_Sueno { get; set; }
    public virtual String Tiempo_Descanso { get; set; }
    public virtual String Tiempo_Activo { get; set; }
    public virtual String Tiempo_Inactivo { get; set; }
    public virtual int Color_Id { get; set; }
    public virtual String Tiempo_Plan { get; set; }
    public virtual String actividad { get; set; }

    public Monitor()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
}