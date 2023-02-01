using System.Collections.Generic;

namespace Datamatics.Models
{
    public class MenuItem
    {
        public string linkText { get; set; }
        public string linkUrl { get; set; }
        public List<MenuItem> subNavigationItems { get; set; }

    }
}