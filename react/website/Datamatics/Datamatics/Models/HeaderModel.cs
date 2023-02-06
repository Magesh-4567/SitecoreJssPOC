using System.Collections.Generic;

namespace Datamatics.Models
{
    public class HeaderModel
    {
        public string headerTitle { get; set; }
        public string backgroundColor { get; set; }
        public string homePageUrl { get; set; }
        public List<MenuItem> mainNavigationItems { get; set; }
    }
}