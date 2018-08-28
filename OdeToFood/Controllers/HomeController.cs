using OdeToFood.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        private OdeToFoodDb _db = new OdeToFoodDb();

        public ActionResult AutoComplete(string term)
        {
            IQueryable<object> model = _db.Restaurants
                .Where(restaurant => restaurant.Name.Contains(term))
                .Take(10)
                .Select(restaurant => new
                {
                    label = restaurant.Name
                });
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(
            string searchTerm = null,
            int withReviewsPage = 0,
            int withoutReviewsPage = 0)
        {
            Boolean withReviewsChanged = false;
            Boolean withoutReviewsChanged = false;

            if (withReviewsPage != 0)
            {
                Session["WithReviewsPage"] = withReviewsPage;
                withReviewsChanged = true;
            }

            if (withoutReviewsPage != 0)
            {
                Session["WithoutReviewsPage"] = withoutReviewsPage;
                withoutReviewsChanged = true;
            }

            if(searchTerm != null || Session["SearchTerm"] == null)
            {
                Session["withReviewsPage"] = 1;
                withReviewsChanged = true;

                Session["withoutReviewsPage"] = 1;
                withoutReviewsChanged = true;
            }

            if ( searchTerm != null )
            {
                Session["SearchTerm"] = searchTerm;

            } else if(Session["SearchTerm"] == null)
            {
                Session["SearchTerm"] = "";
            }

            ListWithReviewsViewModel reviewsModel = new ListWithReviewsViewModel((string)Session["SearchTerm"], (int)Session["withReviewsPage"]);
            ListWithoutReviewsViewModel withoutReviewsModel = new ListWithoutReviewsViewModel((string)Session["SearchTerm"], (int)Session["withoutReviewsPage"]);
            RestaurantListViewModel model = new RestaurantListViewModel();

            model.withReviews = reviewsModel.restaurantsWithReviews;
            model.withoutReviews = withoutReviewsModel.restaurantsWithoutReviews;

            ViewBag.RouteData = RouteData.Values;

            if (Request.IsAjaxRequest() && withReviewsChanged)
            {
                return PartialView("_WithReviewsList", model.withReviews);
            }

            if (Request.IsAjaxRequest() && withoutReviewsChanged)
            {
                return PartialView("_WithoutReviewsList", model.withoutReviews);
            }

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.RouteData = RouteData.Values;

            var model = new AboutModel
            {
                Name = "gfunk",
                Location = "myLocation"
            };

            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.RouteData = RouteData.Values;
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (_db != null)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}