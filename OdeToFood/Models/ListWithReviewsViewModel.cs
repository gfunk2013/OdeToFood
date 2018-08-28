using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OdeToFood.Models
{
    public class ListWithReviewsViewModel
    {
        public string _searchTerm;
        public int _withReviewsPage;
        private OdeToFoodDb _db = new OdeToFoodDb();

        public ListWithReviewsViewModel(string searchTerm, int withReviewsPage)
        {
            _searchTerm = searchTerm;
            _withReviewsPage = withReviewsPage;
        }

        public IPagedList<RestaurantViewModel> restaurantsWithReviews
        {
            get
            {
                return _db.Restaurants
                .OrderByDescending(restaurant =>
                    restaurant.Reviews.Average(review =>
                        review.Rating))
                .ThenBy(restaurant => restaurant.Name)
                .Where(restaurant => restaurant.Reviews.Count() != 0 && (_searchTerm == null || restaurant.Name.Contains(_searchTerm)))
                .Select(restaurant => new RestaurantViewModel
                {
                    Id = restaurant.Id,
                    Name = restaurant.Name,
                    City = restaurant.City,
                    Country = restaurant.Country,
                    CountOfReviews = restaurant.Reviews.Count(),
                    SumOfReviews = restaurant.Reviews.Sum(review =>
                        review.Rating)
                }).ToPagedList(_withReviewsPage, 10);
            }

            set
            {
            }
        }
    }
}