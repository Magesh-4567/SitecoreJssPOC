using Datamatics.Models;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.LayoutService.Configuration;
using Sitecore.LayoutService.ItemRendering.ContentsResolvers;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Datamatics.Resolvers
{
    public class HeaderItemResolver : IRenderingContentsResolver
    {
        public bool IncludeServerUrlInMediaUrls { get; set; }
        public bool UseContextItem { get; set; }
        public string ItemSelectorQuery { get; set; }
        public NameValueCollection Parameters { get; set; }
        public object ResolveContents(Rendering rendering, IRenderingConfiguration renderingConfig)
        {
            Item contextItem = Sitecore.Context.Database.GetItem("/sitecore/content/sitecorejsspoc/Content/PageComponents/Header Component");
            if (contextItem != null)
            {
                HeaderModel headerModel = new HeaderModel();
                headerModel.headerTitle = contextItem.Fields["headerTitle"]?.Value;
                headerModel.backgroundColor = contextItem.Fields["backgroundColor"]?.Value;
                LinkField homePageLinkField = contextItem.Fields["homePageItem"];
                var s = homePageLinkField.GetFriendlyUrl();
                headerModel.homePageUrl = Sitecore.Links.LinkManager.GetItemUrl(homePageLinkField.TargetItem);

                MultilistField multilistField = contextItem.Fields["mainNavigationItems"];
                Item[] items = multilistField.GetItems();

                if (items != null && items.Length > 0)
                {
                    headerModel.mainNavigationItems = new List<MenuItem>();
                    foreach (var item in items)
                    {
                        MenuItem menuItem = new MenuItem();
                        menuItem.linkText = item.Fields["linkText"]?.Value;
                        menuItem.linkUrl = item.Fields["linkUrl"]?.Value;
                        var childItems = item.GetChildren();
                        if (childItems != null && childItems.Count > 0)
                        {
                            menuItem.subNavigationItems = new List<MenuItem>();
                            foreach (Item childItem in childItems)
                            {
                                MenuItem subMenuItem = new MenuItem();
                                subMenuItem.linkText = childItem.Fields["linkText"]?.Value;
                                subMenuItem.linkUrl = childItem.Fields["linkUrl"]?.Value;
                                menuItem.subNavigationItems.Add(subMenuItem);
                            }
                        }
                        headerModel.mainNavigationItems.Add(menuItem);
                    }
                }
                return new
                {
                    headerNavResults = headerModel
                };
            }
            return null;
        }
    }
}