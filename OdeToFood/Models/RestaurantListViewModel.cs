using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OdeToFood.Models
{
    public class RestaurantListViewModel
    {
        public IPagedList<RestaurantViewModel> withReviews { get; set; }
        public IPagedList<RestaurantViewModel> withoutReviews { get; set; }
    }
}