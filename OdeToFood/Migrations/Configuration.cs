//In package manager console
//Enable-Migrations -ContextTypeName OdeToFoodDb
//Update-Database

//https://code.msdn.microsoft.com/101-LINQ-Samples-3fb9811b
//https://www.linqpad.net/

namespace OdeToFood.Migrations
{
    using OdeToFood.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OdeToFood.Models.OdeToFoodDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "OdeToFood.Models.OdeToFoodDb";
        }

        private Random rnd = new Random();

        protected override void Seed(OdeToFood.Models.OdeToFoodDb context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            //context.Restaurants.AddOrUpdate(r => r.Name,
            //    new Restaurant { Name = "Name1", City = "City1", Country = "Country1" },
            //    new Restaurant { Name = "Name2", City = "City2", Country = "Country2" },
            //    new Restaurant
            //    {
            //        Name = "Name3",
            //        City = "City3",
            //        Country = "Country3",
            //        Reviews = new List<RestaurantReview> {
            //            new RestaurantReview { Rating=9, Body="Great Food"}
            //        }
            //    });

            List<Restaurant> restaurants = new List<Restaurant>();

            for (int i = 1; i < 200; i++)
            {
                string num = i.ToString();

                Restaurant restaurant = new Restaurant
                {
                    Name = "Name_" + num,
                    City = "City_" + num,
                    Country = "Country_" + num
                };

                if (i>100)
                {
                    List<RestaurantReview> reviews = new List<RestaurantReview>();

                    for (int j = 1; j < 6; j++)
                    {
                        RestaurantReview review = new RestaurantReview();
                        review.Rating = rnd.Next(0, 10);
                        review.Body = "This is review number " + j.ToString() + " for restaurant number " + num;
                        reviews.Add(review);
                    }

                    restaurant.Reviews = reviews;
                }

                restaurants.Add(restaurant);
            }

            foreach (Restaurant restaurant in restaurants)
            {
                context.Restaurants.Add(restaurant);
            }
        }
    }
}