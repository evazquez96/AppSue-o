using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;

/// <summary>
/// Descripción breve de TMSControllerHelper
/// </summary>
public class TMSShipmentControllerHelper
{
    public TMSShipmentControllerHelper()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public static Shipment GetShipment(String solicitud)
    {
        using (ISession session = NHibernateTMSSession.openSession())
        {
            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var shipment = session.QueryOver<Shipment>().Where(x => x.External_Id == solicitud).List().ToList().First();
                    return shipment;
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
    public static String getOperation_Type_Alias(int operation_Type) {
        return operation_Type == 2 ? "T3-S2-R4" : "T3-S2";
    }
}