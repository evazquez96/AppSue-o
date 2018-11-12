using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Consultas
/// </summary>
public class Consultas
{
    public Consultas()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public static Dreams GetDreams(int aidi)
    {
        using (ISession session = NHibernateSession.openSession())
        {
            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var lista = session.QueryOver<Dreams>().
                        Where(x => x.id == aidi).List().ToList().First();
                    return lista;
               }
                // getSumaDeDiferencias(dreams);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
                return null;
                //errorLog.Error("Error:\n" + e);
            }
            finally
            {
                session.Close();
            }

        }
    }

    public static Boolean updateData(int[] aidi, DateTime[] dateTimei, DateTime date, int posicion)
    {
        using (ISession session = ConsultaSession.openSession())
        {
            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        var lista = session.QueryOver<Fechas>().Where(x => x.id == aidi[posicion]).SingleOrDefault();

                        var r = session.Load<Fechas>(aidi[posicion]);

                        r.fecha_fin = date;

                        session.Update(r);

                        transaction.Commit();

                    }

                    catch (Exception i)
                    {

                        Console.WriteLine("erroe", i);
                    }
                }
                // getSumaDeDiferencias(dreams);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
                //errorLog.Error("Error:\n" + e);
            }
            finally
            {
                session.Close();
            }

        }
        return true;
    }

    public static IList<Dreams> getData(int id_user, DateTime fecha_inicio)
    {
        using (ISession session = NHibernateSession.openSession())
        {
            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {

                    var lista = session.QueryOver<Dreams>().
                        Where(x => x.usuario_id == id_user).
                        OrderBy(x => x.fecha_inicio).Asc.
                        List().ToList();

                    return lista;

                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
                return null;
            }
            finally
            {
                session.Close();
            }
        }
    }

    public static IList<Dreams> GetDatos(int aidi_user, DateTime fecha_inicio, DateTime fecha_fin)
    {
        using (ISession session = NHibernateSession.openSession())
        {
            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {

                    var lista = session.QueryOver<Dreams>().
                        Where(x => x.usuario_id == aidi_user).
                        And(x => x.fecha_inicio <= fecha_inicio).
                        And(x => x.fecha_fin >= fecha_fin || x.fecha_fin == null).List().ToList();

                    return lista;

                }

            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
                return null;
                //errorLog.Error("Error:\n" + e);
            }
            finally
            {
                session.Close();
            }

        }
     }

    public static void setDreams(DateTime fecha_inicio, DateTime fecha_fin, string comentarios, int usuario_id, int tipo_actividad_id, byte automatico, int semaforo_id)
    {

        using (ISession session = NHibernateSession.openSession())
        {
            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Dreams ob = new Dreams();

                    ob.fecha_inicio = fecha_inicio;
                    ob.fecha_fin = fecha_fin;
                    ob.comentarios = comentarios;
                    ob.usuario_id = usuario_id;
                    ob.tipo_actividad_id = tipo_actividad_id;
                    ob.automatico = automatico;
                    ob.semaforo_id = semaforo_id;

                    session.Save(ob);
                    transaction.Commit();
                }
                // getSumaDeDiferencias(dreams);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());

                //errorLog.Error("Error:\n" + e);
            }
            finally
            {
                session.Close();
            }

        }
    }

}