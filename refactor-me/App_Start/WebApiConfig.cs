using System.Web.Http;
using Microsoft.Practices.Unity;
using refactor_me.Api;
using refactor_me.Services;

namespace refactor_me
{
    public static class WebApiConfig 
    {
        public static void Register(HttpConfiguration config)
        {
            // Unity configurations
            var container = new UnityContainer();
            container.RegisterType<IProductsService, ProductsService>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductOptionsService, ProductOptionsService>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductOptionsDatabase, ProductOptionsDatabase>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductsDatabase, ProductsDatabase>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            // Web API configuration and services
            var formatters = GlobalConfiguration.Configuration.Formatters;
            formatters.Remove(formatters.XmlFormatter);
            formatters.JsonFormatter.Indent = true;

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
