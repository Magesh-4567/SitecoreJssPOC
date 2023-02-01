using System;
using Sitecore.LayoutService.Configuration;
using Sitecore.LayoutService.ItemRendering.ContentsResolvers;
using Sitecore.Mvc.Presentation;
using System.Collections.Specialized;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using Datamatics.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.ContentSearch.Linq;
using Newtonsoft.Json.Linq;

namespace Datamatics.Resolvers
{
    public class SearchResultFromTagsResolver : IRenderingContentsResolver
    {
        public bool IncludeServerUrlInMediaUrls { get; set; }
        public bool UseContextItem { get; set; }
        public string ItemSelectorQuery { get; set; }
        public NameValueCollection Parameters { get; set; }
        public object ResolveContents(Rendering rendering, IRenderingConfiguration renderingConfig)
        {
            string searchQuery = HttpContext.Current.Request.QueryString["searchQuery"].ToString();
            if(string.IsNullOrEmpty(searchQuery))
                return null;

            searchQuery = HttpUtility.UrlDecode(searchQuery);

            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri("https://v168q.wiremockapi.cloud/")
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, "/json/getattractiontags");

            var response = Task.Run(async () => await client.SendAsync(requestMessage));
            var responseStream = Task.Run(async () => await response.Result.Content.ReadAsStreamAsync());
            string content = string.Empty;
            using (StreamReader stream = new StreamReader(responseStream.Result, true))
            {
                content = stream.ReadToEnd();
            }
            List<ExternalTagListMapping> tagListMappings = JsonConvert.DeserializeObject<List<ExternalTagListMapping>>(content);

            var tagListForQueryString = tagListMappings?.Where(x => x.tagslist.Any(y => string.Equals(y.tagitem, searchQuery, StringComparison.OrdinalIgnoreCase))).Select(x => x.id).ToList();

            string dbName = Sitecore.Context.Database.Name;
            ISearchIndex index = ContentSearchManager.GetIndex($"sitecore_{dbName}_index");

            using(IProviderSearchContext context = index.CreateSearchContext())
            {
                var filterByTemplateName = PredicateBuilder.True<SearchResultItem>(); 
                
                filterByTemplateName = filterByTemplateName.Or(x => x.TemplateName == "RestaurantPage");
                filterByTemplateName = filterByTemplateName.Or(x => x.TemplateName == "AdventurePage");
                filterByTemplateName = filterByTemplateName.Or(x => x.TemplateName == "LeisurePage"); 
                
                var appliedFilter = PredicateBuilder.True<SearchResultItem>();
                appliedFilter = appliedFilter.And(filterByTemplateName); 
                
                var query = context.GetQueryable<SearchResultItem>().Filter(appliedFilter); 
                
                List<SearchResultItem> sitecoreResults = new List<SearchResultItem>();
                foreach (var res in query)
                {
                    try
                    {
                        var priceKey = res.Fields["priceapikey_t"]?.ToString();
                        if (tagListForQueryString.Contains(priceKey))
                        {
                            sitecoreResults.Add(res);
                        }
                    }
                    catch(KeyNotFoundException ex)
                    {
                        
                    }
                }

                var responseSitecoreResults = sitecoreResults.Select(x => new AttractionResultItem
                {
                    Title = x.Fields["title_t"]?.ToString(),
                    Description = x.Fields["description_t"]?.ToString()
                }).ToList();

                return new
                {
                    tagListResults = responseSitecoreResults
                };
            }
        }
    }
}