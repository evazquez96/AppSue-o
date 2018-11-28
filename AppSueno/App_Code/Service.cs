using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;



[WebService(Namespace = "http://app.mexamerik.com")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
[System.Web.Script.Services.ScriptService]
public class Service : System.Web.Services.WebService
{
    public Service () {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod]
    public String insertaSuenoEnActivo(int id_evento_arriba,int id_evento_abajo,String comentarios,DateTime inicio,DateTime fin,bool esPrimero)
    {
        int codigoOperacion = -1;
        String response = "";
        var diferencia = MonitorHelper.diferencia(inicio, fin);
        if (diferencia > 480.00)
        {
            codigoOperacion = 1;
        }
        else if (diferencia < 0.0)
        {
            codigoOperacion = 2;
        }
        else if (diferencia >= 0.0 && diferencia <= 15.00)
        {
            codigoOperacion = 3;
        }
        else
        {
            codigoOperacion= MonitorHelper.insertaSuenoEnActivo(id_evento_arriba, id_evento_abajo, comentarios, inicio, fin, esPrimero);
        }

        switch (codigoOperacion)
        {
            case 0:
                response = "Sueño Insertado con éxito en evento activo";
                break;
            case 1:
                response = "ERROR: La diferencia entre la fecha de inicio y fecha fin es mayor a 8 hrs";
                break;
            case 2:
                response = "ERROR: La fecha inicio del sueño debe ser mayor a la fecha fin del sueño";
                break;
            case 3:
                response = "ERROR: La duración del sueño debe ser mayor a 15 minutos";
                break;
            default:
                response = "ERROR al insertar Sueño";
                break;
        }
        return response;

    }

    [WebMethod]
    public String insertaSueno(int id_evento,String comentarios,DateTime inicio,DateTime fin)
    {
        /**
         * Este servicio inserta un evento de Sueño cuando se tenga un status de sueño, descanso e inactivo.
         * **/
        String response = "";
        int codigoOperacion = -1;
        if (fin == Convert.ToDateTime("0001-01-01 00:00:00"))
        {
            /**
             * Indica que la fecha_fin es null.
             * **/
            codigoOperacion = MonitorHelper.insertarSueno(id_evento, comentarios, inicio, fin);
        }
        else
        {
            
            
            var diferencia = MonitorHelper.diferencia(inicio, fin);
            if (diferencia > 480.00)
            {
                codigoOperacion = 1;
            }
            else if (diferencia < 0.0)
            {
                codigoOperacion = 2;
            }
            else if (diferencia >= 0.0 && diferencia <= 15.00)
            {
                codigoOperacion = 3;
            }
            else
            {
                codigoOperacion = MonitorHelper.insertarSueno(id_evento, comentarios, inicio, fin);
            }
            
        }
        switch (codigoOperacion)
        {
            case 0:
                response = "Sueño Insertado con éxito";
                break;
            case 1:
                response = "ERROR: La diferencia entre la fecha de inicio y fecha fin es mayor a 8 hrs";
                break;
            case 2:
                response = "ERROR: La fecha inicio del sueño debe ser mayor a la fecha fin del sueño";
                break;
            case 3:
                response = "ERROR: La duración del sueño debe ser mayor a 15 minutos";
                break;
            default:
                response = "ERROR al insertar Sueño";
                break;
        }
        return response;
    }



    /*
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public String getOperadores(String name)
    {
        using (ISession session = NHibernateSession.openSession())
        {
            var usuarios = session.QueryOver<Usuario>().
                Where(x => x.nombre.IsLike(name, MatchMode.Anywhere)).List().ToList();
                   
            return new JavaScriptSerializer().Serialize(usuarios);
        }

    }*/


    [WebMethod]
    [ScriptMethod(ResponseFormat=ResponseFormat.Json)]
    public String 
        GetEventosOperador(
        int Id_Operador,
        String Fecha_Inicio,
        String Fecha_Fin)
    {
        DateTime inicio = Convert.ToDateTime(Fecha_Inicio);
        DateTime fin = Convert.ToDateTime(Fecha_Fin);
       
        return new JavaScriptSerializer().Serialize(LogsysDreamControllerHelper.GetEventosOperador(Id_Operador, inicio,fin));

    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public String GetMonitor(//Este Método traera a todos los usuarios, sera invocado cuando todos sean nullos.
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
        //JSonResponseMonitor response = new JSonResponseMonitor();
        /*response.users = LogsysDreamControllerHelper.GetMonitor(
            a1, //Id operador 
            a2, //Id operador
            b1, //Semaforo
            b2, //Semaforo 
            c1, //Actividad
            c2, //Actividad
            d1, //TipoDeOperador
            d2,//TipoDeOperador
            e1,//Grupo
            e2,//Grupo
            nombre
                );*/
       //response.porcentaje=LogsysDreamControllerHelper.getPorcentajeGeneral(response.users);
        return new JavaScriptSerializer().Serialize(LogsysDreamControllerHelper.GetMonitor(
            a1, //Id operador 
            a2, //Id operador
            b1, //Semaforo
            b2, //Semaforo 
            c1, //Actividad
            c2, //Actividad
            d1, //TipoDeOperador
            d2,//TipoDeOperador
            e1,//Grupo
            e2,//Grupo
            nombre
                ));
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public String getRegistroBitacora(int user_id,String alias_unidad,String solicitud,String date) {
        //String f = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        DateTime fecha=Convert.ToDateTime(date);
        List<JsonFormat> list = new List<JsonFormat>();
        JsonFormat format24 = getRegistro(24, user_id, fecha);
        JsonFormat format5_30 = getRegistro(5.50, user_id, fecha);
        list.Add(format24);
        list.Add(format5_30);
        JSonResponse response= new JSonResponse();
        response.list = list;
        response.driver = getDriver(user_id);
        response.vehicle = getInfoUnidad(alias_unidad);
        Shipment s = TMSShipmentControllerHelper.GetShipment(solicitud);
        response.Operation_Type_Alias=TMSShipmentControllerHelper.getOperation_Type_Alias(s.Operation_Type);
        Performance p = getVehiclePerformance(response.vehicle);
        response.Marca_y_Modelo = p.Name;
        //response=getInfoUnidad(response, user_id, alias_unidad);
        //return new JavaScriptSerializer().Serialize(list);
        return new JavaScriptSerializer().Serialize(response);


    }


    private Performance getVehiclePerformance(Vehicle vehicle)
    {
        using (ISession session = NHibernateTMSSession.openSession())
        {

            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {

                    var performance = session.QueryOver<Performance>().Where(x => x.Id ==vehicle.Performance_Type).List().ToList().First();
                    return performance;
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

    private Vehicle getInfoUnidad(String alias)
    {

        using (ISession session = NHibernateTMSSession.openSession())
        {

            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {

                    var vehicle = session.QueryOver<Vehicle>().Where(x => x.alias==alias).List().ToList().First();
                    return vehicle;
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
    private Driver getDriver(int id)
    {
        
        using (ISession session = NHibernateTMSSession.openSession())
        {

            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var driver = session.QueryOver<Driver>().Where(x => x.id == id).List().ToList().First();
                    //response.driver = driver;
                    /*var vehicle = session.QueryOver<Vehicle>().Where(x => x.alias.Equals(alias)).SingleOrDefault();
                    response.vehicle = vehicle;*/
                    return driver;
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

    private JsonFormat getRegistro(double horas,int user_id,DateTime fecha)
    {
        JsonFormat format = new JsonFormat();
        //List<Dreams> dreams=new List<Dreams>();
        using (ISession session = NHibernateSession.openSession())
        {

            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    String f = fecha.ToString("yyyy-MM-dd HH:mm:ss");
                    DateTime b = Convert.ToDateTime(f);
                    DateTime d = fecha.AddHours(-horas);
                    var Dream = session.QueryOver<Dreams>().
                        Where(x => x.usuario_id == user_id).
                        And(x => x.fecha_fin >= d).//d es la fecha menos 24 o menor 5:30
                        And(x => x.fecha_inicio <= b).//b es la fecha con que se llamo el servicio
                        OrderBy(x =>x.fecha_inicio).
                        Asc.List().ToList();
                    /**
                     * Se obtienen los eventos de la tabla Dream.Dreams, este query va a obtener todos los
                     * eventos desde el rango de fechas especificado.
                     * **/

                    HelperBitacora.getEventMonitor(Dream, user_id, fecha,true);//Verifica el evento que esta en Dream.Monitor.
                    format = getFormato(Dream);
                    var last = Dream.First<Dreams>();
                    DateTime a = last.fecha_fin ?? DateTime.Now;
                    TimeSpan s = a - d;
                    //TimeSpan s = last.fecha_fin - d;

                    format.inactivo += s.TotalMinutes;
                    if (horas == 24)
                    {
                        if (format.inactivo > 1400.00)
                            format.inactivo = 1400;
                        else if (format.inactivo < 0.0)
                            format.inactivo = 0.0;
                        else { }

                        format.activo = (24 * 60) - format.inactivo;
                        if (format.activo > 1400.00)
                            format.activo = 1400;
                        else if (format.activo < 0.0)
                            format.activo = 0.0;
                        else { }
                           
                    }
                    else
                    {
                        if (format.inactivo > 330)
                            format.inactivo = 330;
                        else if (format.inactivo < 0.0)
                            format.inactivo = 0.0;
                        else { }

                        format.activo = (5.5 * 60) - format.inactivo;
                        if (format.activo > 330)
                            format.inactivo = 330;
                        else if(format.activo < 0.0)
                            format.activo = 0.0;
                        else { }

                    }

                }
                // getSumaDeDiferencias(dreams);
                return format;
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

    private JsonFormat getFormato(List<Dreams> dreams)
    {
        var sum = 0.0;
        DateTime inicio, fin;
        TimeSpan diferencia;
        DreamsFormatoJSon dfj;
        List<DreamsFormatoJSon> l = new List<DreamsFormatoJSon>();
        JsonFormat format = new JsonFormat();
        var first = dreams.First<Dreams>();

        foreach (var d in dreams)
        {
            DateTime aux = d.fecha_fin ?? DateTime.Now;
            inicio = d.fecha_inicio;
            //fin = d.fecha_fin;
            fin = aux;

            diferencia = fin - inicio;
            dfj = new DreamsFormatoJSon();
            dfj.fecha_inicio = inicio.ToString("yyyy-MM-dd HH:mm:ss");
            dfj.fecha_fin = fin.ToString("yyyy-MM-dd HH:mm:ss");

            if (!d.Equals(first))
            {

                dfj.inactivo = diferencia.TotalMinutes;
                l.Add(dfj);
                sum += diferencia.TotalMinutes;
            }
            else
            {
                l.Add(dfj);
            }

        }
        format.listDreams = l;
        format.inactivo = sum;
        /**
         * Se calcula la diferencia de la ultimma fecha fin
         * con la fecha que se paro al conductor.
         */
        return format;
    }
    
}