using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

/// <summary>
/// Descripción breve de LogsysDreamControllerHelper
/// </summary>
public class LogsysDreamControllerHelper
{
    public LogsysDreamControllerHelper()
    {
        
    }

    public static Porcentajes getPorcentajeGeneral(List<Usuario> users)
    {
        var sumVerde = 0.00m;
        var sumAmarillo = 0.00m;
        var sumRojo = 0.00m;
        foreach(var user in users)
        {
            sumVerde+=user.monitor.Porcentaje_Verde;
            sumAmarillo += user.monitor.Porcentaje_Amarillo;
            sumRojo += user.monitor.Porcentaje_Rojo;
        }
        var total = users.Count*100.00m;
        var porcentajes = new Porcentajes();
        porcentajes.verde = (sumVerde / total) * 100.00m;
        porcentajes.amarillo = (sumAmarillo / total) * 100.00m;
        porcentajes.rojo = (sumRojo / total) * 100.00m;

        return porcentajes;

    }



    public static void GetDiferenciaFechas(Dreams evento)
    {
        var d = evento.fecha_fin ?? DateTime.Now;
        var diff = d - evento.fecha_inicio;
        evento.duracion= diff.TotalMinutes;

    }


    public static List<Dreams> GetEventosActivos(List<Dreams> eventos,DateTime fecha_fin)
    {
        /**
         * Este metodo agregara los eventos 
         * **/
        List<Dreams> nueva = new List<Dreams>();
        Dreams actual;
        Dreams siguiente;
        Dreams activo;
        //if(fecha_inicio_primero > fecha_fin ) omite el primer registro.
       
        for (int index=0  ; index < eventos.Count; index++)
        {
            if (index == 0)
            {
                var primero = eventos.ElementAt(index);
                if(primero.fecha_inicio > fecha_fin) {
                    
                }
                else
                {

                    actual = eventos.ElementAt(index);
                    LogsysDreamControllerHelper.GetDiferenciaFechas(actual);
                    nueva.Add(actual);
                    /**
                     * A la nueva lista se le agrega el evento actual.
                     * */

                    if (index + 1 < eventos.Count)
                    {
                        siguiente = eventos.ElementAt(index + 1);//Se obtiene el siguiente elemento.
                        if (!actual.fecha_inicio.Equals(siguiente.fecha_fin))
                        {
                            activo = new Dreams();
                            activo.tipo_actividad_id = (int)StatusActividad.ACTIVO;
                            activo.actividad = "activo";
                            DateTime z = siguiente.fecha_fin ?? DateTime.Now;
                            //activo.fecha_inicio = siguiente.fecha_fin;
                            activo.fecha_inicio = z;

                            activo.fecha_fin = actual.fecha_inicio;
                            activo.usuario_id = actual.usuario_id;
                            GetDiferenciaFechas(activo);
                            nueva.Add(activo);
                        }
                    }

                }
            }
            else
            {

                actual = eventos.ElementAt(index);
                LogsysDreamControllerHelper.GetDiferenciaFechas(actual);
                nueva.Add(actual);
                /**
                 * A la nueva lista se le agrega el evento actual.
                 * */

                if (index + 1 < eventos.Count)
                {
                    siguiente = eventos.ElementAt(index + 1);//Se obtiene el siguiente elemento.
                    if (!actual.fecha_inicio.Equals(siguiente.fecha_fin))
                    {
                        activo = new Dreams();
                        activo.tipo_actividad_id = (int)StatusActividad.ACTIVO;
                        activo.actividad = "activo";
                        DateTime s = siguiente.fecha_fin ?? DateTime.Now;
                        //activo.fecha_inicio = siguiente.fecha_fin;
                        activo.fecha_inicio = s;
                        activo.fecha_fin = actual.fecha_inicio;
                        activo.usuario_id = actual.usuario_id;
                        GetDiferenciaFechas(activo);
                        nueva.Add(activo);
                    }
                }
            }
            
        }

        return nueva;

    }

    public static List<Dreams> GetEventosOperador(
        int Id_Operador,
        DateTime Fecha_Inicio,
        DateTime Fecha_Fin)

