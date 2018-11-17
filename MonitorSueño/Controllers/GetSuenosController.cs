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

            return lista;
        }
        [HttpGet]
        public String GetAllSuenos()
        {

            return "salida sueños";
        }

    }
    public class Data {
        public int usuario_id;
        public string fechaInicial { get; set; }
        public string fechaFinal { get; set; }
    }
    public class Response
    {
        public String IsMexApp { get; set; }
        public String usuario { get; set; }
        public String color_id { get; set; }
        public String comenatarios { get; set; }
        public String fechaFin { get; set; }
        public String fechaFinDate { get; set; }
        public String fechaInicio { get; set; }
        public String fechaInicioDate { get; set; }
        public String id { get; set; }
        public String int_automatica { get; set; }
        public String tipoActividad { get; set; }


    }
}
