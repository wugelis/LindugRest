﻿using System.Web;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using LindugRest.Formatters;
using LindugRest.Models;
using Microsoft.Data.Edm;

namespace LindugRest
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{ID}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Add(new JpegMediaTypeFormatter(HttpContext.Current.Server.MapPath("~/Images")));

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            config.EnableQuerySupport();


            ODataModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<Book>("hypermediaBooks");
            IEdmModel model = modelBuilder.GetEdmModel();
            config.Routes.MapODataRoute("ODataRoute", "odata", model);


            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();
        }
    }
}
