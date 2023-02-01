using Sitecore.Pipelines;
using System.Web.Http;

namespace Datamatics.Pipelines
{
    public class RegisterDatamaticsApiRoutes
    {
        public void Process(PipelineArgs args)
        {
            var config = GlobalConfiguration.Configuration;
            config.Routes.MapHttpRoute("DatamaticsApiRoute",
                                       "datamaticsapi/{controller}/{action}/{id}",
                                       new { id = RouteParameter.Optional });
        }
    }
}