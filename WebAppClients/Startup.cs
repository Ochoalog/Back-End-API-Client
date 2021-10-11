using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Swashbuckle.Application;
using System;
using System.Threading.Tasks;
using System.Web.Http;

[assembly: OwinStartup(typeof(WebAppClients.Startup))]

namespace WebAppClients
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.EnableSwagger(c => {
                c.SingleApiVersion("v1", "WebAppClients");
                c.IncludeXmlComments(AppDomain.CurrentDomain.BaseDirectory + @"\bin\WebAppClients.xml");
            });

            app.UseCors(CorsOptions.AllowAll);

            ActiveAcessTokens(app);

            app.UseWebApi(config);
        }

        private void ActiveAcessTokens(IAppBuilder app)
        {
            var optionsConfigToken = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                Provider = new ProviderTokenAcess()
            };

            app.UseOAuthAuthorizationServer(optionsConfigToken);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
