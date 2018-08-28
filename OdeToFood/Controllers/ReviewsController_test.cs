using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace OdeToFood.Controllers
{
    public class ReviewsController_test : Controller
    {
        public ActionResult BestReview()
        {
            RestaurantReview bestReview = _reviewsList.OrderBy(review => -review.Rating).ToList().First();
            return PartialView("_Review", bestReview);
        }

        // GET: Reviews
        public ActionResult Index()
        {
            return View(_reviewsList);
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int id)
        {
            return View(_reviewsList.Find(review => review.Id == id));
        }

        // GET: Reviews/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reviews/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                RestaurantReview newReview = new RestaurantReview();

                newReview.Id = _reviewsList.Last().Id + 1;
                newReview.RestaurantId = 1;
                newReview.Rating = Int32.Parse(collection["rating"]);
                newReview.Body = collection["body"];

                _reviewsList.Add(newReview);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int id)
        {
            RestaurantReview reviewToEdit = _reviewsList.Find(review => review.Id == id);
            return View(reviewToEdit);
        }

        // POST: Reviews/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            RestaurantReview reviewToEdit = _reviewsList.Find(review => review.Id == id);
            if (TryUpdateModel(reviewToEdit))
            {
                return RedirectToAction("Index");
            }

            return View(reviewToEdit);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int id)
        {
            RestaurantReview reviewToDelete = _reviewsList.Find(review => review.Id == id);
            return View(reviewToDelete);
        }

        // POST: Reviews/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                _reviewsList.RemoveAll(review => review.Id == id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private static List<RestaurantReview> _reviewsList = new List<RestaurantReview>
        {
            new RestaurantReview
            {
                Id = 1,
                RestaurantId = 1,
                Rating = 10,
                Body = "AAAAAAAAAAAAAAA"
            },
            new RestaurantReview
            {
                Id = 2,
                RestaurantId = 1,
                Rating =10,
                Body="BBBBBBBBBBBB"
            },
            new RestaurantReview
            {
                Id = 3,
                RestaurantId = 1,
                Rating = 10,
                Body = "CCCCCCCCCCCCCC"
            }
        };
    }
}