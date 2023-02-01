using Datamatics.Models;
using Sitecore.Data.Fields;
using Sitecore.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Sitecore.Data;
using System.Web.Http.Cors;

namespace Datamatics.Controllers
{
    public class RestaurantController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public IEnumerable<RestaurantMenuItem> FetchMenuPrice(GetMenuPriceRequest request)
        {
            Database contextDatabase = Sitecore.Configuration.Factory.GetDatabase(request.ContextDatabaseName);
            Language language = Language.Parse(request.ContextLanguageName);
            var contextItem = contextDatabase.GetItem(new ID(request.ContextItemId), language);

            MultilistField menuItemListField = contextItem.Fields["menu"];
            var response = menuItemListField.GetItems().Select(x => new RestaurantMenuItem()
            {
                ItemName = x.Fields["itemName"].Value,
                ItemType = x.Fields["itemType"].Value,
                ItemPrice = x.Fields["itemPrice"].Value,
                ItemDetail = x.Fields["itemDetail"].Value,
            });

            return response;
        }
    }
}
