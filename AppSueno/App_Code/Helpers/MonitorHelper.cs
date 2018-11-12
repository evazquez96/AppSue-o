using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
public class MonitorHelper
{
    public MonitorHelper()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public static double diferencia(DateTime inicio, DateTime? fin)
    {
        var f = fin ?? DateTime.Now;
        return (f - inicio).TotalMinutes;
    }

 
    public static int codigoOperacion(Dreams evento,DateTime inicio,DateTime fin)
    {
        int codigo=0;

        if (diferencia(inicio, evento.fecha_inicio) == 0.0 && diferencia(fin, evento.fecha_fin) == 0)
        {
            codigo = 1;
        }else if (diferencia(inicio, evento.fecha_inicio)==0 && diferencia(fin, evento.fecha_fin) != 0.0)
        {
            codigo = 2;
        }else if (diferencia(inicio, evento.fecha_inicio) != 0 && diferencia(fin, evento.fecha_fin) == 0.0)
        {
            /**
             * Indica que la fecha fin coicide exactamente con la fecha fin del evento.
             * **/
            codigo = 3;
        }
        else
        {
            codigo = 4;
        }
        return codigo;
    }

    public static int codigoOperacionEnActivo(Dreams arriba,Dreams abajo, DateTime inicio, DateTime fin)
    {
        int codigo = 0;
        if (diferencia(inicio, arriba.fecha_fin) == 0.0 && diferencia(fin, abajo.fecha_inicio) == 0)
        {
            codigo = 1;
        }
        else if (diferencia(inicio, arriba.fecha_fin) == 0 && diferencia(fin, abajo.fecha_inicio) > 0.0)
        {
            codigo = 2;
        }
        else if (diferencia(arriba.fecha_inicio, inicio) > 0 && diferencia(fin, abajo.fecha_inicio) == 0.0)
        {
            /**
             * Indica que la fecha fin coicide exactamente con la fecha fin del evento.
             * **/
            codigo = 3;
        }else if (diferencia(arriba.fecha_inicio, inicio) > 0 && diferencia(fin, abajo.fecha_inicio) > 0.0)
        {
            codigo = 4;
        }
        else
        {
            codigo = 5;
        }
        return codigo;
    }


    public static int calcularSemaforoId(DateTime inicio,DateTime? fin)
    {
        double diff = diferencia(inicio, fin);

        if (diff < (5.0 * 60.0))
            return 3;
        else if (diff >= (5.0 * 60.0) && diff < (6.0 * 60.0))
        {
            return 2;
        }
        else
        {
            return 1;
        }

    }


