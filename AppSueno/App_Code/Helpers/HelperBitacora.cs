using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;

/// <summary>
/// Descripción breve de HelperBitacora
/// </summary>
public class HelperBitacora
{
    public static void getEventMonitor(List<Dreams> dreams,int user_id,DateTime fecha,Boolean isBitacora)
    {
        /**
         * El parametro Fecha corresponde a la fecha_fin que se le pondra al 
         * evento actual, es decir el evento que esta en monitor.
         **/
        using (ISession session = NHibernateSession.openSession())
        {
            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    List<Monitor> monitor=null;
                    if (isBitacora) {
                         monitor= session.QueryOver<Monitor>().Where(x => x.Usuario_Id == user_id).And(x => x.Actividad_Actual_Id != (int)StatusActividad.ACTIVO).List().ToList();
                        //Aqui entrara para la app de la Bitacora.
                    }
                    else
                    {
                        monitor = session.QueryOver<Monitor>().
                            Where(x => x.Usuario_Id == user_id).List().ToList();
                        //Aqui entrara para la app de Monitor.
                    }
                    /**
                     * La bandera isBitacora sirve para identificar si el llamado al metodo es para la Bitacora, ya que en bitacora solo
                     * nos interesa los de estado inactivo, para la app de monitor tambien nos importan los activos.
                     * **/
                    Dreams nuevo = new Dreams();
                    var m = monitor.Single();
                    var existe= false;
                    foreach (Dreams dream in dreams)
                    {
                        if (dream.fecha_inicio == m.Fecha_Inicio) {
                            /**Si entra indica que el evento ya esta en dreams**/
                            dream.fecha_fin = fecha;
                            existe = true;
                            break;
                         } 
                    }
                    if (!existe)
                    {
                            nuevo.fecha_inicio = m.Fecha_Inicio;
                            nuevo.fecha_fin = fecha;
                            nuevo.id = m.Id;
                            nuevo.usuario_id = m.Usuario_Id;
                            nuevo.tipo_actividad_id = m.Actividad_Actual_Id;
                            if (isBitacora)
                                dreams.Add(nuevo);
                            else
                                dreams.Insert(0, nuevo);//Agrega el evento de monitor en el primer indice para la app de Monitor.

                    }
                    else{
                        /*
                        var temporal = dreams.ElementAt(dreams.Count - 1);//El ultimo elemento
                        dreams.Insert(0, temporal);
                        dreams.RemoveAt(dreams.Count-1);*/
                    }
                }

            }catch(Exception e)
            {

            }
            finally
            {
                session.Close();
            }

        }
    }
}