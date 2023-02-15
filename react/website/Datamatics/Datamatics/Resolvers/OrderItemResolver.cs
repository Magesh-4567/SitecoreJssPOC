using Datamatics.Models;
using Newtonsoft.Json;
using Sitecore.LayoutService.Configuration;
using Sitecore.LayoutService.ItemRendering.ContentsResolvers;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Datamatics.Resolvers
{
    public class OrderItemResolver : IRenderingContentsResolver
    {
        public bool IncludeServerUrlInMediaUrls { get; set; }
        public bool UseContextItem { get; set; }
        public string ItemSelectorQuery { get; set; }
        public NameValueCollection Parameters { get; set; }
        public object ResolveContents(Rendering rendering, IRenderingConfiguration renderingConfig)
        {
            string orderId = HttpContext.Current.Request.QueryString["orderId"].ToString();
            if (string.IsNullOrEmpty(orderId))
                return null;

            orderId = HttpUtility.UrlDecode(orderId);
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri("https://run.mocky.io/")
                };

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, "/v3/e60d05d1-3960-44d3-b24a-e71d0e6555e7");

                var response = Task.Run(async () => await client.SendAsync(requestMessage));
                var responseStream = Task.Run(async () => await response.Result.Content.ReadAsStreamAsync());
                string content = string.Empty;
                using (StreamReader stream = new StreamReader(responseStream.Result, true))
                {
                    content = stream.ReadToEnd();
                }

                List<OrderItem> ordersList = JsonConvert.DeserializeObject<List<OrderItem>>(content);

                if (ordersList != null && ordersList.Any())
                {
                    var orderItem = ordersList.Where(x => x.orderId == orderId).FirstOrDefault();

                    return new
                    {
                        orderResult = orderItem
                    };
                }
            }

            catch(Exception ex)
            {

            }
            return null;
        }
    }
}