    public static int insertaSuenoEnActivo(int id_evento_arriba, int id_evento_abajo, String comentarios, DateTime inicio, DateTime fin, bool esPrimero)
    {
        int response = -1;
        String c = comentarios.Equals("") ? "Sueño Manual" : comentarios;
        if (esPrimero)
        {
            using (ISession session = NHibernateSession.openSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {

                    try
                    {
                        Dreams evento_arriba = session.QueryOver<Dreams>().Where(x => x.id == id_evento_arriba).List().Single();//Obtiene el evento de arriba.
                        Dreams nuevo = new Dreams();
                        nuevo.fecha_inicio = inicio;
                        nuevo.fecha_fin = fin;
                        nuevo.comentarios = c;
                        nuevo.usuario_id = evento_arriba.usuario_id;
                        nuevo.tipo_actividad_id = (int)StatusActividad.SUENO;
                        nuevo.sql_id = 0;
                        nuevo.automatico = 0;
                        nuevo.semaforo_id = calcularSemaforoId(inicio, fin);
                        session.Save(nuevo);
                        transaction.Commit();
                        response = 0;

                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        response = -1;
                    }
                    finally
                    {
                        if (session != null)
                            session.Close();//Cerramos la sesión.
                    }
                }
            }

        }
        else
        {
            using (ISession session = NHibernateSession.openSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {

                    try
                    {
                        Dreams evento_arriba = session.QueryOver<Dreams>().Where(x => x.id == id_evento_arriba).List().Single();//Obtiene el evento de arriba.
                        Dreams evento_abajo = session.QueryOver<Dreams>().Where(x => x.id == id_evento_abajo).List().Single();//Obtiene el evento de abajo.
                        int codigoOperacion = codigoOperacionEnActivo(evento_arriba, evento_abajo, inicio, fin);
                        Dreams nuevo = new Dreams();

                        switch (codigoOperacion)
                        {
                            case 1:
                                nuevo.fecha_inicio = evento_arriba.fecha_fin??DateTime.Now;
                                nuevo.fecha_fin = evento_abajo.fecha_inicio;
                                nuevo.comentarios = c;
                                nuevo.usuario_id = evento_abajo.usuario_id;
                                nuevo.tipo_actividad_id = (int)StatusActividad.SUENO;
                                nuevo.sql_id = 0;
                                nuevo.automatico = 0;
                                nuevo.semaforo_id = calcularSemaforoId(inicio, fin);
                                session.Save(nuevo);
                                transaction.Commit();
                                response = 0;
                                break;
                            case 2:
                                nuevo.fecha_inicio = evento_arriba.fecha_fin ?? DateTime.Now;
                                nuevo.fecha_fin = fin;
                                nuevo.comentarios = c;
                                nuevo.usuario_id = evento_abajo.usuario_id;
                                nuevo.tipo_actividad_id = (int)StatusActividad.SUENO;
                                nuevo.sql_id = 0;
                                nuevo.automatico = 0;
                                DateTime z = evento_arriba.fecha_fin ?? DateTime.Now;
                                nuevo.semaforo_id = calcularSemaforoId(z, fin);
                                session.Save(nuevo);
                                transaction.Commit();
                                response = 0;
                                break;
                            case 3:
                                nuevo.fecha_inicio = inicio;
                                nuevo.fecha_fin = evento_abajo.fecha_inicio;
                                nuevo.comentarios = c;
                                nuevo.usuario_id = evento_abajo.usuario_id;
                                nuevo.tipo_actividad_id = (int)StatusActividad.SUENO;
                                nuevo.sql_id = 0;
                                nuevo.automatico = 0;
                                nuevo.semaforo_id = calcularSemaforoId(inicio, evento_abajo.fecha_inicio);
                                session.Save(nuevo);
                                transaction.Commit();
                                response = 0;
                                break;
                            case 4:
                                nuevo.fecha_inicio = inicio;
                                nuevo.fecha_fin = fin;
                                nuevo.comentarios = c;
                                nuevo.usuario_id = evento_abajo.usuario_id;
                                nuevo.tipo_actividad_id = (int)StatusActividad.SUENO;
                                nuevo.sql_id = 0;
                                nuevo.automatico = 0;
                                nuevo.semaforo_id = calcularSemaforoId(inicio, fin);
                                session.Save(nuevo);
                                transaction.Commit();
                                response = 0;
                                break;
                            default:
                                break;
                        }

                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        response = -1;
                    }
                    finally
                    {
                        if (session != null)
                            session.Close();//Cerramos la sesión.
                    }
                }
            }

        }



        return response;
    }


