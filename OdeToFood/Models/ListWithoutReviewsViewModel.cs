using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OdeToFood.Models
{
    public class ListWithoutReviewsViewModel
    {
        public string _searchTerm;
        public int _withoutReviewsPage;
        private OdeToFoodDb _db = new OdeToFoodDb();

        public ListWithoutReviewsViewModel(string searchTerm, int withoutReviewsPage)
        {
            _searchTerm = searchTerm;
            _withoutReviewsPage = withoutReviewsPage;
        }

        public IPagedList<RestaurantViewModel> restaurantsWithoutReviews
        {
            get
            {
                return _db.Restaurants
                .OrderBy(restaurant => restaurant.Name)
                .Where(restaurant => restaurant.Reviews.Count() == 0 && (_searchTerm == null || restaurant.Name.Contains(_searchTerm)))
                .Select(restaurant => new RestaurantViewModel
                {
                    Id = restaurant.Id,
                    Name = restaurant.Name,
                    City = restaurant.City,
                    Country = restaurant.Country,
                    CountOfReviews = restaurant.Reviews.Count(),
                }).ToPagedList(_withoutReviewsPage, 10);
            }

            set
            {
            }
        }
    }
}