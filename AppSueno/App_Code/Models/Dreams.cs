using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Dreams
/// </summary>
public class Dreams
{
        /**
         * Modelo para la tabla Dream.Dreams
         */

        
        public virtual int id { get; set; }
        public virtual DateTime fecha_inicio { get; set;}
        public virtual System.Nullable<System.DateTime> fecha_fin { get; set; }
        public virtual int usuario_id { get; set; }
        public virtual int semaforo_id { get; set; }
        public virtual int tipo_actividad_id { get; set; }
        public virtual double duracion { get; set; }//La duración del evento se dara en minutos.
        public virtual String actividad { get; set; }
        public virtual String comentarios { get; set; }
        public virtual int sql_id { get; set; }
        public virtual Byte automatico { get; set; }

        public virtual  Dreams GetTemporal()
    {
        Dreams d = new Dreams();
        d.fecha_inicio = this.fecha_inicio;
        d.fecha_fin = this.fecha_fin;
        d.usuario_id = this.usuario_id;
        d.semaforo_id = this.semaforo_id;
        d.tipo_actividad_id = this.tipo_actividad_id;
        d.duracion = this.duracion;
        d.actividad = this.actividad;
        d.comentarios = this.comentarios;
        d.sql_id = this.sql_id;
        d.automatico = this.automatico;
        return d;

    }
}