    public static int insertarSueno(int id_evento,String comentarios, DateTime inicio, DateTime fin)
    {
        int response = -1;
        String c = comentarios.Equals("") ? "Sueño Manual" : comentarios;
        using (ISession session = NHibernateSession.openSession())
        {
            using (ITransaction transaction = session.BeginTransaction())
            {

                try
                {
                    Dreams evento = session.QueryOver<Dreams>().Where(x => x.id == id_evento).List().Single();//Obtiene el primer elemento
                    Dreams temporal;
                    temporal = evento.GetTemporal();
                    int codigoOp;
                    DateTime aux = Convert.ToDateTime("0001-01-01");

                    //Inicio
                    if (evento.fecha_fin == null)
                    {
                        int auxActividad = evento.tipo_actividad_id;

                        /**
                         * El if Verifica si la fecha_fin es null
                         * **/
                        if (evento.fecha_inicio == inicio)
                        {//Verifica si la fecha_inicio del evento coincide con la fecha inicio del sueño
                            evento.fecha_fin = fin;
                            evento.tipo_actividad_id = (int)StatusActividad.SUENO;
                            evento.comentarios = c;
                            evento.semaforo_id = calcularSemaforoId(evento.fecha_inicio, evento.fecha_fin);
                            Dreams nuevo = new Dreams();
                            nuevo.fecha_inicio = fin;
                            nuevo.fecha_fin = null;
                            //nuevo.comentarios = c;
                            nuevo.comentarios = "Automatico";
                            nuevo.usuario_id = evento.usuario_id;
                            //nuevo.tipo_actividad_id = (int)StatusActividad.SUENO;
                            nuevo.tipo_actividad_id = auxActividad;
                            //nuevo.sql_id = 0;
                            nuevo.sql_id = 999;
                            nuevo.automatico = 0;
                            //nuevo.semaforo_id = calcularSemaforoId(inicio, fin);
                            session.Update(evento);
                            session.Save(nuevo);
                            response = 0;
                        }else if (inicio > evento.fecha_inicio)
                        {
                            evento.fecha_fin = inicio;
                            evento.semaforo_id = calcularSemaforoId(evento.fecha_inicio, evento.fecha_fin);
                            Dreams nuevo = new Dreams();
                            var d = evento.fecha_fin ?? DateTime.Now;
                            nuevo.fecha_inicio = d;
                            nuevo.fecha_fin = fin;
                            nuevo.comentarios = c;
                            nuevo.usuario_id = evento.usuario_id;
                            nuevo.tipo_actividad_id = (int)StatusActividad.SUENO;
                            nuevo.sql_id = 0;
                            nuevo.automatico = 0;
                            Dreams nuevo2 = new Dreams();
                            //var b = evento.fecha_fin ?? DateTime.Now;
                            nuevo2.fecha_inicio = fin;
                            nuevo2.fecha_fin = null;
                            nuevo2.comentarios = "Automatico";
                            nuevo2.comentarios = evento.comentarios;
                            nuevo2.usuario_id = evento.usuario_id;
                            nuevo2.tipo_actividad_id = evento.tipo_actividad_id;
                            nuevo2.sql_id = 999;
                            nuevo2.automatico = 0;
                            session.Update(evento);
                            session.Save(nuevo);
                            session.Save(nuevo2);
                            response = 0;
                            //nuevo.semaforo_id = calcularSemaforoId(inicio, fin);
                        }
                    }
                    else
                    {
                        codigoOp= codigoOperacion(evento, inicio, fin);
                        switch (codigoOp)
                        {
                            case 1://Cambia el status actual del evento a sueño.
                                evento.tipo_actividad_id = (int)StatusActividad.SUENO;
                                evento.comentarios = c;
                                session.Update(evento);
                                response = 0;
                                break;
                            case 2://La fecha inicio del sueño coincide con la fecha_inicio del evento
                                evento.tipo_actividad_id = (int)StatusActividad.SUENO;
                                evento.comentarios = c;
                                evento.fecha_fin = fin;
                                evento.semaforo_id = calcularSemaforoId(evento.fecha_inicio, evento.fecha_fin);
                                temporal.fecha_inicio = fin;
                                session.Save(temporal);
                                session.Update(evento);
                                response = 0;
                                break;
                            case 3://La fecha fin del sueño coincide con la fecha_fin del evento pero el inicio del sueño no coincide con la fecha_inicio del evento
                                evento.fecha_inicio = inicio;
                                evento.comentarios = c;
                                evento.semaforo_id = calcularSemaforoId(evento.fecha_inicio, evento.fecha_fin);
                                temporal.fecha_fin = inicio;
                                session.Save(temporal);
                                session.Update(evento);
                                response = 0;
                                break;
                            case 4://Cuando el inicio y el fin del sueño no coiciden exactamente con la fecha_inicio y fecha_fin del evento.
                                evento.fecha_fin = inicio;
                                evento.semaforo_id = calcularSemaforoId(evento.fecha_inicio, evento.fecha_fin);
                                Dreams nuevo = new Dreams();
                                nuevo.fecha_inicio = inicio;
                                nuevo.fecha_fin = fin;
                                nuevo.comentarios = c;
                                nuevo.usuario_id = evento.usuario_id;
                                nuevo.tipo_actividad_id = (int)StatusActividad.SUENO;
                                nuevo.sql_id = 0;
                                nuevo.automatico = 0;
                                nuevo.semaforo_id = calcularSemaforoId(inicio, fin);
                                Dreams nuevo2 = new Dreams();
                                nuevo2.fecha_inicio = fin;
                                nuevo2.fecha_fin = temporal.fecha_fin;
                                nuevo2.comentarios = temporal.comentarios;
                                nuevo2.usuario_id = temporal.usuario_id;
                                nuevo2.tipo_actividad_id = temporal.tipo_actividad_id;
                                nuevo2.sql_id = temporal.sql_id;
                                nuevo.automatico = temporal.automatico;
                                nuevo.semaforo_id = calcularSemaforoId(fin, temporal.fecha_fin);
                                session.Save(nuevo);
                                session.Save(nuevo2);
                                session.Update(evento);
                                response = 0;
                                break;
                            default:
                                break;
                        }
                        //fin
                    }

                    transaction.Commit();

                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    response = -1;
                }
                finally
                {
                    if (session != null)
                        session.Close();//Cerramos la sesión.
                }
            }
        }

            return response;
    }
}