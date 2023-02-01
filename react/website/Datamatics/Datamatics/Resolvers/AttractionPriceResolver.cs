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

namespace Datamatics.Resolvers
{
    public class AttractionPriceResolver : IRenderingContentsResolver
    {
        public bool IncludeServerUrlInMediaUrls { get; set; }
        public bool UseContextItem { get; set; }
        public string ItemSelectorQuery { get; set; }
        public NameValueCollection Parameters { get; set; }

        public object ResolveContents(Rendering rendering, IRenderingConfiguration renderingConfig)
        {
            var contextItem = Sitecore.Context.Item;
            if (contextItem != null)
            {
                var PriceAPIKey = contextItem.Fields["priceAPIKey"]?.Value;
                if (!string.IsNullOrEmpty(PriceAPIKey))
                {
                    HttpClient client = new HttpClient
                    {
                        BaseAddress = new Uri("https://v168q.wiremockapi.cloud/")
                    };

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, "/json/getattractionprices");

                    var response = Task.Run(async () => await client.SendAsync(requestMessage));
                    var responseStream = Task.Run(async () => await response.Result.Content.ReadAsStreamAsync());
                    string content = string.Empty;
                    using (StreamReader stream = new StreamReader(responseStream.Result, true))
                    {
                        content = stream.ReadToEnd();
                    }

                    List<AttractionPrice> attractionPrices = JsonConvert.DeserializeObject<List<AttractionPrice>>(content);
                    var priceData = attractionPrices.Where(x => x.id == PriceAPIKey).FirstOrDefault();

                    return new
                    {
                        attractionId = PriceAPIKey,
                        attractionPrice = priceData
                    };
                }
                return null;
            }
            return null;
        }
    }
}