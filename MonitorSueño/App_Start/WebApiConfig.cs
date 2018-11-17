using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MonitorSueño
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{user_id}/{inicio}/{fin}",
                defaults: new {
                    id = RouteParameter.Optional,
                    inicio = RouteParameter.Optional,
                    fin=RouteParameter.Optional
                }
            );
        }
    }
}
