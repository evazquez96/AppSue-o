using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace MonitorSueño.Controllers
{
    public class GetSuenosController : ApiController
    {
        [HttpGet]
        public List<Response> GetAllSuenos(int user_id, long inicio, long fin) {
            WebRequest request = WebRequest.Create("http://tms.logsys.com.mx/Dream/Sueno/Sueno.svc/consultarsuenos");
            request.Method = "POST";
            Data d = new Data();

            string fecha_final = "/Date(" + fin + "000-0500)/";
            string fecha_inicio = "/Date(" + inicio + "000-0500)/";
            d.usuario_id = user_id;
            d.fechaInicial = fecha_inicio;
            d.fechaFinal = fecha_final;
            var jsonData = JsonConvert.SerializeObject(d);
            string postData = jsonData;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            //Console.WriteLine(responseFromServer);
            reader.Close();
            dataStream.Close();
            response.Close();
            var lista = JsonConvert.DeserializeObject<List<Response>>(responseFromServer);
            var aux = new List<Response>();
            int auto_increment = 0;
            foreach(Response item in lista)
            {
                var nuevo = new Response();
                nuevo.color_id = item.color_id;
                nuevo.comentarios = item.comentarios;
                nuevo.fechaFin = item.fechaFin;
                nuevo.fechaFinDate = item.fechaFinDate;
                nuevo.fechaInicio = item.fechaInicio;
                nuevo.fechaInicioDate = item.fechaInicioDate;
                nuevo.id = item.id;
                nuevo.int_automatica = item.int_automatica;
                nuevo.IsMexApp = item.IsMexApp;
                nuevo.tipoActividad = item.tipoActividad;
                nuevo.usuario = item.usuario;
                nuevo.auto_increment = auto_increment;
                aux.Add(nuevo);
                auto_increment += 1;
            }
            return aux;
        }
        [Route("GetSuenos/insert/{comentarios}/{fFin}/{fInicio}/{id}/{sqlId}/{tipoActividad}/{usuarioId}")]
        [HttpGet]
        public String GetAllSuenos(String comentarios,long fFin,long fInicio,String id,String sqlId,String tipoActividad,String usuarioId)
        {
            WebRequest request = WebRequest.Create("http://tms.logsys.com.mx/Dream/Sueno/Sueno.svc/guardar");
            request.Method = "POST";

            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            DateTime inicio = start.AddMilliseconds(fInicio).ToLocalTime();
            DateTime fin = start.AddMilliseconds(fFin).ToLocalTime();
            var FchFnUnixInicio = System.Convert.ToInt64((inicio - start).TotalSeconds);
            var FchFnUnixFin = System.Convert.ToInt64((fin - start).TotalSeconds);
            string fecha_final = "/Date(" + FchFnUnixFin + "000-0500)/";
            string fecha_inicial = "/Date(" + FchFnUnixInicio + "000-0500)/";
            GuardarSuenoData nuevo = new GuardarSuenoData();
            nuevo.comentarios = comentarios;
            nuevo.fechaFin = fecha_final;
            nuevo.fechaInicio = fecha_inicial;
            nuevo.id = id;
            nuevo.sql_id = sqlId;
            nuevo.tipoActividadId = tipoActividad;
            nuevo.usuarioId = usuarioId;
            var jsonData = JsonConvert.SerializeObject(nuevo);
            string postData = jsonData;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            //Console.WriteLine(responseFromServer);
            reader.Close();
            dataStream.Close();
            response.Close();
            /***
             * 
             * Este método sera el encargada de realizar las inserciones
             * de los sueños.
             * 
             ***/

            return "salida sueños";
        }
        [Route("GetSuenos/test")]
        [HttpGet]
        public String GetAllSuenos(String comentarios,int usuarioId)
        {
            /***
             * Este método sera el encargada de realizar las inserciones
             * de los sueños.
             * **/

            return "salida sueños desde test";
        }

    }
    public class Data {
        public int usuario_id;
        public string fechaInicial { get; set; }
        public string fechaFinal { get; set; }
    }
    public class GuardarSuenoData {
        public String comentarios { get; set; }
        public String fechaFin { get; set; }
        public String fechaInicio { get; set; }
        public String id { get; set; }
        public String sql_id { get; set; }
        public String tipoActividadId { get; set; }
        public String usuarioId { get; set; }
       
    }
    public class Response
    {
        public String IsMexApp { get; set; }
        public String usuario { get; set; }
        public String color_id { get; set; }
        public String comentarios { get; set; }
        public String fechaFin { get; set; }
        public String fechaFinDate { get; set; }
        public String fechaInicio { get; set; }
        public String fechaInicioDate { get; set; }
        public String id { get; set; }
        public String int_automatica { get; set; }
        public String tipoActividad { get; set; }
        public int auto_increment { get; set; }

    }
}