    {
        using(ISession session = NHibernateSession.openSession())
        {
            List<Dreams> eventos = new List<Dreams>();

            try
            {

                eventos=session.QueryOver<Dreams>().
                    Where(x => x.usuario_id == Id_Operador)
                    .Where(x => x.fecha_inicio >= Fecha_Inicio)
                    .Where(x =>x.fecha_fin <=Fecha_Fin || x.fecha_inicio <= Fecha_Fin)
                    //.OrderBy(x => x.fecha_fin).Desc
                    .OrderBy(x => x.fecha_inicio).Desc
                    .List().ToList();
                /**
                 * Obtiene los eventos de Dream.Dreams.
                 **/
                HelperBitacora.getEventMonitor(eventos, Id_Operador,Fecha_Fin,false);
                eventos = LogsysDreamControllerHelper.GetEventosActivos(eventos,Fecha_Fin);
                /***
                 * Verifica si el evento que esta en Dream.Monitor ya se encuentra en 
                 * Dream.Dreams, en caso de que no se encuentre lo agrega
                 * **/
            }
            catch (Exception e)
            {
                eventos = null;
            }
            finally
            {
                session.Close();
                /**
                 * Se cierra la session.
                 * **/
            }
            return eventos;
        }

    }

    public static List<Usuario> GetMonitor(
        int a1, //Id operador 
        int a2, //Id operador
        int b1, //Semaforo
        int b2, //Semaforo 
        int c1, //Actividad
        int c2, //Actividad
        int d1, //TipoDeOperador
        int d2,//TipoDeOperador
        int e1,//Grupo
        int e2,//Grupo 
        String nombre
        )
    {
        List<Usuario> usuarios=null;
        List<Usuario> nuevos = new List<Usuario>();
        using (ISession session = NHibernateSession.openSession())
        {
            try
            {
                //monitor = session.QueryOver<Monitor>().Where(x =>x.Id >a1).List().ToList();
                usuarios = session.QueryOver<Usuario>().
                    Where(x => x.nombre.IsLike(nombre, MatchMode.Anywhere)).//Filtra de acuerdo al nombre, hace referencia a un LIKE
                    Where(x => x.Id > a1).//IdOperador
                    And(x => x.Id < a2).//IdOperador
                    And(x => x.Group_Id > e1).
                    And(x => x.Group_Id < e2).
                    And(x => x.Driver_Type_Id > d1).
                    And(x => x.Driver_Type_Id < d2).
                    And(x => x.activo == 1).//esActivo
                    OrderBy(x => x.nombre).Asc.
                    List().ToList();
                /**Obtiene a los usuarios que esten entre un rango**/
                foreach(var usuario in usuarios)
                {

                    IList < Monitor >  m= session.QueryOver<Monitor>().
                        Where(x => x.Usuario_Id == usuario.Id).
                        And(x => x.Actividad_Actual_Id > c1).
                        And(x => x.Actividad_Actual_Id < c2).
                        //And(x => x.Actividad_Actual_Id!=3).
                        And(x => x.Color_Id> b1).
                        And(x => x.Color_Id < b2)
                        .List().ToList();
                    if (m.Count() != 0){
                        var a = m.First();
                        usuario.monitor = a; //.First();

                        switch (usuario.monitor.Actividad_Actual_Id)
                        {
                            case (int)StatusActividad.ACTIVO:
                                usuario.monitor.actividad = "Activo";
                                break;
                            case (int)StatusActividad.INACTIVO:
                                usuario.monitor.actividad = "Inactivo";
                                break;
                            case (int)StatusActividad.DESCANSO:
                                usuario.monitor.actividad = "Descanso";
                                break;
                            case (int)StatusActividad.SUENO:
                                usuario.monitor.actividad = "Sueño";
                                break;
                        }

                        nuevos.Add(usuario);
                    }
               }
            }
            catch(Exception e)
            {
                nuevos = null;
            }
            finally
            {
                session.Close();
            }
            
            return nuevos;
        }
    }
    
}