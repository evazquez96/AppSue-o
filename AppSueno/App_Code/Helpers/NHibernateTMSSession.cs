using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
/// <summary>
/// Descripción breve de NHibernateTMSSession
/// </summary>
public class NHibernateTMSSession
{

    private static ISessionFactory sessionFactory = null;
    /*
     * El objeto de tipo ISessionFactory se encargara de realizar
     * la conexión a la Base de Datos.
     */

    private static ISessionFactory SessionFactory
    {
        get
        {
            if (sessionFactory == null)
                CreateSessionFactory();
            return sessionFactory;
        }
    }

    public static void CreateSessionFactory()
    {
        /**
         * Esta funcion se encargara de crear una conexion de caso
         * de que no se tenga una.
         */
        sessionFactory = Fluently.Configure()
        .Database(MsSqlConfiguration.MsSql2005
        .ConnectionString(c => c.FromConnectionStringWithKey("tms_mexa"))

        ).Mappings(m => m.FluentMappings
                        .AddFromAssemblyOf<VehicleMap>()
                        ).BuildSessionFactory();
    }

    public static ISession openSession()
    {

        return SessionFactory.OpenSession();

    }
}